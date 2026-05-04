The command system is a mess. It's not working and does not satisfy all requirements yet.

Goal:

## Support 2 distinct use cases:

### Console commands
Commands may be send  to the console by logged in 'players' or other ways.

Based on method of connection to the server console, messages may be sent back. this is provided in `ConsoleCommandSender`;
but this will need an extension method provided commands project that provides a 'SendResponse' method which either sends a client message to the player associated with sender.

From the entry event, a boolean may be returned to indicate success (true = handled, false = command not found / not handled). Any textual response should be send using extension method described above.

[ConsoleCommands] detected in systems, may or may not have a 'prefix' argument (detected by type) of type `ConsoleCommandSender` which would then be passed in when command is called.

No custom 'command not found' will even be shown thru console commands.

### Player commands
Player commands must always have a 'prefix' argument of the type `Player`. their commands have [PlayerCommand] attribute in systems. Any textual response may be send as a client message to player.

For entry event, a boolean may be returned to indicate command handled (true handled, false = command not found/no); by default return false when command not handled but a user implementable service should be able to provide a custom handling of 'unknown command', which when implemented would make this event return true.

Input text for players always start with a `/`

after right command overload is found, a permission check service should be called with command info object and player component. if service not found handle as if player has permission. if not has permission handle as if not found.
### Common system
With the distinctions/exceptions mentioned above, the rest should be a common implementation, shared between the two cases.

both command systems should have their own `[IConsole/IPlayer]CommandsService`; may have a shared base interface. from there an ICommandEnumerator may be received to get list of commands for user to create a help command. this enumerator will have option to check permissions first (but permission check is only to be implemented for player commands; for console perm check is no-op)

Commands have a name or names. They may be prepended by group names; groups are added with attributes. `[CommandGroup("a", "b")]` would put commands inside into `a -> b` group. making calling like `/a b command name `

methods may have multiple command methods. then they will be registered multiple times. if command alias attribute also added then command is registered another time, but then with given name, without prepending commands groups so alway. e.g. `[CommandGroup("a")]class Sys : ISystem { [CommandGroup("b"), PlayerCommand("c"), PlayerCommand("d"), CommandAlias("e")]}` -> registered as `/a b c`, `/a b d` and `/e` **aliases have no special treatment** they are just also registered in another name, along side other registrations.

a command may receive CommandTag("key", "value") attributes; this are custom metadata added to command. multiple may be added. dictionary<string, string> bag of data.

commands may have optional parameters. denoted with a default value in c#. e.g. `MyCommand(string a, int b = 0)`.

command may have not a name in attribute argument (`[PlayerCommand]`) in that case use lower case method name; if method ends in `command`  then strip that off. e.g. `MeCommand(Player p)`-> `me` as name

commands have searched for in systems and a compiled invoker generated are boot/setup time. generation uses `MethodInvokerFactory` for invoker builder **current implementation of this in command lib is very flawed** take no note of current implementation; set it up properly and better.

parameter of method decide what parameters a command have. parameter may also be a service. rule of thumb: first parameter -> special treatment to detect 'prefix' mentioned before. then next all check parser command parameter parser. no parser found for parameter -> inject it as a service.

look up how event system does this for inspiration. not exactly the same but will generally help. roughly: first param special treatment -> non special parameters: send through parameter parser factory. those with results -> put into parameters list. -> make new array[paramCount + (hasPrefix ? 1 : 0)] named paramSources; q=0, p=0; if(hasprefix){paramSources[q++] = new MethodParameterSource{ParameterIndex = p++}; } then rest match param index to the index where command parameter is found. set ParameterIndex to the index in which the parameter input value can be found. set IsService true if method parameter is not a command parameter. set IsComponent true if !IsService && typeof(Component).IsAssignableFrom(paramType).

example

[PlayerCommand]
public void PmCommand(Player sender, Player receiver, string message, ITranslationService svc)
{
    // usage would be /pm [reciever] [message]
}

parameterSource[0] should be { ParameterIndex = 0, IsComponent = true } /* special treatment, is not a command parameter */
parameterSource[1] should be { ParameterIndex = 1, IsComponent = true }
parameterSource[2] should be { ParameterIndex = 2 }
parameterSource[3] should be { IsService = true }

...

command return value: methods may return void, bool, Task, ValueTask, Task<bool>, ValueTask<bool>

for non bool returning: command always success; for bool return: if return false then handle command is if it was not found;
for Task<bool> / ValueTask<bool> -> if has result then handle like bool with result. if not yet result. register handler for result as to 'handle' unhandled exceptions in command. other than that handle like a void result

also there is RequiresPermissionAttribute. this is implemented differently currently, but how it should be: just inherit CommandTag with key = "permission" and value from the attribute.


command overloading: same name command may have multiple registered commands overloads. overload resolution:

for each possible follow operations: overload -> perm check -> parse parameters from input text -> on parse failure reject overload -> all parameters parsed? path where least amount of remainder input text is left over wins. no winners? show usage message. we have a default usage message handler that can be overridden by the user. by default : 1 overload: `Usage: $prefix$commandNameAsEntered $argumentsWithBracetsToIndicateOptionalOrRequired` multi overload: `Usage:\n<per overload prefix/commandName/etc...\n>`
default usage message e.g. `HiCommand(Player sender, Player receiver, string message, bool shout = false) ` -> `Usage /hi <receiver> <message> [shout]` (`<required> [optional]`). Make the bracket sets properties in the default implementation such that user can override them.

each command service has their own command registry/data/etc. data/register/etc of console commands/player commands dont mix.