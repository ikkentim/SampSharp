using System.Runtime.InteropServices.Marshalling;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents an entry in the ban list.
/// </summary>
/// <param name="Address">The IP address of the banned player.</param>
/// <param name="Time">The time when the ban was issued.</param>
/// <param name="Name">The name of the banned player, if available.</param>
/// <param name="Reason">The reason for the ban, if provided.</param>
[NativeMarshalling(typeof(BanEntryMarshaller))]
public record BanEntry(string Address, DateTimeOffset Time, string? Name, string? Reason)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BanEntry"/> class with only an address.
    /// The ban time is set to the current UTC time, and the name and reason are left null.
    /// </summary>
    /// <param name="address">The IP address of the banned player.</param>
    public BanEntry(string address) : this(address, DateTimeOffset.UtcNow, null, null)
    {
    }
}
