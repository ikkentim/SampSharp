using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class FixesService(SampSharpEnvironment environment) : IFixesService
{
    private readonly IFixesComponent _fixes = environment.Components.QueryComponent<IFixesComponent>();

    public bool SendGameTextToAll(string message, TimeSpan time, int style)
    {
        ArgumentNullException.ThrowIfNull(message);
        return _fixes.SendGameTextToAll(message, time, style);
    }

    public bool HideGameTextForAll(int style)
    {
        return _fixes.HideGameTextForAll(style);
    }

    public void ClearAnimation(Player? player, Actor? actor)
    {
        _fixes.ClearAnimation(player ?? default(IPlayer), actor ?? default(IActor));
    }
}
