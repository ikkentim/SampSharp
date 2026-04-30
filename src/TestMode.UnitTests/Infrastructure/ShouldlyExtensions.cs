using System.Numerics;
using Shouldly;

namespace TestMode.UnitTests;

public static class ShouldlyExtensions
{
    public static void ShouldBe(this Vector3 actual, Vector3 expected, float tolerance = 0.0002f)
    {
        actual.X.ShouldBe(expected.X, tolerance: tolerance, customMessage: $"should be (X) {expected} but was {actual}");
        actual.Y.ShouldBe(expected.Y, tolerance: tolerance, customMessage: $"should be (Y) {expected} but was {actual}");
        actual.Z.ShouldBe(expected.Z, tolerance: tolerance, customMessage: $"should be (Z) {expected} but was {actual}");
    }
}
