using SampSharp.Analyzer;
using SampSharp.Analyzer.Analyzers;
using Shouldly;
using Xunit;

namespace SampSharp.CodeFixes.Tests;

public class Sash0001ExtensionAttributeCodeFixProviderTests
{
    [Fact]
    public void FixableDiagnosticIds_should_contain_only_Sash0001()
    {
        var provider = new Sash0001ExtensionAttributeCodeFixProvider();
        provider.FixableDiagnosticIds.ShouldBe(new[] { AnalyzerIds.Sash0001MissingExtensionAttribute.Id });
    }

    [Fact]
    public async Task Fix_should_add_ExtensionAttribute_to_class_extending_Extension()
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
    public async Task Fix_should_add_using_for_Core_namespace_when_missing()
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
    public async Task Fix_should_not_add_duplicate_using()
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
    public void GetFixAllProvider_should_return_non_null_batch_fixer()
    {
        var provider = new Sash0001ExtensionAttributeCodeFixProvider();
        provider.GetFixAllProvider().ShouldNotBeNull();
    }
}
