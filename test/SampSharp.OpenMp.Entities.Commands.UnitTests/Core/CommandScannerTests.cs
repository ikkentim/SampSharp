using System;
using System.Collections.Generic;
using System.Reflection;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Core;

/// <summary>
/// Tests for CommandScanner, which uses reflection to discover command methods from ISystem types.
/// </summary>
public class CommandScannerTests
{
    private static ISystemRegistry CreateRegistry(params Type[] types)
    {
        var mock = new Mock<ISystemRegistry>();
        mock.Setup(r => r.GetSystemTypes()).Returns(new ReadOnlyMemory<Type>(types));
        return mock.Object;
    }

    private static ICommandParameterParserFactory CreateParserFactory()
    {
        return new DefaultCommandParameterParserFactory();
    }

    private static IUnhandledExceptionHandler CreateExceptionHandler()
    {
        return new Mock<IUnhandledExceptionHandler>().Object;
    }

    // Player commands use prefixParams=1, so each method must have at least 1 parameter.
    // The first parameter is the player prefix (EntityId); subsequent ones are parsed from input.

    private class SimplePlayerCommandSystem : ISystem
    {
        [PlayerCommand]
        public void Hello(EntityId player) { }
    }

    private class NamedPlayerCommandSystem : ISystem
    {
        [PlayerCommand("greet")]
        public void SomeMethod(EntityId player) { }
    }

    private class PlayerCommandWithParamsSystem : ISystem
    {
        [PlayerCommand]
        public void Give(EntityId player, int amount) { }
    }

    private class PlayerCommandWithAliasSystem : ISystem
    {
        [Alias("pm")]
        [PlayerCommand("message")]
        public void Message(EntityId player) { }
    }

    private class PlayerCommandWithTagSystem : ISystem
    {
        [CommandTag("category", "admin")]
        [PlayerCommand]
        public void Kick(EntityId player) { }
    }

    [CommandGroup("admin")]
    private class GroupedPlayerCommandSystem : ISystem
    {
        [PlayerCommand]
        public void Kick(EntityId player) { }
    }

    private class MethodGroupPlayerCommandSystem : ISystem
    {
        [CommandGroup("admin")]
        [PlayerCommand]
        public void Kick(EntityId player) { }
    }

    private class MultipleCommandSystem : ISystem
    {
        [PlayerCommand]
        public void Kick(EntityId player) { }

        [PlayerCommand]
        public void Ban(EntityId player) { }
    }

    private class CommandWithSuffixSystem : ISystem
    {
        [PlayerCommand]
        public void HelpCommand(EntityId player) { }
    }

    private class InvalidReturnTypeSystem : ISystem
    {
        // int is not a valid return type for player commands
        [PlayerCommand]
        public int BadReturnCommand(EntityId player) => 0;
    }

    private class SimpleConsoleCommandSystem : ISystem
    {
        [ConsoleCommand]
        public void Status() { }
    }

    private class ConsoleCommandWithContextSystem : ISystem
    {
        [ConsoleCommand]
        public void Echo(ConsoleCommandDispatchContext ctx, int value) { }
    }

    [Fact]
    public void ScanPlayerCommands_DiscoversSingleCommand()
    {
        var registry = new CommandRegistry();
        var scanner = new CommandScanner(CreateRegistry(typeof(SimplePlayerCommandSystem)), CreateExceptionHandler());

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        ((ICommandRegistry)registry).TryFind("hello").ShouldNotBeNull();
    }

    [Fact]
    public void ScanPlayerCommands_UsesExplicitCommandName()
    {
        var registry = new CommandRegistry();
        var scanner = new CommandScanner(CreateRegistry(typeof(NamedPlayerCommandSystem)), CreateExceptionHandler());

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        ((ICommandRegistry)registry).TryFind("greet").ShouldNotBeNull();
    }

    [Fact]
    public void ScanPlayerCommands_StripsSuffixFromMethodName()
    {
        var registry = new CommandRegistry();
        var scanner = new CommandScanner(CreateRegistry(typeof(CommandWithSuffixSystem)), CreateExceptionHandler());

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        // Method is "HelpCommand" -> command name should be "help"
        ((ICommandRegistry)registry).TryFind("help").ShouldNotBeNull();
    }

    [Fact]
    public void ScanPlayerCommands_ExtractsParsedParameters()
    {
        var registry = new CommandRegistry();
        var scanner = new CommandScanner(CreateRegistry(typeof(PlayerCommandWithParamsSystem)), CreateExceptionHandler());

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        var found = ((ICommandRegistry)registry).TryFind("give");
        found.ShouldNotBeNull();
        // First param (EntityId player) is prefix; second (int amount) is parsed
        found!.ParsedParameters.Length.ShouldBe(1);
        found.ParsedParameters[0].Name.ShouldBe("amount");
    }

    [Fact]
    public void ScanPlayerCommands_RegistersAlias()
    {
        var registry = new CommandRegistry();
        var scanner = new CommandScanner(CreateRegistry(typeof(PlayerCommandWithAliasSystem)), CreateExceptionHandler());

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        // Command should be findable by alias
        ((ICommandRegistry)registry).TryFind("pm").ShouldNotBeNull();
    }

    [Fact]
    public void ScanPlayerCommands_RegistersTag()
    {
        var registry = new CommandRegistry();
        var scanner = new CommandScanner(CreateRegistry(typeof(PlayerCommandWithTagSystem)), CreateExceptionHandler());

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        var found = ((ICommandRegistry)registry).TryFind("kick");
        found.ShouldNotBeNull();
        found!.Tags["category"].ShouldBe("admin");
    }

    [Fact]
    public void ScanPlayerCommands_UsesClassLevelCommandGroup()
    {
        var registry = new CommandRegistry();
        var scanner = new CommandScanner(CreateRegistry(typeof(GroupedPlayerCommandSystem)), CreateExceptionHandler());

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        var found = ((ICommandRegistry)registry).TryFindByPath(["admin", "kick"]);
        found.ShouldNotBeNull();
        found!.FullName.ShouldBe("admin kick");
    }

    [Fact]
    public void ScanPlayerCommands_UsesMethodLevelCommandGroup()
    {
        var registry = new CommandRegistry();
        var scanner = new CommandScanner(CreateRegistry(typeof(MethodGroupPlayerCommandSystem)), CreateExceptionHandler());

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        var found = ((ICommandRegistry)registry).TryFindByPath(["admin", "kick"]);
        found.ShouldNotBeNull();
    }

    [Fact]
    public void ScanPlayerCommands_MultipleCommands_AllRegistered()
    {
        var registry = new CommandRegistry();
        var scanner = new CommandScanner(CreateRegistry(typeof(MultipleCommandSystem)), CreateExceptionHandler());

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        ((ICommandRegistry)registry).TryFind("kick").ShouldNotBeNull();
        ((ICommandRegistry)registry).TryFind("ban").ShouldNotBeNull();
    }

    [Fact]
    public void ScanPlayerCommands_EmptySystemRegistry_RegistersNothing()
    {
        var registry = new CommandRegistry();
        var scanner = new CommandScanner(CreateRegistry(), CreateExceptionHandler());

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        ((ICommandRegistry)registry).GetAll().ShouldBeEmpty();
    }

    [Fact]
    public void ScanPlayerCommands_InvalidReturnType_SkipsMethod()
    {
        var registry = new CommandRegistry();
        var scanner = new CommandScanner(CreateRegistry(typeof(InvalidReturnTypeSystem)), CreateExceptionHandler());

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        ((ICommandRegistry)registry).GetAll().ShouldBeEmpty();
    }

    [Fact]
    public void ScanConsoleCommands_DiscoversSingleCommand()
    {
        var registry = new CommandRegistry();
        var scanner = new CommandScanner(CreateRegistry(typeof(SimpleConsoleCommandSystem)), CreateExceptionHandler());

        scanner.ScanConsoleCommands(registry, CreateParserFactory());

        ((ICommandRegistry)registry).TryFind("status").ShouldNotBeNull();
    }

    [Fact]
    public void ScanConsoleCommands_WithContext_ContextIsNotParsedParameter()
    {
        var registry = new CommandRegistry();
        var scanner = new CommandScanner(CreateRegistry(typeof(ConsoleCommandWithContextSystem)), CreateExceptionHandler());

        scanner.ScanConsoleCommands(registry, CreateParserFactory());

        var found = ((ICommandRegistry)registry).TryFind("echo");
        found.ShouldNotBeNull();
        // The context param is prefix param; "value" (int) is the only parsed param
        found!.ParsedParameters.Length.ShouldBe(1);
        found.ParsedParameters[0].Name.ShouldBe("value");
    }
}
