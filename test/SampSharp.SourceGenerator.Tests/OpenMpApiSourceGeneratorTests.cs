using SampSharp.SourceGenerator.Generators;
using VerifyXunit;

namespace SampSharp.SourceGenerator.Tests;

public class OpenMpApiSourceGeneratorTests : VerifyBase
{
    public OpenMpApiSourceGeneratorTests() : base() { }
    [Fact]
    public Task SimpleApi_WithPrimitiveReturnTypes_GeneratesStub()
    {
        var source = """
            using SampSharp.OpenMp.Core;

            namespace TestApi;

            [OpenMpApi]
            public readonly partial struct ITestApi
            {
                public partial int GetId();
                public partial void SetName(string name);
                public partial bool IsActive();
            }
            """;

        var driver = CompilationHelper.RunGenerator(source, new OpenMpApiSourceGenerator());

        return Verify(driver);
    }

    [Fact]
    public Task ApiWithBaseType_GeneratesForwardingAndCastMembers()
    {
        var source = """
            using SampSharp.OpenMp.Core;

            namespace TestApi;

            [OpenMpApi]
            public readonly partial struct IBase
            {
                public partial int GetId();
            }

            [OpenMpApi(typeof(IBase))]
            public readonly partial struct IDerived
            {
                public partial string GetName();
            }
            """;

        var driver = CompilationHelper.RunGenerator(source, new OpenMpApiSourceGenerator());

        return Verify(driver);
    }

    [Fact]
    public Task ApiMarkedAsPartial_GeneratesPartialStub()
    {
        var source = """
            using SampSharp.OpenMp.Core;

            namespace TestApi;

            [OpenMpApi]
            [OpenMpApiPartial]
            public readonly partial struct IPartialApi
            {
                public partial int GetComponentType();
                public partial string GetName();
            }
            """;

        var driver = CompilationHelper.RunGenerator(source, new OpenMpApiSourceGenerator());

        return Verify(driver);
    }

    [Fact]
    public Task ApiWithCustomLibraryAndNativeTypeName_UsesProvidedValues()
    {
        var source = """
            using SampSharp.OpenMp.Core;

            namespace TestApi;

            [OpenMpApi(Library = "MyLib", NativeTypeName = "MyNativeType")]
            public readonly partial struct ICustomLib
            {
                public partial int GetValue();
            }
            """;

        var driver = CompilationHelper.RunGenerator(source, new OpenMpApiSourceGenerator());

        return Verify(driver);
    }

    [Fact]
    public Task ApiWithRefAndOutParameters_GeneratesCorrectPInvoke()
    {
        var source = """
            using SampSharp.OpenMp.Core;

            namespace TestApi;

            [OpenMpApi]
            public readonly partial struct IRefOutApi
            {
                public partial void GetValues(out int x, out int y);
                public partial void Transform(ref int value);
            }
            """;

        var driver = CompilationHelper.RunGenerator(source, new OpenMpApiSourceGenerator());

        return Verify(driver);
    }

    [Fact]
    public Task ApiWithOverloadAttribute_AppendsSuffixToNativeName()
    {
        var source = """
            using SampSharp.OpenMp.Core;

            namespace TestApi;

            [OpenMpApi]
            public readonly partial struct IOverloadApi
            {
                [OpenMpApiOverload("2")]
                public partial int GetValue(int x, int y);
                public partial int GetValue(int x);
            }
            """;

        var driver = CompilationHelper.RunGenerator(source, new OpenMpApiSourceGenerator());

        return Verify(driver);
    }

    [Fact]
    public Task ApiWithFunctionAttribute_UsesCustomFunctionName()
    {
        var source = """
            using SampSharp.OpenMp.Core;

            namespace TestApi;

            [OpenMpApi]
            public readonly partial struct IFunctionApi
            {
                [OpenMpApiFunction("customFunctionName")]
                public partial int DoSomething();
            }
            """;

        var driver = CompilationHelper.RunGenerator(source, new OpenMpApiSourceGenerator());

        return Verify(driver);
    }
}
