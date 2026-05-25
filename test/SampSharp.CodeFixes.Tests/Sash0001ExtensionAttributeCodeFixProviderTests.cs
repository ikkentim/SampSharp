using SampSharp.Analyzer;
using SampSharp.Analyzer.Analyzers;
using Shouldly;
using Xunit;

namespace SampSharp.CodeFixes.Tests;

public class Sash0001ExtensionAttributeCodeFixProviderTests
{
    [Fact]
    public void Fixable_id_matches_Sash0001()
    {
        var provider = new Sash0001ExtensionAttributeCodeFixProvider();
        provider.FixableDiagnosticIds.ShouldBe(new[] { AnalyzerIds.Sash0001MissingExtensionAttribute.Id });
    }

    [Fact]
    public async Task Adds_ExtensionAttribute_to_class_extending_Extension()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            public class MyExt : Extension { }
            """;

        var result = await CodeFixTestHelper.ApplyFixAsync(
            new Sash0001ExtensionAttributeAnalyzer(),
            new Sash0001ExtensionAttributeCodeFixProvider(),
            source);

        result.ShouldContain("[Extension(0x");
        result.ShouldContain("public class MyExt : Extension");
    }

    [Fact]
    public async Task Adds_using_for_Core_namespace_when_missing()
    {
        const string source = """
            public class MyExt : SampSharp.OpenMp.Core.Extension { }
            """;

        var result = await CodeFixTestHelper.ApplyFixAsync(
            new Sash0001ExtensionAttributeAnalyzer(),
            new Sash0001ExtensionAttributeCodeFixProvider(),
            source);

        result.ShouldContain("using SampSharp.OpenMp.Core;");
        result.ShouldContain("[Extension(");
    }

    [Fact]
    public async Task Does_not_add_duplicate_using()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            public class MyExt : Extension { }
            """;

        var result = await CodeFixTestHelper.ApplyFixAsync(
            new Sash0001ExtensionAttributeAnalyzer(),
            new Sash0001ExtensionAttributeCodeFixProvider(),
            source);

        var occurrences = result.Split("using SampSharp.OpenMp.Core;").Length - 1;
        occurrences.ShouldBe(1);
    }

    [Fact]
    public void Provides_batch_fix_all_provider()
    {
        var provider = new Sash0001ExtensionAttributeCodeFixProvider();
        provider.GetFixAllProvider().ShouldNotBeNull();
    }
}
