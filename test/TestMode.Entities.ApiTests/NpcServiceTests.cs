using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using Shouldly;
using Xunit;

namespace TestMode.Entities.ApiTests;

public class NpcServiceTests : TestBase
{
    private INpcService Sut => Services.GetRequiredService<INpcService>();

    // --- Path operations ---

    [Fact]
    public void CreatePath_should_return_non_negative_id()
    {
        var pathId = Sut.CreatePath();

        try
        {
            pathId.ShouldBeGreaterThanOrEqualTo(0);
        }
        finally
        {
            Sut.DestroyPath(pathId);
        }
    }

    [Fact]
    public void IsValidPath_should_be_true_for_created_path()
    {
        var pathId = Sut.CreatePath();

        try
        {
            Sut.IsValidPath(pathId).ShouldBeTrue();
        }
        finally
        {
            Sut.DestroyPath(pathId);
        }
    }

    [Fact]
    public void IsValidPath_should_be_false_for_unknown_id()
    {
        Sut.IsValidPath(999999).ShouldBeFalse();
    }

    [Fact]
    public void DestroyPath_should_invalidate_path()
    {
        var pathId = Sut.CreatePath();
        Sut.DestroyPath(pathId).ShouldBeTrue();
        Sut.IsValidPath(pathId).ShouldBeFalse();
    }

    [Fact]
    public void GetPathCount_should_increase_after_create()
    {
        var before = (int)Sut.GetPathCount();
        var pathId = Sut.CreatePath();

        try
        {
            ((int)Sut.GetPathCount()).ShouldBe(before + 1);
        }
        finally
        {
            Sut.DestroyPath(pathId);
        }
    }

    [Fact]
    public void AddPointToPath_should_succeed()
    {
        var pathId = Sut.CreatePath();

        try
        {
            Sut.AddPointToPath(pathId, new Vector3(10, 20, 30), 2.0f).ShouldBeTrue();
        }
        finally
        {
            Sut.DestroyPath(pathId);
        }
    }

    [Fact]
    public void GetPathPointCount_should_reflect_added_points()
    {
        var pathId = Sut.CreatePath();

        try
        {
            Sut.AddPointToPath(pathId, new Vector3(1, 2, 3), 1.0f);
            Sut.AddPointToPath(pathId, new Vector3(4, 5, 6), 1.0f);

            ((int)Sut.GetPathPointCount(pathId)).ShouldBe(2);
        }
        finally
        {
            Sut.DestroyPath(pathId);
        }
    }

    [Fact]
    public void GetPathPoint_should_return_correct_data()
    {
        var pathId = Sut.CreatePath();

        try
        {
            var pos = new Vector3(10, 20, 30);
            Sut.AddPointToPath(pathId, pos, 2.5f);

            Sut.GetPathPoint(pathId, 0, out var outPos, out var outRange).ShouldBeTrue();
            outPos.ShouldBe(pos);
            outRange.ShouldBe(2.5f);
        }
        finally
        {
            Sut.DestroyPath(pathId);
        }
    }

    [Fact]
    public void HasPathPointInRange_should_return_true_when_in_range()
    {
        var pathId = Sut.CreatePath();

        try
        {
            Sut.AddPointToPath(pathId, new Vector3(10, 20, 30), 1.0f);
            Sut.HasPathPointInRange(pathId, new Vector3(10, 20, 30), 5.0f).ShouldBeTrue();
        }
        finally
        {
            Sut.DestroyPath(pathId);
        }
    }

    [Fact]
    public void HasPathPointInRange_should_return_false_when_out_of_range()
    {
        var pathId = Sut.CreatePath();

        try
        {
            Sut.AddPointToPath(pathId, new Vector3(0, 0, 0), 1.0f);
            Sut.HasPathPointInRange(pathId, new Vector3(9999, 9999, 9999), 1.0f).ShouldBeFalse();
        }
        finally
        {
            Sut.DestroyPath(pathId);
        }
    }

    [Fact]
    public void RemovePointFromPath_should_decrease_count()
    {
        var pathId = Sut.CreatePath();

        try
        {
            Sut.AddPointToPath(pathId, new Vector3(1, 2, 3), 1.0f);
            Sut.AddPointToPath(pathId, new Vector3(4, 5, 6), 1.0f);

            Sut.RemovePointFromPath(pathId, 0).ShouldBeTrue();

            ((int)Sut.GetPathPointCount(pathId)).ShouldBe(1);
        }
        finally
        {
            Sut.DestroyPath(pathId);
        }
    }

    [Fact]
    public void ClearPath_should_remove_all_points()
    {
        var pathId = Sut.CreatePath();

        try
        {
            Sut.AddPointToPath(pathId, new Vector3(1, 2, 3), 1.0f);
            Sut.AddPointToPath(pathId, new Vector3(4, 5, 6), 1.0f);

            Sut.ClearPath(pathId).ShouldBeTrue();

            ((int)Sut.GetPathPointCount(pathId)).ShouldBe(0);
        }
        finally
        {
            Sut.DestroyPath(pathId);
        }
    }

    [Fact]
    public void DestroyAllPaths_should_succeed()
    {
        Sut.CreatePath();
        Sut.CreatePath();

        Sut.DestroyAllPaths();

        ((int)Sut.GetPathCount()).ShouldBe(0);
    }

    // --- Record operations ---

    [Fact]
    public void LoadRecord_with_nonexistent_file_should_return_negative()
    {
        var recordId = Sut.LoadRecord("nonexistent_recording_file.rec");
        recordId.ShouldBeLessThan(0);
    }

    [Fact]
    public void IsValidRecord_should_be_false_for_invalid_id()
    {
        Sut.IsValidRecord(-1).ShouldBeFalse();
    }

    [Fact]
    public void GetRecordCount_should_succeed()
    {
        _ = Sut.GetRecordCount();
    }

    [Fact]
    public void UnloadAllRecords_should_succeed()
    {
        Sut.UnloadAllRecords();
    }

    [Fact]
    public void UnloadRecord_with_invalid_id_should_return_false()
    {
        Sut.UnloadRecord(-1).ShouldBeFalse();
    }
}
