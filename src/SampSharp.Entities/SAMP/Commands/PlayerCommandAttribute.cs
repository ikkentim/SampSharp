using System;

namespace SampSharp.Entities.SAMP.Commands
{
    /// <summary>
    /// An attribute which indicates the method is invokable as a player command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PlayerCommandAttribute : Attribute, ICommandMethodInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerCommandAttribute"/> class.
        /// </summary>
        public PlayerCommandAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerCommandAttribute"/> class.
        /// </summary>
        /// <param name="name">The overridden name of the command.</param>
        public PlayerCommandAttribute(string name)
        {
            Name = name;
        }

        /// <inheritdoc />
        public string Name { get; set; }
    }
}