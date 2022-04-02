using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using SampSharp.Core.Callbacks;
using SampSharp.Core.Hosting;
using SampSharp.Core.Logging;
using SampSharp.Core.Natives;
using SampSharp.Core.Natives.NativeObjects;
using SampSharp.Core.Natives.NativeObjects.FastNatives;
using SampSharp.Core.Threading;

namespace SampSharp.Core
{
    /// <summary>
    ///     Represents a SampSharp game mode client for hosted game modes.
    /// </summary>
    public sealed class HostedGameModeClient : IGameModeClient, IGameModeRunner, ISynchronizationProvider
    {
        private readonly Dictionary<string, NewCallback> _newCallbacks = new();
        
        private NoWaitMessageQueue _messageQueue;
        private SampSharpSynchronizationContext _synchronizationContext;
        private readonly IGameModeProvider _gameModeProvider;
        private int _mainThread;
        private int _rconThread = int.MinValue;
        private bool _running;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HostedGameModeClient" /> class.
        /// </summary>
        /// <param name="gameModeProvider">The game mode provider.</param>
        /// <param name="encoding">The encoding to use when en/decoding text messages sent to/from the server.</param>
        public HostedGameModeClient(IGameModeProvider gameModeProvider, Encoding encoding)
        {
            Encoding = encoding;
            _gameModeProvider = gameModeProvider ?? throw new ArgumentNullException(nameof(gameModeProvider));
            NativeObjectProxyFactory = new FastNativeBasedNativeObjectProxyFactory(this);

            ServerPath = Directory.GetCurrentDirectory();
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
        
        internal void PublicCall(IntPtr amx, string name, IntPtr parameters, IntPtr retval)
        {
            try
            {
                if (name == "OnRconCommand")
                {
                    // RCON can be processed by the SA-MP server on a different thread. Mark thread as additional main thread.
                    _rconThread = Environment.CurrentManagedThreadId;
                }

                if (_newCallbacks.TryGetValue(name, out var callback))
                {
                    callback.Invoke(amx, parameters, retval);
                }
            }
            catch (Exception e)
            {
                OnUnhandledException(new UnhandledExceptionEventArgs(name, e));
            }
        }
        
        #region Implementation of IGameModeClient

        /// <inheritdoc />
        public Encoding Encoding { get; }
        
        /// <inheritdoc />
        public INativeObjectProxyFactory NativeObjectProxyFactory { get; }

        /// <inheritdoc />
        public ISynchronizationProvider SynchronizationProvider => this;
        
        /// <inheritdoc />
        public string ServerPath { get; private set; }
        
        /// <inheritdoc />
        public event EventHandler<UnhandledExceptionEventArgs> UnhandledException;
        
        public void RegisterCallback(string name, object target, MethodInfo methodInfo)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));

            AssertRunning();
            
            if (_newCallbacks.ContainsKey(name))
            {
                throw new CallbackRegistrationException($"Duplicate callback registration for '{name}'");
            }

            _newCallbacks[name] = NewCallback.For(target, methodInfo);
        }

        public void RegisterCallback(string name, object target, MethodInfo methodInfo, Type[] parameterTypes, uint?[] lengthIndices)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));

            AssertRunning();

            if (_newCallbacks.ContainsKey(name))
            {
                throw new CallbackRegistrationException($"Duplicate callback registration for '{name}'");
            }

            _newCallbacks[name] = NewCallback.For(target, methodInfo, parameterTypes, lengthIndices);
        }
        
        /// <inheritdoc />
        public void Print(string text)
        {
            if (IsOnMainThread)
                Interop.Print(text);
            else
                _synchronizationContext.Send(ctx => Interop.Print(text), null);
        }
        
        #endregion

        #region Implementation of IGameModeRunner
        
        /// <inheritdoc />
        public bool Run()
        {
            InternalStorage.RunningClient = this;
            Interop.Initialize();

            // Prepare the synchronization context
            _messageQueue = new NoWaitMessageQueue();
            _synchronizationContext = new SampSharpSynchronizationContext(_messageQueue);

            SynchronizationContext.SetSynchronizationContext(_synchronizationContext);
            
            _mainThread = Thread.CurrentThread.ManagedThreadId;
            _running = true;

            var version = Assembly.GetExecutingAssembly().GetName().Version!;
            CoreLog.Log(CoreLogLevel.Initialisation, "SampSharp GameMode Client");
            CoreLog.Log(CoreLogLevel.Initialisation, "-------------------------");
            CoreLog.Log(CoreLogLevel.Initialisation, $"v{version.ToString(3)} (C)2014-2022 Tim Potze");
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
