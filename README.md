![SampSharp](https://raw.githubusercontent.com/ikkentim/SampSharp/master/SampSharp.png)

SampSharp
===
SampSharp is a plugin and library that allows you to write San Andreas: Multiplayer(SA-MP) gamemodes in C# (and possibly VB in the future). SampSharp aims on OO-programming, it contains many classes (see the SampSharp.World, SampSharp.SAMP and SampSharp.Screen namespaces) in order to get the most out of OO. All classes contain extensive documention. (although in the beta stage, some files may lack this yet)

Although the .NET framework only exists for windows, SampSharp also works on Linux. SampSharp uses the [Mono Framework](http://www.mono-project.com/). Be aware! Altough you can thoroughly enjoy the possiblilities of .NET, NuGet and C#, Mono is *not* **fully** compatible with all of .NET's features. Check [their website](http://www.mono-project.com/Compatibility) for more details.

**SampSharp is still in Beta! Although it's pretty stable already, crashes may occur.**

All feedback is welcome, file an Issue or contact me on the SA-MP forums. http://forum.sa-mp.com/member.php?u=76946

Contains
===
This repository contains the following projects:
* SampSharp

  The plugin to run your gamemodes.
  
* SampSharp.GameMode

  The basemode to extend on. Contains a lot of usefull classes
  
* TestMode

  Just ignore this project. When i work on features i ofter put code in this project to test them.

* RiverShell

  One of the example gamemodes provided by SA-MP, converted to C#. This is a gamemode I use to check whether all classes and systems are efficient and usefull. The code may change when I add features to SampSharp.

Things to do
===
* Write an install guide
* Add docs to methods and classes not having any.
* Focus on OO (Avoid having to call natives).
* Object streaming utilities.
* Area detection functionality (like in the streamer plugin)
* Test async functionallity(.NET 4.5)
* Benchmark testing.
* Convert all SA-MP sample gamemodes to C#.

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
