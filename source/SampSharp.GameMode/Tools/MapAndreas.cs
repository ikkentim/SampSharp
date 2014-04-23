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
using System.IO;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Tools
{
    public static class MapAndreas
    {
        private const string FullFile = "scriptfiles/SAfull.hmap";
        private const string MinimalFile = "scriptfiles/SAmin.hmap";

        private static MapAndreasMode _mode;
        private static ushort[] _data;

        /// <summary>
        ///     Loads the mapdata in to the memory.
        /// </summary>
        /// <param name="mode">The mode to load with</param>
        /// <exception cref="FileLoadException">Thrown when the file couldn't be loaded</exception>
        public static void Load(MapAndreasMode mode)
        {
            _mode = mode;

            switch (mode)
            {
                case MapAndreasMode.Full:
                    try
                    {
                        using (var memstream = new FileStream(FullFile, FileMode.Open))
                        {
                            _data = new ushort[memstream.Length/2];
                            var buffer = new byte[2];
                            int loc = 0;
                            while ((memstream.Read(buffer, 0, 2)) == 2)
                                _data[loc++] = BitConverter.ToUInt16(buffer, 0);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new FileLoadException("Couldn't load " + FullFile, e);
                    }
                    break;
                case MapAndreasMode.Minimal:
                    try
                    {
                        using (var memstream = new FileStream(MinimalFile, FileMode.Open))
                        {
                            _data = new ushort[memstream.Length/2];
                            var buffer = new byte[2];
                            int loc = 0;
                            while ((memstream.Read(buffer, 0, 2)) == 2)
                                _data[loc++] = BitConverter.ToUInt16(buffer, 0);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new FileLoadException("Couldn't load " + MinimalFile, e);
                    }
                    break;
            }

            Console.WriteLine("[MapAndreas] Successfully loaded using mode: " + mode);
        }

        /// <summary>
        ///     Unloads the mapdate from the memory.
        /// </summary>
        public static void Unload()
        {
            _data = null;
            _mode = MapAndreasMode.None;
            Console.WriteLine("[MapAndreas] Successfully unloaded");
        }

        /// <summary>
        ///     Finds highest Z point (ground level) for the provided point.
        /// </summary>
        /// <param name="x">X-coordinate of the point.</param>
        /// <param name="y">Y-coordinate of the point.</param>
        /// <returns>Ground level at the given point.</returns>
        public static float Find(float x, float y)
        {
            // check for a co-ord outside the map
            if (x < -3000.0f || x > 3000.0f || y > 3000.0f || y < -3000.0f) return 0.0f;

            // get row/col on 6000x6000 grid
            int iGridX = ((int) x) + 3000;
            int iGridY = (((int) y) - 3000)*-1;
            int iDataPos;

            switch (_mode)
            {
                case MapAndreasMode.Full:
                    iDataPos = (iGridY*6000) + iGridX;
                    // for every Y, increment by the number of cols, add the col index.
                    return _data[iDataPos]/100.0f; // the data is a float stored as ushort * 100

                case MapAndreasMode.Minimal:
                    iDataPos = ((iGridY/3)*2000) + iGridX/3;
                    // for every Y, increment by the number of cols, add the col index.
                    return _data[iDataPos]/100.0f; // the data is a float stored as ushort * 100
            }
            return 0.0f;
        }

        /// <summary>
        ///     Finds highest Z point (ground level) for the provided point.
        /// </summary>
        /// <param name="point">The point to look at.</param>
        /// <returns>Ground level at the given point.</returns>
        public static Vector Find(Vector point)
        {
            return new Vector(point.X, point.Y, Find(point.X, point.Y));
        }

        /// <summary>
        ///     Calculates a linear approximation of the ground level at the provided point.
        /// </summary>
        /// <param name="x">X-coordinate of the point.</param>
        /// <param name="y">Y-coordinate of the point.</param>
        /// <returns>A approximation of the ground level at the given point.</returns>
        public static float FindAverage(float x, float y)
        {
            if (_mode == MapAndreasMode.None) return 0;

            float gridsize = _mode == MapAndreasMode.Full ? 1 : 3;

            // Get the Z value of 2 neighbor grids
            float p1 = Find(x, y);
            float p2 = x < 0.0f ? Find(x + gridsize, y) : Find(x - gridsize, y);
            float p3 = y < 0.0f ? Find(x, y + gridsize) : Find(x, y - gridsize);

            // Filter the decimal part only
            float xx = x%1;
            float yy = y%1;
            if (xx < 0) x = -xx; //Pointless? shouldn't it be xx = -xx? TODO: figure that out
            if (yy < 0) y = -yy;

            // Calculate a linear approximation of the z coordinate
            return p1 + xx*(p1 - p2) + yy*(p1 - p3);
        }

        /// <summary>
        ///     Calculates a linear approximation of the ground level at the provided point.
        /// </summary>
        /// <param name="point">The point to look at.</param>
        /// <returns>A approximation of the ground level at the given point.</returns>
        public static Vector FindAverage(Vector point)
        {
            return new Vector(point.X, point.Y, FindAverage(point.X, point.Y));
        }
    }
}