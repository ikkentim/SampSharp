using System.Numerics;
using SampSharp.OpenMp.Core.Std;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="ITextDrawBase" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtensible), typeof(IIDProvider))]
public readonly partial struct ITextDrawBase
{
    private partial void GetPosition(out Vector2 position);

    /// <summary>
    /// Gets the position of the textdraw.
    /// </summary>
    /// <returns>The position of the textdraw.</returns>
    public Vector2 GetPosition()
    {
        GetPosition(out var result);
        return result;
    }

    /// <summary>
    /// Sets the position of the textdraw.
    /// </summary>
    /// <param name="position">The new position of the textdraw.</param>
    /// <returns>This instance for chaining.</returns>
    public partial ref ITextDrawBase SetPosition(Vector2 position);

    /// <summary>
    /// Sets the text of the textdraw.
    /// </summary>
    /// <param name="text">The new text to display.</param>
    public partial void SetText(string text);

    /// <summary>
    /// Gets the text of the textdraw.
    /// </summary>
    /// <returns>The text currently displayed by the textdraw.</returns>
    public partial string GetText();

    /// <summary>
    /// Sets the size of the textdraw's letters.
    /// </summary>
    /// <param name="size">The new letter size.</param>
    /// <returns>This instance for chaining.</returns>
    public partial ref ITextDrawBase SetLetterSize(Vector2 size);
    private partial void GetLetterSize(out Vector2 size);

    /// <summary>
    /// Gets the size of the textdraw's letters.
    /// </summary>
    /// <returns>The size of the textdraw's letters.</returns>
    public Vector2 GetLetterSize()
    {
        GetLetterSize(out var result);
        return result;
    }

    /// <summary>
    /// Sets the size of the textdraw area.
    /// </summary>
    /// <param name="size">The new textdraw area size.</param>
    /// <returns>This instance for chaining.</returns>
    public partial ref ITextDrawBase SetTextSize(Vector2 size);
    private partial void GetTextSize(out Vector2 size);

    /// <summary>
    /// Gets the size of the textdraw area.
    /// </summary>
    /// <returns>The size of the textdraw area.</returns>
    public Vector2 GetTextSize()
    {
        GetTextSize(out var result);
        return result;
    }

    /// <summary>
    /// Sets the alignment of the textdraw's text.
    /// </summary>
    /// <param name="alignment">The alignment to set.</param>
    /// <returns>This instance for chaining.</returns>
    public partial ref ITextDrawBase SetAlignment(TextDrawAlignmentTypes alignment);

    /// <summary>
    /// Gets the alignment of the textdraw's text.
    /// </summary>
    /// <returns>The current text alignment.</returns>
    public partial TextDrawAlignmentTypes GetAlignment();

    /// <summary>
    /// Sets the colour of the textdraw's letters.
    /// </summary>
    /// <param name="colour">The new letter colour.</param>
    /// <returns>This instance for chaining.</returns>
    public partial ref ITextDrawBase SetColour(Colour colour);
    private partial void GetLetterColour(out Colour colour);

    /// <summary>
    /// Gets the colour of the textdraw's letters.
    /// </summary>
    /// <returns>The colour of the textdraw's letters.</returns>
    public Colour GetLetterColour()
    {
        GetLetterColour(out var result);
        return result;
    }

    /// <summary>
    /// Sets whether the textdraw uses a box.
    /// </summary>
    /// <param name="use">Whether to use a box. <see langword="true" /> to use a box, <see langword="false" /> otherwise.</param>
    /// <returns>This instance for chaining.</returns>
    public partial ref ITextDrawBase UseBox(bool use);

    /// <summary>
    /// Gets whether the textdraw uses a box.
    /// </summary>
    /// <returns><see langword="true" /> if the textdraw uses a box; otherwise, <see langword="false" />.</returns>
    public partial bool HasBox();

    /// <summary>
    /// Sets the colour of the textdraw's box.
    /// </summary>
    /// <param name="colour">The new box colour.</param>
    /// <returns>This instance for chaining.</returns>
    public partial ref ITextDrawBase SetBoxColour(Colour colour);

    /// <summary>
    /// Gets the colour of the textdraw's box.
    /// </summary>
    /// <param name="colour">The box colour.</param>
    public partial void GetBoxColour(out Colour colour);

    /// <summary>
    /// Gets the colour of the textdraw's box.
    /// </summary>
    /// <returns>The colour of the textdraw's box.</returns>
    public Colour GetBoxColour()
    {
        GetBoxColour(out var result);
        return result;
    }

    /// <summary>
    /// Sets the shadow strength of the textdraw.
    /// </summary>
    /// <param name="shadow">The shadow strength.</param>
    /// <returns>This instance for chaining.</returns>
    public partial ref ITextDrawBase SetShadow(int shadow);

    /// <summary>
    /// Gets the shadow strength of the textdraw.
    /// </summary>
    /// <returns>The shadow strength.</returns>
    public partial int GetShadow();

    /// <summary>
    /// Sets the outline of the textdraw.
    /// </summary>
    /// <param name="outline">The outline thickness.</param>
    /// <returns>This instance for chaining.</returns>
    public partial ref ITextDrawBase SetOutline(int outline);

    /// <summary>
    /// Gets the outline thickness of the textdraw.
    /// </summary>
    /// <returns>The outline thickness.</returns>
    public partial int GetOutline();

    /// <summary>
    /// Sets the background colour of the textdraw.
    /// </summary>
    /// <param name="colour">The new background colour.</param>
    /// <returns>This instance for chaining.</returns>
    public partial ref ITextDrawBase SetBackgroundColour(Colour colour);
    private partial void GetBackgroundColour(out Colour colour);

    /// <summary>
    /// Gets the background colour of the textdraw.
    /// </summary>
    /// <returns>The background colour.</returns>
    public Colour GetBackgroundColour()
    {
        GetBackgroundColour(out var result);
        return result;
    }

    /// <summary>
    /// Sets the drawing style of the textdraw.
    /// </summary>
    /// <param name="style">The drawing style.</param>
    /// <returns>This instance for chaining.</returns>
    public partial ref ITextDrawBase SetStyle(TextDrawStyle style);

    /// <summary>
    /// Gets the drawing style of the textdraw.
    /// </summary>
    /// <returns>The drawing style.</returns>
    public partial TextDrawStyle GetStyle();

    /// <summary>
    /// Sets whether the textdraw is proportional.
    /// </summary>
    /// <param name="proportional">Whether the textdraw is proportional. <see langword="true" /> for proportional, <see langword="false" /> otherwise.</param>
    /// <returns>This instance for chaining.</returns>
    public partial ref ITextDrawBase SetProportional(bool proportional);

    /// <summary>
    /// Gets whether the textdraw is proportional.
    /// </summary>
    /// <returns><see langword="true" /> if the textdraw is proportional; otherwise, <see langword="false" />.</returns>
    public partial bool IsProportional();

    /// <summary>
    /// Sets whether the textdraw is selectable.
    /// </summary>
    /// <param name="selectable">Whether the textdraw is selectable. <see langword="true" /> for selectable, <see langword="false" /> otherwise.</param>
    /// <returns>This instance for chaining.</returns>
    public partial ref ITextDrawBase SetSelectable(bool selectable);

    /// <summary>
    /// Gets whether the textdraw is selectable.
    /// </summary>
    /// <returns><see langword="true" /> if the textdraw is selectable; otherwise, <see langword="false" />.</returns>
    public partial bool IsSelectable();

    /// <summary>
    /// Sets the preview model for the textdraw.
    /// </summary>
    /// <param name="model">The model ID to preview.</param>
    /// <returns>This instance for chaining.</returns>
    public partial ref ITextDrawBase SetPreviewModel(int model);

    /// <summary>
    /// Gets the preview model for the textdraw.
    /// </summary>
    /// <returns>The model ID being previewed.</returns>
    public partial int GetPreviewModel();

    /// <summary>
    /// Sets the preview rotation for the textdraw.
    /// </summary>
    /// <param name="rotation">The rotation vector.</param>
    /// <returns>This instance for chaining.</returns>
    public partial ref ITextDrawBase SetPreviewRotation(Vector3 rotation);

    /// <summary>
    /// Gets the preview rotation for the textdraw.
    /// </summary>
    /// <returns>The rotation vector.</returns>
    public partial Vector3 GetPreviewRotation();

    /// <summary>
    /// Sets the preview vehicle colours for the textdraw.
    /// </summary>
    /// <param name="colour1">The first vehicle colour.</param>
    /// <param name="colour2">The second vehicle colour.</param>
    /// <returns>This instance for chaining.</returns>
    public partial ref ITextDrawBase SetPreviewVehicleColour(int colour1, int colour2);
    private partial void GetPreviewVehicleColour(out Pair<int, int> result);

    /// <summary>
    /// Gets the preview vehicle colours for the textdraw.
    /// </summary>
    /// <returns>A tuple containing the two vehicle colours.</returns>
    public (int, int) GetPreviewVehicleColour()
    {
        GetPreviewVehicleColour(out var result);
        return result;
    }

    /// <summary>
    /// Sets the preview zoom factor for the textdraw.
    /// </summary>
    /// <param name="zoom">The zoom factor.</param>
    /// <returns>This instance for chaining.</returns>
    public partial ref ITextDrawBase SetPreviewZoom(float zoom);

    /// <summary>
    /// Gets the preview zoom factor for the textdraw.
    /// </summary>
    /// <returns>The preview zoom factor.</returns>
    public partial float GetPreviewZoom();

    /// <summary>
    /// Restreams the textdraw. TODO: Add more details.
    /// </summary>
    public partial void Restream();
}