using System;
using System.Text;
using Moq;
using Shouldly;
using Xunit;

namespace SampSharp.Core.UnitTests;

public class HostedGameModeClientTests
{
    [Fact]
    public unsafe void PublicCall_should_invoke_registered_callback()
    {
        // arrange
        var handler = () => 321;

        var args = new[] { 0 };
        var result = 0;
            
        var sut = new HostedGameModeClient(Mock.Of<IGameModeProvider>(), Encoding.ASCII);
        sut.InitializeForTesting();
        
        // act
        sut.RegisterCallback("OnTesting", handler.Target, handler.Method);
            
        fixed (int* argsPtr = args)
        {
            sut.PublicCall(IntPtr.Zero, "OnTesting", (IntPtr)argsPtr, (IntPtr)(&result));
        }

        // assert
        result.ShouldBe(321);
    }

    [Fact]
    public void RegisterCallback_should_throw_when_game_mode_not_running()
    {
        // arrange
        var handler = () => { };
        
        var sut = new HostedGameModeClient(Mock.Of<IGameModeProvider>(), Encoding.ASCII);

        // act
        Should.Throw<GameModeNotRunningException>(() =>
            sut.RegisterCallback("OnTesting", handler.Target, handler.Method));
    }
}