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

using System;

namespace SampSharp.GameMode
{
    /// <summary>
    ///     Contains an index property for defining which parameter of a callback contains the length of the parameter this
    ///     attribute is attached to.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ParameterLengthAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the ParameterLengthAttribute class.
        /// </summary>
        /// <param name="index">The index of the parameter which contains the length of the paramter this attribute is attached to.</param>
        public ParameterLengthAttribute(int index)
        {
            Index = index;
        }

        /// <summary>
        ///     Gets the index of the parameter which contains the length of the paramter this attribute is attached to.
        /// </summary>
        public int Index { get; private set; }
    }
}