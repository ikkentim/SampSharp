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

namespace GameMode.World
{
    /// <summary>
    ///     Represents a hexidecimal color.
    /// </summary>
    public struct Color
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the Color struct.
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
        ///     Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="r">The red value of this Color.</param>
        /// <param name="g">The red value of this Color.</param>
        /// <param name="b">The red value of this Color.</param>
        public Color(byte r, byte g, byte b)
            : this()
        {
            R = r;
            G = g;
            B = b;
        }


        /// <summary>
        ///     Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="color">The Color values to use for this Color.</param>
        public Color(Color color)
            : this()
        {
            R = color.R;
            G = color.G;
            B = color.B;
        }

        /// <summary>
        ///     Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="color">The Color values to use for this Color.</param>
        public Color(int color)
            : this()
        {
            Color c = GetColorFromValue(color, ColorFormat.RGBA); //Default format
            R = c.R;
            G = c.G;
            B = c.B;
        }

        /// <summary>
        ///     Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="color">The Color values to use for this Color.</param>
        public Color(uint color)
            : this()
        {
            Color c = GetColorFromValue(color, ColorFormat.RGBA); //Default format
            R = c.R;
            G = c.G;
            B = c.B;
        }

        #endregion

        #region Defaults

        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFF0F8FF.
        /// </summary>
        public static Color AliceBlue
        {
            get { return new Color(0xF0, 0xF8, 0xFF, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFAEBD7.
        /// </summary>
        public static Color AntiqueWhite
        {
            get { return new Color(0xFA, 0xEB, 0xD7, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF00FFFF.
        /// </summary>
        public static Color Aqua
        {
            get { return new Color(0x00, 0xFF, 0xFF, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF7FFFD4.
        /// </summary>
        public static Color Aquamarine
        {
            get { return new Color(0x7F, 0xFF, 0xD4, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFF0FFFF.
        /// </summary>
        public static Color Azure
        {
            get { return new Color(0xF0, 0xFF, 0xFF, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFF5F5DC.
        /// </summary>
        public static Color Beige
        {
            get { return new Color(0xF5, 0xF5, 0xDC, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFE4C4.
        /// </summary>
        public static Color Bisque
        {
            get { return new Color(0xFF, 0xE4, 0xC4, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF000000.
        /// </summary>
        public static Color Black
        {
            get { return new Color(0x00, 0x00, 0x00, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFEBCD.
        /// </summary>
        public static Color BlanchedAlmond
        {
            get { return new Color(0xFF, 0xEB, 0xCD, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF0000FF.
        /// </summary>
        public static Color Blue
        {
            get { return new Color(0x00, 0x00, 0xFF, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF8A2BE2.
        /// </summary>
        public static Color BlueViolet
        {
            get { return new Color(0x8A, 0x2B, 0xE2, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFA52A2A.
        /// </summary>
        public static Color Brown
        {
            get { return new Color(0xA5, 0x2A, 0x2A, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFDEB887.
        /// </summary>
        public static Color BurlyWood
        {
            get { return new Color(0xDE, 0xB8, 0x87, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF5F9EA0.
        /// </summary>
        public static Color CadetBlue
        {
            get { return new Color(0x5F, 0x9E, 0xA0, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF7FFF00.
        /// </summary>
        public static Color Chartreuse
        {
            get { return new Color(0x7F, 0xFF, 0x00, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFD2691E.
        /// </summary>
        public static Color Chocolate
        {
            get { return new Color(0xD2, 0x69, 0x1E, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFF7F50.
        /// </summary>
        public static Color Coral
        {
            get { return new Color(0xFF, 0x7F, 0x50, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF6495ED.
        /// </summary>
        public static Color CornflowerBlue
        {
            get { return new Color(0x64, 0x95, 0xED, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFF8DC.
        /// </summary>
        public static Color Cornsilk
        {
            get { return new Color(0xFF, 0xF8, 0xDC, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFDC143C.
        /// </summary>
        public static Color Crimson
        {
            get { return new Color(0xDC, 0x14, 0x3C, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF00FFFF.
        /// </summary>
        public static Color Cyan
        {
            get { return new Color(0x00, 0xFF, 0xFF, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF00008B.
        /// </summary>
        public static Color DarkBlue
        {
            get { return new Color(0x00, 0x00, 0x8B, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF008B8B.
        /// </summary>
        public static Color DarkCyan
        {
            get { return new Color(0x00, 0x8B, 0x8B, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFB8860B.
        /// </summary>
        public static Color DarkGoldenrod
        {
            get { return new Color(0xB8, 0x86, 0x0B, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFA9A9A9.
        /// </summary>
        public static Color DarkGray
        {
            get { return new Color(0xA9, 0xA9, 0xA9, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF006400.
        /// </summary>
        public static Color DarkGreen
        {
            get { return new Color(0x00, 0x64, 0x00, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFBDB76B.
        /// </summary>
        public static Color DarkKhaki
        {
            get { return new Color(0xBD, 0xB7, 0x6B, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF8B008B.
        /// </summary>
        public static Color DarkMagenta
        {
            get { return new Color(0x8B, 0x00, 0x8B, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF556B2F.
        /// </summary>
        public static Color DarkOliveGreen
        {
            get { return new Color(0x55, 0x6B, 0x2F, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFF8C00.
        /// </summary>
        public static Color DarkOrange
        {
            get { return new Color(0xFF, 0x8C, 0x00, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF9932CC.
        /// </summary>
        public static Color DarkOrchid
        {
            get { return new Color(0x99, 0x32, 0xCC, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF8B0000.
        /// </summary>
        public static Color DarkRed
        {
            get { return new Color(0x8B, 0x00, 0x00, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFE9967A.
        /// </summary>
        public static Color DarkSalmon
        {
            get { return new Color(0xE9, 0x96, 0x7A, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF8FBC8F.
        /// </summary>
        public static Color DarkSeaGreen
        {
            get { return new Color(0x8F, 0xBC, 0x8F, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF483D8B.
        /// </summary>
        public static Color DarkSlateBlue
        {
            get { return new Color(0x48, 0x3D, 0x8B, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF2F4F4F.
        /// </summary>
        public static Color DarkSlateGray
        {
            get { return new Color(0x2F, 0x4F, 0x4F, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF00CED1.
        /// </summary>
        public static Color DarkTurquoise
        {
            get { return new Color(0x00, 0xCE, 0xD1, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF9400D3.
        /// </summary>
        public static Color DarkViolet
        {
            get { return new Color(0x94, 0x00, 0xD3, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFF1493.
        /// </summary>
        public static Color DeepPink
        {
            get { return new Color(0xFF, 0x14, 0x93, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF00BFFF.
        /// </summary>
        public static Color DeepSkyBlue
        {
            get { return new Color(0x00, 0xBF, 0xFF, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF696969.
        /// </summary>
        public static Color DimGray
        {
            get { return new Color(0x69, 0x69, 0x69, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF1E90FF.
        /// </summary>
        public static Color DodgerBlue
        {
            get { return new Color(0x1E, 0x90, 0xFF, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFB22222.
        /// </summary>
        public static Color Firebrick
        {
            get { return new Color(0xB2, 0x22, 0x22, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFFAF0.
        /// </summary>
        public static Color FloralWhite
        {
            get { return new Color(0xFF, 0xFA, 0xF0, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF228B22.
        /// </summary>
        public static Color ForestGreen
        {
            get { return new Color(0x22, 0x8B, 0x22, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFF00FF.
        /// </summary>
        public static Color Fuchsia
        {
            get { return new Color(0xFF, 0x00, 0xFF, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFDCDCDC.
        /// </summary>
        public static Color Gainsboro
        {
            get { return new Color(0xDC, 0xDC, 0xDC, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFF8F8FF.
        /// </summary>
        public static Color GhostWhite
        {
            get { return new Color(0xF8, 0xF8, 0xFF, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFD700.
        /// </summary>
        public static Color Gold
        {
            get { return new Color(0xFF, 0xD7, 0x00, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFDAA520.
        /// </summary>
        public static Color Goldenrod
        {
            get { return new Color(0xDA, 0xA5, 0x20, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF808080.
        /// </summary>
        public static Color Gray
        {
            get { return new Color(0x80, 0x80, 0x80, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF008000.
        /// </summary>
        public static Color Green
        {
            get { return new Color(0x00, 0x80, 0x00, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFADFF2F.
        /// </summary>
        public static Color GreenYellow
        {
            get { return new Color(0xAD, 0xFF, 0x2F, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFF0FFF0.
        /// </summary>
        public static Color Honeydew
        {
            get { return new Color(0xF0, 0xFF, 0xF0, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFF69B4.
        /// </summary>
        public static Color HotPink
        {
            get { return new Color(0xFF, 0x69, 0xB4, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFCD5C5C.
        /// </summary>
        public static Color IndianRed
        {
            get { return new Color(0xCD, 0x5C, 0x5C, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF4B0082.
        /// </summary>
        public static Color Indigo
        {
            get { return new Color(0x4B, 0x00, 0x82, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFFFF0.
        /// </summary>
        public static Color Ivory
        {
            get { return new Color(0xFF, 0xFF, 0xF0, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFF0E68C.
        /// </summary>
        public static Color Khaki
        {
            get { return new Color(0xF0, 0xE6, 0x8C, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFE6E6FA.
        /// </summary>
        public static Color Lavender
        {
            get { return new Color(0xE6, 0xE6, 0xFA, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFF0F5.
        /// </summary>
        public static Color LavenderBlush
        {
            get { return new Color(0xFF, 0xF0, 0xF5, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF7CFC00.
        /// </summary>
        public static Color LawnGreen
        {
            get { return new Color(0x7C, 0xFC, 0x00, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFFACD.
        /// </summary>
        public static Color LemonChiffon
        {
            get { return new Color(0xFF, 0xFA, 0xCD, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFADD8E6.
        /// </summary>
        public static Color LightBlue
        {
            get { return new Color(0xAD, 0xD8, 0xE6, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFF08080.
        /// </summary>
        public static Color LightCoral
        {
            get { return new Color(0xF0, 0x80, 0x80, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFE0FFFF.
        /// </summary>
        public static Color LightCyan
        {
            get { return new Color(0xE0, 0xFF, 0xFF, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFAFAD2.
        /// </summary>
        public static Color LightGoldenrodYellow
        {
            get { return new Color(0xFA, 0xFA, 0xD2, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFD3D3D3.
        /// </summary>
        public static Color LightGray
        {
            get { return new Color(0xD3, 0xD3, 0xD3, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF90EE90.
        /// </summary>
        public static Color LightGreen
        {
            get { return new Color(0x90, 0xEE, 0x90, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFB6C1.
        /// </summary>
        public static Color LightPink
        {
            get { return new Color(0xFF, 0xB6, 0xC1, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFA07A.
        /// </summary>
        public static Color LightSalmon
        {
            get { return new Color(0xFF, 0xA0, 0x7A, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF20B2AA.
        /// </summary>
        public static Color LightSeaGreen
        {
            get { return new Color(0x20, 0xB2, 0xAA, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF87CEFA.
        /// </summary>
        public static Color LightSkyBlue
        {
            get { return new Color(0x87, 0xCE, 0xFA, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF778899.
        /// </summary>
        public static Color LightSlateGray
        {
            get { return new Color(0x77, 0x88, 0x99, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFB0C4DE.
        /// </summary>
        public static Color LightSteelBlue
        {
            get { return new Color(0xB0, 0xC4, 0xDE, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFFFE0.
        /// </summary>
        public static Color LightYellow
        {
            get { return new Color(0xFF, 0xFF, 0xE0, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF00FF00.
        /// </summary>
        public static Color Lime
        {
            get { return new Color(0x00, 0xFF, 0x00, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF32CD32.
        /// </summary>
        public static Color LimeGreen
        {
            get { return new Color(0x32, 0xCD, 0x32, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFAF0E6.
        /// </summary>
        public static Color Linen
        {
            get { return new Color(0xFA, 0xF0, 0xE6, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFF00FF.
        /// </summary>
        public static Color Magenta
        {
            get { return new Color(0xFF, 0x00, 0xFF, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF800000.
        /// </summary>
        public static Color Maroon
        {
            get { return new Color(0x80, 0x00, 0x00, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF66CDAA.
        /// </summary>
        public static Color MediumAquamarine
        {
            get { return new Color(0x66, 0xCD, 0xAA, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF0000CD.
        /// </summary>
        public static Color MediumBlue
        {
            get { return new Color(0x00, 0x00, 0xCD, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFBA55D3.
        /// </summary>
        public static Color MediumOrchid
        {
            get { return new Color(0xBA, 0x55, 0xD3, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF9370DB.
        /// </summary>
        public static Color MediumPurple
        {
            get { return new Color(0x93, 0x70, 0xDB, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF3CB371.
        /// </summary>
        public static Color MediumSeaGreen
        {
            get { return new Color(0x3C, 0xB3, 0x71, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF7B68EE.
        /// </summary>
        public static Color MediumSlateBlue
        {
            get { return new Color(0x7B, 0x68, 0xEE, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF00FA9A.
        /// </summary>
        public static Color MediumSpringGreen
        {
            get { return new Color(0x00, 0xFA, 0x9A, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF48D1CC.
        /// </summary>
        public static Color MediumTurquoise
        {
            get { return new Color(0x48, 0xD1, 0xCC, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFC71585.
        /// </summary>
        public static Color MediumVioletRed
        {
            get { return new Color(0xC7, 0x15, 0x85, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF191970.
        /// </summary>
        public static Color MidnightBlue
        {
            get { return new Color(0x19, 0x19, 0x70, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFF5FFFA.
        /// </summary>
        public static Color MintCream
        {
            get { return new Color(0xF5, 0xFF, 0xFA, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFE4E1.
        /// </summary>
        public static Color MistyRose
        {
            get { return new Color(0xFF, 0xE4, 0xE1, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFE4B5.
        /// </summary>
        public static Color Moccasin
        {
            get { return new Color(0xFF, 0xE4, 0xB5, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFDEAD.
        /// </summary>
        public static Color NavajoWhite
        {
            get { return new Color(0xFF, 0xDE, 0xAD, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF000080.
        /// </summary>
        public static Color Navy
        {
            get { return new Color(0x00, 0x00, 0x80, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFDF5E6.
        /// </summary>
        public static Color OldLace
        {
            get { return new Color(0xFD, 0xF5, 0xE6, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF808000.
        /// </summary>
        public static Color Olive
        {
            get { return new Color(0x80, 0x80, 0x00, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF6B8E23.
        /// </summary>
        public static Color OliveDrab
        {
            get { return new Color(0x6B, 0x8E, 0x23, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFA500.
        /// </summary>
        public static Color Orange
        {
            get { return new Color(0xFF, 0xA5, 0x00, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFF4500.
        /// </summary>
        public static Color OrangeRed
        {
            get { return new Color(0xFF, 0x45, 0x00, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFDA70D6.
        /// </summary>
        public static Color Orchid
        {
            get { return new Color(0xDA, 0x70, 0xD6, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFEEE8AA.
        /// </summary>
        public static Color PaleGoldenrod
        {
            get { return new Color(0xEE, 0xE8, 0xAA, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF98FB98.
        /// </summary>
        public static Color PaleGreen
        {
            get { return new Color(0x98, 0xFB, 0x98, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFAFEEEE.
        /// </summary>
        public static Color PaleTurquoise
        {
            get { return new Color(0xAF, 0xEE, 0xEE, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFDB7093.
        /// </summary>
        public static Color PaleVioletRed
        {
            get { return new Color(0xDB, 0x70, 0x93, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFEFD5.
        /// </summary>
        public static Color PapayaWhip
        {
            get { return new Color(0xFF, 0xEF, 0xD5, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFDAB9.
        /// </summary>
        public static Color PeachPuff
        {
            get { return new Color(0xFF, 0xDA, 0xB9, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFCD853F.
        /// </summary>
        public static Color Peru
        {
            get { return new Color(0xCD, 0x85, 0x3F, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFC0CB.
        /// </summary>
        public static Color Pink
        {
            get { return new Color(0xFF, 0xC0, 0xCB, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFDDA0DD.
        /// </summary>
        public static Color Plum
        {
            get { return new Color(0xDD, 0xA0, 0xDD, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFB0E0E6.
        /// </summary>
        public static Color PowderBlue
        {
            get { return new Color(0xB0, 0xE0, 0xE6, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF800080.
        /// </summary>
        public static Color Purple
        {
            get { return new Color(0x80, 0x00, 0x80, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFF0000.
        /// </summary>
        public static Color Red
        {
            get { return new Color(0xFF, 0x00, 0x00, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFBC8F8F.
        /// </summary>
        public static Color RosyBrown
        {
            get { return new Color(0xBC, 0x8F, 0x8F, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF4169E1.
        /// </summary>
        public static Color RoyalBlue
        {
            get { return new Color(0x41, 0x69, 0xE1, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF8B4513.
        /// </summary>
        public static Color SaddleBrown
        {
            get { return new Color(0x8B, 0x45, 0x13, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFA8072.
        /// </summary>
        public static Color Salmon
        {
            get { return new Color(0xFA, 0x80, 0x72, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFF4A460.
        /// </summary>
        public static Color SandyBrown
        {
            get { return new Color(0xF4, 0xA4, 0x60, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF2E8B57.
        /// </summary>
        public static Color SeaGreen
        {
            get { return new Color(0x2E, 0x8B, 0x57, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFF5EE.
        /// </summary>
        public static Color SeaShell
        {
            get { return new Color(0xFF, 0xF5, 0xEE, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFA0522D.
        /// </summary>
        public static Color Sienna
        {
            get { return new Color(0xA0, 0x52, 0x2D, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFC0C0C0.
        /// </summary>
        public static Color Silver
        {
            get { return new Color(0xC0, 0xC0, 0xC0, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF87CEEB.
        /// </summary>
        public static Color SkyBlue
        {
            get { return new Color(0x87, 0xCE, 0xEB, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF6A5ACD.
        /// </summary>
        public static Color SlateBlue
        {
            get { return new Color(0x6A, 0x5A, 0xCD, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF708090.
        /// </summary>
        public static Color SlateGray
        {
            get { return new Color(0x70, 0x80, 0x90, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFFAFA.
        /// </summary>
        public static Color Snow
        {
            get { return new Color(0xFF, 0xFA, 0xFA, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF00FF7F.
        /// </summary>
        public static Color SpringGreen
        {
            get { return new Color(0x00, 0xFF, 0x7F, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF4682B4.
        /// </summary>
        public static Color SteelBlue
        {
            get { return new Color(0x46, 0x82, 0xB4, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFD2B48C.
        /// </summary>
        public static Color Tan
        {
            get { return new Color(0xD2, 0xB4, 0x8C, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF008080.
        /// </summary>
        public static Color Teal
        {
            get { return new Color(0x00, 0x80, 0x80, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFD8BFD8.
        /// </summary>
        public static Color Thistle
        {
            get { return new Color(0xD8, 0xBF, 0xD8, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFF6347.
        /// </summary>
        public static Color Tomato
        {
            get { return new Color(0xFF, 0x63, 0x47, 0xFF); }
        }

        /// <summary>
        ///     Gets a system-defined color.
        /// </summary>
        public static Color Transparent
        {
            get { return new Color(0x00, 0x00, 0x00, 0x00); }
        }

        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF40E0D0.
        /// </summary>
        public static Color Turquoise
        {
            get { return new Color(0x40, 0xE0, 0xD0, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFEE82EE.
        /// </summary>
        public static Color Violet
        {
            get { return new Color(0xEE, 0x82, 0xEE, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFF5DEB3.
        /// </summary>
        public static Color Wheat
        {
            get { return new Color(0xF5, 0xDE, 0xB3, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFFFFF.
        /// </summary>
        public static Color White
        {
            get { return new Color(0xFF, 0xFF, 0xFF, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFF5F5F5.
        /// </summary>
        public static Color WhiteSmoke
        {
            get { return new Color(0xF5, 0xF5, 0xF5, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FFFFFF00.
        /// </summary>
        public static Color Yellow
        {
            get { return new Color(0xFF, 0xFF, 0x00, 0xFF); }
        }


        /// <summary>
        ///     Gets a system-defined color that has an ARGB value of #FF9ACD32.
        /// </summary>
        public static Color YellowGreen
        {
            get { return new Color(0x9A, 0xCD, 0x32, 0xFF); }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the red value of this Color.
        /// </summary>
        public byte R { get; set; }

        /// <summary>
        ///     Gets or sets the green value of this Color.
        /// </summary>
        public byte G { get; set; }

        /// <summary>
        ///     Gets or sets the blue value of this Color.
        /// </summary>
        public byte B { get; set; }

        /// <summary>
        ///     Gets or sets the alpha value of this Color.
        /// </summary>
        public byte A { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Returns an Integer representation of this Color.
        /// </summary>
        /// <param name="colorFormat">The ColorFormat to use in the conversion.</param>
        /// <returns>An Integer representation of this Color.</returns>
        public int GetColorValue(ColorFormat colorFormat)
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
        ///     Returns an Color representation of this Integer.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <param name="colorFormat">The ColorFormat to use in the conversion.</param>
        /// <returns>An Color representation of this Integer.</returns>
        public static Color GetColorFromValue(uint color, ColorFormat colorFormat)
        {
            byte r = 0,
                g = 0,
                b = 0,
                a = 0;

            switch (colorFormat)
            {
                case ColorFormat.ARGB:
                    b = (byte) (color & 0xFF);
                    g = (byte) ((color >>= 8) & 0xFF);
                    r = (byte) ((color >>= 8) & 0xFF);
                    a = (byte) ((color >> 8) & 0xFF);
                    break;
                case ColorFormat.RGBA:
                    a = (byte) (color & 0xFF);
                    b = (byte) ((color >>= 8) & 0xFF);
                    g = (byte) ((color >>= 8) & 0xFF);
                    r = (byte) ((color >> 8) & 0xFF);
                    break;
                case ColorFormat.RGB:
                    b = (byte) (color & 0xFF);
                    g = (byte) ((color >>= 8) & 0xFF);
                    r = (byte) ((color >> 8) & 0xFF);
                    break;
            }
            return new Color(r, g, b, a);
        }

        /// <summary>
        ///     Returns an Color representation of this Integer.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <param name="colorFormat">The ColorFormat to use in the conversion.</param>
        /// <returns>An Color representation of this Integer.</returns>
        public static Color GetColorFromValue(int color, ColorFormat colorFormat)
        {
            return GetColorFromValue(unchecked((uint) color), colorFormat);
        }

        public static implicit operator int(Color color)
        {
            return color.GetColorValue(ColorFormat.RGBA); //Default format
        }

        public static implicit operator Color(int color)
        {
            return new Color(color);
        }

        public static implicit operator Color(uint color)
        {
            return new Color(color);
        }

        /// <summary>
        ///     Returns a String representation of this Color.
        /// </summary>
        /// <param name="colorFormat">The format to use to convert the color to a string.</param>
        /// <returns>A String representation of this Color.</returns>
        public string ToString(ColorFormat colorFormat)
        {
            switch (colorFormat)
            {
                case ColorFormat.RGB:
                    return "{" + GetColorValue(colorFormat).ToString("X6") + "}";
                default:
                    return "{" + GetColorValue(colorFormat).ToString("X8") + "}";
            }
        }

        /// <summary>
        ///     Returns a String representation of this Color.
        /// </summary>
        /// <returns>A String representation of this Color.</returns>
        public override string ToString()
        {
            return "{" + GetColorValue(ColorFormat.RGB).ToString("X6") + "}";
        }

        #endregion
    }
}