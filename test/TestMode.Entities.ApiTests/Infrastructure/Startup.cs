using SampSharp.Entities;
using SampSharp.OpenMp.Core;
using Shouldly;

namespace TestMode.Entities.ApiTests;

public class Startup : IStartup
{
    public void Initialize(IStartupContext context)
    {
        ShouldlyConfiguration.DefaultFloatingPointTolerance = 0.0005f;
        context.UseEntities();
    }
}