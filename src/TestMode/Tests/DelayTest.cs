// SampSharp
// Copyright (C) 2015 Tim Potze
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
using System.Threading;
using SampSharp.GameMode.SAMP;

namespace TestMode.Tests
{
    public class DelayTest : ITest
    {
        public void Start(GameMode gameMode)
        {
            Thread main = Thread.CurrentThread;

            Console.WriteLine("Starting delay on main thread");

            Delay.Run(1500,
                () => Console.WriteLine("Delay passed. Now on main thread? {0}", main == Thread.CurrentThread));
        }
    }
}