using SampSharp.Entities;
using Shouldly;
using Xunit;
using OmpLogLevel = SampSharp.OpenMp.Core.Api.LogLevel;

namespace SampSharp.OpenMp.Entities.Tests;

public class OmpLoggerOptionsTests
{
    [Fact]
    public void Defaults_should_match_documented_level_mapping()
    {
        var options = new OmpLoggerOptions();
        options.TraceLevel.ShouldBe(OmpLogLevel.Message);
        options.DebugLevel.ShouldBe(OmpLogLevel.Message);
        options.InformationLevel.ShouldBe(OmpLogLevel.Message);
        options.WarningLevel.ShouldBe(OmpLogLevel.Warning);
        options.ErrorLevel.ShouldBe(OmpLogLevel.Error);
        options.CriticalLevel.ShouldBe(OmpLogLevel.Error);
    }

    [Fact]
    public void Properties_should_be_settable()
    {
        var options = new OmpLoggerOptions
        {
            TraceLevel = OmpLogLevel.Debug,
            DebugLevel = OmpLogLevel.Debug,
            InformationLevel = OmpLogLevel.Message,
            WarningLevel = OmpLogLevel.Warning,
            ErrorLevel = OmpLogLevel.Error,
            CriticalLevel = OmpLogLevel.Error
        };
        options.TraceLevel.ShouldBe(OmpLogLevel.Debug);
        options.DebugLevel.ShouldBe(OmpLogLevel.Debug);
    }
}
