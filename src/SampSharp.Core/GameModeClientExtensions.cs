using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using SampSharp.Core.Callbacks;

namespace SampSharp.Core
{
    /// <summary>
    /// Contains <see cref="IGameModeClient"/> extension methods.
    /// </summary>
    public static class GameModeClientExtensions
    {
        /// <summary>
        ///     Registers a callback with the specified <paramref name="name" />. When the callback is called, the specified
        ///     <paramref name="methodInfo" /> will be invoked on the specified <paramref name="target" />.
        /// </summary>
        /// <param name="gameModeClient">The game mode client.</param>
        /// <param name="name">The name af the callback to register.</param>
        /// <param name="target">The target on which to invoke the method.</param>
        /// <param name="methodInfo">The method information of the method to invoke when the callback is called.</param>
        public static void RegisterCallback(this IGameModeClient gameModeClient, string name, object target, MethodInfo methodInfo)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));
            
            var parameterInfos = methodInfo.GetParameters();
            var parameters = new CallbackParameterInfo[parameterInfos.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                if (Callback.IsValidValueType(parameterInfos[i].ParameterType))
                    parameters[i] = CallbackParameterInfo.Value;
                else if (Callback.IsValidArrayType(parameterInfos[i].ParameterType))
                {
                    var attribute = parameterInfos[i].GetCustomAttribute<ParameterLengthAttribute>();
                    if (attribute == null)
                        throw new CallbackRegistrationException("Parameters of array types must have an attached ParameterLengthAttribute.");
                    parameters[i] = CallbackParameterInfo.Array(attribute.Index);
                }
                else if (Callback.IsValidStringType(parameterInfos[i].ParameterType))
                    parameters[i] = CallbackParameterInfo.String;
                else
                    throw new CallbackRegistrationException("The method contains unsupported parameter types");
            }

            gameModeClient.RegisterCallback(name, target, methodInfo, parameters);
        }

        /// <summary>
        ///     Registers a callback with the specified <paramref name="name" />. When the callback is called, the specified
        ///     <paramref name="methodInfo" /> will be invoked on the specified <paramref name="target" />.
        /// </summary>
        /// <param name="gameModeClient">The game mode client.</param>
        /// <param name="name">The name af the callback to register.</param>
        /// <param name="target">The target on which to invoke the method.</param>
        /// <param name="methodInfo">The method information of the method to invoke when the callback is called.</param>
        public static void RegisterCallback(this IGameModeClient gameModeClient, string name, object target, MethodInfo methodInfo, Type[] parameterTypes)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));
            
            var parameters = new CallbackParameterInfo[parameterTypes.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                if (Callback.IsValidValueType(parameterTypes[i]))
                    parameters[i] = CallbackParameterInfo.Value;
                else if (Callback.IsValidArrayType(parameterTypes[i]))
                    throw new CallbackRegistrationException(
                        "Parameters of array types must specify a parameter length.");
                else if (Callback.IsValidStringType(parameterTypes[i]))
                    parameters[i] = CallbackParameterInfo.String;
                else
                    throw new CallbackRegistrationException("The method contains unsupported parameter types");
            }

            gameModeClient.RegisterCallback(name, target, methodInfo, parameters, parameterTypes);
        }

        /// <summary>
        ///     Registers all callbacks in the specified target object. Instance methods with a <see cref="CallbackAttribute" />
        ///     attached will be loaded.
        /// </summary>
        /// <param name="gameModeClient">The game mode client.</param>
        /// <param name="target">The target.</param>
        public static void RegisterCallbacksInObject(this IGameModeClient gameModeClient, object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            
            foreach (var method in target.GetType().GetTypeInfo().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var attribute = method.GetCustomAttribute<CallbackAttribute>();

                if (attribute == null)
                    continue;

                var name = attribute.Name;
                if (string.IsNullOrEmpty(name))
                    name = method.Name;

                gameModeClient.RegisterCallback(name, target, method);
            }
        }
    }
}
