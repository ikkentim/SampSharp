# SampSharp Changelog

All notable changes to this project will be documented in this file.

## [0.11.1] - 2026-04-15

### SampSharp.Core
- Fixed an error which occurs when a callback has an empty string as argument (#450)

### SampSharp.GameMode
- (No changes)

### SampSharp.Entities
- (No changes)

### Plugin
- (No changes)

---

## [0.11.0] - 2026-03-20

### SampSharp.Core
- Added `TaskHelper.SwitchToMainThread` (#402)
- Improved performance of callbacks by 25%-30% (#399, #401)
- Minor improvement of natives performance
- Fixed encoding of strings in callback arguments ignoring game mode builder configuration
- Removed multi-process mode

### SampSharp.GameMode
- Added `VehicleModelInfo.VehicleModelInfo`
- Fixed missing Flatbed in `VehicleModelInfo` (#407)

### SampSharp.Entities
- Added `AddComponent` method that takes an instance as parameter (#427)
- Fixed issues related to open dialogs when a player disconnects (#434)
- Fixed an issue where player entity may stay alive when an exception is thrown in `OnPlayerDisconnect` event
- Fixed an issue where an error may occur when `OnDialogResponse` is called without an active dialog (#443)

### Plugin
- Added interop API (#399, #401)
- Added non-standard implementation of reading open.mp config.json
- Removed callback registration (#399, #401)
- Removed multi-process mode

---

## [0.10.1] - 2026-01-05

### SampSharp.Core
- Fixed a possible crash when calling natives which return a string

### SampSharp.GameMode
- (No changes)

### SampSharp.Entities
- (No changes)

### Plugin
- (No changes)

---

## [0.10.0] - 2025-12-20

### SampSharp.Core
- Added support for "fastnative" calls, improves native call performance by 95% (#365)
- Added support for varargs in "fastnative" calls (#260)
- Added `IGameModeClient.NativeObjectProxyFactory`
- Added option `NativeMethodAttribute.ReferenceIndices` which provides the option to invoke a native with input parameters which should be passed by reference
- Added embedded codepages into the library which are available through `GameModeBuilder.UseEncodingCodePage(string pageName)`
- Updated to .NET Standard 2.1
- Removed setter from `IGameModeClient.NativeLoader`
- Fixed HostedGameModeClient.ServerPath not returning correct path
- Deprecated multi-process mode and native-handle related types and methods

### SampSharp.GameMode
- Added `BasePlayer.CancelEdit` (#362)
- Added `BasePlayer.SetMapIcon` and `BasePlayer.RemoveMapIcon` (#364)
- Added `ListDialog<T>` (#383, #395)
- Updated to .NET Standard 2.1
- Updated documentation links from SA-MP wiki to open.mp docs
- Fixed `PlayerTextLabel.Text`, `PlayerTextLabel.AttachedPlayer` and `PlayerTextLabel.AttachedVehicle` not being set
- Fixed command group help commands not being invoked in some cases (#361)
- Fixed command overloading not working in some cases (#344)
- Fixed `BasePlayer.IsNPC` not returning `true` when the NPC has not yet connected (#346)
- Fixed typo in `Server.GetWeaponName` result for `Weapon.ThermalGoggles` (#380)
- Fixed a problem which caused successive calls to `ShowAsync` not to show the dialog (#384, #389)

### SampSharp.Entities
- Added support for player names as command arguments (#354)
- Added timers (#326)
- Updated to .NET Standard 2.1
- Updated documentation links from SA-MP wiki to open.mp docs
- Fixed `OnDestroyComponent` not being called when the component is destroyed, but the entities remains alive (#358)
- Fixed an error which could occur when event's occur with IDs of entities which don't exist, but aren't 0 (#355, #353, #352, #350)
- Fixed exception thrown when a dialog is shown with no `null` button2 text (#376)
- Fixed components not being removed from global component registry when destroying an entity

### Plugin
- Added automatic coreclr and gamemode detection
- Added support for "fastnative" calls (#365)
- Added callback name to "Callback parameters count mismatch" error (#342)
- Added coreclr detection based on `SAMPSHARP_RUNTIME` environment variable
- Changed hosted mode to be the default, multi-process mode can be enabled with the `use_multi_process_mode=1` server setting
- Fixed a possible server crash when a callback is handled with an unexpected parameter count
- Fixed a possible server crash when calling the `CallRemoveFunction` native (#363)
- Codepage files are no longer provided in the plugin release package. Codepages are available as embedded resources in SampSharp.Core via the `GameModeBuilder.UseEncodingCodePage` method. The codepage files are available in [the codepages repository](https://github.com/SampSharp/codepages) if you still need these files for some reason

---

## [0.9.3] - 2025-10-15

### SampSharp.Core
- No changes

### SampSharp.GameMode
- No changes

### SampSharp.Entities
- Fixed player- textdraws, labels and objects not working as intended

### Plugin
- (No changes)

---

## [0.9.2] - 2025-10-10

### SampSharp.Core
- No changes

### SampSharp.GameMode
- No changes

### SampSharp.Entities
- Fixed exception thrown when trying to destroy all components twice (#333)
- Fixed dialog response not being handled if the dialog was shown as a response to another dialog

### Plugin
- (No changes)

---

## [0.9.1] - 2025-10-05

### SampSharp.Core
- Fixed GMX startup behaviour not reconnecting to the SA-MP server properly

### SampSharp.GameMode
- Removed unused `PlayerDisconnectReason` enum in favour of `DisconnectReason` enum (#330)

### SampSharp.Entities
- Added code annotations
- Fixed server freezing when destroying an entity after destroying its components in a specific order
- Fixed command being called with component not in entity would cause the command not to be called but still succeed (#331)
- Removed unused `PlayerDisconnectReason` enum in favour of `DisconnectReason` enum (#330)

### Plugin
- (No changes)

---

## [0.9.0] - 2025-09-20

### SampSharp.Core
- Added `IGameModeClient.RegisterCallback` overloads which allows the arguments of the callback to be provided as an `object[]` to the specified method
- Added option to specify the index at which the identifier arguments are located within natives of native objects
- Added notice to multi-process run mode: "for development purposes only", use hosted mode for production environments
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

### SampSharp.GameMode
- Added `Vector3.XY` which returns a `Vector2` with the x and y components (#299/#300)
- Added `Color.Brightness` property (#309)
- Added `Color.AddGrammaCorrection`, `Color.RemoveGammaCorrection`, `Color.Grayscale` methods (#309)
- Added `blendAlpha` option to `Color.Lerp`, `Color.Darken` and `Color.Lighten` (#305/#310)
- Added `Timer.Run` and `Timer.RunOnce` overloads with an `int` or `double` interval in milliseconds (#315)
- Added `BasePlayer.PickUpPickup` event (#319)
- Added `BasePlayer.CameraTargetPlayerObject` (#320)
- Added `BasePlayer.SurfingPlayerObject` (#324)
- Added `Server.GetServerTickRate` (#295)
- Added automatic detection for command and command group names (#273)
- Added command parameter attribute `NullableParam` to indicate a `BasePlayer` or `BaseVehicle` argument is allowed to be null, enum values can be marked as nullable by using nullable enums like `VehicleModelType?` (#268/#290)
- Added `Server.GetWeaponName` (#311)
- Added `CommandAttribute.IsGroupHelp`, if this value is true, the command will run if the command group is entered by the player without a specified command in the command group
- **breaking** Added `PlayerCancelClickTextDraw` event which is called when player presses ESC while selecting textdraws, `PlayerClickTextDraw` is no longer called when ESC is pressed (#304/#321)
- Updated `VehicleModelInfo` to include seat count and a missing entry for ID 611, Utility Trailer (#302)
- Updated minimum .NET Standard version to 2.0
- **breaking** Changed `BasePlayer.CameraTargetObject` to `BasePlayer.CameraTargetGlobalObject` (#320)
- **breaking** Changed `BasePlayer.SurfingObject` to `BasePlayer.SurfingGlobalObject` (#324)
- **breaking** Changed `BaseMode.PlayerPickUp` event to have event arguments of type `PickUpPickupEventArgs` and changed the sender of event from the pickup to the player (#319)
- **breaking** Changed `Pickup.PickUp` event to have event arguments of type `PickUpPickupEventArgs` (#319)
- Fixed `Quaternion` coordinate system not matching SA-MP coordinate system
- Fixed `Edited` and `Selected` on `GlobalObject` and `PlayerObject` not being fired (#303/#306)
- Fixed `Color.FromInteger` with `ColorFormat.RGB` returning a value with 0 alpha
- Fixed conversion from `Color` to `Vector3` not returning decimal values
- Fixed enum numeric values not being recognized as a command argument if an enum value name contains the numeric value (#274)
- **breaking** Removed `ObjectModel` enum because it was too big and might not be complete

### SampSharp.Entities
- Initial version

### Plugin
- Updated sampgdk to 4.6.2
- Changed maximum native arguments to 64
- Fixed server in multi-process run mode freezing when calling native which calls a callback which calls a native
- Fixed intermission script getting stuck between runs

---

## Older Versions

Detailed history for 0.8.x and earlier versions can be found in the individual changelog files in the `changelogs/` directory.
