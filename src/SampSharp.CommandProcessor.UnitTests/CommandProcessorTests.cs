using System.Collections;
using System.Linq;
using Moq;
using Moq.AutoMock;
using Shouldly;
using Xunit;

namespace SampSharp.CommandProcessor.UnitTests;

public class CommandProcessorTests
{
    [Fact]
    public void Run_should_parse_and_run_command()
    {
        // arrange
        CommandContext? executedContext = null;
        ParsedCommand? executedCommand = null;

        var mocker = new AutoMocker();
        var sut = mocker.CreateInstance<CommandProcessor>();

        sut.AddCommand(new TestCommandParser("foo", (ctx, cmd) =>
        {
            executedContext = ctx;
            executedCommand = cmd;
        }));

        // act
        var result = sut.Run(null, "foo");

        // assert
        result.ShouldBeTrue();
        executedContext.ShouldNotBeNull();
        executedCommand.ShouldNotBeNull();
    }
    
    [Theory]
    [InlineData(" foo", false, false)]
    [InlineData(" foo", true, true)]
    [InlineData("   foo", false, false)]
    [InlineData("   foo", true, true)]
    [InlineData(" bar", true, false)]
    public void Run_should_trim_start(string commandText, bool trimStart, bool expectedResult)
    {
        // arrange
        var mocker = new AutoMocker();
        mocker.Use(new CommandProcessorOptions
        {
            TrimStart = trimStart
        });
        var sut = mocker.CreateInstance<CommandProcessor>();

        sut.AddCommand(new TestCommandParser("foo", null));

        // act
        var result = sut.Run(null, commandText);

        // assert
        result.ShouldBe(expectedResult);
    }

    [Fact]
    public void AddCommand_should_add_command()
    {
        // arrange
        var mocker = new AutoMocker();
        var sut = mocker.CreateInstance<CommandProcessor>();

        var parser = Mock.Of<ICommandParser>();

        // act
        sut.AddCommand(parser);

        // assert
        sut.ShouldHaveSingleItem()
            .ShouldBe(parser);
    }
}