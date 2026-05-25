using SampSharp.Analyzer;
using SampSharp.Analyzer.Analyzers;
using Shouldly;
using Xunit;

namespace SampSharp.Analyzer.Tests;

public class Sash0003ApiStructMustBeReadonlyPartialAnalyzerTests
{
    [Fact]
    public async Task Sash0003_should_report_when_api_struct_missing_partial()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpApi]
            public readonly struct MyApi { }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0003ApiStructMustBeReadonlyPartialAnalyzer(), source);

        diags.Count(d => d.Id == AnalyzerIds.Sash0003ApiStructMustBeReadonlyPartial.Id).ShouldBe(1);
    }

    [Fact]
    public async Task Sash0003_should_report_when_api_struct_missing_readonly()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpApi]
            public partial struct MyApi { }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0003ApiStructMustBeReadonlyPartialAnalyzer(), source);

        diags.Count(d => d.Id == AnalyzerIds.Sash0003ApiStructMustBeReadonlyPartial.Id).ShouldBe(1);
    }

    [Fact]
    public async Task Sash0003_should_report_when_api_struct_missing_both()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpApi]
            public struct MyApi { }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0003ApiStructMustBeReadonlyPartialAnalyzer(), source);

        diags.Count(d => d.Id == AnalyzerIds.Sash0003ApiStructMustBeReadonlyPartial.Id).ShouldBe(1);
    }

    [Fact]
    public async Task Sash0003_should_not_report_when_api_struct_is_readonly_partial()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpApi]
            public readonly partial struct MyApi { }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0003ApiStructMustBeReadonlyPartialAnalyzer(), source);

        diags.ShouldNotContain(d => d.Id == AnalyzerIds.Sash0003ApiStructMustBeReadonlyPartial.Id);
    }

    [Fact]
    public async Task Sash0003_should_not_report_for_non_api_struct()
    {
        const string source = """
            public struct Plain { }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0003ApiStructMustBeReadonlyPartialAnalyzer(), source);

        diags.ShouldNotContain(d => d.Id == AnalyzerIds.Sash0003ApiStructMustBeReadonlyPartial.Id);
    }
}
