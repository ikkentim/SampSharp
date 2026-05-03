# Commands Project Rewrite Plan

## Executive Summary
Complete rewrite of `SampSharp.OpenMp.Entities.Commands` to support advanced command features while maintaining backward compatibility patterns. The rewrite will be modular, testable, and shared between player and console commands.

---

## Phase 1: Architecture & Core Infrastructure

### 1.1 Define Core Command Model
**Files to create:**
- `Core/CommandDefinition.cs` - Immutable command metadata (name, groups, aliases, return type info)
- `Core/CommandGroup.cs` - Represents hierarchical command groups (e.g., `["msg", "send"]`)
- `Core/CommandAlias.cs` - Simple alias wrapper
- `Core/CommandOverload.cs` - Single overload of a command (method + parameters)
- `Core/CommandRegistry.cs` - Central registry of all commands (replaceable, testable)

**Key Design:**
- Separate command *definition* from command *dispatch*
- Registry is injectable - enables testing without reflection
- Immutable metadata for thread-safety

### 1.2 Define Attribute Interfaces
**Files to create:**
- `Attributes/ICommandAttribute.cs` - Base for PlayerCommand/ConsoleCommand
- `Attributes/CommandGroupAttribute.cs` - Can stack on class/method
- `Attributes/AliasAttribute.cs` - One or more aliases
- `Attributes/ConsoleCommandAttribute.cs` - Console-specific variant
- `Attributes/RequiresPermissionAttribute.cs` - Permission requirement for player commands

**Implementation notes:**
- `[CommandGroup]` accepts `string[]` parameter, stackable via `[AttributeUsage(AllowMultiple = true)]`
- `[ConsoleCommand]` similar to `[PlayerCommand]` but no permission check
- `[RequiresPermission]` stores permission key (string)

### 1.3 Extensibility Services
**Files to create:**
- `Services/ICommandNameProvider.cs` - Customizable usage message generation
- `Services/IPermissionChecker.cs` - Custom permission validation for player commands  
- `Services/ICommandNotFoundHandler.cs` - Localization + custom "command not found" messages
- `Services/ICommandHelpProvider.cs` - Generate help text (uses registry enumeration)

**Design:**
- Interfaces with default implementations
- Registered in DI container
- User supplies custom implementations if needed

---

## Phase 2: Command Dispatch & Parsing (Composition-based)

### 2.1 Create CommandDispatcher
**Goal:** Core dispatch logic as a standalone, composable service (no inheritance needed).

**Files to create:**
- `Core/CommandDispatcher.cs` - Pure dispatch logic

**Methods:**
- `Dispatch(CommandRegistry registry, IServiceProvider services, object[] prefix, string inputText)` → `DispatchResult`
  - `ParseCommandLine(string inputText)` → `(string commandName, string remainingArgs)`
  - `LookupCommands(registry, commandName)` → `CommandOverload[]`
  - `MatchParameters(string args, CommandOverload)` → parsed args or null
  - `ExecuteCommand(...)` → async-aware execution with proper exception handling
- Async support:
  - Detect `Task`, `Task<T>` return types
  - Return type mapping: `Task<bool>` → treat as bool; `Task` → treat as void
  - Fire-and-forget Task execution (exception handling via context)

### 2.2 Enhance Parameter Parsing
**Files to modify:**
- `Parsers/ICommandParameterParser.cs` - Add metadata (e.g., for help text)

**Enhancements:**
- Keep existing parsers, add:
  - `BooleanParser`
  - `DecimalParser`  
  - Extensibility for custom types via service provider
- Add `TryParseAll(...)` method to handle overload matching more efficiently

### 2.3 Command Group Parsing
**Files to create:**
- `Parsing/CommandLineParser.cs` - Parse multi-word commands (e.g., `"message send hello"`)

**Algorithm:**
1. Try to match longest prefix in registered command groups
2. Fall back to single-word lookup
3. Return `(fullCommandName, remainingArgs)` tuple

---

## Phase 3: Player Command System

### 3.1 Implement PlayerCommandService
**Files to create/modify:**
- `Player/PlayerCommandService.cs` - New implementation using composition
- `PlayerCommandAttribute.cs` - Extend with alias, localization keys

**Architecture:**
- Composes `CommandRegistry` (build/cache metadata)
- Composes `CommandDispatcher` (core dispatch logic)
- Adds player-specific logic: permission checking, auto-format usage messages
- Maintains backward compatibility with existing return types
- Handles component resolution for player commands (first param can be any Component type)

**Critical Implementation Details:**
1. **Prefix handling**: Strip leading `/` from inputText before dispatch
   - Attribute: `[PlayerCommand("test")]` (no slash)
   - Incoming: `/test arguments` (with slash)
   - Strip `/` before passing to dispatcher
2. **Component Parameters**: First parameter can be any Component type (not just Player)
   - If first param is `Player`: resolve from prefix arguments
   - If first param is other Component type (e.g., `Vehicle`): 
     - Call `player.GetComponent<ComponentType>()` to get component
     - If component not found: command unavailable for this player
   - Use expression tree compilation (already in current system via `MethodInvokerFactory`)
3. **Permission Checking**: Before execution, call permission checker
   - If permission denied: return false (don't execute)

**Method Flow:**
```csharp
public bool Invoke(IServiceProvider services, EntityId player, string inputText)
{
    // Strip leading /
    if (!inputText.StartsWith('/')) return false;
    inputText = inputText[1..];
    
    var result = _dispatcher.Dispatch(_registry, services, [player], inputText);
    
    // Player-specific handling:
    // - Check permissions before execution (already in dispatcher)
    // - Format usage messages for in-game chat
    // - Send error messages to player on failure
    
    if (result.Response == InvokeResponse.InvalidArguments)
    {
        _entityManager.GetComponent<Player>(player)?.SendClientMessage(
            result.UsageMessage ?? "Invalid arguments.");
    }
    
    return result.Response == InvokeResponse.Success;
}
```

### 3.2 Player-Specific Logic
**Files to create:**
- `Player/PlayerCommandContext.cs` - Metadata about current player command invocation
- `Player/PlayerCommandPermissionChecker.cs` - Default permission implementation (injectable)

**Design:**
- Permission checker receives: `(player, permissionKey, context)` → bool
- Integrated into dispatcher as pre-execution check

---

## Phase 4: Console Command System

### 4.1 Create ConsoleCommandSender Type
**Files to create:**
- `Console/ConsoleCommandSender.cs` - New wrapper type for command context

**Definition:**
```csharp
public class ConsoleCommandSender
{
    public IPlayer? Player { get; }
    public Action<string>? MessageHandler { get; }
    
    // Constructor takes ConsoleCommandSenderData from open.mp API
    public ConsoleCommandSender(ConsoleCommandSenderData data)
    {
        Player = data.Player;
        MessageHandler = data.Handler?.Send;
    }
}
```

### 4.2 Implement ConsoleCommandService
**Files to create:**
- `Console/ConsoleCommandService.cs` - New implementation using composition
- `Console/ConsoleCommandAttribute.cs` - Equivalent to PlayerCommand (no permissions)
- `Console/ConsoleCommandContext.cs` - Metadata for console context

**Architecture:**
- Composes `CommandRegistry` (build/cache metadata)
- Composes `CommandDispatcher` (core dispatch logic)
- Adds console-specific logic: no permission checks, text-based response
- Same dispatch core as PlayerCommandService, different wrapper

**Critical Implementation Details:**
1. **ConsoleCommandSender as prefix parameter**:
   - If first parameter is `ConsoleCommandSender`: automatically provided (not parsed)
   - Passed as part of prefix to dispatcher: `[consoleCommandSender]`
   - Not included in parameter count for parsing
2. **No permission checking** (unlike player commands)
3. **Message delivery**:
   - Use `ConsoleCommandSender.MessageHandler?.Invoke(message)` to send responses
   - Can also work with `Player` if sender was a player

**Method Flow:**
```csharp
public string? Invoke(IServiceProvider services, ConsoleCommandSenderData senderData, string inputText)
{
    var sender = new ConsoleCommandSender(senderData);
    var result = _dispatcher.Dispatch(_registry, services, [sender], inputText);
    
    // Console-specific handling:
    // - No permission checks
    // - Return text response (success/error/usage)
    
    return result.Message ?? (result.Response == InvokeResponse.Success ? "OK" : "Command not found");
}
```

### 4.3 Integration
- Register ConsoleCommandService in DI container
- Hook into open.mp console command event (provide sender with ConsoleCommandSenderData)

---

## Phase 5: Help System & Enumeration API

### 5.1 Command Enumeration
**Files to create:**
- `Help/CommandEnumerator.cs` - Public API to iterate commands
- `Help/CommandGroupEnumerator.cs` - Iterate by group

**Public Methods:**
```csharp
// Get all commands
IEnumerable<CommandDefinition> GetAllCommands();

// Get command groups (hierarchical)
IEnumerable<CommandGroup> GetCommandGroups();
IEnumerable<CommandDefinition> GetCommandsInGroup(CommandGroup group);

// Search/filter
CommandDefinition? FindCommand(string name);
IEnumerable<CommandDefinition> SearchCommands(string query);
```

### 5.2 Usage & Help Generation
**Files to create:**
- `Help/CommandUsageFormatter.cs` - Format usage strings with custom messages

**Features:**
- Supports multiple overloads (show each variant)
- Respects custom `ICommandNameProvider` implementations
- Localizable via custom provider

---

## Phase 6: Advanced Features

### 6.1 Async Support
**Files to create:**
- `Async/AsyncCommandExecutor.cs` - Handle Task/Task<bool>/Task<T> returns

**Implementation:**
- Detect return type via reflection
- For `Task<bool>`: await and check result
- For `Task`: await and treat as success (void-like)
- For `Task<T>`: await and check truthiness of result
- Fire-and-forget: exceptions → unhandled exception handler

### 6.2 Localization Support
**Files to modify:**
- Create default `ICommandNameProvider` + `ICommandNotFoundHandler` implementations that support resource keys
- Optional: define `.resx` files for built-in messages

**Pattern:**
- Attribute property: `UsageMessageKey = "message_send_usage"`
- User's custom provider resolves key to localized text

---

## Phase 7: Unit Testing

### 7.1 Create New Test Project
**Project:** `SampSharp.OpenMp.Entities.Commands.Tests` (pure C#, no runtime dependency)

**Structure:**
```
SampSharp.OpenMp.Entities.Commands.Tests/
├── Unit/
│   ├── CommandRegistryTests.cs
│   ├── CommandLineParserTests.cs
│   ├── PlayerCommandServiceTests.cs
│   ├── ConsoleCommandServiceTests.cs
│   ├── ParameterParsingTests.cs
│   ├── CommandGroupTests.cs
│   ├── AliasTests.cs
│   ├── PermissionCheckingTests.cs
│   ├── AsyncCommandTests.cs
│   └── HelpEnumerationTests.cs
├── Mocks/
│   ├── MockServiceProvider.cs
│   ├── MockEntityManager.cs
│   ├── MockPermissionChecker.cs
│   ├── MockCommandNameProvider.cs
│   └── MockSystemRegistry.cs
└── Fixtures/
    ├── TestSystemFixtures.cs (sample ISystem implementations)
    └── TestCommandDefinitions.cs
```

**Coverage Goals:**
- ✓ Command registration from reflection
- ✓ Command group stacking
- ✓ Alias resolution
- ✓ Parameter parsing (all types)
- ✓ Overload matching
- ✓ Permission checking
- ✓ Async execution paths
- ✓ Error cases (invalid args, permission denied, command not found)
- ✓ Help enumeration

### 7.2 Test Organization
- **Unit tests** for pure logic (parsing, matching, enumeration)
- **Integration tests** (mocked DI) for end-to-end dispatch
- **Snapshot tests** for usage messages
- **Mock DI setup** for all external services

---

## Phase 8: Backward Compatibility & Migration

### 8.1 Compatibility Layer
**Files to create:**
- `Migration/PlayerCommandLegacyAdapter.cs` - Support old-style methods without new attributes

**Features:**
- Detect `[PlayerCommand]` without `[ConsoleCommand]` → generate sensible defaults
- Auto-wrap methods to work with new dispatch logic

### 8.2 Documentation
- Update README.md with new examples
- Migration guide for existing code
- Feature showcase (groups, aliases, async, help API)

---

## Phase 9: Integration & Cleanup

### 9.1 Update EcsBuilderCommandsExtensions
**Files to modify:**
- `EcsBuilderCommandsExtensions.cs`

**Changes:**
- Register both PlayerCommandService and ConsoleCommandService
- Default service registrations for extensibility interfaces
- Builder methods to customize (e.g., `AddCustomPermissionChecker()`)

### 9.2 Cleanup & Polish
- Remove/merge obsolete files
- Update InvokeResult/InvokeResponse if needed
- Deprecate any old patterns

---

## Implementation Order (Recommended)

1. **Phase 1** - Core model & architecture (no runtime deps needed, testable immediately)
2. **Phase 2** - Parsing refactor (parallel development possible)
3. **Phase 7** - Unit tests (can start alongside Phase 1, grows with each phase)
4. **Phase 3** - Player commands (build on Phase 1 & 2)
5. **Phase 4** - Console commands (parallel to Phase 3)
6. **Phase 5** - Help system (after Phase 3 & 4)
7. **Phase 6** - Advanced features (async, etc.)
8. **Phase 8** - Backward compat & migration
9. **Phase 9** - Integration & cleanup

---

## Key Design Decisions

### 1. Composition Over Inheritance
- **Registry** = metadata collection (reflection scanning, caching)
- **Dispatcher** = core dispatch logic (pure, composable, testable)
- **Services** = orchestration & context-specific handling (PlayerCommandService, ConsoleCommandService)
- **Parsers** = parameter extraction
- **Extensibility** = user-provided implementations (permission checker, name provider, etc.)

### 2. Async Design
- Detect async patterns at registration time
- Execute async methods with proper context
- Don't block on results (fire-and-forget for `Task`)
- Exceptions propagate to unhandled handler

### 3. Command Groups
- Represented as `string[]` (ordered)
- Stacked via attribute repetition
- Parsed during command dispatch (longest-match first)
- User-friendly: `message send` reads naturally

### 4. Testing Strategy
- Minimal mocking (mostly interfaces)
- No runtime/ECS dependencies for unit tests
- Integration tests with mocked DI
- Snapshot tests for generated usage strings

### 5. Extensibility
- All user-facing services (permission, localization, names) are interfaces
- Default implementations provided
- Registry can be replaced entirely
- Custom parameter parsers via service provider

---

## Estimated Complexity

| Phase | Difficulty | Time | Dependencies |
|-------|-----------|------|--------------|
| 1 | Low | 4-6 hrs | None |
| 2 | Medium | 8-10 hrs | Phase 1 |
| 3 | Medium | 6-8 hrs | Phase 1-2 |
| 4 | Medium | 4-6 hrs | Phase 1-2 |
| 5 | Low-Medium | 4-6 hrs | Phase 1-4 |
| 6 | Medium | 6-8 hrs | Phase 1-2, 5 |
| 7 | Medium-High | 12-16 hrs | Parallel with all |
| 8 | Low | 2-4 hrs | Phase 3-4 |
| 9 | Low | 2-3 hrs | All others |

**Total Estimate: ~48-70 hours of development**

---

## Success Criteria

- [x] All requirements met (see list above)
- [x] Player + Console commands working
- [x] Command groups, aliases, overloads functional
- [x] Permission system integrated
- [x] Async support implemented
- [x] Help enumeration API working
- [x] Unit tests > 85% coverage
- [x] Backward compatible with existing player commands
- [x] Documentation updated
- [x] No performance regression vs. current impl
