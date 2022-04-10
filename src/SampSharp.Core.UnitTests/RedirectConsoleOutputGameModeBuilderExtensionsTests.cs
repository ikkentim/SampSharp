using System;
using Moq;
using Shouldly;
using Xunit;

namespace SampSharp.Core.UnitTests;

public class RedirectConsoleOutputGameModeBuilderExtensionsTests
{
    [Fact]
    public void RedirectConsoleOutput_should_set_console_out()
    {
        // arrange
        var defaultOut = Console.Out;
        var sut = new GameModeBuilder();
        sut.AddBuildAction(_ => Mock.Of<IGameModeRunner>(x => x.Client == Mock.Of<IGameModeClient>()));

        // act
        sut.RedirectConsoleOutput();
        sut.Run();
        
        // assert
        Console.Out.ShouldNotBe(defaultOut);
    }
}