

SampSharp
=========

| Plugin | Framework |
|:------:|:---------:|
|[![Travis](https://img.shields.io/travis/ikkentim/SampSharp.svg)](https://travis-ci.org/ikkentim/SampSharp)|[![AppVeyor](https://img.shields.io/appveyor/ci/ikkentim/sampsharp.svg)](https://ci.appveyor.com/project/ikkentim/sampsharp/)|

[![GitHub release](https://img.shields.io/github/release/ikkentim/sampsharp.svg)](https://github.com/ikkentim/sampsharp/releases)
[![GitHub issues](https://img.shields.io/github/issues/ikkentim/sampsharp.svg)](https://github.com/ikkentim/sampsharp/issues) [![Gitter](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/ikkentim/SampSharp?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)

SA-MP# is a plugin and library that allows you to write San Andreas: Multiplayer(SA-MP) gamemodes in C# and VB. SA-MP#'s aim is to allow you to enjoy all features of OO-programming and .NET. SA-MP# uses the [Mono Framework] to allow linux servers to run this plugin as well. For information about installing and building SA-MP#, check the wiki.

All feedback is welcome, file an Issue or contact me on the SA-MP forums:

[My SA-MP profile]

[SA-MP# topic on SA-MP]

NuGet
-----
SA-MP# hosts it's own [NuGet Package Repository][nuget repository]. SampSharp.GameMode and all useful SA-MP# libraries are available from this repository. Instructions on adding the repository to Visual Studio are available [here][nuget repository].
Feel free to contribute your useful SA-MP# libraries to this repository!

Documation
----------
The SampSharp.GameMode library provided by the NuGet repository contains all available documentation. You can also view the documentation on the web via http://sampsharp.timpotze.nl/

Examples
--------
Some example gamemodes are available here:
- [Grand Larceny][GrandLarc] - SA-MP default gamemode ported to C#
- [RiverShell][RiverShell] - Another SA-MP default gamemode ported to C#
- [NHibernate test][NHibernateTest] - Since I haven't found a way to get EntityFramework to work, here is a simple example of using NHibernate together with FluentNHibernate. [NHibernate][nhibernate] is an ORM for C#.

Libraries
---------
Some useful SA-MP# libraries:
- [Streamer wrapper][streamer] - OO wrapper around streamer plugin.

[gitter]: https://badges.gitter.im/Join%20Chat.svg
[gitter page]: https://gitter.im/ikkentim/SampSharp?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge

[mono framework]: http://www.mono-project.com/
[my sa-mp profile]: http://forum.sa-mp.com/member.php?u=76946
[sa-mp# topic on sa-mp]: http://forum.sa-mp.com/showthread.php?t=511686

[nuget repository]: http://sampsharp.timpotze.nl/NuGet-repository

[GrandLarc]: https://github.com/ikkentim/SampSharp-grandlarc
[RiverShell]: https://github.com/ikkentim/SampSharp-rivershell
[NHibernateTest]: https://github.com/ikkentim/SampSharp/tree/993f44b77356ad0c544ac10ad100919b5d1830cb/src/NHibernateTest
[nhibernate]: http://nhibernate.info/

[streamer]: https://github.com/ikkentim/SampSharp-streamer
