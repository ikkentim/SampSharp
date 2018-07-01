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
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
        private bool _hosted;
        private TextWriter _logWriter;
        private bool _logWriterSet;
        
        private const string DefaultUnixDomainSocketPath = "/tmp/SampSharp";
        private const string DefaultPipeName = "SampSharp";
        private const string DefaultTcpIp = "127.0.0.1";
        private const int DefaultTcpPort = 8888;


        /// <summary>
        ///     Initializes a new instance of the <see cref="GameModeBuilder"/> class.
        /// </summary>
        public GameModeBuilder()
        {
            ParseArguments();
        }

        #region Communication
        
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
            return UseCommunicationClient(new TcpCommunicationClient(DefaultTcpIp, port));
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

        #endregion

        #region Encoding

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

        #endregion
        
        #region Game Mode Provider

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

        #endregion
        
        /// <summary>
        ///     Redirect the console output to the server.
        /// </summary>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder RedirectConsoleOutput()
        {
            _redirectConsoleOutput = true;
            return this;
        }

        #region Logging

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
        ///     Uses the specified text writer to log SampSharp log messages to.
        /// </summary>
        /// <param name="textWriter">The text writer to log SampSharp log messages to.</param>
        /// <remarks>If a null value is specified as text writer, no log messages will appear.</remarks>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UseLogWriter(TextWriter textWriter)
        {
            _logWriter = textWriter;
            _logWriterSet = true;
            return this;
        }

        #endregion

        /// <summary>
        ///     Indicate the game mode will be hosted in the SA-MP server process.
        /// </summary>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UseHosted()
        {
            _hosted = true;
            return RedirectConsoleOutput();
        }

        /// <summary>
        ///     Sets the behaviour used once a OnGameModeExit call has been received.
        /// </summary>
        /// <param name="exitBehaviour">The exit behaviour.</param>
        /// <remarks>The exit behaviour is ignored when using a hosted game mode environment.</remarks>
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
            ApplyDefaults();

            if (!_hosted && _communicationClient == null)
                throw new GameModeBuilderException("No communication client has been specified");
            if (_gameModeProvider == null)
                throw new GameModeBuilderException("No game mode provider has been specified");

            var redirect = _redirectConsoleOutput;

            // Build the game mode runner
            var runner = Build();

            if (runner == null)
                return;

            // Redirect console output
            ServerLogWriter redirectWriter = null;
            
            if (redirect)
            {
                redirectWriter = new ServerLogWriter(runner.Client);
                Console.SetOut(redirectWriter);
            }

            // Set framework log writer
            var logWriter = _logWriter;

            if (!_logWriterSet)
                logWriter = redirectWriter != null
                    ? (TextWriter) redirectWriter
                    : new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };

            CoreLog.TextWriter = logWriter;

            // Run game mode runner
            if (_hosted)
            {
                // Ignore exit behaviours for hosted environments.
                runner.Run();
            }
            else
            {
                do
                {
                    // If true is returned, the runner wants to shut down.
                    if (runner.Run())
                        break;
                } while (_exitBehaviour == GameModeExitBehaviour.Restart);

                if (redirect)
                    Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            }
        }

        private void ParseArguments()
        {
            var args = Environment.GetCommandLineArgs();

            if (args == null || args.Length == 0)
            {
                return;
            }
            
            for (int i = 0; i < args.Length; i++)
            {
                string option = null;
                string value;
                if (args[i].Length < 2 || !args[i].StartsWith("-"))
                    continue;

                if (args[i].StartsWith("--"))
                {
                    option = args[i];
                    value = args.Length > i + 1
                        ? args[i + 1]
                        : null;
                }
                else
                {
                    option = args[i].Substring(0, 2);
                    value = args[i].Length > 2
                        ? args[i].Substring(2)
                        : args.Length > i + 1
                            ? args[i + 1]
                            : null;
                }

                switch (option)
                {
                    case "--hosted":
                    case "-h":
                        UseHosted();
                        break;
                    case "--redirect-console-output":
                    case "-r":
                        RedirectConsoleOutput();
                        break;
                    case "--pipe":
                    case "-p":
                        if (value != null && !value.StartsWith("-"))
                        {
                            UsePipe(value);
                            i++;
                        }
                        else
                        {
                            UsePipe(DefaultPipeName);
                        }
                        break;
                    case "--unix":
                    case "-u":
                        if (value != null && !value.StartsWith("-"))
                        {
                            UseUnixDomainSocket(value);
                            i++;
                        }
                        else
                        {
                            UseUnixDomainSocket(DefaultUnixDomainSocketPath);
                        }
                        break;
                    case "--tcp":
                        var ip = DefaultTcpIp;
                        var port = DefaultTcpPort;

                        if (value != null && !value.StartsWith("-"))
                        {
                            var colon = value.IndexOf(":", StringComparison.Ordinal);

                            if (colon < 0)
                            {
                                if (IPAddress.TryParse(value.Substring(0, colon), out var addr) && addr.AddressFamily == AddressFamily.InterNetwork)
                                    ip = value.Substring(0, colon);
                                int.TryParse(value.Substring(colon + 1), out port);
                            }
                            else
                            {
                                int.TryParse(value, out port);
                            }

                            i++;
                        }

                        UseTcpClient(ip, port);
                        break;
                    case "--log-level":
                    case "-l":
                        if (value == null)
                            break;

                        if (Enum.TryParse<CoreLogLevel>(value, out var level))
                            UseLogLevel(level);

                        i++;
                        break;
                    case "--start-behaviour":
                    case "-s":
                        if (value == null)
                            break;

                        if (Enum.TryParse<GameModeStartBehaviour>(value, out var startBehaviour))
                            UseStartBehaviour(startBehaviour);

                        i++;
                        break;
                    case "--exit-behaviour":
                    case "-e":
                        if (value == null)
                            break;

                        if (Enum.TryParse<GameModeExitBehaviour>(value, out var exitBehaviour))
                            UseExitBehaviour(exitBehaviour);

                        i++;
                        break;
                }
            }
        }

        private void ApplyDefaults()
        {
            if (_communicationClient == null && !_hosted)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    _communicationClient = new UnixDomainSocketCommunicationClient(DefaultUnixDomainSocketPath);
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    _communicationClient = new NamedPipeClient(DefaultPipeName);
            }
        }

        private IGameModeRunner Build()
        {
            if (_builder != null)
            {
                return _builder(_communicationClient, _startBehaviour, _gameModeProvider);
            }

            if (_hosted)
            {
                return new HostedGameModeClient(_startBehaviour, _gameModeProvider, _encoding);
            }
            else
            {
                return new MultiProcessGameModeClient(_communicationClient, _startBehaviour, _gameModeProvider, _encoding);
            }
        }
    }
}