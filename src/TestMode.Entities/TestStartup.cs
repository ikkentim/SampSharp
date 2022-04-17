// SampSharp
// Copyright 2020 Tim Potze
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
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Core.Natives;
using SampSharp.Core.Natives.NativeObjects;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TestMode.Entities.Services;

namespace TestMode.Entities
{
    public class TestStartup : IStartup
    {
        public void Configure(IServiceCollection services)
        {
            // Load services (e.g. repositories), systems, etc.
            services
                .AddTransient<IVehicleRepository, VehicleRepository>()
                .AddTransient<IFunnyService, FunnyService>()
                .AddScoped<IScopedFunnyService, FunnyService>()
                .AddSystemsInAssembly();
        }

        public void Configure(IEcsBuilder builder)
        {
            builder.EnableSampEvents()
                .EnablePlayerCommands()
                .EnableRconCommands()
                .EnableEventScope("OnPlayerConnect")
                .EnableEventScope("OnPlayerText");

            builder.EnableEvent<int, int>("TestCallback");

            // Load middleware:
            // Can also be loaded by systems which are IConfiguringSystem
            builder.UseMiddleware("OnGameModeInit", (_, next) =>
            {
                Console.WriteLine("I am middleware for OnGameModeInit!");
                return next();
            });

            builder.UseMiddleware("OnPlayerText", (ctx, next) =>
            {
                if (ctx.Arguments[1] is string txt && txt.Contains("I dislike SampSharp"))
                    return null;
                return next();
            });

            WarmUpNativeObjects();
        }

        private void WarmUpNativeObjects()
        {
            // Warm up native objects for profiling purposes

            // Components
            NativeObjectProxyFactory.CreateInstance<NativeActor>();
            NativeObjectProxyFactory.CreateInstance<NativeGangZone>();
            NativeObjectProxyFactory.CreateInstance<NativeMenu>();
            NativeObjectProxyFactory.CreateInstance<NativeObject>();
            NativeObjectProxyFactory.CreateInstance<NativePickup>();
            NativeObjectProxyFactory.CreateInstance<NativePlayer>();
            NativeObjectProxyFactory.CreateInstance<NativePlayerObject>();
            NativeObjectProxyFactory.CreateInstance<NativePlayerTextDraw>();
            NativeObjectProxyFactory.CreateInstance<NativePlayerTextLabel>();
            NativeObjectProxyFactory.CreateInstance<NativeTextDraw>();
            NativeObjectProxyFactory.CreateInstance<NativeTextLabel>();
            NativeObjectProxyFactory.CreateInstance<NativeVehicle>();

            // Services
            NativeObjectProxyFactory.CreateInstance<ServerServiceNative>();
            NativeObjectProxyFactory.CreateInstance<VehicleInfoServiceNative>();
            NativeObjectProxyFactory.CreateInstance<WorldServiceNative>();
        }
    }
}