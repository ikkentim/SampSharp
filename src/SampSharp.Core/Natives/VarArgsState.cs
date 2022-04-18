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
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SampSharp.Core.Natives;

/// <summary>Provides a state for variable arguments handling of native calls.</summary>
public class VarArgsState : IDisposable
{
    private List<GCHandle> _pinnedHandles;

    /// <summary>Pins a buffer which will be freed once this state is disposed of.</summary>
    /// <param name="buffer">The buffer to pin.</param>
    /// <returns>The address at which the buffer has been pinned.</returns>
    public int PinBuffer(object buffer)
    {
        _pinnedHandles ??= new List<GCHandle>();

        var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        _pinnedHandles.Add(handle);

        var ptr = handle.AddrOfPinnedObject();

        return ptr.ToInt32();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_pinnedHandles == null)
        {
            return;
        }

        foreach (var handle in _pinnedHandles)
        {
            handle.Free();
        }

        _pinnedHandles = null;

        GC.SuppressFinalize(this);
    }
}