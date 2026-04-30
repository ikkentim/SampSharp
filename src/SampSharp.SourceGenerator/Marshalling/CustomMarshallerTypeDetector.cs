using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using SampSharp.SourceGenerator.Helpers;

namespace SampSharp.SourceGenerator.Marshalling;

/// <summary>
/// Provides methods for finding the best fitting custom marshaller.
/// </summary>
public class CustomMarshallerTypeDetector
{
    private readonly WellKnownMarshallerTypes _wellKnownMarshallerTypes;

    public CustomMarshallerTypeDetector(WellKnownMarshallerTypes wellKnownMarshallerTypes)
    {
        _wellKnownMarshallerTypes = wellKnownMarshallerTypes;
    }

    /// <summary>
    /// Gets the best fitting custom marshaller for the specified parameter.
    /// </summary>
    public CustomMarshallerInfo? GetCustomMarshaller(IParameterSymbol parameter, ITypeSymbol type, MarshalDirection direction)
    {
        var marshalUsing = parameter.GetAttribute(Constants.MarshalUsingAttributeFQN);
        var typeMarshaller = type.GetAttribute(Constants.NativeMarshallingAttributeFQN);

        var entryPoint = GetEntryPoint(typeMarshaller, marshalUsing, type);

        return entryPoint == null
            ? null
            : GetCustomMarshaller(entryPoint,  type, direction, parameter.RefKind);
    }

    /// <summary>
    /// Gets the best fitting custom marshaller for the return value of the specified method.
    /// </summary>
    public CustomMarshallerInfo? GetCustomMarshaller(IMethodSymbol method, ITypeSymbol type, MarshalDirection direction)
    {
        if (method.ReturnsVoid)
        {
            return null;
        }

        var marshalUsing = method.GetReturnTypeAttribute(Constants.MarshalUsingAttributeFQN);
        var typeMarshaller = type.GetAttribute(Constants.NativeMarshallingAttributeFQN);

        var entryPoint = GetEntryPoint(typeMarshaller, marshalUsing, type);

        return entryPoint == null
            ? null
            : GetCustomMarshaller(entryPoint, type, direction, RefKind.Out);
    }

    private static CustomMarshallerInfo? GetCustomMarshaller(
        ITypeSymbol entryPoint,
        ITypeSymbol managedType,
        MarshalDirection direction,
        RefKind refKind)
    {
        var dir = GetDirectionInfo(direction);

        var filteredModes = GetModesFromEntryPoint(entryPoint, managedType).Where(x => managedType.IsSame(x.ManagedType.Symbol)).ToList();

        if (filteredModes.Count == 0)
        {
            return null;
        }

        // Select best fitting marshaller mode for the parameter
        var marshallerMode = refKind switch
        {
            RefKind.In or RefKind.RefReadOnlyParameter or RefKind.None => filteredModes.FirstOrDefault(x => x.MarshalMode == dir.In),
            RefKind.Out => filteredModes.FirstOrDefault(x => x.MarshalMode == dir.Out),
            RefKind.Ref => filteredModes.FirstOrDefault(x => x.MarshalMode == dir.Ref),
            _ => null
        };

        var defaultInfo = filteredModes.FirstOrDefault(x => x.MarshalMode == MarshalMode.Default);

        return marshallerMode ?? defaultInfo;
    }

    private static CustomMarshallerInfo[] GetModesFromEntryPoint(ITypeSymbol entryPoint, ITypeSymbol forType)
    {
        return entryPoint.GetAttributes(Constants.CustomMarshallerAttributeFQN)
            .Select(x => GetModeFromAttribute(x, forType))
            .WhereNotNull()
            .ToArray();
    }

    private static CustomMarshallerInfo? GetModeFromAttribute(AttributeData attributeData, ITypeSymbol forType)
    {
        var managedType = (ITypeSymbol)attributeData.ConstructorArguments[0].Value!;
        var mode = ModeForValue(attributeData.ConstructorArguments[1].Value!);

        if (attributeData.ConstructorArguments[2].Value is not INamedTypeSymbol marshallerType)
        {
            return null;
        }

        if (managedType.IsSame(Constants.GenericPlaceholderFQN))
        {
            managedType = forType;
        }

        // Replace generic placeholders with the actual type
        if (marshallerType is { IsGenericType: true })
        {
            marshallerType = ReplacePlaceholderWithType(marshallerType, forType);
        }

        if (marshallerType.ContainingType is { IsGenericType: true })
        {
            var containing = ReplacePlaceholderWithType(marshallerType.ContainingType, forType);

            // TODO: might not work properly for nested generic types
            marshallerType = containing.GetMembers(marshallerType.Name)
                .OfType<INamedTypeSymbol>()
                .First();
        }


        return new CustomMarshallerInfo(new ManagedType(managedType), mode, new ManagedType(marshallerType));
    }

    private static INamedTypeSymbol ReplacePlaceholderWithType(INamedTypeSymbol namedType, ITypeSymbol type)
    {
        var replacements = 0;
        var typeArguments = namedType.TypeArguments;
        while (typeArguments.Any(x => x.IsSame(Constants.GenericPlaceholderFQN)))
        {
            var placeholder = namedType.TypeArguments.First(x => x.IsSame(Constants.GenericPlaceholderFQN));

            typeArguments = typeArguments.Replace(placeholder, type);
            replacements++;
        }

        return replacements > 0
            ? namedType.ConstructedFrom.Construct(typeArguments.ToArray())
            : namedType;
    }

    private static MarshalMode ModeForValue(object constant)
    {
        return constant is int number
            ? (MarshalMode)number
            : MarshalMode.Other;
    }

    private static MarshalDirectionInfo GetDirectionInfo(MarshalDirection direction)
    {
        return direction switch
        {
            MarshalDirection.ManagedToUnmanaged => MarshalDirectionInfo.ManagedToUnmanaged,
            MarshalDirection.UnmanagedToManaged => MarshalDirectionInfo.UnmanagedToManaged,
            _ => throw new ArgumentOutOfRangeException(nameof(direction))
        };
    }

    private ITypeSymbol? GetEntryPoint(AttributeData? typeMarshallerAttrib, AttributeData? marshalUsingAttrib,
        ITypeSymbol managedType)
    {
        var marshaller = typeMarshallerAttrib?.ConstructorArguments[0].Value as ITypeSymbol;
        if (marshalUsingAttrib?.ConstructorArguments.Length > 0)
        {
            if (marshalUsingAttrib.ConstructorArguments[0].Value is ITypeSymbol marshallerOverride)
            {
                marshaller = marshallerOverride;
            }
        }

        // If no marshaller specified, look at a matching well known marshaller
        if (marshaller != null)
        {
            return marshaller;
        }

        return _wellKnownMarshallerTypes.Marshallers
            .FirstOrDefault(x => x.matcher(managedType) && x.marshaller != null)
            .marshaller;
    }

    private record MarshalDirectionInfo(MarshalMode In, MarshalMode Out, MarshalMode Ref)
    {
        public static readonly MarshalDirectionInfo ManagedToUnmanaged =
            new(MarshalMode.ManagedToUnmanagedIn,
                MarshalMode.ManagedToUnmanagedOut,
                MarshalMode.ManagedToUnmanagedRef);

        public static readonly MarshalDirectionInfo UnmanagedToManaged =
            new(MarshalMode.UnmanagedToManagedIn,
                MarshalMode.UnmanagedToManagedOut,
                MarshalMode.UnmanagedToManagedRef);
    }
}