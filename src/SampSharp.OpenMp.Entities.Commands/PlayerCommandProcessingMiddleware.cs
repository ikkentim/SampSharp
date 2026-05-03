using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands.Core;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Middleware for processing player command text via <see cref="IPlayerCommandService" />.
/// </summary>
public class PlayerCommandProcessingMiddleware : ISystem
{
    private readonly IPlayerCommandService _playerCommandService;
    private readonly IServiceProvider _services;

    /// <summary>Initializes a new instance.</summary>
    public PlayerCommandProcessingMiddleware(IPlayerCommandService playerCommandService, IServiceProvider services)
    {
        _playerCommandService = playerCommandService ?? throw new ArgumentNullException(nameof(playerCommandService));
        _services = services ?? throw new ArgumentNullException(nameof(services));
    }

    /// <summary>Handles OnPlayerCommandText events.</summary>
    [Event]
    public void OnPlayerCommandText(SampSharp.Entities.SAMP.Player player, string text)
    {
        // Try to process the command
        // Returns true if a handler claimed it, false otherwise
        _playerCommandService.Invoke(_services, player.Entity, text);
    }
}
