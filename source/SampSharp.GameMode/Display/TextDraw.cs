// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using System.Collections.Generic;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Display
{
    public class TextDraw : IdentifiedPool<TextDraw>, IIdentifyable
    {
        #region Fields

        /// <summary>
        ///     Gets an ID commonly returned by methods to point out that no textdraw matched the requirements.
        /// </summary>
        public const int InvalidId = Misc.InvalidTextDraw;

        private readonly List<Player> _playersShownTo = new List<Player>();

        private TextDrawAlignment _alignment;
        private Color _backColor;
        private Color _boxColor;
        private TextDrawFont _font;
        private Color _foreColor;
        private float _height;
        private float _letterHeight;
        private float _letterWidth;
        private int _outline;
        private int _previewModel;
        private int _previewPrimaryColor = -1;
        private Vector _previewRotation;
        private int _previewSecondaryColor = -1;
        private float _previewZoom = 1;
        private bool _proportional;
        private bool _selectable;
        private int _shadow;
        private string _text;
        private bool _useBox;
        private float _width;
        private float _x;
        private float _y;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextDraw" /> class.
        /// </summary>
        public TextDraw()
        {
            Id = -1;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextDraw" /> class.
        /// </summary>
        /// <param name="id">The ID of the textdraw.</param>
        public TextDraw(int id)
        {
            Id = id;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextDraw" /> class.
        /// </summary>
        /// <param name="x">The x-position of the textdraw on the screen.</param>
        /// <param name="y">The y-position of the textdraw on the screen.</param>
        /// <param name="text">The text of the textdraw.</param>
        public TextDraw(float x, float y, string text) : this()
        {
            X = x;
            Y = y;
            Text = text;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextDraw" /> class.
        /// </summary>
        /// <param name="x">The x-position of the textdraw on the screen.</param>
        /// <param name="y">The y-position of the textdraw on the screen.</param>
        /// <param name="text">The text of the textdraw.</param>
        /// <param name="font">The <see cref="TextDrawFont" /> of the textdraw.</param>
        public TextDraw(float x, float y, string text, TextDrawFont font) : this(x, y, text)
        {
            Font = font;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextDraw" /> class.
        /// </summary>
        /// <param name="x">The x-position of the textdraw on the screen.</param>
        /// <param name="y">The y-position of the textdraw on the screen.</param>
        /// <param name="text">The text of the textdraw.</param>
        /// <param name="font">The <see cref="TextDrawFont" /> of the textdraw.</param>
        /// <param name="foreColor">The foreground <see cref="Color" /> of the textdraw.</param>
        public TextDraw(float x, float y, string text, TextDrawFont font, Color foreColor) : this(x, y, text, font)
        {
            ForeColor = foreColor;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the <see cref="TextDrawAlignment" /> of this textdraw.
        /// </summary>
        public virtual TextDrawAlignment Alignment
        {
            get { return _alignment; }
            set
            {
                _alignment = value;
                if (Id == -1) return;
                Native.TextDrawAlignment(Id, (int) value);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets or sets the background <see cref="Color" /> of this textdraw.
        /// </summary>
        public virtual Color BackColor
        {
            get { return _backColor; }
            set
            {
                _backColor = value;
                if (Id == -1) return;
                Native.TextDrawBackgroundColor(Id, value);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets or sets the foreground <see cref="Color" /> of this textdraw.
        /// </summary>
        public virtual Color ForeColor
        {
            get { return _foreColor; }
            set
            {
                _foreColor = value;
                if (Id == -1) return;
                Native.TextDrawColor(Id, value);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets or sets the box <see cref="Color" /> of this textdraw.
        /// </summary>
        public virtual Color BoxColor
        {
            get { return _boxColor; }
            set
            {
                _boxColor = value;
                if (Id == -1) return;
                Native.TextDrawBoxColor(Id, value);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets or sets the <see cref="TextDrawFont" /> to use in this textdraw.
        /// </summary>
        public virtual TextDrawFont Font
        {
            get { return _font; }
            set
            {
                _font = value;
                if (Id == -1) return;
                Native.TextDrawFont(Id, (int) value);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets or sets the letter-width of this textdraw.
        /// </summary>
        public virtual float LetterWidth
        {
            get { return _letterWidth; }
            set
            {
                _letterWidth = value;
                if (Id == -1) return;
                Native.TextDrawLetterSize(Id, _letterWidth, _letterHeight);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets or sets the letter-height of this textdraw.
        /// </summary>
        public virtual float LetterHeight
        {
            get { return _letterHeight; }
            set
            {
                _letterHeight = value;
                if (Id == -1) return;
                Native.TextDrawLetterSize(Id, _letterWidth, _letterHeight);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets or sets the outline size of this textdraw.
        /// </summary>
        public virtual int Outline
        {
            get { return _outline; }
            set
            {
                _outline = value;
                if (Id == -1) return;
                Native.TextDrawSetOutline(Id, value);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets or sets wheter proporionally space the characters of this textdraw.
        /// </summary>
        public virtual bool Proportional
        {
            get { return _proportional; }
            set
            {
                _proportional = value;
                if (Id == -1) return;
                Native.TextDrawSetProportional(Id, value);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets or sets the shadow-size of this textdraw.
        /// </summary>
        public virtual int Shadow
        {
            get { return _shadow; }
            set
            {
                _shadow = value;
                if (Id == -1) return;
                Native.TextDrawSetShadow(Id, value);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets or sets the text of this textdraw.
        /// </summary>
        public virtual string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                if (Id == -1) return;
                Native.TextDrawSetString(Id, value);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets or sets the x-position of this textdraw on the screen.
        /// </summary>
        public virtual float X
        {
            get { return _x; }
            set
            {
                _x = value;
                if (Id == -1) return;
                Refresh();
            }
        }

        /// <summary>
        ///     Gets or sets the y-position of this textdraw on the screen.
        /// </summary>
        public virtual float Y
        {
            get { return _y; }
            set
            {
                _y = value;
                if (Id == -1) return;
                Refresh();
            }
        }

        /// <summary>
        ///     Gets or sets the width of this textdraw's box.
        /// </summary>
        public virtual float Width
        {
            get { return _width; }
            set
            {
                _width = value;
                if (Id == -1) return;
                Native.TextDrawTextSize(Id, _width, _height);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets or sets the height of this textdraw's box.
        /// </summary>
        public virtual float Height
        {
            get { return _height; }
            set
            {
                _height = value;
                if (Id == -1) return;
                Native.TextDrawTextSize(Id, _width, _height);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets or sets whether to draw a box behind the textdraw.
        /// </summary>
        public virtual bool UseBox
        {
            get { return _useBox; }
            set
            {
                _useBox = value;
                if (Id == -1) return;
                Native.TextDrawUseBox(Id, value);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets or sets whether this textdraw is selectable.
        /// </summary>
        public virtual bool Selectable
        {
            get { return _selectable; }
            set
            {
                _selectable = value;
                if (Id == -1) return;
                Native.TextDrawSetSelectable(Id, value);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets or sets the previewmodel to draw on this textdraw.
        /// </summary>
        public virtual int PreviewModel
        {
            get { return _previewModel; }
            set
            {
                _previewModel = value;
                if (Id == -1) return;
                Native.TextDrawSetPreviewModel(Id, value);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets or sets the rotation of this textdraw's previewmodel.
        /// </summary>
        public virtual Vector PreviewRotation
        {
            get { return _previewRotation; }
            set
            {
                _previewRotation = value;
                if (Id == -1) return;
                Native.TextDrawSetPreviewRot(Id, value.X, value.Y, value.Z, PreviewZoom);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets or sets the zoom level of this textdraw's previewmodel.
        /// </summary>
        public virtual float PreviewZoom
        {
            get { return _previewZoom; }
            set
            {
                _previewZoom = value;
                if (Id == -1) return;
                Native.TextDrawSetPreviewRot(Id, PreviewRotation.X, PreviewRotation.Y, PreviewRotation.Z, value);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets or sets the primary vehicle color of this textdraw's previewmodel.
        /// </summary>
        public virtual int PreviewPrimaryColor
        {
            get { return _previewPrimaryColor; }
            set
            {
                _previewPrimaryColor = value;
                if (Id == -1) return;
                Native.TextDrawSetPreviewVehCol(Id, _previewPrimaryColor, _previewSecondaryColor);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets or sets the secondary vehicle color of this textdraw's previewmodel.
        /// </summary>
        public virtual int PreviewSecondaryColor
        {
            get { return _previewSecondaryColor; }
            set
            {
                _previewSecondaryColor = value;
                if (Id == -1) return;
                Native.TextDrawSetPreviewVehCol(Id, _previewPrimaryColor, _previewSecondaryColor);
                UpdateClients();
            }
        }

        /// <summary>
        ///     Gets the id of this textdraw.
        /// </summary>
        public virtual int Id { get; protected set; }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerClickTextDraw" /> is being called.
        ///     This callback is called when a player clicks on a textdraw or cancels the select mode(ESC).
        /// </summary>
        public event PlayerClickTextDrawHandler Click;

        #endregion

        #region Methods

        /// <summary>
        ///     Destroys this textdraw and removes it from the known instances list.
        /// </summary>
        public override void Dispose()
        {
            if (Id == -1) return;

            _playersShownTo.Clear();
            Native.TextDrawDestroy(Id);

            base.Dispose();
        }

        /// <summary>
        ///     Displays this textdraw to all players.
        /// </summary>
        public virtual void Show()
        {
            if (Id == -1) Refresh();
            _playersShownTo.Clear();
            _playersShownTo.AddRange(Player.All);
            Native.TextDrawShowForAll(Id);
        }

        /// <summary>
        ///     Display this textdraw to the given <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player to display this textdraw to.</param>
        public virtual void Show(Player player)
        {
            if (Id == -1) Refresh();
            if (player != null)
            {
                if (!_playersShownTo.Contains(player))
                    _playersShownTo.Add(player);
                Native.TextDrawShowForPlayer(player.Id, Id);
            }
        }

        /// <summary>
        ///     Hides this textdraw.
        /// </summary>
        public virtual void Hide()
        {
            if (Id == -1) return;
            _playersShownTo.Clear();
            Native.TextDrawHideForAll(Id);
        }

        /// <summary>
        ///     Hides this textdraw for the given <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player to hide this textdraw from.</param>
        public virtual void Hide(Player player)
        {
            if (Id == -1 || player == null) return;
            _playersShownTo.Remove(player);
            Native.TextDrawHideForPlayer(player.Id, Id);
        }

        /// <summary>
        ///     Recreates this textdraw with all set properties. Called when changing the location on the screen.
        /// </summary>
        protected virtual void Refresh()
        {
            if (Id != -1) Native.TextDrawDestroy(Id);
            Id = Native.TextDrawCreate(X, Y, Text);

            //Reset properties
            if (Alignment != default(TextDrawAlignment)) Alignment = Alignment;
            if (BackColor != 0) BackColor = BackColor;
            if (ForeColor != 0) ForeColor = ForeColor;
            if (BoxColor != 0) BoxColor = BoxColor;
            if (Font != default(TextDrawFont)) Font = Font;
            if (LetterWidth != 0) LetterWidth = LetterWidth;
            if (LetterHeight != 0) LetterHeight = LetterHeight;
            if (Outline > 0) Outline = Outline;
            if (Proportional) Proportional = Proportional;
            if (Shadow > 0) Shadow = Shadow;
            if (Width != 0) Width = Width;
            if (Height != 0) Height = Height;
            if (UseBox) UseBox = UseBox;
            if (Selectable) Selectable = Selectable;
            if (PreviewModel != 0) PreviewModel = PreviewModel;
            if (PreviewRotation != Vector.Zero) PreviewRotation = PreviewRotation;
            if (PreviewZoom != 1) PreviewZoom = PreviewZoom;
            if (PreviewPrimaryColor != -1) PreviewPrimaryColor = PreviewPrimaryColor;
            if (PreviewSecondaryColor != -1) PreviewSecondaryColor = PreviewSecondaryColor;

            UpdateClients();
        }

        /// <summary>
        ///     Updates this textdraw on all client's screens.
        /// </summary>
        protected virtual void UpdateClients()
        {
            foreach (Player p in _playersShownTo.AsReadOnly())
                Show(p);
        }

        #endregion

        #region Event raisers

        /// <summary>
        ///     Raises the <see cref="Click" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerClickTextDrawEventArgs" /> that contains the event data. </param>
        public virtual void OnClick(PlayerClickTextDrawEventArgs e)
        {
            if (Click != null)
                Click(this, e);
        }

        #endregion
    }
}