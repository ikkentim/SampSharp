namespace SampSharp.Entities.SAMP.Commands;

/// <summary>Parses an enum by integer value or by (case-insensitive) substring of name.</summary>
public class EnumParser : ICommandParameterParser
{
    private readonly Type _enumType;
    private readonly WordParser _wordParser = new();

    /// <summary>Initializes a new instance.</summary>
    /// <exception cref="ArgumentException">If <paramref name="enumType" /> is not an enum type.</exception>
    public EnumParser(Type enumType)
    {
        ArgumentNullException.ThrowIfNull(enumType);
        if (!enumType.IsEnum)
        {
            throw new ArgumentException("Type must be an enum", nameof(enumType));
        }

        _enumType = enumType;
    }

    /// <inheritdoc />
    public bool TryParse(IServiceProvider services, ref string inputText, out object? result)
    {
        if (!_wordParser.TryParse(services, ref inputText, out var sub) || sub is not string word)
        {
            result = null;
            return false;
        }

        if (int.TryParse(word, out var intWord) && Enum.IsDefined(_enumType, intWord))
        {
            result = Enum.ToObject(_enumType, intWord);
            return true;
        }

        var lowerWord = word.ToLowerInvariant();
        var names = Enum.GetNames(_enumType).Where(n => n.Contains(lowerWord, StringComparison.InvariantCultureIgnoreCase)).ToArray();

        if (names.Length > 1)
        {
            names = Enum.GetNames(_enumType).Where(n => n.Contains(word, StringComparison.Ordinal)).ToArray();
        }

        if (names.Length == 1)
        {
            result = Enum.Parse(_enumType, names[0]);
            return true;
        }

        result = null;
        return false;
    }
}