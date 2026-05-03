using System.Linq;
using System.Reflection;
using Shouldly;
using SampSharp.Entities.SAMP.Commands.Core;
using SampSharp.Entities.SAMP.Commands.Help;
using SampSharp.Entities.SAMP.Commands.Services;
using Xunit;

namespace SampSharp.Entities.SAMP.Commands.Tests.Help;

public class CommandEnumeratorTests
{
    private readonly CommandRegistry _registry;
    private readonly DefaultCommandEnumerator _enumerator;

    public CommandEnumeratorTests()
    {
        _registry = new CommandRegistry();
        _enumerator = new DefaultCommandEnumerator(_registry);
    }

    [Fact]
    public void GetAllCommands_EmptyRegistry_ReturnsEmpty()
    {
        var commands = _enumerator.GetAllCommands();
        commands.Count().ShouldBe(0);
    }

    [Fact]
    public void GetAllCommands_WithRegisteredCommand_ReturnsCommand()
    {
        var testMethod = typeof(CommandEnumeratorTests).GetMethod(
            nameof(TestCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandEnumeratorTests), new CommandParameterInfo[0]);
        var definition = new CommandDefinition("test", null, new[] { overload }, null, null, true, false);
        _registry.Register(definition);

        var commands = _enumerator.GetAllCommands();
        commands.Count().ShouldBe(1);
        commands.First().Name.ShouldBe("test");
    }

    [Fact]
    public void FindCommand_ExistingCommand_ReturnsCommand()
    {
        var testMethod = typeof(CommandEnumeratorTests).GetMethod(
            nameof(TestCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandEnumeratorTests), new CommandParameterInfo[0]);
        var definition = new CommandDefinition("kick", null, new[] { overload }, null, null, true, false);
        _registry.Register(definition);

        var command = _enumerator.FindCommand("kick");
        command.ShouldNotBeNull();
        command!.Name.ShouldBe("kick");
        command.IsPlayerCommand.ShouldBeTrue();
    }

    [Fact]
    public void FindCommand_NonExistentCommand_ReturnsNull()
    {
        var command = _enumerator.FindCommand("nonexistent");
        command.ShouldBeNull();
    }

    [Fact]
    public void GetCommandsInGroup_WithGroup_ReturnsCommands()
    {
        var group = new CommandGroup("admin");
        var testMethod = typeof(CommandEnumeratorTests).GetMethod(
            nameof(TestCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandEnumeratorTests), new CommandParameterInfo[0]);
        var definition = new CommandDefinition("ban", group, new[] { overload }, null, null, true, false);
        _registry.Register(definition);

        var commands = _enumerator.GetCommandsInGroup(group);
        commands.Count().ShouldBe(1);
        commands.First().Name.ShouldBe("ban");
    }

    [Fact]
    public void SearchCommands_MatchingName_ReturnsCommand()
    {
        var testMethod = typeof(CommandEnumeratorTests).GetMethod(
            nameof(TestCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandEnumeratorTests), new CommandParameterInfo[0]);
        var definition = new CommandDefinition("kick", null, new[] { overload }, null, null, true, false);
        _registry.Register(definition);

        var results = _enumerator.SearchCommands("kick");
        results.Count().ShouldBe(1);
    }

    [Fact]
    public void CommandEnumerator_IncludesHelpText()
    {
        var testMethod = typeof(CommandEnumeratorTests).GetMethod(
            nameof(TestCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandEnumeratorTests), new CommandParameterInfo[0]);
        var definition = new CommandDefinition("test", null, new[] { overload }, null, null, true, false);
        _registry.Register(definition);

        var command = _enumerator.FindCommand("test");
        command.ShouldNotBeNull();
        command!.HelpText.ShouldContain("test");
        command.HelpText.ShouldContain("Command:");
    }

    [Fact]
    public void CommandEnumerator_WithAliases_IncludesAliasesInHelp()
    {
        var testMethod = typeof(CommandEnumeratorTests).GetMethod(
            nameof(TestCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandEnumeratorTests), new CommandParameterInfo[0]);
        var aliases = new[] { new CommandAlias("k"), new CommandAlias("remove") };
        var definition = new CommandDefinition("kick", null, new[] { overload }, aliases, null, true, false);
        _registry.Register(definition);

        var command = _enumerator.FindCommand("kick");
        command.ShouldNotBeNull();
        command!.HelpText.ShouldContain("Aliases:");
    }

    private void TestCommand() { }
}

public class CommandGroupEnumeratorTests
{
    private readonly CommandRegistry _registry;
    private readonly DefaultCommandEnumerator _enumerator;

    public CommandGroupEnumeratorTests()
    {
        _registry = new CommandRegistry();
        _enumerator = new DefaultCommandEnumerator(_registry);
    }

    [Fact]
    public void GetAllCommandGroups_EmptyRegistry_ReturnsEmpty()
    {
        var groups = _enumerator.GetAllCommandGroups();
        groups.Count().ShouldBe(0);
    }

    [Fact]
    public void GetAllCommandGroups_WithGroupedCommand_ReturnsGroup()
    {
        var group = new CommandGroup("admin");
        var testMethod = typeof(CommandGroupEnumeratorTests).GetMethod(
            nameof(TestCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandGroupEnumeratorTests), new CommandParameterInfo[0]);
        var definition = new CommandDefinition("ban", group, new[] { overload }, null, null, true, false);
        _registry.Register(definition);

        var groups = _enumerator.GetAllCommandGroups();
        groups.Count().ShouldBe(1);
        groups.First().Name.ShouldContain("admin");
    }

    [Fact]
    public void CommandGroupEnumerator_IncludesCommands()
    {
        var group = new CommandGroup("admin");
        var testMethod = typeof(CommandGroupEnumeratorTests).GetMethod(
            nameof(TestCommand),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandGroupEnumeratorTests), new CommandParameterInfo[0]);
        var definition = new CommandDefinition("ban", group, new[] { overload }, null, null, true, false);
        _registry.Register(definition);

        var groups = _enumerator.GetAllCommandGroups();
        groups.First().Commands.Count().ShouldBe(1);
    }

    private void TestCommand() { }
}
