using System;
using System.Text;
using Moq;
using SampSharp.Core.Callbacks;
using Shouldly;

namespace SampSharp.Core.UnitTests;

public class GameModeClientHelperTests
{
    public unsafe void RegisterCallbacksInObject_should_register_all_callbacks()
    {
        // arrange
        var args = new[] { 0 };
        var result1 = 0;
        var result2 = 0;
            
        var sut = new HostedGameModeClient(Mock.Of<IGameModeProvider>(), Encoding.ASCII);

        // act
        sut.RegisterCallbacksInObject(this);
            
        fixed (int* argsPtr = args)
        {
            sut.PublicCall(IntPtr.Zero, "OnTesting1", (IntPtr)argsPtr, (IntPtr)(&result1));
            sut.PublicCall(IntPtr.Zero, "OnTesting2", (IntPtr)argsPtr, (IntPtr)(&result2));
        }

        // assert
        result1.ShouldBe(321);
        result2.ShouldBe(123);
    }
        
    [Callback]
    public int OnTesting1() => 321;
        
    [Callback]
    public int OnTesting2() => 123;
}