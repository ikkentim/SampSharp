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

namespace SampSharp.Entities.Utilities;

/// <summary>
/// Invoker for an instance method with dependency injection.
/// </summary>
/// <param name="target">The target instance to invoke the method on.</param>
/// <param name="args">The arguments of the method excluding the injected dependencies.</param>
/// <param name="services">The service provider from which dependencies are loaded.</param>
/// <param name="entityManager">The entity manager from which components are loaded.</param>
/// <returns>The result of the method.</returns>
public delegate object MethodInvoker(object target, object[] args, IServiceProvider services, IEntityManager entityManager);