using SampSharp.OpenMp.Core;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Core.Tests;

public class BlittableBooleanTests
{
    [Fact]
    public void DefaultValue_IsFalse()
    {
        BlittableBoolean b = default;
        ((bool)b).ShouldBeFalse();
    }

    [Fact]
    public void ImplicitConversion_FromTrue_IsTrue()
    {
        BlittableBoolean b = true;
        ((bool)b).ShouldBeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromFalse_IsFalse()
    {
        BlittableBoolean b = false;
        ((bool)b).ShouldBeFalse();
    }

    [Fact]
    public void ImplicitConversion_ToBool_True()
    {
        BlittableBoolean b = new BlittableBoolean(true);
        bool result = b;
        result.ShouldBeTrue();
    }

    [Fact]
    public void ImplicitConversion_ToBool_False()
    {
        BlittableBoolean b = new BlittableBoolean(false);
        bool result = b;
        result.ShouldBeFalse();
    }

    [Fact]
    public void EqualityOperator_SameValues_AreEqual()
    {
        BlittableBoolean a = true;
        BlittableBoolean b = true;
        (a == b).ShouldBeTrue();
    }

    [Fact]
    public void EqualityOperator_DifferentValues_AreNotEqual()
    {
        BlittableBoolean a = true;
        BlittableBoolean b = false;
        (a == b).ShouldBeFalse();
    }

    [Fact]
    public void InequalityOperator_DifferentValues_AreNotEqual()
    {
        BlittableBoolean a = true;
        BlittableBoolean b = false;
        (a != b).ShouldBeTrue();
    }

    [Fact]
    public void InequalityOperator_SameValues_AreEqual()
    {
        BlittableBoolean a = false;
        BlittableBoolean b = false;
        (a != b).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WithSameBool_ReturnsTrue()
    {
        BlittableBoolean b = true;
        b.Equals(true).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WithDifferentBool_ReturnsFalse()
    {
        BlittableBoolean b = true;
        b.Equals(false).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WithSameBlittableBoolean_ReturnsTrue()
    {
        BlittableBoolean a = true;
        BlittableBoolean b = true;
        a.Equals(b).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WithDifferentBlittableBoolean_ReturnsFalse()
    {
        BlittableBoolean a = true;
        BlittableBoolean b = false;
        a.Equals(b).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WithObject_NullReturnsFalse()
    {
        BlittableBoolean b = true;
        b.Equals(null).ShouldBeFalse();
    }

    [Fact]
    public void GetHashCode_True_MatchesBoolHashCode()
    {
        BlittableBoolean b = true;
        b.GetHashCode().ShouldBe(true.GetHashCode());
    }

    [Fact]
    public void GetHashCode_False_MatchesBoolHashCode()
    {
        BlittableBoolean b = false;
        b.GetHashCode().ShouldBe(false.GetHashCode());
    }

    [Fact]
    public void ToString_True_ReturnsTrueString()
    {
        BlittableBoolean b = true;
        b.ToString().ShouldBe("True");
    }

    [Fact]
    public void ToString_False_ReturnsFalseString()
    {
        BlittableBoolean b = false;
        b.ToString().ShouldBe("False");
    }
}
