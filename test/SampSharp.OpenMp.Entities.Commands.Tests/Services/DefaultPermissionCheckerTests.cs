using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Services;

public class DefaultPermissionCheckerTests
{
    [Fact]
    public void DefaultPermissionChecker_AlwaysGrantsPermission()
    {
        var checker = new DefaultPermissionChecker();

        var result = checker.HasPermission(null!, null!);

        result.ShouldBeTrue();
    }

    [Fact]
    public void DefaultPermissionChecker_WithMultipleCalls_AlwaysGrantsPermission()
    {
        var checker = new DefaultPermissionChecker();

        for (int i = 0; i < 5; i++)
        {
            var result = checker.HasPermission(null!, null!);
            result.ShouldBeTrue();
        }
    }
}
