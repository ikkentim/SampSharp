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

using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Display
{
    public class TextDraw : InstanceKeeper<TextDraw>, IIdentifyable
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
                if (Id == -1) return;
                Native.TextDrawAlignment(Id, (int) value);
                UpdatePlayers();
            }
        }

        public virtual Color BackColor
        {
            get { return _backColor; }
            set
            {
                _backColor = value;
                if (Id == -1) return;
                Native.TextDrawBackgroundColor(Id, value);
                UpdatePlayers();
            }
        }

        public virtual Color ForeColor
        {
            get { return _foreColor; }
            set
            {
                _foreColor = value;
                if (Id == -1) return;
                Native.TextDrawColor(Id, value);
                UpdatePlayers();
            }
        }

        public virtual Color BoxColor
        {
            get { return _boxColor; }
            set
            {
                _boxColor = value;
                if (Id == -1) return;
                Native.TextDrawBoxColor(Id, value);
                UpdatePlayers();
            }
        }

        public virtual TextDrawFont Font
        {
            get { return _font; }
            set
            {
                _font = value;
                if (Id == -1) return;
                Native.TextDrawFont(Id, (int) value);
                UpdatePlayers();
            }
        }

        public virtual float LetterWidth
        {
            get { return _letterWidth; }
            set
            {
                _letterWidth = value;
                if (Id == -1) return;
                Native.TextDrawLetterSize(Id, _letterWidth, _letterHeight);
                UpdatePlayers();
            }
        }

        public virtual float LetterHeight
        {
            get { return _letterHeight; }
            set
            {
                _letterHeight = value;
                if (Id == -1) return;
                Native.TextDrawLetterSize(Id, _letterWidth, _letterHeight);
                UpdatePlayers();
            }
        }

        public virtual int Outline
        {
            get { return _outline; }
            set
            {
                _outline = value;
                if (Id == -1) return;
                Native.TextDrawSetOutline(Id, value);
                UpdatePlayers();
            }
        }

        public virtual bool Proportional
        {
            get { return _proportional; }
            set
            {
                _proportional = value;
                if (Id == -1) return;
                Native.TextDrawSetProportional(Id, value);
                UpdatePlayers();
            }
        }

        public virtual int Shadow
        {
            get { return _shadow; }
            set
            {
                _shadow = value;
                if (Id == -1) return;
                Native.TextDrawSetShadow(Id, value);
                UpdatePlayers();
            }
        }

        public virtual string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                if (Id == -1) return;
                Native.TextDrawSetString(Id, value);
                UpdatePlayers();
            }
        }

        public virtual float X
        {
            get { return _x; }
            set
            {
                _x = value;
                if (Id == -1) return;
                Prepare();
            }
        }

        public virtual float Y
        {
            get { return _y; }
            set
            {
                _y = value;
                if (Id == -1) return;
                Prepare();
            }
        }

        public virtual float Width
        {
            get { return _width; }
            set
            {
                _width = value;
                if (Id == -1) return;
                Native.TextDrawTextSize(Id, _width, _height);
                UpdatePlayers();
            }
        }

        public virtual float Height
        {
            get { return _height; }
            set
            {
                _height = value;
                if (Id == -1) return;
                Native.TextDrawTextSize(Id, _width, _height);
                UpdatePlayers();
            }
        }

        public virtual bool UseBox
        {
            get { return _useBox; }
            set
            {
                _useBox = value;
                if (Id == -1) return;
                Native.TextDrawUseBox(Id, value);
                UpdatePlayers();
            }
        }

        public virtual bool Selectable
        {
            get { return _selectable; }
            set
            {
                _selectable = value;
                if (Id == -1) return;
                Native.TextDrawSetSelectable(Id, value);
                UpdatePlayers();
            }
        }

        public virtual int PreviewModel
        {
            get { return _previewModel; }
            set
            {
                _previewModel = value;
                if (Id == -1) return;
                Native.TextDrawSetPreviewModel(Id, value);
                UpdatePlayers();
            }
        }

        public virtual Vector PreviewRotation
        {
            get { return _previewRotation; }
            set
            {
                _previewRotation = value;
                if (Id == -1) return;
                Native.TextDrawSetPreviewRot(Id, value.X, value.Y, value.Z, PreviewZoom);
                UpdatePlayers();
            }
        }

        public virtual float PreviewZoom
        {
            get { return _previewZoom; }
            set
            {
                _previewZoom = value;
                if (Id == -1) return;
                Native.TextDrawSetPreviewRot(Id, PreviewRotation.X, PreviewRotation.Y, PreviewRotation.Z, value);
                UpdatePlayers();
            }
        }

        public virtual int PreviewPrimaryColor
        {
            get { return _previewPrimaryColor; }
            set
            {
                _previewPrimaryColor = value;
                if (Id == -1) return;
                Native.TextDrawSetPreviewVehCol(Id, _previewPrimaryColor, _previewSecondaryColor);
                UpdatePlayers();
            }
        }

        public virtual int PreviewSecondaryColor
        {
            get { return _previewSecondaryColor; }
            set
            {
                _previewSecondaryColor = value;
                if (Id == -1) return;
                Native.TextDrawSetPreviewVehCol(Id, _previewPrimaryColor, _previewSecondaryColor);
                UpdatePlayers();
            }
        }

        public virtual int Id { get; protected set; }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerClickTextDraw" /> is being called.
        ///     This callback is called when a player clicks on a textdraw or cancels the select mode(ESC).
        /// </summary>
        public event PlayerClickTextDrawHandler Click;

        #endregion

        #region Constructors

        public TextDraw()
        {
            Id = -1;
        }

        public TextDraw(int id)
        {
            Id = id;
        }

        public TextDraw(float x, float y, string text) : this()
        {
            X = x;
            Y = y;
            Text = text;
        }

        public TextDraw(float x, float y, string text, TextDrawFont font) : this(x, y, text)
        {
            Font = font;
        }

        public TextDraw(float x, float y, string text, TextDrawFont font, Color foreColor) : this(x, y, text, font)
        {
            ForeColor = foreColor;
        }

        #endregion

        #region Methods

        public virtual void Show()
        {
            if (Id == -1) Prepare();
            Native.TextDrawShowForAll(Id);
        }

        public virtual void Show(Player player)
        {
            if (Id == -1) Prepare();
            if (player != null) Native.TextDrawShowForPlayer(player.Id, Id);
        }

        public virtual void Hide()
        {
            if (Id == -1) return;
            Native.TextDrawHideForAll(Id);
        }

        public virtual void Hide(Player player)
        {
            if (Id == -1 || player == null) return;
            Native.TextDrawHideForPlayer(player.Id, Id);
        }

        protected virtual void Prepare()
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

            UpdatePlayers();
        }

        protected virtual void UpdatePlayers()
        {
            //TODO: Check what happens after Show() or Show() Hide(Player), does it still show for newcomers?
            //Then update the TD for these players here.
        }

        public override void Dispose()
        {
            if (Id == -1) return;
            Native.TextDrawDestroy(Id);

            base.Dispose();
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