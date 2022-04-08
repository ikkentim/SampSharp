###
- Added interop API (#399, #401)
- Removed callback registration (#399, #401)

### 0.10.0
- Added automatic coreclr and gamemode detection
- Added support for "fastnative" calls (#365)
- Added callback name to "Callback parameters count mismatch" error (#342)
- Added coreclr detection based on `SAMPSHARP_RUNTIME` environment variable
- Changed hosted mode to be the default, multi-process mode can be enabled with the `use_multi_process_mode=1` server setting
- Fixed a possible server crash when a callback is handled with an unexpected parameter count
- Fixed a possible server crash when calling the `CallRemoveFunction` native (#363)
- Codepage files are no longer provided in the plugin release package. Codepages are available as embedded resources in SampSharp.Core via the `GameModeBuilder.UseEncodingCodePage` method. The codepage files are available in [the codepages repository](https://github.com/SampSharp/codepages) if you still need these files for some reason.

### 0.10.0-alpha2
- Added coreclr detection based on `SAMPSHARP_RUNTIME` environment variable

### 0.10.0-alpha1
- Added automatic coreclr and gamemode detection
- Added support for "fastnative" calls (#365)
- Added callback name to "Callback parameters count mismatch" error (#342)
- Changed hosted mode to be the default, multi-process mode can be enabled with the `use_multi_process_mode=1` server setting
- Fixed a possible server crash when a callback is handled with an unexpected parameter count
- Fixed a possible server crash when calling the `CallRemoveFunction` native (#363)
- Codepage files are no longer provided in the plugin release package. Codepages are available as embedded resources in SampSharp.Core via the `GameModeBuilder.UseEncodingCodePage` method. The codepage files are available in [the codepages repository](https://github.com/SampSharp/codepages) if you still need these files for some reason.

### 0.9.0
- Updated sampgdk to 4.6.2
- Changed maximum native arguments to 64
- Fixed server in multi-process run mode freezing when calling native which calls a callback which calls a native
- Fixed intermission script getting stuck between runs
