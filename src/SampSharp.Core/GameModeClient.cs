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
using SampSharp.Core.Callbacks;
using SampSharp.Core.Communication;
using SampSharp.Core.Logging;
using SampSharp.Core.Natives;
using SampSharp.Core.Threading;

namespace SampSharp.Core
{
    /// <summary>
    ///     Represents a SampSharp game mode client.
    /// </summary>
    /// <seealso cref="SampSharp.Core.IGameModeClient" />
    public sealed class GameModeClient : IGameModeClient
    {
        private static readonly byte[] AOne = { 1 };
        private static readonly byte[] AZero = { 0 };

        private readonly Dictionary<string, Callback> _callbacks = new Dictionary<string, Callback>();
        private readonly Queue<PongReceiver> _pongs = new Queue<PongReceiver>();
        private readonly IGameModeProvider _gameModeProvider;
        private readonly GameModeStartBehaviour _startBehaviour;
        private readonly string _pipeName;
        private int _mainThread;
        private bool _started;
        private bool _initReceived;
        private bool _running = true;
        private SampSharpSyncronizationContext _syncronizationContext;
        private MessagePump _messagePump;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameModeClient" /> class.
        /// </summary>
        /// <param name="pipeName">Name of the pipe.</param>
        /// <param name="startBehaviour">The start method.</param>
        /// <param name="gameModeProvider">The game mode provider.</param>
        public GameModeClient(string pipeName, GameModeStartBehaviour startBehaviour, IGameModeProvider gameModeProvider)
        {
            _startBehaviour = startBehaviour;
            _gameModeProvider = gameModeProvider ?? throw new ArgumentNullException(nameof(gameModeProvider));
            _pipeName = pipeName ?? throw new ArgumentNullException(nameof(pipeName));
            NativeLoader = new NativeLoader(this);
        }

        private Thread _communicationThread;

        /// <summary>
        ///     Gets a value indicating whether this property is invoked on the main thread.
        /// </summary>
        public bool IsOnMainThread => _mainThread == Thread.CurrentThread.ManagedThreadId;

        private void ProcessCommand(ServerCommandData data)
        {
            switch (data.Command)
            {
                case ServerCommand.Tick:
                    _gameModeProvider.Tick();
                    break;
                case ServerCommand.Pong:
                    if (_pongs.Count == 0)
                        CoreLog.Log(CoreLogLevel.Error, "Received a random pong");
                    else
                        _pongs.Dequeue().Pong();
                    break;
                case ServerCommand.Announce:
                    CoreLog.Log(CoreLogLevel.Error, "Received a random server announcement");
                    break;
                case ServerCommand.PublicCall:
                    var name = ValueConverter.ToString(data.Data, 0);
                    if (_callbacks.TryGetValue(name, out var callback))
                    {
                        int? result = null;
                        try
                        {
                            result = callback.Invoke(data.Data, name.Length + 1);
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
                        CoreLog.Log(CoreLogLevel.Error, "Received unknown callback " + name);

                    if (!_initReceived && name == "OnGameModeInit")
                    {
                        _initReceived = true;
                    }
                    else if (_initReceived && name == "OnGameModeExit")
                    {
                        _gameModeProvider.Dispose();
                        Pipe.Dispose();
                        _messagePump.Dispose();
                        _running = false;
                    }
                    break;
                case ServerCommand.Reply:
                    CoreLog.Log(CoreLogLevel.Error, "Received a random reply");
                    break;
                default:
                    CoreLog.Log(CoreLogLevel.Error, $"Unknown command {data.Command} recieved with {data.Data.Length} data");
                    break;
            }
        }

        private async Task MainRoutine()
        {
            while(_running)
            {
                var data = await ReceiveCommandAsync();
                ProcessCommand(data);
            }
        }

        private void NetworkingRoutine()
        {
            while (_running)
            {
                var data = Pipe.Receive();
                _dataReceivers.Push(data);
            }
        }
        
        private readonly DataReceiveStack<ServerCommandData> _dataReceivers = new DataReceiveStack<ServerCommandData>();


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
            CoreLog.Log(CoreLogLevel.Initialisation, "SampSharp GameMode Server");
            CoreLog.Log(CoreLogLevel.Initialisation, "-------------------------");
            CoreLog.Log(CoreLogLevel.Initialisation, $"v{CoreVersion.Version.ToString(3)}, (C)2014-2017 Tim Potze");
            CoreLog.Log(CoreLogLevel.Initialisation, "");

            _mainThread = Thread.CurrentThread.ManagedThreadId;

            Pipe = new PipeClient();

            CoreLog.Log(CoreLogLevel.Info, $"Connecting to the server on pipe {_pipeName}...");
            await Pipe.Connect(_pipeName);

            CoreLog.Log(CoreLogLevel.Info, "Set up networking thread...");
            _communicationThread = new Thread(NetworkingRoutine) { Name = "Networking Thread" };
            _communicationThread.Start();// TODO: Kill thread properly when done

            CoreLog.Log(CoreLogLevel.Info, "Connected! Waiting for server annoucement...");
            var data = await ReceiveCommandAsync();

            if (!VerifyVersionData(data))
                return;
            
            CoreLog.Log(CoreLogLevel.Info, "Initialializing game mode provider...");
            _gameModeProvider.Initialize(this);
        }

        internal void Run()
        {
            if (InternalStorage.RunningClient != null)
                throw new Exception("A game mode is already running!");

            InternalStorage.RunningClient = this;

            // Prepare the syncronization context
            _syncronizationContext = new SampSharpSyncronizationContext();
            _messagePump = _syncronizationContext.MessagePump;

            SynchronizationContext.SetSynchronizationContext(_syncronizationContext);

            // Initialize the game mode
            Initialize();

            // Pump new tasks
            _messagePump.Pump();

            InternalStorage.RunningClient = null;
        }

        private void AssertRunning()
        {
            if (Pipe == null || !_running)
                throw new GameModeNotRunningException();
        }
        
        private void OnUnhandledException(UnhandledExceptionEventArgs e)
        {
            UnhandledException?.Invoke(this, e);
        }

        #region Communication

        private void Send(ServerCommand command, IEnumerable<byte> data)
        {
            if (!IsOnMainThread)
                throw new GameModeClientException("Cannot send data to the server from a thread other than the main thread.");

            Pipe.Send(command, data);
        }

        private async Task<ServerCommandData> ReceiveCommandAsync()
        {
            return await _dataReceivers.PopAsync();
            //return await Pipe.ReceiveAsync();
        }
        
        private ServerCommandData ReceiveCommand()
        {
            return _dataReceivers.Pop();
            //return Pipe.Receive();
        }

        private ServerCommandData ReceiveCommand(ServerCommand type)
        {
            for (;;)
            {
                var data = ReceiveCommand();
                if (data.Command == type)
                    return data;

                ProcessCommand(data);
            }
        }

        #endregion

        #region Implementation of IGameModeClient

        /// <summary>
        ///     Gets the named pipe connection.
        /// </summary>
        public IPipeClient Pipe { get; private set; }

        /// <summary>
        ///     Gets or sets the native loader to be used to load natives.
        /// </summary>
        public INativeLoader NativeLoader { get; set; }

        /// <summary>
        ///     Occurs when an exception is unhandled during the execution of a callback or tick.
        /// </summary>
        public event EventHandler<UnhandledExceptionEventArgs> UnhandledException;

        /// <summary>
        ///     Registers a callback with the specified <paramref name="name"/>. When the callback is called, the specified
        ///     <paramref name="methodInfo"/> will be invoked on the specified <paramref name="target" />.
        /// </summary>
        /// <param name="name">The name af the callback to register.</param>
        /// <param name="target">The target on which to invoke the method.</param>
        /// <param name="methodInfo">The method information of the method to invoke when the callback is called.</param>
        /// <param name="parameters">The parameters of the callback.</param>
        public void RegisterCallback(string name, object target, MethodInfo methodInfo, params CallbackParameterInfo[] parameters)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));

            AssertRunning();

            if (!Callback.IsValidReturnType(methodInfo.ReturnType))
                throw new CallbackRegistrationException("The method uses an unsupported return type");

            _callbacks[name] = new Callback(target, methodInfo, name, parameters);

            if (IsOnMainThread)
            {
                Send(ServerCommand.RegisterCall, ValueConverter.GetBytes(name)
                    .Concat(parameters.SelectMany(c => c.GetBytes()))
                    .Concat(new[] { (byte) ServerCommandArgument.Terminator }));
            }
            else
            {
                _syncronizationContext.Send(ctx =>
                    Send(ServerCommand.RegisterCall, ValueConverter.GetBytes(name)
                        .Concat(parameters.SelectMany(c => c.GetBytes()))
                        .Concat(new[] { (byte) ServerCommandArgument.Terminator })), null);
            }
        }

        /// <summary>
        ///     Registers a callback with the specified <paramref name="name" />. When the callback is called, the specified
        ///     <paramref name="methodInfo" /> will be invoked on the specified <paramref name="target" />.
        /// </summary>
        /// <param name="name">The name af the callback to register.</param>
        /// <param name="target">The target on which to invoke the method.</param>
        /// <param name="methodInfo">The method information of the method to invoke when the callback is called.</param>
        public void RegisterCallback(string name, object target, MethodInfo methodInfo)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));

            AssertRunning();

            var parameterInfos = methodInfo.GetParameters();
            var parameters = new CallbackParameterInfo[parameterInfos.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                if (Callback.IsValidValueType(parameterInfos[i].ParameterType))
                    parameters[i] = CallbackParameterInfo.Value;
                else if (Callback.IsValidArrayType(parameterInfos[i].ParameterType))
                {
                    var attribute = parameterInfos[i].GetCustomAttribute<ParameterLengthAttribute>();
                    if (attribute == null)
                        throw new CallbackRegistrationException("Parameters of array types must have an attached ParameterLengthAttribute.");
                    parameters[i] = CallbackParameterInfo.Array(attribute.Index);
                }
                else if (Callback.IsValidStringType(parameterInfos[i].ParameterType))
                    parameters[i] = CallbackParameterInfo.String;
                else
                    throw new CallbackRegistrationException("The method contains unsupported parameter types");
            }

            RegisterCallback(name, target, methodInfo, parameters);
        }

        /// <summary>
        ///     Registers all callbacks in the specified target object. Instance methods with a <see cref="CallbackAttribute" />
        ///     attached will be loaded.
        /// </summary>
        /// <param name="target">The target.</param>
        public void RegisterCallbacksInObject(object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            AssertRunning();

            foreach (var method in target.GetType().GetTypeInfo().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var attribute = method.GetCustomAttribute<CallbackAttribute>();

                if (attribute == null)
                    continue;

                var name = attribute.Name;
                if (string.IsNullOrEmpty(name))
                    name = method.Name;

                RegisterCallback(name, target, method);
            }
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

            if (IsOnMainThread)
                Send(ServerCommand.Print, ValueConverter.GetBytes(text));
            else
                _syncronizationContext.Send(ctx => { Send(ServerCommand.Print, ValueConverter.GetBytes(text)); }, null);
        }

        /// <summary>
        ///     Start receiving ticks and public calls.
        /// </summary>
        public async void Start()
        {
            if (_started)
                throw new GameModeClientException("Game mode client already started.");

            _started = true;

            CoreLog.Log(CoreLogLevel.Info, "Sending start signal to server...");

            var method = new byte[1];
            method[0] = (byte) _startBehaviour;

            if (IsOnMainThread)
                Send(ServerCommand.Start, method);
            else
                _syncronizationContext.Send(ctx => Send(ServerCommand.Start, method), null);

            // The main routine ends when the game mode ends
            await MainRoutine();
        }

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

        /// <summary>
        ///     Gets the handle of the native with the specified <paramref name="name" />.
        /// </summary>
        /// <param name="name">The name of the native.</param>
        /// <returns>The handle of the native with the specified <paramref name="name" />.</returns>
        public int GetNativeHandle(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            var data = default(ServerCommandData);

            if (IsOnMainThread)
            {
                Send(ServerCommand.FindNative, ValueConverter.GetBytes(name));

                data = ReceiveCommand(ServerCommand.Response);
            }
            else
            {
                _syncronizationContext.Send(ctx =>
                {
                    Send(ServerCommand.FindNative, ValueConverter.GetBytes(name));

                    data = ReceiveCommand(ServerCommand.Response);
                }, null);
            }

            if (data.Data.Length != 4)
                throw new Exception("Invalid FindNative response from server.");

            return ValueConverter.ToInt32(data.Data, 0);
        }

        /// <summary>
        ///     Invokes a native using the specified <paramref name="data" /> buffer.
        /// </summary>
        /// <param name="data">The data buffer to be used.</param>
        /// <returns>The response from the native.</returns>
        public byte[] InvokeNative(IEnumerable<byte> data)
        {
            var response = default(ServerCommandData);

            if (IsOnMainThread)
            {
                Send(ServerCommand.InvokeNative, data);

                response = ReceiveCommand(ServerCommand.Response);
            }
            else
            {
                _syncronizationContext.Send(ctx =>
                {
                    Send(ServerCommand.InvokeNative, data);

                    response = ReceiveCommand(ServerCommand.Response);
                }, null);
            }

            return response.Data;
        }

        #endregion
    }
}