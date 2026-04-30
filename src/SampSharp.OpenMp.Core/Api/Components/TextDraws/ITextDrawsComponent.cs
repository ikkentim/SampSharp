using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="ITextDrawsComponent" /> interface.
/// </summary>
[OpenMpApi(typeof(IPoolComponent<ITextDraw>))]
public readonly partial struct ITextDrawsComponent
{
    /// <inheritdoc />
    public static UID ComponentId => new(0x9b5dc2b1d15c992a);

    /// <summary>
    /// Retrieves the event dispatcher for text draw events.
    /// </summary>
    public partial IEventDispatcher<ITextDrawEventHandler> GetEventDispatcher();

    /// <summary>
    /// Creates a new text draw with the specified position and text.
    /// </summary>
    /// <param name="position">The position of the text draw.</param>
    /// <param name="text">The text of the text draw.</param>
    /// <returns>The created text draw.</returns>
    public partial ITextDraw Create(Vector2 position, string text);

    /// <summary>
    /// Creates a new text draw with the specified position and preview model.
    /// </summary>
    /// <param name="position">The position of the text draw.</param>
    /// <param name="model">The preview model of the text draw.</param>
    /// <returns>The created text draw.</returns>
    [OpenMpApiOverload("_model")]
    public partial ITextDraw Create(Vector2 position, int model);
}