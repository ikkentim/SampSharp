using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace SampSharp.Entities.Logging;

internal sealed class OmpLoggerConfigureOptions : IConfigureOptions<OmpLoggerOptions>
{
    private readonly IConfiguration _configuration;

    public OmpLoggerConfigureOptions(ILoggerProviderConfiguration<OmpLoggerProvider> providerConfiguration)
    {
        _configuration = providerConfiguration.Configuration;
    }

    public void Configure(OmpLoggerOptions options) => _configuration.Bind(options);
}