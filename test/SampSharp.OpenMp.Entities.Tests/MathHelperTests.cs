using System.Numerics;
using SampSharp.Entities;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Entities.Tests;

public class MathHelperTests
{
    private const float Tolerance = 0.0001f;

    [Fact]
    public void Barycentric_AtVertex1_ReturnsValue1()
    {
        var result = MathHelper.Barycentric(1f, 2f, 3f, 0f, 0f);
        result.ShouldBe(1f, Tolerance);
    }

    [Fact]
    public void Barycentric_AtVertex2_ReturnsValue2()
    {
        var result = MathHelper.Barycentric(1f, 2f, 3f, 1f, 0f);
        result.ShouldBe(2f, Tolerance);
    }

    [Fact]
    public void Barycentric_AtVertex3_ReturnsValue3()
    {
        var result = MathHelper.Barycentric(1f, 2f, 3f, 0f, 1f);
        result.ShouldBe(3f, Tolerance);
    }

    [Fact]
    public void CatmullRom_AtZero_ReturnsValue2()
    {
        var result = MathHelper.CatmullRom(0f, 1f, 2f, 3f, 0f);
        result.ShouldBe(1f, Tolerance);
    }

    [Fact]
    public void CatmullRom_AtOne_ReturnsValue3()
    {
        var result = MathHelper.CatmullRom(0f, 1f, 2f, 3f, 1f);
        result.ShouldBe(2f, Tolerance);
    }

    [Fact]
    public void Distance_SameValues_ReturnsZero()
    {
        MathHelper.Distance(5f, 5f).ShouldBe(0f);
    }

    [Fact]
    public void Distance_DifferentValues_ReturnsAbsoluteDifference()
    {
        MathHelper.Distance(3f, 7f).ShouldBe(4f, Tolerance);
    }

    [Fact]
    public void Distance_NegativeOrder_ReturnsPositive()
    {
        MathHelper.Distance(7f, 3f).ShouldBe(4f, Tolerance);
    }

    [Fact]
    public void Hermite_AtZero_ReturnsValue1()
    {
        var result = MathHelper.Hermite(1f, 0f, 2f, 0f, 0f);
        result.ShouldBe(1f, Tolerance);
    }

    [Fact]
    public void Hermite_AtOne_ReturnsValue2()
    {
        var result = MathHelper.Hermite(1f, 0f, 2f, 0f, 1f);
        result.ShouldBe(2f, Tolerance);
    }

    [Theory]
    [InlineData(0f, 0f)]
    [InlineData(1f, 1f)]
    [InlineData(0.5f, 0.5f)]
    public void SmoothStep_Midpoint_ReturnsExpected(float amount, float expected)
    {
        // With value1=0 and value2=1, result should interpolate
        var result = MathHelper.SmoothStep(0f, 1f, amount);
        result.ShouldBeInRange(0f, 1f);
        if (amount is 0f or 1f)
        {
            result.ShouldBe(expected, Tolerance);
        }
    }

    [Fact]
    public void SmoothStep_BelowZero_ClampsToValue1()
    {
        var result = MathHelper.SmoothStep(1f, 2f, -1f);
        result.ShouldBe(1f, Tolerance);
    }

    [Fact]
    public void SmoothStep_AboveOne_ClampsToValue2()
    {
        var result = MathHelper.SmoothStep(1f, 2f, 2f);
        result.ShouldBe(2f, Tolerance);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(8)]
    [InlineData(16)]
    [InlineData(1024)]
    public void IsPowerOfTwo_PowersOfTwo_ReturnsTrue(int value)
    {
        MathHelper.IsPowerOfTwo(value).ShouldBeTrue();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(-1)]
    [InlineData(-2)]
    public void IsPowerOfTwo_NonPowersOfTwo_ReturnsFalse(int value)
    {
        MathHelper.IsPowerOfTwo(value).ShouldBeFalse();
    }

    [Fact]
    public void WrapAngle_Zero_ReturnsZero()
    {
        MathHelper.WrapAngle(0f).ShouldBe(0f, Tolerance);
    }

    [Fact]
    public void WrapAngle_PastPi_WrapsToNegative()
    {
        var result = MathHelper.WrapAngle(float.Pi + 0.1f);
        result.ShouldBeInRange(-float.Pi, float.Pi);
    }

    [Fact]
    public void WrapAngle_NegativePastMinusPi_WrapsToPositive()
    {
        var result = MathHelper.WrapAngle(-float.Pi - 0.1f);
        result.ShouldBeInRange(-float.Pi, float.Pi);
    }

    [Fact]
    public void WrapAngle_WithinRange_Unchanged()
    {
        const float angle = 1.0f;
        MathHelper.WrapAngle(angle).ShouldBe(angle, Tolerance);
    }

    [Fact]
    public void GetZAngleFromRotationMatrix_IdentityMatrix_ReturnsZero()
    {
        var result = MathHelper.GetZAngleFromRotationMatrix(Matrix4x4.Identity);
        result.ShouldBe(0f, Tolerance);
    }

    [Fact]
    public void PiOver2_IsCorrectValue()
    {
        MathHelper.PiOver2.ShouldBe((float)(Math.PI / 2.0), Tolerance);
    }

    [Fact]
    public void PiOver4_IsCorrectValue()
    {
        MathHelper.PiOver4.ShouldBe((float)(Math.PI / 4.0), Tolerance);
    }

    [Fact]
    public void TwoPi_IsCorrectValue()
    {
        MathHelper.TwoPi.ShouldBe((float)(Math.PI * 2.0), Tolerance);
    }
}
