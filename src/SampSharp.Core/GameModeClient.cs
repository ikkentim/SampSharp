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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SampSharp.Core
{
    internal class GameModeClient : IGameModeClient, ICommandReceiveScope
    {
        private readonly Dictionary<string, Callback> _callbacks = new Dictionary<string, Callback>();
        private readonly IGameModeProvider _gameModeProvider;
        private readonly string _pipeName;
        private readonly Queue<PongReceiver> _pongs = new Queue<PongReceiver>();
        private IPipeClient _client;
        private int _mainThread;

        // TODO: threading: 
        // https://www.codeproject.com/Articles/31971/Understanding-SynchronizationContext-Part-I

        /// <summary>
        /// Initializes a new instance of the <see cref="GameModeClient" /> class.
        /// </summary>
        /// <param name="pipeName">Name of the pipe.</param>
        /// <param name="gameModeProvider">The game mode provider.</param>
        public GameModeClient(string pipeName, IGameModeProvider gameModeProvider)
        {
            _gameModeProvider = gameModeProvider ?? throw new ArgumentNullException(nameof(gameModeProvider));
            _pipeName = pipeName ?? throw new ArgumentNullException(nameof(pipeName));
        }

        /// <summary>
        /// Gets a value indicating whether this property is invoked on the main thread.
        /// </summary>
        private bool IsOnMainThread => _mainThread == Thread.CurrentThread.ManagedThreadId;

        #region Implementation of ICommandReceiveScope

        /// <summary>
        ///     Gets a value indicating whether the <see cref="IGameModeClient" /> should continue to receive commands.
        /// </summary>
        bool ICommandReceiveScope.ShouldReceiveCommands { get; } = true;

        #endregion

        /// <summary>
        ///     Registers a callback with the specified <see cref="name" />. When the callback is called, the specified
        ///     <see cref="methodInfo" /> will be invoked on the specified <see cref="target" />.
        /// </summary>
        /// <param name="name">The name af the callback to register.</param>
        /// <param name="target">The target on which to invoke the method.</param>
        /// <param name="methodInfo">The method information of the method to invoke when the callback is called.</param>
        /// <param name="parameters">The parameters of the callback.</param>
        /// <returns></returns>
        public async Task RegisterCallback(string name, object target, MethodInfo methodInfo, params CallbackParameterInfo[] parameters)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));

            AssertRunning();

            // TODO: threading

            _callbacks[name] = new Callback(target, methodInfo, parameters);

            await _client.Send(ServerCommand.RegisterCall, ValueConverter.GetBytes(name)
                .Concat(parameters.SelectMany(c => c.GetBytes()))
                .Concat(new[] { (byte) ServerCommandArgument.Terminator }));
        }

        /// <summary>
        ///     Registers a callback with the specified <see cref="name" />. When the callback is called, the specified
        ///     <see cref="methodInfo" /> will be invoked on the specified <see cref="target" />.
        /// </summary>
        /// <param name="name">The name af the callback to register.</param>
        /// <param name="target">The target on which to invoke the method.</param>
        /// <param name="methodInfo">The method information of the method to invoke when the callback is called.</param>
        /// <returns></returns>
        public async Task RegisterCallback(string name, object target, MethodInfo methodInfo)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));

            AssertRunning();

            var parameterInfos = methodInfo.GetParameters();
            var parameters = new CallbackParameterInfo[parameterInfos.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                if (new[] { typeof(int), typeof(float), typeof(bool) }.Contains(parameterInfos[i].ParameterType))
                    parameters[i] = CallbackParameterInfo.Value;
                else if (new[] { typeof(int[]), typeof(float[]), typeof(bool[]) }.Contains(parameterInfos[i].ParameterType))
                {
                    var attribute = parameterInfos[i].GetCustomAttribute<ParameterLengthAttribute>();
                    if (attribute == null)
                        throw new CallbackRegistrationException("Parameters of array types must have an attached ParameterLengthAttribute.");
                    parameters[i] = CallbackParameterInfo.Array(attribute.Index);
                }
                else if (typeof(string) == parameterInfos[i].ParameterType)
                    parameters[i] = CallbackParameterInfo.String;
                else
                    throw new CallbackRegistrationException("The method contains unsupported parameter types");
            }

            // TODO: Check return type?

            await RegisterCallback(name, target, methodInfo, parameters);
        }

        public async Task<TimeSpan> Ping()
        {
            if (!IsOnMainThread)
                throw new Exception("Can only ping from the main thread");

            var pong = new PongReceiver();
            _pongs.Enqueue(pong);

            pong.Ping();
            await _client.Send(ServerCommand.Ping, null);
            await Receive(pong);
            return pong.Result;
        }

        private async Task Initialize()
        {
            Log("SampSharp GameMode Server");
            Log("-------------------------");
            Log($"v{CoreVersion.Version.ToString(3)}, (C)2014-2017 Tim Potze");
            Log("");

            _mainThread = Thread.CurrentThread.ManagedThreadId;

            _client = new PipeClient();

            LogInfo($"Connecting to the server on pipe {_pipeName}...");
            await _client.Connect(_pipeName);

            LogInfo("Connected! Waiting for server annoucement...");
            while (true)
            {
                var version = await _client.Receive();
                if (version.Command == ServerCommand.Announce)
                {
                    var protocolVersion = ValueConverter.ToUInt32(version.Data, 0);
                    var pluginVersion = ValueConverter.ToVersion(version.Data, 4);

                    if (protocolVersion != CoreVersion.ProtocolVersion)
                    {
                        var updatee = protocolVersion > CoreVersion.ProtocolVersion ? "game mode" : "server";
                        LogError(
                            $"Protocol version mismatch! The server is running protocol {protocolVersion} but the game mode is running {CoreVersion.ProtocolVersion}");
                        LogError($"Please update your {updatee}.");
                        return;
                    }
                    LogInfo($"Connected to version {pluginVersion.ToString(3)} via protocol {protocolVersion}");

                    break;
                }
                LogError($"Received command {version.Command.ToString().ToLower()} instead of announce.");
            }

            _gameModeProvider.Initialize(this);

            LogInfo("Sending start signal to server...");
            await _client.Send(ServerCommand.Start, null);
        }

        private async Task Receive(ICommandReceiveScope scope)
        {
            if (scope == null) throw new ArgumentNullException(nameof(scope));
            while (scope.ShouldReceiveCommands)
            {
                var data = await _client.Receive();

                LogError($"DEBUG: Received {data.Command} with {data.Data?.Length} data");
                switch (data.Command)
                {
                    case ServerCommand.Tick:
                        _gameModeProvider.Tick();
                        break;
                    case ServerCommand.Pong:
                        if (_pongs.Count == 0)
                            LogError("Received a random pong");
                        else
                            _pongs.Dequeue().Pong();
                        break;
                    case ServerCommand.Announce:
                        LogError("Received a random server announcement");
                        break;
                    case ServerCommand.PublicCall:
                        var name = ValueConverter.ToString(data.Data, 0);
                        if (_callbacks.TryGetValue(name, out var callback))
                        {
                            var result = callback.Invoke(data.Data, name.Length + 1);
                            await _client.Send(ServerCommand.Response, ValueConverter.GetBytes(result));
                        }
                        else
                            LogError("Received unknown callback " + name);
                        break;
                    case ServerCommand.Reply:
                        throw new NotImplementedException();
                }
            }
        }

        public async Task Run()
        {
            await Initialize();

            await Receive(this);
        }

        private void AssertRunning()
        {
            if (_client == null)
                throw new GameModeNotRunningException();
        }

        #region Logging

        // TODO: Some common logging structure which can also be used by SampSharp.GameMode.
        private void Log(string level, string message)
        {
            Console.WriteLine($"[SampSharp:{level}] {message}");
        }

        private void Log(string message)
        {
            Console.WriteLine(message);
        }

        private void LogError(string message) => Log("ERROR", message);
        private void LogInfo(string message) => Log("INFO", message);

        #endregion
    }
}