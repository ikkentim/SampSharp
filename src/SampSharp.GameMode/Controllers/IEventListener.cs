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
    ///     Provides the functionality for an <see cref="IController" /> to act on events.
    /// </summary>
    public interface IEventListener : IController
    {
        /// <summary>
        ///     Registers the events this <see cref="IEventListener" /> wants to listen to.
        /// </summary>
        /// <param name="gameMode">An instance of the <see cref="BaseMode" /> currently running.</param>
        void RegisterEvents(BaseMode gameMode);
    }
}