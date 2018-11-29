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
