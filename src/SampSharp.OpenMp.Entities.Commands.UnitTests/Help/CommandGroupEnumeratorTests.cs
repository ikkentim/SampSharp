using System;
using System.Linq;
using System.Reflection;
using Shouldly;
using Xunit;

namespace SampSharp.Entities.SAMP.Commands.Tests.Help;

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
            BindingFlags.NonPublic | BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandGroupEnumeratorTests), [], TestInvoker, 0);
        var definition = new CommandDefinition("ban", group, [overload]);
        _registry.Register(definition);

        var groups = _enumerator.GetAllCommandGroups().ToList();
        groups.Count.ShouldBe(1);
        groups.First().Name.ShouldContain("admin");
    }

    [Fact]
    public void CommandGroupEnumerator_IncludesCommands()
    {
        var group = new CommandGroup("admin");
        var testMethod = typeof(CommandGroupEnumeratorTests).GetMethod(
            nameof(TestCommand),
            BindingFlags.NonPublic | BindingFlags.Instance)!;

        var overload = new CommandOverload(testMethod, testMethod.GetParameters(), typeof(CommandGroupEnumeratorTests), [], TestInvoker, 0);
        var definition = new CommandDefinition("ban", group, [overload]);
        _registry.Register(definition);

        var groups = _enumerator.GetAllCommandGroups();
        groups.First().Commands.Count.ShouldBe(1);
    }

    private void TestCommand() { }

    private static object TestInvoker(object target, object[] args, IServiceProvider services, IEntityManager entityManager)
    {
        throw new InvalidOperationException();
    }
}