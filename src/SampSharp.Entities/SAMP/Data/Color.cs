﻿// SampSharp
// Copyright 2020 Tim Potze
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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SampSharp.Entities.SAMP
{
    /// <summary>
    /// Represents a hexadecimal color.
    /// </summary>
    [SuppressMessage("ReSharper", "CommentTypo")]
    public struct Color
    {
        private static readonly uint[] VehicleColors =
        {
            0x000000FF, 0xF5F5F5FF, 0x2A77A1FF, 0x840410FF, 0x263739FF, 0x86446EFF, 0xD78E10FF, 0x4C75B7FF, 0xBDBEC6FF,
            0x5E7072FF, 0x46597AFF, 0x656A79FF, 0x5D7E8DFF, 0x58595AFF, 0xD6DAD6FF, 0x9CA1A3FF, 0x335F3FFF, 0x730E1AFF,
            0x7B0A2AFF, 0x9F9D94FF, 0x3B4E78FF, 0x732E3EFF, 0x691E3BFF, 0x96918CFF, 0x515459FF, 0x3F3E45FF, 0xA5A9A7FF,
            0x635C5AFF, 0x3D4A68FF, 0x979592FF, 0x421F21FF, 0x5F272BFF, 0x8494ABFF, 0x767B7CFF, 0x646464FF, 0x5A5752FF,
            0x252527FF, 0x2D3A35FF, 0x93A396FF, 0x6D7A88FF, 0x221918FF, 0x6F675FFF, 0x7C1C2AFF, 0x5F0A15FF, 0x193826FF,
            0x5D1B20FF, 0x9D9872FF, 0x7A7560FF, 0x989586FF, 0xADB0B0FF, 0x848988FF, 0x304F45FF, 0x4D6268FF, 0x162248FF,
            0x272F4BFF, 0x7D6256FF, 0x9EA4ABFF, 0x9C8D71FF, 0x6D1822FF, 0x4E6881FF, 0x9C9C98FF, 0x917347FF, 0x661C26FF,
            0x949D9FFF, 0xA4A7A5FF, 0x8E8C46FF, 0x341A1EFF, 0x6A7A8CFF, 0xAAAD8EFF, 0xAB988FFF, 0x851F2EFF, 0x6F8297FF,
            0x585853FF, 0x9AA790FF, 0x601A23FF, 0x20202CFF, 0xA4A096FF, 0xAA9D84FF, 0x78222BFF, 0x0E316DFF, 0x722A3FFF,
            0x7B715EFF, 0x741D28FF, 0x1E2E32FF, 0x4D322FFF, 0x7C1B44FF, 0x2E5B20FF, 0x395A83FF, 0x6D2837FF, 0xA7A28FFF,
            0xAFB1B1FF, 0x364155FF, 0x6D6C6EFF, 0x0F6A89FF, 0x204B6BFF, 0x2B3E57FF, 0x9B9F9DFF, 0x6C8495FF, 0x4D8495FF,
            0xAE9B7FFF, 0x406C8FFF, 0x1F253BFF, 0xAB9276FF, 0x134573FF, 0x96816CFF, 0x64686AFF, 0x105082FF, 0xA19983FF,
            0x385694FF, 0x525661FF, 0x7F6956FF, 0x8C929AFF, 0x596E87FF, 0x473532FF, 0x44624FFF, 0x730A27FF, 0x223457FF,
            0x640D1BFF, 0xA3ADC6FF, 0x695853FF, 0x9B8B80FF, 0x620B1CFF, 0x5B5D5EFF, 0x624428FF, 0x731827FF, 0x1B376DFF,
            0xEC6AAEFF
        };

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="r">The red value of this Color.</param>
        /// <param name="g">The green value of this Color.</param>
        /// <param name="b">The blue value of this Color.</param>
        /// <param name="a">The alpha value of this Color.</param>
        public Color(byte r, byte g, byte b, byte a)
            : this()
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="r">The red value of this Color.</param>
        /// <param name="g">The green value of this Color.</param>
        /// <param name="b">The blue value of this Color.</param>
        public Color(byte r, byte g, byte b)
            : this()
        {
            R = r;
            G = g;
            B = b;
            A = 255;
        }

        /// <summary>
        /// Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="r">The red value of this Color.</param>
        /// <param name="g">The green value of this Color.</param>
        /// <param name="b">The blue value of this Color.</param>
        /// <param name="a">The alpha value of this Color.</param>
        public Color(byte r, byte g, byte b, float a)
            : this(r, g, b, (byte) MathHelper.Clamp(a * byte.MaxValue, byte.MinValue, byte.MaxValue))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="r">The red value of this Color.</param>
        /// <param name="g">The green value of this Color.</param>
        /// <param name="b">The blue value of this Color.</param>
        /// <param name="a">The alpha value of this Color.</param>
        public Color(int r, int g, int b, int a)
            : this(
                (byte) MathHelper.Clamp(r, byte.MinValue, byte.MaxValue),
                (byte) MathHelper.Clamp(g, byte.MinValue, byte.MaxValue),
                (byte) MathHelper.Clamp(b, byte.MinValue, byte.MaxValue),
                (byte) MathHelper.Clamp(a, byte.MinValue, byte.MaxValue))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="r">The red value of this Color.</param>
        /// <param name="g">The green value of this Color.</param>
        /// <param name="b">The blue value of this Color.</param>
        public Color(int r, int g, int b) : this(r, g, b, 255)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="r">The red value of this Color.</param>
        /// <param name="g">The green value of this Color.</param>
        /// <param name="b">The blue value of this Color.</param>
        /// <param name="a">The alpha value of this Color.</param>
        public Color(float r, float g, float b, float a)
            : this(
                (byte) MathHelper.Clamp(r * byte.MaxValue, byte.MinValue, byte.MaxValue),
                (byte) MathHelper.Clamp(g * byte.MaxValue, byte.MinValue, byte.MaxValue),
                (byte) MathHelper.Clamp(b * byte.MaxValue, byte.MinValue, byte.MaxValue),
                (byte) MathHelper.Clamp(a * byte.MaxValue, byte.MinValue, byte.MaxValue))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="r">The red value of this Color.</param>
        /// <param name="g">The green value of this Color.</param>
        /// <param name="b">The blue value of this Color.</param>
        public Color(float r, float g, float b)
            : this(r, g, b, 1.0f)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="color">The Color values to use for this Color.</param>
        public Color(int color)
            : this()
        {
            var c = FromInteger(color, ColorFormat.RGBA); //Default format
            R = c.R;
            G = c.G;
            B = c.B;
            A = c.A;
        }

        /// <summary>
        /// Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="color">The Color values to use for this Color.</param>
        public Color(uint color)
            : this()
        {
            var c = FromInteger(color, ColorFormat.RGBA); //Default format
            R = c.R;
            G = c.G;
            B = c.B;
            A = c.A;
        }

        #endregion

        #region Defaults

        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFF0F8FF.
        /// </summary>
        public static Color AliceBlue { get; } = new Color(0xF0, 0xF8, 0xFF, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFAEBD7.
        /// </summary>
        public static Color AntiqueWhite { get; } = new Color(0xFA, 0xEB, 0xD7, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF00FFFF.
        /// </summary>
        public static Color Aqua { get; } = new Color(0x00, 0xFF, 0xFF, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF7FFFD4.
        /// </summary>
        public static Color Aquamarine { get; } = new Color(0x7F, 0xFF, 0xD4, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFF0FFFF.
        /// </summary>
        public static Color Azure { get; } = new Color(0xF0, 0xFF, 0xFF, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFF5F5DC.
        /// </summary>
        public static Color Beige { get; } = new Color(0xF5, 0xF5, 0xDC, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFE4C4.
        /// </summary>
        public static Color Bisque { get; } = new Color(0xFF, 0xE4, 0xC4, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF000000.
        /// </summary>
        public static Color Black { get; } = new Color(0x00, 0x00, 0x00, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFEBCD.
        /// </summary>
        public static Color BlanchedAlmond { get; } = new Color(0xFF, 0xEB, 0xCD, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF0000FF.
        /// </summary>
        public static Color Blue { get; } = new Color(0x00, 0x00, 0xFF, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF8A2BE2.
        /// </summary>
        public static Color BlueViolet { get; } = new Color(0x8A, 0x2B, 0xE2, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFA52A2A.
        /// </summary>
        public static Color Brown { get; } = new Color(0xA5, 0x2A, 0x2A, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFDEB887.
        /// </summary>
        public static Color BurlyWood { get; } = new Color(0xDE, 0xB8, 0x87, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF5F9EA0.
        /// </summary>
        public static Color CadetBlue { get; } = new Color(0x5F, 0x9E, 0xA0, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF7FFF00.
        /// </summary>
        public static Color Chartreuse { get; } = new Color(0x7F, 0xFF, 0x00, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFD2691E.
        /// </summary>
        public static Color Chocolate { get; } = new Color(0xD2, 0x69, 0x1E, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFF7F50.
        /// </summary>
        public static Color Coral { get; } = new Color(0xFF, 0x7F, 0x50, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF6495ED.
        /// </summary>
        public static Color CornflowerBlue { get; } = new Color(0x64, 0x95, 0xED, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFF8DC.
        /// </summary>
        public static Color Cornsilk { get; } = new Color(0xFF, 0xF8, 0xDC, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFDC143C.
        /// </summary>
        public static Color Crimson { get; } = new Color(0xDC, 0x14, 0x3C, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF00FFFF.
        /// </summary>
        public static Color Cyan { get; } = new Color(0x00, 0xFF, 0xFF, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF00008B.
        /// </summary>
        public static Color DarkBlue { get; } = new Color(0x00, 0x00, 0x8B, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF008B8B.
        /// </summary>
        public static Color DarkCyan { get; } = new Color(0x00, 0x8B, 0x8B, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFB8860B.
        /// </summary>
        public static Color DarkGoldenrod { get; } = new Color(0xB8, 0x86, 0x0B, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFA9A9A9.
        /// </summary>
        public static Color DarkGray { get; } = new Color(0xA9, 0xA9, 0xA9, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF006400.
        /// </summary>
        public static Color DarkGreen { get; } = new Color(0x00, 0x64, 0x00, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFBDB76B.
        /// </summary>
        public static Color DarkKhaki { get; } = new Color(0xBD, 0xB7, 0x6B, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF8B008B.
        /// </summary>
        public static Color DarkMagenta { get; } = new Color(0x8B, 0x00, 0x8B, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF556B2F.
        /// </summary>
        public static Color DarkOliveGreen { get; } = new Color(0x55, 0x6B, 0x2F, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFF8C00.
        /// </summary>
        public static Color DarkOrange { get; } = new Color(0xFF, 0x8C, 0x00, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF9932CC.
        /// </summary>
        public static Color DarkOrchid { get; } = new Color(0x99, 0x32, 0xCC, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF8B0000.
        /// </summary>
        public static Color DarkRed { get; } = new Color(0x8B, 0x00, 0x00, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFE9967A.
        /// </summary>
        public static Color DarkSalmon { get; } = new Color(0xE9, 0x96, 0x7A, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF8FBC8F.
        /// </summary>
        public static Color DarkSeaGreen { get; } = new Color(0x8F, 0xBC, 0x8F, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF483D8B.
        /// </summary>
        public static Color DarkSlateBlue { get; } = new Color(0x48, 0x3D, 0x8B, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF2F4F4F.
        /// </summary>
        public static Color DarkSlateGray { get; } = new Color(0x2F, 0x4F, 0x4F, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF00CED1.
        /// </summary>
        public static Color DarkTurquoise { get; } = new Color(0x00, 0xCE, 0xD1, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF9400D3.
        /// </summary>
        public static Color DarkViolet { get; } = new Color(0x94, 0x00, 0xD3, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFF1493.
        /// </summary>
        public static Color DeepPink { get; } = new Color(0xFF, 0x14, 0x93, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF00BFFF.
        /// </summary>
        public static Color DeepSkyBlue { get; } = new Color(0x00, 0xBF, 0xFF, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF696969.
        /// </summary>
        public static Color DimGray { get; } = new Color(0x69, 0x69, 0x69, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF1E90FF.
        /// </summary>
        public static Color DodgerBlue { get; } = new Color(0x1E, 0x90, 0xFF, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFB22222.
        /// </summary>
        public static Color Firebrick { get; } = new Color(0xB2, 0x22, 0x22, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFFAF0.
        /// </summary>
        public static Color FloralWhite { get; } = new Color(0xFF, 0xFA, 0xF0, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF228B22.
        /// </summary>
        public static Color ForestGreen { get; } = new Color(0x22, 0x8B, 0x22, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFF00FF.
        /// </summary>
        public static Color Fuchsia { get; } = new Color(0xFF, 0x00, 0xFF, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFDCDCDC.
        /// </summary>
        public static Color Gainsboro { get; } = new Color(0xDC, 0xDC, 0xDC, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFF8F8FF.
        /// </summary>
        public static Color GhostWhite { get; } = new Color(0xF8, 0xF8, 0xFF, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFD700.
        /// </summary>
        public static Color Gold { get; } = new Color(0xFF, 0xD7, 0x00, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFDAA520.
        /// </summary>
        public static Color Goldenrod { get; } = new Color(0xDA, 0xA5, 0x20, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF808080.
        /// </summary>
        public static Color Gray { get; } = new Color(0x80, 0x80, 0x80, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF008000.
        /// </summary>
        public static Color Green { get; } = new Color(0x00, 0x80, 0x00, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFADFF2F.
        /// </summary>
        public static Color GreenYellow { get; } = new Color(0xAD, 0xFF, 0x2F, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFF0FFF0.
        /// </summary>
        public static Color Honeydew { get; } = new Color(0xF0, 0xFF, 0xF0, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFF69B4.
        /// </summary>
        public static Color HotPink { get; } = new Color(0xFF, 0x69, 0xB4, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFCD5C5C.
        /// </summary>
        public static Color IndianRed { get; } = new Color(0xCD, 0x5C, 0x5C, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF4B0082.
        /// </summary>
        public static Color Indigo { get; } = new Color(0x4B, 0x00, 0x82, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFFFF0.
        /// </summary>
        public static Color Ivory { get; } = new Color(0xFF, 0xFF, 0xF0, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFF0E68C.
        /// </summary>
        public static Color Khaki { get; } = new Color(0xF0, 0xE6, 0x8C, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFE6E6FA.
        /// </summary>
        public static Color Lavender { get; } = new Color(0xE6, 0xE6, 0xFA, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFF0F5.
        /// </summary>
        public static Color LavenderBlush { get; } = new Color(0xFF, 0xF0, 0xF5, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF7CFC00.
        /// </summary>
        public static Color LawnGreen { get; } = new Color(0x7C, 0xFC, 0x00, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFFACD.
        /// </summary>
        public static Color LemonChiffon { get; } = new Color(0xFF, 0xFA, 0xCD, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFADD8E6.
        /// </summary>
        public static Color LightBlue { get; } = new Color(0xAD, 0xD8, 0xE6, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFF08080.
        /// </summary>
        public static Color LightCoral { get; } = new Color(0xF0, 0x80, 0x80, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFE0FFFF.
        /// </summary>
        public static Color LightCyan { get; } = new Color(0xE0, 0xFF, 0xFF, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFAFAD2.
        /// </summary>
        public static Color LightGoldenrodYellow { get; } = new Color(0xFA, 0xFA, 0xD2, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFD3D3D3.
        /// </summary>
        public static Color LightGray { get; } = new Color(0xD3, 0xD3, 0xD3, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF90EE90.
        /// </summary>
        public static Color LightGreen { get; } = new Color(0x90, 0xEE, 0x90, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFB6C1.
        /// </summary>
        public static Color LightPink { get; } = new Color(0xFF, 0xB6, 0xC1, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFA07A.
        /// </summary>
        public static Color LightSalmon { get; } = new Color(0xFF, 0xA0, 0x7A, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF20B2AA.
        /// </summary>
        public static Color LightSeaGreen { get; } = new Color(0x20, 0xB2, 0xAA, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF87CEFA.
        /// </summary>
        public static Color LightSkyBlue { get; } = new Color(0x87, 0xCE, 0xFA, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF778899.
        /// </summary>
        public static Color LightSlateGray { get; } = new Color(0x77, 0x88, 0x99, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFB0C4DE.
        /// </summary>
        public static Color LightSteelBlue { get; } = new Color(0xB0, 0xC4, 0xDE, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFFFE0.
        /// </summary>
        public static Color LightYellow { get; } = new Color(0xFF, 0xFF, 0xE0, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF00FF00.
        /// </summary>
        public static Color Lime { get; } = new Color(0x00, 0xFF, 0x00, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF32CD32.
        /// </summary>
        public static Color LimeGreen { get; } = new Color(0x32, 0xCD, 0x32, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFAF0E6.
        /// </summary>
        public static Color Linen { get; } = new Color(0xFA, 0xF0, 0xE6, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFF00FF.
        /// </summary>
        public static Color Magenta { get; } = new Color(0xFF, 0x00, 0xFF, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF800000.
        /// </summary>
        public static Color Maroon { get; } = new Color(0x80, 0x00, 0x00, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF66CDAA.
        /// </summary>
        public static Color MediumAquamarine { get; } = new Color(0x66, 0xCD, 0xAA, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF0000CD.
        /// </summary>
        public static Color MediumBlue { get; } = new Color(0x00, 0x00, 0xCD, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFBA55D3.
        /// </summary>
        public static Color MediumOrchid { get; } = new Color(0xBA, 0x55, 0xD3, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF9370DB.
        /// </summary>
        public static Color MediumPurple { get; } = new Color(0x93, 0x70, 0xDB, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF3CB371.
        /// </summary>
        public static Color MediumSeaGreen { get; } = new Color(0x3C, 0xB3, 0x71, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF7B68EE.
        /// </summary>
        public static Color MediumSlateBlue { get; } = new Color(0x7B, 0x68, 0xEE, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF00FA9A.
        /// </summary>
        public static Color MediumSpringGreen { get; } = new Color(0x00, 0xFA, 0x9A, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF48D1CC.
        /// </summary>
        public static Color MediumTurquoise { get; } = new Color(0x48, 0xD1, 0xCC, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFC71585.
        /// </summary>
        public static Color MediumVioletRed { get; } = new Color(0xC7, 0x15, 0x85, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF191970.
        /// </summary>
        public static Color MidnightBlue { get; } = new Color(0x19, 0x19, 0x70, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFF5FFFA.
        /// </summary>
        public static Color MintCream { get; } = new Color(0xF5, 0xFF, 0xFA, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFE4E1.
        /// </summary>
        public static Color MistyRose { get; } = new Color(0xFF, 0xE4, 0xE1, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFE4B5.
        /// </summary>
        public static Color Moccasin { get; } = new Color(0xFF, 0xE4, 0xB5, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFDEAD.
        /// </summary>
        public static Color NavajoWhite { get; } = new Color(0xFF, 0xDE, 0xAD, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF000080.
        /// </summary>
        public static Color Navy { get; } = new Color(0x00, 0x00, 0x80, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFDF5E6.
        /// </summary>
        public static Color OldLace { get; } = new Color(0xFD, 0xF5, 0xE6, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF808000.
        /// </summary>
        public static Color Olive { get; } = new Color(0x80, 0x80, 0x00, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF6B8E23.
        /// </summary>
        public static Color OliveDrab { get; } = new Color(0x6B, 0x8E, 0x23, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFA500.
        /// </summary>
        public static Color Orange { get; } = new Color(0xFF, 0xA5, 0x00, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFF4500.
        /// </summary>
        public static Color OrangeRed { get; } = new Color(0xFF, 0x45, 0x00, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFDA70D6.
        /// </summary>
        public static Color Orchid { get; } = new Color(0xDA, 0x70, 0xD6, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFEEE8AA.
        /// </summary>
        public static Color PaleGoldenrod { get; } = new Color(0xEE, 0xE8, 0xAA, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF98FB98.
        /// </summary>
        public static Color PaleGreen { get; } = new Color(0x98, 0xFB, 0x98, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFAFEEEE.
        /// </summary>
        public static Color PaleTurquoise { get; } = new Color(0xAF, 0xEE, 0xEE, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFDB7093.
        /// </summary>
        public static Color PaleVioletRed { get; } = new Color(0xDB, 0x70, 0x93, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFEFD5.
        /// </summary>
        public static Color PapayaWhip { get; } = new Color(0xFF, 0xEF, 0xD5, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFDAB9.
        /// </summary>
        public static Color PeachPuff { get; } = new Color(0xFF, 0xDA, 0xB9, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFCD853F.
        /// </summary>
        public static Color Peru { get; } = new Color(0xCD, 0x85, 0x3F, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFC0CB.
        /// </summary>
        public static Color Pink { get; } = new Color(0xFF, 0xC0, 0xCB, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFDDA0DD.
        /// </summary>
        public static Color Plum { get; } = new Color(0xDD, 0xA0, 0xDD, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFB0E0E6.
        /// </summary>
        public static Color PowderBlue { get; } = new Color(0xB0, 0xE0, 0xE6, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF800080.
        /// </summary>
        public static Color Purple { get; } = new Color(0x80, 0x00, 0x80, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFF0000.
        /// </summary>
        public static Color Red { get; } = new Color(0xFF, 0x00, 0x00, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFBC8F8F.
        /// </summary>
        public static Color RosyBrown { get; } = new Color(0xBC, 0x8F, 0x8F, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF4169E1.
        /// </summary>
        public static Color RoyalBlue { get; } = new Color(0x41, 0x69, 0xE1, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF8B4513.
        /// </summary>
        public static Color SaddleBrown { get; } = new Color(0x8B, 0x45, 0x13, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFA8072.
        /// </summary>
        public static Color Salmon { get; } = new Color(0xFA, 0x80, 0x72, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFF4A460.
        /// </summary>
        public static Color SandyBrown { get; } = new Color(0xF4, 0xA4, 0x60, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF2E8B57.
        /// </summary>
        public static Color SeaGreen { get; } = new Color(0x2E, 0x8B, 0x57, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFF5EE.
        /// </summary>
        public static Color SeaShell { get; } = new Color(0xFF, 0xF5, 0xEE, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFA0522D.
        /// </summary>
        public static Color Sienna { get; } = new Color(0xA0, 0x52, 0x2D, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFC0C0C0.
        /// </summary>
        public static Color Silver { get; } = new Color(0xC0, 0xC0, 0xC0, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF87CEEB.
        /// </summary>
        public static Color SkyBlue { get; } = new Color(0x87, 0xCE, 0xEB, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF6A5ACD.
        /// </summary>
        public static Color SlateBlue { get; } = new Color(0x6A, 0x5A, 0xCD, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF708090.
        /// </summary>
        public static Color SlateGray { get; } = new Color(0x70, 0x80, 0x90, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFFAFA.
        /// </summary>
        public static Color Snow { get; } = new Color(0xFF, 0xFA, 0xFA, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF00FF7F.
        /// </summary>
        public static Color SpringGreen { get; } = new Color(0x00, 0xFF, 0x7F, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF4682B4.
        /// </summary>
        public static Color SteelBlue { get; } = new Color(0x46, 0x82, 0xB4, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFD2B48C.
        /// </summary>
        public static Color Tan { get; } = new Color(0xD2, 0xB4, 0x8C, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF008080.
        /// </summary>
        public static Color Teal { get; } = new Color(0x00, 0x80, 0x80, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFD8BFD8.
        /// </summary>
        public static Color Thistle { get; } = new Color(0xD8, 0xBF, 0xD8, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFF6347.
        /// </summary>
        public static Color Tomato { get; } = new Color(0xFF, 0x63, 0x47, 0xFF);

        /// <summary>
        /// Gets a system-defined color.
        /// </summary>
        public static Color Transparent { get; } = new Color(0xff, 0xff, 0xff, 0x00);

        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF40E0D0.
        /// </summary>
        public static Color Turquoise { get; } = new Color(0x40, 0xE0, 0xD0, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFEE82EE.
        /// </summary>
        public static Color Violet { get; } = new Color(0xEE, 0x82, 0xEE, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFF5DEB3.
        /// </summary>
        public static Color Wheat { get; } = new Color(0xF5, 0xDE, 0xB3, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFFFFF.
        /// </summary>
        public static Color White { get; } = new Color(0xFF, 0xFF, 0xFF, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFF5F5F5.
        /// </summary>
        public static Color WhiteSmoke { get; } = new Color(0xF5, 0xF5, 0xF5, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFFF00.
        /// </summary>
        public static Color Yellow { get; } = new Color(0xFF, 0xFF, 0x00, 0xFF);


        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF9ACD32.
        /// </summary>
        public static Color YellowGreen { get; } = new Color(0x9A, 0xCD, 0x32, 0xFF);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the red value of this Color.
        /// </summary>
        public byte R { get; set; }

        /// <summary>
        /// Gets or sets the green value of this Color.
        /// </summary>
        public byte G { get; set; }

        /// <summary>
        /// Gets or sets the blue value of this Color.
        /// </summary>
        public byte B { get; set; }

        /// <summary>
        /// Gets or sets the alpha value of this Color.
        /// </summary>
        public byte A { get; set; }

        /// <summary>
        /// Gets the brightness of this Color.
        /// </summary>
        public float Brightness => 0.212655f * R + 0.715158f * G + 0.072187f * B;

        #endregion

        #region Methods

        /// <summary>
        /// Returns an Integer representation of this Color.
        /// </summary>
        /// <param name="colorFormat">The ColorFormat to use in the conversion.</param>
        /// <returns>An Integer representation of this Color.</returns>
        public int ToInteger(ColorFormat colorFormat)
        {
            unchecked
            {
                switch (colorFormat)
                {
                    case ColorFormat.ARGB:
                        return (((((A << 8) + R) << 8) + G) << 8) + B;
                    case ColorFormat.RGBA:
                        return (((((R << 8) + G) << 8) + B) << 8) + A;
                    case ColorFormat.RGB:
                        return (((R << 8) + G) << 8) + B;
                    default:
                        return 0;
                }
            }
        }

        /// <summary>
        /// Returns an Color representation of this Integer.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <param name="colorFormat">The ColorFormat to use in the conversion.</param>
        /// <returns>An Color representation of this Integer.</returns>
        public static Color FromInteger(uint color, ColorFormat colorFormat)
        {
            byte r = 0,
                g = 0,
                b = 0,
                a = 0;

            switch (colorFormat)
            {
                case ColorFormat.ARGB:
                    b = (byte) (color & 0xFF);
                    g = (byte) ((color >> 8) & 0xFF);
                    r = (byte) ((color >> 16) & 0xFF);
                    a = (byte) ((color >> 24) & 0xFF);
                    break;
                case ColorFormat.RGBA:
                    a = (byte) (color & 0xFF);
                    b = (byte) ((color >> 8) & 0xFF);
                    g = (byte) ((color >> 16) & 0xFF);
                    r = (byte) ((color >> 24) & 0xFF);
                    break;
                case ColorFormat.RGB:
                    b = (byte) (color & 0xFF);
                    g = (byte) ((color >> 8) & 0xFF);
                    r = (byte) ((color >> 16) & 0xFF);
                    a = 0xFF;
                    break;
            }

            return new Color(r, g, b, a);
        }

        /// <summary>
        /// Returns an Color representation of the specified integer.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <param name="colorFormat">The ColorFormat to use in the conversion.</param>
        /// <returns>A Color representation of the input.</returns>
        public static Color FromInteger(int color, ColorFormat colorFormat)
        {
            return FromInteger(unchecked((uint) color), colorFormat);
        }

        /// <summary>
        /// Returns an Color representation of the specified string.
        /// </summary>
        /// <param name="input">The color to convert.</param>
        /// <param name="colorFormat">The ColorFormat to use in the conversion.</param>
        /// <returns>A Color representation of the input.</returns>
        public static Color FromString(string input, ColorFormat colorFormat)
        {
            var hexLength = colorFormat == ColorFormat.RGB ? 6 : 8;
            var hexPattern = $@"^(?:0x)?[\dA-F]{{{hexLength}}}$";
            var isValidHexNumber = Regex.IsMatch(input, hexPattern, RegexOptions.IgnoreCase);

            return isValidHexNumber ? FromInteger(Convert.ToUInt32(input, 16), colorFormat) : White;
        }

        /// <summary>
        /// Returns a <see cref="string" /> representation of this Color.
        /// </summary>
        /// <param name="colorFormat">The format to use to convert the color to a string.</param>
        /// <returns>A <see cref="string" /> representation of this Color.</returns>
        public string ToString(ColorFormat colorFormat)
        {
            switch (colorFormat)
            {
                case ColorFormat.RGB:
                    return "{" + ToInteger(colorFormat).ToString("X6", CultureInfo.InvariantCulture) + "}";
                default:
                    return "{" + ToInteger(colorFormat).ToString("X8", CultureInfo.InvariantCulture) + "}";
            }
        }

        /// <summary>
        /// Returns an Color representation of the specified VehicleColor.
        /// </summary>
        /// <param name="vehicleColor">Color of vehicle</param>
        /// <returns>A Color representation of the input.</returns>
        public static Color FromVehicleColor(VehicleColor vehicleColor)
        {
            return FromInteger(VehicleColors[(int) vehicleColor], ColorFormat.RGBA);
        }

        /// <summary>
        /// Performs linear interpolation of <see cref="Color" />.
        /// </summary>
        /// <param name="value1">Source <see cref="Color" />.</param>
        /// <param name="value2">Destination <see cref="Color" />.</param>
        /// <param name="amount">Interpolation factor.</param>
        /// <param name="blendAlpha">Whether it also blends alpha.</param>
        /// <returns>Interpolated <see cref="Color" />.</returns>
        public static Color Lerp(Color value1, Color value2, float amount, bool blendAlpha = false)
        {
            amount = MathHelper.Clamp(amount, 0, 1);
            return new Color(
                (int) MathHelper.Lerp(value1.R, value2.R, amount),
                (int) MathHelper.Lerp(value1.G, value2.G, amount),
                (int) MathHelper.Lerp(value1.B, value2.B, amount),
                blendAlpha ? (int) MathHelper.Lerp(value1.A, value2.A, amount) : value1.A);
        }

        /// <summary>
        /// Returns this color darkened specified <paramref name="amount" />.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="blendAlpha">Whether it also blends alpha.</param>
        /// <returns>The darkened color.</returns>
        public Color Darken(float amount, bool blendAlpha = false)
        {
            return Lerp(this, Black, amount, blendAlpha);
        }

        /// <summary>
        /// Returns this color lightened specified <paramref name="amount" />.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="blendAlpha">Whether it also blends alpha.</param>
        /// <returns>The lightened color.</returns>
        public Color Lighten(float amount, bool blendAlpha = false)
        {
            return Lerp(this, White, amount, blendAlpha);
        }

        /// <summary>
        /// Returns this color with sRGB gamma correction added to it.
        /// </summary>
        /// <returns>The gamma corrected version of this color.</returns>
        public Color AddGammaCorrection()
        {
            var r = R / (float) byte.MaxValue;
            var g = G / (float) byte.MaxValue;
            var b = B / (float) byte.MaxValue;
            var a = A / (float) byte.MaxValue;
            const float power = 1.0f / 2.4f;

            return new Color(
                r <= 0.0031308f ? r * 12.92f : (float) Math.Pow(r, power) * 1.055f - 0.055f,
                g <= 0.0031308f ? g * 12.92f : (float) Math.Pow(g, power) * 1.055f - 0.055f,
                b <= 0.0031308f ? b * 12.92f : (float) Math.Pow(b, power) * 1.055f - 0.055f,
                a <= 0.0031308f ? a * 12.92f : (float) Math.Pow(a, power) * 1.055f - 0.055f
            );
        }

        /// <summary>
        /// Returns this color with sRGB gamma correction removed from it.
        /// </summary>
        /// <returns>The non-gamma corrected version of this color.</returns>
        public Color RemoveGammaCorrection()
        {
            var r = R / (float) byte.MaxValue;
            var g = G / (float) byte.MaxValue;
            var b = B / (float) byte.MaxValue;
            var a = A / (float) byte.MaxValue;

            return new Color(
                r <= 0.04045f ? r / 12.92f : (float) Math.Pow((r + 0.055f) / 1.055f, 2.4f),
                g <= 0.04045f ? g / 12.92f : (float) Math.Pow((g + 0.055f) / 1.055f, 2.4f),
                b <= 0.04045f ? b / 12.92f : (float) Math.Pow((b + 0.055f) / 1.055f, 2.4f),
                a <= 0.04045f ? a / 12.92f : (float) Math.Pow((a + 0.055f) / 1.055f, 2.4f)
            );
        }

        /// <summary>
        /// Returns the grayscaled version of this color.
        /// </summary>
        /// <returns>The grayscaled version of this color.</returns>
        public Color Grayscale()
        {
            return new Color(Brightness, Brightness, Brightness, A / (float) byte.MaxValue);
        }

        #endregion
        
        #region Overrides of ValueType

        /// <summary>
        /// Returns a <see cref="string" /> representation of this Color.
        /// </summary>
        /// <returns>A <see cref="string" /> representation of this Color.</returns>
        public override string ToString()
        {
            return ToString(ColorFormat.RGB);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Cast a Color to an integer.
        /// </summary>
        /// <param name="value">The Color to cast to an integer.</param>
        /// <returns>The resulting integer.</returns>
        public static implicit operator int(Color value)
        {
            return value.ToInteger(ColorFormat.RGBA); //Default format
        }

        /// <summary>
        /// Cast an integer to a Color.
        /// </summary>
        /// <param name="value">The integer to cast to a Color.</param>
        /// <returns>The resulting Color.</returns>
        public static implicit operator Color(int value)
        {
            return new Color(value);
        }

        /// <summary>
        /// Cast an unsigned integer to a Color.
        /// </summary>
        /// <param name="value">The unsigned integer to cast to a Color.</param>
        /// <returns>The resulting Color.</returns>
        public static implicit operator Color(uint value)
        {
            return new Color(value);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The left color.</param>
        /// <param name="b">The right color.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Color a, Color b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="a">The left color.</param>
        /// <param name="b">The right color.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Color a, Color b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Color operator *(Color value, float scale)
        {
            return new Color((int) (value.R * scale), (int) (value.G * scale), (int) (value.B * scale),
                (int) (value.A * scale));
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="Color" /> to <see cref="Vector3" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static explicit operator Vector3(Color value)
        {
            return new Vector3((float)value.R / byte.MaxValue, (float)value.G / byte.MaxValue, (float)value.B / byte.MaxValue);
        }

        #endregion

        #region Equality members

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="other" /> and this instance represent the same value; otherwise, false.
        /// </returns>
        /// <param name="other">Another object to compare to. </param>
        public bool Equals(Color other)
        {
            return R == other.R && G == other.G && B == other.B && A == other.A;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">Another object to compare to. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Color color && Equals(color);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                // ReSharper disable NonReadonlyMemberInGetHashCode
                var hashCode = R.GetHashCode();
                hashCode = (hashCode * 397) ^ G.GetHashCode();
                hashCode = (hashCode * 397) ^ B.GetHashCode();
                hashCode = (hashCode * 397) ^ A.GetHashCode();
                // ReSharper restore NonReadonlyMemberInGetHashCode
                return hashCode;
            }
        }

        #endregion
    }
}