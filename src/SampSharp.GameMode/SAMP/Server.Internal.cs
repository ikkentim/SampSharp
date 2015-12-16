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
using SampSharp.GameMode.API;

namespace SampSharp.GameMode.SAMP
{
    public static partial class Server
    {
        private static class Internal
        {
            public delegate bool BlockIpAddressImpl(string ipAddress, int timems);

            public delegate bool ConnectNPCImpl(string name, string script);

            public delegate int GetMaxPlayersImpl();

            public delegate bool GetNetworkStatsImpl(out string retstr, int size);

            public delegate bool GetServerVarAsBoolImpl(string varname);

            public delegate int GetServerVarAsIntImpl(string varname);

            public delegate bool GetServerVarAsStringImpl(string varname, out string value, int size); 

            public delegate int GetTickCountImpl();

            public delegate bool IsPlayerConnectedImpl(int playerid);

            public delegate bool SendRconCommandImpl(string command);

            public delegate bool SetWeatherImpl(int weatherid);

            public delegate bool SetWorldTimeImpl(int hour);

            public delegate bool UnBlockIpAddressImpl(string ipAddress);

            [Native("BlockIpAddress")] public static readonly BlockIpAddressImpl BlockIpAddress = null;
            [Native("UnBlockIpAddress")] public static readonly UnBlockIpAddressImpl UnBlockIpAddress = null;
            [Native("IsPlayerConnected")] public static readonly IsPlayerConnectedImpl IsPlayerConnected = null;

            [Native("GetMaxPlayers")] public static readonly GetMaxPlayersImpl GetMaxPlayers = null;

            [Native("GetServerVarAsString")] public static readonly GetServerVarAsStringImpl GetServerVarAsString =
                null;

            [Native("GetServerVarAsInt")] public static readonly GetServerVarAsIntImpl GetServerVarAsInt = null;

            [Native("GetServerVarAsBool")] public static readonly GetServerVarAsBoolImpl GetServerVarAsBool = null;

            [Native("GetTickCount")] public static readonly GetTickCountImpl NativeGetTickCount = null;

            [Native("ConnectNPC")] public static readonly ConnectNPCImpl NativeConnectNPC = null;

            [Native("SendRconCommand")] public static readonly SendRconCommandImpl NativeSendRconCommand = null;

            [Native("SetWorldTime")] public static readonly SetWorldTimeImpl NativeSetWorldTime = null;

            [Native("SetWeather")] public static readonly SetWeatherImpl NativeSetWeather = null;

            [Native("GetNetworkStats")] public static readonly GetNetworkStatsImpl GetNetworkStats = null;
        }
    }
}