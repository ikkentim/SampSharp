using System;
using System.Linq;
using System.Reflection;
using Shouldly;
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
            BindingFlags.NonPublic | BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandEnumeratorTests), [], TestInvoker, 0);
        var definition = new CommandDefinition("test", null, [overload]);
        _registry.Register(definition);

        var commands = _enumerator.GetAllCommands().ToList();
        commands.Count.ShouldBe(1);
        commands.First().Name.ShouldBe("test");
    }

    [Fact]
    public void FindCommand_ExistingCommand_ReturnsCommand()
    {
        var testMethod = typeof(CommandEnumeratorTests).GetMethod(
            nameof(TestCommand),
            BindingFlags.NonPublic | BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandEnumeratorTests), [], TestInvoker, 0);
        var definition = new CommandDefinition("kick", null, [overload]);
        _registry.Register(definition);

        var command = _enumerator.FindCommand("kick");
        command.ShouldNotBeNull();
        command!.Name.ShouldBe("kick");
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
            BindingFlags.NonPublic | BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandEnumeratorTests), [], TestInvoker, 0);
        var definition = new CommandDefinition("give", group, [overload]);
        _registry.Register(definition);

        var commands = _enumerator.GetCommandsInGroup(group).ToList();
        commands.Count.ShouldBe(1);
        commands.First().Name.ShouldBe("give");
    }

    [Fact]
    public void SearchCommands_MatchingName_ReturnsCommand()
    {
        var testMethod = typeof(CommandEnumeratorTests).GetMethod(
            nameof(TestCommand),
            BindingFlags.NonPublic | BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandEnumeratorTests), [], TestInvoker, 0);
        var definition = new CommandDefinition("kick", null, [overload]);
        _registry.Register(definition);

        var results = _enumerator.SearchCommands("kick");
        results.Count().ShouldBe(1);
    }

    [Fact]
    public void CommandEnumerator_IncludesHelpText()
    {
        var testMethod = typeof(CommandEnumeratorTests).GetMethod(
            nameof(TestCommand),
            BindingFlags.NonPublic | BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandEnumeratorTests), [], TestInvoker, 0);
        var definition = new CommandDefinition("test", null, [overload]);
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
            BindingFlags.NonPublic | BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandEnumeratorTests), [], TestInvoker, 0);
        var aliases = new[] { new CommandAlias("k"), new CommandAlias("remove") };
        var definition = new CommandDefinition("kick", null, [overload], aliases);
        _registry.Register(definition);

        var command = _enumerator.FindCommand("kick");
        command.ShouldNotBeNull();
        command!.HelpText.ShouldContain("Aliases:");
    }

    private void TestCommand() { }

    private static object TestInvoker(object target, object[] args, IServiceProvider services, IEntityManager entityManager)
    {
        throw new InvalidOperationException();
    }
}