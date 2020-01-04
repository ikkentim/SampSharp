// SampSharp
// Copyright 2019 Tim Potze
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
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TestMode.Entities.Services;
using TestMode.Entities.Systems;

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

                .AddSingleton<TestSystem>();
        }

        public void Configure(IEcsBuilder builder)
        {
            // Enable systems:
            builder.UseSystem<TestSystem>()
                .EnableSampEvents();

            // Load middlewares:
            // Can also be loaded by systems which are IConfiguringSystem
            builder.UseMiddleware("OnGameModeInit", (ctx, next) =>
            {
                Console.WriteLine("I am middleware for OnGameModeInit!");
                return next();
            });

            builder.UseMiddleware("OnPlayerText", (ctx, next) =>
            {
                // Having to access arguments by an indexed array isn't ideal, might work towards a dictionary structure.
                if (ctx.Arguments[1] is string txt && txt.Contains("I dislike SampSharp"))
                    return null;
                return next();
            });
        }
    }
}