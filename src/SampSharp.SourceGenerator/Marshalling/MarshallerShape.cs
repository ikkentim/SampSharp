using System;

namespace SampSharp.SourceGenerator.Marshalling;

[Flags]
public enum MarshallerShape
{
    None = 0x0,
    ToUnmanaged = 0x1,
    CallerAllocatedBuffer = 0x2,
    StatelessPinnableReference = 0x4,
    StatefulPinnableReference = 0x8,
    ToManaged = 0x10,
    GuaranteedUnmarshal = 0x20,
    Free = 0x40,
    OnInvoked = 0x80,
}