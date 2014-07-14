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

using System;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Natives;

namespace SampSharp.Streamer
{
    public class Streamer
    {
        public static void LoadControllers(ControllerCollection controllers)
        {
            Native.RegisterExtension(new Streamer());
        }

        public void OnDynamicObjectMoved(int objectid)
        {
        }

        public void OnPlayerEditDynamicObject(int playerid, int objectid, int response, float x, float y, float z,
            float rx, float ry, float rz)
        {
        }

        public void OnPlayerSelectDynamicObject(int playerid, int objectid, int modelid, float x, float y, float z)
        {
        }

        public void OnPlayerShootDynamicObject(int playerid, int weaponid, int objectid, float x, float y, float z)
        {
        }

        public void OnPlayerPickUpDynamicPickup(int playerid, int pickupid)
        {
        }

        public void OnPlayerEnterDynamicCP(int playerid, int checkpointid)
        {
        }

        public void OnPlayerLeaveDynamicCP(int playerid, int checkpointid)
        {
        }

        public void OnPlayerEnterDynamicRaceCP(int playerid, int checkpointid)
        {
        }

        public void OnPlayerLeaveDynamicRaceCP(int playerid, int checkpointid)
        {
        }

        public void OnPlayerEnterDynamicArea(int playerid, int areaid)
        {
        }

        public void OnPlayerLeaveDynamicArea(int playerid, int areaid)
        {
        }
    }
}