using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using SampSharp.Core.Callbacks;
using SampSharp.Core.Hosting;
using SampSharp.Core.Logging;
using SampSharp.Core.Natives;
using SampSharp.Core.Natives.NativeObjects;
using SampSharp.Core.Threading;

namespace SampSharp.Core
{
    /// <summary>
    ///     Represents a SampSharp game mode client for hosted game modes.
    /// </summary>
    internal sealed class HostedGameModeClient : IGameModeClient, IGameModeRunner, ISynchronizationProvider
    {
        private readonly Dictionary<string, NewCallback> _callbacks = new();
        private SampSharpSynchronizationContext _synchronizationContext;
        private readonly IGameModeProvider _gameModeProvider;
        private int _mainThread;
        private int _rconThread = int.MinValue;
        private bool _running;
        
        public HostedGameModeClient(IGameModeProvider gameModeProvider, Encoding encoding)
        {
            Encoding = encoding;
            _gameModeProvider = gameModeProvider ?? throw new ArgumentNullException(nameof(gameModeProvider));
            NativeLoader = new NativeLoader(this);

            ServerPath = Directory.GetCurrentDirectory();
        }
        
        private bool IsOnMainThread
        {
            get
            {
                var id = Environment.CurrentManagedThreadId;
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

        private void HandleMessage(SendOrPostCallbackItem message)
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

        internal void Tick()
        {
            // Pump new tasks
            _synchronizationContext.HandleQueue(HandleMessage);

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

                if (_callbacks.TryGetValue(name, out var callback))
                {
                    callback.Invoke(amx, parameters, retval);
                }
            }
            catch (Exception e)
            {
                OnUnhandledException(new UnhandledExceptionEventArgs(name, e));
            }
        }
        
        public Encoding Encoding { get; }
        
        public INativeLoader NativeLoader { get; }
        
        public INativeObjectProxyFactory NativeObjectProxyFactory => NativeLoader.ProxyFactory;
        
        public ISynchronizationProvider SynchronizationProvider => this;
        
        public string ServerPath { get; }
        
        public event EventHandler<UnhandledExceptionEventArgs> UnhandledException;
        
        public void RegisterCallback(string name, object target, MethodInfo methodInfo)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));

            AssertRunning();
            
            if (_callbacks.ContainsKey(name))
            {
                throw new CallbackRegistrationException($"Duplicate callback registration for '{name}'");
            }

            _callbacks[name] = NewCallback.For(target, methodInfo);
        }

        public void RegisterCallback(string name, object target, MethodInfo methodInfo, Type[] parameterTypes, uint?[] lengthIndices)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));

            AssertRunning();

            if (_callbacks.ContainsKey(name))
            {
                throw new CallbackRegistrationException($"Duplicate callback registration for '{name}'");
            }

            _callbacks[name] = NewCallback.For(target, methodInfo, parameterTypes, lengthIndices);
        }

        public void Print(string text)
        {
            if (IsOnMainThread)
                Interop.Print(text);
            else
                _synchronizationContext.Send(_ => Interop.Print(text), null);
        }
       
        public bool Run()
        {
            InternalStorage.RunningClient = this;

            // Prepare the synchronization context
            _synchronizationContext = new SampSharpSynchronizationContext();

            SynchronizationContext.SetSynchronizationContext(_synchronizationContext);
            
            _mainThread = Environment.CurrentManagedThreadId;
            _running = true;

            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            var version = assemblyName.Version!;

            Span<char> line = stackalloc char[assemblyName.Name!.Length];
            line.Fill('-');

            CoreLog.Log(CoreLogLevel.Initialisation, assemblyName.Name);
            CoreLog.Log(CoreLogLevel.Initialisation, line.ToString());
            CoreLog.Log(CoreLogLevel.Initialisation, $"v{version.ToString(3)}, (C)2014-2022 Tim Potze");
            CoreLog.Log(CoreLogLevel.Initialisation, "");

            var pluginVersion = Interop.GetPluginVersion();

            if (pluginVersion < version)
            {
                CoreLog.Log(CoreLogLevel.Warning, "Plugin version is older than SampSharp.Core! If you encounter problems, please update your plugin.");
            }

            _gameModeProvider.Initialize(this);

            return true;
        }
        
        public IGameModeClient Client => this;
        
        bool ISynchronizationProvider.InvokeRequired => !IsOnMainThread;
        
        void ISynchronizationProvider.Invoke(Action action)
        {
            _synchronizationContext.Send(ctx => action(), null);
        }
    }
}
