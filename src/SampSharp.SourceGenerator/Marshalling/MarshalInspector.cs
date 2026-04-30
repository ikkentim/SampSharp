using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using SampSharp.SourceGenerator.Helpers;
using static SampSharp.SourceGenerator.Marshalling.ShapeConstants;

namespace SampSharp.SourceGenerator.Marshalling;

public static class MarshalInspector
{
    public static MarshalMembers GetMembers(CustomMarshallerInfo info)
    {
        var type = info.MarshallerType.Symbol;
        if (info.IsStateful)
        {
            return new MarshalMembers(
                StatefulFreeMethod: GetMethod(type, true, MethodFree), 
                StatefulFromManagedMethod: GetMethod(type, true, MethodFromManaged, false, info.ManagedType.Symbol),
                StatefulFromManagedWithBufferMethod: GetMethod(type, true, MethodFromManaged, false, x => x.Type.IsSame(info.ManagedType.Symbol), x => IsSpanByte(x.Type)),
                StatefulFromUnmanagedMethod: GetMethod(type, true, MethodFromUnmanaged, parameterCount: 1),

                StatefulToManagedMethod: GetMethod(type, true, MethodToManaged),
                StatefulToManagedFinallyMethod: GetMethod(type, true, MethodToManagedFinally),
                StatefulToUnmanagedMethod: GetMethod(type, true, MethodToUnmanaged),

                StatefulOnInvokedMethod: GetMethod(type, true, MethodOnInvoked),
                StatefulGetPinnableReferenceMethod: GetMethod(type, true, MethodGetPinnableReference, true),
                StatelessGetPinnableReferenceMethod: GetMethod(type, false, MethodGetPinnableReference, true, info.ManagedType.Symbol),

                StatelessConvertToUnmanagedMethod: null,
                StatelessConvertToUnmanagedWithBufferMethod: null,
                StatelessConvertToManagedMethod: null,
                StatelessConvertToManagedFinallyMethod: null,
                StatelessFreeMethod: null,

                BufferSizeProperty: GetStaticProperty(type, PropertyBufferSize, x => x.SpecialType == SpecialType.System_Int32)
            );
        }

        return new MarshalMembers(
            StatefulFreeMethod: null, 
            StatefulFromManagedMethod: null,
            StatefulFromManagedWithBufferMethod: null,
            StatefulFromUnmanagedMethod: null,

            StatefulToManagedMethod: null,
            StatefulToManagedFinallyMethod: null,
            StatefulToUnmanagedMethod: null,

            StatefulOnInvokedMethod: null,
            StatefulGetPinnableReferenceMethod: null,
            StatelessGetPinnableReferenceMethod: GetMethod(type, false, MethodGetPinnableReference, true, info.ManagedType.Symbol),

            StatelessConvertToUnmanagedMethod: GetMethod(type, false, MethodConvertToUnmanaged, false, info.ManagedType.Symbol),
            StatelessConvertToUnmanagedWithBufferMethod: GetMethod(type, false, MethodConvertToUnmanaged, false, x => x.Type.IsSame(info.ManagedType.Symbol), x => IsSpanByte(x.Type)),
            StatelessConvertToManagedMethod: GetMethod(type, false, MethodConvertToManaged, parameterCount: 1),
            StatelessConvertToManagedFinallyMethod: GetMethod(type, false, MethodConvertToManagedFinally, parameterCount: 1),
            StatelessFreeMethod: GetMethod(type, false, MethodFree, parameterCount: 1),

            BufferSizeProperty: GetStaticProperty(type, PropertyBufferSize, x => x.SpecialType == SpecialType.System_Int32)
        );
    }

    private static IMethodSymbol? GetMethod(ITypeSymbol type, bool stateful, string name, bool returnsByRef = false, int parameterCount = 0)
    {
        return type
            .GetMembers(name)
            .OfType<IMethodSymbol>()
            .FirstOrDefault(x =>
            {
                if (stateful == x.IsStatic || x.Parameters.Length != parameterCount || x.ReturnsByRef != returnsByRef)
                {
                    return false;
                }

                return !Array.Empty<ITypeSymbol>().Where((t, i) => !x.Parameters[i].Type.IsSame(t))
                    .Any();
            });
    }

    
    private static bool IsSpanByte(ITypeSymbol type)
    {
        return type is INamedTypeSymbol named && named.ToDisplayString() == Constants.SpanOfBytesFQN;
    }

    private static IMethodSymbol? GetMethod(ITypeSymbol type, bool stateful, string name, bool returnsByRef = false, params ITypeSymbol[] paramTypes)
    {
        return type
            .GetMembers(name)
            .OfType<IMethodSymbol>()
            .FirstOrDefault(x =>
            {
                if (stateful == x.IsStatic || x.Parameters.Length != paramTypes.Length)
                {
                    return false;
                }

                if (x.ReturnsByRef != returnsByRef)
                {
                    return false;
                }

                return !paramTypes.Where((t, i) => !x.Parameters[i].Type.IsSame(t))
                    .Any();
            });

    }
    
    private static IMethodSymbol? GetMethod(ITypeSymbol type, bool stateful, string name, bool returnsByRef, params Func<IParameterSymbol, bool>[] paramTypes)
    {
        return type
            .GetMembers(name)
            .OfType<IMethodSymbol>()
            .FirstOrDefault(x =>
            {
                if (stateful == x.IsStatic || x.Parameters.Length != paramTypes.Length)
                {
                    return false;
                }

                if (x.ReturnsByRef != returnsByRef)
                {
                    return false;
                }

                return !paramTypes.Where((check, i) => !check(x.Parameters[i]))
                    .Any();
            });

    }

    private static IPropertySymbol? GetStaticProperty(ITypeSymbol type, string name, Func<ITypeSymbol, bool> propertyType)
    {
        return type
            .GetMembers(name)
            .OfType<IPropertySymbol>()
            .FirstOrDefault(x => x.IsStatic && propertyType(x.Type));
    }
}