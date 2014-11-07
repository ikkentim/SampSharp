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

using System;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Display
{
    /// <summary>
    ///     Represents a player-textdraw.
    /// </summary>
    public class PlayerTextDraw : IdentifiedOwnedPool<PlayerTextDraw>, IIdentifiable, IOwnable<GtaPlayer>
    {
        #region Fields

        /// <summary>
        ///     Gets an ID commonly returned by methods to point out that no textdraw matched the requirements.
        /// </summary>
        public const int InvalidId = Misc.InvalidTextDraw;

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
        private bool _visible;
        private float _width;
        private float _x;
        private float _y;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerTextDraw" /> class.
        /// </summary>
        /// <param name="owner">The <see cref="Owner" /> whose textdraw it is.</param>
        /// <param name="id">The id of the player-textdraw.</param>
        public PlayerTextDraw(GtaPlayer owner, int id)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            Owner = owner;
            Id = id;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerTextDraw" /> class.
        /// </summary>
        /// <param name="owner">The owner of the player-textdraw.</param>
        /// <param name="x">The x-position of the player-textdraw on the screen.</param>
        /// <param name="y">The y-position of the player-textdraw on the screen.</param>
        /// <param name="text">The text of the player-textdraw.</param>
        public PlayerTextDraw(GtaPlayer owner, float x, float y, string text)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            Id = -1;
            Owner = owner;
            X = x;
            Y = y;
            Text = text;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerTextDraw" /> class.
        /// </summary>
        /// <param name="owner">The owner of the player-textdraw.</param>
        /// <param name="x">The x-position of the player-textdraw on the screen.</param>
        /// <param name="y">The y-position of the player-textdraw on the screen.</param>
        /// <param name="text">The text of the player-textdraw.</param>
        /// <param name="font">The <see cref="TextDrawFont" /> of this textdraw.</param>
        public PlayerTextDraw(GtaPlayer owner, float x, float y, string text, TextDrawFont font)
            : this(owner, x, y, text)
        {
            Font = font;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerTextDraw" /> class.
        /// </summary>
        /// <param name="owner">The owner of the player-textdraw.</param>
        /// <param name="x">The x-position of the player-textdraw on the screen.</param>
        /// <param name="y">The y-position of the player-textdraw on the screen.</param>
        /// <param name="text">The text of the player-textdraw.</param>
        /// <param name="font">The <see cref="TextDrawFont" /> of the player-textdraw.</param>
        /// <param name="foreColor">The foreground <see cref="Color" /> of the player-textdraw.</param>
        public PlayerTextDraw(GtaPlayer owner, float x, float y, string text, TextDrawFont font, Color foreColor)
            : this(owner, x, y, text, font)
        {
            ForeColor = foreColor;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the <see cref="TextDrawAlignment" /> of this player-textdraw.
        /// </summary>
        public virtual TextDrawAlignment Alignment
        {
            get { return _alignment; }
            set
            {
                _alignment = value;
                if (Id == -1) return;
                Native.PlayerTextDrawAlignment(Owner.Id, Id, (int) value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the background <see cref="Color" /> of this player-textdraw.
        /// </summary>
        public virtual Color BackColor
        {
            get { return _backColor; }
            set
            {
                _backColor = value;
                if (Id == -1) return;
                Native.PlayerTextDrawBackgroundColor(Owner.Id, Id, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the foreground <see cref="Color" /> of this player-textdraw.
        /// </summary>
        public virtual Color ForeColor
        {
            get { return _foreColor; }
            set
            {
                _foreColor = value;
                if (Id == -1) return;
                Native.PlayerTextDrawColor(Owner.Id, Id, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the box <see cref="Color" /> of this player-textdraw.
        /// </summary>
        public virtual Color BoxColor
        {
            get { return _boxColor; }
            set
            {
                _boxColor = value;
                if (Id == -1) return;
                Native.PlayerTextDrawBoxColor(Owner.Id, Id, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the <see cref="TextDrawFont" /> to use in this player-textdraw.
        /// </summary>
        public virtual TextDrawFont Font
        {
            get { return _font; }
            set
            {
                _font = value;
                if (Id == -1) return;
                Native.PlayerTextDrawFont(Owner.Id, Id, (int) value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the letter-width of this player-textdraw.
        /// </summary>
        public virtual float LetterWidth
        {
            get { return _letterWidth; }
            set
            {
                _letterWidth = value;
                if (Id == -1) return;
                Native.PlayerTextDrawLetterSize(Owner.Id, Id, _letterWidth, _letterHeight);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the letter-height of this player-textdraw.
        /// </summary>
        public virtual float LetterHeight
        {
            get { return _letterHeight; }
            set
            {
                _letterHeight = value;
                if (Id == -1) return;
                Native.PlayerTextDrawLetterSize(Owner.Id, Id, _letterWidth, _letterHeight);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the outline size of this player-textdraw.
        /// </summary>
        public virtual int Outline
        {
            get { return _outline; }
            set
            {
                _outline = value;
                if (Id == -1) return;
                Native.PlayerTextDrawSetOutline(Owner.Id, Id, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets wheter proporionally space the characters of this player-textdraw.
        /// </summary>
        public virtual bool Proportional
        {
            get { return _proportional; }
            set
            {
                _proportional = value;
                if (Id == -1) return;
                Native.PlayerTextDrawSetProportional(Owner.Id, Id, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the shadow-size of this player-textdraw.
        /// </summary>
        public virtual int Shadow
        {
            get { return _shadow; }
            set
            {
                _shadow = value;
                if (Id == -1) return;
                Native.PlayerTextDrawSetShadow(Owner.Id, Id, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the text of this player-textdraw.
        /// </summary>
        public virtual string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                if (Id == -1) return;
                Native.PlayerTextDrawSetString(Owner.Id, Id, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the x-position of this player-textdraw on the screen.
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
        ///     Gets or sets the y-position of this player-textdraw on the screen.
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
        ///     Gets or sets the width of this player-textdraw's box.
        /// </summary>
        public virtual float Width
        {
            get { return _width; }
            set
            {
                _width = value;
                if (Id == -1) return;
                Native.PlayerTextDrawTextSize(Owner.Id, Id, _width, _height);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the height of this player-textdraw's box.
        /// </summary>
        public virtual float Height
        {
            get { return _height; }
            set
            {
                _height = value;
                if (Id == -1) return;
                Native.PlayerTextDrawTextSize(Owner.Id, Id, _width, _height);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets whether to draw a box behind the player-textdraw.
        /// </summary>
        public virtual bool UseBox
        {
            get { return _useBox; }
            set
            {
                _useBox = value;
                if (Id == -1) return;
                Native.PlayerTextDrawUseBox(Owner.Id, Id, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets whether this player-textdraw is selectable.
        /// </summary>
        public virtual bool Selectable
        {
            get { return _selectable; }
            set
            {
                _selectable = value;
                if (Id == -1) return;
                Native.PlayerTextDrawSetSelectable(Owner.Id, Id, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the previewmodel to draw on this player-textdraw.
        /// </summary>
        public virtual int PreviewModel
        {
            get { return _previewModel; }
            set
            {
                _previewModel = value;
                if (Id == -1) return;
                Native.PlayerTextDrawSetPreviewModel(Owner.Id, Id, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the rotation of this player-textdraw's previewmodel.
        /// </summary>
        public virtual Vector PreviewRotation
        {
            get { return _previewRotation; }
            set
            {
                _previewRotation = value;
                if (Id == -1) return;
                Native.PlayerTextDrawSetPreviewRot(Owner.Id, Id, value.X, value.Y, value.Z, PreviewZoom);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the zoom level of this player-textdraw's previewmodel.
        /// </summary>
        public virtual float PreviewZoom
        {
            get { return _previewZoom; }
            set
            {
                _previewZoom = value;
                if (Id == -1) return;
                Native.PlayerTextDrawSetPreviewRot(Owner.Id, Id, PreviewRotation.X, PreviewRotation.Y,
                    PreviewRotation.Z, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the primary vehicle color of this player-textdraw's previewmodel.
        /// </summary>
        public virtual int PreviewPrimaryColor
        {
            get { return _previewPrimaryColor; }
            set
            {
                _previewPrimaryColor = value;
                if (Id == -1) return;
                Native.PlayerTextDrawSetPreviewVehCol(Owner.Id, Id, _previewPrimaryColor,
                    _previewSecondaryColor);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the secondary vehicle color of this player-textdraw's previewmodel.
        /// </summary>
        public virtual int PreviewSecondaryColor
        {
            get { return _previewSecondaryColor; }
            set
            {
                _previewSecondaryColor = value;
                if (Id == -1) return;
                Native.PlayerTextDrawSetPreviewVehCol(Owner.Id, Id, _previewPrimaryColor,
                    _previewSecondaryColor);
                Update();
            }
        }

        /// <summary>
        ///     Gets the textdraw-id of this player-textdraw.
        /// </summary>
        public virtual int Id { get; protected set; }

        /// <summary>
        ///     Gets the owner of this player-textdraw.
        /// </summary>
        public virtual GtaPlayer Owner { get; protected set; }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerClickPlayerTextDraw" /> is being called.
        ///     This callback is called when a player clicks on a player-textdraw.
        /// </summary>
        public event EventHandler<PlayerClickTextDrawEventArgs> Click;

        #endregion

        #region Methods

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (Id == -1) return;

            Native.PlayerTextDrawDestroy(Owner.Id, Id);
        }

        /// <summary>
        ///     Displays this player-textdraw to the <see cref="Owner" /> of this textdraw.
        /// </summary>
        public virtual void Show()
        {
            CheckDisposure();

            if (Id == -1) Refresh();
            _visible = true;

            Native.PlayerTextDrawShow(Owner.Id, Id);
        }

        /// <summary>
        ///     Hides this player-textdraw.
        /// </summary>
        public virtual void Hide()
        {
            CheckDisposure();

            if (Id == -1 || !_visible) return;
            _visible = false;

            Native.PlayerTextDrawHide(Owner.Id, Id);
        }

        /// <summary>
        ///     Recreates this player-textdraw with all set properties. Called when changing the location on the screen.
        /// </summary>
        protected virtual void Refresh()
        {
            Hide();

            if (Id != -1) Native.PlayerTextDrawDestroy(Owner.Id, Id);
            Id = Native.CreatePlayerTextDraw(Owner.Id, X, Y, Text);

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

            Update();
        }

        /// <summary>
        ///     Updates this textdraw on the client's screen.
        /// </summary>
        protected virtual void Update()
        {
            if (_visible) Show();
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