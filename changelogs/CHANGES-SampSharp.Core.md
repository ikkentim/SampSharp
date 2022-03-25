### 0.10.0-alpha2
- Fixed HostedGameModeClient.ServerPath not returning correct path

### 0.10.0-alpha1
- Added support for "fastnative" calls, this improves time consumed by calls to native functions by 95% (#365)
- Added support for varargs in "fastnative" calls (#260)
- Added `IGameModeClient.NativeObjectProxyFactory`
- Added option `NativeMethodAttribute.ReferenceIndices` which provides the option to invoke a native with input parameters which should be passed by reference.
- Added embedded codepages into the library which are available through `GameModeBuilder.UseEncodingCodePage(string pageName)`
- Updated to .NET Standard 2.1
- Removed setter from `IGameModeClient.NativeLoader`
- Deprecated multi-process mode and native-handle related types and methods

### 0.9.3
- No changes

### 0.9.2
- No changes

### 0.9.1
- Fixed GMX startup behaviour not reconnecting to the SA-MP server properly

### 0.9.0
- Added `IGameModeClient.RegisterCallback` overloads which allows the arguments of the callback to be provided as an `object[]` to the specified method
- Added option to specify the index at which the identifier arguments are located within natives of native objects
- Added notice to multi-process run mode: "for development purposes only", use hosted hosted mode for production environments
- Added callback name to the `IGameModeClient.UnhandledException` event
- Added unhandled exception handling in hosted mode
- Added unhandled exception handling in multi-process mode for ticks and synchronisations
- Added `IfHosted` and `IfMultiProcess` methods to `GameModeBuilder`
- Updated minimum .NET Standard version to 2.0
- Improved shutdown behaviour of multi-process run mode
- Improved number of memory allocations during handling of callbacks and native calls in hosted run mode
- Changed hosted mode to not automatically redirect console output to the server_log.txt, use `GameModeBuilder.RedirectConsoleOutput()` to reenable logging to the server log
- Removed `GameModeBuilder.BuildWith`
- Fixed `GameModeBuilder.RedirectConsoleOutput` causing errors during startup in multi-process mode
- Fixed garbage strings being returned when no string is set to out string parameters (#323)

### 0.8.0
- Initial version

### 0.8.0-alpha10
- Added `IGameModeClient.ServerPath` (#292)
- Improved state resetting with intermission script
- Increased native argument buffer size from 32 to 128 (#279)
- Fixed missing framework log messages
- Fixed "Duplicate typename within an assembly" exception being thrown in some cases when loading a gamemode when players are already connect (#258)
- Fixed crash which occurs when using running gmx in non-hosted mode (#280)

### 0.8.0-alpha8
- Added option to host game mode inside samp-server process (experimental)
- Renamed `GameModeClient` to `MultiProcessGameModeClient`
- Changed `GameModeBuilder.UseLogStream(Stream stream)` to `GameModeBuilder.UseLogWriter(TextWriter textWriter)`
- Fixed an `ArgumentOutOfRangeException` being thrown in certain cases when calling a native
- Fixed exceptions from tasks not being sent to the `IGameModeClient.UnhandledException` event
- Fixed log messages from SampSharp not being redirect to the SA-MP server when `RedirectConsoleOutput()` was used

### 0.8.0-alpha7
- Possibly fixed "Duplicate type name within an assembly." error

### 0.8.0-alpha6
- Fixed random server freezes while waiting for a response from the server
- Fixed callbacks called during the `OnGameModeInit` callback not being called

### 0.8.0-alpha5
- Added a separate buffer for native results so the native arguments don't get overwritten by the native when it is writing the output (plugin)
- Changed natives to provide all results in array references instead of just the written values. The unwritten values are 0 by default
- Fixed deadlocks when calling natives which call callbacks which call natives
- Fixed a crash caused by a recursive non-recursive-mutex locking issue (plugin) 

### 0.8.0-alpha4
- Fixed array arguments in natives not working

### 0.8.0-alpha3
- Fixed a threading issue (#220)
- Fixed a communication buffer overflow when pausing the server using a debugger. Set `com_debug 1` in server.cfg to avoid these overflows.

### 0.8.0-alpha2
- Fixed FakeGMX not reconnecting players (#221)

### 0.8.0-alpha1
- Initial version
