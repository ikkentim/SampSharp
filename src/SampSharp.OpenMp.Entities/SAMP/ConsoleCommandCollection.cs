using System.Collections;
using SampSharp.OpenMp.Core.RobinHood;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a collection of console commands.
/// </summary>
[EventParameter]
public class ConsoleCommandCollection(FlatHashSetStringView set) : IReadOnlyCollection<string>
{
    /// <summary>
    /// Gets the number of commands in this collection.
    /// </summary>
    public int Count => set.Count;

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection.</returns>
    public IEnumerator<string> GetEnumerator()
    {
        return set.Select(item => item ?? string.Empty).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Adds a command to this collection.
    /// </summary>
    /// <param name="command">The command to add to the collection.</param>
    public void Add(string command)
    {
        set.Emplace(command);
    }
}