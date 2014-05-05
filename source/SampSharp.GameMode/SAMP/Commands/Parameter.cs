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

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     Represents a command-parameter.
    /// </summary>
    public abstract class Parameter
    {
        /// <summary>
        ///     Initializes a new instance of the Parameter class.
        /// </summary>
        protected Parameter() : this(string.Empty, false)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the Parameter class.
        /// </summary>
        /// <param name="name">The name of this parameter.</param>
        protected Parameter(string name)
            : this(name, false)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the Parameter class.
        /// </summary>
        /// <param name="name">The name of this parameter.</param>
        /// <param name="optional">Whether this parameter is optional.</param>
        protected Parameter(string name, bool optional)
        {
            Name = name;
            Optional = optional;
        }

        /// <summary>
        ///     Get the name of this parameter.
        /// </summary>
        public virtual string Name { get; private set; }

        /// <summary>
        ///     Get whether this parameter is optional.
        /// </summary>
        public virtual bool Optional { get; private set; }

        /// <summary>
        ///     Check if the parameter is well-formatted and return the output.
        /// </summary>
        /// <param name="command">The command text.</param>
        /// <param name="output">The output of this parameter.</param>
        /// <returns>True if the parameter is well-formatted, False otherwise.</returns>
        public abstract bool Check(ref string command, out object output);
    }
}