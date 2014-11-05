![SampSharp](https://raw.githubusercontent.com/ikkentim/SampSharp/master/SampSharp.png)

SA-MP# is a plugin and library that allows you to write San Andreas: Multiplayer(SA-MP) gamemodes in C# and VB. SA-MP#'s aim is to allow you to enjoy all features of OO-programming and .NET. SA-MP# uses the [Mono Framework](http://www.mono-project.com/) to allow linux servers to run this plugin as well.

All feedback is welcome, file an Issue or contact me on the SA-MP forums. http://forum.sa-mp.com/member.php?u=76946

Note of caution
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
