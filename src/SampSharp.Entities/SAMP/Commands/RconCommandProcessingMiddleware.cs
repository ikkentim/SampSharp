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

using SampSharp.Core.Logging;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Represents a middleware which lets unhandled OnRconCommand events be processed by the <see cref="IRconCommandService" />.
/// </summary>
public class RconCommandProcessingMiddleware
{
    private readonly EventDelegate _next;
        
    /// <summary>
    /// Initializes a new instance of the <see cref="RconCommandProcessingMiddleware" /> class.
    /// </summary>
    /// <param name="next">The next middleware handler.</param>
    public RconCommandProcessingMiddleware(EventDelegate next)
    {
        _next = next;
    }
        
    /// <summary>
    /// Invokes the middleware.
    /// </summary>
    public object Invoke(EventContext context, IRconCommandService commandService)
    {
        var result = _next(context);

        if (EventHelper.IsSuccessResponse(result))
            return result;

        if (context.Arguments[0] is string text)
            return commandService.Invoke(context.EventServices, text);

        CoreLog.Log(CoreLogLevel.Error, "Invalid command middleware input argument types!");
        return null;

    }
}