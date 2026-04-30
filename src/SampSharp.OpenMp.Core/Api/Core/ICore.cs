using System.Runtime.InteropServices.Marshalling;
using SampSharp.OpenMp.Core.RobinHood;
using SampSharp.OpenMp.Core.Std.Chrono;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="ICore" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtensible), typeof(ILogger))]
public readonly partial struct ICore
{
    /// <summary>
    /// Gets the version of the open.mp server.
    /// </summary>
    /// <returns>The version of the open.mp server package.</returns>
    public partial SemanticVersion GetVersion();

    /// <summary>
    /// Get the version of the NetworkBitStream class the core was built with.
    /// </summary>
    /// <returns>The version of the NetworkBitStream class.</returns>
    public partial int GetNetworkBitStreamVersion();

    /// <summary>
    /// Gets the player pool
    /// </summary>
    /// <returns>The player pool.</returns>
    public partial IPlayerPool GetPlayers();

    /// <summary>
    /// Gets the core event dispatcher.
    /// </summary>
    /// <returns>The core event dispatcher.</returns>
    public partial IEventDispatcher<ICoreEventHandler> GetEventDispatcher();

    /// <summary>
    /// Gets the server configuration.
    /// </summary>
    /// <returns>Yhe server configuration.</returns>
    public partial IConfig GetConfig();

    /// <summary>
    /// Gets a list of available networks.
    /// </summary>
    /// <returns>A list of available networks.</returns>
    public partial FlatPtrHashSet<INetwork> GetNetworks();

    /// <summary>
    /// Gets the tick count.
    /// </summary>
    /// <returns>The tick count.</returns>
    public partial uint GetTickCount();

    /// <summary>
    /// Sets the server gravity.
    /// </summary>
    /// <param name="gravity">The server gravity.</param>
    public partial void SetGravity(float gravity);

    /// <summary>
    /// Gets the server gravity.
    /// </summary>
    /// <returns>The server gravity.</returns>
    public partial float GetGravity();

    /// <summary>
    /// Sets the server weather.
    /// </summary>
    /// <param name="weather">The server weather.</param>
    public partial void SetWeather(int weather);

    /// <summary>
    /// Sets the server world time.
    /// </summary>
    /// <param name="time">The world time, truncated to whole hours on the native side.</param>
    public partial void SetWorldTime([MarshalUsing(typeof(HoursMarshaller))] TimeSpan time);

    /// <summary>
    /// Toggles server stunt bonuses.
    /// </summary>
    /// <param name="enable"><see langword="true" /> if stunt bonuses should be enabled.</param>
    public partial void UseStuntBonuses(bool enable);

    /// <summary>
    /// Sets string data during runtime.
    /// </summary>
    /// <param name="type">The type of the data.</param>
    /// <param name="data">The data value.</param>
    public partial void SetData(SettableCoreDataType type, string data);

    /// <summary>
    /// Sets the sleep value for each main thread update cycle.
    /// </summary>
    /// <param name="value">The sleep duration.</param>
    public partial void SetThreadSleep(Microseconds value);

    /// <summary>
    /// Toggle dynamic ticks instead of static duration sleep.
    /// </summary>
    /// <param name="enable"><see langword="true" /> if dynamic ticks should be enabled.</param>
    public partial void UseDynTicks(bool enable);

    /// <summary>
    /// Clear all entities that vanish on GM exit.
    /// </summary>
    public partial void ResetAll();

    /// <summary>
    /// Create all entities that appear on GM start.
    /// </summary>
    public partial void ReloadAll();

    /// <summary>
    /// Get the name of a weapon.
    /// </summary>
    /// <param name="weapon">The weapon.</param>
    /// <returns>The name of the weapon.</returns>
    public partial string GetWeaponName(PlayerWeapon weapon);

    /// <summary>
    /// Attempt to connect a new bot to the server.
    /// </summary>
    /// <param name="name">The bot name (player name).</param>
    /// <param name="script">The bot script to execute.</param>
    public partial void ConnectBot(string name, string script);

    /// <summary>
    /// Gets the ticks per second.
    /// </summary>
    /// <returns>Ticks per second.</returns>
    public partial uint TickRate();

    /// <summary>
    /// Gets the version hash of the open.mp server.
    /// </summary>
    /// <returns>The version hash.</returns>
    public partial string GetVersionHash();
}