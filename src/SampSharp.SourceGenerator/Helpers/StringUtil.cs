namespace SampSharp.SourceGenerator.Helpers;

public static class StringUtil
{
    public static string FirstCharToLower(string value)
    {
        return $"{char.ToLowerInvariant(value[0])}{value.Substring(1)}";
    }
}