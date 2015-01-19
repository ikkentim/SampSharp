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

using SampSharp.GameMode.Definitions;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerEnterExitModShop" /> event.
    /// </summary>
    public class PlayerEnterModShopEventArgs : PlayerEventArgs
    {
        public PlayerEnterModShopEventArgs(int playerid, EnterExit enterExit, int interiorid) : base(playerid)
        {
            EnterExit = enterExit;
            InteriorId = interiorid;
        }

        public EnterExit EnterExit { get; private set; }

        public int InteriorId { get; private set; }
    }
}