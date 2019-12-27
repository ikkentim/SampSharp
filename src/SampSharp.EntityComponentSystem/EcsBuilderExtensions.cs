using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.EntityComponentSystem.Events;
using SampSharp.EntityComponentSystem.Systems;

namespace SampSharp.EntityComponentSystem
{
    public static class EcsBuilderExtensions
    {
        private static readonly MethodInfo GetServiceInfo = typeof(EcsBuilderExtensions).GetMethod(nameof(GetService), BindingFlags.NonPublic | BindingFlags.Static);

        public static IEcsBuilder UseSystem<T>(this IEcsBuilder builder) where T : ISystem
        {
            return builder.UseSystem(typeof(T));
        }

        public static IEcsBuilder Use(this IEcsBuilder builder, string name,
            Func<EventContext, Func<object>, object> middleware)
        {
            return builder.Use(name, next =>
                {
                    return context =>
                    {
                        object SimpleNext() => next(context);
                        return middleware(context, SimpleNext);
                    };
                });
        }

        public static IEcsBuilder UseMiddleware<TMiddleware>(this IEcsBuilder app, string name, params object[] args)
        {
            return app.UseMiddleware(name, typeof(TMiddleware), args);
        }

        public static IEcsBuilder UseMiddleware(this IEcsBuilder app, string name, Type middleware, params object[] args)
        {
            var applicationServices = app.Services;
            return app.Use(name, next =>
            {
                var methods = middleware.GetMethods(BindingFlags.Instance | BindingFlags.Public);
                var invokeMethods = methods.Where(m =>
                    string.Equals(m.Name, "Invoke", StringComparison.Ordinal)
                    ).ToArray();

                if (invokeMethods.Length != 1)
                {
                    throw new InvalidOperationException();
                }

                var methodInfo = invokeMethods[0];
 
                var parameters = methodInfo.GetParameters();
                if (parameters.Length < 1 || parameters[0].ParameterType != typeof(EventContext))
                {
                    throw new InvalidOperationException();
                }

                var ctorArgs = new object[args.Length + 1];
                ctorArgs[0] = next;
                Array.Copy(args, 0, ctorArgs, 1, args.Length);
                var instance = ActivatorUtilities.CreateInstance(app.Services, middleware, ctorArgs);
                if (parameters.Length == 1)
                {
                    return (EventDelegate)methodInfo.CreateDelegate(typeof(EventDelegate), instance);
                }

                var factory = Compile<object>(methodInfo, parameters);

                return context =>
                {
                    var serviceProvider = context.EventServices ?? applicationServices;
                    if (serviceProvider == null)
                    {
                        throw new InvalidOperationException();
                    }

                    return factory(instance, context, serviceProvider);
                };
            });
        }
        
        private static Func<T, EventContext, IServiceProvider, object> Compile<T>(MethodInfo methodInfo, ParameterInfo[] parameters)
        {
            var middleware = typeof(T);

            var eventContextArg = Expression.Parameter(typeof(EventContext), "eventContext");
            var providerArg = Expression.Parameter(typeof(IServiceProvider), "serviceProvider");
            var instanceArg = Expression.Parameter(middleware, "middleware");

            var methodArguments = new Expression[parameters.Length];
            methodArguments[0] = eventContextArg;
            for (var i = 1; i < parameters.Length; i++)
            {
                var parameterType = parameters[i].ParameterType;
                if (parameterType.IsByRef)
                {
                    throw new NotSupportedException();
                }

                var parameterTypeExpression = new Expression[]
                {
                    providerArg,
                    Expression.Constant(parameterType, typeof(Type)),
                };

                var getServiceCall = Expression.Call(GetServiceInfo, parameterTypeExpression);
                methodArguments[i] = Expression.Convert(getServiceCall, parameterType);
            }

            Expression middlewareInstanceArg = instanceArg;
            if (methodInfo.DeclaringType != typeof(T))
            {
                middlewareInstanceArg = Expression.Convert(middlewareInstanceArg, methodInfo.DeclaringType);
            }

            var body = Expression.Call(middlewareInstanceArg, methodInfo, methodArguments);

            var lambda = Expression.Lambda<Func<T, EventContext, IServiceProvider, object>>(body, instanceArg, eventContextArg, providerArg);

            return lambda.Compile();
        }

        private static object GetService(IServiceProvider sp, Type type)
        {
            var service = sp.GetService(type);
            return service ?? throw new InvalidOperationException();
        }
    }
}