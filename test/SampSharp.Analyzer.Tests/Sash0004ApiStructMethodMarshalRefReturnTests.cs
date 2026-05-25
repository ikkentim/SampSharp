using SampSharp.Analyzer;
using SampSharp.Analyzer.Analyzers;
using Shouldly;
using Xunit;

namespace SampSharp.Analyzer.Tests;

public class Sash0004ApiStructMethodMarshalRefReturnTests
{
    [Fact]
    public async Task Sash0004_should_report_when_partial_method_uses_ref_return_with_MarshalUsing()
    {
        const string source = """
            using System.Runtime.InteropServices.Marshalling;
            using SampSharp.OpenMp.Core;

            public class Dummy : CustomMarshaller<int, int> { }

            public static class FakeMarshaller { }

            [OpenMpApi]
            public readonly partial struct MyApi
            {
                [return: MarshalUsing(typeof(FakeMarshaller))]
                public partial ref int GetValue();
            }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0004ApiStructMethodMarshalRefReturnNotSupportedAnalyzer(), source);

        diags.Count(d => d.Id == AnalyzerIds.Sash0004ApiStructMarshalRefReturnUnsupported.Id).ShouldBe(1);
    }

    [Fact]
    public async Task Sash0004_should_report_when_partial_method_uses_ref_return_with_NativeMarshalling_target()
    {
        const string source = """
            using System.Runtime.InteropServices.Marshalling;
            using SampSharp.OpenMp.Core;

            public static class FakeMarshaller { }

            [NativeMarshalling(typeof(FakeMarshaller))]
            public struct SomeStruct { }

            [OpenMpApi]
            public readonly partial struct MyApi
            {
                public partial ref SomeStruct GetValue();
            }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0004ApiStructMethodMarshalRefReturnNotSupportedAnalyzer(), source);

        diags.Count(d => d.Id == AnalyzerIds.Sash0004ApiStructMarshalRefReturnUnsupported.Id).ShouldBe(1);
    }

    [Fact]
    public async Task Sash0004_should_not_report_for_ref_return_without_marshalling()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpApi]
            public readonly partial struct MyApi
            {
                public partial ref int GetValue();
            }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0004ApiStructMethodMarshalRefReturnNotSupportedAnalyzer(), source);

        diags.ShouldNotContain(d => d.Id == AnalyzerIds.Sash0004ApiStructMarshalRefReturnUnsupported.Id);
    }

    [Fact]
    public async Task Sash0004_should_not_report_for_non_api_struct()
    {
        const string source = """
            using System.Runtime.InteropServices.Marshalling;

            public static class FakeMarshaller { }

            public readonly partial struct Plain
            {
                [return: MarshalUsing(typeof(FakeMarshaller))]
                public partial ref int GetValue();
            }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0004ApiStructMethodMarshalRefReturnNotSupportedAnalyzer(), source);

        diags.ShouldNotContain(d => d.Id == AnalyzerIds.Sash0004ApiStructMarshalRefReturnUnsupported.Id);
    }
}
