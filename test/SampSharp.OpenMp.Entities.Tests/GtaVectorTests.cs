using System.Numerics;
using SampSharp.Entities;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Entities.Tests;

public class GtaVectorTests
{
    [Fact]
    public void Up_IsCorrect()
    {
        GtaVector.Up.ShouldBe(new Vector3(0, 0, 1));
    }

    [Fact]
    public void Down_IsCorrect()
    {
        GtaVector.Down.ShouldBe(new Vector3(0, 0, -1));
    }

    [Fact]
    public void Left_IsCorrect()
    {
        GtaVector.Left.ShouldBe(new Vector3(-1, 0, 0));
    }

    [Fact]
    public void Right_IsCorrect()
    {
        GtaVector.Right.ShouldBe(new Vector3(1, 0, 0));
    }

    [Fact]
    public void Forward_IsCorrect()
    {
        GtaVector.Forward.ShouldBe(new Vector3(0, 1, 0));
    }

    [Fact]
    public void Backward_IsCorrect()
    {
        GtaVector.Backward.ShouldBe(new Vector3(0, -1, 0));
    }

    [Fact]
    public void UpAndDown_AreOpposite()
    {
        (GtaVector.Up + GtaVector.Down).ShouldBe(Vector3.Zero);
    }

    [Fact]
    public void LeftAndRight_AreOpposite()
    {
        (GtaVector.Left + GtaVector.Right).ShouldBe(Vector3.Zero);
    }

    [Fact]
    public void ForwardAndBackward_AreOpposite()
    {
        (GtaVector.Forward + GtaVector.Backward).ShouldBe(Vector3.Zero);
    }
}
