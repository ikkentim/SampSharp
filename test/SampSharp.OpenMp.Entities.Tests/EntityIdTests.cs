using SampSharp.Entities;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Entities.Tests;

public class EntityIdTests
{
    [Fact]
    public void Empty_IsEmpty_ReturnsTrue()
    {
        EntityId.Empty.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void NewEntityId_IsEmpty_ReturnsFalse()
    {
        EntityId.NewEntityId().IsEmpty.ShouldBeFalse();
    }

    [Fact]
    public void NewEntityId_TwoCallsReturnDistinctIds()
    {
        var a = EntityId.NewEntityId();
        var b = EntityId.NewEntityId();
        a.ShouldNotBe(b);
    }

    [Fact]
    public void ToString_Empty_ReturnsEmptyString()
    {
        EntityId.Empty.ToString().ShouldBe("(Empty)");
    }

    [Fact]
    public void ToString_NonEmpty_ContainsId()
    {
        var id = EntityId.NewEntityId();
        id.ToString().ShouldStartWith("(Id = ");
    }

    [Fact]
    public void ImplicitConversionToBool_Empty_ReturnsFalse()
    {
        bool result = EntityId.Empty;
        result.ShouldBeFalse();
    }

    [Fact]
    public void ImplicitConversionToBool_NonEmpty_ReturnsTrue()
    {
        bool result = EntityId.NewEntityId();
        result.ShouldBeTrue();
    }

    [Fact]
    public void OperatorTrue_Empty_ReturnsFalse()
    {
        var id = EntityId.Empty;
        (id ? true : false).ShouldBeFalse();
    }

    [Fact]
    public void OperatorTrue_NonEmpty_ReturnsTrue()
    {
        var id = EntityId.NewEntityId();
        (id ? true : false).ShouldBeTrue();
    }

    [Fact]
    public void OperatorFalse_Empty_ReturnsTrue()
    {
        var id = EntityId.Empty;
        // operator false returns true when id is empty (used in if(!id) and while(!id))
        if (!id)
        {
            true.ShouldBeTrue();
        }
        else
        {
            false.ShouldBeTrue("Expected operator false to return true for empty EntityId");
        }
    }

    [Fact]
    public void OperatorNot_Empty_ReturnsTrue()
    {
        var id = EntityId.Empty;
        (!id).ShouldBeTrue();
    }

    [Fact]
    public void OperatorNot_NonEmpty_ReturnsFalse()
    {
        var id = EntityId.NewEntityId();
        (!id).ShouldBeFalse();
    }

    [Fact]
    public void Equality_TwoEmptyIds_AreEqual()
    {
        EntityId.Empty.ShouldBe(EntityId.Empty);
    }

    [Fact]
    public void Equality_SameId_IsEqual()
    {
        var id = EntityId.NewEntityId();
        id.ShouldBe(id);
    }

    [Fact]
    public void Equality_TwoDifferentIds_AreNotEqual()
    {
        var a = EntityId.NewEntityId();
        var b = EntityId.NewEntityId();
        a.ShouldNotBe(b);
    }
}
