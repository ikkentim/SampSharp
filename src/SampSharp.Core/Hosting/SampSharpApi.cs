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

using System.Runtime.InteropServices;

namespace SampSharp.Core.Hosting;

/// <summary>
/// Provides the functions and dat exposed by the SampSharp plugin.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly unsafe struct SampSharpApi
{
    /// <summary>
    /// The size of the <see cref="SampSharpApi"/> struct. Used for versioning.
    /// </summary>
    public readonly uint Size;
    /// <summary>
    /// The plugin data provided to the SampSharp plugin by SA-MP.
    /// </summary>
    public readonly PluginData* PluginData;
    /// <summary>
    /// The sampgdk_FindNative function.
    /// </summary>
    public readonly delegate* unmanaged[Stdcall] <byte*, void*> FindNative;
    /// <summary>
    /// The sampgdk_InvokeNativeArray function.
    /// </summary>
    public readonly delegate* unmanaged[Stdcall] <void*, byte*, int*, int> InvokeNative;
}