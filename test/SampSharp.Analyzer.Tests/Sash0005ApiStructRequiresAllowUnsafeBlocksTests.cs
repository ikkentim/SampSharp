using SampSharp.Analyzer;
using SampSharp.Analyzer.Analyzers;
using Shouldly;
using Xunit;

namespace SampSharp.Analyzer.Tests;

public class Sash0005ApiStructRequiresAllowUnsafeBlocksTests
{
    [Fact]
    public async Task Sash0005_should_report_when_AllowUnsafe_is_false()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpApi]
            public readonly partial struct MyApi { }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0005ApiStructRequiresAllowUnsafeBlocks(), source, allowUnsafe: false);

        diags.Count(d => d.Id == AnalyzerIds.Sash0005ApiStructRequiresAllowUnsafeBlocks.Id).ShouldBe(1);
    }

    [Fact]
    public async Task Sash0005_should_not_report_when_AllowUnsafe_is_true()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpApi]
            public readonly partial struct MyApi { }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0005ApiStructRequiresAllowUnsafeBlocks(), source, allowUnsafe: true);

        diags.ShouldNotContain(d => d.Id == AnalyzerIds.Sash0005ApiStructRequiresAllowUnsafeBlocks.Id);
    }

    [Fact]
    public async Task Sash0005_should_not_report_for_attributes_other_than_OpenMpApi()
    {
        const string source = """
            using System;

            [Serializable]
            public class Plain { }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0005ApiStructRequiresAllowUnsafeBlocks(), source, allowUnsafe: false);

        diags.ShouldNotContain(d => d.Id == AnalyzerIds.Sash0005ApiStructRequiresAllowUnsafeBlocks.Id);
    }
}
