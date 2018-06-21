using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SampSharp.Core.Callbacks;
using SampSharp.Core.Communication.Clients;
using SampSharp.Core.Logging;
using SampSharp.Core.Natives;
using SampSharp.Core.Threading;

namespace SampSharp.Core
{
    public sealed  class HostedGameModeClient : IGameModeClient, IGameModeRunner
    {
        private MessagePump _messagePump;
        private SampSharpSyncronizationContext _syncronizationContext;
        private readonly GameModeStartBehaviour _startBehaviour;
        private readonly IGameModeProvider _gameModeProvider;
        private int _mainThread;
        private bool _running;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MultiProcessGameModeClient" /> class.
        /// </summary>
        /// <param name="startBehaviour">The start method.</param>
        /// <param name="gameModeProvider">The game mode provider.</param>
        /// <param name="encoding">The encoding to use when en/decoding text messages sent to/from the server.</param>
        public HostedGameModeClient(GameModeStartBehaviour startBehaviour, IGameModeProvider gameModeProvider, Encoding encoding)
        {
            Encoding = encoding;
            _startBehaviour = startBehaviour;
            _gameModeProvider = gameModeProvider ?? throw new ArgumentNullException(nameof(gameModeProvider));
            NativeLoader = new NativeLoader(this);
        }
        
        /// <summary>
        ///     Gets a value indicating whether this property is invoked on the main thread.
        /// </summary>
        public bool IsOnMainThread => _mainThread == Thread.CurrentThread.ManagedThreadId;

        private void AssertRunning()
        {
            if (!_running)
                throw new GameModeNotRunningException();
        }

        private void OnUnhandledException(UnhandledExceptionEventArgs e)
        {
            UnhandledException?.Invoke(this, e);
        }

        #region Implementation of IGameModeClient

        /// <summary>
        ///     Gets the default encoding to use when translating server messages.
        /// </summary>
        public Encoding Encoding { get; }
        
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
        public void RegisterCallback(string name, object target, MethodInfo methodInfo, params CallbackParameterInfo[] parameters)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        ///     Prints the specified text to the server console.
        /// </summary>
        /// <param name="text">The text to print to the server console.</param>
        public void Print(string text)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets the handle of the native with the specified <paramref name="name" />.
        /// </summary>
        /// <param name="name">The name of the native.</param>
        /// <returns>The handle of the native with the specified <paramref name="name" />.</returns>
        public int GetNativeHandle(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Invokes a native using the specified <paramref name="data" /> buffer.
        /// </summary>
        /// <param name="data">The data buffer to be used.</param>
        /// <returns>The response from the native.</returns>
        public byte[] InvokeNative(IEnumerable<byte> data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Shuts down the server after the current callback has been processed.
        /// </summary>
        public void ShutDown()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IGameModeRunner

        /// <summary>
        ///     Runs the game mode of this runner.
        /// </summary>
        /// <returns>true if shut down by the game mode, false otherwise.</returns>
        /// <exception cref="Exception">Thrown if a game mode is already running.</exception>
        public bool Run()
        {
            InternalStorage.RunningClient = this;

            // Prepare the syncronization context
            _syncronizationContext = new SampSharpSyncronizationContext();
            _messagePump = _syncronizationContext.MessagePump;

            SynchronizationContext.SetSynchronizationContext(_syncronizationContext);

            CoreLog.Log(CoreLogLevel.Initialisation, "SampSharp GameMode Server");
            CoreLog.Log(CoreLogLevel.Initialisation, "-------------------------");
            CoreLog.Log(CoreLogLevel.Initialisation, $"v{CoreVersion.Version.ToString(3)}, (C)2014-2018 Tim Potze");
            CoreLog.Log(CoreLogLevel.Initialisation, "");

            _mainThread = Thread.CurrentThread.ManagedThreadId;
            _running = true;
            
            // TODO: Verify plugin version

            _gameModeProvider.Initialize(this);

            return true;
        }

        /// <summary>
        ///     Gets the client of this game mode runner.
        /// </summary>
        public IGameModeClient Client => this;

        #endregion
    }
}
