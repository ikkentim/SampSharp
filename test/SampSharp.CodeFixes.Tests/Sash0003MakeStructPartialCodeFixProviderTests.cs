using SampSharp.Analyzer;
using SampSharp.Analyzer.Analyzers;
using Shouldly;
using Xunit;

namespace SampSharp.CodeFixes.Tests;

public class Sash0003MakeStructPartialCodeFixProviderTests
{
    [Fact]
    public void Fixable_id_matches_Sash0003()
    {
        var provider = new Sash0003MakeStructPartialCodeFixProvider();
        provider.FixableDiagnosticIds.ShouldBe(new[] { AnalyzerIds.Sash0003ApiStructMustBeReadonlyPartial.Id });
    }

    [Fact]
    public async Task Adds_partial_when_missing()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpApi]
            public readonly struct MyApi { }
            """;

        var result = await CodeFixTestHelper.ApplyFixAsync(
            new Sash0003ApiStructMustBeReadonlyPartialAnalyzer(),
            new Sash0003MakeStructPartialCodeFixProvider(),
            source);

        result.ShouldContain("readonly");
        result.ShouldContain("partial struct MyApi");
    }

    [Fact]
    public async Task Adds_readonly_when_missing()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpApi]
            public partial struct MyApi { }
            """;

        var result = await CodeFixTestHelper.ApplyFixAsync(
            new Sash0003ApiStructMustBeReadonlyPartialAnalyzer(),
            new Sash0003MakeStructPartialCodeFixProvider(),
            source);

        result.ShouldContain("readonly");
        result.ShouldContain("partial struct MyApi");
    }

    [Fact]
    public async Task Adds_both_when_both_missing()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpApi]
            public struct MyApi { }
            """;

        var result = await CodeFixTestHelper.ApplyFixAsync(
            new Sash0003ApiStructMustBeReadonlyPartialAnalyzer(),
            new Sash0003MakeStructPartialCodeFixProvider(),
            source);

        result.ShouldContain("readonly");
        result.ShouldContain("partial struct MyApi");
    }

    [Fact]
    public void Provides_batch_fix_all_provider()
    {
        new Sash0003MakeStructPartialCodeFixProvider().GetFixAllProvider().ShouldNotBeNull();
    }
}
