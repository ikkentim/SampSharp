using SampSharp.Analyzer;
using SampSharp.Analyzer.Analyzers;
using Shouldly;
using Xunit;

namespace SampSharp.Analyzer.Tests;

public class Sash0002EventHandlerWithGenericParametersAnalyzerTests
{
    [Fact]
    public async Task Reports_when_event_handler_interface_has_type_parameters()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpEventHandler]
            public interface IMyHandler<T> { }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0002EventHandlerWithGenericParametersAnalyzer(), source);

        var match = diags.Where(d => d.Id == AnalyzerIds.Sash0002GenericEventHandlerUnsupported.Id).ToList();
        match.Count.ShouldBe(1);
        match[0].GetMessage().ShouldContain("IMyHandler");
    }

    [Fact]
    public async Task Does_not_report_for_non_generic_event_handler()
    {
        const string source = """
            using SampSharp.OpenMp.Core;

            [OpenMpEventHandler]
            public interface IMyHandler { }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0002EventHandlerWithGenericParametersAnalyzer(), source);

        diags.ShouldNotContain(d => d.Id == AnalyzerIds.Sash0002GenericEventHandlerUnsupported.Id);
    }

    [Fact]
    public async Task Does_not_report_for_generic_interface_without_attribute()
    {
        const string source = """
            public interface IPlainGeneric<T> { }
            """;

        var diags = await AnalyzerTestHelper.GetDiagnosticsAsync(
            new Sash0002EventHandlerWithGenericParametersAnalyzer(), source);

        diags.ShouldNotContain(d => d.Id == AnalyzerIds.Sash0002GenericEventHandlerUnsupported.Id);
    }
}
