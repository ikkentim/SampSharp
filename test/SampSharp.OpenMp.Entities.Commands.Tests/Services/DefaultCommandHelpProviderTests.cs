using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Services;

/// <summary>
/// Tests for DefaultCommandHelpProvider, which provides help information about registered commands.
/// </summary>
public class DefaultCommandHelpProviderTests
{
    private static CommandDefinition CreateDefinition(string name, CommandGroup? group = null)
    {
        var method = typeof(DefaultCommandHelpProviderTests).GetMethod(nameof(DummyMethod), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        var mockInvoker = new Mock<CommandInvoker>();

        return new CommandDefinition(
            name, group, method, method.GetParameters(),
            typeof(DefaultCommandHelpProviderTests),
            Array.Empty<CommandParameterInfo>(),
            mockInvoker.Object, 0,
            Array.Empty<CommandAlias>(), Array.Empty<CommandTag>());
    }

    private void DummyMethod() { }

    [Fact]
    public void Constructor_NullRegistry_ThrowsArgumentNullException()
    {
        Should.Throw<ArgumentNullException>(() => new DefaultCommandHelpProvider(null!));
    }

    [Fact]
    public void GetAllCommands_DelegatesToRegistry()
    {
        var def = CreateDefinition("test");
        var registryMock = new Mock<ICommandRegistry>();
        registryMock.Setup(r => r.GetAll()).Returns(new[] { def });

        var provider = new DefaultCommandHelpProvider(registryMock.Object);

        var result = provider.GetAllCommands().ToList();

        result.Count.ShouldBe(1);
        result[0].Name.ShouldBe("test");
    }

    [Fact]
    public void GetCommandGroups_DelegatesToRegistry()
    {
        var group = new CommandGroup("admin");
        var registryMock = new Mock<ICommandRegistry>();
        registryMock.Setup(r => r.GetGroups()).Returns(new[] { group });

        var provider = new DefaultCommandHelpProvider(registryMock.Object);

        var result = provider.GetCommandGroups().ToList();

        result.Count.ShouldBe(1);
        result[0].ShouldBe(group);
    }

    [Fact]
    public void GetCommandsInGroup_DelegatesToRegistry()
    {
        var group = new CommandGroup("admin");
        var def = CreateDefinition("kick", group);
        var registryMock = new Mock<ICommandRegistry>();
        registryMock.Setup(r => r.GetCommandsInGroup(group)).Returns(new[] { def });

        var provider = new DefaultCommandHelpProvider(registryMock.Object);

        var result = provider.GetCommandsInGroup(group).ToList();

        result.Count.ShouldBe(1);
        result[0].Name.ShouldBe("kick");
    }

    [Fact]
    public void SearchCommands_MatchesByName()
    {
        var def1 = CreateDefinition("kick");
        var def2 = CreateDefinition("ban");
        var registryMock = new Mock<ICommandRegistry>();
        registryMock.Setup(r => r.GetAll()).Returns(new[] { def1, def2 });

        var provider = new DefaultCommandHelpProvider(registryMock.Object);

        var result = provider.SearchCommands("ki").ToList();

        result.Count.ShouldBe(1);
        result[0].Name.ShouldBe("kick");
    }

    [Fact]
    public void SearchCommands_CaseInsensitive()
    {
        var def = CreateDefinition("Kick");
        var registryMock = new Mock<ICommandRegistry>();
        registryMock.Setup(r => r.GetAll()).Returns(new[] { def });

        var provider = new DefaultCommandHelpProvider(registryMock.Object);

        var result = provider.SearchCommands("KICK").ToList();

        result.Count.ShouldBe(1);
    }

    [Fact]
    public void SearchCommands_EmptyQuery_ReturnsAll()
    {
        var def1 = CreateDefinition("kick");
        var def2 = CreateDefinition("ban");
        var registryMock = new Mock<ICommandRegistry>();
        registryMock.Setup(r => r.GetAll()).Returns(new[] { def1, def2 });

        var provider = new DefaultCommandHelpProvider(registryMock.Object);

        var result = provider.SearchCommands("").ToList();

        result.Count.ShouldBe(2);
    }

    [Fact]
    public void SearchCommands_MatchesByFullName()
    {
        var def = CreateDefinition("kick", new CommandGroup("admin"));
        var registryMock = new Mock<ICommandRegistry>();
        registryMock.Setup(r => r.GetAll()).Returns(new[] { def });

        var provider = new DefaultCommandHelpProvider(registryMock.Object);

        var result = provider.SearchCommands("admin kick").ToList();

        result.Count.ShouldBe(1);
    }
}
