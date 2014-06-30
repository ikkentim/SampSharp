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

using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode.Natives;

namespace SampSharp.GameMode
{
    /// <summary>
    ///     A TextWriter that writes all input to the serverlog.
    /// </summary>
    public class LogWriter : TextWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.Default; }
        }

        #region WriteLine

        public override void WriteLine(char value)
        {
            Write(value);
        }

        public override void WriteLine(string value)
        {
            Write(value);
        }

        public override void WriteLine()
        {
            Write(string.Empty);
        }

        public override void WriteLine(string format, object arg0)
        {
            Write(format, arg0);
        }

        public override void WriteLine(string format, object arg0, object arg1)
        {
            Write(format, arg0, arg1);
        }

        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            Write(format, arg0, arg1, arg2);
        }

        public override void WriteLine(string format, params object[] arg)
        {
            Write(format, arg);
        }

        public override void WriteLine(bool value)
        {
            Write(value);
        }

        public override void WriteLine(char[] buffer)
        {
            Write(buffer);
        }

        public override void WriteLine(char[] buffer, int index, int count)
        {
            Write(buffer, index, count);
        }

        public override void WriteLine(decimal value)
        {
            Write(value);
        }

        public override void WriteLine(double value)
        {
            Write(value);
        }


        public override void WriteLine(float value)
        {
            Write(value);
        }

        public override void WriteLine(int value)
        {
            Write(value);
        }

        public override void WriteLine(long value)
        {
            Write(value);
        }

        public override void WriteLine(object value)
        {
            Write(value);
        }

        public override void WriteLine(uint value)
        {
            Write(value);
        }

        public override void WriteLine(ulong value)
        {
            Write(value);
        }

        #endregion

        #region Write

        public override void Write(char value)
        {
            switch (value)
            {
                case '\r':
                case '\n':
                    /*
                     * Do not print \r \n characters.
                     * There are emitted by Console.WriteLine, but Native.Print breaks for itself.
                     */
                    break;
                default:
                    Write(value.ToString(CultureInfo.InvariantCulture));
                    break;
            }
        }

        public override void Write(string value)
        {
            if (value != null)
                Native.Print(value);
        }

        public override void Write(bool value)
        {
            Write(value.ToString());
        }

        public override void Write(string format, object arg0)
        {
            // ReSharper disable once RedundantStringFormatCall
            Write(string.Format(format, arg0));
        }

        public override void Write(string format, object arg0, object arg1)
        {
            // ReSharper disable once RedundantStringFormatCall
            Write(string.Format(format, arg0, arg1));
        }

        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            // ReSharper disable once RedundantStringFormatCall
            Write(string.Format(format, arg0, arg1, arg2));
        }

        public override void Write(string format, params object[] arg)
        {
            // ReSharper disable once RedundantStringFormatCall
            Write(string.Format(format, arg));
        }

        public override void Write(char[] buffer)
        {
            Write(string.Join(string.Empty, buffer));
        }

        public override void Write(char[] buffer, int index, int count)
        {
            Write(string.Join(string.Empty, buffer).Substring(index, count));
        }

        public override void Write(decimal value)
        {
            Write(value.ToString());
        }

        public override void Write(double value)
        {
            Write(value.ToString());
        }

        public override void Write(float value)
        {
            Write(value.ToString());
        }

        public override void Write(int value)
        {
            Write(value.ToString());
        }

        public override void Write(long value)
        {
            Write(value.ToString());
        }

        public override void Write(object value)
        {
            Write(value.ToString());
        }

        public override void Write(uint value)
        {
            Write(value.ToString());
        }

        public override void Write(ulong value)
        {
            Write(value.ToString());
        }

        #endregion

        #region Async

        public override Task WriteAsync(char value)
        {
            return new Task(() => Write(value));
        }

        public override Task WriteAsync(char[] buffer, int index, int count)
        {
            return new Task(() => Write(buffer, index, count));
        }

        public override Task WriteAsync(string value)
        {
            return new Task(() => Write(value));
        }

        public override Task WriteLineAsync()
        {
            return new Task(WriteLine);
        }

        public override Task WriteLineAsync(char value)
        {
            return new Task(() => Write(value));
        }

        public override Task WriteLineAsync(char[] buffer, int index, int count)
        {
            return new Task(() => WriteLine(buffer, index, count));
        }

        public override Task WriteLineAsync(string value)
        {
            return new Task(() => WriteLine(value));
        }

        #endregion
    }
}