// SampSharp
// Copyright 2017 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using SampSharp.Core.CodePages;
using SampSharp.Core.Communication.Clients;
using SampSharp.Core.Logging;

namespace SampSharp.Core
{
    /// <summary>
    ///     Represents a configuration build for running a SampSharp game mode.
    /// </summary>
    public sealed class GameModeBuilder
    {
        private Func<ICommunicationClient, GameModeStartBehaviour, IGameModeProvider, IGameModeRunner> _builder;
        private ICommunicationClient _communicationClient;
        private GameModeExitBehaviour _exitBehaviour = GameModeExitBehaviour.ShutDown;
        private IGameModeProvider _gameModeProvider;
        private bool _redirectConsoleOutput;
        private GameModeStartBehaviour _startBehaviour = GameModeStartBehaviour.Gmx;
        private Encoding _encoding;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameModeBuilder" /> class.
        /// </summary>
        public GameModeBuilder()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                _communicationClient = new UnixDomainSocketCommunicationClient("/tmp/SampSharp");
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                _communicationClient = new NamedPipeClient("SampSharp");
        }

        /// <summary>
        ///     Use a named pipe with the specified <paramref name="pipeName" /> to communicate with the SampSharp server.
        /// </summary>
        /// <param name="pipeName">Name of the pipe.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UsePipe(string pipeName)
        {
            if (pipeName == null) throw new ArgumentNullException(nameof(pipeName));
            return UseCommunicationClient(new NamedPipeClient(pipeName));
        }

        /// <summary>
        ///     Use the specified <paramref name="encoding"/> when en/decoding text messages sent to/from the server.
        /// </summary>
        /// <param name="encoding">The encoding to use when en/decoding text messages send to/from the server.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UseEncoding(Encoding encoding)
        {
            _encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
            return this;
        }

        /// <summary>
        ///     Use the code page described by the file at the specified <paramref name="path"/> when en/decoding text messages sent to/from the server.
        /// </summary>
        /// <param name="path">The path to the code page file.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UseEncoding(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            return UseEncoding(CodePageEncoding.Load(path));
        }

        /// <summary>
        ///     Use the code page described by the specified <paramref name="stream"/> when en/decoding text messages sent to/from the server.
        /// </summary>
        /// <param name="stream">The stream containing the code page definition.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UseEncoding(Stream stream)
        {
            return UseEncoding(CodePageEncoding.Load(stream));
        }

        /// <summary>
        ///     Use an unix domain socket with a file at the specified <paramref name="path" /> to communicate with the SampSharp
        ///     server.
        /// </summary>
        /// <param name="path">The path to the domain socket file.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UseUnixDomainSocket(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            return UseCommunicationClient(new UnixDomainSocketCommunicationClient(path));
        }

        /// <summary>
        ///     Use a TCP client to communicate with the SampSharp server on localhost.
        /// </summary>
        /// <param name="port">The port on which to connect.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UseTcpClient(int port)
        {
            return UseCommunicationClient(new TcpCommunicationClient("127.0.0.1", port));
        }

        /// <summary>
        ///     Use a TCP client to communicate with the SampSharp server.
        /// </summary>
        /// <param name="host">The host to which to connect.</param>
        /// <param name="port">The port on which to connect.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UseTcpClient(string host, int port)
        {
            if (host == null) throw new ArgumentNullException(nameof(host));
            return UseCommunicationClient(new TcpCommunicationClient(host, port));
        }

        /// <summary>
        ///     Use the specified communication client to communicate with the SampSharp server.
        /// </summary>
        /// <param name="communicationClient">The communication client.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UseCommunicationClient(ICommunicationClient communicationClient)
        {
            _communicationClient = communicationClient ?? throw new ArgumentNullException(nameof(communicationClient));
            return this;
        }

        /// <summary>
        ///     Redirect the console output to the server.
        /// </summary>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder RedirectConsoleOutput()
        {
            _redirectConsoleOutput = true;
            return this;
        }

        /// <summary>
        ///     Use the specified game mode.
        /// </summary>
        /// <param name="gameMode">The game mode to use.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder Use(IGameModeProvider gameMode)
        {
            _gameModeProvider = gameMode ?? throw new ArgumentNullException(nameof(gameMode));
            return this;
        }

        /// <summary>
        ///     Use the gamemode of the specified <typeparamref name="TGameMode" /> type.
        /// </summary>
        /// <typeparam name="TGameMode">The type of the game mode to use.</typeparam>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder Use<TGameMode>() where TGameMode : IGameModeProvider
        {
            return Use(Activator.CreateInstance<TGameMode>());
        }

        /// <summary>
        ///     Uses the specified log level as the maximum level which is written to the log by SampSharp.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UseLogLevel(CoreLogLevel logLevel)
        {
            CoreLog.LogLevel = logLevel;
            return this;
        }

        /// <summary>
        ///     Uses the specified stream to log SampSharp log messages to.
        /// </summary>
        /// <param name="stream">The stream to log SampSharp log messages to.</param>
        /// <remarks>If a null value is specified as stream, no log messages will appear.</remarks>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UseLogStream(Stream stream)
        {
            CoreLog.Stream = stream;
            return this;
        }

        /// <summary>
        ///     Sets the behaviour used once a OnGameModeExit call has been received.
        /// </summary>
        /// <param name="exitBehaviour">The exit behaviour.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UseExitBehaviour(GameModeExitBehaviour exitBehaviour)
        {
            _exitBehaviour = exitBehaviour;
            return this;
        }


        /// <summary>
        ///     Use the specified start method when attaching to the server.
        /// </summary>
        /// <param name="startBehaviour">The start behaviour.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UseStartBehaviour(GameModeStartBehaviour startBehaviour)
        {
            _startBehaviour = startBehaviour;
            return this;
        }

        /// <summary>
        ///     Use a custom build procedure to load a custom game mode client.
        /// </summary>
        /// <param name="builder">The building function.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder BuildWith(Func<ICommunicationClient, GameModeStartBehaviour, IGameModeProvider, IGameModeRunner> builder)
        {
            _builder = builder;
            return this;
        }


        /// <summary>
        ///     Run the game mode using the build configuration stored in this instance.
        /// </summary>
        public void Run()
        {
            if (_communicationClient == null)
                throw new GameModeBuilderException("No communication client has been specified");
            if (_gameModeProvider == null)
                throw new GameModeBuilderException("No game mode provider has been specified");

            var redirect = _redirectConsoleOutput;

            var runner = Build();

            if (redirect)
                Console.SetOut(new ServerLogWriter(runner.Client));
            
            do
            {
                // If true is returned, the runner wants to shut down.
                if (runner.Run())
                {
                    break;
                }

            } while (_exitBehaviour == GameModeExitBehaviour.Restart);

            if (redirect)
                Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
        }

        private IGameModeRunner Build()
        {
            return _builder?.Invoke(_communicationClient, _startBehaviour, _gameModeProvider)
                   ?? new MultiProcessGameModeClient(_communicationClient, _startBehaviour, _gameModeProvider, _encoding);
        }
    }
}