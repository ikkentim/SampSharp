namespace SampSharp.Entities;

/// <summary>Provides extended functionality for configuring a <see cref="IEcsBuilder" /> instance.</summary>
public static class EcsBuilderEventScopeExtensions
{
    /// <summary>Enabled a Dependency Injection scope for the event with the specified <paramref name="name" />.</summary>
    /// <param name="builder">The ECS builder in which to enable the scope.</param>
    /// <param name="name">The name of the event to add the scope to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IEcsBuilder EnableEventScope(this IEcsBuilder builder, string name)
    {
        return builder.UseMiddleware<EventScopeMiddleware>(name);
    }
}