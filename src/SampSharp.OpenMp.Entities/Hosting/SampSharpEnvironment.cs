using System.Reflection;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities;

/// <summary>
/// Represents the environment of a SampSharp application, providing access to the entry assembly, game server core, and registered components.
/// </summary>
/// <remarks>
/// <see cref="SampSharpEnvironment" /> is initialized during the startup process and provides central access to critical application resources.
/// It is typically used to retrieve services, query components, and invoke core functionality.
/// </remarks>
/// <param name="EntryAssembly">The assembly which was configured to launch in open.mp. Used to discover game mode classes and other application types.</param>
/// <param name="Core">The <see cref="ICore" /> interface for the open.mp server. Provides access to core server functionality and extensions.</param>
/// <param name="Components">The <see cref="IComponentList" /> of open.mp. Manages all game components (players, vehicles, objects, etc.) accessible on the server.</param>
public record SampSharpEnvironment(Assembly EntryAssembly, ICore Core, IComponentList Components);