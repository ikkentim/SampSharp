### 0.7.6107.37493-alpha
- Fixed an error when generating a proxy object for virtual methods for
- Renamed `BasePlayer.GetPlayerLastShotVectors` to `BasePlayer.GetLastShot`

### 0.7.6107.36375-alpha
- Added abstraction layer between interop and implementation

### 0.7.6104.38929-alpha
- Added support for using any codepage
- Added server configuration reader
- Added support for boolean reference native argument type
- Added `BaseVehicleFactory`
- Added system for automatically loading extensions
- Added support for SA-MP 0.3.7R2
- Added a way of automatically loading controllers
- Added `BasePlayer.IsSelectingTextDraw`
- Added `BasePlayer.GetPlayerLastShotVectors`
- Added missing documentation
- Added mechanism for extension dependencies
- Rewritten the command processor
- Rewritten the way native calls are handled
- Improved performance of of all pooling types
- Renamed `GtaPlayer` to `BasePlayer`
- Renamed `GtaVehicle` to `BaseVehicle`
- Renamed `GtaPlayerController` to `BasePlayerController`
- Renamed `GtaVehicleController` to `BaseVehicleController`
- Fixed the the diploma font not being available
- Removed setters from Vector2/Vector3

### 0.6.2
- Added various `Color` constructors and operators (including `Color * float`).
- Added `Color.Lerp()`, `Color.Lighten()` and `Color.Darken()`.
- Added `MathHelper` class.
- Added `BaseMode.CallbackException` event. This event is raised if an exception has been thrown in a callback without being handled. 
- Changed sender of menu related events to player.
- Fixed a bug where `Sync.Run()` causes a `NullReferenceException` if called before controllers are loaded.
- Fixed a bug where `LogWriter` (used by Console.Write) causes buffer overflow errors when handling long strings.
- Fixed a bug causing .mdb files not to be loaded properly.

### 0.6.1
- Fixed a bug in return value conversion of callbacks.
- Fixed a bug where using the build-in MapAndreas might crash the server.

### 0.6
- Added `Dialog.ShowAsync`.
- Added `InputDialog`, `ListDialog`, `MessageDialog` and `TabListDialog` classes which contain useful methods for it's type of dialog.
- Added `Vector2` structure.
- Updated vehicle parameter setters to use the `VehicleParameterValue` to maintain the proper states of other vehicle parameters.
- Updated mono API used to 4.0.0. **The minimum mono version required is 4.0 now**.
- **breaking** The `Vector` structure has been renamed to `Vector3`.
- Removed `GetHashCode` from wrapper classes.
- Removed `double` constructors from `Vector3` sructure.
- Fixed auto loading of empty filterscript (which allows `OnRconCommand` to be used)
- Fixed a small memory leak in the initialization script in the plugin.
- Fixed a bug where Callnative array throws errors when a `out float[]` argument is used.
- Fixed `TextEventArgs.SendToPlayers` (#118).
- (incomplete) Started creating interfaces of wrapper classes.

### 0.5
- Added `GtaPlayer.DefaultClientMessageColor`
- Added the ability to add commands in a class derived from `GtaPlayer` class without the method having to be static.
- Added a basic `IServiceProvider` to `BaseMode` class.
- Updated for SA-MP 0.3.7:
  - Added `Actor` class.
  - Added constructors to `Dialog` class for tab lists.
  - Added siren related arguments to `GtaVehicle.Create` and `GtaVehicle.CreateStatic`.
  - Added door/window parameter properties and methods to `GtaVehicle` class.
  - Added various other new methods and properties.
- Renamed `GtaVehicle.GetParams` to `GtaVehicle.GetParameters`.
- Renamed `GtaVehicle.SetParams` to `GtaVehicle.SetParameters`.
- Added overload of `GtaVehicle.GetParameters` which properly takes care of the 'unset' (-1) values the parameters may contain.
- Removed deprecated methods.
- Removed unused and out-of-date documentation from the `Native` class.

### 0.4.1
- Fixed extensions failing to load before the initialization ended. (Resolves #90)

### 0.4
- Added Native.NativeExists
- Renamed `Color.GetColorFromValue` and `Color.GetColorValue` and add `Color.FromString`. (Resolves #87)
- Renamed Disposable.CheckDisposed to Disposable.AssertNotDisposed
- Improved documentation.
- Improved console messages.
- Improved command system. (Resolves #86 #89 #77)
- Improved build-in MapAndreas. If a suitable MapAndreas plugin is loaded, the plugin is used instead of the build-in logic.
- Fixed PlayerObject and PlayerTextDraw staying in pools after player disconnects (Resolves #82)
- Fixed various memory leaks.
- Fixed various crashes related to calls to custom natives.
- Fixed crashes related to `gmx`/`Native.GameModeExit()`.

### 0.3.1
- Added missing documentation
- Added Bone enumerator
- Add setters to vehicle params Engine, Lights, Alarm, Doors, Bonnet, Boot, Objective
- Updated some methods that used IDs to use objects/enumerators instead
- Updated most events
  - Makes most EventArgs classes cleaner
  - Removes repeated .Find/.FindOrCreate  calls
- Removed logic from Native class. It now only contains external calls.

### 0.3
- Added priorities to keyhandlers
- Added Server.ToggleDebugOutput(toggle) to output Debug.* calls to the console
- Added a number of other functions to the Server class
- Fixed a couple of bugs with timers
- Changed visibility and and names of event raisers in BaseMode class
- Changed various *EventArgs property names and logic
- Improved Sync class and allow make Sync.Run calls awaitable
- Console.* and Debug.* now automatically sync to the main thread when this is required
- Updated sampgdk which resolves some compatibility issues
- Various other improvements and fixes

### 0.2
- Added GtaVehicle.Health property
- Added support for different languages
- Added a per-key press/release handler: GtaPlayer.Key

### 0.1
- Initial version