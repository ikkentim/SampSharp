using System;

namespace SampSharp.GameMode.SAMP.Commands
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        public CommandAttribute(string name) : this(new[] {name})
        {
        }

        public CommandAttribute(params string[] names)
        {
            Names = names;
            IgnoreCase = true;
        }
        
        public string[] Names { get; }

        public bool IgnoreCase { get; set; }

        public string Shortcut { get; set; }

        public string DisplayName { get; set; }
        public string UsageMessage { get; set; }

        public IPermissionChecker PermissionChecker { get; set; }
    }
}
