using SampSharp.Analyzer;
using SampSharp.Analyzer.Analyzers;
using Shouldly;
using Xunit;

namespace SampSharp.Analyzer.Tests;

public class Sash0006ApiStructBaseTypeMustBeApiStructTests
{
    [Fact]
    public async Task Sash0006_should_report_when_base_type_in_OpenMpApi_argument_is_not_an_api_struct()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            public readonly partial struct NotApi { }

            [OpenMpApi(typeof(NotApi))]
            public readonly partial struct MyApi { }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0006ApiStructBaseTypeMustBeApiStruct(), source);

        var match = diags.Where(d => d.Id == AnalyzerIds.Sash0006ApiStructBaseTypeMustBeApiStruct.Id).ToList();
        match.Count.ShouldBe(1);
        match[0].GetMessage().ShouldContain("NotApi");
        match[0].GetMessage().ShouldContain("MyApi");
    }

    [Fact]
    public async Task Sash0006_should_not_report_when_base_type_is_an_api_struct()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpApi]
            public readonly partial struct BaseApi { }

            [OpenMpApi(typeof(BaseApi))]
            public readonly partial struct MyApi { }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0006ApiStructBaseTypeMustBeApiStruct(), source);

        diags.ShouldNotContain(d => d.Id == AnalyzerIds.Sash0006ApiStructBaseTypeMustBeApiStruct.Id);
    }

    [Fact]
    public async Task Sash0006_should_not_report_when_OpenMpApi_has_no_arguments()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpApi]
            public readonly partial struct MyApi { }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0006ApiStructBaseTypeMustBeApiStruct(), source);

        diags.ShouldNotContain(d => d.Id == AnalyzerIds.Sash0006ApiStructBaseTypeMustBeApiStruct.Id);
    }

    [Fact]
    public async Task Sash0006_should_not_report_for_unrelated_attribute()
    {
        const string source = """
            using System;

            [AttributeUsage(AttributeTargets.All)]
            public class Other : Attribute { }

            public readonly partial struct NotApi { }

            [Other]
            public readonly partial struct MyApi { }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0006ApiStructBaseTypeMustBeApiStruct(), source);

        diags.ShouldNotContain(d => d.Id == AnalyzerIds.Sash0006ApiStructBaseTypeMustBeApiStruct.Id);
    }
}
