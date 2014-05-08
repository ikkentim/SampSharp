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

namespace SampSharp.GameMode.Events
{
    /// <summary>
    /// Provides data for the <see cref="BaseMode.IncomingConnection" /> event.
    /// </summary>
    public class ConnectionEventArgs : GameModeEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ConnectionEventArgs class.
        /// </summary>
        /// <param name="playerid">Id of the player trying to connect.</param>
        /// <param name="ipAddress">Ip of the connection.</param>
        /// <param name="port">Port of the connection.</param>
        public ConnectionEventArgs(int playerid, string ipAddress, int port)
            : base(true)
        {
            PlayerId = playerid;
            IpAddress = ipAddress;
            Port = port;
        }

        // Not using Player here as we don't delete the object when
        // the player fails to connect to the server.

        /// <summary>
        /// Gets the id of the player trying to connect.
        /// </summary>
        public int PlayerId { get; private set; }

        /// <summary>
        /// Gets the ip of this connection.
        /// </summary>
        public string IpAddress { get; private set; }

        /// <summary>
        /// Gets the port of this connection.
        /// </summary>
        public int Port { get; private set; }
    }
}