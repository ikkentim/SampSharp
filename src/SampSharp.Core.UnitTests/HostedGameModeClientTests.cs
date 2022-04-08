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

        // act
        sut.RegisterCallback("OnTesting", handler.Target, handler.Method);
            
        fixed (int* argsPtr = args)
        {
            sut.PublicCall(IntPtr.Zero, "OnTesting", (IntPtr)argsPtr, (IntPtr)(&result));
        }

        // assert
        result.ShouldBe(321);
    }
}