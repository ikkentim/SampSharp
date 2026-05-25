using SampSharp.Analyzer;
using SampSharp.Analyzer.Analyzers;
using Shouldly;
using Xunit;

namespace SampSharp.CodeFixes.Tests;

public class Sash0005AllowUnsafeBlocksCodeFixProviderTests
{
    [Fact]
    public void FixableDiagnosticIds_should_contain_only_Sash0005()
    {
        var provider = new Sash0005AllowUnsafeBlocksCodeFixProvider();
        provider.FixableDiagnosticIds.ShouldBe(new[] { AnalyzerIds.Sash0005ApiStructRequiresAllowUnsafeBlocks.Id });
    }

    [Fact]
    public async Task Fix_should_enable_AllowUnsafe_on_project_compilation_options()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpApi]
            public readonly partial struct MyApi { }
            """;

        var options = await CodeFixTestHelper.ApplyFixAndGetCompilationOptionsAsync(
            new Sash0005ApiStructRequiresAllowUnsafeBlocks(),
            new Sash0005AllowUnsafeBlocksCodeFixProvider(),
            source,
            allowUnsafe: false);

        options.ShouldNotBeNull();
        options!.AllowUnsafe.ShouldBeTrue();
    }

    [Fact]
    public void GetFixAllProvider_should_return_non_null_batch_fixer()
    {
        new Sash0005AllowUnsafeBlocksCodeFixProvider().GetFixAllProvider().ShouldNotBeNull();
    }
}
