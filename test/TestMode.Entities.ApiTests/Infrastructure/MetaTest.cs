using SampSharp.OpenMp.Core;
using Shouldly;
using Xunit;

namespace TestMode.Entities.ApiTests;

public class MetaTest
{
    [Fact]
    public void Test_should_run_on_main_thread()
    {
        var result = TaskHelper.IsMainThread();

        result.ShouldBeTrue();
    }
}