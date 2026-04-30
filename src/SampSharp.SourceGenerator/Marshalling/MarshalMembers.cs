using Microsoft.CodeAnalysis;

namespace SampSharp.SourceGenerator.Marshalling;

public record MarshalMembers(
    IMethodSymbol? StatefulFreeMethod,
    IMethodSymbol? StatefulFromManagedMethod,
    IMethodSymbol? StatefulFromManagedWithBufferMethod,
    IMethodSymbol? StatefulFromUnmanagedMethod,
    IMethodSymbol? StatefulToManagedMethod,
    IMethodSymbol? StatefulToManagedFinallyMethod,
    IMethodSymbol? StatefulToUnmanagedMethod,
    IMethodSymbol? StatefulOnInvokedMethod,
    IMethodSymbol? StatefulGetPinnableReferenceMethod,
    IMethodSymbol? StatelessGetPinnableReferenceMethod,
    IMethodSymbol? StatelessConvertToUnmanagedMethod,
    IMethodSymbol? StatelessConvertToUnmanagedWithBufferMethod,
    IMethodSymbol? StatelessConvertToManagedMethod,
    IMethodSymbol? StatelessConvertToManagedFinallyMethod,
    IMethodSymbol? StatelessFreeMethod,

    IPropertySymbol? BufferSizeProperty
);