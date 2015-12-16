using System;

namespace SampSharp.GameMode.SAMP.Commands
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class CommandGroupAttribute : Attribute
    {
        public string[] Paths { get; set; }

        public CommandGroupAttribute(params string[] paths)
        {
            Paths = paths;
        }

        public IPermissionChecker PermissionChecker { get; set; }
    }
}