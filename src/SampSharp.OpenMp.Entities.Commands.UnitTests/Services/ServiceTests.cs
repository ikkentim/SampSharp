using System.Reflection;
using FluentAssertions;
using SampSharp.Entities.SAMP.Commands.Core;
using SampSharp.Entities.SAMP.Commands.Services;
using Xunit;

namespace SampSharp.Entities.SAMP.Commands.Tests.Services;

public class DefaultPermissionCheckerTests
{
    [Fact]
    public void HasPermission_AlwaysReturnsTrue()
    {
        var checker = new DefaultPermissionChecker();
        var result = checker.HasPermission(new EntityId(1), new[] { "admin" });
        result.Should().BeTrue();
    }

    [Fact]
    public void HasPermission_WithMultiplePermissions_ReturnsTrue()
    {
        var checker = new DefaultPermissionChecker();
        var result = checker.HasPermission(new EntityId(1), new[] { "admin", "moderator", "user" });
        result.Should().BeTrue();
    }

    [Fact]
    public void HasPermission_WithNoPermissions_ReturnsTrue()
    {
        var checker = new DefaultPermissionChecker();
        var result = checker.HasPermission(new EntityId(1), new string[0]);
        result.Should().BeTrue();
    }
}

public class DefaultCommandNotFoundHandlerTests
{
    private readonly DefaultCommandNotFoundHandler _handler;

    public DefaultCommandNotFoundHandlerTests()
    {
        _handler = new DefaultCommandNotFoundHandler();
    }

    [Fact]
    public void GetCommandNotFoundMessage_ReturnsMessage()
    {
        var message = _handler.GetCommandNotFoundMessage();
        message.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void GetInvalidArgumentsMessage_ReturnsMessage()
    {
        var message = _handler.GetInvalidArgumentsMessage("kick");
        message.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void GetPermissionDeniedMessage_ReturnsMessage()
    {
        var message = _handler.GetPermissionDeniedMessage("admin");
        message.Should().NotBeNullOrWhiteSpace();
    }
}

public class DefaultCommandNameProviderTests
{
    private readonly DefaultCommandNameProvider _provider;

    public DefaultCommandNameProviderTests()
    {
        _provider = new DefaultCommandNameProvider();
    }

    [Fact]
    public void GetUsageMessage_SingleParameter_FormatsCorrectly()
    {
        var parameters = new[] {
            new CommandParameter("player", typeof(string), isRequired: true, null)
        };
        var message = _provider.GetUsageMessage("kick", null, parameters, null);
        message.Should().Contain("kick");
        message.Should().Contain("player");
    }

    [Fact]
    public void GetUsageMessage_WithGroup_IncludesGroup()
    {
        var group = new CommandGroup("admin");
        var parameters = new CommandParameter[0];
        var message = _provider.GetUsageMessage("ban", group, parameters, null);
        message.Should().Contain("admin");
    }

    [Fact]
    public void GetUsageMessage_NoParameters_FormatsCorrectly()
    {
        var message = _provider.GetUsageMessage("reload", null, new CommandParameter[0], null);
        message.Should().Contain("reload");
    }
}

public class DefaultCommandHelpProviderTests
{
    private readonly DefaultCommandHelpProvider _provider;
    private readonly CommandRegistry _registry;

    public DefaultCommandHelpProviderTests()
    {
        _registry = new CommandRegistry();
        _provider = new DefaultCommandHelpProvider(_registry);
    }

    [Fact]
    public void GetAllCommands_EmptyRegistry_ReturnsEmpty()
    {
        var commands = _provider.GetAllCommands();
        commands.Should().HaveCount(0);
    }

    [Fact]
    public void GetAllCommands_WithRegisteredCommand_ReturnsCommand()
    {
        var testMethod = typeof(DefaultCommandHelpProviderTests).GetMethod(
            nameof(DummyCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, typeof(void), new CommandParameter[0]);
        var definition = new CommandDefinition("test", null, new[] { overload }, null, true, false);
        _registry.Register(definition);

        var commands = _provider.GetAllCommands();
        commands.Should().HaveCount(1);
    }

    [Fact]
    public void GetCommandGroups_EmptyRegistry_ReturnsEmpty()
    {
        var groups = _provider.GetCommandGroups();
        groups.Should().HaveCount(0);
    }

    [Fact]
    public void GetCommandsInGroup_WithGroup_ReturnsCommands()
    {
        var group = new CommandGroup("admin");
        var testMethod = typeof(DefaultCommandHelpProviderTests).GetMethod(
            nameof(DummyCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, typeof(void), new CommandParameter[0]);
        var definition = new CommandDefinition("ban", group, new[] { overload }, null, true, false);
        _registry.Register(definition);

        var commands = _provider.GetCommandsInGroup(group);
        commands.Should().HaveCount(1);
        commands.First().Name.Should().Be("ban");
    }

    [Fact]
    public void SearchCommands_MatchingName_ReturnsCommand()
    {
        var testMethod = typeof(DefaultCommandHelpProviderTests).GetMethod(
            nameof(DummyCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, typeof(void), new CommandParameter[0]);
        var definition = new CommandDefinition("kick", null, new[] { overload }, null, true, false);
        _registry.Register(definition);

        var results = _provider.SearchCommands("kick");
        results.Should().HaveCount(1);
    }

    [Fact]
    public void FindCommand_ExistingCommand_ReturnsCommand()
    {
        var testMethod = typeof(DefaultCommandHelpProviderTests).GetMethod(
            nameof(DummyCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, typeof(void), new CommandParameter[0]);
        var definition = new CommandDefinition("ban", null, new[] { overload }, null, true, false);
        _registry.Register(definition);

        var command = _provider.FindCommand("ban");
        command.Should().NotBeNull();
        command!.Name.Should().Be("ban");
    }

    [Fact]
    public void FindCommand_NonExistentCommand_ReturnsNull()
    {
        var command = _provider.FindCommand("nonexistent");
        command.Should().BeNull();
    }

    private void DummyCommand() { }
}
