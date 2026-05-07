# SampSharp Command Processor - Test Coverage Analysis

## Executive Summary

**Current Coverage: 29% (227 tests covering 16 types out of 56 total types)**

The test suite provides solid foundational coverage of parsers and basic data structures, but has significant gaps in the orchestration and integration layers where most real-world bugs occur.

---

## Coverage Overview

### ✅ Types Currently Tested (16)

#### Core Data Structures (7)
- `StringSpan` - Zero-allocation string slicing utility
- `CommandGroup` - Hierarchical command grouping
- `CommandAlias` - Command shorthand names
- `CommandDefinition` - Individual command overload representation
- `CommandSet` - Command wrapper containing all overloads
- `CommandRegistry` - Command registration and lookup (basic coverage)
- `CommandDispatcher` - Command input parsing (basic coverage)

#### Parser Implementations (7)
- `WordParser` - Whitespace-delimited word parsing
- `StringParser` - Consumes all remaining input
- `IntParser` - Integer parsing
- `FloatParser` - Float parsing
- `DoubleParser` - Double parsing
- `BooleanParser` - Boolean value parsing (true/false, yes/no, on/off, 1/0)
- `EnumParser` - Enum value parsing (exact, case-insensitive, partial matching)

#### Service Utilities (2)
- `DefaultPermissionChecker` - Permission checking (always grants)
- `DefaultCommandTextFormatter` - Usage string generation

---

## ❌ Coverage Gaps - Critical Missing Areas

### 🔴 CRITICAL: Command Discovery & Execution Engine (9 types)

These represent the **core orchestration logic** - untested means the entire system foundation lacks validation.

#### CommandScanner (UNTESTED)
- **File**: `src/SampSharp.OpenMp.Entities.Commands/Core/CommandScanner.cs`
- **Purpose**: Reflection-based command discovery
- **Critical Logic**:
  - Scans classes for command methods using attributes
  - Extracts method signatures and parameter info
  - Associates aliases, tags, and permissions
  - Builds command definitions from metadata
- **Why Untested is Critical**: Bugs here cause commands to fail registration silently
- **Test Scenarios Needed**:
  - Discover methods with `[PlayerCommand]` attribute
  - Extract parameter types and count
  - Handle default parameters
  - Process nested command groups
  - Handle edge cases (no parameters, optional parameters, etc.)

#### CommandExecutor (UNTESTED)
- **File**: `src/SampSharp.OpenMp.Entities.Commands/Core/CommandExecutor.cs`
- **Purpose**: Invokes matched commands with parsed arguments
- **Critical Logic**:
  - Binds parsed arguments to method parameters
  - Handles parameter type conversion
  - Invokes method with correct signature
  - Catches and reports execution errors
- **Why Untested is Critical**: Bugs here cause commands to silently fail or crash
- **Test Scenarios Needed**:
  - Execute simple command (0 parameters)
  - Execute with typed parameters (int, string, enum)
  - Handle type mismatch errors gracefully
  - Invoke with correct object instance
  - Exception handling and error reporting

#### CommandTree & CommandTreeNode (UNTESTED)
- **File**: `src/SampSharp.OpenMp.Entities.Commands/Core/CommandTree.cs`
- **Purpose**: Hierarchical command lookup structure
- **Critical Logic**:
  - Builds tree from flat command list
  - Enables efficient hierarchical lookup
  - Supports grouped command searching (e.g., "admin money give")
  - Tree traversal and filtering
- **Why Untested is Critical**: Bugs here affect ALL command lookups
- **Test Scenarios Needed**:
  - Build tree from single command
  - Build tree with multiple grouped commands
  - Search command in tree
  - Handle overlapping command paths

#### DispatchResult & DispatchResponse (UNTESTED)
- **File**: `src/SampSharp.OpenMp.Entities.Commands/Core/DispatchResult.cs` / `DispatchResponse.cs`
- **Purpose**: Result objects from command dispatch
- **Critical Logic**:
  - Encapsulates dispatch outcome (success, not found, invalid args, permission denied)
  - Stores matched command and parsed arguments
  - Provides error information
- **Why Untested is Critical**: Response interpretation bugs affect error handling
- **Test Scenarios Needed**:
  - Success response with command and arguments
  - Not found response
  - Invalid arguments response
  - Permission denied response
  - Response equality and hashing

#### CommandParameterInfo (UNTESTED)
- **File**: `src/SampSharp.OpenMp.Entities.Commands/Core/CommandParameterInfo.cs`
- **Purpose**: Metadata about command parameters
- **Test Scenarios Needed**:
  - Parameter name and type storage
  - Optional vs. required parameters
  - Default value handling
  - Parameter equality

---

### 🔴 CRITICAL: Player & Console Command Pipelines (10 types)

These are the **entry points** - untested means main use cases are uncovered.

#### PlayerCommandService (UNTESTED)
- **File**: `src/SampSharp.OpenMp.Entities.Commands/Player/PlayerCommandService.cs`
- **Purpose**: Main service for processing player commands
- **Critical Logic**:
  - Receives player input
  - Dispatches to CommandDispatcher
  - Executes matched command with player context
  - Handles permission checks
  - Reports errors to player
- **Impact**: **ENTIRE player command flow untested**
- **Test Scenarios Needed**:
  - Execute valid command as player
  - Handle permission denied
  - Handle unknown commands
  - Execute grouped commands
  - Error message reporting to player

#### PlayerCommandProcessingMiddleware (UNTESTED)
- **File**: `src/SampSharp.OpenMp.Entities.Commands/Player/PlayerCommandProcessingMiddleware.cs`
- **Purpose**: Activation hook for player command system
- **Test Scenarios Needed**:
  - Middleware initialization
  - Command service registration
  - Player event binding

#### ConsoleCommandService (UNTESTED)
- **File**: `src/SampSharp.OpenMp.Entities.Commands/Console/ConsoleCommandService.cs`
- **Purpose**: Main service for processing console commands
- **Impact**: **ENTIRE console command flow untested**
- **Test Scenarios Needed**:
  - Execute valid console command
  - Handle console-specific permissions
  - Report console output

#### ConsoleCommandProcessingMiddleware (UNTESTED)
- **File**: `src/SampSharp.OpenMp.Entities.Commands/Console/ConsoleCommandProcessingMiddleware.cs`
- **Purpose**: Activation hook for console command system
- **Test Scenarios Needed**:
  - Middleware initialization
  - Console event binding

#### ConsoleCommandDispatchContext (UNTESTED)
- **File**: `src/SampSharp.OpenMp.Entities.Commands/Console/ConsoleCommandDispatchContext.cs`
- **Purpose**: Context object for console command dispatch
- **Test Scenarios Needed**:
  - Context creation and property access
  - Message output handling

#### Message Service Types (5 untested)
- `DefaultPlayerCommandMessageService`
- `DefaultConsoleCommandMessageService`
- `IPlayerCommandMessageService`
- `IConsoleCommandMessageService`
- `IConsoleCommandService`

---

### 🟠 HIGH: Advanced Parser & Factory (5 types)

#### DefaultCommandParameterParserFactory (UNTESTED)
- **File**: `src/SampSharp.OpenMp.Entities.Commands/Parsers/DefaultCommandParameterParserFactory.cs`
- **Purpose**: Factory for creating appropriate parser for parameter type
- **Critical Logic**:
  - Type-to-parser mapping
  - Selection of built-in vs. custom parsers
  - Parser caching
- **Test Scenarios Needed**:
  - Get parser for int (returns IntParser)
  - Get parser for custom enum (returns EnumParser)
  - Get parser for custom class (returns custom implementation)
  - Parser caching behavior

#### PlayerParser (UNTESTED)
- **File**: `src/SampSharp.OpenMp.Entities.Commands/Parsers/PlayerParser.cs`
- **Purpose**: Player reference parsing with complex matching
- **Critical Logic**:
  - Parse player by ID
  - Parse player by name (fuzzy matching, partial matching)
  - Handle ambiguous matches
  - Handle not-found cases
- **Test Scenarios Needed**:
  - Parse player by valid ID
  - Parse player by exact name
  - Parse player by partial name
  - Handle multiple name matches (ambiguous)
  - Handle player not found
  - Handle invalid ID format

#### Parser Interfaces (2 untested)
- `ICommandParameterParser`
- `ICommandParameterParserFactory`

---

### 🟡 MEDIUM: Attributes & Metadata (7 types)

#### Attribute Types (UNTESTED)
- `PlayerCommandAttribute` - Marks player command methods
- `ConsoleCommandAttribute` - Marks console command methods
- `CommandGroupAttribute` - Defines command grouping
- `AliasAttribute` - Defines command aliases
- `CommandTagAttribute` - Tags commands with metadata
- `RequiresPermissionAttribute` - Permission requirements
- `ICommandAttribute` - Base interface

**Test Scenarios Needed**:
- Attribute creation with various parameters
- Attribute property access
- Multiple attributes on same method
- Attribute validation

---

### 🟡 MEDIUM: Help & Documentation System (3 types)

#### DefaultCommandHelpProvider (UNTESTED)
- **File**: `src/SampSharp.OpenMp.Entities.Commands/Help/DefaultCommandHelpProvider.cs`
- **Purpose**: Generates help text for commands
- **Test Scenarios Needed**:
  - Generate help for simple command
  - Generate help for grouped command
  - Include parameter descriptions
  - Format usage strings

#### Help Interfaces (2 untested)
- `ICommandHelpProvider`

---

### 🟡 MEDIUM: Middleware & Extensions (4 types)

#### Middleware
- `ConsoleCommandListMiddleware` - Lists available commands

#### Dependency Injection Extensions
- `ServiceCollectionCommandsExtensions`
- `EcsBuilderCommandsExtensions`
- `EcsHostBuilderCommandsExtensions`

**Test Scenarios Needed**:
- DI container setup
- Service registration
- Middleware activation

---

## End-to-End Integration Test Gaps

### Missing Complete Workflows

#### Player Command Flow (UNTESTED)
```
Player Input "give 100 coins"
    ↓
PlayerCommandService.OnCommand()
    ↓
CommandDispatcher.Dispatch()
    ↓
WordParser / IntParser parse arguments
    ↓
CommandExecutor.Execute()
    ↓
Player.SendCommandMessage()
```
**Currently Tested**: Individual parsers and dispatcher
**Missing**: Complete flow from input to execution to output

#### Console Command Flow (UNTESTED)
Similar to player flow but for console

#### Command Discovery Flow (UNTESTED)
```
Class with [PlayerCommand] methods
    ↓
CommandScanner.ScanType()
    ↓
Reflection extraction
    ↓
CommandRegistry.Register()
    ↓
CommandTree built
```
**Currently Tested**: Nothing
**Missing**: Entire discovery pipeline

---

## Impact Assessment

### Critical for Correctness
- **CommandScanner**: Without tests, commands may fail silently on registration
- **CommandExecutor**: Without tests, commands may fail silently on execution
- **PlayerCommandService**: Without tests, entire player command system is unvalidated
- **CommandTree**: Without tests, command lookup reliability is unknown

### High Priority
- **PlayerParser**: Complex matching logic needs validation
- **DefaultCommandParameterParserFactory**: Type routing needs coverage

### Medium Priority
- **Attributes**: Metadata definition needs validation
- **Help system**: User-facing documentation accuracy
- **Services**: Error handling and message formatting

### Lower Priority (Likely Well-Covered Elsewhere)
- **DI Extensions**: Usually covered by integration tests in main application
- **Middleware**: Often tested via full application startup

---

## Recommended Testing Priorities

### Phase 1: Core Engine (Most Critical)
1. **CommandScanner** (15-20 tests)
   - Type scanning
   - Method attribute detection
   - Parameter extraction
   - Group hierarchy building

2. **CommandExecutor** (10-15 tests)
   - Parameter binding
   - Type conversion
   - Exception handling
   - Return value handling

3. **CommandTree** (5-10 tests)
   - Tree construction
   - Lookup operations
   - Path traversal

### Phase 2: Entry Points
4. **PlayerCommandService** (15-20 tests)
   - Command dispatch flow
   - Permission checks
   - Error handling

5. **ConsoleCommandService** (10-15 tests)
   - Similar to player but console-specific

### Phase 3: Advanced Features
6. **PlayerParser** (10-15 tests)
   - Fuzzy matching
   - Ambiguity handling

7. **Help System** (5-10 tests)
   - Help generation
   - Documentation accuracy

### Phase 4: Supporting Infrastructure
8. **Attributes** (5-10 tests)
9. **Middleware** (5-10 tests)
10. **DI Extensions** (5-10 tests)

---

## Current Test Quality Assessment

### Strengths of Existing Tests
- ✅ Good coverage of parser contracts and edge cases
- ✅ Comprehensive data structure testing
- ✅ Proper use of xUnit + Shouldly + Moq
- ✅ Organized by feature (Core/, Parsers/, Services/)
- ✅ All 227 tests passing consistently

### Weaknesses
- ❌ No integration tests showing complete workflows
- ❌ No reflection/discovery testing (CommandScanner)
- ❌ No execution path testing (CommandExecutor)
- ❌ No player/console service testing
- ❌ No middleware testing
- ❌ No DI/extension testing
- ❌ No attribute metadata testing

---

## Estimated Effort to Reach Full Coverage

| Phase | Test Files | Tests | Est. Hours |
|-------|-----------|-------|-----------|
| Current | 12 | 227 | (Done) |
| Phase 1 | 3 | 40 | 6-8 |
| Phase 2 | 2 | 35 | 6-8 |
| Phase 3 | 2 | 30 | 4-6 |
| Phase 4 | 3 | 25 | 4-6 |
| **Total** | **22** | **357** | **20-28** |

---

## Conclusion

The current 227-test suite provides excellent **foundational coverage** of parsers and data structures. However, reaching comprehensive coverage would require:

1. **~40 additional tests for the command discovery/execution engine** (CommandScanner, CommandExecutor)
2. **~35 additional tests for service entry points** (PlayerCommandService, ConsoleCommandService)
3. **~30-55 additional tests for advanced features and integration**

The biggest gaps are in the **orchestration and integration layers** - the actual workflows that tie everything together. These are the areas most likely to have bugs in production.

### Quick Wins
If only implementing one more phase, prioritize **CommandScanner and CommandExecutor tests** - these cover the heart of the system and would catch the most critical bugs.
