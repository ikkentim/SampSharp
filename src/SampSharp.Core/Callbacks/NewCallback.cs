using System;
using System.Linq;
using System.Reflection;
using SampSharp.Core.Communication;
using SampSharp.Core.Logging;

namespace SampSharp.Core.Callbacks;

internal class NewCallback
{
    private readonly INewCallbackParameter[] _parameters;
    private readonly object _target;
    private readonly MethodInfo _method;
    private readonly bool _wrapped;

    public NewCallback(INewCallbackParameter[] parameters, object target, MethodInfo method, bool wrapped)
    {
        _parameters = parameters;
        _target = target;
        _method = method;
        _wrapped = wrapped;
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

    public static NewCallback For(object target, MethodInfo method, Type[] parameterTypes, uint?[] lengthIndices = null)
    {
        var parameters = new INewCallbackParameter[parameterTypes.Length];

        if (lengthIndices != null && lengthIndices.Length != parameterTypes.Length)
        {
            throw new ArgumentException("lengthIndices length must be same as parameterTypes length", nameof(lengthIndices));
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
                
            if (index >= parameterTypes.Length)
            {
                throw new InvalidOperationException("Callback parameter length index out of bounds.");
            }

            // todo: check type of length? also in other method

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

                var attribute = CustomAttributeExtensions.GetCustomAttribute<ParameterLengthAttribute>((ParameterInfo)parameter);
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
        
    public unsafe void Invoke(IntPtr amx, IntPtr parameters, IntPtr retval)
    {
        var paramCount = *(int*)parameters.ToPointer() / 4; // cell size

        if (paramCount != _parameters.Length)
        {
            CoreLog.Log(CoreLogLevel.Error, $"Callback parameter mismatch. Expected {_parameters.Length} but received {paramCount} parameters.");
            return;
        }

        var args = new object[paramCount];

        for (var i = 0; i < paramCount; i++)
        {
            var param = _parameters[i];
            args[i] = param.GetValue(amx, IntPtr.Add(parameters, 4 + (4 * i)));
        }

        if (_wrapped)
        {
            args = new object[] { args };
        }
        var result = _method.Invoke(_target, args);

        if (retval != IntPtr.Zero)
        {
            *(int*)retval = ObjectToInt(result);
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