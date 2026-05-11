using System;
using System.Reflection;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Core;

/// <summary>
/// Tests for CommandExecutor, which invokes a matched command with parsed arguments.
/// </summary>
public class CommandExecutorTests
{
    // Methods with different parameter counts to match prefixParamCount + parsedParams scenarios
    private void DummyOneParam(object p1) { }
    private void DummyTwoParams(object p1, object p2) { }
    private void DummyThreeParams(object p1, object p2, object p3) { }
    private void DummyNoParams() { }

    private CommandDefinition CreateDefinition(
        CommandInvoker invoker,
        string methodName,
        int prefixParamCount,
        CommandParameterInfo[]? parsedParams = null)
    {
        var method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic)!;
        return new CommandDefinition(
            "test", null, method, method.GetParameters(),
            typeof(CommandExecutorTests),
            parsedParams ?? Array.Empty<CommandParameterInfo>(),
            invoker, prefixParamCount,
            Array.Empty<CommandAlias>(), Array.Empty<CommandTag>());
    }

    [Fact]
    public void Execute_InvokesCompiledInvoker()
    {
        var invoked = false;
        CommandInvoker invoker = (target, args, svc, em) => { invoked = true; return true; };

        var entityManager = new Mock<IEntityManager>().Object;
        var executor = new CommandExecutor(entityManager);
        var definition = CreateDefinition(invoker, nameof(DummyOneParam), prefixParamCount: 1);
        var system = new Mock<ISystem>().Object;
        var services = new Mock<IServiceProvider>().Object;

        executor.Execute(definition, [null], [], services, system);

        invoked.ShouldBeTrue();
    }

    [Fact]
    public void Execute_ReturnsInvokerReturnValue_True()
    {
        CommandInvoker invoker = (target, args, svc, em) => true;

        var entityManager = new Mock<IEntityManager>().Object;
        var executor = new CommandExecutor(entityManager);
        var definition = CreateDefinition(invoker, nameof(DummyOneParam), prefixParamCount: 1);
        var system = new Mock<ISystem>().Object;
        var services = new Mock<IServiceProvider>().Object;

        var result = executor.Execute(definition, [null], [], services, system);

        result.ShouldBeTrue();
    }

    [Fact]
    public void Execute_ReturnsInvokerReturnValue_False()
    {
        CommandInvoker invoker = (target, args, svc, em) => false;

        var entityManager = new Mock<IEntityManager>().Object;
        var executor = new CommandExecutor(entityManager);
        var definition = CreateDefinition(invoker, nameof(DummyOneParam), prefixParamCount: 1);
        var system = new Mock<ISystem>().Object;
        var services = new Mock<IServiceProvider>().Object;

        var result = executor.Execute(definition, [null], [], services, system);

        result.ShouldBeFalse();
    }

    [Fact]
    public void Execute_PassesParsedArgsToInvoker()
    {
        object?[]? capturedArgs = null;
        CommandInvoker invoker = (target, args, svc, em) => { capturedArgs = args; return true; };

        var entityManager = new Mock<IEntityManager>().Object;
        var executor = new CommandExecutor(entityManager);
        // Method has 3 params: 1 prefix + 2 parsed
        var definition = CreateDefinition(invoker, nameof(DummyThreeParams), prefixParamCount: 1);
        var system = new Mock<ISystem>().Object;
        var services = new Mock<IServiceProvider>().Object;

        executor.Execute(definition, [null], ["hello", 42], services, system);

        capturedArgs.ShouldNotBeNull();
        capturedArgs![0].ShouldBeNull(); // prefix
        capturedArgs[1].ShouldBe("hello");
        capturedArgs[2].ShouldBe(42);
    }

    [Fact]
    public void Execute_PassesSystemAsTarget()
    {
        object? capturedTarget = null;
        CommandInvoker invoker = (target, args, svc, em) => { capturedTarget = target; return true; };

        var entityManager = new Mock<IEntityManager>().Object;
        var executor = new CommandExecutor(entityManager);
        var definition = CreateDefinition(invoker, nameof(DummyOneParam), prefixParamCount: 1);
        var systemMock = new Mock<ISystem>();
        var services = new Mock<IServiceProvider>().Object;

        executor.Execute(definition, [null], [], services, systemMock.Object);

        capturedTarget.ShouldBeSameAs(systemMock.Object);
    }

    [Fact]
    public void Execute_PassesEntityManagerToInvoker()
    {
        IEntityManager? capturedEntityManager = null;
        CommandInvoker invoker = (target, args, svc, em) => { capturedEntityManager = em; return true; };

        var entityManagerMock = new Mock<IEntityManager>();
        var executor = new CommandExecutor(entityManagerMock.Object);
        var definition = CreateDefinition(invoker, nameof(DummyOneParam), prefixParamCount: 1);
        var system = new Mock<ISystem>().Object;
        var services = new Mock<IServiceProvider>().Object;

        executor.Execute(definition, [null], [], services, system);

        capturedEntityManager.ShouldBeSameAs(entityManagerMock.Object);
    }

    [Fact]
    public void Execute_PassesServiceProviderToInvoker()
    {
        IServiceProvider? capturedServices = null;
        CommandInvoker invoker = (target, args, svc, em) => { capturedServices = svc; return true; };

        var entityManager = new Mock<IEntityManager>().Object;
        var executor = new CommandExecutor(entityManager);
        var definition = CreateDefinition(invoker, nameof(DummyOneParam), prefixParamCount: 1);
        var system = new Mock<ISystem>().Object;
        var servicesMock = new Mock<IServiceProvider>();

        executor.Execute(definition, [null], [], servicesMock.Object, system);

        capturedServices.ShouldBeSameAs(servicesMock.Object);
    }

    [Fact]
    public void Execute_NoPrefixParameters_ParsedArgGoesFirst()
    {
        object?[]? capturedArgs = null;
        CommandInvoker invoker = (target, args, svc, em) => { capturedArgs = args; return true; };

        var entityManager = new Mock<IEntityManager>().Object;
        var executor = new CommandExecutor(entityManager);
        // Method has 1 param: 0 prefix + 1 parsed
        var definition = CreateDefinition(invoker, nameof(DummyOneParam), prefixParamCount: 0);
        var system = new Mock<ISystem>().Object;
        var services = new Mock<IServiceProvider>().Object;

        executor.Execute(definition, [], ["arg1"], services, system);

        capturedArgs.ShouldNotBeNull();
        capturedArgs![0].ShouldBe("arg1");
    }
}
