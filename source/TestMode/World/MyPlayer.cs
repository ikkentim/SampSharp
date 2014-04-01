// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using System.Linq;
using GameMode;
using GameMode.Definitions;
using GameMode.Events;
using GameMode.World;

namespace TestMode.World
{
    public class MyPlayer : Player
    {
        protected MyPlayer(int id) : base(id)
        {
        }

        /// <summary>
        ///     Gets or sets this Player's Database ID (Test purpose, not connected to a DB)
        /// </summary>
        public int DbId { get; set; }

        /// <summary>
        ///     Gets or sets whether this Player has logged in.
        /// </summary>
        public bool LoggedIn { get; set; }

        /// <summary>
        ///     Returns an instance of <see cref="MyPlayer" /> that deals with <paramref name="playerId" />.
        /// </summary>
        /// <param name="playerId">The ID of the player we are dealing with.</param>
        /// <returns>An instance of <see cref="MyPlayer" />.</returns>
        public new static MyPlayer Find(int playerId)
        {
            //Find player in memory or initialize new player
            return Instances.Cast<MyPlayer>().FirstOrDefault(p => p.PlayerId == playerId) ?? new MyPlayer(playerId);
        }

        public override void OnConnected(PlayerEventArgs e)
        {
            //Test dialog
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
                    sendingDialog.Message = Color.Red + "INVALID PASSWORD!\n" + Color.White +
                                            "For this test, it will be 'mono'";
                    sendingDialog.Show(args.Player);
                }
            };
            dialog.Show(e.Player);

            //Test textdraw
            var td = new TextDraw(459.375000f, 78.166671f, "San Andreas", TextDrawFont.Diploma, Color.Red, 0.449999f,
                1.600000f, 6.250000f, 86.333374f, TextDrawAlignment.Left, 0, 1, Color.Black, true);
            td.Show(this);

            //Test playertextdraw
            var ptd = new PlayerTextDraw(this, 359.375000f, 78.166671f, "San Player", TextDrawFont.Diploma, Color.Red,
                0.449999f,
                1.600000f, 6.250000f, 86.333374f, TextDrawAlignment.Left, 0, 1, Color.Black, true);
            ptd.Show();

            base.OnConnected(e);
        }
    }
}