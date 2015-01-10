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

namespace SampSharp.GameMode.Definitions
{
    /// <summary>
    ///     Contains all dialogstyles.
    /// </summary>
    public enum DialogStyle
    {
        /// <summary>
        ///     A box with a caption, text and one or two buttons.
        /// </summary>
        MessageBox = 0,

        /// <summary>
        ///     A box with a caption, text, an inputbox and one or two buttons.
        /// </summary>
        Input = 1,

        /// <summary>
        ///     A box with a caption, a bunch of selectable items and one or two buttons.
        /// </summary>
        List = 2,

        /// <summary>
        ///     A box with a caption, text, an password-inputbox and one or two buttons.
        /// </summary>
        Password = 3
    }
}