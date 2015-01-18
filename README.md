__Notice__
On 28-12-2014 I've removed the mono binaries and some old files from the git history. Download the mono binaries from http://deploy.timpotze.nl/packages/mono-portable.zip and store them inside the /environment folder if you are running SampSharp on windows.

----

[![Gitter](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/ikkentim/SampSharp?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

SampSharp
-------------

SA-MP# is a plugin and library that allows you to write San Andreas: Multiplayer(SA-MP) gamemodes in C# and VB. SA-MP#'s aim is to allow you to enjoy all features of OO-programming and .NET. SA-MP# uses the [Mono Framework] to allow linux servers to run this plugin as well. For information about installing and building SA-MP#, check the wiki.

All feedback is welcome, file an Issue or contact me on the SA-MP forums:<br/>
[My SA-MP profile]<br/>
[SA-MP# topic on SA-MP]

Note of Caution
-------------

- I've only tested SA-MP# on a 32-bit ubuntu linux machine. Altough it should work on other linux distros, I can't guarantee that it does.
- **Be aware!** Mono is not **fully** compatible with all of .NET's features. Check [their website](http://www.mono-project.com/Compatibility) for more details.
- EntityFramework does not seem to be compatible with mono/SA-MP#! Instead, use NHibernate, which is just ase awesome as EntityFramework (if not more awesome). See src/NHibernateTest for an example of how to use NHibernate. For a why and how EF does not work with SA-MP#, see [#24](https://github.com/ikkentim/SampSharp/issues/24).

Licence
-------------

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



[![Analytics](https://ga-beacon.appspot.com/UA-58691640-2/SampSharp/readme)](https://github.com/igrigorik/ga-beacon)
