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

using System;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    public class NativesTest : ITest
    {
        public void Start(GameMode gameMode)
        {
            Console.WriteLine("Does 'DoesNotExist' exist? {0}", Native.NativeExists("DoesNotExist"));
            Console.WriteLine("Does 'MapAndreas_GetAddress' exist? {0}", Native.NativeExists("MapAndreas_GetAddress"));
            Console.WriteLine("Does 'MapAndreas_Init' exist? {0}", Native.NativeExists("MapAndreas_Init"));
            Console.WriteLine("Does 'Streamer_GetTickRate' exist? {0}", Native.NativeExists("Streamer_GetTickRate"));
            Console.WriteLine("Does 'GetNetworkStats' exist? {0}", Native.NativeExists("GetNetworkStats"));

//            Console.WriteLine("CALLING GetNetworkStats");
//            string str = "";
//            Native.CallNative("GetNetworkStats", __arglist(ref str, 512));
//            Console.WriteLine(str);

//            Console.WriteLine("CALLING SetWeather");
//            Native.CallNative("SetWeather", __arglist(10));

//            Console.WriteLine("CALLING AllowAdminTeleport");
//            Native.CallNative("AllowAdminTeleport", __arglist(true));


//            Console.WriteLine("CALLING SetGameModeText");
//            Native.CallNative("SetGameModeText", __arglist("Blablab"));

//            Console.WriteLine("CALLING SetGravity");
//            Native.CallNative("SetGravity", __arglist(0.008f));

//            Console.WriteLine("CALLING CreateVehicle");
//            int vid = Native.CallNative("CreateVehicle", __arglist(400, 50.50f, 60.60f, 70.70f, 0.0f, -1, -1, -1));
//            float xx, yy, zz;
//            Native.GetVehiclePos(vid, out xx, out yy, out zz);
//            Console.WriteLine("pos: {0}", new Vector3(xx, yy, zz));
//            Native.DestroyVehicle(vid);

//            Console.WriteLine("CALLING GetVehicleModelInfo");
//            float x = 1;
//            float y = 2;
//            float z = 3;
//            Native.CallNative("GetVehicleModelInfo",
//                __arglist(400, (int) VehicleModelInfoType.Size, ref x, ref y, ref z));
//            Console.WriteLine("{0} {1} {2}", x, y, z);

//            Console.WriteLine("CALLING GetWeaponName");
//            string weapon;
//            Native.CallNative("GetWeaponName", __arglist((int) Weapon.MP5, out weapon, 32));
//            Console.WriteLine("Weapon: {0}", weapon);
        }
    }
}