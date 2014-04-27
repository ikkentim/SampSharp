// SampSharp
// Copyright (C) 04 Tim Potze
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

        #region Properties

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

        public virtual float X
        {
            get { return _x; }
            set
            {
                _x = value;
                if (TextDrawId == -1) return;
                Prepare();
            }
        }

        public virtual float Y
        {
            get { return _y; }
            set
            {
                _y = value;
                if (TextDrawId == -1) return;
                Prepare();
            }
        }

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

        public virtual int TextDrawId { get; protected set; }

        public virtual Player Player { get; protected set; }

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

        #region Constructors

        public PlayerTextDraw(int id) : this(Player.Find(id/(Limits.MaxTextDraws + 1)), id%(Limits.MaxTextDraws + 1))
        {
        }

        public PlayerTextDraw(Player player, int id)
        {
            Player = player;
            TextDrawId = id;
        }

        public PlayerTextDraw(Player player, float x, float y, string text)
        {
            TextDrawId = -1;
            Player = player;
            X = x;
            Y = y;
            Text = text;
        }

        public PlayerTextDraw(Player player, float x, float y, string text, TextDrawFont font)
            : this(player, x, y, text)
        {
            Font = font;
        }

        public PlayerTextDraw(Player player, float x, float y, string text, TextDrawFont font, Color foreColor)
            : this(player, x, y, text, font)
        {
            ForeColor = foreColor;
        }

        #endregion

        #region Methods

        public override void Dispose()
        {
            if (TextDrawId == -1) return;
            Native.PlayerTextDrawDestroy(Player.Id, TextDrawId);

            base.Dispose();
        }

        public virtual void Show()
        {
            if (TextDrawId == -1) Prepare();
            _visible = true;
            Native.PlayerTextDrawShow(Player.Id, TextDrawId);
        }

        public virtual void Hide()
        {
            if (TextDrawId == -1) return;
            _visible = false;
            Native.PlayerTextDrawHide(Player.Id, TextDrawId);
        }

        protected virtual void Prepare()
        {
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

        protected virtual void Update()
        {
            if (_visible) Show();
        }

        public static PlayerTextDraw Find(Player player, int id)
        {
            return Find(player.Id*(Limits.MaxTextDraws + 1) + id);
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