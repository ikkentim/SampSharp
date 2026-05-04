using SampSharp.OpenMp.Core;
using Shouldly;
using Xunit;

namespace TestMode.UnitTests;

public class MetaTest
{
    [Fact]
    public void Test_should_run_on_main_thread()
    {
        var result = TaskHelper.IsMainThread();

        result.ShouldBeTrue();
    }
}