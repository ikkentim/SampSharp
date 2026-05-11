using Microsoft.Extensions.Logging;
using Moq;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Entities.Commands.Tests.Core;

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

    private static Mock<ILogger> CreateLogger()
    {
        return new Mock<ILogger>();
    }

    private static CommandRegistry CreateCommandRegistry()
    {
        return new CommandRegistry(StringComparison.OrdinalIgnoreCase);
    }

    private static CommandScanner CreateScanner(params Type[] types)
    {
        return new CommandScanner(CreateRegistry(types), CreateExceptionHandler(), CreateLogger().Object);
    }

    private static void VerifyWarningLogged(Mock<ILogger> logger, string messageFragment, Times times)
    {
        logger.Verify(l => l.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((state, _) => LogStateContains(state, messageFragment)),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            times);
    }

    private static bool LogStateContains(object state, string messageFragment)
    {
        var message = state.ToString();
        return message != null && message.Contains(messageFragment, StringComparison.Ordinal);
    }

    // Player commands use prefixParams=1, so each method must have at least 1 parameter.
    // The first parameter is the player prefix (EntityId); subsequent ones are parsed from input.

    private class SimplePlayerCommandSystem : ISystem
    {
        [PlayerCommand]
        public void Hello(Player player) { }
    }

    private class CustomComponentCommandSystem : ISystem
    {
        [PlayerCommand]
        public void Hello(CustomComponent player) { }
    }

    private class EntityIdCommandSystem : ISystem
    {
        [PlayerCommand]
        public void Hello(EntityId player) { }
    }

    private class InvalidParametersCommandSystem : ISystem
    {
        [PlayerCommand]
        public void Hello() { }

        [PlayerCommand]
        public void IdNumber(int playerId) { }
        [PlayerCommand]
        public void IdNumber(string playerId) { }
    }

    private class CustomComponent : Component;

    private class NamedPlayerCommandSystem : ISystem
    {
        [PlayerCommand("greet")]
        public void SomeMethod(Player player) { }
    }

    private class InvalidNamePlayerCommandSystem : ISystem
    {
        [PlayerCommand("spaces in name")]
        public void Example(Player player) { }
    }

    private class PlayerCommandWithParamsSystem : ISystem
    {
        [PlayerCommand]
        public void Give(Player player, int amount) { }
    }

    private class PlayerCommandWithAliasSystem : ISystem
    {
        [Alias("pm")]
        [PlayerCommand("message")]
        public void Message(Player player) { }
    }

    private class PlayerCommandWithTagSystem : ISystem
    {
        [CommandTag("category", "admin")]
        [PlayerCommand]
        public void Kick(Player player) { }
    }

    [CommandGroup("admin")]
    private class GroupedPlayerCommandSystem : ISystem
    {
        [PlayerCommand]
        public void Kick(Player player) { }
    }

    private class MethodGroupPlayerCommandSystem : ISystem
    {
        [CommandGroup("admin")]
        [PlayerCommand]
        public void Kick(Player player) { }
    }

    private class MultipleCommandSystem : ISystem
    {
        [PlayerCommand]
        public void Kick(Player player) { }

        [PlayerCommand]
        public void Ban(Player player) { }
    }

    private class CommandWithSuffixSystem : ISystem
    {
        [PlayerCommand]
        public void HelpCommand(Player player) { }
    }

    private class InvalidReturnTypeSystem : ISystem
    {
        // int is not a valid return type for player commands
        [PlayerCommand]
        public int BadReturnCommand(Player player) => 0;
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

    private static CommandDefinition? FindByName(CommandRegistry registry, string name)
    {
        var span = StringSpan.For(name);
        var overloads = registry.GetCommandGroupByPath(ref span);
        return overloads?.Count > 0 ? overloads[0] : null;
    }

    private static CommandDefinition? FindByPath(CommandRegistry registry, params string[] parts)
    {
        var span = StringSpan.For(string.Join(" ", parts));
        var overloads = registry.GetCommandGroupByPath(ref span);
        return overloads?.Count > 0 ? overloads[0] : null;
    }

    [Fact]
    public void ScanPlayerCommands_DiscoversSingleCommand()
    {
        var registry = CreateCommandRegistry();
        var scanner = CreateScanner(typeof(SimplePlayerCommandSystem));

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        FindByName(registry, "hello").ShouldNotBeNull();
    }

    [Fact]
    public void ScanPlayerCommands_SupportsCustomComponentType()
    {
        var registry = CreateCommandRegistry();
        var scanner = CreateScanner(typeof(CustomComponentCommandSystem));

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        FindByName(registry, "hello").ShouldNotBeNull();
    }

    [Fact]
    public void ScanPlayerCommands_SupportsEntityIdType()
    {
        var registry = CreateCommandRegistry();
        var scanner = CreateScanner(typeof(EntityIdCommandSystem));

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        FindByName(registry, "hello").ShouldNotBeNull();
    }

    [Fact]
    public void ScanPlayerCommands_SkipsNoParameters()
    {
        var registry = CreateCommandRegistry();
        var scanner = CreateScanner(typeof(InvalidParametersCommandSystem));

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        ((ICommandRegistry)registry).GetAll().ShouldHaveCount(0);
    }

    [Fact]
    public void ScanPlayerCommands_UsesExplicitCommandName()
    {
        var registry = CreateCommandRegistry();
        var scanner = CreateScanner(typeof(NamedPlayerCommandSystem));

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        FindByName(registry, "greet").ShouldNotBeNull();
    }

    [Fact]
    public void ScanPlayerCommands_StripsSuffixFromMethodName()
    {
        var registry = CreateCommandRegistry();
        var scanner = CreateScanner(typeof(CommandWithSuffixSystem));

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        // Method is "HelpCommand" -> command name should be "help"
        FindByName(registry, "help").ShouldNotBeNull();
    }

    [Fact]
    public void ScanPlayerCommands_ExtractsParsedParameters()
    {
        var registry = CreateCommandRegistry();
        var scanner = CreateScanner(typeof(PlayerCommandWithParamsSystem));

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        var found = FindByName(registry, "give");
        found.ShouldNotBeNull();
        // First param (EntityId player) is prefix; second (int amount) is parsed
        found!.ParsedParameters.Length.ShouldBe(1);
        found.ParsedParameters[0].Name.ShouldBe("amount");
    }

    [Fact]
    public void ScanPlayerCommands_RegistersAlias()
    {
        var registry = CreateCommandRegistry();
        var scanner = CreateScanner(typeof(PlayerCommandWithAliasSystem));

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        // Command should be findable by alias
        FindByName(registry, "pm").ShouldNotBeNull();
    }

    [Fact]
    public void ScanPlayerCommands_RegistersTag()
    {
        var registry = CreateCommandRegistry();
        var scanner = CreateScanner(typeof(PlayerCommandWithTagSystem));

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        var found = FindByName(registry, "kick");
        found.ShouldNotBeNull();
        found!.Tags["category"].ShouldBe("admin");
    }

    [Fact]
    public void ScanPlayerCommands_UsesClassLevelCommandGroup()
    {
        var registry = CreateCommandRegistry();
        var scanner = CreateScanner(typeof(GroupedPlayerCommandSystem));

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        var found = FindByPath(registry, "admin", "kick");
        found.ShouldNotBeNull();
        found!.FullName.ShouldBe("admin kick");
    }

    [Fact]
    public void ScanPlayerCommands_UsesMethodLevelCommandGroup()
    {
        var registry = CreateCommandRegistry();
        var scanner = CreateScanner(typeof(MethodGroupPlayerCommandSystem));

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        FindByPath(registry, "admin", "kick").ShouldNotBeNull();
    }

    [Fact]
    public void ScanPlayerCommands_MultipleCommands_AllRegistered()
    {
        var registry = CreateCommandRegistry();
        var scanner = CreateScanner(typeof(MultipleCommandSystem));

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        FindByName(registry, "kick").ShouldNotBeNull();
        FindByName(registry, "ban").ShouldNotBeNull();
    }

    [Fact]
    public void ScanPlayerCommands_EmptySystemRegistry_RegistersNothing()
    {
        var registry = CreateCommandRegistry();
        var scanner = CreateScanner();

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        ((ICommandRegistry)registry).GetAll().ShouldBeEmpty();
    }

    [Fact]
    public void ScanPlayerCommands_InvalidReturnType_SkipsMethod()
    {
        var registry = CreateCommandRegistry();
        var scanner = CreateScanner(typeof(InvalidReturnTypeSystem));

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        ((ICommandRegistry)registry).GetAll().ShouldBeEmpty();
    }

    [Fact]
    public void ScanPlayerCommands_InvalidName_LogsWarning()
    {
        var registry = CreateCommandRegistry();
        var logger = CreateLogger();
        var scanner = new CommandScanner(CreateRegistry(typeof(InvalidNamePlayerCommandSystem)), CreateExceptionHandler(), logger.Object);

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        ((ICommandRegistry)registry).GetAll().ShouldBeEmpty();
        VerifyWarningLogged(logger, "invalid command name 'spaces in name'", Times.Once());
    }

    [Fact]
    public void ScanPlayerCommands_InvalidParameters_LogsWarningForEachRejectedMethod()
    {
        var registry = CreateCommandRegistry();
        var logger = CreateLogger();
        var scanner = new CommandScanner(CreateRegistry(typeof(InvalidParametersCommandSystem)), CreateExceptionHandler(), logger.Object);

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        ((ICommandRegistry)registry).GetAll().ShouldBeEmpty();
        VerifyWarningLogged(logger, "first parameter must be a Component or EntityId", Times.Exactly(3));
    }

    [Fact]
    public void ScanPlayerCommands_InvalidReturnType_LogsWarning()
    {
        var registry = CreateCommandRegistry();
        var logger = CreateLogger();
        var scanner = new CommandScanner(CreateRegistry(typeof(InvalidReturnTypeSystem)), CreateExceptionHandler(), logger.Object);

        scanner.ScanPlayerCommands(registry, CreateParserFactory());

        ((ICommandRegistry)registry).GetAll().ShouldBeEmpty();
        VerifyWarningLogged(logger, "invalid return type 'System.Int32'", Times.Once());
    }

    [Fact]
    public void ScanConsoleCommands_DiscoversSingleCommand()
    {
        var registry = CreateCommandRegistry();
        var scanner = CreateScanner(typeof(SimpleConsoleCommandSystem));

        scanner.ScanConsoleCommands(registry, CreateParserFactory());

        FindByName(registry, "status").ShouldNotBeNull();
    }

    [Fact]
    public void ScanConsoleCommands_WithContext_ContextIsNotParsedParameter()
    {
        var registry = CreateCommandRegistry();
        var scanner = CreateScanner(typeof(ConsoleCommandWithContextSystem));

        scanner.ScanConsoleCommands(registry, CreateParserFactory());

        var found = FindByName(registry, "echo");
        found.ShouldNotBeNull();
        // The context param is prefix param; "value" (int) is the only parsed param
        found!.ParsedParameters.Length.ShouldBe(1);
        found.ParsedParameters[0].Name.ShouldBe("value");
    }
}
