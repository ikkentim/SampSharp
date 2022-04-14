using Moq;
using SampSharp.Core.CodePages;
using Shouldly;
using Xunit;

namespace SampSharp.Core.UnitTests;

public class EncodingGameModeBuilderExtensionsTests
{
    [Fact]
    public void UseEncodingCodePage_should_load_correct_codepage()
    {
        // arrange
        IGameModeRunner activeRunner = null;
        var sut = new GameModeBuilder();
        sut.Use(Mock.Of<IGameModeProvider>());
        sut.AddBuildAction(next =>
        {
            var runner = next();
            ((HostedGameModeClient)runner).InitializeForTesting();

            return runner;
        });

        sut.AddRunAction((runner, next) =>
        {
            next(runner);
            activeRunner = runner;
        });

        // act
        sut.UseEncodingCodePage("cp865");
        sut.Run();
        
        // assert
        var client = activeRunner.ShouldBeAssignableTo<IGameModeClient>()!;
        var encoding = client.Encoding.ShouldBeOfType<CodePageEncoding>();

        encoding.CodePage.ShouldBe(865);
        encoding.ConversionTable['Θ'].ShouldBe((ushort)0xe9);
    }
}