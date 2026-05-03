using System;
using System.Linq;
using System.Reflection;
using Shouldly;
using SampSharp.Entities.SAMP.Commands.Core;
using Xunit;

namespace SampSharp.Entities.SAMP.Commands.Tests.Core;

public class CommandGroupTests
{
    [Fact]
    public void Constructor_WithSinglePart_CreatesGroup()
    {
        var group = new CommandGroup("admin");
        group.Parts.Count.ShouldBe(1);
        group.Parts[0].ShouldBe("admin");
    }

    [Fact]
    public void Constructor_WithMultipleParts_CreatesHierarchicalGroup()
    {
        var parts = new[] { "admin", "player", "ban" };
        var group = new CommandGroup(parts);
        group.Parts.ShouldBe(parts);
    }

    [Fact]
    public void Equals_SamePartsAndOrder_ReturnsTrue()
    {
        var group1 = new CommandGroup(new[] { "admin", "player" });
        var group2 = new CommandGroup(new[] { "admin", "player" });
        group1.ShouldBe(group2);
    }

    [Fact]
    public void Equals_DifferentOrder_ReturnsFalse()
    {
        var group1 = new CommandGroup(new[] { "admin", "player" });
        var group2 = new CommandGroup(new[] { "player", "admin" });
        group1.ShouldNotBe(group2);
    }

    [Fact]
    public void ToString_ReturnsSpaceSeparatedParts()
    {
        var group = new CommandGroup(new[] { "admin", "player", "ban" });
        group.ToString().ShouldBe("admin player ban");
    }

    [Fact]
    public void Parent_SinglePart_ReturnsNull()
    {
        var group = new CommandGroup("admin");
        group.Depth.ShouldBe(1);
    }

    [Fact]
    public void Parent_MultipleParts_ReturnsParentGroup()
    {
        var group = new CommandGroup(new[] { "admin", "player", "ban" });
        var parent = group.GetParent(2);
        parent.Parts.ShouldBe(new[] { "admin", "player" });
    }
}

public class CommandAliasTests
{
    [Fact]
    public void Constructor_StoresValue()
    {
        var alias = new CommandAlias("kick");
        alias.Name.ShouldBe("kick");
    }

    [Fact]
    public void Equals_SameValue_ReturnsTrue()
    {
        var alias1 = new CommandAlias("kick");
        var alias2 = new CommandAlias("kick");
        alias1.ShouldBe(alias2);
    }

    [Fact]
    public void Equals_DifferentValue_ReturnsFalse()
    {
        var alias1 = new CommandAlias("kick");
        var alias2 = new CommandAlias("ban");
        alias1.ShouldNotBe(alias2);
    }

    [Fact]
    public void ToString_ReturnsValue()
    {
        var alias = new CommandAlias("kick");
        alias.ToString().ShouldBe("kick");
    }
}

public class CommandOverloadTests
{
    private readonly MethodInfo _testMethod;

    public CommandOverloadTests()
    {
        _testMethod = typeof(CommandOverloadTests).GetMethod(nameof(DummyCommand), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
    }

    [Fact]
    public void Constructor_StoresMethodInfo()
    {
        var overload = new CommandOverload(_testMethod, _testMethod.GetParameters(), typeof(CommandOverloadTests), new CommandParameterInfo[0]);
        overload.Method.ShouldBe(_testMethod);
    }

    [Fact]
    public void IsAsync_VoidReturn_ReturnsFalse()
    {
        var overload = new CommandOverload(_testMethod, _testMethod.GetParameters(), typeof(CommandOverloadTests), new CommandParameterInfo[0]);
        overload.IsAsync.ShouldBeFalse();
    }

    [Fact]
    public void IsAsync_TaskReturn_ReturnsTrue()
    {
        var taskMethod = typeof(CommandOverloadTests).GetMethod(nameof(DummyAsyncCommand), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
        var overload = new CommandOverload(taskMethod, taskMethod.GetParameters(), typeof(CommandOverloadTests), new CommandParameterInfo[0]);
        overload.IsAsync.ShouldBeTrue();
    }

    [Fact]
    public void IsAsync_TaskBoolReturn_ReturnsTrue()
    {
        var taskMethod = typeof(CommandOverloadTests).GetMethod(nameof(DummyAsyncBoolCommand), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
        var overload = new CommandOverload(taskMethod, taskMethod.GetParameters(), typeof(CommandOverloadTests), new CommandParameterInfo[0]);
        overload.IsAsync.ShouldBeTrue();
    }

    private void DummyCommand() { }

    private async System.Threading.Tasks.Task DummyAsyncCommand() { }

    private async System.Threading.Tasks.Task<bool> DummyAsyncBoolCommand() { return true; }
}

public class CommandRegistryTests
{
    private readonly MethodInfo _testMethod;

    public CommandRegistryTests()
    {
        _testMethod = typeof(CommandRegistryTests).GetMethod(
            nameof(DummyCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
    }

    [Fact]
    public void Register_AddsCommandDefinition()
    {
        var registry = new CommandRegistry();
        var overload = new CommandOverload(_testMethod, _testMethod.GetParameters(), typeof(CommandRegistryTests), new CommandParameterInfo[0]);
        var definition = new CommandDefinition(
            name: "test",
            group: null,
            overloads: new[] { overload },
            aliases: null,
            permissions: null,
            playerCommand: true,
            consoleCommand: false);

        registry.Register(definition);

        registry.TryFind("test").ShouldNotBeNull();
    }

    [Fact]
    public void TryFind_ExistingCommand_ReturnsDefinition()
    {
        var registry = new CommandRegistry();
        var overload = new CommandOverload(_testMethod, _testMethod.GetParameters(), typeof(CommandRegistryTests), new CommandParameterInfo[0]);
        var definition = new CommandDefinition("kick", null, new[] { overload }, null, null, true, false);
        registry.Register(definition);

        var found = registry.TryFind("kick");
        found.ShouldNotBeNull();
        found!.Name.ShouldBe("kick");
    }

    [Fact]
    public void TryFind_NonExistentCommand_ReturnsNull()
    {
        var registry = new CommandRegistry();
        registry.TryFind("nonexistent").ShouldBeNull();
    }

    [Fact]
    public void GetAll_ReturnsAllCommands()
    {
        var registry = new CommandRegistry();
        var overload = new CommandOverload(_testMethod, _testMethod.GetParameters(), typeof(CommandRegistryTests), new CommandParameterInfo[0]);
        registry.Register(new CommandDefinition("test1", null, new[] { overload }, null, null, true, false));
        registry.Register(new CommandDefinition("test2", null, new[] { overload }, null, null, true, false));

        registry.GetAll().Count().ShouldBe(2);
    }

    [Fact]
    public void GetPlayerCommands_ReturnsOnlyPlayerCommands()
    {
        var registry = new CommandRegistry();
        var overload = new CommandOverload(_testMethod, _testMethod.GetParameters(), typeof(CommandRegistryTests), new CommandParameterInfo[0]);
        registry.Register(new CommandDefinition("player_cmd", null, new[] { overload }, null, null, playerCommand: true, consoleCommand: false));
        registry.Register(new CommandDefinition("console_cmd", null, new[] { overload }, null, null, playerCommand: false, consoleCommand: true));

        var playerCommands = registry.GetPlayerCommands();
        playerCommands.Count().ShouldBe(1);
        playerCommands.First().Name.ShouldBe("player_cmd");
    }

    [Fact]
    public void GetConsoleCommands_ReturnsOnlyConsoleCommands()
    {
        var registry = new CommandRegistry();
        var overload = new CommandOverload(_testMethod, _testMethod.GetParameters(), typeof(CommandRegistryTests), new CommandParameterInfo[0]);
        registry.Register(new CommandDefinition("player_cmd", null, new[] { overload }, null, null, playerCommand: true, consoleCommand: false));
        registry.Register(new CommandDefinition("console_cmd", null, new[] { overload }, null, null, playerCommand: false, consoleCommand: true));

        var consoleCommands = registry.GetConsoleCommands();
        consoleCommands.Count().ShouldBe(1);
        consoleCommands.First().Name.ShouldBe("console_cmd");
    }

    private void DummyCommand() { }
}

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
