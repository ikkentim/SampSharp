using SampSharp.Analyzer;
using SampSharp.Analyzer.Analyzers;
using Shouldly;
using Xunit;

namespace SampSharp.Analyzer.Tests;

public class Sash0007ApiStructMustNotContainFieldsTests
{
    [Fact]
    public async Task Sash0007_should_report_when_api_struct_contains_field()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpApi]
            public readonly partial struct MyApi
            {
                public readonly int Value;
            }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0007ApiStructMustNotContainFields(), source);

        diags.Count(d => d.Id == AnalyzerIds.Sash0007ApiStructMustNotContainFields.Id).ShouldBe(1);
    }

    [Fact]
    public async Task Sash0007_should_report_when_api_struct_contains_auto_property()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpApi]
            public readonly partial struct MyApi
            {
                public int Value { get; }
            }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0007ApiStructMustNotContainFields(), source);

        diags.Count(d => d.Id == AnalyzerIds.Sash0007ApiStructMustNotContainFields.Id).ShouldBe(1);
    }

    [Fact]
    public async Task Sash0007_should_not_report_for_expression_bodied_property()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpApi]
            public readonly partial struct MyApi
            {
                public int Value => 0;
            }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0007ApiStructMustNotContainFields(), source);

        diags.ShouldNotContain(d => d.Id == AnalyzerIds.Sash0007ApiStructMustNotContainFields.Id);
    }

    [Fact]
    public async Task Sash0007_should_not_report_for_methods_only()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpApi]
            public readonly partial struct MyApi
            {
                public partial void DoThing();
            }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0007ApiStructMustNotContainFields(), source);

        diags.ShouldNotContain(d => d.Id == AnalyzerIds.Sash0007ApiStructMustNotContainFields.Id);
    }

    [Fact]
    public async Task Sash0007_should_not_report_for_non_api_struct_with_field()
    {
        const string source = """
            public readonly partial struct Plain
            {
                public readonly int Value;
            }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0007ApiStructMustNotContainFields(), source);

        diags.ShouldNotContain(d => d.Id == AnalyzerIds.Sash0007ApiStructMustNotContainFields.Id);
    }
}
