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
    ///     Represents an integer command-parameter.
    /// </summary>
    public class IntegerParameter : WordParameter
    {
        /// <summary>
        ///     Initializes a new instance of the IntegerParameter class.
        /// </summary>
        public IntegerParameter()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the IntegerParameter class.
        /// </summary>
        /// <param name="name">The name of this parameter.</param>
        public IntegerParameter(string name) : base(name)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the IntegerParameter class.
        /// </summary>
        /// <param name="name">The name of this parameter.</param>
        /// <param name="optional">Whether this parameter is optional.</param>
        public IntegerParameter(string name, bool optional)
            : base(name, optional)
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
            //Get word from command
            if (!base.Check(ref command, out output))
                return false;

            string word = (string) output;

            //Try to parse this number
            int number;
            if (!int.TryParse(word, out number))
            {
                //Clear output
                output = null;
                return false;
            }

            //Set output
            output = number;

            return true;
        }
    }
}