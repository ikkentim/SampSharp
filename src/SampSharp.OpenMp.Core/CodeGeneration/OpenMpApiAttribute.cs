namespace SampSharp.OpenMp.Core;

/// <summary>
/// This attributes marks a struct as an open.mp API interface. The struct must be marked as partial. All partial
/// methods in the struct will be considered as open.mp API functions and will have P/Invoke implementations generated
/// by the source generator. The invoked function name will be
/// <c>{interface}_{method_in_pascalCase}{overloadAppendix}</c>. For example for the function <c>IConfig.RemoveBan</c>,
/// the C function will be <c>IConfig_removeBan</c>. The overload appendix can be controlled using the <see
/// cref="OpenMpApiOverloadAttribute" />.
/// </summary>
/// <param name="baseTypeList">Specifies which open.mp API interface struct types this struct implements. Equality
/// members, cast operators and forwarding methods will be generated for all specified interfaces.</param>
[AttributeUsage(AttributeTargets.Struct)]
public class OpenMpApiAttribute(params Type[] baseTypeList) : Attribute
{
    /// <summary>
    /// Gets which open.mp API interface struct types this struct implements. Equality members, cast operators and
    /// forwarding methods will be generated for all specified interfaces.
    /// </summary>
    public Type[] BaseTypeList { get; } = baseTypeList;

    /// <summary>
    /// Gets or sets the name of the component library that contains the open.mp API functions. Defaults to "SampSharp".
    /// </summary>
    public string Library { get; set; } = "SampSharp";

    /// <summary>
    /// Gets or sets the name of the open.mp API interface. Defaults to the struct name.
    /// </summary>
    public string? NativeTypeName { get; set; }
}