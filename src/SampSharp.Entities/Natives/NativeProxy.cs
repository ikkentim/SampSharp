﻿// SampSharp
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

using SampSharp.Core.Natives;
using SampSharp.Core.Natives.NativeObjects;

namespace SampSharp.Entities;

/// <summary>Provides a proxy object around a native object of type <typeparamref name="T" />.</summary>
/// <typeparam name="T">The type of the native object for which a proxy object should be provided.</typeparam>
public class NativeProxy<T> : INativeProxy<T> where T : class
{
    /// <summary>Initializes a new instance of the <see cref="NativeProxy{T}" /> class.</summary>
    public NativeProxy()
    {
        Instance = NativeObjectProxyFactory.CreateInstance<T>();
    }

    /// <inheritdoc />
    public T Instance { get; }
}