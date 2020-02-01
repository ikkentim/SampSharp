### 0.9.1
- Added code annotations
- Fixed server freezing when destroying an entity after destroying its components in a specific order
- Fixed command being called with component not in entity would cause the command not te be called but still succeed (#331)
- Removed unused `PlayerDisconnectReason` enum in favour of `DisconnectReason` enum (#330)

### 0.9.0
- Initial version
