// SampSharp
// Copyright 2020 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SampSharp.Entities.SAMP.Commands.Parsers;
using SampSharp.Entities.Utilities;

namespace SampSharp.Entities.SAMP.Commands
{
    /// <summary>
    /// Represents a base for service which provides functionality for calling commands in system classes.
    /// </summary>
    public abstract class CommandServiceBase
    {
        private readonly IEntityManager _entityManager;
        private readonly int _prefixParameters;

        private readonly Dictionary<string, List<CommandData>> _commands = new Dictionary<string, List<CommandData>>();

        /// <inheritdoc />
        protected CommandServiceBase(IEntityManager entityManager, int prefixParameters)
        {
            if (prefixParameters < 0) throw new ArgumentOutOfRangeException(nameof(prefixParameters));

            _entityManager = entityManager;
            _prefixParameters = prefixParameters;

            CreateCommandsFromAssemblies(prefixParameters);
        }

        /// <summary>
        /// Validates and corrects the specified <paramref name="inputText" />.
        /// </summary>
        /// <param name="inputText">The text to validate. The text can be corrected to remove symbols which should not be processed.</param>
        /// <returns><c>true</c> if the inputText text is valid.</returns>
        protected virtual bool ValidateInputText(ref string inputText)
        {
            return !string.IsNullOrEmpty(inputText);
        }

        /// <summary>
        /// Invokes a command based on the specified <paramref name="inputText" />.
        /// </summary>
        /// <param name="services">A service provider.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="inputText">The inputText.</param>
        /// <returns>The result</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="prefix" /> is null when it must contain values.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="prefix" /> is empty when it must contain values.</exception>
        protected InvokeResult Invoke(IServiceProvider services, object[] prefix, string inputText)
        {
            if (_prefixParameters > 0 && prefix == null)
                throw new ArgumentNullException(nameof(prefix));

            if (_prefixParameters > 0 && prefix.Length != _prefixParameters)
                throw new ArgumentException($"The prefix must contain {_prefixParameters} values.", nameof(prefix));

            if (!ValidateInputText(ref inputText))
                return InvokeResult.CommandNotFound;

            // Find name of the command by the first word
            var index = inputText.IndexOf(" ", StringComparison.InvariantCulture);
            var name = index < 0 ? inputText : inputText.Substring(0, index);
            var result = false;
            var invalidParameters = false;

            // TODO: Commands in groups would have spaces in them, the logic above would not work.

            // Skip command name in inputText for the command
            inputText = inputText.Substring(name.Length);

            // Find commands with the name
            if (!_commands.TryGetValue(name, out var commands))
                return InvokeResult.CommandNotFound;

            foreach (var command in commands)
            {
                var cmdInput = inputText;

                // Parse the command arguments using the parsers provided by the command
                var accept = true;
                var useDefaultValue = false;
                for (var i = 0; i < command.Info.Parameters.Length; i++)
                {
                    var parameter = command.Info.Parameters[i];
                    if (useDefaultValue)
                    {
                        command.Arguments[i + _prefixParameters] = parameter.DefaultValue;
                        continue;
                    }

                    if (parameter.Parser.TryParse(services, ref cmdInput, out command.Arguments[i + _prefixParameters]))
                        continue;

                    if (parameter.IsRequired)
                    {
                        accept = false;
                        break;
                    }

                    useDefaultValue = true;
                    command.Arguments[i + _prefixParameters] = parameter.DefaultValue;
                }

                // If parsing succeeded, invoke the command
                if (accept)
                {
                    if (_prefixParameters > 0)
                        Array.Copy(prefix, command.Arguments, _prefixParameters);

                    if (services.GetService(command.SystemType) is ISystem system)
                    {
                        var commandResult = command.Invoke(system, command.Arguments, services, _entityManager);

                        // Check if execution was successful
                        if (!(commandResult is bool bResult && !bResult) &&
                            !(commandResult is int iResult && iResult == 0)) result = true;
                    }
                }
                else
                {
                    invalidParameters = true;
                }

                Array.Clear(command.Arguments, 0, command.Arguments.Length);

                if (result)
                    return InvokeResult.Success;
            }

            if (!invalidParameters)
                return InvokeResult.CommandNotFound;

            var usageMessage = GetUsageMessage(commands.Select(c => c.Info).ToArray());
            return new InvokeResult(InvokeResponse.InvalidArguments, usageMessage);
        }

        /// <summary>
        /// Scans for methods in <see cref="ISystem" /> which should be considered to be compiled as a command.
        /// </summary>
        /// <param name="scanner">A scanner which is already limited to members of types which implement <see cref="ISystem" />.</param>
        /// <returns>The methods which provide commands.</returns>
        protected abstract IEnumerable<(MethodInfo method, ICommandMethodInfo commandInfo)> ScanMethods(
            AssemblyScanner scanner);

        private IEnumerable<(MethodInfo method, ICommandMethodInfo commandInfo)> ScanMethods()
        {
            var scanner = new AssemblyScanner()
                .IncludeAllAssemblies()
                .IncludeNonPublicMembers()
                .Implements<ISystem>();

            return ScanMethods(scanner);
        }
        
        private static string CommandText(CommandInfo command)
        {
            return command.Parameters.Length == 0
                ? $"Usage: {command.Name}"
                : $"Usage: {command.Name} " + string.Join(" ",
                      command.Parameters.Select(arg => arg.IsRequired ? $"[{arg.Name}]" : $"<{arg.Name}>"));
        }

        /// <summary>
        /// Gets the usage message for one or multiple specified <paramref name="commands" />. 
        /// </summary>
        /// <param name="commands">The commands to get the usage message for. If multiple commands are supplied they can be assumed to be multiple overloads of the same command.</param>
        /// <returns>The usage message for the commands.</returns>
        protected virtual string GetUsageMessage(CommandInfo[] commands)
        {
            return commands.Length == 1
                ? CommandText(commands[0])
                : $"Usage: {string.Join(" -or- ", commands.Select(CommandText))}";
        }
        
        /// <summary>
        /// Creates the parameter parser for the parameter at the specified <paramref name="index" /> in the <paramref name="parameters" /> array..
        /// </summary>
        /// <param name="parameters">An array which contains all parameters.</param>
        /// <param name="index">The index of the parameter to get the parser for.</param>
        /// <returns>A newly created parameter parser or null if no parser could be made for the specified parameter.</returns>
        protected virtual ICommandParameterParser CreateParameterParser(ParameterInfo[] parameters, int index)
        {
            var parameter = parameters[index];

            if(parameter.ParameterType == typeof(int))
                return new IntParser();

            if (parameter.ParameterType == typeof(string))
                return index == parameters.Length - 1
                    ? (ICommandParameterParser) new StringParser()
                    : new WordParser();
            
            if(parameter.ParameterType == typeof(float))
                return new FloatParser();
            
            if(parameter.ParameterType == typeof(double))
                return new DoubleParser();
            
            if(parameter.ParameterType == typeof(Player))
                return new PlayerParser();

            if(parameter.ParameterType == typeof(Vehicle))
                return new EntityParser(SampEntities.VehicleType);

            return parameter.ParameterType.IsEnum 
                ? new EnumParser(parameter.ParameterType) 
                : null;
        }

        /// <summary>
        /// Tries to collect parameter information.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="prefixParameters">The number of prefix parameters.</param>
        /// <param name="result">The collected parameter information.</param>
        /// <returns><c>true</c> if the parameters could be collected; otherwise <c>false</c>.</returns>
        protected virtual bool TryCollectParameters(ParameterInfo[] parameters, int prefixParameters, out CommandParameterInfo[] result)
        {
            if (parameters.Length < prefixParameters)
            {
                result = null;
                return false;
            }

            var list = new List<CommandParameterInfo>();

            // Parameter index in intermediate parameters array between parsing and invoking.
            var parameterIndex = prefixParameters;
            var optionalFound = false;
            for (var i = prefixParameters; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                var attribute = parameter.GetCustomAttribute<CommandParameterAttribute>();

                var name = parameter.Name;
                ICommandParameterParser parser = null;

                if (!string.IsNullOrWhiteSpace(attribute?.Name))
                    name = attribute.Name;

                // Prefer parser supplied by parameter attribute
                if (attribute?.Parser != null && typeof(ICommandParameterParser).IsAssignableFrom(attribute.Parser))
                    parser = Activator.CreateInstance(attribute.Parser) as ICommandParameterParser;

                if (parser == null)
                    parser = CreateParameterParser(parameters, i);

                if (parser == null)
                {
                    // The parameter is injected by DI.
                    parameterIndex++;
                    continue;
                }

                var optional = parameter.HasDefaultValue;

                // Don't allow required parameters after optional parameters.
                if (!optional && optionalFound)
                {
                    result = null;
                    return false;
                }

                optionalFound |= optional;

                list.Add(new CommandParameterInfo(name, parser, !optional, parameter.DefaultValue, parameterIndex++));
            }

            result = list.ToArray();
            return true;
        }

        /// <summary>
        /// Extracts a command name from a method.
        /// </summary>
        /// <param name="method">The method to extract the command name from.</param>
        /// <returns>The extracted command name.</returns>
        protected virtual string GetCommandName(MethodInfo method)
        {
            var name = method.Name.ToLowerInvariant();

            if (name.EndsWith("command"))
                name = name.Substring(0, name.Length - 7);

            return name;
        }

        private void CreateCommandsFromAssemblies(int prefixParameters)
        {
            var methods = ScanMethods();

            foreach (var (method, commandInfo) in methods)
            {
                // Determine command name.
                var name = commandInfo.Name ?? GetCommandName(method);
                if (name == null)
                    continue;

                // Validate acceptable return type.
                if (method.ReturnType != typeof(bool) &&
                    method.ReturnType != typeof(int) &&
                    method.ReturnType != typeof(void))
                    continue;

                var methodParameters = method.GetParameters();
                // Determine command parameter types.
                if (!TryCollectParameters(methodParameters, prefixParameters, out var parameters)) 
                    continue;

                var info = new CommandInfo(name, parameters);

                var argsPtr = 0; // The current pointer in the event arguments array.
                var parameterSources = methodParameters
                    .Select(inf => new MethodParameterSource(inf))
                    .ToArray();

                // Determine the source of each parameter.
                for (var i = 0; i < parameterSources.Length; i++)
                {
                    var source = parameterSources[i];
                    var type = source.Info.ParameterType;

                    if (typeof(Component).IsAssignableFrom(type))
                    {
                        // Components are provided by the entity in the arguments array of the event.
                        source.ParameterIndex = argsPtr++;
                        source.IsComponent = true;
                    }
                    else if (info.Parameters.FirstOrDefault(p => p.Index == i) != null)
                    {
                        // Default types are passed straight trough.
                        source.ParameterIndex = argsPtr++;
                    }
                    else
                    {
                        // Other types are provided trough Dependency Injection.
                        source.IsService = true;
                    }
                }

                var data = new CommandData
                {
                    Arguments = new object[info.Parameters.Length + prefixParameters],
                    Info = info,
                    Invoke = MethodInvokerFactory.Compile(method, parameterSources, false),
                    SystemType = method.DeclaringType
                };

                if (!_commands.TryGetValue(info.Name, out var lst))
                    lst = _commands[info.Name] = new List<CommandData>();

                lst.Add(data);
            }
        }

        private class CommandData
        {
            public object[] Arguments;
            public CommandInfo Info;
            public MethodInvoker Invoke;
            public Type SystemType;
        }
    }
}