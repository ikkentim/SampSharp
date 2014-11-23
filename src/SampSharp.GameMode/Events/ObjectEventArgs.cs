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

using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.ObjectMoved" /> event.
    /// </summary>
    public class ObjectEventArgs : GameModeEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the ObjectEventArgs class.
        /// </summary>
        /// <param name="objectid">Id of the object.</param>
        public ObjectEventArgs(int objectid)
        {
            ObjectId = objectid;
        }

        /// <summary>
        ///     Gets the id of the object.
        /// </summary>
        public int ObjectId { get; private set; }

        /// <summary>
        ///     Gets the object.
        /// </summary>
        public GlobalObject GlobalObject
        {
            get { return ObjectId == GlobalObject.InvalidId ? null : GlobalObject.FindOrCreate(ObjectId); }
        }
    }
}