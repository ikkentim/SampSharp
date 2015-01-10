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

using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerEditAttachedObject" /> event.
    /// </summary>
    public class PlayerEditAttachedObjectEventArgs : PlayerEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the PlayerEditAttachedObjectEventArgs class.
        /// </summary>
        /// <param name="playerid">Id of the player.</param>
        /// <param name="response">EditObjectResponse.</param>
        /// <param name="index">Index of the attached object.</param>
        /// <param name="modelid">Model of the attached object.</param>
        /// <param name="boneid">Id of the bone the object was attached to.</param>
        /// <param name="offset">Offset of the attached object.</param>
        /// <param name="rotation">Rotation of the attached object.</param>
        /// <param name="scale">Scale of the attached object.</param>
        public PlayerEditAttachedObjectEventArgs(int playerid, EditObjectResponse response, int index, int modelid,
            int boneid, Vector offset, Vector rotation, Vector scale)
            : base(playerid)
        {
            EditObjectResponse = response;
            Index = index;
            ModelId = modelid;
            BoneId = boneid;
            Offset = offset;
            Rotation = rotation;
            Scale = scale;
        }

        /// <summary>
        ///     Gets the EditObjectResponse.
        /// </summary>
        public EditObjectResponse EditObjectResponse { get; private set; }

        /// <summary>
        ///     Gets the index of the attachedobject.
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        ///     Gets the id of the model.
        /// </summary>
        public int ModelId { get; private set; }

        /// <summary>
        ///     Gets the boneid the object was attached to.
        /// </summary>
        public int BoneId { get; private set; }

        /// <summary>
        ///     Gets the offset of the attached object.
        /// </summary>
        public Vector Offset { get; private set; }

        /// <summary>
        ///     Gets the rotation of the attached object.
        /// </summary>
        public Vector Rotation { get; private set; }

        /// <summary>
        ///     Gets the scale of the attached object.
        /// </summary>
        public Vector Scale { get; private set; }
    }
}