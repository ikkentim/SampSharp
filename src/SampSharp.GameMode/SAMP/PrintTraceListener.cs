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

using System.Diagnostics;
using SampSharp.GameMode.Natives;

namespace SampSharp.GameMode.SAMP
{
    /// <summary>
    ///     TraceListener that writes to the console.
    /// </summary>
    public class PrintTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            Native.Print(message);
        }

        public override void WriteLine(string message)
        {
            Write(message.TrimEnd('\r', '\n'));
        }
    }
}