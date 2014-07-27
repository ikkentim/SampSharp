Bugs:
-Player.PutInVehicle sometimes crashes the server. See CommandsTest.cs for an example

Missing Classes:
-Pickup

Missing Documention:
-Multiple classes in SampSharp.GameMode lack documentation
-SampSharp.Streamer? (don't know whether i will add documentation here, it currently has as much documentation as the plugin itself)
-How to use EntityFramework with SampSharp
-Build guide
-Installation guide
-Usage guide
-Benchmark testing

Misc:
-Look if MenuColumn/MenuRow should be a struct

Mono features to look forward to:
-Implementation of AttributeTargets.Parameter
 When this is implemented ParameterAttribute instances (command system) can be atached to
 the parameter itself instead of the method and no longer need the .Name property.
-Implementation of ParameterInfo.HasDefaultValue and ParameterInfo.DefaultValue
 When these are implemented, ParameterAttribute (command system) no longer needs .Optional and .DefaultValue.
 These default values can instead be detected using ParameterInfo's properties.