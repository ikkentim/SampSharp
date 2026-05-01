using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SampSharp.Entities;

/// <summary>
/// Provides a compiler for an invoke method for an instance method with injected dependencies and entity-to-component conversion.
/// </summary>
public static class MethodInvokerFactory
{
    private static readonly MethodInfo _getComponentInfo = typeof(IEntityManager).GetMethod(nameof(IEntityManager.GetComponent),
        BindingFlags.Public | BindingFlags.Instance, null, [typeof(EntityId)], null)!;

    private static readonly MethodInfo _getServiceInfo =
        typeof(MethodInvokerFactory).GetMethod(nameof(GetService), BindingFlags.NonPublic | BindingFlags.Static)!;

    /// <summary>
    /// Compiles the invoker for the specified method.
    /// </summary>
    /// <param name="methodInfo">The method information.</param>
    /// <param name="parameterSources">The sources of the parameters.</param>
    /// <param name="uninvokedReturnValue">The value returned if the method is not invoked when a parameter could not be converted to the correct component.</param>
    /// <param name="retBoolToResult">Indicates whether the <see langword="bool" /> return value should be converted to a <see cref="MethodResult" /> value.</param>
    /// <returns>The method invoker.</returns>
    public static MethodInvoker Compile(MethodInfo methodInfo, MethodParameterSource[] parameterSources, object? uninvokedReturnValue = null, bool retBoolToResult = true)
    {
        if (methodInfo.DeclaringType == null)
        {
            throw new ArgumentException("Method must have declaring type", nameof(methodInfo));
        }

        // Input arguments
        var instanceArg = Expression.Parameter(typeof(object), "instance");
        var argsArg = Expression.Parameter(typeof(object[]), "args");
        var serviceProviderArg = Expression.Parameter(typeof(IServiceProvider), "serviceProvider");
        var entityManagerArg = Expression.Parameter(typeof(IEntityManager), "entityManager");
        var entityEmpty = Expression.Constant(EntityId.Empty, typeof(EntityId));
        Expression? argsCheckExpression = null;

        var locals = new List<ParameterExpression>();
        var expressions = new List<Expression>();
        var methodArguments = new Expression[parameterSources.Length];

        for (var i = 0; i < parameterSources.Length; i++)
        {
            var source = parameterSources[i];
            var parameterType = source
                .Info.ParameterType;
            if (parameterType.IsByRef)
            {
                throw new NotSupportedException("Reference parameters are not supported");
            }

            if (source.IsComponent)
            {
                // Get component from entity

                // Declare local variables
                var entityArg = Expression.Parameter(typeof(EntityId), $"entity{i}");
                var componentArg = Expression.Parameter(source
                        .Info.ParameterType, $"component{i}");
                var componentNull = Expression.Constant(null, source.Info.ParameterType);

                locals.Add(entityArg);
                locals.Add(componentArg);

                // Constant index in args array
                Expression index = Expression.Constant(source.ParameterIndex);

                // Assign entity from args array to entity variable.
                var getEntityExpression = Expression.Assign(entityArg, Expression.Convert(Expression.ArrayIndex(argsArg, index), typeof(EntityId)));
                expressions.Add(getEntityExpression);

                // If entity is not null, convert entity to component. Assign component to component variable.
                var getComponentInfo = _getComponentInfo.MakeGenericMethod(source.Info.ParameterType);
                var getComponentExpression = Expression.Assign(componentArg,
                    Expression.Condition(Expression.Equal(entityArg, entityEmpty), componentNull,
                        Expression.Call(entityManagerArg, getComponentInfo, entityArg)));
                expressions.Add(getComponentExpression);

                // If an entity was provided in the args list, the entity must be convertible to the component. Add
                // check for entity to either be null or the component to not be null.
                var checkExpression = Expression.OrElse(Expression.Equal(entityArg, entityEmpty), Expression.NotEqual(componentArg, componentNull));

                argsCheckExpression = argsCheckExpression == null
                    ? checkExpression
                    : Expression.AndAlso(argsCheckExpression, checkExpression);

                // Add component variable as the method argument.
                methodArguments[i] = componentArg;
            }
            else if (source.IsService)
            {
                // Get service
                var getServiceCall = Expression.Call(_getServiceInfo, serviceProviderArg, Expression.Constant(parameterType, typeof(Type)));
                methodArguments[i] = Expression.Convert(getServiceCall, parameterType);
            }
            else if (source.ParameterIndex >= 0)
            {
                // Pass through. Args come in as boxed object[]; an exact unbox-to-T cast
                // (Expression.Convert(object, T)) only succeeds when the boxed value's
                // runtime type is exactly T. open.mp dispatchers commonly hand us a
                // uint where the [Event] handler signature wants int (and similar
                // numeric mismatches across enums) — for those we route through
                // Convert.ChangeType so a boxed uint → int conversion succeeds.
                Expression index = Expression.Constant(source.ParameterIndex);
                var getValue = Expression.ArrayIndex(argsArg, index);

                if (RequiresPrimitiveConversion(parameterType))
                {
                    var convertMethod = typeof(MethodInvokerFactory).GetMethod(
                        nameof(ConvertNumeric), BindingFlags.NonPublic | BindingFlags.Static)!;
                    var converted = Expression.Call(convertMethod, getValue, Expression.Constant(parameterType, typeof(Type)));
                    methodArguments[i] = Expression.Convert(converted, parameterType);
                }
                else
                {
                    methodArguments[i] = Expression.Convert(getValue, parameterType);
                }
            }
        }

        var service = Expression.Convert(instanceArg, methodInfo.DeclaringType);
        Expression body = Expression.Call(service, methodInfo, methodArguments);

        if (body.Type == typeof(void))
        {
            body = Expression.Block(body, Expression.Constant(null));
        }
        else if (retBoolToResult && body.Type == typeof(bool))
        {
            var boxMethod = typeof(MethodResult).GetMethod(nameof(MethodResult.From))!;
            body = Expression.Call(boxMethod, body);
        }
        else if (body.Type != typeof(object))
        {
            body = Expression.Convert(body, typeof(object));
        }

        if (argsCheckExpression != null)
        {
            body = Expression.Condition(argsCheckExpression, body, Expression.Constant(uninvokedReturnValue, typeof(object)));
        }

        if (locals.Count > 0 || expressions.Count > 0)
        {
            expressions.Add(body);
            body = Expression.Block(locals, expressions);
        }

        var lambda = Expression.Lambda<MethodInvoker>(body, instanceArg, argsArg, serviceProviderArg, entityManagerArg);

        return lambda.Compile();
    }

    private static object GetService(IServiceProvider serviceProvider, Type type)
    {
        return serviceProvider.GetRequiredService(type);
    }

    /// <summary>
    /// Returns <see langword="true" /> for primitive-numeric / enum types where the
    /// dispatcher might box a value of a different (but assignable) numeric type
    /// (uint↔int, ushort↔int, the enum's underlying type, etc.).
    /// </summary>
    private static bool RequiresPrimitiveConversion(Type t)
    {
        if (t.IsEnum) return true;
        return Type.GetTypeCode(t) is
            TypeCode.SByte or TypeCode.Byte or
            TypeCode.Int16 or TypeCode.UInt16 or
            TypeCode.Int32 or TypeCode.UInt32 or
            TypeCode.Int64 or TypeCode.UInt64 or
            TypeCode.Single or TypeCode.Double or
            TypeCode.Char;
    }

    /// <summary>
    /// Runtime numeric/enum coercion. Same-type pass-through; otherwise routes
    /// through <see cref="Convert.ChangeType(object,Type,IFormatProvider)" />
    /// (or <see cref="Enum.ToObject(Type,object)" /> for enum targets). When
    /// the source isn't <see cref="IConvertible" /> (e.g. a Vector3 mistakenly
    /// landed at an int slot due to dispatcher arg-order mismatch), returns
    /// the value as-is so the outer <see cref="Expression.Convert(Expression,Type)" />
    /// throws an <see cref="InvalidCastException" /> with the actual managed
    /// types — much easier to debug than "Object must implement IConvertible".
    /// </summary>
    private static object? ConvertNumeric(object? value, Type targetType)
    {
        if (value is null) return null;
        var sourceType = value.GetType();
        if (sourceType == targetType) return value;

        if (value is not IConvertible) return value;

        if (targetType.IsEnum)
        {
            var underlying = Enum.GetUnderlyingType(targetType);
            var asUnderlying = sourceType == underlying
                ? value
                : Convert.ChangeType(value, underlying, CultureInfo.InvariantCulture);
            return Enum.ToObject(targetType, asUnderlying);
        }
        return Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
    }
}