using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            gameMode.PlayerSpawned += (sender, args) =>
            {
                (sender as GtaPlayer).GiveWeapon(Weapon.AK47, 100);
            };
            actor.PlayerGiveDamage += (sender, args) =>
            {
                args.OtherPlayer.SendClientMessage("You damaged an actor ({0} {1} {2})", args.Weapon, args.Amount, args.BodyPart);
                args.OtherPlayer.SendClientMessage("New health: {0}", actor.Health);

            };
            actor.StreamIn += (sender, args) =>
            {
                args.Player.SendClientMessage("Actor streamed in!");
            };
            actor.StreamOut += (sender, args) =>
            {
                args.Player.SendClientMessage("Actor streamed out!");
            };
        }

        #endregion
    }
}
