using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using SampSharp.Core.Callbacks;

namespace SampSharp.Core
{
    /// <summary>
    /// Contains <see cref="IGameModeClient" /> extension methods.
    /// </summary>
    public static class GameModeClientExtensions
    {
        /// <summary>
        ///     Registers all callbacks in the specified target object. Instance methods with a <see cref="CallbackAttribute" />
        ///     attached will be loaded.
        /// </summary>
        /// <param name="gameModeClient">The game mode client.</param>
        /// <param name="target">The target.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = "Loading entry points of any visibility")]
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
