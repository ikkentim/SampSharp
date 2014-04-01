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
using GameMode.Definitions;
using GameMode.World;

namespace GameMode.Display
{
    public class TextDraw : IIdentifyable, IDisposable
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
                SetAlignment(value);
            }
        }

        public virtual Color BackColor
        {
            get { return _backColor; }
            set
            {
                _backColor = value;
                SetBackColor(value);
            }
        }

        public virtual Color ForeColor
        {
            get { return _foreColor; }
            set
            {
                _foreColor = value;
                SetForeColor(value);
            }
        }

        public virtual Color BoxColor
        {
            get { return _boxColor; }
            set
            {
                _boxColor = value;
                SetBoxColor(value);
            }
        }

        public virtual TextDrawFont Font
        {
            get { return _font; }
            set
            {
                _font = value;
                SetFont(value);
            }
        }

        public virtual float LetterWidth
        {
            get { return _letterWidth; }
            set
            {
                _letterWidth = value;
                SetLetterSize(value, _letterHeight);
            }
        }

        public virtual float LetterHeight
        {
            get { return _letterHeight; }
            set
            {
                _letterHeight = value;
                SetLetterSize(_letterWidth, value);
            }
        }

        public virtual int Outline
        {
            get { return _outline; }
            set
            {
                _outline = value;
                SetOutline(value);
            }
        }

        public virtual bool Proportional
        {
            get { return _proportional; }
            set
            {
                _proportional = value;
                SetProportional(value);
            }
        }

        public virtual int Shadow
        {
            get { return _shadow; }
            set
            {
                _shadow = value;
                SetShadow(value);
            }
        }

        public virtual string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                SetText(value);
            }
        }

        public virtual float X
        {
            get { return _x; }
            set
            {
                _x = value;
                if (Id < 0) return;
                Prepare();
            }
        }

        public virtual float Y
        {
            get { return _y; }
            set
            {
                _y = value;
                if (Id < 0) return;
                Prepare();
            }
        }

        public virtual float Width
        {
            get { return _width; }
            set
            {
                _width = value;
                SetSize(value, _height);
            }
        }

        public virtual float Height
        {
            get { return _height; }
            set
            {
                _height = value;
                SetSize(_width, value);
            }
        }

        public virtual bool UseBox
        {
            get { return _useBox; }
            set
            {
                _useBox = value;
                SetUseBox(value);
            }
        }

        public virtual bool Selectable
        {
            get { return _selectable; }
            set
            {
                _selectable = value;
                SetSelectable(value);
            }
        }

        public virtual int PreviewModel
        {
            get { return _previewModel; }
            set
            {
                _previewModel = value;
                SetPreviewModel(value);
            }
        }

        public virtual Vector PreviewRotation
        {
            get { return _previewRotation; }
            set
            {
                _previewRotation = value;
                SetPreviewRotation(value, _previewZoom);
            }
        }

        public virtual float PreviewZoom
        {
            get { return _previewZoom; }
            set
            {
                _previewZoom = value;
                SetPreviewRotation(_previewRotation, value);
            }
        }

        public virtual int PreviewPrimaryColor
        {
            get { return _previewPrimaryColor; }
            set
            {
                _previewPrimaryColor = value;
                SetPreviewVehicleColors(_previewPrimaryColor, _previewSecondaryColor);
            }
        }

        public virtual int PreviewSecondaryColor
        {
            get { return _previewSecondaryColor; }
            set
            {
                _previewSecondaryColor = value;
                SetPreviewVehicleColors(_previewPrimaryColor, _previewSecondaryColor);
            }
        }

        public virtual int Id { get; protected set; }

        #endregion

        #region Constructors

        public TextDraw()
        {
            Id = -1;
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

        public TextDraw(float x, float y, string text, TextDrawFont font, Color foreColor, float letterWidth,
            float letterHeight) : this(x, y, text, font, foreColor)
        {
            LetterWidth = letterWidth;
            LetterHeight = letterHeight;
        }

        public TextDraw(float x, float y, string text, TextDrawFont font, Color foreColor, float letterWidth,
            float letterHeight, float width, float height)
            : this(x, y, text, font, foreColor, letterWidth, letterHeight)
        {
            Width = width;
            Height = height;
        }

        public TextDraw(float x, float y, string text, TextDrawFont font, Color foreColor, float letterWidth,
            float letterHeight, float width, float height, TextDrawAlignment alignment)
            : this(x, y, text, font, foreColor, letterWidth, letterHeight, width, height)
        {
            Alignment = alignment;
        }

        public TextDraw(float x, float y, string text, TextDrawFont font, Color foreColor, float letterWidth,
            float letterHeight, float width, float height, TextDrawAlignment alignment, int shadow)
            : this(x, y, text, font, foreColor, letterWidth, letterHeight, width, height, alignment)
        {
            Shadow = shadow;
        }

        public TextDraw(float x, float y, string text, TextDrawFont font, Color foreColor, float letterWidth,
            float letterHeight, float width, float height, TextDrawAlignment alignment, int shadow, int outline)
            : this(x, y, text, font, foreColor, letterWidth, letterHeight, width, height, alignment, shadow)
        {
            Outline = outline;
        }

        public TextDraw(float x, float y, string text, TextDrawFont font, Color foreColor, float letterWidth,
            float letterHeight, float width, float height, TextDrawAlignment alignment, int shadow, int outline,
            Color backColor)
            : this(x, y, text, font, foreColor, letterWidth, letterHeight, width, height, alignment, shadow, outline)
        {
            BackColor = backColor;
        }

        public TextDraw(float x, float y, string text, TextDrawFont font, Color foreColor, float letterWidth,
            float letterHeight, float width, float height, TextDrawAlignment alignment, int shadow, int outline,
            Color backColor, bool proportional)
            : this(
                x, y, text, font, foreColor, letterWidth, letterHeight, width, height, alignment, shadow, outline,
                backColor)
        {
            Proportional = proportional;
        }

        #endregion

        #region Methods

        public virtual void Dispose()
        {
            if (Id < 0) return;
            Native.TextDrawDestroy(Id);
        }

        public virtual void Show()
        {
            if (Id == -1)
                Prepare();

            Native.TextDrawShowForAll(Id);
        }

        public virtual void Show(Player player)
        {
            if (Id == -1)
                Prepare();
            if (player != null)
                Native.TextDrawShowForPlayer(player.Id, Id);
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

        protected virtual void Create()
        {
            Id = Native.TextDrawCreate(X, Y, Text);
        }

        protected virtual void Prepare()
        {
            Create();

            if (Alignment != default(TextDrawAlignment))
                SetAlignment(Alignment);
            if (BackColor != 0)
                SetBackColor(BackColor);
            if (ForeColor != 0)
                SetForeColor(ForeColor);
            if (BoxColor != 0)
                SetBoxColor(BoxColor);
            if (Font != default(TextDrawFont))
                SetFont(Font);
            if (LetterWidth != 0 || LetterHeight != 0)
                SetLetterSize(LetterWidth, LetterHeight);
            if (Outline > 0)
                SetOutline(Outline);
            if (Proportional)
                SetProportional(Proportional);
            if (Shadow > 0)
                SetShadow(Shadow);
            if (Width != 0 || Height != 0)
                SetSize(Width, Height);
            if (UseBox)
                SetUseBox(UseBox);
            if (Selectable)
                SetSelectable(Selectable);
            if (PreviewModel != 0)
                SetPreviewModel(PreviewModel);
            if (PreviewRotation.X != 0 || PreviewRotation.Y != 0 || PreviewRotation.Z != 0 || PreviewZoom != 1)
                SetPreviewRotation(PreviewRotation, PreviewZoom);
            if (PreviewPrimaryColor == -1 && PreviewSecondaryColor == -1)
                SetPreviewVehicleColors(PreviewPrimaryColor, PreviewSecondaryColor);
        }

        protected virtual void UpdatePlayers()
        {
            //TODO: Check what happens after Show() or Show() Hide(Player), does it still show for newcomers?
            //Then update the TD for these players here.
        }

        protected virtual void SetAlignment(TextDrawAlignment alignment)
        {
            if (Id < 0) return;
            Native.TextDrawAlignment(Id, (int) alignment);
            UpdatePlayers();
        }

        protected virtual void SetText(string text)
        {
            if (Id < 0) return;
            Native.TextDrawSetString(Id, text);
            UpdatePlayers();
        }

        protected virtual void SetBackColor(Color color)
        {
            if (Id < 0) return;
            Native.TextDrawBackgroundColor(Id, color);
            UpdatePlayers();
        }

        protected virtual void SetForeColor(Color color)
        {
            if (Id < 0) return;
            Native.TextDrawColor(Id, color);
            UpdatePlayers();
        }

        protected virtual void SetBoxColor(Color color)
        {
            if (Id < 0) return;
            Native.TextDrawBoxColor(Id, color);
            UpdatePlayers();
        }

        protected virtual void SetFont(TextDrawFont font)
        {
            if (Id < 0) return;
            Native.TextDrawFont(Id, (int) font);
            UpdatePlayers();
        }

        protected virtual void SetLetterSize(float width, float height)
        {
            if (Id < 0) return;
            Native.TextDrawLetterSize(Id, width, height);
            UpdatePlayers();
        }

        protected virtual void SetOutline(int size)
        {
            if (Id < 0) return;
            Native.TextDrawSetOutline(Id, size);
            UpdatePlayers();
        }

        protected virtual void SetProportional(bool proportional)
        {
            if (Id < 0) return;
            Native.TextDrawSetProportional(Id, proportional);
            UpdatePlayers();
        }

        protected virtual void SetShadow(int shadow)
        {
            if (Id < 0) return;
            Native.TextDrawSetShadow(Id, shadow);
            UpdatePlayers();
        }

        protected virtual void SetSize(float width, float height)
        {
            if (Id < 0) return;
            Native.TextDrawTextSize(Id, width, height);
            UpdatePlayers();
        }

        protected virtual void SetUseBox(bool useBox)
        {
            if (Id < 0) return;
            Native.TextDrawUseBox(Id, useBox);
            UpdatePlayers();
        }

        protected virtual void SetSelectable(bool selectable)
        {
            if (Id < 0) return;
            Native.TextDrawSetSelectable(Id, selectable);
            UpdatePlayers();
        }

        protected virtual void SetPreviewModel(int model)
        {
            if (Id < 0) return;
            Native.TextDrawSetPreviewModel(Id, model);
            UpdatePlayers();
        }

        protected virtual void SetPreviewRotation(Vector rotation, float zoom)
        {
            if (Id < 0) return;
            Native.TextDrawSetPreviewRot(Id, rotation.X, rotation.Y, rotation.Z, zoom);
            UpdatePlayers();
        }

        protected virtual void SetPreviewVehicleColors(int primaryColor, int secondaryColor)
        {
            if (Id < 0) return;
            Native.TextDrawSetPreviewVehCol(Id, primaryColor, secondaryColor);
            UpdatePlayers();
        }

        #endregion
    }
}