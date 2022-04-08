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
using System.Text;
using SampSharp.Core.CodePages;
using SampSharp.Core.Logging;

namespace SampSharp.Core
{
    /// <summary>
    ///     Represents a configuration build for running a SampSharp game mode.
    /// </summary>
    public sealed class GameModeBuilder
    {
        private IGameModeProvider _gameModeProvider;
        private bool _redirectConsoleOutput;
        private Encoding _encoding;
        private TextWriter _logWriter;
        private bool _logWriterSet;
        
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

        /// <summary>
        /// Uses the encoding code page.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <returns></returns>
        public GameModeBuilder UseEncodingCodePage(string pageName)
        {
            if (pageName == null) throw new ArgumentNullException(nameof(pageName));

            var type = typeof(CodePageEncoding);

            var name = $"{type.Namespace}.data.{pageName.ToLowerInvariant()}.dat";

            using var stream = type.Assembly.GetManifestResourceStream(name);

            if (stream == null)
            {
                throw new GameModeBuilderException($"Code page with name '{pageName}' is not available.");
            }
            var encoding = CodePageEncoding.Deserialize(stream);
            return UseEncoding(encoding);
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
        ///     Run the game mode using the build configuration stored in this instance.
        /// </summary>
        public void Run()
        {
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
            {
                logWriter = redirectWriter != null
                    ? (TextWriter) redirectWriter
                    : new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
            }

            CoreLog.TextWriter = logWriter;

            // Run game mode runner
            runner.Run();
        }
        
        private IGameModeRunner Build()
        { 
            return new HostedGameModeClient(_gameModeProvider, _encoding);
        }
    }
}