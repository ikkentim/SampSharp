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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;

namespace SampSharp.Streamer.Natives
{
    public static partial class StreamerNative
    {
        //
        //TODO: All length refs are probably 1-off.
        //
        public static int CreateDynamicObjectEx(int modelid, float x, float y, float z, float rx, float ry, float rz,
            float drawdistance = 0.0f, float streamdistance = 200.0f, int[] worlds = null, int[] interiors = null,
            int[] players = null, int maxworlds = -1, int maxinteriors = -1, int maxplayers = -1)
        {
            //Check defaults
            if (worlds == null) worlds = new[] {-1};
            if (interiors == null) interiors = new[] {-1};
            if (players == null) players = new[] {-1};

            if (maxworlds < 0) maxworlds = worlds.Length;
            if (maxinteriors < 0) maxinteriors = interiors.Length;
            if (maxplayers < 0) maxplayers = players.Length;

            return Native.CallNative("CreateDynamicObjectEx", new[] {13, 14, 15},
                __arglist(
                    modelid, x, y, z, rx, ry, rz, drawdistance, streamdistance, worlds, interiors, players, maxworlds,
                    maxinteriors, maxplayers));
        }

        public static int CreateDynamicPickupEx(int modelid, int type, float x, float y, float z,
            float streamdistance = 100.0f, int[] worlds = null, int[] interiors = null, int[] players = null,
            int maxworlds = -1, int maxinteriors = -1, int maxplayers = -1)
        {
            //Check defaults
            if (worlds == null) worlds = new[] {-1};
            if (interiors == null) interiors = new[] {-1};
            if (players == null) players = new[] {-1};

            if (maxworlds < 0) maxworlds = worlds.Length;
            if (maxinteriors < 0) maxinteriors = interiors.Length;
            if (maxplayers < 0) maxplayers = players.Length;

            return Native.CallNative("CreateDynamicPickupEx", new[] {10, 11, 12},
                __arglist(
                    modelid, type, x, y, z, streamdistance, worlds, interiors, players, maxworlds, maxinteriors,
                    maxplayers));
        }

        public static int CreateDynamicCPEx(float x, float y, float z, float size, float streamdistance = 100.0f,
            int[] worlds = null, int[] interiors = null, int[] players = null, int maxworlds = -1, int maxinteriors = -1,
            int maxplayers = -1)
        {
            //Check defaults
            if (worlds == null) worlds = new[] {-1};
            if (interiors == null) interiors = new[] {-1};
            if (players == null) players = new[] {-1};

            if (maxworlds < 0) maxworlds = worlds.Length;
            if (maxinteriors < 0) maxinteriors = interiors.Length;
            if (maxplayers < 0) maxplayers = players.Length;

            return Native.CallNative("CreateDynamicCPEx", new[] {9, 10, 11},
                __arglist(x, y, z, size, streamdistance, worlds, interiors, players, maxworlds, maxinteriors, maxplayers
                    ));
        }

        public static int CreateDynamicRaceCPEx(int type, float x, float y, float z, float nextx, float nexty,
            float nextz, float size, float streamdistance = 100.0f, int[] worlds = null, int[] interiors = null,
            int[] players = null, int maxworlds = -1, int maxinteriors = -1, int maxplayers = -1)
        {
            //Check defaults
            if (worlds == null) worlds = new[] {-1};
            if (interiors == null) interiors = new[] {-1};
            if (players == null) players = new[] {-1};

            if (maxworlds < 0) maxworlds = worlds.Length;
            if (maxinteriors < 0) maxinteriors = interiors.Length;
            if (maxplayers < 0) maxplayers = players.Length;

            return Native.CallNative("CreateDynamicRaceCPEx", new[] {13, 14, 15},
                __arglist(
                    type, x, y, z, nextx, nexty, nextz, size, streamdistance, worlds, interiors, players, maxworlds,
                    maxinteriors, maxplayers));
        }

        public static int CreateDynamicMapIconEx(float x, float y, float z, int type, Color color,
            MapIconType style = MapIconType.Local, float streamdistance = 100.0f, int[] worlds = null,
            int[] interiors = null, int[] players = null, int maxworlds = -1, int maxinteriors = -1, int maxplayers = -1)
        {
            //Check defaults
            if (worlds == null) worlds = new[] {-1};
            if (interiors == null) interiors = new[] {-1};
            if (players == null) players = new[] {-1};

            if (maxworlds < 0) maxworlds = worlds.Length;
            if (maxinteriors < 0) maxinteriors = interiors.Length;
            if (maxplayers < 0) maxplayers = players.Length;

            return Native.CallNative("CreateDynamicMapIconEx", new[] {11, 12, 13},
                __arglist(
                    x, y, z, type, (int) color, (int) style, streamdistance, worlds, interiors, players, maxworlds,
                    maxinteriors, maxplayers));
        }

        public static int CreateDynamic3DTextLabelEx(string text, Color color, float x, float y, float z,
            float drawdistance, int attachedplayer = Player.InvalidId, int attachedvehicle = Vehicle.InvalidId,
            bool testlos = false, float streamdistance = 100.0f, int[] worlds = null, int[] interiors = null,
            int[] players = null, int maxworlds = -1, int maxinteriors = -1, int maxplayers = -1)
        {
            //Check defaults
            if (worlds == null) worlds = new[] {-1};
            if (interiors == null) interiors = new[] {-1};
            if (players == null) players = new[] {-1};

            if (maxworlds < 0) maxworlds = worlds.Length;
            if (maxinteriors < 0) maxinteriors = interiors.Length;
            if (maxplayers < 0) maxplayers = players.Length;

            return Native.CallNative("CreateDynamic3DTextLabelEx", new[] {14, 15, 16},
                __arglist(
                    text, (int) color, x, y, z, drawdistance, attachedplayer, attachedvehicle, testlos, streamdistance,
                    worlds, interiors, players, maxworlds, maxinteriors, maxplayers));
        }


        public static int CreateDynamicCircleEx(float x, float y, float size, int[] worlds = null,
            int[] interiors = null, int[] players = null, int maxworlds = -1, int maxinteriors = -1, int maxplayers = -1)
        {
            //Check defaults
            if (worlds == null) worlds = new[] {-1};
            if (interiors == null) interiors = new[] {-1};
            if (players == null) players = new[] {-1};

            if (maxworlds < 0) maxworlds = worlds.Length;
            if (maxinteriors < 0) maxinteriors = interiors.Length;
            if (maxplayers < 0) maxplayers = players.Length;

            return Native.CallNative("CreateDynamicCircleEx", new[] {7, 8, 9},
                __arglist(x, y, size, worlds, interiors, players, maxworlds, maxinteriors, maxplayers));
        }

        public static int CreateDynamicRectangleEx(float minx, float miny, float maxx, float maxy, int[] worlds = null,
            int[] interiors = null, int[] players = null, int maxworlds = -1, int maxinteriors = -1, int maxplayers = -1)
        {
            //Check defaults
            if (worlds == null) worlds = new[] {-1};
            if (interiors == null) interiors = new[] {-1};
            if (players == null) players = new[] {-1};

            if (maxworlds < 0) maxworlds = worlds.Length;
            if (maxinteriors < 0) maxinteriors = interiors.Length;
            if (maxplayers < 0) maxplayers = players.Length;

            return Native.CallNative("CreateDynamicRectangleEx", new[] {8, 9, 10},
                __arglist(minx, miny, maxx, maxy, worlds, interiors, players, maxworlds, maxinteriors, maxplayers));
        }

        public static int CreateDynamicSphereEx(float x, float y, float z, float size, int[] worlds = null,
            int[] interiors = null, int[] players = null, int maxworlds = -1, int maxinteriors = -1, int maxplayers = -1)
        {
            //Check defaults
            if (worlds == null) worlds = new[] {-1};
            if (interiors == null) interiors = new[] {-1};
            if (players == null) players = new[] {-1};

            if (maxworlds < 0) maxworlds = worlds.Length;
            if (maxinteriors < 0) maxinteriors = interiors.Length;
            if (maxplayers < 0) maxplayers = players.Length;

            return Native.CallNative("CreateDynamicSphereEx", new[] {8, 9, 10},
                __arglist(x, y, z, size, worlds, interiors, players, maxworlds, maxinteriors, maxplayers));
        }

        public static int CreateDynamicCubeEx(float minx, float miny, float minz, float maxx, float maxy, float maxz,
            int[] worlds = null, int[] interiors = null, int[] players = null, int maxworlds = -1, int maxinteriors = -1,
            int maxplayers = -1)
        {
            //Check defaults
            if (worlds == null) worlds = new[] {-1};
            if (interiors == null) interiors = new[] {-1};
            if (players == null) players = new[] {-1};

            if (maxworlds < 0) maxworlds = worlds.Length;
            if (maxinteriors < 0) maxinteriors = interiors.Length;
            if (maxplayers < 0) maxplayers = players.Length;

            return Native.CallNative("CreateDynamicCubeEx", new[] {10, 11, 12},
                __arglist(
                    minx, miny, minz, maxx, maxy, maxz, worlds, interiors, players, maxworlds, maxinteriors, maxplayers));
        }

        public static int CreateDynamicPolygonEx(float[] points, float minz = float.NegativeInfinity,
            float maxz = float.PositiveInfinity, int maxpoints = -1, int[] worlds = null, int[] interiors = null,
            int[] players = null, int maxworlds = -1, int maxinteriors = -1, int maxplayers = -1)
        {
            if (points == null)
                throw new NullReferenceException("points cannot be null");

            //Check defaults
            if (worlds == null) worlds = new[] {-1};
            if (interiors == null) interiors = new[] {-1};
            if (players == null) players = new[] {-1};

            if (maxworlds < 0) maxworlds = worlds.Length;
            if (maxinteriors < 0) maxinteriors = interiors.Length;
            if (maxplayers < 0) maxplayers = players.Length;
            if (maxpoints < 0) maxpoints = points.Length;

            return Native.CallNative("CreateDynamicPolygonEx", new[] {8, 9, 10},
                __arglist(points, minz, maxz, maxpoints, worlds, interiors, players, maxworlds, maxinteriors, maxplayers
                    ));
        }
    }
}