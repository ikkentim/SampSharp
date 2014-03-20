using System.Globalization;
using System.IO;
using System.Text;

namespace GameMode
{
    /// <summary>
    /// A TextWriter that writes all input to the serverlog.
    /// </summary>
    public class LogWriter : TextWriter
    {
        /// <summary>
        /// Writes a character to the log.
        /// </summary>
        /// <param name="value">The character to write. </param>
        public override void Write(char value)
        {
            Native.Print(value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Writes a string to the log.
        /// </summary>
        /// <param name="value">The string to write. </param>
        public override void Write(string value)
        {
            if(value != null)
                Native.Print(value);
        }
        /// <summary>
        /// Writes a character followed by a line terminator to the log.
        /// </summary>
        /// <param name="value">The character to write to the log. </param>
        public override void WriteLine(char value)
        {
            Native.Print(value.ToString(CultureInfo.InvariantCulture));
        }
        /// <summary>
        /// Writes a string followed by a line terminator to the log.
        /// </summary>
        /// <param name="value">The string to write. If <paramref name="value"/> is null, only the line terminator is written. </param>
        public override void WriteLine(string value)
        {
            Native.Print(value ?? string.Empty);
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}
