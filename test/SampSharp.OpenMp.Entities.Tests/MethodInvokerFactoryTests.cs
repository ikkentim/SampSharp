using System.Reflection;
using Moq;
using SampSharp.Entities;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Entities.Tests;

public class MethodInvokerFactoryTests
{
    private class SimpleService
    {
        public int LastValue { get; private set; }
        public bool WasCalled { get; private set; }

        public void VoidMethod()
        {
            WasCalled = true;
        }

        public bool BoolMethod() => true;

        public void IntMethod(int value)
        {
            LastValue = value;
        }

        public void ServiceMethod(FakeService svc)
        {
            WasCalled = true;
        }
    }

    private class FakeService { }

    private static MethodParameterSource[] NoParams() => [];

    private static MethodParameterSource[] PassThroughParams(MethodInfo method)
    {
        return method.GetParameters()
            .Select((p, i) => new MethodParameterSource(p) { ParameterIndex = i })
            .ToArray();
    }

    private static MethodParameterSource[] ServiceParams(MethodInfo method)
    {
        return method.GetParameters()
            .Select(p => new MethodParameterSource(p) { IsService = true })
            .ToArray();
    }

    private static IEntityManager CreateEntityManager() => new EntityManager();

    private static IServiceProvider EmptyServiceProvider()
    {
        return new Mock<IServiceProvider>().Object;
    }

    [Fact]
    public void Compile_VoidMethod_ExecutesAndReturnsNull()
    {
        var instance = new SimpleService();
        var method = typeof(SimpleService).GetMethod(nameof(SimpleService.VoidMethod))!;
        var invoker = MethodInvokerFactory.Compile(method, NoParams());

        var result = invoker(instance, [], EmptyServiceProvider(), CreateEntityManager());

        result.ShouldBeNull();
        instance.WasCalled.ShouldBeTrue();
    }

    [Fact]
    public void Compile_BoolMethod_ReturnsMethodResult()
    {
        var instance = new SimpleService();
        var method = typeof(SimpleService).GetMethod(nameof(SimpleService.BoolMethod))!;
        var invoker = MethodInvokerFactory.Compile(method, NoParams());

        var result = invoker(instance, [], EmptyServiceProvider(), CreateEntityManager());

        result.ShouldBeOfType<MethodResult>();
        ((MethodResult)result!).Value.ShouldBeTrue();
    }

    [Fact]
    public void Compile_IntPassThrough_PassesValueCorrectly()
    {
        var instance = new SimpleService();
        var method = typeof(SimpleService).GetMethod(nameof(SimpleService.IntMethod))!;
        var sources = PassThroughParams(method);
        var invoker = MethodInvokerFactory.Compile(method, sources);

        invoker(instance, [42], EmptyServiceProvider(), CreateEntityManager());

        instance.LastValue.ShouldBe(42);
    }

    [Fact]
    public void Compile_IntPassThrough_NumericCoercion_UintToInt()
    {
        var instance = new SimpleService();
        var method = typeof(SimpleService).GetMethod(nameof(SimpleService.IntMethod))!;
        var sources = PassThroughParams(method);
        var invoker = MethodInvokerFactory.Compile(method, sources);

        // Passing uint where int is expected - should coerce successfully
        invoker(instance, [(uint)99], EmptyServiceProvider(), CreateEntityManager());

        instance.LastValue.ShouldBe(99);
    }

    [Fact]
    public void Compile_ServiceInjected_CallsMethod()
    {
        var fakeService = new FakeService();
        var mockProvider = new Mock<IServiceProvider>();
        mockProvider.Setup(sp => sp.GetService(typeof(FakeService))).Returns(fakeService);

        var instance = new SimpleService();
        var method = typeof(SimpleService).GetMethod(nameof(SimpleService.ServiceMethod))!;
        var sources = ServiceParams(method);
        var invoker = MethodInvokerFactory.Compile(method, sources);

        invoker(instance, [], mockProvider.Object, CreateEntityManager());

        instance.WasCalled.ShouldBeTrue();
    }

    [Fact]
    public void Compile_NullMethodInfo_ThrowsArgumentNullException()
    {
        Should.Throw<ArgumentNullException>(() => MethodInvokerFactory.Compile(null!, []));
    }

    [Fact]
    public void Compile_NullParameterSources_ThrowsArgumentNullException()
    {
        var method = typeof(SimpleService).GetMethod(nameof(SimpleService.VoidMethod))!;
        Should.Throw<ArgumentNullException>(() => MethodInvokerFactory.Compile(method, null!));
    }
}
