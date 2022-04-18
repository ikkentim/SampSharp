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
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Tools;

/// <summary>Contains methods for awaiting calls. Throws <see cref="PlayerDisconnectedException" /> if the player has disconnected.</summary>
/// <typeparam name="TArguments"></typeparam>
public class ASyncPlayerWaiter<TArguments> : ASyncWaiter<BasePlayer, TArguments>
{
    /// <summary>Waits for the <see cref="ASyncWaiter{TKey,TArguments}.Fire" /> to be called with the specified <paramref name="key" />.</summary>
    /// <param name="key">The key.</param>
    /// <returns>The arguments passed to the <see cref="ASyncWaiter{TKey,TArguments}.Fire" /> method.</returns>
    public override async Task<TArguments> Result(BasePlayer key)
    {
        try
        {
            return await base.Result(key);
        }
        catch (TaskCanceledException e)
        {
            throw new PlayerDisconnectedException("The player has left the server.", e);
        }
    }
}