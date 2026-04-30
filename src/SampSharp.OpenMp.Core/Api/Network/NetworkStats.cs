using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents network statistics for a connection.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct NetworkStats
{
    /// <summary>
    /// Gets the time when the connection started, in milliseconds since the application started.
    /// </summary>
    public readonly uint ConnectionStartTime;

    /// <summary>
    /// Gets the size of the message send buffer.
    /// </summary>
    public readonly uint MessageSendBuffer;

    /// <summary>
    /// Gets the total number of messages sent.
    /// </summary>
    public readonly uint MessagesSent;

    /// <summary>
    /// Gets the total number of bytes sent.
    /// </summary>
    public readonly uint TotalBytesSent;

    /// <summary>
    /// Gets the total number of acknowledgements sent.
    /// </summary>
    public readonly uint AcknowlegementsSent;

    /// <summary>
    /// Gets the number of acknowledgements currently pending.
    /// </summary>
    public readonly uint AcknowlegementsPending;

    /// <summary>
    /// Gets the number of messages currently on the resend queue.
    /// </summary>
    public readonly uint MessagesOnResendQueue;

    /// <summary>
    /// Gets the total number of message resends.
    /// </summary>
    public readonly uint MessageResends;

    /// <summary>
    /// Gets the total number of bytes resent for messages.
    /// </summary>
    public readonly uint MessagesTotalBytesResent;

    /// <summary>
    /// Gets the packet loss percentage.
    /// </summary>
    public readonly float Packetloss;

    /// <summary>
    /// Gets the total number of messages received.
    /// </summary>
    public readonly uint MessagesReceived;

    /// <summary>
    /// Gets the number of messages received per second.
    /// </summary>
    public readonly uint MessagesReceivedPerSecond;

    /// <summary>
    /// Gets the total number of bytes received.
    /// </summary>
    public readonly uint BytesReceived;

    /// <summary>
    /// Gets the total number of acknowledgements received.
    /// </summary>
    public readonly uint AcknowlegementsReceived;

    /// <summary>
    /// Gets the total number of duplicate acknowledgements received.
    /// </summary>
    public readonly uint DuplicateAcknowlegementsReceived;

    /// <summary>
    /// Gets the current bits per second rate.
    /// </summary>
    public readonly double BitsPerSecond;

    /// <summary>
    /// Gets the bits per second rate for sent data.
    /// </summary>
    public readonly double BpsSent;

    /// <summary>
    /// Gets the bits per second rate for received data.
    /// </summary>
    public readonly double BpsReceived;

    /// <summary>
    /// Gets a value indicating whether the connection is active.
    /// </summary>
    public readonly bool IsActive;

    /// <summary>
    /// Gets the connection mode.
    /// </summary>
    public readonly int ConnectMode;

    /// <summary>
    /// Gets the elapsed time of the connection, in milliseconds.
    /// </summary>
    public readonly uint ConnectionElapsedTime;
}
