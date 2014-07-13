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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Natives;

namespace TestMode.Tests
{
    public class NativesTest : ITest
    {
        public void Start(GameMode gameMode)
        {
            Console.WriteLine("CALLING GetNetworkStats");
            string str = "";
            Native.CallNative("GetNetworkStats", __arglist(ref str, 330));
            Console.WriteLine(str);

            Console.WriteLine("CALLING SetWeather");
            Native.CallNative("SetWeather", __arglist(20));

            Console.WriteLine("CALLING AllowAdminTeleport");
            Native.CallNative("AllowAdminTeleport", __arglist(true));


            Console.WriteLine("CALLING SetGameModeText");
            Native.CallNative("SetGameModeText", __arglist("Blablab"));

            Console.WriteLine("CALLING SetGravity");
            Native.CallNative("SetGravity", __arglist(0.42f));

            Console.WriteLine("CALLING CreateVehicle");
            int vid = Native.CallNative("CreateVehicle", __arglist(400, 50.50f, 60.60f, 70.70f, 0.0f, -1, -1, -1));
            Console.WriteLine("pos: {0}",
                Native.GetVehiclePos(vid));

            Console.WriteLine("CALLING GetVehicleModelInfo");
            float x = 1;
            float y = 2;
            float z = 3;
            Native.CallNative("GetVehicleModelInfo",
                __arglist(400, (int) VehicleModelInfoType.Size, ref x, ref y, ref z));
            Console.WriteLine("{0} {1} {2}", x, y, z);

            Console.WriteLine("CALLING GetWeaponName");
            string weapon;
            Native.CallNative("GetWeaponName", __arglist((int) Weapon.MP5, out weapon, 32));
            Console.WriteLine("Weapon: {0}", weapon);
        }
    }
}