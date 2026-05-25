using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using SampSharp.Entities;
using Shouldly;
using Xunit;
using OmpLogLevel = SampSharp.OpenMp.Core.Api.LogLevel;

namespace SampSharp.OpenMp.Entities.Tests;

public class OmpLoggerTests
{
    private static OmpLogger CreateLogger(OmpLoggerOptions? options = null)
    {
        var pool = new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy());
        return new OmpLogger(default, options ?? new OmpLoggerOptions(), "test", pool);
    }

    [Fact]
    public void IsEnabled_returns_false_for_None()
    {
        CreateLogger().IsEnabled(LogLevel.None).ShouldBeFalse();
    }

    [Theory]
    [InlineData(LogLevel.Trace)]
    [InlineData(LogLevel.Debug)]
    [InlineData(LogLevel.Information)]
    [InlineData(LogLevel.Warning)]
    [InlineData(LogLevel.Error)]
    [InlineData(LogLevel.Critical)]
    public void IsEnabled_returns_true_for_all_non_None_levels(LogLevel level)
    {
        CreateLogger().IsEnabled(level).ShouldBeTrue();
    }

    [Fact]
    public void BeginScope_returns_null()
    {
        CreateLogger().BeginScope(new { test = 1 }).ShouldBeNull();
    }

    [Fact]
    public void Options_property_is_settable()
    {
        var logger = CreateLogger();
        var newOptions = new OmpLoggerOptions { TraceLevel = OmpLogLevel.Debug };
        logger.Options = newOptions;
        logger.Options.ShouldBe(newOptions);
    }

    [Fact]
    public void Log_short_circuits_on_None_level()
    {
        // Verifies Log doesn't reach the native writer call when level is None.
        // If it did, the default ILogger struct (zeroed function pointers) would crash.
        var logger = CreateLogger();
        Should.NotThrow(() => logger.Log(LogLevel.None, new EventId(0), state: "msg", exception: null,
            formatter: (_, _) => "msg"));
    }
}
