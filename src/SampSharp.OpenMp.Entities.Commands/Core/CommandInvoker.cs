namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Delegate type for the compiled method invoker of a command overload.
/// </summary>
/// <param name="target">The target system instance.</param>
/// <param name="args">The arguments for the command.</param>
/// <param name="services">The service provider for dependency injection.</param>
/// <param name="entityManager">The entity manager, if applicable.</param>
/// <returns><see langword="true" /> if the command executed successfully; otherwise, <see langword="false" />.</returns>
public delegate bool CommandInvoker(object target, object?[]? args, IServiceProvider services, IEntityManager? entityManager);