# SampSharp Project Guide for AI Agents

## Overview

SampSharp is a comprehensive C# framework for building game modes and plugins for open.mp and SA-MP servers using the .NET runtime. The project focuses on **open.mp (v1.x) as the actively developed version** with full entity systems, command processing, code analysis, and source code generation. **SA-MP support (v0.x) is considered legacy** with limited support for critical bug fixes and security patches only. Migration to open.mp is recommended for new projects.

## Project Structure

### Source Code (`src/`)

#### Active Development (open.mp v1.x)
- **SampSharp.OpenMp.Core/** - Core open.mp server functionality and bindings
- **SampSharp.OpenMp.Entities/** - Entity system for players, vehicles, objects, etc.
- **SampSharp.OpenMp.Entities.Commands/** - Rich command processor and dispatcher
- **SampSharp.Analyzer/** - Roslyn-based code analyzer for best practices
- **SampSharp.CodeFixes/** - Automatic code fixes for analyzer diagnostics
- **SampSharp.SourceGenerator/** - Source code generator for AOT compilation
- **SampSharp.Sdk/** - SDK tools and utilities

#### Legacy (SA-MP v0.x)
- **legacy/** - Legacy SA-MP implementation (limited support for critical fixes only)

### Testing (`test/`)
- **TestMode.OpenMp.Entities/** - Integration tests for entity system
- **TestMode.OpenMp.Core/** - Core functionality tests
- **TestMode.UnitTests/** - Unit tests

### Build & Environment
- **build/** - Build artifacts and CMake configurations
- **env/** - Runtime environment (game mode DLLs, test configurations, server files)
- **build.cmd** / **build.sh** - Main build scripts
- **SampSharp.slnx** - Slice project file (modern VS project format)

## Build System

### Building the open.mp Component (C++)
The open.mp component (C++ code) is built via `build.cmd` or `build.sh`:
```
./build.cmd component                  # Build open.mp component
./build.cmd component publish          # Build and publish open.mp component
```

### Building .NET Code
The .NET code is built using the dotnet CLI with `SampSharp.slnx` (Slice project file in root):
```
dotnet build SampSharp.slnx            # Build all .NET projects
dotnet build SampSharp.slnx -c Release # Build in Release configuration
```

### Legacy (SA-MP)
```
./build.cmd legacy-plugin              # Build legacy SA-MP plugin
./build.cmd legacy-plugin publish      # Build and publish legacy plugin
./build.cmd legacy-libraries           # Build legacy C# libraries
./build.cmd legacy-libraries publish   # Build and pack legacy NuGet packages
```

**Key files:**
- `SampSharp.slnx` - Slice project file for building .NET code
- `Directory.Build.props` - Centralized build properties
- `Directory.Packages.props` - Centralized NuGet package versions

## Code Conventions

### Naming
- **Classes/Interfaces:** PascalCase
- **Methods/Properties:** PascalCase
- **Private fields:** _camelCase
- **Namespaces:** SampSharp.* (folders do not necessarily map to namespaces)

### Architecture
- **Entity Pattern:** Entity components and systems
- **Dependency Injection:** Used throughout for testability
- **Async/Await:** Used for server operations
- **Generics:** Extensive use for type safety

## Common Tasks

### Building
- Build open.mp component (C++): `./build.cmd component`
- Build open.mp component with publishing: `./build.cmd component publish`
- Build all .NET projects: `dotnet build SampSharp.slnx`
- Build specific .NET project: `dotnet build SampSharp.slnx -p:ProjectName=ProjectNameHere`

### Testing
- Tests are located in `test/` directory
- Run via standard .NET test commands: `dotnet test SampSharp.slnx`
- Include unit tests and integration tests

### Code Analysis
- The project includes Roslyn analyzers for code quality
- Code fixes are automatically suggested and applicable

## Dependencies

### For open.mp (Active Development)
- **.NET SDK 10** - For building C# libraries
- **CMake 3.19+** - For building the open.mp component
- **Visual Studio 2026 with C++ workload** (Windows) or **gcc/g++ and cmake** (Linux)
- **open.mp SDK:** For server bindings

### For SA-MP (Legacy)
- **.NET SDK 6.0** - For building C# libraries
- **CMake 3.19+** - For building the x86 plugin
- **Visual Studio 2026 with C++ workload** (Windows) or **gcc/g++ with 32-bit support** (Linux)

## Important Notes

- The project bridges C# and C++ through P/Invoke and native bindings
- Entity system is core to the framework design
- Command processor is a critical component being actively developed
- Tests should cover both happy path and edge cases
- Source generation is used for performance-critical code paths

## Getting Started

### For open.mp (Recommended)
1. Ensure .NET SDK 10 and CMake 3.19+ are installed
2. Build the .NET libraries: `dotnet build SampSharp.slnx`
3. Build the open.mp component: `./build.cmd component`
4. Run tests to verify setup: `dotnet test SampSharp.slnx`
5. Check `env/` for runtime configuration examples

### For SA-MP (Legacy)
1. Ensure .NET SDK 6.0 and CMake 3.19+ are installed
2. Run `./build.cmd legacy-plugin` to build the legacy plugin
3. For new projects, migration to open.mp is recommended
