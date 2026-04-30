using System.Reflection;

namespace SampSharp.Entities;

/// <summary>Provides information about the origin of a parameter of a method.</summary>
public class MethodParameterSource
{
    /// <summary>Initializes a new instance of the <see cref="MethodParameterSource" /> class.</summary>
    /// <param name="info">The parameter information.</param>
    public MethodParameterSource(ParameterInfo info)
    {
        Info = info;
    }

    /// <summary>Gets the parameter information.</summary>
    public ParameterInfo Info { get; }

    /// <summary>
    /// The index in the arguments array which contains the value for this parameter. A value of -1 indicates this
    /// parameter is not supplied by the arguments array.
    /// </summary>
    public int ParameterIndex { get; set; } = -1;

    /// <summary>Gets or sets a value indicating whether the value of this parameter is a service which should be
    /// retrieved from the service provider.</summary>
    public bool IsService { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this value of this parameter is a component which should be retrieved of
    /// the entity provided in the arguments array.
    /// </summary>
    public bool IsComponent { get; set; }
}