// SampSharp
// Copyright 2022 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace SampSharp.Entities.Utilities;

/// <summary>Provides a compiler for an invoke method for an instance method with injected dependencies and entity-to-component conversion.</summary>
public static class MethodInvokerFactory
{
    private static readonly MethodInfo _getComponentInfo = typeof(IEntityManager).GetMethod(nameof(IEntityManager.GetComponent),
        BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(EntityId) }, null);

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell",
        "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = "Member of own type")]
    private static readonly MethodInfo _getServiceInfo =
        typeof(MethodInvokerFactory).GetMethod(nameof(GetService), BindingFlags.NonPublic | BindingFlags.Static);

    /// <summary>Compiles the invoker for the specified method.</summary>
    /// <param name="methodInfo">The method information.</param>
    /// <param name="parameterSources">The sources of the parameters.</param>
    /// <param name="uninvokedReturnValue">The value returned if the method is not invoked when a parameter could not be converted to the correct component.</param>
    /// <returns>The method invoker.</returns>
    public static MethodInvoker Compile(MethodInfo methodInfo, MethodParameterSource[] parameterSources, object uninvokedReturnValue = null)
    {
        if (methodInfo.DeclaringType == null)
            throw new ArgumentException("Method must have declaring type", nameof(methodInfo));

        // Input arguments
        var instanceArg = Expression.Parameter(typeof(object), "instance");
        var argsArg = Expression.Parameter(typeof(object[]), "args");
        var serviceProviderArg = Expression.Parameter(typeof(IServiceProvider), "serviceProvider");
        var entityManagerArg = Expression.Parameter(typeof(IEntityManager), "entityManager");
        var entityEmpty = Expression.Constant(EntityId.Empty, typeof(EntityId));
        Expression argsCheckExpression = null;

        var locals = new List<ParameterExpression>();
        var expressions = new List<Expression>();
        var methodArguments = new Expression[parameterSources.Length];

        for (var i = 0; i < parameterSources.Length; i++)
        {
            var parameterType = parameterSources[i]
                .Info.ParameterType;
            if (parameterType.IsByRef) throw new NotSupportedException("Reference parameters are not supported");

            if (parameterSources[i]
                .IsComponent)
            {
                // Get component from entity

                // Declare local variables
                var entityArg = Expression.Parameter(typeof(EntityId), $"entity{i}");
                var componentArg = Expression.Parameter(parameterSources[i]
                    .Info.ParameterType, $"component{i}");
                var componentNull = Expression.Constant(null, parameterSources[i]
                    .Info.ParameterType);

                locals.Add(entityArg);
                locals.Add(componentArg);

                // Constant index in args array
                Expression index = Expression.Constant(parameterSources[i]
                    .ParameterIndex);

                // Assign entity from args array to entity variable.
                var getEntityExpression = Expression.Assign(entityArg, Expression.Convert(Expression.ArrayIndex(argsArg, index), typeof(EntityId)));
                expressions.Add(getEntityExpression);

                // If entity is not null, convert entity to component. Assign component to component variable.
                var getComponentInfo = _getComponentInfo.MakeGenericMethod(parameterSources[i]
                    .Info.ParameterType);
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
            else if (parameterSources[i]
                     .IsService)
            {
                // Get service
                var getServiceCall = Expression.Call(_getServiceInfo, serviceProviderArg, Expression.Constant(parameterType, typeof(Type)));
                methodArguments[i] = Expression.Convert(getServiceCall, parameterType);
            }
            else if (parameterSources[i]
                         .ParameterIndex >= 0)
            {
                // Pass through
                Expression index = Expression.Constant(parameterSources[i]
                    .ParameterIndex);

                var getValue = Expression.ArrayIndex(argsArg, index);
                methodArguments[i] = Expression.Convert(getValue, parameterType);
            }
        }

        var service = Expression.Convert(instanceArg, methodInfo.DeclaringType);
        Expression body = Expression.Call(service, methodInfo, methodArguments);


        if (body.Type == typeof(void))
            body = Expression.Block(body, Expression.Constant(null));
        else if (body.Type != typeof(object))
            body = Expression.Convert(body, typeof(object));

        if (argsCheckExpression != null)
            body = Expression.Condition(argsCheckExpression, body, Expression.Constant(uninvokedReturnValue, typeof(object)));

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
        var service = serviceProvider.GetService(type);
        return service ?? throw new InvalidOperationException($"Service of type {type} is not available.");
    }
}