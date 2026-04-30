using System.Reflection;

namespace SampSharp.Entities.Utilities;

/// <summary>Represents a utility for scanning for classes and members with specific attributes in loaded assemblies.</summary>
public sealed class ClassScanner
{
    private List<Assembly> _assemblies = [];
    private List<Type> _classAttributes = [];
    private List<Type> _classImplements = [];
    private List<Type> _classTypes = [];
    private bool _includeNonPublicMembers;
    private List<Type> _memberAttributes = [];
    private bool _includeAbstract;

    private BindingFlags MemberBindingFlags =>
        BindingFlags.Instance |
        BindingFlags.Public |
        (_includeNonPublicMembers ? BindingFlags.NonPublic : BindingFlags.Default);

    private ClassScanner()
    {
    }

    /// <summary>
    /// Creates a new <see cref="ClassScanner" /> instance.
    /// </summary>
    /// <returns>A new scanner instance.</returns>
    public static ClassScanner Create()
    {
        return new ClassScanner();
    }
    /// <summary>Includes the specified <paramref name="assembly" /> in the scan.</summary>
    /// <param name="assembly">The assembly to include.</param>
    /// <returns>An updated scanner.</returns>
    public ClassScanner IncludeAssembly(Assembly assembly)
    {
        if (_assemblies.Contains(assembly))
        {
            return this;
        }

        var result = Clone();
        result._assemblies.Add(assembly);
        return result;
    }

    /// <summary>Includes the referenced assemblies of the previously included assemblies in the scan.</summary>
    /// <returns>An updated scanner.</returns>
    public ClassScanner IncludeReferencedAssemblies()
    {
        var assemblies = new List<Assembly>();

        foreach (var a in _assemblies)
        {
            AddToScan(a);
        }

        var result = Clone();
        result._assemblies = assemblies;
        return result;

        void AddToScan(Assembly asm)
        {
            if (assemblies.Contains(asm))
            {
                return;
            }

            assemblies.Add(asm);

            foreach (var assemblyRef in asm.GetReferencedAssemblies())
            {
                if (IsSystemAssembly(assemblyRef))
                {
                    continue;
                }

                AddToScan(Assembly.Load(assemblyRef));
            }
        }
    }

    /// <summary>
    /// Includes the specified <paramref name="types" /> in the scan. This can be used to include types from assemblies which are not directly referenced by the loaded assemblies, e.g. plugin assemblies.
    /// </summary>
    /// <param name="types">The types to include.</param>
    /// <returns>An updated scanner.</returns>
    public ClassScanner IncludeTypes(ReadOnlySpan<Type> types)
    {
        var result = Clone();
        result._classTypes.AddRange(types);
        return result;
    }

    private static bool IsSystemAssembly(AssemblyName assemblyRef)
    {
        return (assemblyRef.Name!.StartsWith("System", StringComparison.InvariantCulture) ||
                assemblyRef.Name.StartsWith("Microsoft", StringComparison.InvariantCulture) ||
                assemblyRef.Name.StartsWith("netstandard", StringComparison.InvariantCulture));
    }

    /// <summary>Includes non-public members in the scan.</summary>
    /// <returns>An updated scanner.</returns>
    public ClassScanner IncludeNonPublicMembers()
    {
        var result = Clone();
        result._includeNonPublicMembers = true;
        return result;
    }

    /// <summary>Includes members of abstract classes in the scan.</summary>
    /// <returns>An updated scanner.</returns>
    public ClassScanner IncludeAbstractClasses()
    {
        var result = Clone();
        result._includeAbstract = true;
        return result;
    }

    /// <summary>Includes only members of classes which implement <typeparamref name="T" /> in the scan.</summary>
    /// <typeparam name="T">The class or interface the results of the scan should implement.</typeparam>
    /// <returns>An updated scanner.</returns>
    public ClassScanner Implements<T>()
    {
        var result = Clone();
        result._classImplements.Add(typeof(T));
        return result;
    }

    /// <summary>Includes only members of classes which have an attribute <typeparamref name="T" />.</summary>
    /// <typeparam name="T">The type of the attribute the class should have.</typeparam>
    /// <returns>An updated scanner.</returns>
    public ClassScanner HasClassAttribute<T>() where T : Attribute
    {
        var result = Clone();
        result._classAttributes.Add(typeof(T));
        return result;
    }

    /// <summary>Includes only members which have an attribute <typeparamref name="T" />.</summary>
    /// <typeparam name="T">The type of the attribute the member should have.</typeparam>
    /// <returns>An updated scanner.</returns>
    public ClassScanner HasMemberAttribute<T>() where T : Attribute
    {
        var result = Clone();
        result._memberAttributes.Add(typeof(T));
        return result;
    }

    private bool ApplyTypeFilter(Type type)
    {
        return type.IsClass &&
               (_includeAbstract || !type.IsAbstract) &&
               _classImplements.All(i => i.IsAssignableFrom(type)) &&
               _classAttributes.All(a => type.GetCustomAttribute(a) != null);
    }

    private bool ApplyMemberFilter(MemberInfo memberInfo)
    {
        return _memberAttributes.All(a => memberInfo.GetCustomAttribute(a) != null);
    }

    /// <summary>Runs the scan for methods.</summary>
    /// <returns>The found methods.</returns>
    public IEnumerable<Type> ScanTypes()
    {
        return _assemblies.SelectMany(a => a.GetTypes())
            .Concat(_classTypes)
            .Where(ApplyTypeFilter)
            .Distinct();
    }

    /// <summary>Runs the scan for methods and provides the attribute <typeparamref name="TAttribute" /> in the
    /// results.</summary>
    /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
    /// <returns>The found methods with their attribute of type <typeparamref name="TAttribute" />.</returns>
    public IEnumerable<(Type target, MethodInfo method, TAttribute attribute)> ScanMethods<TAttribute>() where TAttribute : Attribute
    {
        return HasMemberAttribute<TAttribute>()
            .ScanMethods()
            .Select(x => (x.target, x.method, attribute: x.method.GetCustomAttribute<TAttribute>()!));
    }

    /// <summary>Runs the scan for methods.</summary>
    /// <returns>The found methods.</returns>
    public IEnumerable<(Type target, MethodInfo method)> ScanMethods()
    {
        return ScanTypes()
            .SelectMany(t => t.GetMethods(MemberBindingFlags).Select(m => (t, m)))
            .Where(x => ApplyMemberFilter(x.m));
    }

    private ClassScanner Clone()
    {
        return new ClassScanner
        {
            _assemblies = [.. _assemblies],
            _classTypes = [.. _classTypes],
            _includeNonPublicMembers = _includeNonPublicMembers,
            _classImplements = [.. _classImplements],
            _classAttributes = [.. _classAttributes],
            _memberAttributes = [.. _memberAttributes],
            _includeAbstract = _includeAbstract
        };
    }
}