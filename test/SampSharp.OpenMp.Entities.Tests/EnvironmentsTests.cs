using System.Reflection;
using Moq;
using SampSharp.Entities;
using SampSharp.OpenMp.Core.Api;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Entities.Tests;

public class EnvironmentsTests
{
    [Fact]
    public void Production_should_equal_string_Production()
    {
        Environments.Production.ShouldBe("Production");
    }

    [Fact]
    public void Development_should_equal_string_Development()
    {
        Environments.Development.ShouldBe("Development");
    }

    [Fact]
    public void Staging_should_equal_string_Staging()
    {
        Environments.Staging.ShouldBe("Staging");
    }
}

public class SampSharpEnvironmentTests
{
    [Fact]
    public void Ctor_should_store_constructor_arguments()
    {
        var asm = typeof(SampSharpEnvironmentTests).Assembly;
        var core = default(ICore);
        var components = default(IComponentList);
        var handles = new Mock<ISafeComponentHandleProvider>().Object;
        var env = new SampSharpEnvironment(asm, core, components, handles, Environments.Development);
        env.EntryAssembly.ShouldBe(asm);
        env.Core.ShouldBe(core);
        env.Components.ShouldBe(components);
        env.SafeComponentHandleProvider.ShouldBe(handles);
        env.EnvironmentName.ShouldBe(Environments.Development);
    }

    [Fact]
    public void Equals_should_be_true_for_records_with_identical_values()
    {
        var asm = Assembly.GetExecutingAssembly();
        var handles = new Mock<ISafeComponentHandleProvider>().Object;
        var a = new SampSharpEnvironment(asm, default, default, handles, "X");
        var b = new SampSharpEnvironment(asm, default, default, handles, "X");
        a.ShouldBe(b);
    }

    [Fact]
    public void Equals_should_be_false_for_records_with_different_environment_name()
    {
        var asm = Assembly.GetExecutingAssembly();
        var handles = new Mock<ISafeComponentHandleProvider>().Object;
        var a = new SampSharpEnvironment(asm, default, default, handles, "Production");
        var b = new SampSharpEnvironment(asm, default, default, handles, "Development");
        a.ShouldNotBe(b);
    }

    [Fact]
    public void With_expression_should_replace_field()
    {
        var asm = Assembly.GetExecutingAssembly();
        var handles = new Mock<ISafeComponentHandleProvider>().Object;
        var original = new SampSharpEnvironment(asm, default, default, handles, "Production");
        var modified = original with { EnvironmentName = "Staging" };
        modified.EnvironmentName.ShouldBe("Staging");
        original.EnvironmentName.ShouldBe("Production");
    }
}
