// SampSharp
// Copyright 2022 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a component which provides the data and functionality of a textdraw.
/// </summary>
public sealed class TextDraw : Component
{
    private TextDrawAlignment _alignment = TextDrawAlignment.Left;
    private Color _backColor = Color.Black;
    private Color _boxColor = Color.Transparent;
    private TextDrawFont _font = TextDrawFont.Normal;
    private Color _foreColor = Color.White;
    private Vector2 _letterSize = Vector2.One;
    private int _outline;
    private int _previewModel;
    private bool _proportional = true;
    private bool _selectable;
    private int _shadow;
    private string _text;
    private Vector2 _textSize;
    private bool _useBox;

    private TextDraw(Vector2 position, string text)
    {
        Position = position;
        _text = text;
    }

    /// <summary>
    /// Gets or sets the size of the letters of this textdraw.
    /// </summary>
    public Vector2 LetterSize
    {
        get => _letterSize;
        set
        {
            GetComponent<NativeTextDraw>().TextDrawLetterSize(value.X, value.Y);
            _letterSize = value;
        }
    }

    /// <summary>
    /// Gets or sets the size of this textdraw box and click-able area.
    /// </summary>
    public Vector2 TextSize
    {
        get => _textSize;
        set
        {
            GetComponent<NativeTextDraw>().TextDrawTextSize(value.X, value.Y);
            _textSize = value;
        }
    }

    /// <summary>
    /// Gets or sets the alignment of this textdraw.
    /// </summary>
    public TextDrawAlignment Alignment
    {
        get => _alignment;
        set
        {
            GetComponent<NativeTextDraw>().TextDrawAlignment((int) value);
            _alignment = value;
        }
    }

    /// <summary>
    /// Gets or sets the color of the text of this textdraw.
    /// </summary>
    public Color ForeColor
    {
        get => _foreColor;
        set
        {
            GetComponent<NativeTextDraw>().TextDrawColor(value);
            _foreColor = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether a box is used for this textdraw.
    /// </summary>
    public bool UseBox
    {
        get => _useBox;
        set
        {
            GetComponent<NativeTextDraw>().TextDrawUseBox(value);
            _useBox = value;
        }
    }

    /// <summary>
    /// Gets or sets the color of the box of this textdraw.
    /// </summary>
    public Color BoxColor
    {
        get => _boxColor;
        set
        {
            GetComponent<NativeTextDraw>().TextDrawBoxColor(value);
            _boxColor = value;
        }
    }

    /// <summary>
    /// Gets or sets the shadow size of this textdraw.
    /// </summary>
    public int Shadow
    {
        get => _shadow;
        set
        {
            GetComponent<NativeTextDraw>().TextDrawSetShadow(value);
            _shadow = value;
        }
    }

    /// <summary>
    /// Gets or sets the outline size of this textdraw.
    /// </summary>
    public int Outline
    {
        get => _outline;
        set
        {
            GetComponent<NativeTextDraw>().TextDrawSetOutline(value);
            _outline = value;
        }
    }

    /// <summary>
    /// Gets or sets the background color of this textdraw.
    /// </summary>
    public Color BackColor
    {
        get => _backColor;
        set
        {
            GetComponent<NativeTextDraw>().TextDrawBackgroundColor(value);
            _backColor = value;
        }
    }

    /// <summary>
    /// Gets or sets the font of this textdraw.
    /// </summary>
    public TextDrawFont Font
    {
        get => _font;
        set
        {
            GetComponent<NativeTextDraw>().TextDrawFont((int) value);
            _font = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the font of this textdraw is rendered as a monospaced font.
    /// </summary>
    public bool Proportional
    {
        get => _proportional;
        set
        {
            GetComponent<NativeTextDraw>().TextDrawSetProportional(value);
            _proportional = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this textdraw is selectable by the player.
    /// </summary>
    public bool Selectable
    {
        get => _selectable;
        set
        {
            GetComponent<NativeTextDraw>().TextDrawSetSelectable(value);
            _selectable = value;
        }
    }

    /// <summary>
    /// Gets or sets the text of this textdraw.
    /// </summary>
    public string Text
    {
        get => _text;
        set
        {
            GetComponent<NativeTextDraw>().TextDrawSetString(string.IsNullOrEmpty(value) ? "_" : value);
            _text = value;
        }
    }

    /// <summary>
    /// Gets or sets the preview model of this textdraw.
    /// </summary>
    public int PreviewModel
    {
        get => _previewModel;
        set
        {
            GetComponent<NativeTextDraw>().TextDrawSetPreviewModel(value);
            _previewModel = value;
        }
    }

    /// <summary>
    /// Gets the position of this textdraw.
    /// </summary>
    public Vector2 Position { get; }

    /// <summary>
    /// Sets the preview object rotation and zoom of this textdraw.
    /// </summary>
    /// <param name="rotation">The rotation of the preview object.</param>
    /// <param name="zoom">The zoom of the preview object.</param>
    public void SetPreviewRotation(Vector3 rotation, float zoom = 1.0f)
    {
        GetComponent<NativeTextDraw>().TextDrawSetPreviewRot(rotation.X, rotation.Y, rotation.Z, zoom);
    }

    /// <summary>
    /// Sets the color of the preview vehicle of this textdraw.
    /// </summary>
    /// <param name="color1">The primary color of the vehicle.</param>
    /// <param name="color2">The secondary color of the vehicle.</param>
    public void SetPreviewVehicleColor(int color1, int color2)
    {
        GetComponent<NativeTextDraw>().TextDrawSetPreviewVehCol(color1, color2);
    }

    /// <summary>
    /// Shows this textdraw for all connected players.
    /// </summary>
    public void Show()
    {
        GetComponent<NativeTextDraw>().TextDrawShowForAll();
    }
        
    /// <summary>
    /// Shows this textdraw for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The player to show this textdraw to.</param>
    public void Show(EntityId player)
    {
        if (!player.IsOfType(SampEntities.PlayerType))
            throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

        GetComponent<NativeTextDraw>().TextDrawShowForPlayer(player);
    }

    /// <summary>
    /// Hides this textdraw for all connected players.
    /// </summary>
    public void Hide()
    {
        GetComponent<NativeTextDraw>().TextDrawHideForAll();
    }
        
    /// <summary>
    /// Hides this textdraw for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The player to show this textdraw to.</param>
    public void Hide(EntityId player)
    {
        if (!player.IsOfType(SampEntities.PlayerType))
            throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

        GetComponent<NativeTextDraw>().TextDrawHideForPlayer(player);
    }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        GetComponent<NativeTextDraw>().TextDrawDestroy();
    }
}