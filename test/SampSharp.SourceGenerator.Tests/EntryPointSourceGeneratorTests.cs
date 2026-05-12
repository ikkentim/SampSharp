using Microsoft.CodeAnalysis.CSharp;
using SampSharp.SourceGenerator.Generators;
using VerifyXunit;

namespace SampSharp.SourceGenerator.Tests;

public class EntryPointSourceGeneratorTests : VerifyBase
{
    public EntryPointSourceGeneratorTests() : base() { }
    [Fact]
    public Task SimpleStartupClass_GeneratesEntryPoint()
    {
        var source = """
            namespace MyGame;

            public class MyStartup : global::SampSharp.OpenMp.Core.IStartup
            {
                public void Initialize(global::SampSharp.OpenMp.Core.IStartupContext context) { }
            }
            """;

        var compilation = CompilationHelper.CreateCompilation(source);
        var generator = new EntryPointSourceGenerator();
        var driver = CSharpGeneratorDriver.Create(generator).RunGenerators(compilation);

        return Verify(driver);
    }

    [Fact]
    public Task NestedNamespaceStartupClass_GeneratesEntryPoint()
    {
        var source = """
            namespace My.Game.Server;

            public class Startup : global::SampSharp.OpenMp.Core.IStartup
            {
                public void Initialize(global::SampSharp.OpenMp.Core.IStartupContext context) { }
            }
            """;

        var compilation = CompilationHelper.CreateCompilation(source);
        var generator = new EntryPointSourceGenerator();
        var driver = CSharpGeneratorDriver.Create(generator).RunGenerators(compilation);

        return Verify(driver);
    }

    [Fact]
    public Task NonStartupClass_ProducesNoOutput()
    {
        var source = """
            namespace MyGame;

            public class NotAStartup
            {
                public void Initialize() { }
            }
            """;

        var compilation = CompilationHelper.CreateCompilation(source);
        var generator = new EntryPointSourceGenerator();
        var driver = CSharpGeneratorDriver.Create(generator).RunGenerators(compilation);

        return Verify(driver);
    }
}
