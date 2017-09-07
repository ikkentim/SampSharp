// SampSharp
// Copyright 2017 Tim Potze
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
using System;
using System.IO;
using SampSharp.GameMode.API;

namespace SampSharp.GameMode.Tools
{
    /// <summary>
    ///     Contains methods for reading SA height map files.
    /// </summary>
    /// <remarks>
    ///     If MapAndreas 1.2(.1) is loaded, the plugin will be used instead of
    ///     the managed logic. This is to save your precious resources. Most of
    ///     this logic has been copied from MapAndreas v1.2 released at
    ///     http://forum.sa-mp.com/showthread.php?t=275492
    /// </remarks>
    internal static partial class MapAndreas // TODO: Temporarily disabled
    {
        private const string FullFile = "scriptfiles/SAfull.hmap";
        private const string MinimalFile = "scriptfiles/SAmin.hmap";
        private static MapAndreasMode _mode;
        private static bool _usePlugin;
        private static FileStream _fileStream;
        private static ushort[] _data;

        private static bool IsPluginLoaded()
        {
            /*
             * Require version 1.2 or newer
             */
            throw new NotImplementedException();
           // return Native.Exists("MapAndreas_SaveCurrentHMap");
        }

        /// <summary>
        ///     Loads the map data into the memory.
        /// </summary>
        /// <param name="mode">
        ///     The <see cref="MapAndreasMode" /> to load with.
        /// </param>
        /// <exception cref="FileLoadException">
        ///     Thrown if the file couldn't be
        ///     loaded.
        /// </exception>
        public static void Load(MapAndreasMode mode)
        {
            if (_mode != MapAndreasMode.None) return;
            _mode = mode;

            if (IsPluginLoaded())
            {
                MapAndreasInternal.Instance.Init((int) mode, string.Empty, 1);
                _usePlugin = true;
                return;
            }

            switch (mode)
            {
                case MapAndreasMode.Full:
                    try
                    {
                        using (var memstream = new FileStream(FullFile, FileMode.Open))
                        {
                            _data = new ushort[memstream.Length/2];
                            var buffer = new byte[2];
                            var loc = 0;
                            while ((memstream.Read(buffer, 0, 2)) == 2)
                                _data[loc++] = BitConverter.ToUInt16(buffer, 0);
                        }
                    }
                    catch (Exception e)
                    {
                        _mode = MapAndreasMode.None;
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
                            var loc = 0;
                            while ((memstream.Read(buffer, 0, 2)) == 2)
                                _data[loc++] = BitConverter.ToUInt16(buffer, 0);
                        }
                    }
                    catch (Exception e)
                    {
                        _mode = MapAndreasMode.None;
                        throw new FileLoadException("Couldn't load " + MinimalFile, e);
                    }
                    break;
                case MapAndreasMode.NoBuffer:
                    try
                    {
                        _fileStream = new FileStream(FullFile, FileMode.Open);
                    }
                    catch (Exception e)
                    {
                        _mode = MapAndreasMode.None;
                        throw new FileLoadException("Couldn't load " + MinimalFile, e);
                    }
                    break;
            }
        }

        /// <summary>
        ///     Unloads the map data from the memory.
        /// </summary>
        public static void Unload()
        {
            if (_usePlugin)
            {
                MapAndreasInternal.Instance.Unload();

                _usePlugin = false;
                _mode = MapAndreasMode.None;
                return;
            }

            switch (_mode)
            {
                case MapAndreasMode.NoBuffer:
                    _fileStream.Dispose();
                    break;
                default:
                    _data = null;
                    break;
            }

            _data = null;
            _mode = MapAndreasMode.None;
        }

        /// <summary>
        ///     Finds highest Z point (ground level) for the provided point.
        /// </summary>
        /// <param name="x">X-coordinate of the point.</param>
        /// <param name="y">Y-coordinate of the point.</param>
        /// <returns>Ground level at the given point.</returns>
        public static float Find(float x, float y)
        {
            if (_mode == MapAndreasMode.None) return 0;

            if (_usePlugin)
            {
                float result;
                MapAndreasInternal.Instance.FindZ(x, y, out result);
                return result;
            }
            // check for a co-ord outside the map
            if (x < -3000.0f || x > 3000.0f || y > 3000.0f || y < -3000.0f) return 0.0f;

            // get row/col on 6000x6000 grid
            var iGridX = ((int) x) + 3000;
            var iGridY = (((int) y) - 3000)*-1;
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
        public static Vector3 Find(Vector3 point)
        {
            return new Vector3(point.X, point.Y, Find(point.X, point.Y));
        }

        /// <summary>
        ///     Finds highest Z point (ground level) for the provided point.
        /// </summary>
        /// <param name="point">The point to move to ground level.</param>
        public static void Find(ref Vector3 point)
        {
            point = Find(point);
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

            if (_usePlugin)
            {
                float result;
                MapAndreasInternal.Instance.FindAverageZ(x, y, out result);
                return result;
            }

            float gridsize = _mode == MapAndreasMode.Full ? 1 : 3;

            // Get the Z value of 2 neighbor grids
            var p1 = Find(x, y);
            var p2 = x < 0.0f ? Find(x + gridsize, y) : Find(x - gridsize, y);
            var p3 = y < 0.0f ? Find(x, y + gridsize) : Find(x, y - gridsize);

            // Filter the decimal part only
            var xx = x%1;
            var yy = y%1;
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
        public static Vector3 FindAverage(Vector3 point)
        {
            return new Vector3(point.X, point.Y, FindAverage(point.X, point.Y));
        }

        /// <summary>
        ///     Calculates a linear approximation of the ground level at the provided point.
        /// </summary>
        /// <param name="point">The point to move to the approximate ground level.</param>
        public static void FindAverage(ref Vector3 point)
        {
            point = FindAverage(point);
        }

        /// <summary>
        ///     Set the highest Z point at the provided point.
        /// </summary>
        /// <param name="x">X-coordinate of the point.</param>
        /// <param name="y">Y-coordinate of the point.</param>
        /// <param name="z">Z-coordinate of the hight at the provided point.</param>
        /// <returns>True on success; False otherwise.</returns>
        public static bool SetZ(float x, float y, float z)
        {
            if (_usePlugin)
            {
                return MapAndreasInternal.Instance.SetZ(x, y, z);
            }

            if (x < -3000.0f || x > 3000.0f || y > 3000.0f || y < -3000.0f) return false;
            if (z < 0 || z > 655.35) return false;

            // get row/col on 6000x6000 grid
            var iGridX = ((int) x) + 3000;
            var iGridY = -(((int) y) - 3000);
            int iDataPos;

            if (_mode == MapAndreasMode.Full)
            {
                iDataPos = (iGridY*6000) + iGridX;
                _data[iDataPos] = (ushort) (z*100.0f + 0.5); // Add 0.5 to round it properly
                return true;
            }
            if (_mode == MapAndreasMode.Minimal)
            {
                iDataPos = ((iGridY/3)*2000) + iGridX/3; // skip every 2nd and 3rd line
                _data[iDataPos] = (ushort) (z*100.0f + 0.5); // Add 0.5 to round it properly
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Set the highest Z point at the provided point.
        /// </summary>
        /// <param name="point">The point with the new Z-coordinate.</param>
        /// <returns>True on success; False otherwise.</returns>
        public static bool SetZ(Vector3 point)
        {
            return SetZ(point.X, point.Y, point.Z);
        }

        /// <summary>
        ///     Saves the current height map to the provided file.
        /// </summary>
        /// <param name="file"></param>
        public static bool Save(string file)
        {
            if (_usePlugin)
            {
                return MapAndreasInternal.Instance.SaveCurrentHMap(file);
            }

            try
            {
                using (var stream = new FileStream(file, FileMode.Create, FileAccess.Write))
                {
                    foreach (var v in _data)
                        stream.Write(BitConverter.GetBytes(v), 0, 2);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}