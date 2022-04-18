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

using Microsoft.Extensions.DependencyInjection;

namespace SampSharp.Entities;

/// <summary>Contains the functionality of a class which provides a startup configuration for an EntitySystemComponent game mode.</summary>
public interface IStartup
{
    /// <summary>Configures the specified service collection by adding or removing required services.</summary>
    /// <param name="services">The service collection to configure.</param>
    void Configure(IServiceCollection services);

    /// <summary>Configures the specified ECS builder by enabling, disabling and configuring modules in the game mode.</summary>
    /// <param name="builder">The ECS builder to configure.</param>
    void Configure(IEcsBuilder builder);
}