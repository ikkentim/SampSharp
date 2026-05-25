using SampSharp.Analyzer;
using SampSharp.Analyzer.Analyzers;
using Shouldly;
using Xunit;

namespace SampSharp.Analyzer.Tests;

public class Sash0008EventHandlerMarshalRefReturnTests
{
    [Fact]
    public async Task Sash0008_should_report_for_event_handler_method_with_ref_return_and_MarshalUsing()
    {
        const string source = """
            using System.Runtime.InteropServices.Marshalling;
            using SampSharp.OpenMp.Core;

            public static class FakeMarshaller { }

            [OpenMpEventHandler]
            public interface IMyHandler
            {
                [return: MarshalUsing(typeof(FakeMarshaller))]
                ref int GetValue();
            }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0008EventHandlerMarshalRefReturnUnsupported(), source);

        diags.Count(d => d.Id == AnalyzerIds.Sash0008EventHandlerMarshalRefReturnUnsupported.Id).ShouldBe(1);
    }

    [Fact]
    public async Task Sash0008_should_report_for_event_handler_method_with_ref_return_and_NativeMarshalling_target()
    {
        const string source = """
            using System.Runtime.InteropServices.Marshalling;
            using SampSharp.OpenMp.Core;

            public static class FakeMarshaller { }

            [NativeMarshalling(typeof(FakeMarshaller))]
            public struct SomeStruct { }

            [OpenMpEventHandler]
            public interface IMyHandler
            {
                ref SomeStruct GetValue();
            }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0008EventHandlerMarshalRefReturnUnsupported(), source);

        diags.Count(d => d.Id == AnalyzerIds.Sash0008EventHandlerMarshalRefReturnUnsupported.Id).ShouldBe(1);
    }

    [Fact]
    public async Task Sash0008_should_not_report_for_ref_return_without_marshalling()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpEventHandler]
            public interface IMyHandler
            {
                ref int GetValue();
            }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0008EventHandlerMarshalRefReturnUnsupported(), source);

        diags.ShouldNotContain(d => d.Id == AnalyzerIds.Sash0008EventHandlerMarshalRefReturnUnsupported.Id);
    }

    [Fact]
    public async Task Sash0008_should_not_report_for_non_event_handler_interface()
    {
        const string source = """
            using System.Runtime.InteropServices.Marshalling;

            public static class FakeMarshaller { }

            public interface IPlain
            {
                [return: MarshalUsing(typeof(FakeMarshaller))]
                ref int GetValue();
            }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0008EventHandlerMarshalRefReturnUnsupported(), source);

        diags.ShouldNotContain(d => d.Id == AnalyzerIds.Sash0008EventHandlerMarshalRefReturnUnsupported.Id);
    }
}
