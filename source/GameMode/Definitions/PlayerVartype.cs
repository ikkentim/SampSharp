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

namespace GameMode.Definitions
{
    /// <summary>
    ///     Contains all playervar types.
    /// </summary>
    public enum PlayerVarType
    {
        /// <summary>
        ///     Var does not exist.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Var as an integer.
        /// </summary>
        Int = 1,

        /// <summary>
        ///     Var is a string.
        /// </summary>
        String = 2,

        /// <summary>
        ///     Var is a float.
        /// </summary>
        Float = 3
    }
}