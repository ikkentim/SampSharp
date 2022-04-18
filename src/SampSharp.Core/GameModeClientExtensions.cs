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
using System.Reflection;
using SampSharp.Core.Callbacks;

namespace SampSharp.Core;

/// <summary>Contains <see cref="IGameModeClient" /> extension methods.</summary>
public static class GameModeClientExtensions
{
    /// <summary>Registers all callbacks in the specified target object. Instance methods with a <see cref="CallbackAttribute" /> attached will be loaded.</summary>
    /// <param name="gameModeClient">The game mode client.</param>
    /// <param name="target">The target.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell",
        "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields",
        Justification = "Loading entry points of any visibility")]
    public static void RegisterCallbacksInObject(this IGameModeClient gameModeClient, object target)
    {
        if (target == null) throw new ArgumentNullException(nameof(target));

        foreach (var method in target.GetType()
                     .GetTypeInfo()
                     .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
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