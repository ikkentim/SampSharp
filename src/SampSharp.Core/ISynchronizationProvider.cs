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

namespace SampSharp.Core;

/// <summary>Contains the methods of a provider of synchronization to the main thread.</summary>
public interface ISynchronizationProvider
{
    /// <summary>Gets a value indicating whether an invoke is required to synchronize to the main tread.</summary>
    public bool InvokeRequired { get; }

    /// <summary>Invokes the specified action on the main thread.</summary>
    /// <param name="action">The action to invoke on the main thread.</param>
    public void Invoke(Action action);
}