﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SampSharp.Core.Callbacks;
using SampSharp.Core.Logging;

namespace SampSharp.Core.Hosting;

internal class NewCallback
{
    private readonly INewCallbackParameter[] _parameters;
    private readonly object _target;
    private readonly object[] _parametersBuffer;
    private readonly FastMethodInfo _fastMethod;
    private readonly object[] _wrapBuffer;

    public NewCallback(INewCallbackParameter[] parameters, object target, MethodInfo method, bool wrapped)
    {
        _parameters = parameters;
        _parametersBuffer = new object[parameters.Length];
        _target = target;
        _fastMethod = new FastMethodInfo(method);

        if (wrapped) _wrapBuffer = new object[] { _parametersBuffer };
    }

    private static INewCallbackParameter ParameterForType1(Type type)
    {
        if (type == typeof(int))
        {
            return NewCallbackParameterInt.Instance;
        }

        if (type == typeof(bool))
        {
            return NewCallbackParameterBoolean.Instance;
        }

        if (type == typeof(float))
        {
            return NewCallbackParameterSingle.Instance;
        }

        if (type == typeof(string))
        {
            return NewCallbackParameterString.Instance;
        }

        return null;
    }

    private static INewCallbackParameter ParameterForType2(Type type, int offset)
    {
        if (type == typeof(int[]))
        {
            return new NewCallbackParameterIntArray(offset);
        }

        if (type == typeof(bool[]))
        {
            return new NewCallbackParameterBooleanArray(offset);
        }

        if (type == typeof(float[]))
        {
            return new NewCallbackParameterSingleArray(offset);
        }

        return null;
    }

    public static NewCallback For(object target, MethodInfo method, Type[] parameterTypes = null,
        uint?[] lengthIndices = null)
    {
        var wrapped = false;
        var methodParameters = method.GetParameters();
        if (methodParameters.Length == 1 && methodParameters[0].ParameterType == typeof(object[]))
        {
            wrapped = true;
        }

        if (wrapped)
        {
            parameterTypes ??= Type.EmptyTypes;
        }
        else
        {
            if (parameterTypes == null)
            {
                parameterTypes = methodParameters.Select(x => x.ParameterType).ToArray();
            }
            else
            {
                if (parameterTypes.Length != methodParameters.Length)
                {
                    throw new ArgumentException(
                        "Value does not match method parameters. The specified method should either only accept an array of objects, or the value should be should be null or match the method parameters.",
                        nameof(parameterTypes));
                }
                for (var i = 0; i < parameterTypes.Length; i++)
                {
                    if (parameterTypes[i] != methodParameters[i].ParameterType)
                    {
                        throw new ArgumentException(
                            $"Type at index {i} does not match the method parameters. The specified method should either only accept an array of objects, or the value should be should be null or match the method parameters.",
                            nameof(parameterTypes));
                    }
                }
            }
        }

        var parameters = new INewCallbackParameter[parameterTypes.Length];

        if (lengthIndices != null && lengthIndices.Length != parameterTypes.Length)
        {
            throw new ArgumentException("lengthIndices length must be same as parameterTypes length",
                nameof(lengthIndices));
        }

        for (var i = 0; i < parameterTypes.Length; i++)
        {
            parameters[i] = ParameterForType1(parameterTypes[i]);

            if (parameters[i] != null)
            {
                continue;
            }

            var index = (int?)lengthIndices?[i] ?? (i + 1);
            var offset = index - i;

            if (index >= parameterTypes.Length || index < 0)
            {
                throw new InvalidOperationException("Callback parameter length index out of bounds.");
            }

            if (parameterTypes[index] != typeof(int))
            {
                throw new ArgumentException(
                    $"Expected an int argument at index {index} for the parameter at index {i}.", nameof(method));
            }

            parameters[i] = ParameterForType2(parameterTypes[i], offset);

            if (parameters[i] != null)
            {
                continue;
            }

            throw new CallbackRegistrationException("Unknown callback parameter type.");
        }

        return new NewCallback(parameters, target, method, true);
    }

    public static NewCallback For(object target, MethodInfo method)
    {
        var methodParameters = method.GetParameters();
        var parameters = methodParameters.Select(parameter =>
            {
                var par = ParameterForType1(parameter.ParameterType);

                if (par != null)
                {
                    return par;
                }

                var attribute =
                    CustomAttributeExtensions
                        .GetCustomAttribute<ParameterLengthAttribute>((ParameterInfo)parameter);
                var index = (int?)attribute?.Index ?? parameter.Position + 1;
                var offset = index - parameter.Position;


                if (index >= methodParameters.Length)
                {
                    throw new InvalidOperationException("Callback parameter length index out of bounds.");
                }

                par = ParameterForType2(parameter.ParameterType, offset);

                if (par != null)
                {
                    return par;
                }

                throw new CallbackRegistrationException("Unknown callback parameter type.");
            })
            .ToArray();

        return new NewCallback(parameters, target, method, false);
    }

    public class FastMethodInfo
    {
        private delegate object ReturnValueDelegate(object instance, object[] arguments);
        private delegate void VoidDelegate(object instance, object[] arguments);

        public FastMethodInfo(MethodInfo methodInfo)
        {
            var instanceExpression = Expression.Parameter(typeof(object), "instance");
            var argumentsExpression = Expression.Parameter(typeof(object[]), "arguments");
            var argumentExpressions = new List<Expression>();
            var parameterInfos = methodInfo.GetParameters();
            for (var i = 0; i < parameterInfos.Length; ++i)
            {
                var parameterInfo = parameterInfos[i];
                argumentExpressions.Add(Expression.Convert(Expression.ArrayIndex(argumentsExpression, Expression.Constant(i)), parameterInfo.ParameterType));
            }
            var callExpression = Expression.Call(!methodInfo.IsStatic ? Expression.Convert(instanceExpression, methodInfo.ReflectedType ?? methodInfo.DeclaringType!) : null, methodInfo, argumentExpressions);
            if (callExpression.Type == typeof(void))
            {
                var voidDelegate = Expression.Lambda<VoidDelegate>(callExpression, instanceExpression, argumentsExpression).Compile();
                Delegate = (instance, arguments) => { voidDelegate(instance, arguments); return null; };
            }
            else
                Delegate = Expression.Lambda<ReturnValueDelegate>(Expression.Convert(callExpression, typeof(object)), instanceExpression, argumentsExpression).Compile();
        }

        private ReturnValueDelegate Delegate { get; }

        public object Invoke(object instance, object[] arguments)
        {
            return Delegate(instance, arguments);
        }
    }

    public unsafe void Invoke(IntPtr amx, IntPtr parameters, IntPtr retval)
    {
        var paramCount = *(int*)parameters.ToPointer() / 4; // cell size

        if (paramCount != _parameters.Length)
        {
            CoreLog.Log(CoreLogLevel.Error,
                $"Callback parameter mismatch. Expected {_parameters.Length} but received {paramCount} parameters.");
            return;
        }

        var args = _parametersBuffer;// new object[paramCount];

        for (var i = 0; i < paramCount; i++)
        {
            var param = _parameters[i];
            args[i] = param.GetValue(amx, IntPtr.Add(parameters, 4 + (4 * i)));
        }

        if (_wrapBuffer != null)
        {
            args = _wrapBuffer;
        }

            
        // var result = _method.Invoke(_target, args);
        var result = _fastMethod.Invoke(_target, args);

        if (retval != IntPtr.Zero)
        {
            *(int*)retval = ObjectToInt(result);
        }

        // to avoid holding on to handles, clear buffer
        for (var i = 0; i < paramCount; i++)
        {
            _parametersBuffer[i] = null;
        }
    }

    private static int ObjectToInt(object obj)
    {
        return obj switch
        {
            bool value => value ? 1 : 0,
            int value => value,
            float value => ValueConverter.ToInt32(value),
            _ => 1
        };
    }
}