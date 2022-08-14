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
The SampSharp .NET packages provided are available on NuGet.org and contains all API documentation. Additional documentation is available on https://sampsharp.net/ and https://api.sampsharp.net/

Examples
--------
Some example gamemodes are available here:
- [GrandLarc (GM)](https://github.com/SampSharp/sample-gm-grandlarc) - SA-MP default gamemode "grandlarc" ported to C# using SampSharp.GameMode
- [GrandLarc (ECS)](https://github.com/SampSharp/sample-ecs-grandlarc) - SA-MP default gamemode "grandlarc" ported to C# using SampSharp.Entities
- [RiverShell (GM)](https://github.com/SampSharp/sample-gm-rivershell) - SA-MP default gamemode "rivershell" ported to C# using SampSharp.GameMode

Building SampSharp
------------------
In order to build the .NET libraries you can simply open and build `SampSharp.sln` using Visual Studio 2022, or you can build it from the command line using dotnet (version 6 or newer) `dotnet publish SampSharp.sln --configuration Release`.

To build the plugin on Windows, build `SampSharp.Plugin.sln` with Visual Studio 2022. You'll need to have the "Desktop development with C++" workload installed using Visal Studio Installer.

To build the plugin on Linux you'll at least need to have the packages `make gcc g++ gcc-multilib g++-multilib` installed. Run `make` to build the plugin.
