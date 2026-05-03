using System.Reflection;

namespace SampSharp.Entities.SAMP.Commands.Core;

/// <summary>
/// Represents a single overload of a command (one specific method implementation).
/// Multiple overloads can exist for the same command with different parameter types.
/// </summary>
public class CommandOverload
{
    /// <summary>Initializes a new instance.</summary>
    public CommandOverload(
        MethodInfo method,
        ParameterInfo[] parameters,
        Type declaringSystemType,
        CommandParameterInfo[] parsedParameters)
    {
        if (method == null)
        {
            throw new ArgumentNullException(nameof(method));
        }

        if (parameters == null)
        {
            throw new ArgumentNullException(nameof(parameters));
        }

        if (declaringSystemType == null)
        {
            throw new ArgumentNullException(nameof(declaringSystemType));
        }

        if (parsedParameters == null)
        {
            throw new ArgumentNullException(nameof(parsedParameters));
        }

        Method = method;
        MethodParameters = parameters;
        DeclaringSystemType = declaringSystemType;
        ParsedParameters = parsedParameters;
    }

    /// <summary>The method that implements this command overload.</summary>
    public MethodInfo Method { get; }

    /// <summary>All parameters of the method (including prefix and DI).</summary>
    public ParameterInfo[] MethodParameters { get; }

    /// <summary>The type of the ISystem that declares this command.</summary>
    public Type DeclaringSystemType { get; }

    /// <summary>
    /// Parameters that are parsed from command input (excludes prefix and DI parameters).
    /// These are in the order they appear in the method signature.
    /// </summary>
    public CommandParameterInfo[] ParsedParameters { get; }

    /// <summary>The return type of the method.</summary>
    public Type ReturnType => Method.ReturnType;

    /// <summary>Whether this method returns a Task or Task&lt;T&gt;.</summary>
    public bool IsAsync => ReturnType.IsGenericType
        ? (ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
        : ReturnType == typeof(Task) || ReturnType == typeof(ValueTask);

    /// <summary>For Task&lt;T&gt;, returns T. For Task/ValueTask, returns void. Otherwise returns the actual return type.</summary>
    public Type GetEffectiveReturnType()
    {
        if (ReturnType == typeof(Task) || ReturnType == typeof(ValueTask))
        {
            return typeof(void);
        }

        if (ReturnType.IsGenericType)
        {
            var genericDef = ReturnType.GetGenericTypeDefinition();
            if (genericDef == typeof(Task<>) || genericDef == typeof(ValueTask<>))
            {
                return ReturnType.GetGenericArguments()[0];
            }
        }

        return ReturnType;
    }
}
