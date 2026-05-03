using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>Provides typed access to the open.mp server config (<c>config.json</c> / runtime values).</summary>
public interface IConfigService
{
    /// <summary>Gets a string config value.</summary>
    /// <param name="key">The config key.</param>
    /// <returns>The value, or <see langword="null" /> if the key is missing or has a non-string type.</returns>
    string? GetString(string key);

    /// <summary>Gets an integer config value.</summary>
    /// <param name="key">The config key.</param>
    /// <returns>The value, or <see langword="null" /> if the key is missing or has a non-integer type.</returns>
    int? GetInt(string key);

    /// <summary>Gets a floating-point config value.</summary>
    /// <param name="key">The config key.</param>
    /// <returns>The value, or <see langword="null" /> if the key is missing or has a non-float type.</returns>
    float? GetFloat(string key);

    /// <summary>Gets a boolean config value.</summary>
    /// <param name="key">The config key.</param>
    /// <returns>The value, or <see langword="null" /> if the key is missing or has a non-boolean type.</returns>
    bool? GetBool(string key);

    /// <summary>Gets a string-array config value.</summary>
    /// <param name="key">The config key.</param>
    /// <returns>The value array; an empty array if the key is missing.</returns>
    string?[] GetStrings(string key);

    /// <summary>Gets the type of the value stored under the given key.</summary>
    /// <param name="key">The config key.</param>
    /// <returns>The value type.</returns>
    ConfigOptionType GetValueType(string key);

    /// <summary>
    /// Retrieves a read-only collection of available configuration options and their associated types.
    /// </summary>
    /// <returns>An <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the names and types of all available configuration
    /// options.</returns>
    IReadOnlyDictionary<string, ConfigOptionType> GetOptions();
}
