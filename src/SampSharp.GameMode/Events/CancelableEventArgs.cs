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
using SampSharp.GameMode.SAMP;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for events in <see cref="KeyHandlerSet" />.
    /// </summary>
    public class CancelableEventArgs : EventArgs
    {
        /// <summary>
        ///     Gets a value indicating whether this event should not be trigger events with lower priorities.
        /// </summary>
        public bool IsCanceled { get; set; }
    }
}