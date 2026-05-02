namespace SampSharp.Entities.SAMP;

/// <summary>
/// Contains the connection status values possible for player connections.
/// </summary>
public enum ConnectionStatus
{
    /// <summary>
    /// NO_ACTION
    /// </summary>
    NoAction = 0,

    /// <summary>
    /// DISCONNECT_ASAP
    /// </summary>
    DisconnectASAP = 1,

    /// <summary>
    /// DISCONNECT_ASAP_SILENTLY
    /// </summary>
    DisconnectASAPSilently = 2,

    /// <summary>
    /// DISCONNECT_ON_NO_ACK
    /// </summary>
    DisconnectOnNoAck = 3,

    /// <summary>
    /// REQUESTED_CONNECTION
    /// </summary>
    RequestedConnection = 4,

    /// <summary>
    /// HANDLING_CONNECTION_REQUEST
    /// </summary>
    HandlingConnectionRequest = 5,

    /// <summary>
    /// UNVERIFIED_SENDER
    /// </summary>
    UnverifiedSender = 6,

    /// <summary>
    /// SET_ENCRYPTION_ON_MULTIPLE_16_BYTE_PACKET
    /// </summary>
    SetEncryptionOnMultiple16BytePacket = 7,

    /// <summary>
    /// CONNECTED
    /// </summary>
    Connected = 8
}