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

using RiverShell.World;
using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Natives;

namespace RiverShell.Controllers
{
    public class ResupplyController : IController, IEventListener
    {
        public void RegisterEvents(BaseMode gameMode)
        {
            gameMode.PlayerUpdate += (sender, args) =>
            {
                var player = args.Player as Player;
                if (player.IsInRangeOfPoint(2.5f, GameMode.BlueTeam.ResupplyPosition))
                {
                    Resupply(player);
                }
            };
        }

        private void Resupply(Player player)
        {
            //Check if we haven't resupplied recently
            if (player.LastResupplyTime != 0 &&
                (Native.GetTickCount() - player.LastResupplyTime) <= Config.ResupplyTime*1000) return;

            //Resupply
            player.LastResupplyTime = Native.GetTickCount();

            player.ResetWeapons();
            player.GiveWeapon(Weapon.M4, 100);
            player.GiveWeapon(Weapon.MP5, 200);
            player.GiveWeapon(Weapon.Sniper, 10);

            player.Health = 100.0f;
            player.Armour = 100.0f;

            player.GameText("Resupplied", 2000, 5);
            player.PlaySound(1150);
        }
    }
}