using System;
using System.Runtime.Serialization;

namespace SampSharp.EntityComponentSystem.Entities
{
    [Serializable]
    public class EntityCreationException : Exception
    {
        public EntityCreationException()
        {
        }

        public EntityCreationException(string message) : base(message)
        {
        }

        public EntityCreationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected EntityCreationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}