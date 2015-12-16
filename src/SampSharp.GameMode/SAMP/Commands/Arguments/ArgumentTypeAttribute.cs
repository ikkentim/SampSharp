using System;

namespace SampSharp.GameMode.SAMP.Commands.Arguments
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ArgumentTypeAttribute : Attribute
    {
        public Type Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Attribute"/> class.
        /// </summary>
        public ArgumentTypeAttribute(Type type)
        {
            Type = type;
        }
    }
}