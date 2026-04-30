using SampSharp.OpenMp.Core.Api;

namespace SampSharp.OpenMp.Core;

/// <summary>
/// This attribute marks a type which should be cloned by the source generator with a changed constant field value. This type is mainly used by <see cref="HybridString16" /> and related types.
/// </summary>
/// <param name="fieldName">The name of the field which should be updated.</param>
/// <param name="value">The updated value to set to the field.</param>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = true)]
public class NumberedTypeGeneratorAttribute(string fieldName, int value) : Attribute
{
    /// <summary>
    /// Gets the name of the field which should be updated.
    /// </summary>
    public string FieldName { get; } = fieldName;

    /// <summary>
    /// Gets the updated value to set to the field.
    /// </summary>
    public int Value { get; } = value;
}