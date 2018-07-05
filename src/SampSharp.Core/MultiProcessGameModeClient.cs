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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SampSharp.Core.Callbacks;
using SampSharp.Core.Communication;
using SampSharp.Core.Communication.Clients;
using SampSharp.Core.Logging;
using SampSharp.Core.Natives;
using SampSharp.Core.Threading;

namespace SampSharp.Core
{
    /// <summary>
    ///     Represents a SampSharp game mode client.
    /// </summary>
    public sealed class MultiProcessGameModeClient : IGameModeClient, IGameModeRunner
    {
        private static readonly byte[] AOne = { 1 };
        private static readonly byte[] AZero = { 0 };

        private readonly Dictionary<string, Callback> _callbacks = new Dictionary<string, Callback>();
        private readonly CommandWaitQueue _commandWaitQueue = new CommandWaitQueue();
        private readonly IGameModeProvider _gameModeProvider;
        private readonly Queue<PongReceiver> _pongs = new Queue<PongReceiver>();
        private readonly GameModeStartBehaviour _startBehaviour;
        private int _mainThread;
        private MessagePump _messagePump;
        private bool _initReceived;
        private bool _running;
        private bool _shuttingDown;
        private bool _canTick;
        private SampSharpSyncronizationContext _syncronizationContext;
        private DateTime _lastSend;
        private ushort _callerIndex;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MultiProcessGameModeClient" /> class.
        /// </summary>
        /// <param name="communicationClient">The communication client to be used by the game mode client.</param>
        /// <param name="startBehaviour">The start method.</param>
        /// <param name="gameModeProvider">The game mode provider.</param>
        /// <param name="encoding">The encoding to use when en/decoding text messages sent to/from the server.</param>
        public MultiProcessGameModeClient(ICommunicationClient communicationClient, GameModeStartBehaviour startBehaviour, IGameModeProvider gameModeProvider, Encoding encoding)
        {
            Encoding = encoding;
            _startBehaviour = startBehaviour;
            _gameModeProvider = gameModeProvider ?? throw new ArgumentNullException(nameof(gameModeProvider));
            CommunicationClient = communicationClient ?? throw new ArgumentNullException(nameof(communicationClient));
            NativeLoader = new NativeLoader(this);
        }

        /// <summary>
        ///     Gets the default encoding to use when translating server messages.
        /// </summary>
        public Encoding Encoding { get; }

        /// <summary>
        ///     Gets a value indicating whether this property is invoked on the main thread.
        /// </summary>
        public bool IsOnMainThread => _mainThread == Thread.CurrentThread.ManagedThreadId;

        private void ProcessCommand(ServerCommandData data)
        {
            switch (data.Command)
            {
                case ServerCommand.Tick:
                    if (!_canTick)
                        break;

                    _gameModeProvider.Tick();

                    // The server expects at least a message every 5 seconds or else the debug pause
                    // detector kicks in. Send one at least every 3 to be safe.
                    if (DateTime.UtcNow - _lastSend > TimeSpan.FromSeconds(3))
                        Send(ServerCommand.Alive, null);

                    break;
                case ServerCommand.Pong:
                    if (_pongs.Count == 0)
                    {
                        CoreLog.Log(CoreLogLevel.Error, "Received a random pong");
                        CoreLog.Log(CoreLogLevel.Debug, Environment.StackTrace);
                    }
                    else
                        _pongs.Dequeue().Pong();
                    break;
                case ServerCommand.Announce:
                    CoreLog.Log(CoreLogLevel.Error, "Received a random server announcement");
                    CoreLog.Log(CoreLogLevel.Debug, Environment.StackTrace);
                    break;
                case ServerCommand.PublicCall:
                    var name = ValueConverter.ToString(data.Data, 0, Encoding);
                    var isInit = name == "OnGameModeInit";

                    if (CoreLog.DoesLog(CoreLogLevel.Verbose))
                        CoreLog.LogVerbose("Incoming public call: {0}", name);

                    if ((_startBehaviour == GameModeStartBehaviour.Gmx || _startBehaviour == GameModeStartBehaviour.FakeGmx) && !_initReceived &&
                        !isInit)
                    {
                        CoreLog.Log(CoreLogLevel.Debug, $"Skipping callback {name} because OnGameModeInit has not yet been called");
                        Send(ServerCommand.Response, AZero);
                        break;
                    }

                    var isFirstInit = isInit && !_initReceived;
                    if (isFirstInit)
                    {
                        _initReceived = true;
                    }

                    if (_callbacks.TryGetValue(name, out var callback))
                    {
                        int? result = null;
                        try
                        {
                            result = callback.Invoke(data.Data, name.Length + 1);
                            
                            CoreLog.LogVerbose("Public call response for {0}: {1}", name, result);
                        }
                        catch (Exception e)
                        {
                            OnUnhandledException(new UnhandledExceptionEventArgs(e));
                        }

                        Send(ServerCommand.Response,
                            result != null
                                ? AOne.Concat(ValueConverter.GetBytes(result.Value))
                                : AZero);
                    }
                    else
                    {
                        CoreLog.Log(CoreLogLevel.Error, "Received unknown callback " + name);
                        CoreLog.Log(CoreLogLevel.Debug, Environment.StackTrace);
                    }

                    if (isFirstInit)
                    {
                        if (_startBehaviour == GameModeStartBehaviour.FakeGmx)
                        {
                            _callbacks.TryGetValue("OnIncomingConnection", out var onIncomingConnection);
                            _callbacks.TryGetValue("OnPlayerConnect", out var onPlayerConnect);
                            _callbacks.TryGetValue("OnPlayerRequestClass", out var onPlayerRequestClass);

                            var natIsPlayerConnected = NativeLoader.Load("IsPlayerConnected", new[]
                            {
                                NativeParameterInfo.ForType(typeof(int))
                            });
                            var natGetPlayerPoolSize = NativeLoader.Load("GetPlayerPoolSize", new NativeParameterInfo[0]);
                            var natForceClassSelection =
                                NativeLoader.Load("ForceClassSelection", new[]
                                {
                                    NativeParameterInfo.ForType(typeof(int))
                                });
                            var natTogglePlayerSpectating = NativeLoader.Load("TogglePlayerSpectating",
                                new[]
                                {
                                    NativeParameterInfo.ForType(typeof(int)),
                                    NativeParameterInfo.ForType(typeof(int))
                                });
                            var natGetPlayerIp = NativeLoader.Load("GetPlayerIp",
                                new[]
                                {
                                    NativeParameterInfo.ForType(typeof(int)),
                                    new NativeParameterInfo(NativeParameterType.StringReference, 2),
                                    NativeParameterInfo.ForType(typeof(int)), 
                                });
                            
                            var poolSize = natGetPlayerPoolSize.Invoke();
                            for (var i = 0; i <= poolSize; i++)
                            {
                                var isConnected = natIsPlayerConnected.Invoke(i);

                                if (isConnected == 0)
                                    continue;

                                var args = new object[] { i, null, 16 };
                                natGetPlayerIp.Invoke(args);

                                if (args[1] is string ip)
                                    onIncomingConnection?.Invoke(
                                        ValueConverter.GetBytes(i)
                                            .Concat(ValueConverter.GetBytes(ip, Encoding.ASCII))
                                            .Concat(ValueConverter.GetBytes(9999999))
                                            .ToArray(), 0);

                                onPlayerConnect?.Invoke(ValueConverter.GetBytes(i), 0);

                                natForceClassSelection.Invoke(i);
                                natTogglePlayerSpectating.Invoke(i, 1);
                                natTogglePlayerSpectating.Invoke(i, 0);

                                onPlayerRequestClass?.Invoke(
                                    ValueConverter.GetBytes(i)
                                        .Concat(ValueConverter.GetBytes(0))
                                        .ToArray(), 0);

                            }
                        }

                        _canTick = true;
                    }
                    else if (_initReceived && name == "OnGameModeExit")
                    {
                        CoreLog.Log(CoreLogLevel.Info, "OnGameModeExit received, sending reconnect signal...");
                        Send(ServerCommand.Reconnect, null);

                        // Give the server time to receive the reconnect signal.
                        // TODO: This is an ugly freeze/comms-deadlock fix.
                        Thread.Sleep(100);

                        CleanUp();
                    }
                    break;
                default:
                    CoreLog.Log(CoreLogLevel.Error, $"Unknown command {data.Command} recieved with {data.Data?.Length.ToString() ?? "NULL"} data");
                    CoreLog.Log(CoreLogLevel.Debug, Environment.StackTrace);
                    break;
            }
        }

        private void CleanUp()
        {
            _running = false;
            _canTick = false;
            _initReceived = false;
            _messagePump.Dispose();
        }

        private static bool VerifyVersionData(ServerCommandData data)
        {
            CoreLog.Log(CoreLogLevel.Debug, $"Received {data.Command} while waiting for server announcement.");
            if (data.Command == ServerCommand.Announce)
            {
                var protocolVersion = ValueConverter.ToUInt32(data.Data, 0);
                var pluginVersion = ValueConverter.ToVersion(data.Data, 4);

                if (protocolVersion != CoreVersion.ProtocolVersion)
                {
                    var updatee = protocolVersion > CoreVersion.ProtocolVersion ? "game mode" : "server";
                    CoreLog.Log(CoreLogLevel.Error,
                        $"Protocol version mismatch! The server is running protocol {protocolVersion} but the game mode is running {CoreVersion.ProtocolVersion}");
                    CoreLog.Log(CoreLogLevel.Error, $"Please update your {updatee}.");
                    return false;
                }
                CoreLog.Log(CoreLogLevel.Info, $"Connected to version {pluginVersion.ToString(3)} via protocol {protocolVersion}");

                return true;
            }
            CoreLog.Log(CoreLogLevel.Error, $"Received command {data.Command.ToString().ToLower()} instead of announce.");
            return false;
        }

        private async void Initialize()
        {
            CoreLog.Log(CoreLogLevel.Initialisation, "SampSharp GameMode Client");
            CoreLog.Log(CoreLogLevel.Initialisation, "-------------------------");
            CoreLog.Log(CoreLogLevel.Initialisation, $"v{CoreVersion.Version.ToString(3)}, (C)2014-2018 Tim Potze");
            CoreLog.Log(CoreLogLevel.Initialisation, "");

            _mainThread = Thread.CurrentThread.ManagedThreadId;
            _running = true;

            CoreLog.Log(CoreLogLevel.Info, $"Connecting to the server via {CommunicationClient}...");
            await CommunicationClient.Connect();

            CoreLog.Log(CoreLogLevel.Info, "Set up networking routine...");
            StartNetworkingRoutine();

            CoreLog.Log(CoreLogLevel.Info, "Connected! Waiting for server annoucement...");
            ServerCommandData data;

            do
            {
                data = await _commandWaitQueue.WaitAsync();

                // Could receive ticks if reconnecting.
            } while (data.Command == ServerCommand.Tick);

            if (!VerifyVersionData(data))
                return;

            CoreLog.Log(CoreLogLevel.Info, "Initialializing game mode provider...");
            _gameModeProvider.Initialize(this);

            CoreLog.Log(CoreLogLevel.Info, "Sending start signal to server...");
            Send(ServerCommand.Start, new[] { (byte) _startBehaviour });

            CoreLog.Log(CoreLogLevel.Info, "Set up main routine...");
            MainRoutine();
        }

        private void AssertRunning()
        {
            if (CommunicationClient == null || !_running)
                throw new GameModeNotRunningException();
        }

        private void OnUnhandledException(UnhandledExceptionEventArgs e)
        {
            UnhandledException?.Invoke(this, e);
        }

        #region Routines

        private async void MainRoutine()
        {
            while (_running)
            {
                var data = await _commandWaitQueue.WaitAsync();
                ProcessCommand(data);

                if (_shuttingDown)
                    CleanUp();
            }
        }

        private async Task NetworkingRoutine()
        {
            try
            {
                while (_running)
                {
                    var data = await CommunicationClient.ReceiveAsync();

                    if (!_running)
                        return;

                    _commandWaitQueue.Release(data);
                }
            }
            catch (StreamCommunicationClientClosedException)
            {
                CoreLog.Log(CoreLogLevel.Warning, "Network routine ended because the communication with the SA:MP server was closed.");
            }
            catch (Exception e)
            {
                CoreLog.Log(CoreLogLevel.Error, "Network routine died! " + e);
            }
        }

        private void StartNetworkingRoutine()
        {
            Task.Run(() => NetworkingRoutine().ConfigureAwait(false));
        }

        #endregion

        #region Communication

        private void Send(ServerCommand command, IEnumerable<byte> data)
        {
            if (!IsOnMainThread)
                throw new GameModeClientException("Cannot send data to the server from a thread other than the main thread.");

            try
            {
                CommunicationClient.Send(command, data);
                _lastSend = DateTime.UtcNow;
            }
            catch (IOException e)
            {
                throw new ServerConnectionClosedException("The server connection has closed. Did the server shut down?", e);
            }
        }

        private void SendOnMainThread(ServerCommand command, IEnumerable<byte> data)
        {
            if (IsOnMainThread)
                Send(command, data);
            else
                _syncronizationContext.Send(ctx => Send(command, data), null);
        }

        private ServerCommandData SendAndWait(ServerCommand command, IEnumerable<byte> data, Func<ServerCommandData, bool> accept = null)
        {
            Send(command, data);
            
            for (;;)
            {
                var responseData = _commandWaitQueue.Wait(accept);
                
                if (responseData.Command == ServerCommand.Response)
                    return responseData;
                
                ProcessCommand(responseData);
            }
        }

        private ServerCommandData SendAndWaitOnMainThread(ServerCommand command, IEnumerable<byte> data, Func<ServerCommandData, bool> accept = null)
        {

            if (IsOnMainThread)
                return SendAndWait(command, data, accept);

            var responseData = default(ServerCommandData);
            _syncronizationContext.Send(ctx => responseData = SendAndWait(command, data, accept), null);
            return responseData;
        }

        #endregion
        
        /// <summary>
        ///     Pings the server.
        /// </summary>
        /// <returns>The ping to the server.</returns>
        public async Task<TimeSpan> Ping()
        {
            var pong = new PongReceiver();
            if (IsOnMainThread)
            {
                _pongs.Enqueue(pong);

                pong.Ping();
                Send(ServerCommand.Ping, null);
            }
            else
            {
                _syncronizationContext.Send(ctx =>
                {
                    _pongs.Enqueue(pong);

                    pong.Ping();
                    Send(ServerCommand.Ping, null);
                }, null);
            }
            return await pong.Task;
        }

        private ushort GetCallerId()
        {
            unchecked
            {
                _callerIndex++;
            }

            return _callerIndex;
        }

        #region Implementation of IGameModeClient

        /// <summary>
        ///     Gets the communicaton client.
        /// </summary>
        public ICommunicationClient CommunicationClient { get; }

        /// <summary>
        ///     Gets or sets the native loader to be used to load natives.
        /// </summary>
        public INativeLoader NativeLoader { get; set; }

        /// <summary>
        ///     Occurs when an exception is unhandled during the execution of a callback or tick.
        /// </summary>
        public event EventHandler<UnhandledExceptionEventArgs> UnhandledException;

        /// <summary>
        ///     Registers a callback with the specified <paramref name="name" />. When the callback is called, the specified
        ///     <paramref name="methodInfo" /> will be invoked on the specified <paramref name="target" />.
        /// </summary>
        /// <param name="name">The name af the callback to register.</param>
        /// <param name="target">The target on which to invoke the method.</param>
        /// <param name="methodInfo">The method information of the method to invoke when the callback is called.</param>
        /// <param name="parameters">The parameters of the callback.</param>
        public void RegisterCallback(string name, object target, MethodInfo methodInfo, CallbackParameterInfo[] parameters)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));

            AssertRunning();

            if (!Callback.IsValidReturnType(methodInfo.ReturnType))
                throw new CallbackRegistrationException("The method uses an unsupported return type");

            _callbacks[name] = new Callback(target, methodInfo, name, parameters, this);

            SendOnMainThread(ServerCommand.RegisterCall, ValueConverter.GetBytes(name, Encoding)
                .Concat(parameters.SelectMany(c => c.GetBytes()))
                .Concat(new[] { (byte) ServerCommandArgument.Terminator }));
        }
        
        /// <summary>
        ///     Prints the specified text to the server console.
        /// </summary>
        /// <param name="text">The text to print to the server console.</param>
        public void Print(string text)
        {
            AssertRunning();

            if (text == null)
                text = string.Empty;

            SendOnMainThread(ServerCommand.Print, ValueConverter.GetBytes(text, Encoding));
        }

        /// <summary>
        ///     Gets the handle of the native with the specified <paramref name="name" />.
        /// </summary>
        /// <param name="name">The name of the native.</param>
        /// <returns>The handle of the native with the specified <paramref name="name" />.</returns>
        public int GetNativeHandle(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            var caller = GetCallerId();
            var data = SendAndWaitOnMainThread(ServerCommand.FindNative,
                ValueConverter.GetBytes(caller).Concat(ValueConverter.GetBytes(name, Encoding)), d => d.Command != ServerCommand.Response || (d.Data != null && d.Data.Length >= 2 && ValueConverter.ToUInt16(d.Data, 0) == caller));

            if (data.Data.Length != 6)
                throw new Exception("Invalid FindNative response from server.");
            
            return ValueConverter.ToInt32(data.Data, 2);
        }

        /// <summary>
        ///     Invokes a native using the specified <paramref name="data" /> buffer.
        /// </summary>
        /// <param name="data">The data buffer to be used.</param>
        /// <returns>The response from the native.</returns>
        public byte[] InvokeNative(IEnumerable<byte> data)
        {
            var caller = GetCallerId();
            data = ValueConverter.GetBytes(caller).Concat(data);
            var response = SendAndWaitOnMainThread(ServerCommand.InvokeNative, data, d =>
            {
                return d.Command != ServerCommand.Response ||
                           (d.Data != null && d.Data.Length >= 2 && ValueConverter.ToUInt16(d.Data, 0) == caller);
            });
            return response.Data.Skip(2).ToArray(); // TODO: Optimize GC allocations
        }

        /// <summary>
        ///     Shuts down the server after the current callback has been processed.
        /// </summary>
        public void ShutDown()
        {
            if (_shuttingDown || !_running)
                return;

            CommunicationClient.Send(ServerCommand.Disconnect, null);

            // Give the server time to receive the reconnect signal.
            // TODO: Unexpected behaviour if called from outside a callback (because shuttingDown hook is inside callback handler).
            // TODO: This is an ugly fix.
            Thread.Sleep(100);
            _shuttingDown = true;
        }

        #endregion

        #region Implementation of IGameModeRunner

        /// <summary>
        ///     Runs this game mode client.
        /// </summary>
        /// <returns>true if shut down by the game mode, false otherwise.</returns>
        /// <exception cref="Exception">Thrown if a game mode is already running.</exception>
        public bool Run()
        {
            if (InternalStorage.RunningClient != null)
                throw new Exception("A game mode is already running!");

            InternalStorage.RunningClient = this;

            // Prepare the syncronization context
            var queue = new SemaphoreMessageQueue();
            _syncronizationContext = new SampSharpSyncronizationContext(queue);
            _messagePump = new MessagePump(queue);

            SynchronizationContext.SetSynchronizationContext(_syncronizationContext);

            // Initialize the game mode and start the main routine
            Initialize();

            // Pump new tasks
            _messagePump.Pump(e => OnUnhandledException(new UnhandledExceptionEventArgs(e)));

            // Clean up
            InternalStorage.RunningClient = null;
            CommunicationClient.Disconnect();

            return _shuttingDown;
        }

        /// <summary>
        ///     Gets the client of this game mode runner.
        /// </summary>
        public IGameModeClient Client => this;

        #endregion
    }
}