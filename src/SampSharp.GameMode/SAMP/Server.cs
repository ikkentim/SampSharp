// SampSharp
// Copyright 2017 Tim Potze
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
using System.Diagnostics;
using System.Linq;
using SampSharp.GameMode.API;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP
{
    /// <summary>
    ///     Contains methods affecting the SA-MP server.
    /// </summary>
    public static partial class Server
    {
        private static ServerConfig _serverConfig;
        private static SVarCollection _varCollection;

        /// <summary>
        ///     Gets the maximum number of players that can join the server, as set by the server var 'maxplayers' in server.cfg.
        /// </summary>
        public static int MaxPlayers => ServerInternal.Instance.GetMaxPlayers();

        /// <summary>
        /// Gets the network stats as a string.
        /// </summary>
        public static string NetworkStats
        {
            get
            {
                string result;
                ServerInternal.Instance.GetNetworkStats(out result, 500);
                return result;
            }
        }

        /// <summary>
        ///     Gets the server configuration.
        /// </summary>
        public static ServerConfig Config => _serverConfig ?? (_serverConfig = new ServerConfig());

        /// <summary>
        ///     Gets the server variables.
        /// </summary>
        public static SVarCollection Variables => _varCollection ?? (_varCollection = new SVarCollection());

        /// <summary>
        ///     Blocks an IP address from further communication with the server
        ///     for a set amount of time (with wildcards allowed).
        ///     Players trying to connect to the server with a blocked IP address
        ///     will receive the generic "You are banned from this server." message.
        ///     Players that are online on the specified IP before the block
        ///     will timeout after a few seconds and, upon reconnect,
        ///     will receive the same message.
        /// </summary>
        /// <param name="ip">
        ///     The IP to block.
        ///     <remarks>
        ///         Wildcards can be used with this function,
        ///         for example blocking the IP '6.9.*.*' will block all IPs where the first two octets are 6 and 9 respectively.
        ///         Any number can be in place of an asterisk.
        ///     </remarks>
        /// </param>
        /// <param name="time">The time that the connection will be blocked for. 0 can be used for an indefinite block.</param>
        public static void BlockIPAddress(string ip, TimeSpan time)
        {
            ServerInternal.Instance.BlockIpAddress(ip, (int) time.TotalMilliseconds);
        }

        /// <summary>
        ///     Unblock an IP address that was previously blocked using <see cref="BlockIPAddress" />.
        /// </summary>
        /// <param name="ip">The IP address to unblock</param>
        public static void UnBlockIPAddress(string ip)
        {
            ServerInternal.Instance.UnBlockIpAddress(ip);
        }

        /// <summary>
        ///     Retrieve a console variable.
        /// </summary>
        /// <typeparam name="T">
        ///     The type to which the variable should be cast. Supported types: <see cref="int" />,
        ///     <see cref="bool" />, <see cref="string" />.
        /// </typeparam>
        /// <param name="varName">The server variable to read.</param>
        /// <returns>The value of the server variable.</returns>
        /// <exception cref="NotSupportedException"><typeparamref name="T" /> is not supported by SA-MP.</exception>
        public static T GetConsoleVariable<T>(string varName)
        {
            if (typeof (T) == typeof (string))
            {
                ServerInternal.Instance.GetConsoleVarAsString(varName, out var value, 64);
                return (T) Convert.ChangeType(value, typeof(T));
            }

            if (typeof (T) == typeof (bool))
            {
                return (T) Convert.ChangeType(ServerInternal.Instance.GetConsoleVarAsBool(varName), typeof(T));
            }

            if (typeof (T) == typeof (int))
            {
                return (T) Convert.ChangeType(ServerInternal.Instance.GetConsoleVarAsInt(varName), typeof(T));
            }

            throw new NotSupportedException("Type " + typeof (T) + " is not supported by SA:MP");
        }

        /// <summary>
        ///     Returns the uptime of the actual server in milliseconds.
        /// </summary>
        /// <returns>Uptime of the SA:MP server(NOT the physical box).</returns>
        public static int GetTickCount()
        {
            return ServerInternal.Instance.GetTickCount();
        }
        
        /// <summary>
        ///     Prints the specified message to the console.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Print(string message)
        {
            if(BaseMode.Instance == null || BaseMode.Instance.Client == null)
                throw new Exception("The game mode has not yet been initialized.");
            
            BaseMode.Instance.Client.Print(message);
        }

        /// <summary>
        ///     Sends an RCON command.
        /// </summary>
        /// <param name="command">The RCON command to be executed.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static void SendRconCommand(string command)
        {
            ServerInternal.Instance.SendRconCommand(command);
        }

        /// <summary>
        ///     Connect an NPC to the server.
        /// </summary>
        /// <param name="name">The name the NPC should connect as. Must follow the same rules as normal player names.</param>
        /// <param name="script">The NPC script name that is located in the npcmodes folder (without the .amx extension).</param>
        /// <returns>
        ///     An instance of <see cref="BasePlayer" /> based on the first available player slot. If no slots are available,
        ///     null.
        /// </returns>
        public static BasePlayer ConnectNPC(string name, string script)
        {
            var id = -1;
            var max = MaxPlayers;

            for (var i = 0; i < max; i++)
                if (!ServerInternal.Instance.IsPlayerConnected(i))
                    id = i;

            if (id == -1)
                return null;

            ServerInternal.Instance.ConnectNPC(name, script);
            return BasePlayer.FindOrCreate(id);
        }

        /// <summary>
        ///     Set the world weather for all players.
        /// </summary>
        /// <param name="weatherid">The weather to set.</param>
        public static void SetWeather(int weatherid)
        {
            ServerInternal.Instance.SetWeather(weatherid);
        }

        /// <summary>
        ///     Sets the world time to a specific hour.
        /// </summary>
        /// <param name="hour">Which time to set.</param>
        public static void SetWorldTime(int hour)
        {
            ServerInternal.Instance.SetWorldTime(hour);
        }
    }
}