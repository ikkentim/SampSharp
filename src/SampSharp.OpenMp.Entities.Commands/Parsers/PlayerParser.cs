using Microsoft.Extensions.DependencyInjection;

namespace SampSharp.Entities.SAMP.Commands.Parsers;

/// <summary>
/// Parses a <see cref="Player" /> reference. Accepts either:
/// <list type="bullet">
///     <item>integer playerid (e.g. <c>/kick 5</c>)</item>
///     <item>full player name (case-insensitive)</item>
///     <item>name prefix — picks the player with the lowest playerid among matches</item>
/// </list>
/// Returns the matched <see cref="EntityId" /> so the command pipeline can convert it to a <see cref="Player" /> component.
/// </summary>
public class PlayerParser : ICommandParameterParser
{
    private readonly WordParser _wordParser = new();

    /// <inheritdoc />
    public bool TryParse(IServiceProvider services, ref string inputText, out object? result)
    {
        if (!_wordParser.TryParse(services, ref inputText, out var sub) || sub is not string word)
        {
            result = null;
            return false;
        }

        var entityProvider = services.GetRequiredService<IOmpEntityProvider>();

        if (int.TryParse(word, out var intWord))
        {
            var byId = entityProvider.GetPlayer(intWord);
            if (byId is { IsComponentAlive: true })
            {
                result = byId.Entity;
                return true;
            }
        }

        var entityManager = services.GetRequiredService<IEntityManager>();
        var players = entityManager.GetComponents<Player>();

        Player? bestCandidate = null;
        foreach (var player in players)
        {
            if (!player.IsComponentAlive) continue;
            var name = player.Name;
            if (name.Equals(word, StringComparison.OrdinalIgnoreCase))
            {
                result = player.Entity;
                return true;
            }
            if (!name.StartsWith(word, StringComparison.OrdinalIgnoreCase)) continue;
            if (bestCandidate == null || player.Id < bestCandidate.Id)
                bestCandidate = player;
        }

        result = bestCandidate?.Entity;
        return bestCandidate != null;
    }
}
