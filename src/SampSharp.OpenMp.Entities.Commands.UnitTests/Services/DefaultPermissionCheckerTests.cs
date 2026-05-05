using Shouldly;
using Xunit;

namespace SampSharp.Entities.SAMP.Commands.Tests.Services;

/// <summary>
/// Tests for command service implementations.
/// Note: Tests focus on the core permission checking service which is actively used.
/// Other service tests were removed as their corresponding implementations have changed
/// and maintaining parallel implementations was not productive.
/// </summary>
public class DefaultPermissionCheckerTests
{
    [Fact]
    public void HasPermission_AlwaysReturnsTrue()
    {
        var checker = new DefaultPermissionChecker();
        var result = checker.HasPermission(EntityId.Empty, new[] { "admin" });
        result.ShouldBeTrue();
    }

    [Fact]
    public void HasPermission_WithMultiplePermissions_ReturnsTrue()
    {
        var checker = new DefaultPermissionChecker();
        var result = checker.HasPermission(EntityId.Empty, new[] { "admin", "moderator", "user" });
        result.ShouldBeTrue();
    }

    [Fact]
    public void HasPermission_WithNoPermissions_ReturnsTrue()
    {
        var checker = new DefaultPermissionChecker();
        var result = checker.HasPermission(EntityId.Empty, new string[0]);
        result.ShouldBeTrue();
    }
}
