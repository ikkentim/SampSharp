namespace SampSharp.Entities;

/// <summary>
/// Invoker for an instance method with dependency injection.
/// </summary>
/// <param name="target">The target instance to invoke the method on.</param>
/// <param name="args">The arguments of the method excluding the injected dependencies.</param>
/// <param name="services">The service provider from which dependencies are loaded.</param>
/// <param name="entityManager">The entity manager from which components are loaded.</param>
/// <returns>The result of the method.</returns>
public delegate object? MethodInvoker(object target, object?[]? args, IServiceProvider services, IEntityManager? entityManager);