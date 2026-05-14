using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities;
using SampSharp.Entities.SAMP.Commands;
using SampSharp.OpenMp.Core;

namespace Company.GameMode;

public class Startup : IEcsStartup
{
    public void Initialize(IStartupContext context)
    {
        context.UseEntities().UseCommands();
    }

    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void Configure(IEcsBuilder builder)
    {
    }
}
