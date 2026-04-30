namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IComponent" /> interface.
/// </summary>
[OpenMpApi]
[OpenMpApiPartial]
public readonly partial struct IComponent
{
    /// <summary>
    /// Gets the type of the component.
    /// </summary>
    /// <returns>The component type.</returns>
    public partial ComponentType GetComponentType();

    /// <summary>
    /// Gets the supported version of the component.
    /// </summary>
    /// <remarks>
    ///	The idea is for the SDK to be totally forward compatible, so code built at any time will always work, thanks to
    /// ABI compatibility.  This method is an emergency trap door, just in case that's ever not the problem.  Check
    /// which major version this component was built for, if it isn't the current major version, fail to load it.
    /// Always just returns a constant, recompiling will often be enough to upgrade. `virtual` and `final` to be the
    /// vtable, but it can't be overridden because it is a constant.
    /// </remarks>
    /// <returns>The supported version of the component.</returns>
    public partial int SupportedVersion();

    /// <summary>
    /// Gets the name of the component.
    /// </summary>
    /// <returns>The component name.</returns>
    public partial string ComponentName();

    /// <summary>
    /// Gets the version of the component.
    /// </summary>
    /// <returns>The component version.</returns>
    public partial SemanticVersion ComponentVersion();

    public partial interface IManagedInterface
    {
        /// <summary>
        /// Gets the identifier of the component type.
        /// </summary>
        static abstract UID ComponentId { get; }

        /// <summary>
        /// Casts a handle from a IComponent handle to a handle of this type.
        /// </summary>
        /// <param name="handle">The IComponent handle.</param>
        /// <returns>The handle of this type.</returns>
        static abstract nint FromComponentHandle(nint handle);
    }
}