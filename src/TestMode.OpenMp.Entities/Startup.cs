using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampSharp.Entities;
using SampSharp.Entities.SAMP.Commands;
using SampSharp.OpenMp.Core;

namespace TestMode.OpenMp.Entities;

public class Startup : IEcsStartup
{
    public void Initialize(IStartupContext context)
    {
        context.UseEntities()
            .UsePlayerCommands()
            .ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Information));
    }

    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void Configure(IEcsBuilder builder)
    {
    }
}