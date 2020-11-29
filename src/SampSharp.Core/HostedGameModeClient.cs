using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using SampSharp.Core.Callbacks;
using SampSharp.Core.Communication;
using SampSharp.Core.Hosting;
using SampSharp.Core.Logging;
using SampSharp.Core.Natives;
using SampSharp.Core.Natives.NativeObjects;
using SampSharp.Core.Natives.NativeObjects.NativeHandles;
using SampSharp.Core.Threading;

namespace SampSharp.Core
{
    /// <summary>
    ///     Represents a SampSharp game mode client for hosted game modes.
    /// </summary>
    public sealed class HostedGameModeClient : IGameModeClient, IGameModeRunner, ISynchronizationProvider
    {
        private readonly Dictionary<string, Callback> _callbacks = new Dictionary<string, Callback>();
        private NoWaitMessageQueue _messageQueue;
        private SampSharpSynchronizationContext _synchronizationContext;
        private readonly IGameModeProvider _gameModeProvider;
        private int _mainThread;
        private int _rconThread = int.MinValue;
        private bool _running;
        private byte[] _publicCallBuffer = new byte[1024 * 6];
        private IntPtr _buffer;
        private readonly IntPtr _buffer1K;
        private int _txBufferLength;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MultiProcessGameModeClient" /> class.
        /// </summary>
        /// <param name="gameModeProvider">The game mode provider.</param>
        /// <param name="encoding">The encoding to use when en/decoding text messages sent to/from the server.</param>
        public HostedGameModeClient(IGameModeProvider gameModeProvider, Encoding encoding)
        {
            Encoding = encoding;
            _gameModeProvider = gameModeProvider ?? throw new ArgumentNullException(nameof(gameModeProvider));
            NativeLoader = new NativeLoader(this);
            _buffer = Marshal.AllocHGlobal(_txBufferLength = 1024 * 6);
            _buffer1K = Marshal.AllocHGlobal(1024);

            ServerPath = AppContext.BaseDirectory;
        }

        /// <summary>
        ///     Gets a value indicating whether this property is invoked on the main thread.
        /// </summary>
        public bool IsOnMainThread
        {
            get
            {
                var id = Thread.CurrentThread.ManagedThreadId;
                return _mainThread == id || _rconThread == id;
            }
        }

        private void AssertRunning()
        {
            if (!_running)
                throw new GameModeNotRunningException();
        }

        private void OnUnhandledException(UnhandledExceptionEventArgs e)
        {
            UnhandledException?.Invoke(this, e);
        }

        internal void Tick()
        {
            // Pump new tasks
            var message = _messageQueue.GetMessage();

            if (message != null)
            {
                try
                {
                    message.Execute();
                }
                catch (Exception e)
                {
                    OnUnhandledException(new UnhandledExceptionEventArgs("async", e));
                }
            }

            try
            {
                _gameModeProvider.Tick();
            }
            catch (Exception e)
            {
                OnUnhandledException(new UnhandledExceptionEventArgs("Tick", e));
            }
        }

        internal int PublicCall(string name, IntPtr data, int length)
        {
            try
            {
                if (name == "OnRconCommand")
                {
                    // RCON can be processed by the SA-MP server on a different thread. Mark thread as additional main thread.
                    _rconThread = Thread.CurrentThread.ManagedThreadId;
                }

                if (_callbacks.TryGetValue(name, out var callback))
                {
                    while (_publicCallBuffer.Length < length)
                        _publicCallBuffer = new byte[_publicCallBuffer.Length * 2];

                    Marshal.Copy(data, _publicCallBuffer, 0, length);

                    return callback.Invoke(_publicCallBuffer, 0) ?? 1;
                }
            }
            catch (Exception e)
            {
                OnUnhandledException(new UnhandledExceptionEventArgs(name, e));
            }

            return 1;
        }
        
        private void EnsureBufferSize(int length)
        {
            if (_txBufferLength >= length)
                return;
            
            Marshal.FreeHGlobal(_buffer);
            while (_txBufferLength < length) _txBufferLength *= 2;
            _buffer = Marshal.AllocHGlobal(_txBufferLength);
        }

        #region Implementation of IGameModeClient

        /// <inheritdoc />
        public Encoding Encoding { get; }
        
        /// <inheritdoc />
        public INativeLoader NativeLoader { get; }
        
        /// <inheritdoc />
        public ISynchronizationProvider SynchronizationProvider => this;
        
        /// <inheritdoc />
        public string ServerPath { get; private set; }
        
        /// <inheritdoc />
        public event EventHandler<UnhandledExceptionEventArgs> UnhandledException;
        
        /// <inheritdoc />
        public void RegisterCallback(string name, object target, MethodInfo methodInfo, CallbackParameterInfo[] parameters)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            AssertRunning();

            if (!Callback.IsValidReturnType(methodInfo.ReturnType))
                throw new CallbackRegistrationException("The method uses an unsupported return type");

            var callback = new Callback(target, methodInfo, name, this);

            if(!callback.MatchesParameters(parameters))
                throw new CallbackRegistrationException("The method does not match the specified parameters.");

            _callbacks[name] = callback;

            var data = ValueConverter.GetBytes(name, Encoding)
                .Concat(parameters.SelectMany(c => c.GetBytes()))
                .Concat(new[] { (byte) ServerCommandArgument.Terminator }).ToArray();
            
            if (IsOnMainThread)
            {
                EnsureBufferSize(data.Length);
                Marshal.Copy(data, 0, _buffer, data.Length);
                Interop.RegisterCallback(_buffer);
            }
            else
                _synchronizationContext.Send(ctx =>
                {
                    EnsureBufferSize(data.Length);
                    Marshal.Copy(data, 0, _buffer, data.Length);
                    Interop.RegisterCallback(_buffer);
                }, null);
        }

        /// <inheritdoc />
        public void RegisterCallback(string name, object target, MethodInfo methodInfo, CallbackParameterInfo[] parameters, Type[] parameterTypes)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (parameterTypes == null) throw new ArgumentNullException(nameof(parameterTypes));

            AssertRunning();

            if (!Callback.IsValidReturnType(methodInfo.ReturnType))
                throw new CallbackRegistrationException("The method uses an unsupported return type");

            var callback = new Callback(target, methodInfo, name, parameterTypes, this);

            if(!callback.MatchesParameters(parameters))
                throw new CallbackRegistrationException("The method does not match the specified parameters.");

            _callbacks[name] = callback;

            var data = ValueConverter.GetBytes(name, Encoding)
                .Concat(parameters.SelectMany(c => c.GetBytes()))
                .Concat(new[] { (byte) ServerCommandArgument.Terminator }).ToArray();
            
            if (IsOnMainThread)
            {
                EnsureBufferSize(data.Length);
                Marshal.Copy(data, 0, _buffer, data.Length);
                Interop.RegisterCallback(_buffer);
            }
            else
                _synchronizationContext.Send(ctx =>
                {
                    EnsureBufferSize(data.Length);
                    Marshal.Copy(data, 0, _buffer, data.Length);
                    Interop.RegisterCallback(_buffer);
                }, null);

        }
        
        /// <inheritdoc />
        public void Print(string text)
        {
            if (IsOnMainThread)
                Interop.Print(text);
            else
                _synchronizationContext.Send(ctx => Interop.Print(text), null);
        }
        
        /// <inheritdoc />
        public int GetNativeHandle(string name)
        {
            if (IsOnMainThread)
                return Interop.GetNativeHandle(name);

            var result = 0;
            _synchronizationContext.Send(ctx => result = Interop.GetNativeHandle(name), null);
            return result;
        }
        
        /// <inheritdoc />
        public byte[] InvokeNative(IEnumerable<byte> data)
        {
            // TODO: Optimize byte array allocations
            var adata = data.ToArray();

            var outlen = 1024;
            var response = new byte[outlen];

            if (IsOnMainThread)
            {
                EnsureBufferSize(adata.Length);
                Marshal.Copy(adata, 0, _buffer, adata.Length);
                Interop.InvokeNative(_buffer, adata.Length, _buffer1K, ref outlen);
                Marshal.Copy(_buffer1K, response, 0, outlen);
            }
            else
                _synchronizationContext.Send(ctx =>
                    {
                        EnsureBufferSize(adata.Length);
                        Marshal.Copy(adata, 0, _buffer, adata.Length);
                        Interop.InvokeNative(_buffer, adata.Length, _buffer1K, ref outlen);
                        Marshal.Copy(_buffer1K, response, 0, outlen);
                    },
                    null);


            return response;
        }
        
        /// <inheritdoc />
        public void ShutDown()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IGameModeRunner
        
        /// <inheritdoc />
        public bool Run()
        {
            InternalStorage.RunningClient = this;

            // Prepare the syncronization context
            _messageQueue = new NoWaitMessageQueue();
            _synchronizationContext = new SampSharpSynchronizationContext(_messageQueue);

            SynchronizationContext.SetSynchronizationContext(_synchronizationContext);
            
            _mainThread = Thread.CurrentThread.ManagedThreadId;
            _running = true;

            CoreLog.Log(CoreLogLevel.Initialisation, "SampSharp GameMode Client");
            CoreLog.Log(CoreLogLevel.Initialisation, "-------------------------");
            CoreLog.Log(CoreLogLevel.Initialisation, $"v{CoreVersion.Version.ToString(3)}, (C)2014-2020 Tim Potze");
            CoreLog.Log(CoreLogLevel.Initialisation, "Hosted run mode is active.");
            CoreLog.Log(CoreLogLevel.Initialisation, "");
            
            // TODO: Verify plugin version

            _gameModeProvider.Initialize(this);

            return true;
        }
        
        /// <inheritdoc />
        public IGameModeClient Client => this;

        #endregion

        #region Implementation of ISynchronizationProvider
        
        /// <inheritdoc />
        bool ISynchronizationProvider.InvokeRequired => !IsOnMainThread;
        
        /// <inheritdoc />
        void ISynchronizationProvider.Invoke(Action action)
        {
            _synchronizationContext.Send(ctx => action(), null);
        }

        #endregion
    }
}
