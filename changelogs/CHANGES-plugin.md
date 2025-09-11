### 0.10.0
- Added automatic coreclr and gamemode detection
- Added support for "fastnative" calls (#365)
- Added callback name to "Callback parameters count mismatch" error (#342)
- Changed hosted mode to be the default, multi-process mode can be enabled with the `use_multi_process_mode=1` server setting
- Fixed a possible server crash when a callback is handled with an unexpected parameter cound
- Fixed a possible server crash when calling the `CallRemoveFunction` native (#363)

### 0.9.0
- Updated sampgdk to 4.6.2
- Changed maximum native arguments to 64
- Fixed server in multi-process run mode freezing when calling native which calls a callback which calls a native
- Fixed intermission script getting stuck between runs
