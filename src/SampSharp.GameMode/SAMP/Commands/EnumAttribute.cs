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
using System.Linq;
using SampSharp.GameMode.Helpers;

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     Represents an enum command-parameter.
    /// </summary>
    public class EnumAttribute : WordAttribute
    {
        public EnumAttribute(string name, Type type) : base(name)
        {
            if (!type.IsEnum)
            {
                throw new ArgumentException("type is not an enumerator");
            }

            Type = type;
        }

        /// <summary>
        ///     Gets the enum type in which to look for values.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        ///     Gets or sets whether input should be matches against the enum values.
        ///     When False, the input will only be matches agains the names.
        /// </summary>
        public bool TestForValue { get; set; }

        /// <summary>
        ///     Check if the parameter is well-formatted and return the output.
        /// </summary>
        /// <param name="command">The command text.</param>
        /// <param name="output">The output of this parameter.</param>
        /// <returns>True if the parameter is well-formatted, False otherwise.</returns>
        public override bool Check(ref string command, out object output)
        {
            if (!base.Check(ref command, out output))
                return false;

            var word = (output as string).ToLower();

            /*
             * Find an enum value that contains the given word and select its index.
             */
            var names = Type.GetEnumNames();
            var values = Type.GetEnumValues();
            var results =
                names.Where(
                    (e, i) => e.ToLower().Contains(word) || (TestForValue && values.GetValue(i).ToString() == word));

            if (results.Count() > 1)
            {
                results = results.Where(e => e.ToLower() == word);
            }
            if (results.Count() == 1)
            {
                output = values.GetValue(Type.GetEnumNames().IndexOf(results.First()));
                return true;
            }

            output = null;
            return false;
        }
    }
}