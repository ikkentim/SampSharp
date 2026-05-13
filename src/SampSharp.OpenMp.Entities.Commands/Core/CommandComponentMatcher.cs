namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Delegate type for validating whether command arguments can be resolved to the required component types.
/// </summary>
/// <param name="prefixArgs">The prefix arguments for the command.</param>
/// <param name="parsedArgs">The parsed arguments for the command.</param>
/// <param name="entityManager">The entity manager used for entity-to-component resolution.</param>
/// <returns><see langword="true" /> if the arguments satisfy all component requirements; otherwise, <see langword="false" />.</returns>
public delegate bool CommandComponentMatcher(object?[] prefixArgs, object?[] parsedArgs, IEntityManager entityManager);
