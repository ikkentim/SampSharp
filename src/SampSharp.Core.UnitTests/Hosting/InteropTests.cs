using System.Runtime.InteropServices;
using Moq;
using SampSharp.Core.Hosting;
using SampSharp.Core.UnitTests.TestHelpers;
using Shouldly;
using Xunit;

namespace SampSharp.Core.UnitTests.Hosting
{
    public unsafe class InteropTests
    {
        private static string _printfFormat;
        private static string _printfMessage;

        [UnmanagedCallersOnly]
        static void PrintfUnmanaged(byte* format, byte* message)
        {
            _printfFormat = new string((sbyte*)format);
            _printfMessage = new string((sbyte*)message);
        }

        [Theory]
        [InlineData("test message")]
        [InlineData("")]
        [InlineData(" %s %d test ")]
        public void Print_should_invoke_correct_api(string message)
        {
            // arrange
            _printfFormat = null;
            _printfMessage = null;

            var pluginData = new InteropStructs.PluginDataRw { Logprintf = &PrintfUnmanaged };
            var api = new InteropStructs.SampSharpApiRw { PluginData = &pluginData };

            using var gmScope = new GameModeClientScope(Mock.Of<IGameModeClient>());
            using var apiScope = new ApiScope(&api);
            
            // act
            Interop.Print(message);

            // assert
            _printfFormat.ShouldBe("%s");
            _printfMessage.ShouldBe(message);
        }
    }
}
