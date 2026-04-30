namespace SampSharp.OpenMp.Core;

/// <summary>
/// This attribute marks an open.mp API interface struct as partial. Partial API structs will not implement their own managed interface.
/// </summary>
[AttributeUsage(AttributeTargets.Struct)]
public class OpenMpApiPartialAttribute : Attribute;