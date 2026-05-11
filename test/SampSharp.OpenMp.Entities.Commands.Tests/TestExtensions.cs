using System.Collections.Generic;
using System.Linq;
using Shouldly;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests;

/// <summary>
/// Extension methods for test assertions.
/// </summary>
internal static class TestExtensions
{
    /// <summary>
    /// Asserts that a collection has the specified count.
    /// </summary>
    public static void ShouldHaveCount<T>(this IEnumerable<T> collection, int expectedCount)
    {
        collection.Count().ShouldBe(expectedCount);
    }

    /// <summary>
    /// Asserts that an array has the specified count.
    /// </summary>
    public static void ShouldHaveCount<T>(this T[] array, int expectedCount)
    {
        array.Length.ShouldBe(expectedCount);
    }

    /// <summary>
    /// Asserts that a read-only list has the specified count.
    /// </summary>
    public static void ShouldHaveCount<T>(this IReadOnlyList<T> list, int expectedCount)
    {
        list.Count.ShouldBe(expectedCount);
    }

    /// <summary>
    /// Asserts that a list has the specified count.
    /// </summary>
    public static void ShouldHaveCount<T>(this List<T> list, int expectedCount)
    {
        list.Count.ShouldBe(expectedCount);
    }

    /// <summary>
    /// Asserts that a read-only collection has the specified count.
    /// </summary>
    public static void ShouldHaveCount<T>(this IReadOnlyCollection<T> collection, int expectedCount)
    {
        collection.Count.ShouldBe(expectedCount);
    }

    /// <summary>
    /// Asserts that a dictionary has the specified count.
    /// </summary>
    public static void ShouldHaveCount<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dict, int expectedCount)
    {
        dict.Count.ShouldBe(expectedCount);
    }
}
