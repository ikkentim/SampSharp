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

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Represents a response to an invoked command.
/// </summary>
public struct InvokeResult
{
    /// <summary>
    /// A command not found result value.
    /// </summary>
    public static readonly InvokeResult CommandNotFound = new(InvokeResponse.CommandNotFound);

    /// <summary>
    /// The success result value.
    /// </summary>
    public static readonly InvokeResult Success = new(InvokeResponse.Success);

    /// <summary>
    /// Initializes a new instance of the <see cref="InvokeResult" /> struct.
    /// </summary>
    /// <param name="response">The response.</param>
    /// <param name="usageMessage">The usage message. This value should only be provided when <paramref name="response" /> is equal to <see cref="InvokeResponse.InvalidArguments" />.</param>
    public InvokeResult(InvokeResponse response, string usageMessage = null)
    {
        Response = response;
        UsageMessage = usageMessage;
    }

    /// <summary>
    /// Gets the response.
    /// </summary>
    public InvokeResponse Response { get; }

    /// <summary>
    /// Gets the usage message. This value is only available when <see cref="Response" /> is equal to <see cref="InvokeResponse.InvalidArguments" />.
    /// </summary>
    public string UsageMessage { get; }
}