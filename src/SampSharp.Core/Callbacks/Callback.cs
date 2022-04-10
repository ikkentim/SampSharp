using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SampSharp.Core.Logging;

namespace SampSharp.Core.Callbacks;

internal class Callback
{
    private readonly ICallbackParameter[] _parameters;
    private readonly object _target;
    private readonly object[] _parametersBuffer;
    private readonly FastMethodInfo _fastMethod;
    private readonly object[] _wrapBuffer;

    public Callback(ICallbackParameter[] parameters, object target, MethodInfo method, bool wrapped)
    {
        _parameters = parameters;
        _parametersBuffer = new object[parameters.Length];
        _target = target;
        _fastMethod = new FastMethodInfo(method);

        if (wrapped) _wrapBuffer = new object[] { _parametersBuffer };
    }

    public ICallbackParameter[] Parameters => _parameters;

    private static bool IsSupportedParameterType(Type type)
    {
        return type == typeof(int) || type == typeof(float) || type == typeof(bool) || type == typeof(int[]) ||
               type == typeof(float[]) || type == typeof(bool[]) || type == typeof(string);
    }

    private static ICallbackParameter ParameterForType1(Type type)
    {
        if (type == typeof(int))
        {
            return CallbackParameterInt.Instance;
        }

        if (type == typeof(bool))
        {
            return CallbackParameterBoolean.Instance;
        }

        if (type == typeof(float))
        {
            return CallbackParameterSingle.Instance;
        }

        if (type == typeof(string))
        {
            return CallbackParameterString.Instance;
        }

        return null;
    }

    private static ICallbackParameter ParameterForType2(Type type, int offset)
    {
        if (type == typeof(int[]))
        {
            return new CallbackParameterIntArray(offset);
        }

        if (type == typeof(bool[]))
        {
            return new CallbackParameterBooleanArray(offset);
        }

        if (type == typeof(float[]))
        {
            return new CallbackParameterSingleArray(offset);
        }

        return null;
    }

    public static Callback For(object target, MethodInfo method, Type[] parameterTypes = null,
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
                    throw new InvalidOperationException(
                        "Value does not match method parameters. The specified method should either only accept an array of objects, or the parameterTypes value should be null or match the method parameters.");
                }
                for (var i = 0; i < parameterTypes.Length; i++)
                {
                    if (parameterTypes[i] != methodParameters[i].ParameterType)
                    {
                        throw new InvalidOperationException(
                            $"Type at index {i} does not match the method parameters. The specified method should either only accept an array of objects, or the parameterTypes value should be null or match the method parameters.");
                    }
                }
            }
        }

        var parameters = new ICallbackParameter[parameterTypes.Length];

        if (lengthIndices != null && lengthIndices.Length != parameterTypes.Length)
        {
            throw new InvalidOperationException("lengthIndices length must be same as parameterTypes length");
        }

        for (var i = 0; i < parameterTypes.Length; i++)
        {
            GetParameter(parameterTypes, lengthIndices, i, parameters, wrapped, methodParameters);
        }

        return new Callback(parameters, target, method, wrapped);
    }

    private static void GetParameter(Type[] parameterTypes, uint?[] lengthIndices, int index, ICallbackParameter[] parameters,
        bool wrapped, ParameterInfo[] methodParameters)
    {
        if (!IsSupportedParameterType(parameterTypes[index]))
        {
            throw new InvalidOperationException($"Unsupported parameter type '{parameterTypes[index]}' at index {index}");
        }

        var parameter = ParameterForType1(parameterTypes[index]);

        if (parameter != null)
        {
            parameters[index] = parameter;
            return;
        }

        var lengthIndexNullable = (int?)lengthIndices?[index];

        if (lengthIndexNullable == null && !wrapped)
        {
            var attribute = methodParameters[index].GetCustomAttribute<ParameterLengthAttribute>();
            lengthIndexNullable = (int?)attribute?.Index;
        }

        var lengthIndex = lengthIndexNullable ?? (index + 1);
        var lengthIndexOffset = lengthIndex - index;

        if (lengthIndex >= parameterTypes.Length || lengthIndex < 0)
        {
            throw new InvalidOperationException("Callback parameter length index out of bounds.");
        }

        if (parameterTypes[lengthIndex] != typeof(int))
        {
            throw new InvalidOperationException(
                $"Expected an integer argument at index {lengthIndex} for the parameter at index {index}.");
        }

        parameter = ParameterForType2(parameterTypes[index], lengthIndexOffset);

        if (parameter != null)
        {
            parameters[index] = parameter;
            return;
        }

        throw new InvalidOperationException("Unknown callback parameter type.");
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

        var args = _parametersBuffer;

        for (var i = 0; i < paramCount; i++)
        {
            var param = _parameters[i];
            args[i] = param.GetValue(amx, IntPtr.Add(parameters, 4 + (4 * i)));
        }

        if (_wrapBuffer != null)
        {
            args = _wrapBuffer;
        }
        
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