SampSharp
=========

[![GitHub Action: dotnet](https://github.com/ikkentim/sampsharp/workflows/dotnet/badge.svg)](https://github.com/ikkentim/SampSharp/actions?query=workflow%3Adotnet)
[![GitHub Action: Plugin (Linux)](https://github.com/ikkentim/sampsharp/workflows/Plugin%20(Linux)/badge.svg)](https://github.com/ikkentim/SampSharp/actions?query=workflow%3A%22Plugin+%28Linux%29%22)
[![GitHub Action: Plugin (win32)](https://github.com/ikkentim/sampsharp/workflows/Plugin%20(win32)/badge.svg)](https://github.com/ikkentim/SampSharp/actions?query=workflow%3A%22Plugin+%28win32%29%22)
[![GitHub Releases](https://img.shields.io/github/release/ikkentim/sampsharp.svg)](https://github.com/ikkentim/sampsharp/releases)
[![GitHub Issues](https://img.shields.io/github/issues/ikkentim/sampsharp.svg)](https://github.com/ikkentim/sampsharp/issues)

[![Join us on Discord](https://discordapp.com/api/guilds/758751593725558794/widget.png?style=banner2)](https://discord.gg/gwcHpqp)

SampSharp is a plugin and library that allows you to write San Andreas: Multiplayer(SA-MP) gamemodes in C#. SampSharp's aim is to allow you to enjoy all features of object-oriented programming and .NET. SampSharp uses the .NET runtime which allows the plugin to run on both Windows and Linux server. For information about installing and building SampSharp, check the documentation. If you have any questions, feel free to join our Discord chat or file an issue.

Documentation
----------
The SampSharp .NET packages provided are available on [NuGet.org](https://www.nuget.org/packages/SampSharp.Core/) and contain all API documentation.

For general documentation and guides, see [https://sampsharp.net](https://sampsharp.net)

Examples
--------
Example gamemodes and sample projects are available at [https://github.com/sampsharp/samples](https://github.com/sampsharp/samples)

Building for Developers
-----------------------

### Building SampSharp (New)

> TODO: New SampSharp code structure not yet in place. Check back soon.

### Building Legacy SampSharp

The legacy SampSharp code (v1) is located in the `src/legacy/` directory. Use the build scripts in the root to build components:

**On Windows:**
```
.\build.cmd legacy-plugin           # Build x86 plugin
.\build.cmd legacy-plugin publish   # Build and publish plugin
.\build.cmd legacy-libraries        # Build C# libraries
.\build.cmd legacy-libraries publish # Build and pack NuGet packages
.\build.cmd clean                   # Clean build directory
```

**On Linux:**
```
./build.sh legacy-plugin            # Build x86 plugin
./build.sh legacy-plugin publish    # Build and publish plugin
./build.sh legacy-libraries         # Build C# libraries
./build.sh legacy-libraries publish # Build and pack NuGet packages
./build.sh clean                    # Clean build directory
```

Artifacts are placed in `build/artifacts/` and `build/bin/`:
- Plugin binaries: `build/artifacts/sampsharp-legacy/`
- NuGet packages: `build/artifacts/packages/`
- C# libraries: `build/bin/SampSharp.*/Release/net6.0/`

**Requirements:**
- **.NET SDK 6.0** (for building C# libraries)
- **CMake 3.19+** (for building the x86 plugin)
- **Visual Studio 2026 with C++ workload** (Windows)
- **gcc/g++ with 32-bit support** (Linux: `gcc-multilib g++-multilib`)
