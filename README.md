![SampSharp][icon]

SA-MP# is a plugin and library that allows you to write San Andreas: Multiplayer(SA-MP) gamemodes in C# and VB. SA-MP#'s aim is to allow you to enjoy all features of OO-programming and .NET. SA-MP# uses the [Mono Framework] to allow linux servers to run this plugin as well.

All feedback is welcome, file an Issue or contact me on the SA-MP forums. 

[My SA-MP profile]<br/>
[SA-MP# topic on SA-MP]

---

| **Table of Contents:** |
| --- |
| [Note of Caution](#note-of-caution) |
| [Requirements](#requirements) |
| [Contains](#contains) |
| [Building the SA-MP# plugin](#building-the-sa-mp-plugin) |
| [Installing SA-MP# and requirements](#installing-sa-mp-and-requirements) |
| [Building a gamemode with SA-MP#](#building-a-gamemode-with-sa-mp) |
| [Installing MySql.Data.dll on linux](#installing-mysqldatadll-on-linux) |
| [Missing Documentation](#missing-documention) |
| [License](#licence) |

Note of Caution
===
- I've only tested SA-MP# on a 32-bit ubuntu linux machine. Altough it should work on other linux distros, I can't guarantee that it does.
- **Be aware!** Mono is not **fully** compatible with all of .NET's features. Check [their website](http://www.mono-project.com/Compatibility) for more details.
- EntityFramework does not seem to be compatible with mono/SA-MP#! Instead, use NHibernate, which is just ase awesome as EntityFramework (if not more awesome). See src/NHibernateTest for an example of how to use NHibernate. For a why and how EF does not work with SA-MP#, see [#24](https://github.com/ikkentim/SampSharp/issues/24).

Requirements
===
- Mono version 3.10 or higher. (Embedded in windows build)
- sampgdk version 4.1.0 or higher. (Embedded in windows build)

Contains
===
This repository contains the following projects:
* SampSharp

  The plugin to run your gamemodes.
  
* SampSharp.GameMode

  The basemode to extend on. Contains a lot of usefull classes

* SampSharp.Streamer

  Wrapper for the streamer plugin by Incognito.
  
* TestMode

  Just ignore this project. When i work on features i ofter put code in this project to test them.

* NHibernateTest

  An example which shows you how to use NHibernate with SA-MP#

* RiverShell

  One of the example gamemodes provided by SA-MP, converted to C#. This is a gamemode I use to check whether all classes and systems are efficient and usefull. The code may change when I add features to SA-MP#.

* Grandlarc

  Another gamemode available in the default server package, converted to C#.

Building the SA-MP# plugin
===
**Windows**

- Download visual studio (if you haven't already)
- Open src/SampSharp.sln
- In the toolbar select Debug or Release
- In the Solution Explorer, right click on SampSharp > build.
- If you build in Debug mode, the library will be saved in environment/plugins. If you build in Release mode, the library wil be saved in src/Release.

**Linux**

*The following guide is for Ubuntu, it might be a little different on different distros*

- Install premake4 and g++: sudo apt-get install premake4 g++
- Change to the directory src/SampSharp
- Build the makefiles: premake4 gmake
- Build the plugin: make (builds in debug mode) -or- make config=release32 (builds in release mode).
- If you build in Debug mode, the library will be saved in environment/plugins. If you build in Release mode, the library wil be saved in src/SampSharp/bin/linux/Release.

Installing SA-MP# and requirements
===
**Windows**

- Download the windows release(sampsharp-*-win32.zip).
- Copy the contents of (sampsharp-*-win32.zip)/environment to your server's directory.

**Linux**

- Install mono 3.10 or newer. You can find their instructions [here](http://www.mono-project.com/download/#download-lin).
- Download sampgdk-*-linux.tar.gz [here](https://github.com/Zeex/sampgdk/releases) and copy all libsampgdk.x files to /usr/local/lib
- Download the linux release(sampsharp-*-linux.zip).
- Copy the contents of (sampsharp-*-linux.zip)/environment to your server's directory.

Building a gamemode with SA-MP#
===
- Using visual studio, create a new Visual C# Class Library project.
- In the Solution Explorer, under your newly created project, right click on References > Add Reference...
- Click on Browse > Browse... and locate SampSharp.GameMode.
- Create a class and Set it's base-class to SampSharp.GameMode.BaseMode. This will be your entrypoint.
- For more examples, check src/RiverShell and src/Grandlarc.

Installing MySql.Data.dll on linux
===
*The instructions on [MySql's website](http://dev.mysql.com/doc/connector-net/en/connector-net-installation-unix.html) are a little outdated, there for a more complete set of instructions:*

- Make sure you've got mono installed already. (type ```mono --version``` to check if it's installed and to see your current version)
- Download and extract the latest mysql-connector-net-(version)-noinstall.zip from http://dev.mysql.com/downloads/connector/net/ (select Platform: .NET & Mono)
- Locate MySql.Data.dll for .NET 4.5 and change to it's directory. (usually in /v4.5/)
- Add the library to the GAC using ```gacutil /i MySql.Data.dll```.
- Open up the following file with a texteditor (mono-directory)/4.5/machine.config (usually /etc/mono/4.5/machine.config).
- Locate the section ```<system.data>```.
- Inside ```<DbProviderFactories>```, add the following. If you have a MySql Connector/Net version other than 6.9.4.0, change the ```Version``` field accordingly.

  ```
<add name="MySQL Data Provider" invariant="MySql.Data.MySqlClient"
                 description=".Net Framework Data Provider for MySQL"
                 type="MySql.Data.MySqlClient.MySqlClientFactory, MySql.Data, Version=6.9.4.0, Culture=neutral, PublicKeyToken=c5687fc88969c44d" />
```

Missing Documention
===

- Multiple classes in SampSharp.GameMode lack documentation
- Benchmark testing

Licence
===
This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain. We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors. We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <http://unlicense.org>

[icon]: https://raw.githubusercontent.com/ikkentim/SampSharp/master/SampSharp.png
[mono framework]: http://www.mono-project.com/
[my sa-mp profile]: http://forum.sa-mp.com/member.php?u=76946
[sa-mp# topic on sa-mp]: http://forum.sa-mp.com/showthread.php?t=511686
