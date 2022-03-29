### 0.10.0
- Added support for  player names as command arguments (#354)
- Added timers (#326)
- Updated to .NET Standard 2.1
- Updated documentation links from SA-MP wiki to open.mp docs
- Fixed `OnDestroyComponent` not being called when the component is destroyed, but the entities remains alive (#358)
- Fixed an error which could occur when event's occur with IDs of entities which don't exist, but aren't 0 (#355, #353, #352, #350)
- Fixed exception thrown when a dialog is shown with no `null` button2 text (#376)
- Fixed components not being removed from global component registry when destroying an entity

### 0.10.0-alpha2
- Fixed components not being removed from global component registry when destroying an entity

### 0.10.0-alpha1
- Added support for  player names as command arguments (#354)
- Added timers (#326)
- Updated to .NET Standard 2.1
- Updated documentation links from SA-MP wiki to open.mp docs
- Fixed `OnDestroyComponent` not being called when the component is destroyed, but the entities remains alive (#358)
- Fixed an error which could occur when event's occur with IDs of entities which don't exist, but aren't 0 (#355, #353, #352, #350)
- Fixed exception thrown when a dialog is shown with no `null` button2 text (#376)

### 0.9.3
- Fixed player- textdraws, labels and objects not working as intended

### 0.9.2
- Fixed exception thrown when trying to destroy all components twice (#333)
- Fixed dialog response not being handled if the dialog was shown as a response to another dialog

### 0.9.1
- Added code annotations
- Fixed server freezing when destroying an entity after destroying its components in a specific order
- Fixed command being called with component not in entity would cause the command not te be called but still succeed (#331)
- Removed unused `PlayerDisconnectReason` enum in favour of `DisconnectReason` enum (#330)

### 0.9.0
- Initial version
