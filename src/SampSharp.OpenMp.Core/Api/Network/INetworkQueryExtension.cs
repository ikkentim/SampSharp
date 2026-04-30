namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="INetworkQueryExtension" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtension))]
public readonly partial struct INetworkQueryExtension
{
    /// <inheritdoc />
    public static UID ExtensionId => new(0xfd46e147ea474971);

    /// <summary>
    /// Adds a query rule to the server's query response.
    /// </summary>
    /// <param name="rule">The name of the rule.</param>
    /// <param name="value">The value of the rule.</param>
    /// <returns><c>true</c> if the rule was added successfully; otherwise, <c>false</c>.</returns>
    public partial bool AddRule(string rule, string value);

    /// <summary>
    /// Removes a query rule from the server's query response.
    /// </summary>
    /// <param name="rule">The name of the rule to remove.</param>
    /// <returns><c>true</c> if the rule was removed successfully; otherwise, <c>false</c>.</returns>
    public partial bool RemoveRule(string rule);

    /// <summary>
    /// Checks if a query rule is valid.
    /// </summary>
    /// <param name="rule">The name of the rule to check.</param>
    /// <returns><c>true</c> if the rule is valid; otherwise, <c>false</c>.</returns>
    public partial bool IsValidRule(string rule);
}