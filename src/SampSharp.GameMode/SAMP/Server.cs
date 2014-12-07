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
using System.Diagnostics;
using System.Linq;
using SampSharp.GameMode.Natives;

namespace SampSharp.GameMode.SAMP
{
    /// <summary>
    ///     Contains methods affecting the SA-MP server.
    /// </summary>
    public static class Server
    {
        /// <summary>
        ///     Blocks an IP address from further communication with the server
        ///     for a set amount of time (with wildcards allowed).
        ///     Players trying to connect to the server with a blocked IP address
        ///     will receive the generic "You are banned from this server." message.
        ///     Players that are online on the specified IP before the block
        ///     will timeout after a few seconds and, upon reconnect,
        ///     will receive the same message.
        /// </summary>
        /// <param name="ip">
        ///     The IP to block.
        ///     <remarks>
        ///         Wildcards can be used with this function,
        ///         for example blocking the IP '6.9.*.*' will block all IPs where the first two octets are 6 and 9 respectively.
        ///         Any number can be in place of an asterisk.
        ///     </remarks>
        /// </param>
        /// <param name="time">The time that the connection will be blocked for. 0 can be used for an indefinite block.</param>
        public static void BlockIPAddress(string ip, TimeSpan time)
        {
            Native.BlockIpAddress(ip, (int) time.TotalMilliseconds);
        }

        /// <summary>
        ///     Unblock an IP address that was previously blocked using <see cref="BlockIPAddress" />.
        /// </summary>
        /// <param name="ip">The IP address to unblock</param>
        public static void UnBlockIPAddress(string ip)
        {
            Native.UnBlockIpAddress(ip);
        }

        /// <summary>
        ///     Retrieve a server variable.
        /// </summary>
        /// <typeparam name="T">
        ///     The type to which the variable should be cast. Supported types: <see cref="int" />,
        ///     <see cref="bool" />, <see cref="string" />.
        /// </typeparam>
        /// <param name="varName">The server variable to read.</param>
        /// <returns>The value of the server variable.</returns>
        /// <exception cref="NotSupportedException"><typeparamref name="T" /> is not supported by SA-MP.</exception>
        public static T Get<T>(string varName)
        {
            if (typeof (T) == typeof (string))
            {
                return (T) Convert.ChangeType(Native.GetServerVarAsString(varName), TypeCode.String);
            }

            if (typeof (T) == typeof (bool))
            {
                return (T) Convert.ChangeType(Native.GetServerVarAsBool(varName), TypeCode.Boolean);
            }

            if (typeof (T) == typeof (int))
            {
                return (T) Convert.ChangeType(Native.GetServerVarAsInt(varName), TypeCode.Int32);
            }

            throw new NotSupportedException("Type " + typeof (T) + " is not supported by SA:MP");
        }

        /// <summary>
        ///     Returns the uptime of the actual server in milliseconds.
        /// </summary>
        /// <returns>Uptime of the SA:MP server(NOT the physical box).</returns>
        public static int GetTickCount()
        {
            return Native.GetTickCount();
        }

        /// <summary>
        ///     Sets the currently active codepage.
        /// </summary>
        /// <param name="codepage">Codepage to use.</param>
        public static void SetCodepage(int codepage)
        {
            Native.SetCodepage(codepage);
        }

        /// <summary>
        ///     Sends an RCON command.
        /// </summary>
        /// <param name="command">The RCON command to be executed.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static void SendRconCommand(string command)
        {
            Native.SendRconCommand(command);
        }

        /// <summary>
        ///     Toggle debug output in console.
        /// </summary>
        /// <param name="toggle">True to log debug output to console, False otherwise.</param>
        public static void ToggleDebugOutput(bool toggle)
        {
            var logger = Debug.Listeners.OfType<ConsoleTraceListener>().FirstOrDefault();

            if (toggle && logger == null)
                Debug.Listeners.Add(new ConsoleTraceListener());
                
            else if (!toggle && logger != null)
                Debug.Listeners.Remove(logger);
        }
    }
}