using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.OpenMp.Core;

namespace Company.GameMode;

public class Startup : IEcsStartup
{
    public void Initialize(IStartupContext context)
    {
        context.UseEntities().UseCommands();
    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    public void Configure(IEcsBuilder builder)
    {
    }
}
