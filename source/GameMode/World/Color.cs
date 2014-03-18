namespace GameMode.World
{
    /// <summary>
    /// Represents a hexidecimal color.
    /// </summary>
    public struct Color
    {
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
        /// Gets or sets the ColorFormat to use when converting this number to an Integer.
        /// </summary>
        public ColorFormat ColorFormat { get; set; }

        /// <summary>
        /// Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="colorFormat">The red value of this Color.</param>
        public Color(ColorFormat colorFormat) : this()
        {
            ColorFormat = colorFormat;
        }

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
        }

        /// <summary>
        /// Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="r">The red value of this Color.</param>
        /// <param name="g">The red value of this Color.</param>
        /// <param name="b">The red value of this Color.</param>
        /// <param name="a">The red value of this Color.</param>
        /// <param name="colorFormat">The ColorFormat to use for this Color.</param>
        public Color(byte r, byte g, byte b, byte a, ColorFormat colorFormat)
            : this()
        {
            R = r;
            G = g;
            B = b;
            A = a;
            ColorFormat = colorFormat;
        }

        /// <summary>
        /// Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="r">The red value of this Color.</param>
        /// <param name="g">The red value of this Color.</param>
        /// <param name="b">The red value of this Color.</param>
        /// <param name="colorFormat">The ColorFormat to use for this Color.</param>
        public Color(byte r, byte g, byte b, ColorFormat colorFormat)
            : this()
        {
            R = r;
            G = g;
            B = b;
            ColorFormat = colorFormat;
        }

        /// <summary>
        /// Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="color">The Color values to use for this Color.</param>
        /// <param name="colorFormat">The ColorFormat to use for this Color.</param>
        public Color(Color color, ColorFormat colorFormat)
            : this()
        {
            R = color.R;
            G = color.G;
            B = color.B;
            ColorFormat = colorFormat;
        }

        /// <summary>
        /// Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="color">The Color values to use for this Color.</param>
        public Color(Color color)
            : this()
        {
            R = color.R;
            G = color.G;
            B = color.B;
            ColorFormat = color.ColorFormat;
        }

        /// <summary>
        /// Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="color">The Color values to use for this Color.</param>
        /// <param name="colorFormat">The ColorFormat to use for this Color.</param>
        public Color(int color, ColorFormat colorFormat)
            : this()
        {
            var c = GetColorFromValue(color, colorFormat);
            R = c.R;
            G = c.G;
            B = c.B;
            ColorFormat = c.ColorFormat;
        }

        /// <summary>
        /// Initializes a new instance of the Color struct.
        /// </summary>
        /// <param name="color">The Color values to use for this Color.</param>
        public Color(int color)
            : this()
        {
            var c = GetColorFromValue(color, ColorFormat.RGBA);//Default format
            R = c.R;
            G = c.G;
            B = c.B;
            ColorFormat = c.ColorFormat;
        }

        /// <summary>
        /// Returns an Integer representation of this Color.
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
                        return ((((A*0xFF) + R)*0xFF) + G)*0xFF + B;
                    case ColorFormat.RGBA:
                        return ((((R*0xFF) + G)*0xFF) + B)*0xFF + A;
                    case ColorFormat.RGB:
                        return (((R*0xFF) + G)*0xFF) + B;
                    default:
                        return 0;
                }
            }
        }

        /// <summary>
        /// Returns an Color representation of this Integer.
        /// </summary>
        /// <param name="color">The color to con</param>
        /// <param name="colorFormat">The ColorFormat to use in the conversion.</param>
        /// <returns>An Color representation of this Integer.</returns>
        public static Color GetColorFromValue(int color, ColorFormat colorFormat)
        {
            unchecked
            {
                switch (colorFormat)
                {
                    case ColorFormat.ARGB:
                        return new Color((byte)((color & 0xFF000000) >> 6), (byte)((color & 0xFF0000) >> 4), (byte)((color & 0xFF00) >> 2), (byte)(color & 0xFF), colorFormat);
                    case ColorFormat.RGBA:
                        return new Color((byte)((color & 0xFF0000) >> 4), (byte)((color & 0xFF00) >> 2), (byte)((color & 0xFF)), (byte)((color & 0xFF000000) >> 6), colorFormat);
                    case ColorFormat.RGB:
                        return new Color((byte)((color & 0xFF0000) >> 4), (byte)((color & 0xFF00) >> 2), (byte)((color & 0xFF)), colorFormat);
                    default:
                        return new Color(colorFormat);
                }
            }
        }

        /// <summary>
        /// Returns an Integer representation of this Color.
        /// </summary>
        /// <returns>An Integer representation of this Color.</returns>
        public int GetColorValue()
        {
            return GetColorValue(ColorFormat);
        }

        public static implicit operator int(Color color)
        {
            return color.GetColorValue();
        }

        public static implicit operator Color(int color)
        {
            return new Color(color);
        }

        /// <summary>
        /// Returns a String representation of this Color.
        /// </summary>
        /// <returns>A String representation of this Color.</returns>
        public override string ToString()
        {
            return "{" + GetColorValue().ToString("X") + "}";
        }
    }
}
