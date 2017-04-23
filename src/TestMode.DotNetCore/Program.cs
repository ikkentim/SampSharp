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

using SampSharp.Core;
using SampSharp.Core.Logging;

namespace TestMode.DotNetCore
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new GameModeBuilder()
                .UseLogLevel(CoreLogLevel.Debug) // Minimum log level for SampSharp's internal log messages to appear. Default: CoreLogLevel.Info
                //.UseLogStream(new CustomLogStream()) // A way of redirecting SampSharp's internal log messages trough your own logging system
                // .RedirectConsoleOutput() // Redirects all console output to the server (better not do this, it's useless and only slows things down)
                //.UseExitBehaviour(GameModeExitBehaviour.Restart) // Not yet implemented
                .UseStartBehaviour(GameModeStartBehaviour.FakeGmx) // Option : None (OnInitialized will not be called), Gmx (OnInitialized is called as you expect, is a little slow because of GMX rcon command), FakeGmx (OnInitialized is called, but not during the servers' OnGameModeInit, so you you can't do actual server initialization stuff, like static vehicles, but is much faster than Gmx option )
                .Use<GameMode>() // Game mode to run
                //.Use(new GameMode()) // Different way of specifying game mode to run
                //.UsePipe("myPipeName") // Use a different named pipe name, must be the same as specified in the server' config (under `sampsharp_pipe` in server.cfg) Default: SampSharp
                .Run(); // Start
        }
    }
}