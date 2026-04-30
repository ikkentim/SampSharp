namespace SampSharp.OpenMp.Core;

/// <summary>
/// Specifies the overload name to append to the open.mp API function name. For example, if overload is "_index" for
/// IConfig.RemoveBan, the C function will be "IConfig_removeBan_index".
/// </summary>
/// <param name="overload">The overload name to append to the function name.</param>
[AttributeUsage(AttributeTargets.Method)]
public class OpenMpApiOverloadAttribute(string overload) : Attribute
{
    /// <summary>
    /// Gets the overload name to append to the function name.
    /// </summary>
    public string Overload { get; } = overload;
}