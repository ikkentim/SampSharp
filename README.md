SampSharp
=========

[![GitHub Releases](https://img.shields.io/github/release/ikkentim/sampsharp.svg)](https://github.com/ikkentim/sampsharp/releases)
[![GitHub Issues](https://img.shields.io/github/issues/ikkentim/sampsharp.svg)](https://github.com/ikkentim/sampsharp/issues)

[![Join us on Discord](https://discordapp.com/api/guilds/758751593725558794/widget.png?style=banner2)](https://discord.gg/gwcHpqp)

SampSharp is a plugin and library that allows you to write gamemodes in C# for open.mp and SA-MP servers using the .NET runtime. SampSharp's aim is to provide all features of object-oriented programming and modern .NET development.

SampSharp for open.mp (v1.x) is the actively developed version and is recommended for new projects. The original SampSharp (v0.x), created in 2014 for SA-MP, remains available with limited support for critical bug fixes and security patches. If you're currently running SA-MP, you can continue using the legacy version, but migration to open.mp is recommended to benefit from ongoing development and improvements.

For information about installing and building SampSharp, check the documentation. If you have any questions, feel free to join our Discord chat or file an issue.

Documentation
----------
The SampSharp .NET packages provided are available on [NuGet.org](https://www.nuget.org/packages/SampSharp.Core/) and contain all API documentation.

For general documentation and guides, see [https://sampsharp.net](https://sampsharp.net)

Examples
--------
Example gamemodes and sample projects are available at [https://github.com/sampsharp/samples](https://github.com/sampsharp/samples)

Building for Developers
-----------------------

### Building SampSharp

Use the build scripts in the root to build components:

**On Windows:**
```
.\build.cmd component                  # Build open.mp component
.\build.cmd component publish          # Build and publish open.mp component
.\build.cmd component-libraries        # Build C# libraries
.\build.cmd component-libraries publish # Build and pack C# libraries
.\build.cmd clean                      # Clean build directory
```

**On Linux:**
```
./build.sh component                   # Build open.mp component
./build.sh component publish           # Build and publish open.mp component
./build.sh component-libraries         # Build C# libraries
./build.sh component-libraries publish # Build and pack C# libraries
./build.sh clean                       # Clean build directory
```

Artifacts are placed in `build/artifacts/`.

**Requirements:**
- **.NET SDK 10** (for building C# libraries)
- **CMake 3.19+** (for building the open.mp component)
- **Visual Studio 2026 with C++ workload** (Windows)
- **gcc/g++ and cmake** (Linux: `gcc g++ cmake`)

### Building Legacy SampSharp

The legacy SampSharp code (v0.x) is located in the `src/legacy/` directory. Use the build scripts in the root to build components:

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

Artifacts are placed in `build/artifacts/`.

**Requirements:**
- **.NET SDK 6.0** (for building C# libraries)
- **CMake 3.19+** (for building the x86 plugin)
- **Visual Studio 2026 with C++ workload** (Windows)
- **gcc/g++ with 32-bit support** (Linux: `gcc-multilib g++-multilib cmake`)
