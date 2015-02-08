SampSharp
=========
[![Plugin Build Status][travis_status]][travis_page]
[![Library build status][appveyor_status]][appveyor_page]
[![Gitter]][gitter page]

SA-MP# is a plugin and library that allows you to write San Andreas: Multiplayer(SA-MP) gamemodes in C# and VB. SA-MP#'s aim is to allow you to enjoy all features of OO-programming and .NET. SA-MP# uses the [Mono Framework] to allow linux servers to run this plugin as well. For information about installing and building SA-MP#, check the wiki.

All feedback is welcome, file an Issue or contact me on the SA-MP forums:

[My SA-MP profile]

[SA-MP# topic on SA-MP]

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

NuGet
-----
SA-MP# hosts it's own [NuGet Package Repository][nuget repository]. SampSharp.GameMode and all useful SA-MP# libraries are available from this repository. Instructions on adding the repository to Visual Studio are available [here][nuget repository].

[travis_status]: https://travis-ci.org/ikkentim/SampSharp.svg?branch=master
[travis_page]: https://travis-ci.org/ikkentim/SampSharp

[appveyor_status]: https://ci.appveyor.com/api/projects/status/p0jc1f8kbwgaceny/branch/master?svg=true
[appveyor_page]: https://ci.appveyor.com/project/ikkentim/sampsharp/branch/master

[gitter]: https://badges.gitter.im/Join%20Chat.svg
[gitter page]: https://gitter.im/ikkentim/SampSharp?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge

[mono framework]: http://www.mono-project.com/
[my sa-mp profile]: http://forum.sa-mp.com/member.php?u=76946
[sa-mp# topic on sa-mp]: http://forum.sa-mp.com/showthread.php?t=511686

[nuget repository]: https://github.com/ikkentim/SampSharp/wiki/NuGet-repository

[GrandLarc]: https://github.com/ikkentim/SampSharp-grandlarc
[RiverShell]: https://github.com/ikkentim/SampSharp-rivershell
[NHibernateTest]: https://github.com/ikkentim/SampSharp/tree/993f44b77356ad0c544ac10ad100919b5d1830cb/src/NHibernateTest
[nhibernate]: http://nhibernate.info/

[streamer]: https://github.com/ikkentim/SampSharp-streamer

[![Analytics](https://ga-beacon.appspot.com/UA-58691640-2/SampSharp/readme?pixel)](https://github.com/igrigorik/ga-beacon)
