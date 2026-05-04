using SampSharp.Entities.SAMP.Commands.Core;
using Shouldly;
using Xunit;

namespace SampSharp.Entities.SAMP.Commands.Tests.Core;

public class DispatchResultTests
{
    [Fact]
    public void CreateSuccess_CreatesSuccessResult()
    {
        var result = DispatchResult.CreateSuccess();
        result.Response.ShouldBe(DispatchResponse.Success);
    }

    [Fact]
    public void CreateNotFound_CreatesNotFoundResult()
    {
        var result = DispatchResult.CreateNotFound();
        result.Response.ShouldBe(DispatchResponse.CommandNotFound);
    }

    [Fact]
    public void CreateInvalidArguments_CreatesInvalidArgumentsResult()
    {
        var result = DispatchResult.CreateInvalidArguments("Usage: /kick <player>");
        result.Response.ShouldBe(DispatchResponse.InvalidArguments);
        result.UsageMessage.ShouldBe("Usage: /kick <player>");
    }

    [Fact]
    public void CreatePermissionDenied_CreatesPermissionDeniedResult()
    {
        var result = DispatchResult.CreatePermissionDenied("You don't have permission");
        result.Response.ShouldBe(DispatchResponse.PermissionDenied);
        result.Message.ShouldBe("You don't have permission");
    }

    [Fact]
    public void CreateError_CreatesErrorResult()
    {
        var result = DispatchResult.CreateError("An error occurred");
        result.Response.ShouldBe(DispatchResponse.Error);
        result.Message.ShouldBe("An error occurred");
    }
}