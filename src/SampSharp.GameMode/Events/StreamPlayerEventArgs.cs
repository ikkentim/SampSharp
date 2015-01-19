// SampSharp
// Copyright 2015 Tim Potze
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

using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerStreamIn" /> or
    ///     <see cref="BaseMode.PlayerStreamOut" /> event.
    /// </summary>
    public class StreamPlayerEventArgs : PlayerEventArgs
    {
        public StreamPlayerEventArgs(int playerid, int forplayerid) : base(playerid)
        {
            ForPlayerId = forplayerid;
        }

        public int ForPlayerId { get; private set; }

        public GtaPlayer ForPlayer
        {
            get { return ForPlayerId == GtaPlayer.InvalidId ? null : GtaPlayer.FindOrCreate(ForPlayerId); }
        }
    }
}