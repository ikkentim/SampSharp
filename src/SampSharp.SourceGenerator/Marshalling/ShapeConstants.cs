namespace SampSharp.SourceGenerator.Marshalling;

public static class ShapeConstants
{
    public const string MethodFree = "Free";
    public const string MethodFromManaged = "FromManaged";
    public const string MethodFromUnmanaged = "FromUnmanaged";
    public const string MethodToManaged = "ToManaged";
    public const string MethodToManagedFinally = "ToManagedFinally";
    public const string MethodToUnmanaged = "ToUnmanaged";
    public const string MethodOnInvoked = "OnInvoked";

    public const string MethodConvertToUnmanaged = "ConvertToUnmanaged";
    public const string MethodConvertToManaged = "ConvertToManaged";
    public const string MethodConvertToManagedFinally = "ConvertToManagedFinally";
    public const string MethodGetPinnableReference = "GetPinnableReference";

    public const string PropertyBufferSize = "BufferSize";
}
