using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Core;

/// <summary>
/// Tests for DispatchResult and DispatchResponse, which represent the outcome of a command dispatch.
/// </summary>
public class DispatchResultTests
{
    [Fact]
    public void CreateSuccess_ReturnsSuccessResponse()
    {
        var result = DispatchResult.CreateSuccess();

        result.Response.ShouldBe(DispatchResponse.Success);
    }

    [Fact]
    public void CreateNotFound_ReturnsCommandNotFoundResponse()
    {
        var result = DispatchResult.CreateNotFound();

        result.Response.ShouldBe(DispatchResponse.CommandNotFound);
    }

    [Fact]
    public void CreateInvalidArguments_ReturnsInvalidArgumentsResponse()
    {
        var result = DispatchResult.CreateInvalidArguments();

        result.Response.ShouldBe(DispatchResponse.InvalidArguments);
    }

    [Fact]
    public void CreatePermissionDenied_ReturnsPermissionDeniedResponse()
    {
        var result = DispatchResult.CreatePermissionDenied();

        result.Response.ShouldBe(DispatchResponse.PermissionDenied);
    }

    [Fact]
    public void CreateSuccess_CommandOverloadIsNullByDefault()
    {
        var result = DispatchResult.CreateSuccess();

        result.CommandOverload.ShouldBeNull();
    }

    [Fact]
    public void CreateSuccess_ParsedArgumentsIsNullByDefault()
    {
        var result = DispatchResult.CreateSuccess();

        result.ParsedArguments.ShouldBeNull();
    }

    [Fact]
    public void CreateSuccess_UsedCommandNameIsEmptyByDefault()
    {
        var result = DispatchResult.CreateSuccess();

        result.UsedCommandName.ShouldBe(string.Empty);
    }

    [Fact]
    public void UsedCommandName_CanBeSet()
    {
        var result = DispatchResult.CreateSuccess();
        result.UsedCommandName = "mycommand";

        result.UsedCommandName.ShouldBe("mycommand");
    }

    [Fact]
    public void ParsedArguments_CanBeSet()
    {
        var result = DispatchResult.CreateSuccess();
        result.ParsedArguments = ["hello", 42];

        result.ParsedArguments.ShouldNotBeNull();
        result.ParsedArguments.Length.ShouldBe(2);
        result.ParsedArguments[0].ShouldBe("hello");
        result.ParsedArguments[1].ShouldBe(42);
    }
}
