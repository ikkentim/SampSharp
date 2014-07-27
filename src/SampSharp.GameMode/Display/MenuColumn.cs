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

namespace SampSharp.GameMode.Display
{
    /// <summary>
    ///     Represents a column in a <see cref="Menu" />.
    /// </summary>
    public class MenuColumn
    {
        /// <summary>
        ///     Initializes a new instance of the MenuColumn class.
        /// </summary>
        /// <param name="caption">The caption of the column.</param>
        /// <param name="width">The width of the column.</param>
        public MenuColumn(string caption, float width)
        {
            Caption = caption;
            Width = width;
        }

        /// <summary>
        ///     Initializes a new instance of the menuColumn structure.
        /// </summary>
        /// <param name="width">The width of the column.</param>
        public MenuColumn(float width)
        {
            Width = width;
        }

        /// <summary>
        ///     Gets or sets the caption of this column.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        ///     Gets ro sets the width if this column.
        /// </summary>
        public float Width { get; set; }
    }
}