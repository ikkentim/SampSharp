using System;
using System.Linq;
using System.Reflection;
using SampSharp.Entities.SAMP.Commands.Core;
using Shouldly;
using Xunit;

namespace SampSharp.Entities.SAMP.Commands.Tests.Core;

public class CommandRegistryTests
{
    private readonly MethodInfo _testMethod;

    public CommandRegistryTests()
    {
        _testMethod = typeof(CommandRegistryTests).GetMethod(
            nameof(DummyCommand),
            BindingFlags.NonPublic | BindingFlags.Instance)!;
    }

    [Fact]
    public void Register_AddsCommandDefinition()
    {
        var registry = new CommandRegistry();
        var overload = new CommandOverload(_testMethod, _testMethod.GetParameters(), typeof(CommandRegistryTests), [], TestInvoker, 0);
        var definition = new CommandDefinition(
            name: "test",
            group: null,
            overloads: new[] { overload },
            aliases: null,
            permissions: null);

        registry.Register(definition);

        registry.TryFind("test").ShouldNotBeNull();
    }

    [Fact]
    public void TryFind_ExistingCommand_ReturnsDefinition()
    {
        var registry = new CommandRegistry();
        var overload = new CommandOverload(_testMethod, _testMethod.GetParameters(), typeof(CommandRegistryTests), [], TestInvoker, 0);
        var definition = new CommandDefinition("kick", null, new[] { overload });
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
        var overload = new CommandOverload(_testMethod, _testMethod.GetParameters(), typeof(CommandRegistryTests), [], TestInvoker, 0);
        registry.Register(new CommandDefinition("test1", null, new[] { overload }));
        registry.Register(new CommandDefinition("test2", null, new[] { overload }));

        registry.GetAll().Count().ShouldBe(2);
    }

    private void DummyCommand() { }

    private static object TestInvoker(object target, object[] args, IServiceProvider services, IEntityManager entityManager)
    {
        throw new InvalidOperationException();
    }
}