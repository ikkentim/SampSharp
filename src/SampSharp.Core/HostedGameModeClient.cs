using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SampSharp.Core.Callbacks;
using SampSharp.Core.Communication;
using SampSharp.Core.Communication.Clients;
using SampSharp.Core.Hosting;
using SampSharp.Core.Logging;
using SampSharp.Core.Natives;
using SampSharp.Core.Threading;

namespace SampSharp.Core
{
    /// <summary>
    ///     Represents a SampSharp game mode client for hosted game modes.
    /// </summary>
    public sealed class HostedGameModeClient : IGameModeClient, IGameModeRunner
    {
        private readonly Dictionary<string, Callback> _callbacks = new Dictionary<string, Callback>();
        private NoWaitMessageQueue _messageQueue;
        private SampSharpSynchronizationContext _synchronizationContext;
        private readonly GameModeStartBehaviour _startBehaviour;
        private readonly IGameModeProvider _gameModeProvider;
        private int _mainThread;
        private int _rconThread = int.MinValue;
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
                    // TODO: Not sure if this will cause issues.
                    _rconThread = Thread.CurrentThread.ManagedThreadId;
                }

                if (_callbacks.TryGetValue(name, out var callback))
                {
                    // TODO: Optimize
                    var arr = new byte[length];
                    Marshal.Copy(data, arr, 0, length);

                    return callback.Invoke(arr, 0) ?? 1;
                }
            }
            catch (Exception e)
            {
                OnUnhandledException(new UnhandledExceptionEventArgs(name, e));
            }

            return 1;
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
        ///     Gets the path to the server directory.
        /// </summary>
        public string ServerPath { get; private set; }

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
            
            // TODO: Only call on main thread
            var ptr = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, ptr, data.Length);

            if (IsOnMainThread)
                Interop.RegisterCallback(ptr);
            else
                _synchronizationContext.Send(ctx => Interop.RegisterCallback(ptr), null);

            Marshal.FreeHGlobal(ptr);
        }
        
        /// <summary>
        ///     Registers a callback with the specified <paramref name="name" />. When the callback is called, the specified
        ///     <paramref name="methodInfo" /> will be invoked on the specified <paramref name="target" />.
        /// </summary>
        /// <param name="name">The name af the callback to register.</param>
        /// <param name="target">The target on which to invoke the method.</param>
        /// <param name="methodInfo">The method information of the method to invoke when the callback is called.</param>
        /// <param name="parameters">The parameters of the callback.</param>
        /// <param name="parameterTypes">The types of the parameters.</param>
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
            
            // TODO: Only call on main thread
            var ptr = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, ptr, data.Length);

            if (IsOnMainThread)
                Interop.RegisterCallback(ptr);
            else
                _synchronizationContext.Send(ctx => Interop.RegisterCallback(ptr), null);

            Marshal.FreeHGlobal(ptr);
        }
        
        /// <summary>
        ///     Prints the specified text to the server console.
        /// </summary>
        /// <param name="text">The text to print to the server console.</param>
        public void Print(string text)
        {
            if (IsOnMainThread)
                Interop.Print(text);
            else
                _synchronizationContext.Send(ctx => Interop.Print(text), null);
        }

        /// <summary>
        ///     Gets the handle of the native with the specified <paramref name="name" />.
        /// </summary>
        /// <param name="name">The name of the native.</param>
        /// <returns>The handle of the native with the specified <paramref name="name" />.</returns>
        public int GetNativeHandle(string name)
        {
            if (IsOnMainThread)
                return Interop.GetNativeHandle(name);

            var result = 0;
            _synchronizationContext.Send(ctx => result = Interop.GetNativeHandle(name), null);
            return result;
        }

        /// <summary>
        ///     Invokes a native using the specified <paramref name="data" /> buffer.
        /// </summary>
        /// <param name="data">The data buffer to be used.</param>
        /// <returns>The response from the native.</returns>
        public byte[] InvokeNative(IEnumerable<byte> data)
        {
            // TODO: Only call on main thread
            // TODO: Optimize
            var adata = data.ToArray();
            var inbuf = Marshal.AllocHGlobal(adata.Length);
            Marshal.Copy(adata, 0, inbuf, adata.Length);

            var outbuf = Marshal.AllocHGlobal(1024);// TODO proper allocation/ global buf
            int outlen = 1024;
            
            if (IsOnMainThread)
                Interop.InvokeNative(inbuf, adata.Length, outbuf, ref outlen);
            else
                _synchronizationContext.Send(ctx => Interop.InvokeNative(inbuf, adata.Length, outbuf, ref outlen), null);

            var outarr = new byte[outlen];
            Marshal.Copy(outbuf, outarr, 0, outlen);

            Marshal.FreeHGlobal(inbuf);
            Marshal.FreeHGlobal(outbuf);
            return outarr;
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

        /// <summary>
        ///     Gets the client of this game mode runner.
        /// </summary>
        public IGameModeClient Client => this;

        #endregion
    }
}
