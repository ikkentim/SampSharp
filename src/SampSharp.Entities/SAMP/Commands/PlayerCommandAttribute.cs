using System;
using SampSharp.Entities.Annotations;

namespace SampSharp.Entities.SAMP.Commands
{
    /// <summary>
    /// An attribute which indicates the method is invokable as a player command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    [MeansImplicitUse]
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
        public PlayerCommandAttribute(string name) : this(new[] { name })
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerCommandAttribute"/> class.
        /// </summary>
        /// <param name="names">The overridden names of the command.</param>      
        public PlayerCommandAttribute(params string[] names)
        {
            Names = names;
            IgnoreCase = true;
        }

        /// <inheritdoc />
        public string[] Names { get; set; }

        /// <inheritdoc />
        public bool IgnoreCase { get; set; }
    }
}