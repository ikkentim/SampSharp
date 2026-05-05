# Commands Project Rewrite Plan

## Executive Summary

Complete rewrite of `SampSharp.OpenMp.Entities.Commands` to support two distinct command systems (player and console) with advanced features including command groups, aliases, permissions, and help enumeration. The architecture is modular, testable, and composition-based rather than inheritance-based.

**Key Goals:**
- Support both **player commands** (with `/` prefix and permission checks) and **console commands** (without permission checks)
- Enable command grouping and aliasing (e.g., `/message send`, `/msg`, `/m`)
- Support method overloading with intelligent overload resolution
- Provide extensible services for permissions, help, and localization
- Maintain testability with injectable dependencies and no ECS coupling in core logic
- Support async methods with proper exception handling

---

## Phase 1: Architecture & Core Infrastructure

### 1.1 Define Core Command Model

**Files to create:**
- `Core/CommandDefinition.cs` - Immutable command metadata (name, groups, aliases, return type info, tags)
- `Core/CommandGroup.cs` - Represents hierarchical command groups as `string[]` (e.g., `["msg", "send"]`)
- `Core/CommandAlias.cs` - Simple alias wrapper (alternative name, no groups)
- `Core/CommandOverload.cs` - Single method overload of a command with parsed parameter info
- `Core/CommandRegistry.cs` - Central registry of all commands (one per command system: player/console)
- `Core/CommandTag.cs` - Key-value metadata attached to commands (used for permissions, custom data, etc.)

**Key Design:**
- Separate command *definition* (metadata, parsed from reflection) from command *dispatch* (execution logic)
- Registry is injectable and testable without reflection
- Immutable metadata for thread-safety
- **NOTE: Permission tags** are implemented via `CommandTag` with key `"permission"` (not separate attribute)

### 1.2 Define Attributes

**Files to create:**
- `Attributes/PlayerCommandAttribute.cs` - Marks method as player command; optional name parameter
- `Attributes/ConsoleCommandAttribute.cs` - Marks method as console command; optional name parameter
- `Attributes/CommandGroupAttribute.cs` - Stackable on class or method; adds hierarchical grouping
- `Attributes/CommandAliasAttribute.cs` - Creates additional registration without groups (e.g., shorthand `/m` for `/msg send`)
- `Attributes/CommandTagAttribute.cs` - Stores key-value metadata (e.g., `[CommandTag("permission", "admin")]`)

**Implementation notes:**
- Both `[PlayerCommand]` and `[ConsoleCommand]` accept optional `string name` parameter
  - If omitted, use **lowercase method name**, stripping `"Command"` suffix if present (e.g., `MeCommand()` → `"me"`)
- `[CommandGroup]` accepts `string[] groups` and can be applied multiple times (stackable)
  - Order matters: applied top-to-bottom, innermost takes precedence
  - Result is a hierarchical path (e.g., class `[CommandGroup("a", "b")]` + method `[CommandGroup("c")]` → `["a", "b", "c"]`)
- `[CommandAlias("name")]` can be applied multiple times for multiple aliases
  - Aliases bypass group hierarchy (always directly registered)
  - Example: `[CommandGroup("a"), CommandAlias("e")]` method → commands `/a command`, `/a alias_name`, and `/e`
- `[CommandTag("key", "value")]` can be applied multiple times for custom metadata bag
  - Built-in tag: `[CommandTag("permission", "key")]` (replacing old `[RequiresPermission]`)

### 1.3 Service Interfaces (Extensibility)

**Files to create:**
- `Services/ICommandParameterParser.cs` - Parse a string to a specific type (updated with metadata)
- `Services/ICommandParameterParserFactory.cs` - Factory to get parser for a given type
- `Services/IPermissionChecker.cs` - Check if player has permission for a command (player-commands only)
  - Signature: `(Player player, CommandDefinition command) → bool`
  - Optional in DI; if not provided, default to "always allowed"
- `Services/ICommandNotFoundHandler.cs` - Called when command not found; can provide custom message
  - Signature: `(string inputText, CommandDefinition[]? suggestions) → string?`
  - Optional in DI; default implementation returns "Command not found"
- `Services/ICommandUsageFormatter.cs` - Format usage message for single or multiple overloads
  - Customizable bracket characters for `<required>` vs `[optional]`
- `Services/ICommandEnumerator.cs` - Public API to list/search commands with optional permission filtering
  - Methods: `GetAll()`, `GetByGroup(CommandGroup)`, `FindByName(string)`, `Search(string query)`
  - When called from player context, filter by permission

**Design:**
- Interfaces with sensible default implementations
- Registered in DI container; user can override
- **Gap: Details on exception handling** in services (how are parser exceptions handled?)
---

## Phase 2: Command Dispatch & Parsing

### 2.1 Parameter Source Resolution

**Core concept:** Each method parameter is classified into one of three categories during registration:

1. **Prefix parameter** (special first parameter, not parsed from input)
   - For player commands: `Player` or any `Component` type
   - For console commands: `ConsoleCommandDispatchContext`
   - Only the first parameter can be a prefix parameter
   - Not included in command argument count

2. **Command parameter** (parsed from user input)
   - Has a registered `ICommandParameterParser` for its type
   - Parser extracts value from remaining input string

3. **Service parameter** (injected from DI container)
   - No parser found for type → inject via `IServiceProvider`
   - Not parsed from input, not included in command argument count
   - Allows access to services like `ITranslationService`, `ILogger`, etc.

**Parameter resolution algorithm (at registration time):**
```
foreach method parameter (p):
  if p is first parameter && (typeof(Component).IsAssignableFrom || typeof(ConsoleCommandDispatchContext)):
    → ParameterSource { IsPrefix = true, ParameterIndex = 0 }
  else if parser found for type:
    → ParameterSource { IsCommandParameter = true, ParameterIndex = commandParamIdx++ }
  else:
    → ParameterSource { IsService = true }
```

**Example:**
```csharp
[PlayerCommand]
public void PmCommand(Player sender, Player receiver, string message, ITranslationService svc)
{
    // Usage: /pm <receiver> <message>
}

// Resolved parameters:
// sender (Player): IsPrefix = true
// receiver (Player): IsCommandParameter = true, index = 0
// message (string): IsCommandParameter = true, index = 1  
// svc (ITranslationService): IsService = true
```

### 2.2 Command Overload Matching Algorithm

**Goal:** Given input text and multiple method overloads, find the best match.

**Algorithm:**
1. **Parse command name** from input (single word or multi-word for groups)
   - Command name matching is **case-insensitive by default**
   - Configurable option to enable case-sensitive matching if needed
2. **Find all overloads** matching that command name (across all aliases and groups)
3. **For each candidate overload:**
   - Perform permission check (player commands only)
   - Skip if permission denied
   - Try to parse all command parameters from remaining input
   - If parse fails: skip this overload
   - If parse succeeds: record how much input remains
4. **Select winner:** Overload with **least remaining input**
   - If tie: first registration wins (order matters)
5. **If no winner:** Use `ICommandUsageFormatter` service to format and return usage message

**Unhandled input policy:** Extra input is accepted (trailing args ignored). This allows abbreviations and graceful degradation.

**Parser exception handling:** Parser exceptions are **not caught by dispatcher**
- Parser should return `false` if parsing fails (parse exception = failure)
- If parser throws: exception propagates to caller's event system exception handler
- Command implementation should have try/catch; exception handler includes command name in context

### 2.3 CommandDispatcher Service

**Files to create:**
- `Core/CommandDispatcher.cs` - Pure, stateless dispatch logic

**Public method:**
```csharp
Task<DispatchResult> DispatchAsync(
    CommandRegistry registry, 
    IServiceProvider services, 
    object[] prefixArgs,  // [Player] or [ConsoleCommandDispatchContext]
    string inputText)
```

**Returns `DispatchResult`:**
```csharp
public class DispatchResult
{
    public CommandInvokeResponse Response { get; set; }  // Success, CommandNotFound, InvalidArguments, PermissionDenied, Error
    public string? UsageMessage { get; set; }  // For InvalidArguments
    public string? Message { get; set; }  // For console commands, optional text response
    public Exception? Exception { get; set; }  // For Error response
}

public enum CommandInvokeResponse
{
    Success,
    CommandNotFound,
    InvalidArguments,      // Overload matched but args didn't parse
    PermissionDenied,      // Only player commands
    Error                  // Exception during execution
}
```

**Flow:**
1. Parse command line → `(commandName, remainingInput)`
2. Look up all overloads for `commandName`
3. For each overload:
   - Check permission
   - Try to parse parameters
4. Execute best match or show usage
5. Handle async results with `Task`/`Task<bool>` detection
6. Capture exceptions → `Error` response

**Async handling:**
- Detect return type at registration time
- If method returns `Task<bool>` → await and treat result as bool
- If method returns `Task` → await and treat as success (void-like)
- If method returns `Task<T>` → await and check truthiness of result
- If method returns bool → synchronous: `false` = not found, `true` = success
- Exceptions in async methods caught and stored in result

### 2.4 Parameter Parsing

**Files to modify/create:**
- `Parsers/ICommandParameterParser.cs`
- `Parsers/CommandParameterParserFactory.cs`
- Built-in parsers: `StringParser`, `IntParser`, `FloatParser`, `BooleanParser`, `DoubleParser`, etc.

**Parser interface:**
```csharp
public interface ICommandParameterParser
{
    bool TryParse(string input, out object? result);
    
    // For help generation:
    string? GetTypeName();  // e.g., "number", "text"
    string? GetExample();   // e.g., "123"
}
```

**Factory finds parser by type:**
- Built-in type → use registered parser
- Custom type → attempt from DI container
- No parser → treat as service injection

### 2.5 Command Line Parsing (Groups & Aliases)

**Files to create:**
- `Parsing/CommandLineParser.cs` - Parse multi-word commands

**Algorithm:**
1. Split input into words
2. Try to match **longest command group prefix** (left-to-right)
   - E.g., for commands `/message send list` and `/msg`, input `/message send hello`:
     - Try `message send` → found, remainder = `hello`
     - Try `message` → found but `send hello` remains
     - Choose longest match
3. Fall back to single-word lookup for aliases
4. Return `(fullCommandName, remainingInput)` tuple

**Implementation note:** The command name returned should be the **fully qualified** name (groups + base name) for registry lookup



---

## Phase 3: Player Command System

### 3.1 PlayerCommandService

**Files to create/modify:**
- `Player/PlayerCommandService.cs` - Main service for dispatching player commands
- `Player/PlayerCommandAttribute.cs` - Attribute to mark command methods

**Responsibilities:**
- Scan systems at startup for `[PlayerCommand]` methods
- Build and cache `CommandRegistry` (only contains player commands)
- Hook into player input event (e.g., OnPlayerCommandText)
- Dispatch commands using `CommandDispatcher`
- Apply player-specific logic: permission checks, message formatting

**Public interface:**
```csharp
public class PlayerCommandService
{
    public bool Invoke(IServiceProvider services, EntityId player, string inputText)
    {
        // Returns true if command was found and executed (or permission denied)
        // Returns false if command not found
    }
}
```

**Invocation flow:**
1. Check if input starts with `/` → strip it
2. Call `_dispatcher.DispatchAsync(_registry, services, [player], inputText)`
3. Handle response:
   - `Success` → return `true`
   - `CommandNotFound` → call `ICommandNotFoundHandler.HandleAsync(player, inputText)` to send message to player, return `false`
   - `InvalidArguments` → call `ICommandUsageFormatter.FormatUsageAsync(player, commandDef)` to send usage message to player, return `true`
   - `PermissionDenied` → send "Permission denied" message via usage formatter, return `true`
   - `Error` → exception already handled by event system (propagated from dispatcher)

**Message sending:**
- `ICommandUsageFormatter` service is responsible for sending messages to player
- Default implementation calls `player.SendClientMessage(message)`
- User can override to send messages via different channel (chat, dialog, etc.)

### 3.2 Permission Checking

**Files to create:**
- `Player/PlayerPermissionChecker.cs` - Default implementation

**Permission flow (during dispatch):**
1. Extract `CommandTag` with key `"permission"` from `CommandDefinition`
2. If tag found: call `IPermissionChecker.HasPermission(player, commandDef)` (**synchronous**)
3. If `IPermissionChecker` not in DI → default to `true` (allow all)
4. If checker returns `false` → set response to `PermissionDenied`, skip execution

**Permission checker interface:**
```csharp
public interface IPermissionChecker
{
    bool HasPermission(Player player, CommandDefinition command);  // Synchronous
}
```

**Design note:** Permission checks must be synchronous to avoid latency spikes during command dispatch

### 3.4 Component Parameter Resolution

**For player commands only:** First parameter can be any `Component` type (not just `Player`)

**Behavior:**
- If first param is `Player` → pass the invoking player directly
- If first param is other `Component` type (e.g., `Vehicle`, `Object`)
  - Try to get component from invoking player via `player.GetComponent<T>()`
  - If component not found → command unavailable (same as permission denied)
  - Pass retrieved component as first argument

**Example:**
```csharp
[PlayerCommand("honk")]
public void HonkCommand(Vehicle vehicle)
{
    // Called only if player is in a vehicle
}
```



---

## Phase 4: Console Command System

### 4.1 ConsoleCommandDispatchContext Type

**Files to create:**
- `Console/ConsoleCommandDispatchContext.cs` - Context wrapper for console command execution

**Definition:**
```csharp
public class ConsoleCommandDispatchContext
{
    public IPlayer? Player { get; }  // null if sent from server console, Player if from in-game console
    public Action<string>? MessageHandler { get; }  // Call to send response message
    
    public ConsoleCommandDispatchContext(ConsoleCommandSenderData data)
    {
        Player = data.Player;
        MessageHandler = data.MessageHandler?.Send;
    }
}
```

### 4.2 Console Command Methods

**Attribute:** `[ConsoleCommand]` (same as `[PlayerCommand]` but for console)

**Prefix parameter:** First parameter can be `ConsoleCommandDispatchContext` (optional)
- If present: automatically provided during dispatch
- Allows access to sender context (to send responses)
- If not present: method can still read from services

**Example:**
```csharp
[ConsoleCommand("restart")]
public void RestartCommand(ConsoleCommandDispatchContext sender)
{
    sender.MessageHandler?.Invoke("Restarting server...");
}

[ConsoleCommand("players")]
public void PlayersCommand()  // No sender needed
{
    // Return type determines response
}
```

### 4.3 Console Command Return Values & Responses

**Return type handling:**
- `void` → No automatic response; void methods recommended for console commands
- `bool` → Not typically used for console commands
- `string` → Not typically used; void methods recommended
- `Task`, `Task<bool>`, `Task<string>` → Async variants

**Message sending:**
- `ICommandUsageFormatter` service is responsible for sending messages to console via `ConsoleCommandDispatchContext`
- Default implementation calls `consoleCommandDispatchContext.MessageHandler?.Invoke(message)`
- For invalid arguments or command not found, usage formatter sends appropriate message

### 4.4 ConsoleCommandService

**Files to create:**
- `Console/ConsoleCommandService.cs` - Dispatches console commands

**Responsibilities:**
- Scan systems for `[ConsoleCommand]` methods
- Build `CommandRegistry` (console-only)
- Hook into open.mp console command event
- Dispatch using `CommandDispatcher`
- Use `ICommandUsageFormatter` for error/usage messages

**Public interface:**
```csharp
public class ConsoleCommandService
{
    public bool Invoke(IServiceProvider services, ConsoleCommandSenderData senderData, string inputText)
    {
        // Returns true if command was found and executed
        // Returns false if command not found
        // All messages sent via ICommandUsageFormatter.FormatUsageAsync(consoleCommandDispatchContext, ...)
    }
}
```

**Invocation flow:**
1. Create `ConsoleCommandDispatchContext` from `senderData`
2. Call `_dispatcher.DispatchAsync(_registry, services, [sender], inputText)`
3. Handle response:
   - `Success` → return `true`
   - `CommandNotFound` → call `ICommandUsageFormatter.FormatNotFoundAsync(sender, inputText)`, return `false`
   - `InvalidArguments` → call `ICommandUsageFormatter.FormatUsageAsync(sender, commandDef)`, return `true`
   - `PermissionDenied` → never happens (no permission checks for console)
   - `Error` → exception already handled by event system

### 4.5 Console Command Access Control

**Players can only invoke console commands if:**
- They are logged in to the server
- Access control is handled by the underlying open.mp system, not by the command framework
- **No additional permission checks are needed for console commands** (unlike player commands)

### 4.6 Key Differences from Player Commands

- No permission checks (framework level)
- No `/` prefix stripping (console doesn't use `/`)
- `ConsoleCommandDispatchContext` is prefix parameter (provides context for message sending)
- Response handling via `ICommandUsageFormatter`, not direct messages

**Gap: Can console commands be called from in-game console as a player?** If so, should they bypass permission checks?



---

## Phase 5: Help System & Enumeration API

### 5.1 Command Enumeration

**Files to create:**
- `Help/ICommandEnumerator.cs` - Public API to list and search commands
- `Help/CommandEnumeratorImpl.cs` - Default implementation

**Public methods:**
```csharp
public interface ICommandEnumerator
{
    // Get all commands (optionally filter by permission for player context)
    IEnumerable<CommandDefinition> GetAllCommands(Player? player = null);
    
    // Get commands organized by group
    IEnumerable<CommandGroup> GetCommandGroups();
    IEnumerable<CommandDefinition> GetCommandsInGroup(CommandGroup group, Player? player = null);
    
    // Search/lookup
    CommandDefinition? FindCommand(string commandName, Player? player = null);
    IEnumerable<CommandDefinition> SearchCommands(string query, Player? player = null);
    
    // Get suggestions (e.g., for "command not found" handler)
    IEnumerable<CommandDefinition> GetSuggestions(string input, Player? player = null);
}
```

**Permission filtering:**
- When `player` is provided, only return commands the player has permission for
- When `player` is null, return all commands (console context)

### 5.2 Usage Message Generation

**Files to create:**
- `Help/CommandUsageFormatter.cs` - Format usage strings for single and multiple overloads

**Default format:**
- Single overload: `Usage: /command <requiredParam> [optionalParam]`
- Multiple overloads:
  ```
  Usage:
    /command <target> <message>
    /command <target> <message> [broadcast]
  ```

**Key design points:**
- **No parameter types shown**, only parameter names
- Bracket notation: `<name>` for required, `[name]` for optional
- Customizable bracket style via `ICommandUsageFormatter` implementation
- Default implementation sends message via `ICommandUsageFormatter` to player (or `ConsoleCommandDispatchContext` for console)



---

## Phase 6: Advanced Features

### 6.1 Async/Await Support

**Return type detection (at registration time):**

| Return Type | Behavior |
|---|---|
| `void` | Synchronous, always success |
| `bool` | Synchronous, `false` = not found, `true` = success |
| `Task` | Asynchronous, await then treat as success |
| `Task<bool>` | Asynchronous, await then check result |
| `string` | Synchronous, return string as response (console only) |
| `Task<string>` | Asynchronous, await then use result as response |
| Other `Task<T>` | Asynchronous, await then check `T` for truthiness |

**Exception handling:**
- Synchronous exceptions → caught by dispatcher, stored in `DispatchResult.Exception`
- Asynchronous exceptions (from Task) → caught, stored in result
- Unhandled exceptions should be logged via a configurable `IExceptionHandler` service

**Fire-and-forget considerations:**
- For player commands, we likely want to await and handle results
- For console commands, depends on whether command is background task or interactive
- **Gap: How do we distinguish between "fire-and-forget" and "await for response"?**

### 6.2 Attribute-Based Metadata (Tags)

**Implementation:**
- `[CommandTag("key", "value")]` adds custom metadata to command
- Multiple tags allowed (stackable)
- Accessible at runtime via `CommandDefinition.Tags` (Dictionary<string, string>)

**Built-in tags:**
- `"permission"` → used for permission checking (value is permission key)
- User can define custom tags for their own purposes

**Example:**
```csharp
[PlayerCommand]
[CommandTag("permission", "admin")]
[CommandTag("category", "moderation")]
public void KickCommand(Player sender, Player target) { }
```

### 6.3 Command Method Naming Conventions

**Name derivation (if not specified in attribute):**
1. Use method name
2. If ends with `"Command"` → strip suffix
3. Convert to lowercase

**Examples:**
- `MeCommand()` → `"me"`
- `PmCommand()` → `"pm"`
- `SendMessage()` → `"sendmessage"`
- `GetPlayerInfo()` → `"getplayerinfo"`

### 6.4 Command Name Matching Case Sensitivity

**Default behavior:** Command names are **case-insensitive**
- Input `/Test`, `/test`, `/TEST` all match command registered as `test`

**Customization:** Configurable via registration or registry option
- Per-registry setting to enable case-sensitive matching if needed
- Example: `PlayerCommandService.EnableCaseSensitiveMatching = true`

### 6.5 Multiple Registrations per Method

**One method can be registered multiple times via multiple attributes:**

```csharp
[CommandGroup("msg")]
[PlayerCommand("send")]      // → /msg send
[CommandAlias("pm")]         // → /pm (no group)
[CommandAlias("message")]    // → /message (no group)
public void SendPmCommand(Player sender, Player target, string message) { }
```

**Result: 3 command registrations**
- `/msg send <target> <message>`
- `/pm <target> <message>`
- `/message <target> <message>`

**Aliases ignore command groups** (always direct, no hierarchy)



---

## Phase 7: Unit Testing

**Goal:** Comprehensive test coverage of core logic with minimal dependencies

### 7.1 Test Project Structure

**Project:** `SampSharp.OpenMp.Entities.Commands.UnitTests` (pure C#, no runtime/ECS dependencies)

**Test categories:**
- **Core Logic Tests** - No mocking, pure input/output validation
- **Integration Tests** - With mocked DI container
- **Fixture Tests** - Realistic command scenarios

**Recommended tools:**
- xUnit (test framework)
- Moq or NSubstitute (mocking)
- FluentAssertions (readable assertions)

### 7.2 Test Coverage Areas

Core areas to test:

1. **Command Registration**
   - Attributes detected and parsed correctly
   - Groups stacked in correct order
   - Aliases registered separately
   - Method naming convention applied
   - Return types classified correctly

2. **Parameter Resolution**
   - Prefix parameters identified
   - Command parameters matched to parsers
   - Service parameters flagged correctly
   - Component parameter handling (Player, Vehicle, etc.)

3. **Command Dispatch**
   - Command lookup by name and alias
   - Overload matching with correct winner selection
   - Permission checks (player commands)
   - Permission denied blocks execution

4. **Parameter Parsing**
   - Each parser type (int, string, bool, float, etc.) works
   - Parse failures handled gracefully
   - Overload resolution picks best match

5. **Usage Message Generation**
   - Single overload format correct
   - Multiple overload format correct
   - Required vs optional parameters marked correctly
   - Custom bracket styles work

6. **Async Execution**
   - `Task` return awaited
   - `Task<bool>` return checked
   - Exceptions caught and stored
   - Result properly propagated

7. **Player Commands**
   - `/` prefix stripped
   - Permission check performed
   - Component resolution attempted
   - Error messages formatted

8. **Console Commands**
   - `ConsoleCommandDispatchContext` injected correctly
   - No permission checks applied
   - Response message returned

9. **Help/Enumeration**
   - Commands searchable
   - Groups organized correctly
   - Permission filtering works
   - Suggestions generated

### 7.3 Example Test Structure

```csharp
public class CommandRegistrationTests
{
    [Fact]
    public void RegisterPlayerCommand_WithoutName_UsesMethodName()
    {
        // Arrange
        var registry = new CommandRegistry();
        var method = typeof(TestSystem).GetMethod(nameof(TestSystem.TestCommand));
        
        // Act
        registry.RegisterPlayerCommand(method, ...);
        
        // Assert
        var cmd = registry.FindCommand("test");
        Assert.NotNull(cmd);
    }
}

public class CommandDispatchTests
{
    [Fact]
    public async Task Dispatch_WithValidInput_InvokesMethod()
    {
        // Arrange
        var registry = CreateRegistryWithTestCommand();
        var dispatcher = new CommandDispatcher();
        var services = new MockServiceProvider();
        var player = new MockPlayer();
        
        // Act
        var result = await dispatcher.DispatchAsync(
            registry, services, [player], "test arg1 arg2");
        
        // Assert
        Assert.Equal(CommandInvokeResponse.Success, result.Response);
    }
}
```



---

## Phase 8: Documentation

### 8.1 Documentation Content

**API Reference:**
- Attributes: `[PlayerCommand]`, `[ConsoleCommand]`, `[CommandGroup]`, `[CommandAlias]`, `[CommandTag]`
- Services: `PlayerCommandService`, `ConsoleCommandService`, `CommandDispatcher`, `CommandRegistry`
- Interfaces: `ICommandParameterParser`, `IPermissionChecker`, `ICommandUsageFormatter`, `ICommandEnumerator`
- Extension points and default implementations

**Examples:**
- Basic player command
- Player command with groups and aliases
- Console command
- Permission checking
- Custom usage formatter
- Building a help system with `ICommandEnumerator`

### 8.2 Documentation Structure

- **Getting Started:** Setup and basic usage
- **Advanced Topics:** Command groups, aliases, permissions, async methods
- **API Reference:** All public types and methods
- **Customization:** Implementing custom services
- **Troubleshooting:** Common issues and solutions



---

## Phase 9: Integration & Finalization

### 9.1 DI Container Registration

**Files to modify:**
- `EcsBuilderCommandsExtensions.cs` - Hook to register services

**Services to register (two separate registries):**
```csharp
public static IEcsBuilder AddCommands(this IEcsBuilder builder)
{
    // Core services (shared)
    builder.AddSingleton<CommandDispatcher>();
    
    // Player command system
    builder.AddSingleton<CommandRegistry>(sp => new CommandRegistry("player"));
    builder.AddScoped<PlayerCommandService>();
    
    // Console command system (separate registry)
    builder.AddSingleton<CommandRegistry>(sp => new CommandRegistry("console"));
    builder.AddScoped<ConsoleCommandService>();
    
    // Extensibility (defaults, user can override)
    builder.TryAddScoped<ICommandParameterParserFactory>(
        sp => new DefaultCommandParameterParserFactory(sp));
    builder.TryAddScoped<IPermissionChecker>(
        sp => new DefaultPermissionChecker());  // Always allow
    builder.TryAddScoped<ICommandUsageFormatter>(
        sp => new DefaultCommandUsageFormatter());
    builder.TryAddScoped<ICommandEnumerator>(
        sp => new DefaultCommandEnumerator(
            sp.GetRequiredService<CommandRegistry>(),
            sp.GetRequiredService<IPermissionChecker>()));
    
    return builder;
}
```

**Builder extension methods for customization:**
```csharp
builder.AddCommands()
    .WithPermissionChecker<MyPermissionChecker>()
    .WithUsageFormatter<MyUsageFormatter>()
    .EnableCaseSensitiveMatching(false);  // default: case-insensitive
```

### 9.2 Event Hook Integration

**Player commands:**
- Hook `IPlayer.OnCommandText` event or equivalent
- Call `PlayerCommandService.Invoke(services, player, text)`
- Use return value to indicate if command handled

**Console commands:**
- Hook open.mp server console command event
- Build `ConsoleCommandSenderData` from event context (mapped to `ConsoleCommandContext`)
- Call `ConsoleCommandService.Invoke(services, senderData, text)`
- Use return value to indicate if command handled

### 9.3 System Discovery & Registration

**At startup (when systems registered):**
1. After all systems are registered, scan for commands
2. For each `ISystem` type:
   - Scan methods for `[PlayerCommand]` attributes → add to player registry
   - Scan methods for `[ConsoleCommand]` attributes → add to console registry
3. Parse attributes (groups, aliases, tags, permissions)
4. Validate parameter types (all must be parseable or services)
5. Build `CommandDefinition` and `CommandOverload` objects
6. Cache for runtime lookup

**Triggers:**
- Called once at `IEcsBuilder.Build()` completion
- Before any game logic starts
- After all systems configured

### 9.4 Final Cleanup & Polish

- Remove any old command-related classes (if migrating from old system)
- Ensure DI registration is clean and documented
- Validate all service interfaces have default implementations
- Performance testing (command dispatch latency)
- Memory profiling (registry size, cache efficiency)

---

## Implementation Roadmap

**Suggested sequence (with parallellization where possible):**

1. **Week 1-2: Core Architecture (Phase 1)**
   - Define models, attributes, service interfaces
   - Create unit tests alongside (TDD approach)
   - Deliverable: Testable core with no dependencies

2. **Week 2-3: Dispatch Logic (Phase 2)**
   - Build parameter resolution algorithm
   - Implement overload matching
   - Build `CommandDispatcher`
   - Parallel: Enhance parsers (Phase 2.4)

3. **Week 3-4: Player Commands (Phase 3)**
   - Implement `PlayerCommandService`
   - Add permission checking
   - Component resolution
   - Deliverable: Functional player commands

4. **Week 4-5: Console Commands (Phase 4)**
   - Implement `ConsoleCommandService`
   - Create `ConsoleCommandDispatchContext`
   - Deliverable: Functional console commands

5. **Week 5: Help System (Phase 5)**
   - Build enumerator
   - Usage message formatter
   - Deliverable: Help API available

6. **Week 6: Advanced Features (Phase 6)**
   - Async/await support verification
   - Tag-based metadata
   - Deliverable: All features working

7. **Week 6-7: Testing (Phase 7)**
   - Comprehensive unit tests
   - Integration tests
   - Deliverable: >90% code coverage

8. **Week 7: Documentation & Migration (Phase 8)**
   - Migration guide
   - API docs
   - Examples

9. **Week 8: Integration & Polish (Phase 9)**
   - Register services in DI
   - Hook events
   - Final testing
   - Deliverable: Ready for release

---

## Key Design Principles

### 1. **Composition Over Inheritance**
- No base classes required
- Mix-and-match components
- Testable in isolation

### 2. **Dependency Injection**
- All external services injected via `IServiceProvider`
- Easy to swap implementations for testing
- User-friendly customization

### 3. **Immutable Metadata**
- Command definitions readonly at runtime
- Thread-safe, cacheable
- Fast lookups

### 4. **Extensibility via Interfaces**
- Custom parsers, permission checkers, formatters all pluggable
- Minimal base implementation, maximum flexibility
- User code never touches internal dispatch logic

### 5. **Async-First, Sync-Compatible**
- Dispatcher async by default
- Supports both sync and async methods
- Clean exception handling

### 6. **Clear Separation of Concerns**
- Registry = metadata collection
- Dispatcher = pure dispatch logic
- Services (Player/Console) = context-specific handling
- Parsers = parameter extraction
- Extensibility = user implementations

## Design Decisions (Settled)

The following design aspects have been clarified and settled:

1. **Message Delivery**
   - No direct messages from dispatcher to command caller
   - `ICommandUsageFormatter` service sends messages (receives Player or ConsoleCommandDispatchContext)
   - Default implementation sends via `player.SendClientMessage()` or `consoleCommandDispatchContext.MessageHandler?.Invoke()`

2. **Parser Exception Handling**
   - Parsers should return `false` on failure (not throw)
   - If parser throws: exception propagates to event system's exception handler
   - Command implementation should have try/catch; exception handler context includes command name

3. **Permission Checks**
   - Always **synchronous** (no async overhead)
   - Interface: `bool HasPermission(Player player, CommandDefinition command)`
   - Default: always allow (no permission checker registered)

4. **Console Commands Access**
   - Players can only execute console commands if logged in to server
   - Access control handled by underlying open.mp system (not framework)
   - No additional permission checks needed

5. **Parameter Display in Usage Messages**
   - **No parameter types shown**, only parameter names
   - Format: `Usage: /cmd <requiredName> [optionalName]`
   - Parameter type information not exposed in user messages

6. **Command Discovery**
   - **One-time at startup** (during system registration)
   - Scan systems from `ISystem` implementations
   - Dynamic addition not supported

7. **Localization**
   - Not implemented in this version
   - Text is centralized in `ICommandUsageFormatter` and "command not found" services
   - Can be added later via service implementations

8. **Help Command**
   - **User-implemented** (not provided by framework)
   - Framework provides `ICommandEnumerator` service for listing/searching commands
   - Users build their own `/help` command using this API

9. **Backward Compatibility**
   - This is a **new system** - no backward compatibility needed
   - No migration from old system required

10. **Case Sensitivity**
    - Command names **case-insensitive by default**
    - Configurable per-registry to enable case-sensitive matching if needed

---

## Success Criteria

- [x] All requirements from COMMANDS_REWORK_2.md met
- [x] Player commands with permission checking functional
- [x] Console commands (separate system) functional
- [x] Command groups, aliases, and method overloads working
- [x] Overload resolution (best-match algorithm) working
- [x] Async support implemented (Task, Task<bool>, etc.)
- [x] Case-insensitive command matching (with case-sensitive option)
- [x] Help enumeration API (`ICommandEnumerator`) working
- [x] Usage message formatting (no parameter types) working
- [x] Unit tests > 85% coverage
- [x] Documentation complete with examples
