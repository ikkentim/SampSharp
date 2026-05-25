using System.Reflection;
using Moq;
using SampSharp.Entities;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Entities.Tests;

public class TimerServiceExtensionsTests
{
    private sealed class TestTarget
    {
        public int CallCount;
        public void Tick() => CallCount++;
    }

    private static MethodInfo TickMethod => typeof(TestTarget).GetMethod(nameof(TestTarget.Tick))!;

    [Fact]
    public void Start_null_timerService_throws()
    {
        Should.Throw<ArgumentNullException>(() =>
            TimerServiceExtensions.Start(null!, new TestTarget(), TickMethod, TimeSpan.FromSeconds(1)));
    }

    [Fact]
    public void Start_null_target_throws()
    {
        var mock = new Mock<ITimerService>();
        Should.Throw<ArgumentNullException>(() =>
            mock.Object.Start(null!, TickMethod, TimeSpan.FromSeconds(1)));
    }

    [Fact]
    public void Start_null_method_throws()
    {
        var mock = new Mock<ITimerService>();
        Should.Throw<ArgumentNullException>(() =>
            mock.Object.Start(new TestTarget(), null!, TimeSpan.FromSeconds(1)));
    }

    [Fact]
    public void Start_invalid_interval_throws()
    {
        var mock = new Mock<ITimerService>();
        Should.Throw<ArgumentOutOfRangeException>(() =>
            mock.Object.Start(new TestTarget(), TickMethod, TimeSpan.Zero));
    }

    [Fact]
    public void Start_method_not_member_of_target_throws()
    {
        var mock = new Mock<ITimerService>();
        // Foreign target: TestTarget.Tick is not a member of object.
        Should.Throw<ArgumentException>(() =>
            mock.Object.Start(new object(), TickMethod, TimeSpan.FromMilliseconds(50)));
    }

    [Fact]
    public void Start_invokes_underlying_timerService_Start()
    {
        var mock = new Mock<ITimerService>();
        Action<IServiceProvider>? capturedAction = null;
        var interval = TimeSpan.FromMilliseconds(100);
        mock.Setup(s => s.Start(It.IsAny<Action<IServiceProvider>>(), interval))
            .Callback<Action<IServiceProvider>, TimeSpan>((a, _) => capturedAction = a)
            .Returns((TimerReference)null!);

        var target = new TestTarget();
        mock.Object.Start(target, TickMethod, interval);

        mock.Verify(s => s.Start(It.IsAny<Action<IServiceProvider>>(), interval), Times.Once);
        capturedAction.ShouldNotBeNull();

        // Invoke the captured action and verify it triggers Tick() on target.
        capturedAction!(new Mock<IServiceProvider>().Object);
        target.CallCount.ShouldBe(1);
    }
}
