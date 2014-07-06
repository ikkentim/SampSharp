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
using SampSharp.GameMode;
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

            return true;
        }

        public override bool OnRconCommand(string command)
        {
            Console.WriteLine("[DEBUG-RCON] " + command);

            return base.OnRconCommand(command);
        }
    }
}