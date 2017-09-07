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

namespace SampSharp.Core
{
    /// <summary>
    ///     Contains the version of the SampSharp.Core package.
    /// </summary>
    internal static class CoreVersion
    {
        /// <summary>
        ///     Gets the version of the SampSharp.Core package.
        /// </summary>
        public static Version Version { get; } = new Version(0, 8, 0);

        /// <summary>
        ///     Gets the version of the communication protocol used to communicate with the SampSharp server.
        /// </summary>
        public static uint ProtocolVersion { get; } = 2;
    }
}