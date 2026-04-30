using System.Runtime.InteropServices;
using SampSharp.OpenMp.Core.RobinHood;

namespace SampSharp.OpenMp.Core.Api;

[StructLayout(LayoutKind.Sequential)]
internal struct MarkedPoolIteratorData
{
    public nint Pool;
    public int LockedId;
    public nint Entries;
    public FlatPtrHashSetIterator Iter;
}