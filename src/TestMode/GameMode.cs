// SampSharp
// Copyright 2017 Tim Potze
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
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using SampSharp.GameMode;
using SampSharp.GameMode.API;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;
using TestMode.Tests;
using TestMode.World;

namespace TestMode
{
    public class GameMode : BaseMode
    {
        #region Overrides of BaseMode

        protected override void OnInitialized(EventArgs args)
        {
            base.OnInitialized(args);
            
            Console.WriteLine($"TestMode for SampSharp v{GetType().Assembly.GetName().Version}");
            Console.WriteLine("----------------------");

            Server.ToggleDebugOutput(true);

            SetGameModeText("sa-mp# testmode");

            UsePlayerPedAnimations();

            AddPlayerClass(65, new Vector3(5), 0);
            
            foreach (
                var test in
                    GetType()
                        .Assembly.GetTypes()
                        .Where(t => t.IsClass)
                        .Where(typeof (ITest).IsAssignableFrom)
                        .Select(t => Activator.CreateInstance(t) as ITest))
            {
                Console.WriteLine();
                Console.WriteLine("=========");
                Console.WriteLine(test);
                Console.WriteLine("=========");
                test.Start(this);
                Console.WriteLine($"Test {test} completed.");
                Console.WriteLine();
            }

            var pooledTypes = new[]
            {
                typeof(IdentifiedPool<>),
                typeof(IdentifiedOwnedPool<,>)
            };


            Console.WriteLine(DateTime.Now);
            foreach (var type in new[] { typeof(GameMode).Assembly, typeof(BaseMode).Assembly }
                .SelectMany(a => a.GetTypes())
                .Where(t => !t.IsGenericType && t.IsClass)
                .Select(t1 => pooledTypes.Select(t2 => GetBaseTypeOfGenericType(t1, t2))
                    .FirstOrDefault(t => t != null))
                .Where(t => t != null)
                .Distinct())
            {
                Console.WriteLine($"Pool: {type.GetGenericArguments().FirstOrDefault()} \t=> {type.GetProperty("InstanceType", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)?.GetValue(null)}");
            }

            throw new Exception();
        }

        #endregion


        private Type GetBaseTypeOfGenericType(Type type, Type genericType)
        {
            if (type == null)
                return null;
            if (genericType == null)
                throw new ArgumentNullException(nameof(genericType));
            
            if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
            {
                return type;
            }
            
            if ((type.BaseType == null) || (type.BaseType == typeof(object)))
                return null;
            
            return GetBaseTypeOfGenericType(type.BaseType, genericType);
        }

    }
}