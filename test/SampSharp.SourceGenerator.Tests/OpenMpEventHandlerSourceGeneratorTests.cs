using SampSharp.SourceGenerator.Generators;
using VerifyXunit;

namespace SampSharp.SourceGenerator.Tests;

public class OpenMpEventHandlerSourceGeneratorTests : VerifyBase
{
    public OpenMpEventHandlerSourceGeneratorTests() : base() { }
    [Fact]
    public Task SimpleEventHandler_GeneratesMarshaller()
    {
        var source = """
            using SampSharp.OpenMp.Core;

            namespace TestEvents;

            [OpenMpEventHandler]
            public partial interface IMyEventHandler
            {
                bool OnSomeEvent(int id, string name);
                void OnOtherEvent(int value);
            }
            """;

        var driver = CompilationHelper.RunGenerator(source, new OpenMpEventHandlerSourceGenerator());

        return Verify(driver);
    }

    [Fact]
    public Task EventHandlerWithBoolParameters_GeneratesBoolMarshalling()
    {
        var source = """
            using SampSharp.OpenMp.Core;

            namespace TestEvents;

            [OpenMpEventHandler]
            public partial interface IBoolEventHandler
            {
                bool OnToggle(bool enabled, int id);
            }
            """;

        var driver = CompilationHelper.RunGenerator(source, new OpenMpEventHandlerSourceGenerator());

        return Verify(driver);
    }

    [Fact]
    public Task EventHandlerWithCustomLibraryAndNativeTypeName_UsesProvidedValues()
    {
        var source = """
            using SampSharp.OpenMp.Core;

            namespace TestEvents;

            [OpenMpEventHandler(Library = "MyLib", NativeTypeName = "MyEventHandler")]
            public partial interface ICustomEventHandler
            {
                void OnEvent(int id);
            }
            """;

        var driver = CompilationHelper.RunGenerator(source, new OpenMpEventHandlerSourceGenerator());

        return Verify(driver);
    }

    [Fact]
    public Task EventHandlerWithBaseInterface_IncludesBaseInterfaceMethods()
    {
        var source = """
            using SampSharp.OpenMp.Core;

            namespace TestEvents;

            public interface IBaseEvents
            {
                void OnBaseEvent(int id);
            }

            [OpenMpEventHandler]
            public partial interface IDerivedEventHandler : IBaseEvents
            {
                bool OnDerivedEvent(string name);
            }
            """;

        var driver = CompilationHelper.RunGenerator(source, new OpenMpEventHandlerSourceGenerator());

        return Verify(driver);
    }
}
