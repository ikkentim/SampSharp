// SampSharp
// Copyright 2015 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Runtime.CompilerServices;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Natives
{
    public static partial class Native
    {
        /// <summary>
        ///     Creates an object.
        /// </summary>
        /// <param name="modelid">The model you want to use.</param>
        /// <param name="x">The X coordinate to create the object at.</param>
        /// <param name="y">The Y coordinate to create the object at.</param>
        /// <param name="z">The Z coordinate to create the object at.</param>
        /// <param name="rX">The X rotation of the object.</param>
        /// <param name="rY">The Y rotation of the object.</param>
        /// <param name="rZ">The Z rotation of the object.</param>
        /// <param name="drawDistance">
        ///     The distance that San Andreas renders objects at. 0.0 will cause objects to render at their
        ///     default distances.
        /// </param>
        /// <returns>The ID of the object that was created.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreateObject(int modelid, float x, float y, float z, float rX, float rY, float rZ,
            float drawDistance);

        /// <summary>
        ///     Attach an object to a vehicle.
        /// </summary>
        /// <param name="objectid">The ID of the object to attach to the vehicle.</param>
        /// <param name="vehicleid">The ID of the vehicle to attach the object to.</param>
        /// <param name="offsetX">The X axis offset.</param>
        /// <param name="offsetY">The Y axis offset.</param>
        /// <param name="offsetZ">The Z axis offset.</param>
        /// <param name="rotX">The X rotation offset.</param>
        /// <param name="rotY">The Y rotation offset.</param>
        /// <param name="rotZ">The Z rotation offset.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AttachObjectToVehicle(int objectid, int vehicleid, float offsetX, float offsetY,
            float offsetZ, float rotX, float rotY, float rotZ);

        /// <summary>
        ///     You can use this function to attach objects to other objects. The objects will folow the main object.
        /// </summary>
        /// <param name="objectid">The object to attach to another object.</param>
        /// <param name="attachtoid">The object to attach the object to.</param>
        /// <param name="offsetX">The distance between the main object and the object in the X direction.</param>
        /// <param name="offsetY">The distance between the main object and the object in the Y direction.</param>
        /// <param name="offsetZ">The distance between the main object and the object in the Z direction.</param>
        /// <param name="rotX">The X rotation between the object and the main object.</param>
        /// <param name="rotY">The Y rotation between the object and the main object.</param>
        /// <param name="rotZ">The Z rotation between the object and the main object.</param>
        /// <param name="syncRotation">
        ///     If set to false, objects' rotation will not be changed. See ferriswheel filterscript for
        ///     example.
        /// </param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AttachObjectToObject(int objectid, int attachtoid, float offsetX, float offsetY,
            float offsetZ, float rotX, float rotY, float rotZ, bool syncRotation);

        /// <summary>
        ///     Attach an object to a player.
        /// </summary>
        /// <param name="objectid">The ID of the object to attach to the player.</param>
        /// <param name="playerid">The ID of the player to attach the object to.</param>
        /// <param name="offsetX">The distance between the player and the object in the X direction.</param>
        /// <param name="offsetY">The distance between the player and the object in the Y direction.</param>
        /// <param name="offsetZ">The distance between the player and the object in the Z direction.</param>
        /// <param name="rotX">The X rotation between the object and the player.</param>
        /// <param name="rotY">The Y rotation between the object and the player.</param>
        /// <param name="rotZ">The Z rotation between the object and the player.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AttachObjectToPlayer(int objectid, int playerid, float offsetX, float offsetY,
            float offsetZ, float rotX, float rotY, float rotZ);

        /// <summary>
        ///     Change the position of an object.
        /// </summary>
        /// <param name="objectid">The ID of the object to set the position of.</param>
        /// <param name="x">The X coordinate to position the object at.</param>
        /// <param name="y">The Y coordinate to position the object at.</param>
        /// <param name="z">The Z coordinate to position the object at.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetObjectPos(int objectid, float x, float y, float z);

        /// <summary>
        ///     Returns the coordinates of the current position of the given object.
        /// </summary>
        /// <param name="objectid">The object's id of which you want the current location.</param>
        /// <param name="x">The variable to store the X coordinate, passed by reference.</param>
        /// <param name="y">The variable to store the Y coordinate, passed by reference.</param>
        /// <param name="z">The variable to store the Z coordinate, passed by reference.</param>
        /// <returns>The objects position.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetObjectPos(int objectid, out float x, out float y, out float z);

        /// <summary>
        ///     Rotates an object in all directions.
        /// </summary>
        /// <param name="objectid">The objectid of the object you want to rotate.</param>
        /// <param name="rotX">The X rotation.</param>
        /// <param name="rotY">The Y rotation.</param>
        /// <param name="rotZ">The Z rotation.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetObjectRot(int objectid, float rotX, float rotY, float rotZ);

        /// <summary>
        ///     Use this function to get the objects current rotation. The rotation is saved by reference in three RotX/RotY/RotZ
        ///     variables.
        /// </summary>
        /// <param name="objectid">The objectid of the object you want to get the rotation from.</param>
        /// <param name="rotX">The variable to store the X rotation, passed by reference.</param>
        /// <param name="rotY">The variable to store the Y rotation, passed by reference.</param>
        /// <param name="rotZ">The variable to store the Z rotation, passed by reference.</param>
        /// <returns>The objects rotation.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetObjectRot(int objectid, out float rotX, out float rotY, out float rotZ);

        /// <summary>
        ///     Check if the given objectid is valid.
        /// </summary>
        /// <param name="objectid">The objectid to check the validity of.</param>
        /// <returns>True if the object exists, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsValidObject(int objectid);

        /// <summary>
        ///     Destroys (removes) the given object.
        /// </summary>
        /// <param name="objectid">The objectid from the object you want to delete.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DestroyObject(int objectid);

        /// <summary>
        ///     Move an object to a new position with a set speed. Players/vehicles will 'surf' the object as it moves.
        /// </summary>
        /// <param name="objectid">The ID of the object to move.</param>
        /// <param name="x">The X coordinate to move the object to.</param>
        /// <param name="y">The Y coordinate to move the object to.</param>
        /// <param name="z">The Z coordinate to move the object to.</param>
        /// <param name="speed">The speed at which to move the object (units per second).</param>
        /// <param name="rotX">The FINAL X rotation (optional).</param>
        /// <param name="rotY">The FINAL Y rotation (optional).</param>
        /// <param name="rotZ">The FINAL Z rotation (optional).</param>
        /// <returns>The time it will take for the object to move in milliseconds.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int MoveObject(int objectid, float x, float y, float z, float speed, float rotX, float rotY,
            float rotZ);

        /// <summary>
        ///     Stop a moving object after <see cref="MoveObject" /> has been used.
        /// </summary>
        /// <param name="objectid">The ID of the object to stop moving.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool StopObject(int objectid);

        /// <summary>
        ///     Checks if the given objectid is moving.
        /// </summary>
        /// <param name="objectid">The objectid you want to check if is moving.</param>
        /// <returns>True if the object is moving, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsObjectMoving(int objectid);

        /// <summary>
        ///     Allows a player to edit an object (position and rotation) using a GUI (Graphical User Interface).
        /// </summary>
        /// <param name="playerid">The ID of the player that should edit the object.</param>
        /// <param name="objectid">The ID of the object to be edited by the player.</param>
        /// <returns>True on success, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EditObject(int playerid, int objectid);

        /// <summary>
        ///     Let the player edit (move, rotate) the given player object.
        /// </summary>
        /// <param name="playerid">The ID of the player that should edit the object.</param>
        /// <param name="objectid">The object to be edited by the player.</param>
        /// <returns>True on success and 0, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EditPlayerObject(int playerid, int objectid);

        /// <summary>
        ///     Display the cursor and allow the player to select an object. <see cref="BaseMode.OnPlayerSelectObject" /> is called
        ///     when the player selects an object.
        /// </summary>
        /// <param name="playerid">The ID of the player that should be able to select the object.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SelectObject(int playerid);

        /// <summary>
        ///     Cancel object edition mode for a player.
        /// </summary>
        /// <param name="playerid">The ID of the player to cancel edition for.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool CancelEdit(int playerid);

        /// <summary>
        ///     Creates an object which will be visible to only one player.
        /// </summary>
        /// <param name="playerid">The ID of the player to create the object for.</param>
        /// <param name="modelid">The model to create.</param>
        /// <param name="x">The X coordinate to create the object at.</param>
        /// <param name="y">The Y coordinate to create the object at.</param>
        /// <param name="z">The Z coordinate to create the object at.</param>
        /// <param name="rX">The X rotation of the object.</param>
        /// <param name="rY">The Y rotation of the object.</param>
        /// <param name="rZ">The Z rotation of the object.</param>
        /// <param name="drawDistance">
        ///     The distance from which objects will appear to players. 0.0 will cause an object to render
        ///     at its default distance. Leaving this parameter out will cause objects to be rendered at their default distance.
        /// </param>
        /// <returns>The ID of the object that was created, or INVALID_OBJECT_ID if the object limit (MAX_OBJECTS) was reached.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreatePlayerObject(int playerid, int modelid, float x, float y, float z, float rX,
            float rY, float rZ, float drawDistance);

        /// <summary>
        ///     The same as <see cref="AttachObjectToPlayer" /> but for objects which were created for
        ///     player.
        /// </summary>
        /// <param name="objectplayer">The id of the player which is linked with the object.</param>
        /// <param name="objectid">The objectid you want to attach to the player.</param>
        /// <param name="attachplayerid">The id of the player you want to attach to the object.</param>
        /// <param name="offsetX">The distance between the player and the object in the X direction.</param>
        /// <param name="offsetY">The distance between the player and the object in the Y direction.</param>
        /// <param name="offsetZ">The distance between the player and the object in the Z direction.</param>
        /// <param name="rX">The X rotation.</param>
        /// <param name="rY">The Y rotation.</param>
        /// <param name="rZ">The Z rotation.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AttachPlayerObjectToPlayer(int objectplayer, int objectid, int attachplayerid,
            float offsetX, float offsetY, float offsetZ, float rX, float rY, float rZ);

        /// <summary>
        ///     Attach a player object to a vehicle.
        /// </summary>
        /// <param name="playerid">The ID of the player the object was created for.</param>
        /// <param name="objectid">The ID of the object to attach to the vehicle.</param>
        /// <param name="vehicleid">The ID of the vehicle to attach the object to.</param>
        /// <param name="offsetX">The X position offset for attachment.</param>
        /// <param name="offsetY">The Y position offset for attachment.</param>
        /// <param name="offsetZ">The Z position offset for attachment.</param>
        /// <param name="rotX">The X rotation offset for attachment.</param>
        /// <param name="rotY">The Y rotation offset for attachment.</param>
        /// <param name="rotZ">The Z rotation offset for attachment.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AttachPlayerObjectToVehicle(int playerid, int objectid, int vehicleid, float offsetX,
            float offsetY, float offsetZ, float rotX, float rotY, float rotZ);

        /// <summary>
        ///     Sets the position of a player-object to the specified coordinates.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player-object to set the position of.</param>
        /// <param name="objectid">
        ///     The ID of the player-object to set the position of. Returned by
        ///     <see cref="CreatePlayerObject" />.
        /// </param>
        /// <param name="x">The X coordinate to put the object at.</param>
        /// <param name="y">The Y coordinate to put the object at.</param>
        /// <param name="z">The Z coordinate to put the object at.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerObjectPos(int playerid, int objectid, float x, float y, float z);

        /// <summary>
        ///     Returns the coordinates of the current position of the given object. The position is saved by reference in three
        ///     x/y/z variables.
        /// </summary>
        /// <param name="playerid">The player you associated this object to.</param>
        /// <param name="objectid">The object's id of which you want the current location.</param>
        /// <param name="x">The variable to store the X coordinate, passed by reference.</param>
        /// <param name="y">The variable to store the Y coordinate, passed by reference.</param>
        /// <param name="z">The variable to store the Z coordinate, passed by reference.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerObjectPos(int playerid, int objectid, out float x, out float y, out float z);

        /// <summary>
        ///     Rotates an object in all directions.
        /// </summary>
        /// <param name="playerid">The player you associated this object to.</param>
        /// <param name="objectid">The objectid of the object you want to rotate.</param>
        /// <param name="rotX">The X rotation.</param>
        /// <param name="rotY">The Y rotation.</param>
        /// <param name="rotZ">The Z rotation.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerObjectRot(int playerid, int objectid, float rotX, float rotY, float rotZ);

        /// <summary>
        ///     Use this function to get the object' s current rotation. The rotation is saved by reference in three RotX/RotY/RotZ
        ///     variables.
        /// </summary>
        /// <param name="playerid">The player you associated this object to.</param>
        /// <param name="objectid">The objectid of the object you want to get the rotation from.</param>
        /// <param name="rotX">The variable to store the X rotation, passed by reference.</param>
        /// <param name="rotY">The variable to store the Y rotation, passed by reference.</param>
        /// <param name="rotZ">The variable to store the Z rotation, passed by reference.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerObjectRot(int playerid, int objectid, out float rotX, out float rotY,
            out float rotZ);

        /// <summary>
        ///     Checks if the given objectid is valid for the given player.
        /// </summary>
        /// <param name="playerid">The player you associated this object to.</param>
        /// <param name="objectid">The objectid you want to validate.</param>
        /// <returns>True if the object exists, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsValidPlayerObject(int playerid, int objectid);

        /// <summary>
        ///     Destroy a player-object.
        /// </summary>
        /// <param name="playerid">The ID of the player the object is associated to.</param>
        /// <param name="objectid">
        ///     The ID of the player-object to delete (returned by
        ///     <see cref="CreatePlayerObject" />).
        /// </param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DestroyPlayerObject(int playerid, int objectid);

        /// <summary>
        ///     Move an object with a set speed. Also supports rotation. Players/vehicles will surf moving objects.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player-object to move.</param>
        /// <param name="objectid">The ID of the object to move.</param>
        /// <param name="x">The X coordinate to move the object to.</param>
        /// <param name="y">The Y coordinate to move the object to.</param>
        /// <param name="z">The Z coordinate to move the object to.</param>
        /// <param name="speed">The speed at which to move the object.</param>
        /// <param name="rotX">The final X rotation (optional).</param>
        /// <param name="rotY">The final Y rotation (optional).</param>
        /// <param name="rotZ">The final Z rotation (optional).</param>
        /// <returns>The time it will take for the object to move in milliseconds.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int MovePlayerObject(int playerid, int objectid, float x, float y, float z, float speed,
            float rotX, float rotY, float rotZ);

        /// <summary>
        ///     Stop a moving player-object after <see cref="MovePlayerObject" /> has been used.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player-object to stop.</param>
        /// <param name="objectid">The ID of the player-object to stop.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool StopPlayerObject(int playerid, int objectid);

        /// <summary>
        ///     Checks if the given player objectid is moving.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player-object you want to theck if is moving.</param>
        /// <param name="objectid">The player objectid you want to check if is moving.</param>
        /// <returns>True if the player object is moving, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerObjectMoving(int playerid, int objectid);

        /// <summary>
        ///     Replace the texture of an object with the texture from another model in the game.
        /// </summary>
        /// <param name="objectid">The ID of the object to change the texture of.</param>
        /// <param name="materialindex">The material index on the object to change.</param>
        /// <param name="modelid">
        ///     The modelid on which the replacement texture is located. Use 0 for alpha. Use -1 to change the
        ///     material color without altering the texture.
        /// </param>
        /// <param name="txdname">The name of the txd file which contains the replacement texture (use "none" if not required).</param>
        /// <param name="texturename">The name of the texture to use as the replacement (use "none" if not required).</param>
        /// <param name="materialcolor">
        ///     The object color to set, as an integer or hex in ARGB color format. Using 0 keeps the
        ///     existing material color.
        /// </param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetObjectMaterial(int objectid, int materialindex, int modelid, string txdname,
            string texturename, int materialcolor);

        /// <summary>
        ///     Replace the texture of a player-object with the texture from another model in the game.
        /// </summary>
        /// <param name="playerid">The ID of the player the object is associated to.</param>
        /// <param name="objectid">The ID of the object to replace the texture of.</param>
        /// <param name="materialindex">The material index on the object to change.</param>
        /// <param name="modelid">
        ///     The modelid on which replacement texture is located. Use 0 for alpha. Use -1 to change the
        ///     material color without altering the existing texture.
        /// </param>
        /// <param name="txdname">The name of the txd file which contains the replacement texture (use "none" if not required).</param>
        /// <param name="texturename">The name of the texture to use as the replacement (use "none" if not required).</param>
        /// <param name="materialcolor">
        ///     The object color to set, as an integer or hex in ARGB format. Using 0 keeps the existing
        ///     material color.
        /// </param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerObjectMaterial(int playerid, int objectid, int materialindex, int modelid,
            string txdname, string texturename, int materialcolor);

        /// <summary>
        ///     Replace the texture of an object with text.
        /// </summary>
        /// <param name="objectid">The ID of the object to replace the texture of with text.</param>
        /// <param name="text">The text to show on the object.</param>
        /// <param name="materialindex">The object's material index to replace with text.</param>
        /// <param name="materialsize">The size of the material.</param>
        /// <param name="fontface">The font to use.</param>
        /// <param name="fontsize">The size of the text (MAX 255).</param>
        /// <param name="bold">Bold text. Set to True for bold, False for not.</param>
        /// <param name="fontcolor">The color of the text, in ARGB format.</param>
        /// <param name="backcolor">The background color, in ARGB format.</param>
        /// <param name="textalignment">The alignment of the text (default: left).</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetObjectMaterialText(int objectid, string text, int materialindex, int materialsize,
            string fontface, int fontsize, bool bold, int fontcolor, int backcolor, int textalignment);

        /// <summary>
        ///     Replace the texture of a player object with text.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player object to set the text of.</param>
        /// <param name="objectid">The ID of the object on which to place the text.</param>
        /// <param name="text">The text to set.</param>
        /// <param name="materialindex">The material index to replace with text (DEFAULT: 0).</param>
        /// <param name="materialsize">The size of the material (DEFAULT: 256x128).</param>
        /// <param name="fontface">The font to use (DEFAULT: Arial).</param>
        /// <param name="fontsize">The size of the text (DEFAULT: 24) (MAX 255).</param>
        /// <param name="bold">Bold text. Set to True for bold, False for not (DEFAULT: True).</param>
        /// <param name="fontcolor">The color of the text (DEFAULT: White).</param>
        /// <param name="backcolor">The background color (DEFAULT: None (transparent)).</param>
        /// <param name="textalignment">The alignment of the text (DEFAULT: Left).</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerObjectMaterialText(int playerid, int objectid, string text, int materialindex,
            int materialsize, string fontface, int fontsize, bool bold, int fontcolor, int backcolor, int textalignment);
    }
}