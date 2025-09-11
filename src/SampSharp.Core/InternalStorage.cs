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

namespace SampSharp.Core;

/// <summary>Storage for some values used internally in <see cref="InternalStorage" />.</summary>
internal static class InternalStorage
{
    /// <summary>The currently running client.</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Critical Code Smell", "S2223:Non-constant static fields should not be visible",
        Justification = "By design")]
    public static IGameModeClient RunningClient;

    public static void SetRunningClient(IGameModeClient client)
    {
        RunningClient = client;
    }
}