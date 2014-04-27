// SampSharp
// Copyright (C) 04 Tim Potze
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
            get { return Encoding.UTF8; }
        }

        /// <summary>
        ///     Writes a character to the log.
        /// </summary>
        /// <param name="value">The character to write. </param>
        public override void Write(char value)
        {
            Native.Print(value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        ///     Writes a string to the log.
        /// </summary>
        /// <param name="value">The string to write. </param>
        public override void Write(string value)
        {
            if (value != null)
                Native.Print(value);
        }

        /// <summary>
        ///     Writes a character followed by a line terminator to the log.
        /// </summary>
        /// <param name="value">The character to write to the log. </param>
        public override void WriteLine(char value)
        {
            Native.Print(value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        ///     Writes a string followed by a line terminator to the log.
        /// </summary>
        /// <param name="value">The string to write. If <paramref name="value" /> is null, only the line terminator is written. </param>
        public override void WriteLine(string value)
        {
            Native.Print(value ?? string.Empty);
        }
    }
}