// SampSharp
// Copyright (C) 2015 Tim Potze
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
    ///     Represents an word command-parameter.
    /// </summary>
    public class WordAttribute : ParameterAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the WordAttribute class.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        public WordAttribute(string name) : base(name)
        {
        }

        /// <summary>
        ///     Check if the parameter is well-formatted and return the output.
        /// </summary>
        /// <param name="command">The command text.</param>
        /// <param name="output">The output of this parameter.</param>
        /// <returns>True if the parameter is well-formatted, False otherwise.</returns>
        public override bool Check(ref string command, out object output)
        {
            /*
             * A word should at least be one character long.
             */
            if (command.Length == 0)
            {
                output = null;
                return false;
            }

            int idx = command.IndexOf(' ');
            string word = idx == -1 ? command : command.Substring(0, idx);

            output = word;
            command = word == command ? string.Empty : command.Substring(word.Length);

            return true;
        }
    }
}