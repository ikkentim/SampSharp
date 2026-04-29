// SampSharp
// Copyright 2022 Tim Potze
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
using System.Reflection;
using System.Text;
using System.Threading;
using SampSharp.Core.Callbacks;
using SampSharp.Core.Hosting;
using SampSharp.Core.Logging;
using SampSharp.Core.Natives;
using SampSharp.Core.Natives.NativeObjects;

namespace SampSharp.Core;

internal sealed class HostedGameModeClient : IGameModeClient, IGameModeRunner, ISynchronizationProvider
{
    private readonly Dictionary<string, Callback> _newCallbacks = new();
    private SampSharpSynchronizationContext? _synchronizationContext;
    private readonly IGameModeProvider _gameModeProvider;
    private int _mainThread;
    private int _rconThread = int.MinValue;
    private bool _running;

    public HostedGameModeClient(IGameModeProvider gameModeProvider, Encoding encoding)
    {
        Encoding = encoding;
        _gameModeProvider = gameModeProvider ?? throw new ArgumentNullException(nameof(gameModeProvider));
        NativeObjectProxyFactory = new NativeObjectProxyFactoryImpl(this);

        ServerPath = Directory.GetCurrentDirectory();
    }

    public bool IsOnMainThread
    {
        get
        {
            var id = Environment.CurrentManagedThreadId;
            return _mainThread == id || _rconThread == id;
        }
    }

    public Encoding Encoding { get; }

    public INativeObjectProxyFactory NativeObjectProxyFactory { get; }

    public ISynchronizationProvider SynchronizationProvider => this;

    public string ServerPath { get; }

    public event EventHandler<UnhandledExceptionEventArgs>? UnhandledException;

    public void RegisterCallback(string name, object? target, MethodInfo methodInfo)
    {
        RegisterCallback(name, target, methodInfo, null);
    }

    public void RegisterCallback(string name, object? target, MethodInfo methodInfo, Type[]? parameterTypes, uint?[]? lengthIndices = null)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));

        if (!_running)
            throw new GameModeNotRunningException();

        if (_newCallbacks.ContainsKey(name))
        {
            throw new CallbackRegistrationException($"Duplicate callback registration for '{name}'");
        }

        try
        {
            _newCallbacks[name] = Callback.For(target, methodInfo, parameterTypes, lengthIndices);
        }
        catch (InvalidOperationException e)
        {
            throw new CallbackRegistrationException($"Failed to register callback '{name}'. {e.Message}", e);
        }
    }

    public void Print(string text)
    {
        if (!_running)
            throw new GameModeNotRunningException();

        if (IsOnMainThread)
            Interop.Print(text);
        else
            _synchronizationContext!.Send(_ => Interop.Print(text), null);
    }

    public bool Run()
    {
        if (_running)
        {
            return false;
        }

        InternalStorage.SetRunningClient(this);

        try
        {
            Interop.Initialize();
        }
        catch (InvalidOperationException e)
        {
            // May be thrown when the plugin is to old for this version of SampSharp.Core.
            Console.WriteLine(e.Message);
            throw;
        }

        // Prepare the synchronization context
        _synchronizationContext = new SampSharpSynchronizationContext();
        SynchronizationContext.SetSynchronizationContext(_synchronizationContext);

        _mainThread = Environment.CurrentManagedThreadId;
        _running = true;

        var version = Assembly.GetExecutingAssembly()
            .GetName()
            .Version!;
        CoreLog.Log(CoreLogLevel.Initialisation, "SampSharp GameMode Client");
        CoreLog.Log(CoreLogLevel.Initialisation, "-------------------------");
        CoreLog.Log(CoreLogLevel.Initialisation, $"v{version.ToString(3)} (C)2014-2022 Tim Potze");
        CoreLog.Log(CoreLogLevel.Initialisation, "");

        _gameModeProvider.Initialize(this);

        return true;
    }

    IGameModeClient IGameModeRunner.Client => this;

    bool ISynchronizationProvider.InvokeRequired => !IsOnMainThread;

    void ISynchronizationProvider.Invoke(Action action)
    {
        if (!_running)
        {
            throw new GameModeNotRunningException();
        }

        _synchronizationContext!.Send(_ => action(), null);
    }

    private void OnUnhandledException(UnhandledExceptionEventArgs e)
    {
        UnhandledException?.Invoke(this, e);
    }

    internal void Tick()
    {
        if (!_running)
        {
            return;
        }

        while (true)
        {
            var message = _synchronizationContext!.GetMessage();

            if (message == null)
            {
                break;
            }

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
        if (!_running)
        {
            return;
        }

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

    internal void InitializeForTesting()
    {
        _running = true;
    }
}