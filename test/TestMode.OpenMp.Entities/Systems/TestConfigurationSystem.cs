using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SampSharp.Entities;

namespace TestMode.OpenMp.Entities.Systems;

public class TestConfigurationSystem : ISystem
{
    [Event]
    public void OnGameModeInit(IConfiguration configuration, IOptions<TestSampSharpOptions> options)
    {
        var art = configuration["artwork:enable"];
        Console.WriteLine($"artwork enabled: {art}");

        Console.WriteLine($"Directory: {options.Value.Assembly}");
    }

}