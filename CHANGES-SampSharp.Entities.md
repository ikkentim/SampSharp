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
