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
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    public class DisposureTest : ITest
    {
        public void Start(GameMode gameMode)
        {
            var playercount = Player.All.Count;
            var success = true;

            Player player = Player.Create(499);

            if (Player.All.Count - 1 != playercount)
            {
                Console.WriteLine("DisposureTest: Adding didn't add player to pool.");
                success = false;
            }
            player.Dispose();

            if (Player.All.Count != playercount)
            {
                Console.WriteLine("DisposureTest: Disposing didn't remove player from pool.");
                success = false;
            }
            try
            {
                player.SetChatBubble("Test!", Color.Yellow, 100, 10);

                Console.WriteLine("DisposureTest: Passed SetChatBubble.");
                success = false;
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine("DisposureTest: Exception thrown.");
            }

            Console.WriteLine("DisposureTest successful: {0}", success);
        }
    }
}