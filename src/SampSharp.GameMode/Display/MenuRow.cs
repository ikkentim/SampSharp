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

namespace SampSharp.GameMode.Display
{
    /// <summary>
    ///     Represents a row in a <see cref="Menu" />.
    /// </summary>
    public class MenuRow
    {
        /// <summary>
        ///     Initializes a new instance of the MenuRow class.
        /// </summary>
        /// <param name="col1Text">The text in the first column.</param>
        /// <param name="disabled">Whether this row should be disabled.</param>
        public MenuRow(string col1Text, bool disabled = false)
        {
            Text = new[] {col1Text};
            Disabled = disabled;
        }

        /// <summary>
        ///     Initializes a new instance of the MenuRow class.
        /// </summary>
        /// <param name="col1Text">The text in the first column.</param>
        /// <param name="col2Text">The text in the second column.</param>
        /// <param name="disabled">Whether this row should be disabled.</param>
        public MenuRow(string col1Text, string col2Text, bool disabled = false)
        {
            Text = new[] {col1Text, col2Text};
            Disabled = disabled;
        }

        /// <summary>
        ///     Gets or sets the text displayed in this row on each column.
        /// </summary>
        public string[] Text { get; set; }

        /// <summary>
        ///     Gets or sets whether this row is disabled.
        /// </summary>
        public bool Disabled { get; set; }
    }
}