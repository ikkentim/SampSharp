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
using SampSharp.GameMode.Events;
using System;
using static SampSharp.GameMode.SAMP.Server;

namespace SampSharp.GameMode.World
{
    /// <summary>
    ///     Represents a SA-MP NPC
    /// </summary>
    public class NPC
    {
        internal string Name { get; }

        #region Constructors

        /// <summary>
        ///     Initialize a new instance of the <see cref="NPC"/> class.
        /// </summary>
        /// <param name="name">The name the NPC should connect as. Must follow the same rules as normal player names.</param>
        /// <param name="script">The NPC script name that is located in the npcmodes folder (without the .amx extension).</param>
        public NPC(string name, string script)
        {
            this.Name = name;
            BaseMode.Instance.PlayerConnected += (p, e) => this.OnNPCConnect(p as BasePlayer);
            ServerInternal.Instance.ConnectNPC(name, script);
        }
        #endregion

        #region Callbacks

        private void OnNPCConnect(BasePlayer p)
        {
            if(p.Name.Equals(Name) && p.IsNPC)
            {
                PlayerInstance = p;
                Connected?.Invoke(this, new NPCConnectionEventArgs(p));
            }

        }
        
        #endregion

        #region Properties

        /// <summary>
        ///     The <see cref="BasePlayer"/> of the NPC
        /// </summary>
        public BasePlayer PlayerInstance { get; set; }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the NPC instance has been connected
        /// </summary>
        public event EventHandler<NPCConnectionEventArgs> Connected;

        #endregion
    }
}
