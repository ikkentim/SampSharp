// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using System;

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     Represents a command-parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public abstract class ParameterAttribute : Attribute
    {
        protected ParameterAttribute(string name)
        {
            Name = name;
            DisplayName = name;
        }

        /// <summary>
        ///     Gets the name of this parameter.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Gets or sets the displayname of this parameter.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     Gets or sets whether this parameter is optional.
        /// </summary>
        public bool Optional { get; set; }

        /// <summary>
        ///     Check if the parameter is well-formatted and return the output.
        /// </summary>
        /// <param name="command">The command text.</param>
        /// <param name="output">The output of this parameter.</param>
        /// <returns>True if the parameter is well-formatted, False otherwise.</returns>
        public abstract bool Check(ref string command, out object output);
    }
}