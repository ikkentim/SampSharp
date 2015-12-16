using System;

namespace SampSharp.GameMode.SAMP.Commands.Arguments
{
    public class CommandParameterInfo
    {
        public ICommandParameterType CommandParameterType { get; }
        public bool IsOptional { get; }
        public string Name { get; }
        public object DefaultValue { get;  }
        
        public CommandParameterInfo(string name, ICommandParameterType commandParameterType, bool isOptional,
            object defaultValue)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (commandParameterType == null) throw new ArgumentNullException(nameof(commandParameterType));

            CommandParameterType = commandParameterType;
            IsOptional = isOptional;
            Name = name;
            DefaultValue = defaultValue;
        }
    }
}