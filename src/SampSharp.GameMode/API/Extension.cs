using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampSharp.GameMode.API
{
    public static class Extension
    {
        /// <summary>
        ///     Registers an extension to the plugin.
        /// </summary>
        /// <param name="extension">The extension to register.</param>
        /// <returns>
        ///     True on success, False otherwise.
        /// </returns>
        public static bool Register<T>(T extension) where T : class
        {
            return Interop.RegisterExtension(extension);
        }
    }
}
