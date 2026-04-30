using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IMenusComponent" /> interface.
/// </summary>
[OpenMpApi(typeof(IPoolComponent<IMenu>))]
public readonly partial struct IMenusComponent
{
    /// <inheritdoc />
    public static UID ComponentId => new(0x621e219eb97ee0b2);

    /// <summary>
    /// Gets the event dispatcher for menu events.
    /// </summary>
    /// <returns>An event dispatcher for <see cref="IMenuEventHandler"/> events.</returns>
    public partial IEventDispatcher<IMenuEventHandler> GetEventDispatcher();

    /// <summary>
    /// Creates a new menu with the specified properties.
    /// </summary>
    /// <param name="title">The title of the menu.</param>
    /// <param name="position">The position to display the menu.</param>
    /// <param name="columns">The number of columns (1 or 2).</param>
    /// <param name="col1Width">The width of the first column.</param>
    /// <param name="col2Width">The width of the second column (if applicable).</param>
    /// <returns>The created menu, or <c>null</c> if creation failed.</returns>
    public partial IMenu Create(string title, Vector2 position, byte columns, float col1Width, float col2Width);
}