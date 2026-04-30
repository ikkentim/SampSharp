using Microsoft.CodeAnalysis;

namespace SampSharp.SourceGenerator.Marshalling;

public static class MarshallerHelper
{
    public static string GetVar(IParameterSymbol? parameterSymbol)
    {
        return parameterSymbol?.Name ?? MarshallerConstants.LocalReturnValue;
    }
    
    public static string GetMarshallerVar(IParameterSymbol? parameterSymbol)
    {
        return GetNativeExtraVar(parameterSymbol, "marshaller");
    }

    public static string GetNativeVar(IParameterSymbol? parameterSymbol)
    {
        return parameterSymbol == null 
            ? $"{MarshallerConstants.LocalReturnValue}_native" 
            : $"__{parameterSymbol.Name}_native";
    }
    
    public static string GetNativeExtraVar(IParameterSymbol? parameterSymbol, string extra)
    {
        return $"{GetNativeVar(parameterSymbol)}__{extra}";
    }
}