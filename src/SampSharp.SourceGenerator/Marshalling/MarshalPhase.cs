namespace SampSharp.SourceGenerator.Marshalling;

public enum MarshalPhase
{
    Setup,
    Marshal,
    PinnedMarshal,
    Pin,
    NotifyForSuccessfulInvoke,
    UnmarshalCapture,
    Unmarshal,
    CleanupCalleeAllocated,
    CleanupCallerAllocated,
    GuaranteedUnmarshal
}