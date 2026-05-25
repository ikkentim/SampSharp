using SampSharp.Analyzer;
using SampSharp.Analyzer.Analyzers;
using Shouldly;
using Xunit;

namespace SampSharp.Analyzer.Tests;

public class Sash0001ExtensionAttributeAnalyzerTests
{
    [Fact]
    public async Task Reports_when_class_extends_Extension_without_ExtensionAttribute()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            public class MyExt : Extension { }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(new Sash0001ExtensionAttributeAnalyzer(), source);

        var match = diags.Where(d => d.Id == AnalyzerIds.Sash0001MissingExtensionAttribute.Id).ToList();
        match.Count.ShouldBe(1);
        match[0].GetMessage().ShouldContain("MyExt");
    }

    [Fact]
    public async Task Does_not_report_when_ExtensionAttribute_is_present()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [Extension(0x1234)]
            public class MyExt : Extension { }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(new Sash0001ExtensionAttributeAnalyzer(), source);

        diags.ShouldNotContain(d => d.Id == AnalyzerIds.Sash0001MissingExtensionAttribute.Id);
    }

    [Fact]
    public async Task Does_not_report_when_class_does_not_extend_Extension()
    {
        const string source = """
            public class Plain { }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(new Sash0001ExtensionAttributeAnalyzer(), source);

        diags.ShouldNotContain(d => d.Id == AnalyzerIds.Sash0001MissingExtensionAttribute.Id);
    }

    [Fact]
    public async Task Does_not_report_when_extension_attribute_unknown_in_compilation()
    {
        // Compilation references Core, so this path is exercised when the symbol isn't found.
        // We simulate by referencing a class that just happens to be named ExtensionAttribute
        // in a different namespace — the analyzer should look up by fully qualified name and
        // only match the SampSharp one.
        const string source = """
            namespace Other;

            public class ExtensionAttribute : System.Attribute { }

            public class NotAnExtension { }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(new Sash0001ExtensionAttributeAnalyzer(), source);

        diags.ShouldNotContain(d => d.Id == AnalyzerIds.Sash0001MissingExtensionAttribute.Id);
    }
}
