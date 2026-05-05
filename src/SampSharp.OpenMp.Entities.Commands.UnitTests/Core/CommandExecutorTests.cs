using System;
using System.Reflection;
using Moq;
using Shouldly;
using Xunit;

namespace SampSharp.Entities.SAMP.Commands.Tests.Core;

public class CommandExecutorTests
{
    private readonly CommandExecutor _executor;
    private readonly Mock<IEntityManager> _mockEntityManager;
    private readonly Mock<IServiceProvider> _mockServices;
    private readonly Mock<ISystem> _mockSystem;

    public CommandExecutorTests()
    {
        _mockEntityManager = new Mock<IEntityManager>();
        _mockServices = new Mock<IServiceProvider>();
        _mockSystem = new Mock<ISystem>();
        _executor = new CommandExecutor(_mockEntityManager.Object);
    }

    [Fact]
    public void Execute_CommandWithNoPrefixParameters_DoesNotIncludePrefixInArgs()
    {
        // Setup: Create a simple overload with NO prefix parameters
        var mockMethod = new Mock<MethodInfo>();
        mockMethod.Setup(m => m.Name).Returns("TestCommand");
        mockMethod.Setup(m => m.ReturnType).Returns(typeof(void));

        var parameters = new ParameterInfo[2];
        var paramInfo1 = new Mock<ParameterInfo>();
        paramInfo1.Setup(p => p.Name).Returns("a");
        paramInfo1.Setup(p => p.ParameterType).Returns(typeof(int));
        parameters[0] = paramInfo1.Object;

        var paramInfo2 = new Mock<ParameterInfo>();
        paramInfo2.Setup(p => p.Name).Returns("b");
        paramInfo2.Setup(p => p.ParameterType).Returns(typeof(int));
        parameters[1] = paramInfo2.Object;

        var parsedParams = new[]
        {
            new CommandParameterInfo("a", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 0),
            new CommandParameterInfo("b", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 1)
        };

        // Track the arguments passed to the invoker
        object?[]? invokerArgs = null;
        MethodInvoker invoker = (target, args, services, entityManager) =>
        {
            invokerArgs = args;
            return null;
        };

        // Create overload with PrefixParameterCount = 0 (NO prefix params expected)
        var overload = new CommandOverload(
            mockMethod.Object,
            parameters,
            typeof(ISystem),
            parsedParams,
            invoker: invoker,
            prefixParameterCount: 0);

        // Execute: Pass extra prefix args (should be ignored)
        var prefixArgs = new object?[] { "extra_prefix_1", "extra_prefix_2" };
        var parsedArgs = new object?[] { 3, 5 };

        _executor.Execute(
            overload,
            prefixArgs,
            parsedArgs,
            _mockServices.Object,
            _mockSystem.Object);

        // Verify: The invoker should receive ONLY the parsed args, not the prefix args
        invokerArgs.ShouldNotBeNull("Invoker should have been called");
        invokerArgs!.Length.ShouldBe(2, "Should have exactly 2 arguments (no prefix)");
        invokerArgs[0].ShouldBe(3, "First argument should be parsed arg");
        invokerArgs[1].ShouldBe(5, "Second argument should be parsed arg");
    }

    [Fact]
    public void Execute_CommandWithPrefixParameter_IncludesOnlyExpectedPrefix()
    {
        // Setup: Create an overload with ONE prefix parameter
        var mockMethod = new Mock<MethodInfo>();
        mockMethod.Setup(m => m.Name).Returns("TestCommand");
        mockMethod.Setup(m => m.ReturnType).Returns(typeof(void));

        var parameters = new ParameterInfo[3];
        var paramInfo1 = new Mock<ParameterInfo>();
        paramInfo1.Setup(p => p.Name).Returns("player");
        paramInfo1.Setup(p => p.ParameterType).Returns(typeof(object));
        parameters[0] = paramInfo1.Object;

        var paramInfo2 = new Mock<ParameterInfo>();
        paramInfo2.Setup(p => p.Name).Returns("a");
        paramInfo2.Setup(p => p.ParameterType).Returns(typeof(int));
        parameters[1] = paramInfo2.Object;

        var paramInfo3 = new Mock<ParameterInfo>();
        paramInfo3.Setup(p => p.Name).Returns("b");
        paramInfo3.Setup(p => p.ParameterType).Returns(typeof(int));
        parameters[2] = paramInfo3.Object;

        var parsedParams = new[]
        {
            new CommandParameterInfo("a", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 1),
            new CommandParameterInfo("b", new IntParser(), isRequired: true, defaultValue: null, parameterIndex: 2)
        };

        // Track the arguments passed to the invoker
        object?[]? invokerArgs = null;
        MethodInvoker invoker = (target, args, services, entityManager) =>
        {
            invokerArgs = args;
            return null;
        };

        // Create overload with PrefixParameterCount = 1 (ONE prefix param expected)
        var overload = new CommandOverload(
            mockMethod.Object,
            parameters,
            typeof(ISystem),
            parsedParams,
            invoker: invoker,
            prefixParameterCount: 1);

        // Execute: Pass one prefix arg and two parsed args
        var prefixArgs = new object?[] { "player_obj" };
        var parsedArgs = new object?[] { 3, 5 };

        _executor.Execute(
            overload,
            prefixArgs,
            parsedArgs,
            _mockServices.Object,
            _mockSystem.Object);

        // Verify: The invoker should receive prefix + parsed args
        invokerArgs.ShouldNotBeNull("Invoker should have been called");
        invokerArgs!.Length.ShouldBe(3, "Should have exactly 3 arguments (1 prefix + 2 parsed)");
        invokerArgs[0].ShouldBe("player_obj", "First argument should be prefix arg");
        invokerArgs[1].ShouldBe(3, "Second argument should be first parsed arg");
        invokerArgs[2].ShouldBe(5, "Third argument should be second parsed arg");
    }
}