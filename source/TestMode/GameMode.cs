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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.World;
using TestMode.Tests;

namespace TestMode
{
    public class GameMode : BaseMode
    {
 
        public override bool OnGameModeInit()
        {
            SetGameModeText("sa-mp# testmode");
            UsePlayerPedAnims();

            AddPlayerClass(65, new Vector(5), 0);

            List<ITest> tests = new List<ITest>
            {
                new CommandsTest(),
                new ASyncTest(),
                new RegionsTest(),
                new MenuTest(),
                new CheckpointTest(),
                new DisposureTest(),
                new DialogTest(),
                new CharsetTest(),
                new VehicleInfoTest(),
            };

            foreach (var test in tests)
                test.Start(this);

            
            /*Console.WriteLine("CALLING GetNetworkStats");
            string str = "";
            Native.CallNative("GetNetworkStats", __arglist(ref str, 400));
            Console.WriteLine(str);*/

            Console.WriteLine("CALLING SetGameModeText");
            Native.CallNative("SetGameModeText", __arglist("SetGameModeText"));

            Console.WriteLine("CALLING SetGravity");
            Native.CallNative("SetGravity", __arglist(0.42f));
            
            Console.WriteLine("CALLING GetVehicleModelInfo (Doesn't work yet)");
            float x = 1;
            float y = 2;
            float z = 3;
            Native.CallNative("GetVehicleModelInfo", __arglist(400, (int)VehicleModelInfoType.Size, ref x, ref y, ref z));
            Console.WriteLine("{0} {1} {2}", x, y, z);
            
            /*Console.WriteLine("CALLING GetWeaponName");
            string weapon = "empty";

            Native.CallNative("GetWeaponName", __arglist((int)Weapon.MP5, ref weapon, 32));
            Console.WriteLine("Weapon: {0}", weapon);*/
            return true;
        }

        public override bool OnRconCommand(string command)
        {
            Console.WriteLine("[DEBUG-RCON] " + command);

            return base.OnRconCommand(command);
        }
    }
}