
namespace SampSharp.GameMode.Definitions
{
   public enum ConnectionStatusType
    {
        NoAction,
        DisconnectASAP,
        DisconnectASAPSilently,
        DisconnectOnNoAck,
        RequestedConnection,
        HandlingConnectionRequest,
        UnverifiedSender,
        SetEncryptionOnMultiple16BytePacket,
        Connected
    }
}
