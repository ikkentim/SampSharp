using System;
using System.Reflection;
using System.Threading.Tasks;
using SampSharp.Entities.SAMP.Commands.Core;
using Shouldly;
using Xunit;

namespace SampSharp.Entities.SAMP.Commands.Tests.Core;

public class CommandOverloadTests
{
    private readonly MethodInfo _testMethod;

    public CommandOverloadTests()
    {
        _testMethod = typeof(CommandOverloadTests).GetMethod(nameof(DummyCommand), BindingFlags.NonPublic | BindingFlags.Instance)!;
    }

    [Fact]
    public void Constructor_StoresMethodInfo()
    {
        var overload = new CommandOverload(_testMethod, _testMethod.GetParameters(), typeof(CommandOverloadTests), [], TestInvoker, 0);
        overload.Method.ShouldBe(_testMethod);
    }

    [Fact]
    public void IsAsync_VoidReturn_ReturnsFalse()
    {
        var overload = new CommandOverload(_testMethod, _testMethod.GetParameters(), typeof(CommandOverloadTests), [], TestInvoker, 0);
        overload.IsAsync.ShouldBeFalse();
    }

    [Fact]
    public void IsAsync_TaskReturn_ReturnsTrue()
    {
        var taskMethod = typeof(CommandOverloadTests).GetMethod(nameof(DummyAsyncCommand), BindingFlags.NonPublic | BindingFlags.Instance)!;
        var overload = new CommandOverload(taskMethod, taskMethod.GetParameters(), typeof(CommandOverloadTests), [], TestInvoker, 0);
        overload.IsAsync.ShouldBeTrue();
    }

    [Fact]
    public void IsAsync_TaskBoolReturn_ReturnsTrue()
    {
        var taskMethod = typeof(CommandOverloadTests).GetMethod(nameof(DummyAsyncBoolCommand), BindingFlags.NonPublic | BindingFlags.Instance)!;
        var overload = new CommandOverload(taskMethod, taskMethod.GetParameters(), typeof(CommandOverloadTests), [], TestInvoker, 0);
        overload.IsAsync.ShouldBeTrue();
    }

    private void DummyCommand() { }

    private static object TestInvoker(object target, object[] args, IServiceProvider services, IEntityManager entityManager)
    {
        throw new InvalidOperationException();
    }

    private Task DummyAsyncCommand()
    {
        return Task.CompletedTask;
    }

    private Task<bool> DummyAsyncBoolCommand() { return Task.FromResult(true); }
}