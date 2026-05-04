using SampSharp.Entities.SAMP.Commands.Attributes;
using SampSharp.Entities.SAMP.Commands.Help;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Dispatches chat input from a player to a method marked with <see cref="PlayerCommandAttribute" />.
/// </summary>
public interface IPlayerCommandService
{
    ICommandEnumerator GetCommands();

    /// <summary>Invokes a player command from the given chat input.</summary>
    /// <param name="services">Service provider used to resolve handler systems and DI parameters.</param>
    /// <param name="player">The invoking player.</param>
    /// <param name="inputText">Raw chat input including the leading slash.</param>
    /// <returns><see langword="true" /> if a handler claimed the input; otherwise <see langword="false" />.</returns>
    bool Invoke(IServiceProvider services, EntityId player, string inputText);
}
