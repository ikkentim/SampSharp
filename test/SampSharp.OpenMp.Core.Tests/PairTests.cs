using SampSharp.OpenMp.Core.Std;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Core.Tests;

public class PairTests
{
    [Fact]
    public void Implicit_from_tuple_sets_components()
    {
        Pair<int, int> pair = (10, 20);
        pair.First.ShouldBe(10);
        pair.Second.ShouldBe(20);
    }

    [Fact]
    public void Implicit_to_tuple_returns_components()
    {
        Pair<int, int> pair = (3, 4);
        (int a, int b) = pair;
        a.ShouldBe(3);
        b.ShouldBe(4);
    }

    [Fact]
    public void Deconstruct_returns_components()
    {
        Pair<int, long> pair = (5, 99L);
        pair.Deconstruct(out var first, out var second);
        first.ShouldBe(5);
        second.ShouldBe(99L);
    }

    [Fact]
    public void ToString_formats_as_paren_pair()
    {
        Pair<int, int> pair = (1, 2);
        pair.ToString().ShouldBe("(1, 2)");
    }

    [Fact]
    public void Implicit_tuple_conversion_preserves_mixed_types()
    {
        Pair<byte, long> pair = ((byte)7, 12345L);
        ValueTuple<byte, long> t = pair;
        t.Item1.ShouldBe((byte)7);
        t.Item2.ShouldBe(12345L);
    }
}
