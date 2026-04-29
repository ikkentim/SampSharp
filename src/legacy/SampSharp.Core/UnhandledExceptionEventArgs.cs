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

/// <summary>Provides data for the <see cref="IGameModeClient.UnhandledException" /> event.</summary>
public class UnhandledExceptionEventArgs : EventArgs
{
    /// <summary>Initializes a new instance of the <see cref="UnhandledExceptionEventArgs" /> class.</summary>
    /// <param name="callbackName">The name of the callback during which the exception was thrown.</param>
    /// <param name="exception">The exception.</param>
    public UnhandledExceptionEventArgs(string callbackName, Exception exception)
    {
        CallbackName = callbackName;
        Exception = exception;
    }

    /// <summary>Gets or sets the name of the callback during which the exception was thrown.</summary>
    public string CallbackName { get; }

    /// <summary>Gets the exception.</summary>
    public Exception Exception { get; }
}