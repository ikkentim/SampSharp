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
using SampSharp.Core.Natives.NativeObjects;

namespace SampSharp.GameMode.SAMP
{
    public static partial class Server
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public class ServerInternal : NativeObjectSingleton<ServerInternal>
        {
            [NativeMethod]
            public virtual bool BlockIpAddress(string ipAddress, int timems)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool UnBlockIpAddress(string ipAddress)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsPlayerConnected(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetMaxPlayers()
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetTickCount()
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool ConnectNPC(string name, string script)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SendRconCommand(string command)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetWorldTime(int hour)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetWeather(int weatherid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetNetworkStats(out string retstr, int size)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetConsoleVarAsString(string varname, out string value, int size)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetConsoleVarAsInt(string varname)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetConsoleVarAsBool(string varname)
            {
                throw new NativeNotImplementedException();
            }
        }
#pragma warning restore CS1591
    }
}