using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SampSharp.Core
{
    /// <summary>
    ///     A TextWriter that writes all input to the server log.
    /// </summary>
    public class ServerLogWriter : TextWriter
    {
        private readonly IGameModeClient _gameModeClient;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ServerLogWriter"/> class.
        /// </summary>
        /// <param name="gameModeClient">The game mode client.</param>
        /// <exception cref="System.ArgumentNullException">gameModeClient</exception>
        public ServerLogWriter(IGameModeClient gameModeClient)
        {
            _gameModeClient = gameModeClient ?? throw new ArgumentNullException(nameof(gameModeClient));
        }

        /// <summary>
        ///     When overridden in a derived class, returns the character encoding in which the output is written.
        /// </summary>
        public override Encoding Encoding => Encoding.ASCII;

        #region WriteLine

        /// <summary>
        ///     Writes a character followed by a line terminator to the text string or stream.
        /// </summary>
        /// <param name="value">The character to write to the text stream.</param>
        public override void WriteLine(char value)
        {
            Write(value);
        }

        /// <summary>
        ///     Writes a string followed by a line terminator to the text string or stream.
        /// </summary>
        /// <param name="value">The string to write. If <paramref name="value" /> is null, only the line terminator is written.</param>
        public override void WriteLine(string value)
        {
            Write(value);
        }

        /// <summary>
        ///     Writes a line terminator to the text string or stream.
        /// </summary>
        public override void WriteLine()
        {
            Write(string.Empty);
        }

        /// <summary>
        ///     Writes a formatted string and a new line to the text string or stream, using the same semantics as the
        ///     <see cref="M:System.String.Format(System.String,System.Object)" /> method.
        /// </summary>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="arg0">The object to format and write.</param>
        public override void WriteLine(string format, object arg0)
        {
            Write(format, arg0);
        }

        /// <summary>
        ///     Writes a formatted string and a new line to the text string or stream, using the same semantics as the
        ///     <see cref="M:System.String.Format(System.String,System.Object,System.Object)" /> method.
        /// </summary>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="arg0">The first object to format and write.</param>
        /// <param name="arg1">The second object to format and write.</param>
        public override void WriteLine(string format, object arg0, object arg1)
        {
            Write(format, arg0, arg1);
        }

        /// <summary>
        ///     Writes out a formatted string and a new line, using the same semantics as
        ///     <see cref="M:System.String.Format(System.String,System.Object)" />.
        /// </summary>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="arg0">The first object to format and write.</param>
        /// <param name="arg1">The second object to format and write.</param>
        /// <param name="arg2">The third object to format and write.</param>
        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            Write(format, arg0, arg1, arg2);
        }

        /// <summary>
        ///     Writes out a formatted string and a new line, using the same semantics as
        ///     <see cref="M:System.String.Format(System.String,System.Object)" />.
        /// </summary>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="arg">An object array that contains zero or more objects to format and write.</param>
        public override void WriteLine(string format, params object[] arg)
        {
            Write(format, arg);
        }

        /// <summary>
        ///     Writes the text representation of a Boolean value followed by a line terminator to the text string or stream.
        /// </summary>
        /// <param name="value">The Boolean value to write.</param>
        public override void WriteLine(bool value)
        {
            Write(value);
        }

        /// <summary>
        ///     Writes an array of characters followed by a line terminator to the text string or stream.
        /// </summary>
        /// <param name="buffer">The character array from which data is read.</param>
        public override void WriteLine(char[] buffer)
        {
            Write(buffer);
        }

        /// <summary>
        ///     Writes a subarray of characters followed by a line terminator to the text string or stream.
        /// </summary>
        /// <param name="buffer">The character array from which data is read.</param>
        /// <param name="index">The character position in <paramref name="buffer" /> at which to start reading data.</param>
        /// <param name="count">The maximum number of characters to write.</param>
        public override void WriteLine(char[] buffer, int index, int count)
        {
            Write(buffer, index, count);
        }

        /// <summary>
        ///     Writes the text representation of a decimal value followed by a line terminator to the text string or stream.
        /// </summary>
        /// <param name="value">The decimal value to write.</param>
        public override void WriteLine(decimal value)
        {
            Write(value);
        }

        /// <summary>
        ///     Writes the text representation of a 8-byte floating-point value followed by a line terminator to the text string or
        ///     stream.
        /// </summary>
        /// <param name="value">The 8-byte floating-point value to write.</param>
        public override void WriteLine(double value)
        {
            Write(value);
        }


        /// <summary>
        ///     Writes the text representation of a 4-byte floating-point value followed by a line terminator to the text string or
        ///     stream.
        /// </summary>
        /// <param name="value">The 4-byte floating-point value to write.</param>
        public override void WriteLine(float value)
        {
            Write(value);
        }

        /// <summary>
        ///     Writes the text representation of a 4-byte signed integer followed by a line terminator to the text string or
        ///     stream.
        /// </summary>
        /// <param name="value">The 4-byte signed integer to write.</param>
        public override void WriteLine(int value)
        {
            Write(value);
        }

        /// <summary>
        ///     Writes the text representation of an 8-byte signed integer followed by a line terminator to the text string or
        ///     stream.
        /// </summary>
        /// <param name="value">The 8-byte signed integer to write.</param>
        public override void WriteLine(long value)
        {
            Write(value);
        }

        /// <summary>
        ///     Writes the text representation of an object by calling the ToString method on that object, followed by a line
        ///     terminator to the text string or stream.
        /// </summary>
        /// <param name="value">The object to write. If <paramref name="value" /> is null, only the line terminator is written.</param>
        public override void WriteLine(object value)
        {
            Write(value);
        }

        /// <summary>
        ///     Writes the text representation of a 4-byte unsigned integer followed by a line terminator to the text string or
        ///     stream.
        /// </summary>
        /// <param name="value">The 4-byte unsigned integer to write.</param>
        public override void WriteLine(uint value)
        {
            Write(value);
        }

        /// <summary>
        ///     Writes the text representation of an 8-byte unsigned integer followed by a line terminator to the text string or
        ///     stream.
        /// </summary>
        /// <param name="value">The 8-byte unsigned integer to write.</param>
        public override void WriteLine(ulong value)
        {
            Write(value);
        }

        #endregion

        #region Write

        /// <summary>
        ///     Writes a character to the text string or stream.
        /// </summary>
        /// <param name="value">The character to write to the text stream.</param>
        public override void Write(char value)
        {
            // TODO: Buffer until a line break is sent
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
                    Write(value.ToString());
                    break;
            }
        }

        /// <summary>
        ///     Writes a string to the text string or stream.
        /// </summary>
        /// <param name="value">The string to write.</param>
        public override void Write(string value)
        {
            foreach (var ln in value.Split('\n'))
            {
                var line = ln.Trim('\r');
                while (line.Length > 512)
                {
                    var block = line.Substring(0, 512);
                    line = line.Substring(512);
                    _gameModeClient.Print(block);
                }
                _gameModeClient.Print(line);
            }
        }

        /// <summary>
        ///     Writes the text representation of a Boolean value to the text string or stream.
        /// </summary>
        /// <param name="value">The Boolean value to write.</param>
        public override void Write(bool value)
        {
            Write(value.ToString());
        }

        /// <summary>
        ///     Writes a formatted string to the text string or stream, using the same semantics as the
        ///     <see cref="M:System.String.Format(System.String,System.Object)" /> method.
        /// </summary>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="arg0">The object to format and write.</param>
        public override void Write(string format, object arg0)
        {
            // ReSharper disable once RedundantStringFormatCall
            Write(string.Format(format, arg0));
        }

        /// <summary>
        ///     Writes a formatted string to the text string or stream, using the same semantics as the
        ///     <see cref="M:System.String.Format(System.String,System.Object,System.Object)" /> method.
        /// </summary>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="arg0">The first object to format and write.</param>
        /// <param name="arg1">The second object to format and write.</param>
        public override void Write(string format, object arg0, object arg1)
        {
            // ReSharper disable once RedundantStringFormatCall
            Write(string.Format(format, arg0, arg1));
        }

        /// <summary>
        ///     Writes a formatted string to the text string or stream, using the same semantics as the
        ///     <see cref="M:System.String.Format(System.String,System.Object,System.Object,System.Object)" /> method.
        /// </summary>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="arg0">The first object to format and write.</param>
        /// <param name="arg1">The second object to format and write.</param>
        /// <param name="arg2">The third object to format and write.</param>
        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            // ReSharper disable once RedundantStringFormatCall
            Write(string.Format(format, arg0, arg1, arg2));
        }

        /// <summary>
        ///     Writes a formatted string to the text string or stream, using the same semantics as the
        ///     <see cref="M:System.String.Format(System.String,System.Object[])" /> method.
        /// </summary>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="arg">An object array that contains zero or more objects to format and write.</param>
        public override void Write(string format, params object[] arg)
        {
            // ReSharper disable once RedundantStringFormatCall
            Write(string.Format(format, arg));
        }

        /// <summary>
        ///     Writes a character array to the text string or stream.
        /// </summary>
        /// <param name="buffer">The character array to write to the text stream.</param>
        public override void Write(char[] buffer)
        {
            Write(string.Join(string.Empty, buffer));
        }

        /// <summary>
        ///     Writes a subarray of characters to the text string or stream.
        /// </summary>
        /// <param name="buffer">The character array to write data from.</param>
        /// <param name="index">The character position in the buffer at which to start retrieving data.</param>
        /// <param name="count">The number of characters to write.</param>
        public override void Write(char[] buffer, int index, int count)
        {
            Write(string.Join(string.Empty, buffer).Substring(index, count));
        }

        /// <summary>
        ///     Writes the text representation of a decimal value to the text string or stream.
        /// </summary>
        /// <param name="value">The decimal value to write.</param>
        public override void Write(decimal value)
        {
            Write(value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        ///     Writes the text representation of an 8-byte floating-point value to the text string or stream.
        /// </summary>
        /// <param name="value">The 8-byte floating-point value to write.</param>
        public override void Write(double value)
        {
            Write(value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        ///     Writes the text representation of a 4-byte floating-point value to the text string or stream.
        /// </summary>
        /// <param name="value">The 4-byte floating-point value to write.</param>
        public override void Write(float value)
        {
            Write(value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        ///     Writes the text representation of a 4-byte signed integer to the text string or stream.
        /// </summary>
        /// <param name="value">The 4-byte signed integer to write.</param>
        public override void Write(int value)
        {
            Write(value.ToString());
        }

        /// <summary>
        ///     Writes the text representation of an 8-byte signed integer to the text string or stream.
        /// </summary>
        /// <param name="value">The 8-byte signed integer to write.</param>
        public override void Write(long value)
        {
            Write(value.ToString());
        }

        /// <summary>
        ///     Writes the text representation of an object to the text string or stream by calling the ToString method on that
        ///     object.
        /// </summary>
        /// <param name="value">The object to write.</param>
        public override void Write(object value)
        {
            Write(value?.ToString() ?? string.Empty);
        }

        /// <summary>
        ///     Writes the text representation of a 4-byte unsigned integer to the text string or stream.
        /// </summary>
        /// <param name="value">The 4-byte unsigned integer to write.</param>
        public override void Write(uint value)
        {
            Write(value.ToString());
        }

        /// <summary>
        ///     Writes the text representation of an 8-byte unsigned integer to the text string or stream.
        /// </summary>
        /// <param name="value">The 8-byte unsigned integer to write.</param>
        public override void Write(ulong value)
        {
            Write(value.ToString());
        }

        #endregion

        #region Async

        // TODO: Improve Async variants
        /// <summary>
        ///     Writes a character to the text string or stream asynchronously.
        /// </summary>
        /// <param name="value">The character to write to the text stream.</param>
        /// <returns>
        ///     A task that represents the asynchronous write operation.
        /// </returns>
        public override Task WriteAsync(char value)
        {
            return new Task(() => Write(value));
        }

        /// <summary>
        ///     Writes a subarray of characters to the text string or stream asynchronously.
        /// </summary>
        /// <param name="buffer">The character array to write data from.</param>
        /// <param name="index">The character position in the buffer at which to start retrieving data.</param>
        /// <param name="count">The number of characters to write.</param>
        /// <returns>
        ///     A task that represents the asynchronous write operation.
        /// </returns>
        public override Task WriteAsync(char[] buffer, int index, int count)
        {
            return new Task(() => Write(buffer, index, count));
        }

        /// <summary>
        ///     Writes a string to the text string or stream asynchronously.
        /// </summary>
        /// <param name="value">The string to write. If <paramref name="value" /> is null, nothing is written to the text stream.</param>
        /// <returns>
        ///     A task that represents the asynchronous write operation.
        /// </returns>
        public override Task WriteAsync(string value)
        {
            return new Task(() => Write(value));
        }

        /// <summary>
        ///     Writes a line terminator asynchronously to the text string or stream.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous write operation.
        /// </returns>
        public override Task WriteLineAsync()
        {
            return new Task(WriteLine);
        }

        /// <summary>
        ///     Writes a character followed by a line terminator asynchronously to the text string or stream.
        /// </summary>
        /// <param name="value">The character to write to the text stream.</param>
        /// <returns>
        ///     A task that represents the asynchronous write operation.
        /// </returns>
        public override Task WriteLineAsync(char value)
        {
            return new Task(() => Write(value));
        }

        /// <summary>
        ///     Writes a subarray of characters followed by a line terminator asynchronously to the text string or stream.
        /// </summary>
        /// <param name="buffer">The character array to write data from.</param>
        /// <param name="index">The character position in the buffer at which to start retrieving data.</param>
        /// <param name="count">The number of characters to write.</param>
        /// <returns>
        ///     A task that represents the asynchronous write operation.
        /// </returns>
        public override Task WriteLineAsync(char[] buffer, int index, int count)
        {
            return new Task(() => WriteLine(buffer, index, count));
        }

        /// <summary>
        ///     Writes a string followed by a line terminator asynchronously to the text string or stream.
        /// </summary>
        /// <param name="value">The string to write. If the value is null, only a line terminator is written.</param>
        /// <returns>
        ///     A task that represents the asynchronous write operation.
        /// </returns>
        public override Task WriteLineAsync(string value)
        {
            return new Task(() => WriteLine(value));
        }

        #endregion
    }
}