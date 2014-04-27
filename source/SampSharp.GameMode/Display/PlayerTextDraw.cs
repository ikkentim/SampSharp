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
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Display
{
    /// <summary>
    ///     Represents a player-textdraw.
    /// </summary>
    public class PlayerTextDraw : InstanceKeeper<PlayerTextDraw>, IIdentifyable, IDisposable
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
        /// <param name="id">The ID of the textdraw.</param>
        public PlayerTextDraw(int id) : this(Player.Find(id/(Limits.MaxTextDraws + 1)), id%(Limits.MaxTextDraws + 1))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerTextDraw" /> class.
        /// </summary>
        /// <param name="player">The <see cref="Player" /> whose textdraw it is.</param>
        /// <param name="id">The id of the player-textdraw.</param>
        public PlayerTextDraw(Player player, int id)
        {
            Player = player;
            TextDrawId = id;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerTextDraw" /> class.
        /// </summary>
        /// <param name="player">The owner of the player-textdraw.</param>
        /// <param name="x">The x-position of the player-textdraw on the screen.</param>
        /// <param name="y">The y-position of the player-textdraw on the screen.</param>
        /// <param name="text">The text of the player-textdraw.</param>
        public PlayerTextDraw(Player player, float x, float y, string text)
        {
            TextDrawId = -1;
            Player = player;
            X = x;
            Y = y;
            Text = text;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerTextDraw" /> class.
        /// </summary>
        /// <param name="player">The owner of the player-textdraw.</param>
        /// <param name="x">The x-position of the player-textdraw on the screen.</param>
        /// <param name="y">The y-position of the player-textdraw on the screen.</param>
        /// <param name="text">The text of the player-textdraw.</param>
        /// <param name="font">The <see cref="TextDrawFont" /> of this textdraw.</param>
        public PlayerTextDraw(Player player, float x, float y, string text, TextDrawFont font)
            : this(player, x, y, text)
        {
            Font = font;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerTextDraw" /> class.
        /// </summary>
        /// <param name="player">The owner of the player-textdraw.</param>
        /// <param name="x">The x-position of the player-textdraw on the screen.</param>
        /// <param name="y">The y-position of the player-textdraw on the screen.</param>
        /// <param name="text">The text of the player-textdraw.</param>
        /// <param name="font">The <see cref="TextDrawFont" /> of the player-textdraw.</param>
        /// <param name="foreColor">The foreground <see cref="Color" /> of the player-textdraw.</param>
        public PlayerTextDraw(Player player, float x, float y, string text, TextDrawFont font, Color foreColor)
            : this(player, x, y, text, font)
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawAlignment(Player.Id, TextDrawId, (int) value);
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawBackgroundColor(Player.Id, TextDrawId, value);
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawColor(Player.Id, TextDrawId, value);
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawBoxColor(Player.Id, TextDrawId, value);
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawFont(Player.Id, TextDrawId, (int) value);
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawLetterSize(Player.Id, TextDrawId, _letterWidth, _letterHeight);
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawLetterSize(Player.Id, TextDrawId, _letterWidth, _letterHeight);
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawSetOutline(Player.Id, TextDrawId, value);
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawSetProportional(Player.Id, TextDrawId, value);
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawSetShadow(Player.Id, TextDrawId, value);
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawSetString(Player.Id, TextDrawId, value);
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
                if (TextDrawId == -1) return;
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
                if (TextDrawId == -1) return;
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawTextSize(Player.Id, TextDrawId, _width, _height);
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawTextSize(Player.Id, TextDrawId, _width, _height);
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawUseBox(Player.Id, TextDrawId, value);
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawSetSelectable(Player.Id, TextDrawId, value);
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawSetPreviewModel(Player.Id, TextDrawId, value);
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawSetPreviewRot(Player.Id, TextDrawId, value.X, value.Y, value.Z, PreviewZoom);
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawSetPreviewRot(Player.Id, TextDrawId, PreviewRotation.X, PreviewRotation.Y,
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawSetPreviewVehCol(Player.Id, TextDrawId, _previewPrimaryColor,
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
                if (TextDrawId == -1) return;
                Native.PlayerTextDrawSetPreviewVehCol(Player.Id, TextDrawId, _previewPrimaryColor,
                    _previewSecondaryColor);
                Update();
            }
        }

        /// <summary>
        ///     Gets the textdraw-id of this player-textdraw.
        /// </summary>
        public virtual int TextDrawId { get; protected set; }

        /// <summary>
        ///     Gets the owner of this player-textdraw.
        /// </summary>
        public virtual Player Player { get; protected set; }

        /// <summary>
        ///     Gets the id of this player-textdraw.
        /// </summary>
        public virtual int Id
        {
            get { return Player.Id*(Limits.MaxTextDraws + 1) + TextDrawId; }
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerClickPlayerTextDraw" /> is being called.
        ///     This callback is called when a player clicks on a player-textdraw.
        /// </summary>
        public event PlayerClickTextDrawHandler Click;

        #endregion

        #region Methods

        /// <summary>
        ///     Destroys this player-textdraw and removes it from the known instances list.
        /// </summary>
        public override void Dispose()
        {
            if (TextDrawId == -1) return;
            Native.PlayerTextDrawDestroy(Player.Id, TextDrawId);

            base.Dispose();
        }

        /// <summary>
        ///     Displays this player-textdraw to the <see cref="Player" /> of this textdraw.
        /// </summary>
        public virtual void Show()
        {
            if (TextDrawId == -1) Refresh();
            _visible = true;
            Native.PlayerTextDrawShow(Player.Id, TextDrawId);
        }

        /// <summary>
        ///     Hides this player-textdraw.
        /// </summary>
        public virtual void Hide()
        {
            if (TextDrawId == -1 && _visible) return;
            _visible = false;
            Native.PlayerTextDrawHide(Player.Id, TextDrawId);
        }

        /// <summary>
        ///     Recreates this player-textdraw with all set properties. Called when changing the location on the screen.
        /// </summary>
        protected virtual void Refresh()
        {
            Hide();

            if (TextDrawId != -1) Native.PlayerTextDrawDestroy(Player.Id, TextDrawId);
            TextDrawId = Native.CreatePlayerTextDraw(Player.Id, X, Y, Text);

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

        /// <summary>
        ///     Finds an instance with the given <paramref name="id" /> and <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player of the instance to find</param>
        /// <param name="id">The identity of the instance to find.</param>
        /// <returns>The found instance.</returns>
        public static PlayerTextDraw Find(Player player, int id)
        {
            return FindExisting(player.Id*(Limits.MaxTextDraws + 1) + id);
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