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

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     Contains a Group property to group a command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandGroupAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the CommandGroupAttribute class.
        /// </summary>
        /// <param name="group">The group in which this command lives.</param>
        public CommandGroupAttribute(params string[] group)
        {
            Group = string.Join(" ", group);
        }

        /// <summary>
        ///     Initializes a new instance of the CommandGroupAttribute class.
        /// </summary>
        /// <param name="group">The group in which this command lives.</param>
        public CommandGroupAttribute(string group)
        {
            Group = group;
        }

        /// <summary>
        ///     The group in which this command lives.
        /// </summary>
        public string Group { get; set; }
    }
}