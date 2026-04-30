namespace SampSharp.OpenMp.Core;

/// <summary>
/// Represents a blittable boolean value. That is, a boolean value that is represented in memory as a single byte.
/// </summary>
/// <param name="value">The boolean value</param>
public readonly struct BlittableBoolean(bool value) : IEquatable<BlittableBoolean>
{
    private const byte True = 1;
    private const byte False = 0;

    private readonly byte _data = value ? True : False;

    /// <inheritdoc />
    public bool Equals(BlittableBoolean other)
    {
        return (bool)this == (bool)other;
    }
    
    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return (obj is BlittableBoolean other && Equals(other)) || (obj is bool b && (bool)this == b);
    }
    
    /// <inheritdoc />
    public override int GetHashCode()
    {
        return ((bool)this).GetHashCode();
    }

    /// <summary>
    /// Compares two <see cref="BlittableBoolean" /> values for equality.
    /// </summary>
    /// <param name="lhs">The left hand side value.</param>
    /// <param name="rhs">The right hand side value.</param>
    /// <returns>The result of the comparison.</returns>
    public static bool operator ==(BlittableBoolean lhs, BlittableBoolean rhs)
    {
        return lhs.Equals(rhs);
    }

    /// <summary>
    /// Compares two <see cref="BlittableBoolean" /> values for inequality.
    /// </summary>
    /// <param name="lhs">The left hand side value.</param>
    /// <param name="rhs">The right hand side value.</param>
    /// <returns>The result of the comparison.</returns>
    public static bool operator !=(BlittableBoolean lhs, BlittableBoolean rhs)
    {
        return !lhs.Equals(rhs);
    }

    /// <summary>
    /// Implicitly converts a <see cref="BlittableBoolean" /> to a <see cref="bool" />.
    /// </summary>
    /// <param name="b">The value to convert.</param>
    public static implicit operator bool(BlittableBoolean b)
    {
        return  b._data != 0;
    }

    /// <summary>
    /// Implicitly converts a <see cref="bool" /> to a <see cref="BlittableBoolean" />.
    /// </summary>
    /// <param name="b">The value to convert.</param>
    public static implicit operator BlittableBoolean(bool b)
    {
        return new BlittableBoolean(b);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return  ((bool)this).ToString();
    }
}