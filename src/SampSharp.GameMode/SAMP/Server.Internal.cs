using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode.Natives;

namespace SampSharp.GameMode.SAMP
{
    public static partial class Server
    {
        private static class Internal
        {
            public delegate bool IsPlayerConnectedImpl(int playerid);

            public delegate int GetMaxPlayersImpl();

            public delegate bool BlockIpAddressImpl(string ipAddress, int timems);

            public delegate bool UnBlockIpAddressImpl(string ipAddress);

            public delegate bool GetServerVarAsBoolImpl(string varname);

            public delegate int GetServerVarAsIntImpl(string varname);

            public delegate bool GetServerVarAsStringImpl(string varname, out string value, int size);

            public delegate bool ConnectNPCImpl(string name, string script);

            public delegate int GetTickCountImpl();

            public delegate bool SendRconCommandImpl(string command);

            public delegate bool SetWorldTimeImpl(int hour);

            public delegate bool SetWeatherImpl(int weatherid);

            public delegate bool GetNetworkStatsImpl(out string retstr, int size);

            [Native("BlockIpAddress")]
            public static readonly BlockIpAddressImpl BlockIpAddress = null;
            [Native("UnBlockIpAddress")]
            public static readonly UnBlockIpAddressImpl UnBlockIpAddress = null;
            [Native("IsPlayerConnected")]
            public static readonly IsPlayerConnectedImpl IsPlayerConnected = null;

            [Native("GetMaxPlayers")]
            public static readonly GetMaxPlayersImpl GetMaxPlayers = null;

            [Native("GetServerVarAsString")]
            public static readonly GetServerVarAsStringImpl GetServerVarAsString =
                null;

            [Native("GetServerVarAsInt")]
            public static readonly GetServerVarAsIntImpl GetServerVarAsInt = null;

            [Native("GetServerVarAsBool")]
            public static readonly GetServerVarAsBoolImpl GetServerVarAsBool = null;

            [Native("GetTickCount")]
            public static readonly GetTickCountImpl NativeGetTickCount = null;

            [Native("ConnectNPC")]
            public static readonly ConnectNPCImpl NativeConnectNPC = null;

            [Native("SendRconCommand")]
            public static readonly SendRconCommandImpl NativeSendRconCommand = null;

            [Native("SetWorldTime")]
            public static readonly SetWorldTimeImpl NativeSetWorldTime = null;

            [Native("SetWeather")]
            public static readonly SetWeatherImpl NativeSetWeather = null;

            [Native("GetNetworkStats")]
            public static readonly GetNetworkStatsImpl GetNetworkStats = null;
        }

    }
}
