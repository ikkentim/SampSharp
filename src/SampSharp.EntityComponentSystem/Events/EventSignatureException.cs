using System;
using System.Runtime.Serialization;

namespace SampSharp.EntityComponentSystem.Events
{
    [Serializable]
    public class EventSignatureException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public EventSignatureException()
        {
        }

        public EventSignatureException(string message) : base(message)
        {
        }

        public EventSignatureException(string message, Exception inner) : base(message, inner)
        {
        }

        protected EventSignatureException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}