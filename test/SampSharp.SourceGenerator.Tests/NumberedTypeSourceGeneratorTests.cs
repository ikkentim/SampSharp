using Microsoft.CodeAnalysis.CSharp;
using SampSharp.SourceGenerator.Generators;
using VerifyXunit;

namespace SampSharp.SourceGenerator.Tests;

public class NumberedTypeSourceGeneratorTests : VerifyBase
{
    public NumberedTypeSourceGeneratorTests() : base() { }
    [Fact]
    public Task SingleAttribute_GeneratesNumberedStruct()
    {
        var source = """
            using SampSharp.OpenMp.Core;
            using System.Runtime.InteropServices;

            namespace TestNs;

            [NumberedTypeGenerator(nameof(Size), 32)]
            [StructLayout(LayoutKind.Explicit)]
            public readonly struct MyString16
            {
                private const int Size = 16;

                public MyString16(int value) { Value = value; }

                public readonly int Value;
            }
            """;

        var compilation = CompilationHelper.CreateCompilation(source);
        var generator = new NumberedTypeSourceGenerator();
        var driver = CSharpGeneratorDriver.Create(generator).RunGenerators(compilation);

        return Verify(driver);
    }

    [Fact]
    public Task MultipleAttributes_GeneratesMultipleNumberedStructs()
    {
        var source = """
            using SampSharp.OpenMp.Core;
            using System.Runtime.InteropServices;

            namespace TestNs;

            [NumberedTypeGenerator(nameof(Size), 24)]
            [NumberedTypeGenerator(nameof(Size), 32)]
            [StructLayout(LayoutKind.Explicit)]
            public readonly struct MyString16
            {
                private const int Size = 16;

                public MyString16(int value) { Value = value; }

                public readonly int Value;
            }
            """;

        var compilation = CompilationHelper.CreateCompilation(source);
        var generator = new NumberedTypeSourceGenerator();
        var driver = CSharpGeneratorDriver.Create(generator).RunGenerators(compilation);

        return Verify(driver);
    }
}
