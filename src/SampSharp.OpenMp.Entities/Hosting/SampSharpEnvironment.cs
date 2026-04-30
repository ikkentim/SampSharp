using System.Reflection;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities;

/// <summary>
/// Represents the environment of a SampSharp application.
/// </summary>
/// <param name="EntryAssembly">The assembly which was configured to launch in open.mp.</param>
/// <param name="Core">The <see cref="ICore" /> of open.mp.</param>
/// <param name="Components">The <see cref="IComponentList" /> of open.mp.</param>
public record SampSharpEnvironment(Assembly EntryAssembly, ICore Core, IComponentList Components);