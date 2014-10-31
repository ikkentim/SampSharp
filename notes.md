Bugs:
------

* Player.PutInVehicle sometimes crashes the server. See CommandsTest.cs for an example


Missing Classes:
------

 

Missing Documention:
------

- Multiple classes in SampSharp.GameMode lack documentation
- Build guide
- Installation guide
- Usage guide
- Benchmark testing

Misc:
------

* Look if MenuColumn/MenuRow should be a struct

Mono features to look forward to:
------

* Implementation of AttributeTargets.Parameter

   When this is implemented ParameterAttribute instances (command system) can be atached to the parameter itself instead of the method and no longer need the .Name property.