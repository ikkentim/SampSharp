using Microsoft.CodeAnalysis.Diagnostics;
using SampSharp.Analyzer;
using SampSharp.Analyzer.Analyzers;
using Shouldly;
using Xunit;

namespace SampSharp.Analyzer.Tests;

public class SupportedDiagnosticsTests
{
    public static TheoryData<DiagnosticAnalyzer, string> Cases() => new()
    {
        { new Sash0001ExtensionAttributeAnalyzer(), AnalyzerIds.Sash0001MissingExtensionAttribute.Id },
        { new Sash0002EventHandlerWithGenericParametersAnalyzer(), AnalyzerIds.Sash0002GenericEventHandlerUnsupported.Id },
        { new Sash0003ApiStructMustBeReadonlyPartialAnalyzer(), AnalyzerIds.Sash0003ApiStructMustBeReadonlyPartial.Id },
        { new Sash0004ApiStructMethodMarshalRefReturnNotSupportedAnalyzer(), AnalyzerIds.Sash0004ApiStructMarshalRefReturnUnsupported.Id },
        { new Sash0005ApiStructRequiresAllowUnsafeBlocks(), AnalyzerIds.Sash0005ApiStructRequiresAllowUnsafeBlocks.Id },
        { new Sash0006ApiStructBaseTypeMustBeApiStruct(), AnalyzerIds.Sash0006ApiStructBaseTypeMustBeApiStruct.Id },
        { new Sash0007ApiStructMustNotContainFields(), AnalyzerIds.Sash0007ApiStructMustNotContainFields.Id },
        { new Sash0008EventHandlerMarshalRefReturnUnsupported(), AnalyzerIds.Sash0008EventHandlerMarshalRefReturnUnsupported.Id },
    };

    [Theory]
    [MemberData(nameof(Cases))]
    public void Analyzer_exposes_expected_single_diagnostic(DiagnosticAnalyzer analyzer, string expectedId)
    {
        analyzer.SupportedDiagnostics.Length.ShouldBe(1);
        analyzer.SupportedDiagnostics[0].Id.ShouldBe(expectedId);
    }
}

public class AnalyzerIdsTests
{
    [Fact]
    public void All_descriptors_have_correctness_category()
    {
        var descriptors = new[]
        {
            AnalyzerIds.Sash0001MissingExtensionAttribute,
            AnalyzerIds.Sash0002GenericEventHandlerUnsupported,
            AnalyzerIds.Sash0003ApiStructMustBeReadonlyPartial,
            AnalyzerIds.Sash0004ApiStructMarshalRefReturnUnsupported,
            AnalyzerIds.Sash0005ApiStructRequiresAllowUnsafeBlocks,
            AnalyzerIds.Sash0006ApiStructBaseTypeMustBeApiStruct,
            AnalyzerIds.Sash0007ApiStructMustNotContainFields,
            AnalyzerIds.Sash0008EventHandlerMarshalRefReturnUnsupported,
        };

        foreach (var d in descriptors)
        {
            d.Category.ShouldBe(DiagnosticCategories.Correctness);
            d.DefaultSeverity.ShouldBe(Microsoft.CodeAnalysis.DiagnosticSeverity.Error);
            d.IsEnabledByDefault.ShouldBeTrue();
            d.Id.ShouldStartWith("SASH");
        }
    }

    [Fact]
    public void Ids_are_unique()
    {
        var ids = new[]
        {
            AnalyzerIds.Sash0001MissingExtensionAttribute.Id,
            AnalyzerIds.Sash0002GenericEventHandlerUnsupported.Id,
            AnalyzerIds.Sash0003ApiStructMustBeReadonlyPartial.Id,
            AnalyzerIds.Sash0004ApiStructMarshalRefReturnUnsupported.Id,
            AnalyzerIds.Sash0005ApiStructRequiresAllowUnsafeBlocks.Id,
            AnalyzerIds.Sash0006ApiStructBaseTypeMustBeApiStruct.Id,
            AnalyzerIds.Sash0007ApiStructMustNotContainFields.Id,
            AnalyzerIds.Sash0008EventHandlerMarshalRefReturnUnsupported.Id,
        };

        ids.Distinct().Count().ShouldBe(ids.Length);
    }
}
