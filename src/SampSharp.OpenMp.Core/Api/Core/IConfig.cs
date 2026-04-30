using System.Runtime.InteropServices;
using SampSharp.OpenMp.Core.Std;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IConfig" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtensible))]
public readonly partial struct IConfig
{
    /// <summary>
    /// Get a variable as a string.
    /// </summary>
    /// <param name="key">The key of the variable.</param>
    /// <returns>The value of the variable or <see langword="null" /> if not found.</returns>
    // TODO: is it nullable?
    public partial string? GetString(string key);

    /// <summary>
    /// Get a variable as an integer.
    /// </summary>
    /// <param name="key">The key of the variable.</param>
    /// <returns>A pointer to the value.</returns>
    public partial BlittableRef<int> GetInt(string key);

    /// <summary>
    /// Get a variable as a float.
    /// </summary>
    /// <param name="key">The key of the variable.</param>
    /// <returns>A pointer to the value.</returns>
    public partial BlittableRef<float> GetFloat(string key);

    [OpenMpApiFunction("getStrings")]
    private partial Size GetStringsImpl(string key, SpanLite<StringView> output);

    private partial Size GetStringsCount(string key);

    /// <summary>
    /// Gets a list of strings.
    /// </summary>
    /// <param name="key">The key of the variable.</param>
    /// <returns>An array containing the string values.</returns>
    public unsafe string?[] GetStrings(string key)
    {
        var count = GetStringsCount(key);

        if (count.Value == 0)
        {
            return [];
        }

        var ptr = Marshal.AllocHGlobal(count.Value.ToInt32() * sizeof(StringView));

        try
        {
            var output = new SpanLite<StringView>((StringView*)ptr, count);
            GetStringsImpl(key, output);

            var result = new string?[count.Value.ToInt32()];
            var index = 0;
            foreach (var value in output.AsSpan())
            {
                result[index++] = value;
            }

            return result;
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }

    /// <summary>
    /// Gets the type of a variable.
    /// </summary>
    /// <param name="key">The key of the variable.</param>
    /// <returns>tHE type of the variable.</returns>
    [OpenMpApiFunction("getType")]
    public partial ConfigOptionType GetValueType(string key);

    /// <summary>
    /// Gets the number of bans.
    /// </summary>
    /// <returns>The number of bans.</returns>
    public partial Size GetBansCount();

    /// <summary>
    /// Gets the ban at the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index">The index of the ban.</param>
    /// <returns>The ban entry.</returns>
    public partial BanEntry? GetBan(Size index);

    /// <summary>
    /// Adds a ban.
    /// </summary>
    /// <param name="entry">The ban entry to add.</param>
    public partial void AddBan(BanEntry entry);
    
    /// <summary>
    /// Removes a ban.
    /// </summary>
    /// <param name="index">The index of the ban to remove.</param>
    [OpenMpApiOverload("_index")]
    public partial void RemoveBan(Size index);

    /// <summary>
    /// Removes a ban.
    /// </summary>
    /// <param name="entry">The ban entry to remove.</param>
    public partial void RemoveBan(BanEntry entry);

    /// <summary>
    /// Writes the bans to the file.
    /// </summary>
    public partial void WriteBans();

    /// <summary>
    /// Reloads the bans.
    /// </summary>
    public partial void ReloadBans();

    /// <summary>
    /// Clears all bans.
    /// </summary>
    public partial void ClearBans();

    /// <summary>
    /// Checks if a ban entry is banned.
    /// </summary>
    /// <param name="entry">The ban entry to check.</param>
    /// <returns><see langword="true" /> if the entry is banned; otherwise, <see langword="false" />.</returns>
    public partial bool IsBanned(BanEntry entry);

    private partial void GetNameFromAlias(string alias, out Pair<BlittableBoolean, StringView> result);

    /// <summary>
    /// Get an option name from an alias if available.
    /// </summary>
    /// <param name="alias">The alias to find.</param>
    /// <returns>A pair of bool which is true if the alias is deprecated and a string with the real config name.</returns>
    public (bool, string?) GetNameFromAlias(string alias)
    {
        GetNameFromAlias(alias, out var pair);
        return (pair.First, pair.Second);
    }

    // TODO: public partial void enumOptions(OptionEnumeratorCallback& callback); // enumerator callback not available

    /// <summary>
    /// Get a variable as a boolean.
    /// </summary>
    /// <param name="key">The key of the variable.</param>
    /// <returns>A pointer to the value.</returns>
    public partial BlittableRef<bool> GetBool(string key);
}