using System;
using System.Linq;
using GameMode;
using GameMode.Definitions;
using GameMode.Events;
using GameMode.World;

namespace TestMode.World
{
    public class MyPlayer : Player
    {
        protected MyPlayer(int playerId) : base(playerId)
        {
        }

        /// <summary>
        /// Get sor sets this Player's Database ID (Test purpose, not connected to a DB)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets whether this Player has logged in.
        /// </summary>
        public bool LoggedIn { get; set; }

        /// <summary>
        /// Returns an instance of <see cref="MyPlayer"/> that deals with <paramref name="playerId"/>.
        /// </summary>
        /// <param name="playerId">The ID of the player we are dealing with.</param>
        /// <returns>An instance of <see cref="MyPlayer"/>.</returns>
        public static new MyPlayer Find(int playerId)
        {
            //Find player in memory or initialize new player
            return Instances.Cast<MyPlayer>().FirstOrDefault(p => p.PlayerId == playerId) ?? new MyPlayer(playerId);
        }

        public static new void RegisterEvents(BaseMode gameMode)
        {
            RegisterEvents(gameMode, Find);
        }

        public override void OnConnected(PlayerEventArgs e)
        {
            var dialog = new Dialog(DialogStyle.Password, Color.Red + "Gimme yo password",
                Color.White + "For this test, it will be 'mono'", "Login");
            dialog.Response += (sender, args) =>
            {
                var sendingDialog = sender as Dialog;

                if (args.InputText == "mono")
                {
                    //Log in
                    var sendingPlayer = args.Player as MyPlayer;
                    sendingPlayer.LoggedIn = true;
                    Native.SendClientMessage(sendingPlayer.PlayerId, Color.GreenYellow, "You logged in!");
                }
                else
                {
                    //Re-enter
                    sendingDialog.Message = Color.Red + "INVALID PASSWORD!\n" + Color.White + "For this test, it will be 'mono'";
                    sendingDialog.Show(args.Player);
                }
            };
            dialog.Show(e.Player);

            base.OnConnected(e);

        }
    }
}
