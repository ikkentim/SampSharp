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

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     Provides the functionality for a controller to act on events.
    /// </summary>
    public interface IController
    {
        /// <summary>
        ///     Registers the events this IController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        void RegisterEvents(BaseMode gameMode);

        /// <summary>
        ///     Registers types this IController requires the system to use.
        /// </summary>
        void RegisterTypes();
    }
}