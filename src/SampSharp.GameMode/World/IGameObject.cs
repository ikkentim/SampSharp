﻿// SampSharp
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

using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;

namespace SampSharp.GameMode.World
{
    /// <summary>
    /// Contains methods and properties for accessing a SA-MP object of any type.
    /// </summary>
    public interface IGameObject : IWorldObject
    {
        /// <summary>
        /// Gets whether this IGameObject is moving.
        /// </summary>
        bool IsMoving { get; }

        /// <summary>
        /// Gets whether this IGameObject is valid.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Gets the model of this IGameObject.
        /// </summary>
        int ModelId { get; }

        /// <summary>
        /// Gets the draw distance of this IGameObject.
        /// </summary>
        float DrawDistance { get; }

        /// <summary>
        /// Gets the rotation of this IGameObject.
        /// </summary>
        Vector Rotation { get; set; }

        /// <summary>
        /// Moves this IGameObject to the given position and rotation with the given speed.
        /// </summary>
        /// <param name="position">The position to which to move this IGameObject.</param>
        /// <param name="speed">The speed at which to move this IGameObject.</param>
        /// <param name="rotation">The rotation to which to move this IGameObject.</param>
        /// <returns>The time it will take for the object to move in milliseconds.</returns>
        int Move(Vector position, float speed, Vector rotation);
        /// <summary>
        /// Moves this IGameObject to the given position with the given speed.
        /// </summary>
        /// <param name="position">The position to which to move this IGameObject.</param>
        /// <param name="speed">The speed at which to move this IGameObject.</param>
        /// <returns>The time it will take for the object to move in milliseconds.</returns>
        int Move(Vector position, float speed);

        /// <summary>
        /// Stop this IGameObject from moving any further.
        /// </summary>
        void Stop();
        /// <summary>
        /// Sets the material of this IGameObject.
        /// </summary>
        /// <param name="materialindex">The material index on the object to change.</param>
        /// <param name="modelid">The modelid on which the replacement texture is located. Use 0 for alpha. Use -1 to change the material color without altering the texture.</param>
        /// <param name="txdname">The name of the txd file which contains the replacement texture (use "none" if not required).</param>
        /// <param name="texturename">The name of the texture to use as the replacement (use "none" if not required).</param>
        /// <param name="materialcolor">The object color to set (use default(Color) to keep the existing material color).</param>
        void SetMaterial(int materialindex, int modelid, string txdname, string texturename, Color materialcolor);

        /// <summary>
        /// Sets the material text of this IGameObject. 
        /// </summary>
        /// <param name="materialindex">The material index on the object to change.</param>
        /// <param name="text">The text to show on the object. (MAX 2048 characters)</param>
        /// <param name="materialsize">The object's material index to replace with text.</param>
        /// <param name="fontface">The font to use.</param>
        /// <param name="fontsize">The size of the text (max 255).</param>
        /// <param name="bold">Whether to write in bold.</param>
        /// <param name="foreColor">The color of the text.</param>
        /// <param name="backColor">The background color of the text.</param>
        /// <param name="textalignment">The alignment of the text.</param>
        void SetMaterialText(int materialindex, string text, ObjectMaterialSize materialsize, string fontface,
            int fontsize, bool bold, Color foreColor, Color backColor, ObjectMaterialTextAlign textalignment);
    }
}