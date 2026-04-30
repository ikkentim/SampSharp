namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents the network statistics of a player.
/// </summary>
/// <param name="stats">The open.mp stats this value represents.</param>
public readonly struct NetworkStats(OpenMp.Core.Api.NetworkStats stats)
{
    /// <summary>
    /// Gets the connection start time.
    /// </summary>
    public int ConnectionStartTime => (int)stats.ConnectionStartTime;

    /// <summary>
    /// Gets the number of messages in the send buffer.
    /// </summary>
    public int MessageSendBuffer => (int)stats.MessageSendBuffer;

    /// <summary>
    /// Gets the number of messages sent.
    /// </summary>
    public int MessagesSent => (int)stats.MessagesSent;

    /// <summary>
    /// Gets the total number of bytes sent.
    /// </summary>
    public int TotalBytesSent => (int)stats.TotalBytesSent;

    /// <summary>
    /// Gets the number of acknowledgements sent.
    /// </summary>
    public int AcknowlegementsSent => (int)stats.AcknowlegementsSent;

    /// <summary>
    /// Gets the number of acknowledgements pending.
    /// </summary>
    public int AcknowlegementsPending => (int)stats.AcknowlegementsPending;

    /// <summary>
    /// Gets the number of messages on the resend queue.
    /// </summary>
    public int MessagesOnResendQueue => (int)stats.MessagesOnResendQueue;

    /// <summary>
    /// Gets the number of message resends.
    /// </summary>
    public int MessageResends => (int)stats.MessageResends;

    /// <summary>
    /// Gets the total number of bytes of messages resent.
    /// </summary>
    public int MessagesTotalBytesResent => (int)stats.MessagesTotalBytesResent;

    /// <summary>
    /// Gets the packet loss percentage.
    /// </summary>
    public float Packetloss => stats.Packetloss;

    /// <summary>
    /// Gets the number of messages received.
    /// </summary>
    public int MessagesReceived => (int)stats.MessagesReceived;

    /// <summary>
    /// Gets the number of messages received per second.
    /// </summary>
    public int MessagesReceivedPerSecond => (int)stats.MessagesReceivedPerSecond;

    /// <summary>
    /// Gets the total number of bytes received.
    /// </summary>
    public int BytesReceived => (int)stats.BytesReceived;

    /// <summary>
    /// Gets the number of acknowledgements received.
    /// </summary>
    public int AcknowlegementsReceived => (int)stats.AcknowlegementsReceived;

    /// <summary>
    /// Gets the number of duplicate acknowledgements received.
    /// </summary>
    public int DuplicateAcknowlegementsReceived => (int)stats.DuplicateAcknowlegementsReceived;

    /// <summary>
    /// Gets the number of bits per second sent and received.
    /// </summary>
    public double BitsPerSecond => stats.BitsPerSecond;

    /// <summary>
    /// Gets the number of bits per second sent.
    /// </summary>
    public double BpsSent => stats.BpsSent;

    /// <summary>
    /// Gets the number of bits per second received.
    /// </summary>
    public double BpsReceived => stats.BpsReceived;

    /// <summary>
    /// Gets a value indicating whether the connection is active.
    /// </summary>
    public bool IsActive => stats.IsActive;

    /// <summary>
    /// Gets the connection mode/status.
    /// </summary>
    public ConnectionStatus ConnectMode => (ConnectionStatus)stats.ConnectMode;

    /// <summary>
    /// Gets the connection elapsed time.
    /// </summary>
    public uint ConnectionElapsedTime => stats.ConnectionElapsedTime;
}
