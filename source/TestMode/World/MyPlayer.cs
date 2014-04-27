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

using System;
using System.Linq;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;

namespace TestMode.World
{
    public class MyPlayer : Player
    {
        public MyPlayer(int id) : base(id)
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

        public override void OnConnected(PlayerEventArgs e)
        {
            Color = new uint[]
            {
                0xFF8C13FF, 0xC715FFFF, 0x20B2AAFF, 0xDC143CFF, 0x6495EDFF,
                0xF0E68CFF, 0x778899FF, 0xFF1493FF, 0xF4A460FF, 0xEE82EEFF,
                0xFFD720FF, 0x8B4513FF, 0x4949A0FF, 0x148B8BFF, 0x14FF7FFF,
                0x556B2FFF, 0x0FD9FAFF, 0x10DC29FF, 0x534081FF, 0x0495CDFF,
                0xEF6CE8FF, 0xBD34DAFF, 0x247C1BFF, 0x0C8E5DFF, 0x635B03FF,
                0xCB7ED3FF, 0x65ADEBFF, 0x5C1ACCFF, 0xF2F853FF, 0x11F891FF,
                0x7B39AAFF, 0x53EB10FF, 0x54137DFF, 0x275222FF, 0xF09F5BFF,
                0x3D0A4FFF, 0x22F767FF, 0xD63034FF, 0x9A6980FF, 0xDFB935FF,
                0x3793FAFF, 0x90239DFF, 0xE9AB2FFF, 0xAF2FF3FF, 0x057F94FF,
                0xB98519FF, 0x388EEAFF, 0x028151FF, 0xA55043FF, 0x0DE018FF,
                0x93AB1CFF, 0x95BAF0FF, 0x369976FF, 0x18F71FFF, 0x4B8987FF,
                0x491B9EFF, 0x829DC7FF, 0xBCE635FF, 0xCEA6DFFF, 0x20D4ADFF,
                0x2D74FDFF, 0x3C1C0DFF, 0x12D6D4FF, 0x48C000FF, 0x2A51E2FF,
                0xE3AC12FF, 0xFC42A8FF, 0x2FC827FF, 0x1A30BFFF, 0xB740C2FF,
                0x42ACF5FF, 0x2FD9DEFF, 0xFAFB71FF, 0x05D1CDFF, 0xC471BDFF,
                0x94436EFF, 0xC1F7ECFF, 0xCE79EEFF, 0xBD1EF2FF, 0x93B7E4FF,
                0x3214AAFF, 0x184D3BFF, 0xAE4B99FF, 0x7E49D7FF, 0x4C436EFF,
                0xFA24CCFF, 0xCE76BEFF, 0xA04E0AFF, 0x9F945CFF, 0xDCDE3DFF,
                0x10C9C5FF, 0x70524DFF, 0x0BE472FF, 0x8A2CD7FF, 0x6152C2FF,
                0xCF72A9FF, 0xE59338FF, 0xEEDC2DFF, 0xD8C762FF, 0xD8C762FF
            }.Select(u => unchecked((int) u)).ElementAt(Id%100);


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
                    Native.SendClientMessage(sendingPlayer.Id, Color.GreenYellow, "You logged in!");
                    Console.WriteLine(CameraPosition.ToString());
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
            /*
            var td = new TextDraw(459.375000f, 78.166671f, "San Andreas", TextDrawFont.Diploma, Color.Red)
            {
                LetterWidth = 0.449999f,
                LetterHeight = 1.600000f,
                Width = 6.250000f,
                Height = 86.333374f,
                Shadow = 1,
                BackColor = Color.Black

            };
            td.Show(this);

            //Test playertextdraw
            var ptd = new PlayerTextDraw(this, 359.375000f, 78.166671f, "San Player", TextDrawFont.Diploma, Color.Red)
            {
                LetterWidth = 0.449999f,
                LetterHeight = 1.600000f,
                Width = 6.250000f,
                Height = 86.333374f,
                Shadow = 1,
                BackColor = Color.Black

            };

            ptd.Show();*/

            base.OnConnected(e);
        }

        public override void OnText(PlayerTextEventArgs e)
        {
            SendClientMessageToAll(Color, string.Format("{0}{1}: {2}", this, Color.White, e.Text));
            e.Success = false;
            base.OnText(e);
        }
    }
}