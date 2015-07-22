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

using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    public class ActorTest : ITest
    {
        #region Implementation of ITest

        public void Start(GameMode gameMode)
        {
            var actor = Actor.Create(0, new Vector3(10, 10, 2), 0);
            actor.Health = 100;

            gameMode.PlayerSpawned += (sender, args) => { (sender as BasePlayer).GiveWeapon(Weapon.AK47, 100); };
            actor.PlayerGiveDamage += (sender, args) =>
            {
                args.OtherPlayer.SendClientMessage("You damaged an actor ({0} {1} {2})", args.Weapon, args.Amount,
                    args.BodyPart);
                args.OtherPlayer.SendClientMessage("New health: {0}", actor.Health);
            };
            actor.StreamIn += (sender, args) => { args.Player.SendClientMessage("Actor streamed in!"); };
            actor.StreamOut += (sender, args) => { args.Player.SendClientMessage("Actor streamed out!"); };
        }

        #endregion
    }
}