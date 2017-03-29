namespace CommunicationTest
{
    public enum ServerCommand : byte
    {
        Ping = 0x01,
        Print = 0x02,
        Response = 0x03,
        Reconnect = 0x04,
        RegisterCall = 0x05,
        FindNative = 0x06,
        InvokeNative = 0x07,
        Start = 0x08,

        Tick = 0x11,
        Pong = 0x12,
        PublicCall = 0x13,
        Reply = 0x14,
        Announce = 0x15,
    }
}