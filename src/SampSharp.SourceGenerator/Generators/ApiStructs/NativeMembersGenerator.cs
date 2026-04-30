using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SampSharp.SourceGenerator.Generators.Marshalling;
using SampSharp.SourceGenerator.Models;

namespace SampSharp.SourceGenerator.Generators.ApiStructs;

public static class NativeMembersGenerator
{
    public static IEnumerable<MemberDeclarationSyntax> GenerateNativeMethods(StructStubGenerationContext ctx)
    {
        return ctx.Methods
            .Select(GenerateNativeMethod)
            .Where(x => x != null);
    }

    /// <summary>
    /// Returns a method declaration for a native method including marshalling of parameters and return value.
    /// </summary>
    private static MemberDeclarationSyntax GenerateNativeMethod(ApiMethodStubGenerationContext ctx)
    {
        var xx = new ApiMethodMarshallingGenerator();
        return xx.GenerateNativeMethod(ctx);
    }
}