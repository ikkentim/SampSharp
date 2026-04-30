namespace SampSharp.OpenMp.Core;

/// <summary>
/// Specifies the name of the open.mp API function that this method represents. By default, the name of the method
/// converted to camel case is used as the function name.
/// </summary>
/// <param name="functionName">The name of the open.mp API function.</param>
[AttributeUsage(AttributeTargets.Method)]
public class OpenMpApiFunctionAttribute(string functionName) : Attribute
{
    /// <summary>
    /// Gets the name of the open.mp API function.
    /// </summary>
    public string FunctionName { get; } = functionName;
}