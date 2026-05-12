using SampSharp.OpenMp.Core.Api;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Core.Tests;

public class UidTests
{
    [Fact]
    public void ToString_ReturnsHex16Digits()
    {
        var uid = new UID(0x1234567890abcdef);
        uid.ToString().ShouldBe("1234567890abcdef");
    }

    [Fact]
    public void ToString_ZeroValue_ReturnsAllZeroes()
    {
        var uid = new UID(0);
        uid.ToString().ShouldBe("0000000000000000");
    }

    [Fact]
    public void ToString_MaxValue_ReturnsAllFs()
    {
        var uid = new UID(ulong.MaxValue);
        uid.ToString().ShouldBe("ffffffffffffffff");
    }
}
