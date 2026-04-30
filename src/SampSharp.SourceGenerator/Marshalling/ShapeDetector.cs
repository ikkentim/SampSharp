namespace SampSharp.SourceGenerator.Marshalling;

/// <summary>
/// Provides methods for determining the shape of a custom marshaller.
/// </summary>
public static class ShapeDetector
{
    public static (MarshallerShape shape, ManagedType nativeType) GetShapeOfMarshaller(CustomMarshallerInfo marshaller)
    {
        var members = MarshalInspector.GetMembers(marshaller);
        var nativeType = marshaller.ManagedType;
        var shape = MarshallerShape.None;
        if (marshaller.IsStateless)
        {
            if (members.StatelessConvertToManagedFinallyMethod != null)
            {
                shape |= MarshallerShape.GuaranteedUnmarshal;
                nativeType = new ManagedType(members.StatelessConvertToManagedFinallyMethod.Parameters[0].Type);
            }

            if (members.StatelessConvertToManagedMethod != null)
            {
                shape |= MarshallerShape.ToManaged;
                nativeType = new ManagedType(members.StatelessConvertToManagedMethod.Parameters[0].Type);
            }

            if (members.StatelessConvertToUnmanagedWithBufferMethod != null)
            {
                shape |= MarshallerShape.CallerAllocatedBuffer | MarshallerShape.ToUnmanaged;
                nativeType = new ManagedType(members.StatelessConvertToUnmanagedWithBufferMethod.ReturnType);
            }

            if (members.StatelessConvertToUnmanagedMethod != null)
            {
                shape |= MarshallerShape.ToUnmanaged;
                nativeType = new ManagedType(members.StatelessConvertToUnmanagedMethod.ReturnType);
            }

            if (members.StatelessFreeMethod != null)
            {
                shape |= MarshallerShape.Free;
            }

            if (members.StatelessGetPinnableReferenceMethod != null)
            {
                shape |= MarshallerShape.StatelessPinnableReference;
            }
        }
        else if (marshaller.IsStateful)
        {
            if (members.StatefulToUnmanagedMethod != null)
            {
                shape |= MarshallerShape.ToUnmanaged;
                nativeType = new ManagedType(members.StatefulToUnmanagedMethod.ReturnType);

                if (members.StatefulFromManagedWithBufferMethod != null)
                {
                    shape |= MarshallerShape.CallerAllocatedBuffer;
                }
            }

            if (members.StatefulFromUnmanagedMethod != null)
            {
                nativeType = new ManagedType(members.StatefulFromUnmanagedMethod.Parameters[0].Type);
                shape |= MarshallerShape.ToManaged;
            }
            
            if (members.StatefulFreeMethod != null)
            {
                shape |= MarshallerShape.Free;
            }

            if (members.StatefulOnInvokedMethod != null)
            {
                shape |= MarshallerShape.OnInvoked;
            }

            if (members.StatefulGetPinnableReferenceMethod != null)
            {
                shape |= MarshallerShape.StatefulPinnableReference;
            }

            if (members.StatelessGetPinnableReferenceMethod != null)
            {
                shape |= MarshallerShape.StatelessPinnableReference;
            }
        }

        return (shape, nativeType);
    }
}