using System.Runtime.CompilerServices;

namespace GameMode
{
    public static partial class Native
    {
        /// <summary>
        /// Prints a message to the serverlog.
        /// </summary>
        /// <param name="msg">The message to print to the serverlog.</param>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void Print(string msg);
    }
}
