namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides extension methods for the <see cref="BodyPart"/> enum.
/// </summary>
public static class BodyPartExtensions
{
    private static readonly string[] _names =
    [
        "invalid",
        "invalid",
        "invalid",
        "torso",
        "groin",
        "left arm",
        "right arm",
        "left leg",
        "right leg",
        "head"
    ];

    /// <summary>
    /// Gets the name of the specified <see cref="BodyPart"/>.
    /// </summary>
    /// <param name="bodyPart">The body part to get the name for.</param>
    /// <returns>The name of the body part, or "invalid" if the body part is not valid.</returns>
    public static string GetName(this BodyPart bodyPart)
    {
        var number = (int)bodyPart;

        if (number < 0 || number >= _names.Length)
        {
            return "invalid";
        }

        return _names[number];
    }
}