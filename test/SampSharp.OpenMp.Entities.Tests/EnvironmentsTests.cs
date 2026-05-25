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
    public void Production_constant_value()
    {
        Environments.Production.ShouldBe("Production");
    }

    [Fact]
    public void Development_constant_value()
    {
        Environments.Development.ShouldBe("Development");
    }

    [Fact]
    public void Staging_constant_value()
    {
        Environments.Staging.ShouldBe("Staging");
    }
}

public class SampSharpEnvironmentTests
{
    [Fact]
    public void Record_stores_constructor_arguments()
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
    public void Records_with_identical_values_are_equal()
    {
        var asm = Assembly.GetExecutingAssembly();
        var handles = new Mock<ISafeComponentHandleProvider>().Object;
        var a = new SampSharpEnvironment(asm, default, default, handles, "X");
        var b = new SampSharpEnvironment(asm, default, default, handles, "X");
        a.ShouldBe(b);
    }

    [Fact]
    public void Records_with_different_environment_name_are_not_equal()
    {
        var asm = Assembly.GetExecutingAssembly();
        var handles = new Mock<ISafeComponentHandleProvider>().Object;
        var a = new SampSharpEnvironment(asm, default, default, handles, "Production");
        var b = new SampSharpEnvironment(asm, default, default, handles, "Development");
        a.ShouldNotBe(b);
    }

    [Fact]
    public void Record_with_expression_replaces_field()
    {
        var asm = Assembly.GetExecutingAssembly();
        var handles = new Mock<ISafeComponentHandleProvider>().Object;
        var original = new SampSharpEnvironment(asm, default, default, handles, "Production");
        var modified = original with { EnvironmentName = "Staging" };
        modified.EnvironmentName.ShouldBe("Staging");
        original.EnvironmentName.ShouldBe("Production");
    }
}
