using SampSharp.Entities;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Entities.Tests;

public class MethodResultTests
{
    [Fact]
    public void True_HasValueTrue()
    {
        MethodResult.True.Value.ShouldBeTrue();
    }

    [Fact]
    public void False_HasValueFalse()
    {
        MethodResult.False.Value.ShouldBeFalse();
    }

    [Fact]
    public void True_IsSingletonInstance()
    {
        MethodResult.True.ShouldBeSameAs(MethodResult.True);
    }

    [Fact]
    public void False_IsSingletonInstance()
    {
        MethodResult.False.ShouldBeSameAs(MethodResult.False);
    }

    [Fact]
    public void From_True_ReturnsTrueSingleton()
    {
        var result = MethodResult.From(true);
        result.ShouldBeSameAs(MethodResult.True);
    }

    [Fact]
    public void From_False_ReturnsFalseSingleton()
    {
        var result = MethodResult.From(false);
        result.ShouldBeSameAs(MethodResult.False);
    }
}
