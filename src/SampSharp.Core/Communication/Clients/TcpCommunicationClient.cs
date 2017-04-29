using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SampSharp.Core.Communication.Clients
{
    /// <summary>
    ///     Represents a TCP communictaion client.
    /// </summary>
    public class TcpCommunicationClient : StreamCommunicationClient
    {
        private readonly string _host;
        private readonly int _port;
        private TcpClient _tcp;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TcpCommunicationClient" /> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the specified port is invalid.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="host" /> is null.</exception>
        public TcpCommunicationClient(string host, int port)
        {
            if (port < 1 || port > ushort.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(port));

            _host = host ?? throw new ArgumentNullException(nameof(host));
            _port = port;
        }

        #region Overrides of StreamCommunicationClient

        /// <summary>
        ///     Returns a newly created and connected stream for this client.
        /// </summary>
        /// <returns>A newly created and connected stream for this client.</returns>
        protected override async Task<Stream> CreateStream()
        {
            _tcp = new TcpClient();
            await _tcp.ConnectAsync(_host, _port);
            return _tcp.GetStream();
        }
        
        /// <summary>
        ///     Disconnects this client from the server.
        /// </summary>
        public override void Disconnect()
        {
            base.Disconnect();
            _tcp?.Dispose();
            _tcp = null;
        }
        
        #endregion
    }
}