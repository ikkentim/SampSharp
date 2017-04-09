using System;

namespace SampSharp.Core.Natives
{
    [Flags]
    public enum NativeParameterType : byte
    {
        Int32 = (1 << 0),
        Single = (1 << 1),
        Bool = (1 << 2),
        String = (1 << 3),
        Array = (1 << 4),
        Reference = (1 << 5),

        Int32Reference = Int32 | Reference,
        SingleReference = Single | Reference,
        BoolReference = Bool | Reference,
        StringReference = String | Reference,

        Int32Array = Int32 | Array,
        SingleArray = Single | Array,
        BoolArray = Bool | Array,

        Int32ArrayReference = Int32 | Array | Reference,
        SingleArrayReference = Single | Array | Reference,
        BoolArrayReference = Bool | Array | Reference,
    }
}