using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SampSharp.Entities;

/// <summary>Provides extended functionality for configuring a <see cref="IEcsBuilder" /> instance.</summary>
public static class EcsBuilderUseMiddlewareExtensions
{
    private static readonly MethodInfo _getServiceInfo =
        typeof(EcsBuilderUseMiddlewareExtensions).GetMethod(nameof(GetService), BindingFlags.NonPublic | BindingFlags.Static)!;
    
    /// <summary>Adds a middleware to the handler of the event with the specified <paramref name="name" />.</summary>
    /// <param name="builder">The ECS builder to add the middleware to.</param>
    /// <param name="name">The name of the event.</param>
    /// <param name="middleware">The middleware to add to the event.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IEcsBuilder UseMiddleware(this IEcsBuilder builder, string name, Func<EventDelegate, EventDelegate> middleware)
    {
        builder.Services.GetRequiredService<IEventDispatcher>().UseMiddleware(name, middleware);
        return builder;
    }

    /// <summary>Adds a middleware to the handler of the event with the specified <paramref name="name" />.</summary>
    /// <param name="builder">The ECS builder to add the middleware to.</param>
    /// <param name="name">The name of the event.</param>
    /// <param name="middleware">The middleware to add to the event.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IEcsBuilder UseMiddleware(this IEcsBuilder builder, string name, Func<EventContext, Func<object?>, object?> middleware)
    {
        return builder.UseMiddleware(name, next =>
        {
            return context =>
            {
                return middleware(context, SimpleNext);

                object? SimpleNext()
                {
                    return next(context);
                }
            };
        });
    }

    /// <summary>Adds a middleware of the specified type <typeparamref name="TMiddleware" /> to the handler of the event
    /// with the specified <paramref name="name" />.</summary>
    /// <typeparam name="TMiddleware">The type of the middleware.</typeparam>
    /// <param name="builder">The ECS builder to add the middleware to.</param>
    /// <param name="name">The name of the event.</param>
    /// <param name="args">The arguments for the constructor of the event.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IEcsBuilder UseMiddleware<TMiddleware>(this IEcsBuilder builder, string name, params object[] args)
    {
        return builder.UseMiddleware(name, typeof(TMiddleware), args);
    }

    /// <summary>Adds a middleware of the specified type <paramref name="middleware" /> to the handler of the event with
    /// the specified <paramref name="name" />.</summary>
    /// <param name="builder">The ECS builder to add the middleware to.</param>
    /// <param name="name">The name of the event.</param>
    /// <param name="middleware">The type of the middleware.</param>
    /// <param name="args">The arguments for the constructor of the event.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IEcsBuilder UseMiddleware(this IEcsBuilder builder, string name, Type middleware, params object[] args)
    {
        var applicationServices = builder.Services;
        return builder.UseMiddleware(name, next =>
        {
            var methods = middleware.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            var invokeMethods = methods.Where(m => string.Equals(m.Name, "Invoke", StringComparison.Ordinal))
                .ToArray();

            if (invokeMethods.Length != 1)
            {
                throw new InvalidOperationException();
            }

            var methodInfo = invokeMethods[0];

            var parameters = methodInfo.GetParameters();
            if (parameters.Length < 1 || parameters[0]
                    .ParameterType != typeof(EventContext))
            {
                throw new InvalidOperationException();
            }

            var ctorArgs = new object[args.Length + 1];
            ctorArgs[0] = next;
            Array.Copy(args, 0, ctorArgs, 1, args.Length);
            var instance = ActivatorUtilities.CreateInstance(applicationServices, middleware, ctorArgs);
            if (parameters.Length == 1)
            {
                return methodInfo.CreateDelegate<EventDelegate>(instance);
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
            var parameterType = parameters[i]
                .ParameterType;
            if (parameterType.IsByRef)
            {
                throw new NotSupportedException();
            }

            var parameterTypeExpression = new Expression[] { providerArg, Expression.Constant(parameterType, typeof(Type)) };

            var getServiceCall = Expression.Call(_getServiceInfo, parameterTypeExpression);
            methodArguments[i] = Expression.Convert(getServiceCall, parameterType);
        }

        Expression middlewareInstanceArg = instanceArg;
        if (methodInfo.DeclaringType != typeof(T))
        {
            middlewareInstanceArg = Expression.Convert(middlewareInstanceArg, methodInfo.DeclaringType!);
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