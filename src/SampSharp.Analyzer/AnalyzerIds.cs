using Microsoft.CodeAnalysis;

namespace SampSharp.Analyzer;

public static class AnalyzerIds
{
    public static readonly DiagnosticDescriptor Sash0001MissingExtensionAttribute = new(
        "SASH0001", 
        "Missing 'ExtensionAttribute'",
        "Type '{0} 'is missing the 'ExtensionAttribute'", 
        DiagnosticCategories.Correctness, 
        DiagnosticSeverity.Error, 
        true, 
        "Type {0} must have the 'ExtensionAttribute'.");
    
    public static readonly DiagnosticDescriptor Sash0002GenericEventHandlerUnsupported = new(
        "SASH0002", 
        "Unsupported generic parameters in open.mp event handler",
        "Unsupported generic parameters in open.mp event handler type '{0}'", 
        DiagnosticCategories.Correctness, 
        DiagnosticSeverity.Error, 
        true, 
        "open.mp event handler type '{0}' must not contain generic parameters.");
    
    public static readonly DiagnosticDescriptor Sash0003ApiStructMustBeReadonlyPartial = new(
        "SASH0003", 
        "open.mp api struct must be readonly partial",
        "open.mp api struct '{0}' must be marked as readonly and partial", 
        DiagnosticCategories.Correctness, 
        DiagnosticSeverity.Error, 
        true, 
        "open.mp api struct type '{0}' must be marked as readonly and partial.");

    public static readonly DiagnosticDescriptor Sash0004ApiStructMarshalRefReturnUnsupported = new(
        "SASH0004", 
        "'ref return' marshalling not supported for open.mp api structs",
        "open.mp api function '{0}' use of 'ref return' in combination with marshalling not supported", 
        DiagnosticCategories.Correctness, 
        DiagnosticSeverity.Error, 
        true, 
        "open.mp api function '{0}' returns a value by ref while also using a marshaller which is not supported.");

    public static readonly DiagnosticDescriptor Sash0005ApiStructRequiresAllowUnsafeBlocks = new(
        "SASH0005", 
        "open.mp api struct requires 'AllowUnsafeBlocks'",
        "open.mp api struct require the 'AllowUnsafeBlocks' option to be set to true", 
        DiagnosticCategories.Correctness, 
        DiagnosticSeverity.Error, 
        true, 
        "open.mp api struct require the 'AllowUnsafeBlocks' option to be set to true.");
    
    public static readonly DiagnosticDescriptor Sash0006ApiStructBaseTypeMustBeApiStruct = new(
        "SASH0006", 
        "open.mp api struct base types must be open.mp api structs",
        "base type '{0}' in base type list of open.mp api struct '{1}' is not an open.mp api struct", 
        DiagnosticCategories.Correctness, 
        DiagnosticSeverity.Error, 
        true, 
        "base type '{0}' in base type list of open.mp api struct '{1}' is not an open.mp api struct.");
     
    public static readonly DiagnosticDescriptor Sash0007ApiStructMustNotContainFields = new(
        "SASH0007", 
        "open.mp api struct may not contain fields",
        "open.mp api struct '{0}' may not contain fields", 
        DiagnosticCategories.Correctness, 
        DiagnosticSeverity.Error, 
        true, 
        "open.mp api struct '{0}' may not contain fields or properties with backing fields.");

    public static readonly DiagnosticDescriptor Sash0008EventHandlerMarshalRefReturnUnsupported = new(
        "SASH0008", 
        "'ref return' marshalling not supported for open.mp event handler",
        "open.mp event handler function '{0}' use of 'ref return' in combination with marshalling not supported", 
        DiagnosticCategories.Correctness, 
        DiagnosticSeverity.Error, 
        true, 
        "open.mp event handler function '{0}' returns a value by ref while also using a marshaller which is not supported.");

}
