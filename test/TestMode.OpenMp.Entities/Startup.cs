using Microsoft.Extensions.Configuration;
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
            .UseCommands();
    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TestSampSharpOptions>(configuration.GetSection("sampsharp"));

    }

    public void Configure(IEcsBuilder builder)
    {
        builder.Services.GetRequiredService<ILogger<Startup>>()
            .LogDebug("This is a debug log message!");
    }
}