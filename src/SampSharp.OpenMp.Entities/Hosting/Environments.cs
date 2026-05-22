namespace SampSharp.Entities;

/// <summary>
/// Defines standard environment names for SampSharp applications. These can be used to differentiate between different deployment environments (e.g., Development, Staging, Production) and enable environment-specific configurations and behaviors.
/// </summary>
public static class Environments
{
    /// <summary>
    /// The production environment. This is the default if no environment is specified.
    /// </summary>
    public const string Production = "Production";

    /// <summary>
    /// The development environment. This is used for development and testing purposes. It may enable additional logging and diagnostics.
    /// </summary>
    public const string Development = "Development";

    /// <summary>
    /// The staging environment. This is used for pre-production testing and validation.
    /// </summary>
    public const string Staging = "Staging";
}