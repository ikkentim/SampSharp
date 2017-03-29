using System;

namespace CommunicationTest
{
    [Flags]
    public enum RegisterCallArguments : byte
    {
        Terminator = 0x00,
        Value = 0x01,
        Array = 0x02,
        String = 0x04,
        Reference = 0x08,
        ValueReference = Value | Reference,
        ArrayReference = Array | Reference,
        StringReference = String | Reference
    }
}