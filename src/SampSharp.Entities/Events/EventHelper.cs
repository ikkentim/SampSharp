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

using System.Threading.Tasks;

namespace SampSharp.Entities;

/// <summary>
/// Provides helper methods for event data.
/// </summary>
public static class EventHelper
{
    /// <summary>
    /// Gets a value indicating whether the specified <paramref name="eventResponse" /> indicates the event ran successfully. A
    /// neutral <c>null</c> response is not considered a success response.
    /// </summary>
    /// <param name="eventResponse">The event response to check.</param>
    /// <returns>
    /// <c>true</c> if the specified response indicates success; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsSuccessResponse(object eventResponse)
    {
        return !(eventResponse == null ||
                 eventResponse is false ||
                 eventResponse is 0 ||
                 eventResponse is Task<bool> tB && tB.IsCompleted && !tB.Result ||
                 eventResponse is Task<int> tI && tI.IsCompleted && tI.Result == 0);
    }
}