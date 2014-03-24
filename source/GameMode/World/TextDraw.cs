using System;
using System.Collections.Generic;
using System.Linq;
using GameMode.Definitions;

namespace GameMode.World
{
    public class TextDraw : IDisposable
    {

        #region Fields

        private TextDrawAlignment _alignment;
        private Color _backColor;
        private Color _foreColor;
        private Color _boxColor;
        private TextDrawFont _font;
        private float _letterWidth;
        private float _letterHeight;
        private int _outline;
        private bool _proportional;
        private int _shadow;
        private string _text;
        private float _x;
        private float _y;
        private float _width;
        private float _height;
        private bool _useBox;
        private bool _selectable;
        private int _previewModel;
        private float _previewRotationX;
        private float _previewRotationY;
        private float _previewRotationZ;
        private float _previewZoom = 1;
        private int _previewPrimaryColor = -1;
        private int _previewSecondaryColor = -1;

        #endregion

        #region Properties

        public TextDrawAlignment Alignment
        {
            get { return _alignment; }
            set
            {
                _alignment = value;
                if (TextDrawId < 0) return;
                Native.TextDrawAlignment(TextDrawId, (int) value);
                UpdatePlayers();
            }
        }

        public Color BackColor
        {
            get { return _backColor; }
            set
            {
                _backColor = value;
                if (TextDrawId < 0) return;
                Native.TextDrawBackgroundColor(TextDrawId, value);
                UpdatePlayers();
            }
        }

        public Color ForeColor
        {
            get { return _foreColor; }
            set
            {
                _foreColor = value;
                if (TextDrawId < 0) return;
                Native.TextDrawColor(TextDrawId, value);
                UpdatePlayers();
            }
        }

        public Color BoxColor
        {
            get { return _boxColor; }
            set
            {
                _boxColor = value;
                if (TextDrawId < 0) return;
                Native.TextDrawBoxColor(TextDrawId, value);
                UpdatePlayers();
            }
        }

        public TextDrawFont Font
        {
            get { return _font; }
            set
            {
                _font = value;
                if (TextDrawId < 0) return;
                Native.TextDrawFont(TextDrawId, (int) value);
                UpdatePlayers();
            }
        }

        public float LetterWidth
        {
            get { return _letterWidth; }
            set
            {
                _letterWidth = value;
                if (TextDrawId < 0) return;
                Native.TextDrawLetterSize(TextDrawId, value, _letterHeight);
                UpdatePlayers();
            }
        }

        public float LetterHeight
        {
            get { return _letterHeight; }
            set
            {
                _letterHeight = value;
                if (TextDrawId < 0) return;
                Native.TextDrawLetterSize(TextDrawId, _letterWidth, value);
                UpdatePlayers();
            }
        }

        public int Outline
        {
            get { return _outline; }
            set
            {
                _outline = value;
                if (TextDrawId < 0) return;
                Native.TextDrawSetOutline(TextDrawId, value);
                UpdatePlayers();
            }
        }

        public bool Proportional
        {
            get { return _proportional; }
            set
            {
                _proportional = value;
                if (TextDrawId < 0) return;
                Native.TextDrawSetProportional(TextDrawId, value);
                UpdatePlayers();
            }
        }

        public int Shadow
        {
            get { return _shadow; }
            set
            {
                _shadow = value;
                if (TextDrawId < 0) return;
                Native.TextDrawSetShadow(TextDrawId, value);
                UpdatePlayers();
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                if (TextDrawId < 0) return;
                Native.TextDrawSetString(TextDrawId, value);
                UpdatePlayers();
            }
        }

        public float X
        {
            get { return _x; }
            set
            {
                _x = value;
                if (TextDrawId < 0) return;
                Prepare();
            }
        }

        public float Y
        {
            get { return _y; }
            set
            {
                _y = value;
                if (TextDrawId < 0) return;
                Prepare();
            }
        }

        public float Width
        {
            get { return _width; }
            set
            {
                _width = value;
                if (TextDrawId < 0) return;
                Native.TextDrawTextSize(TextDrawId, value, _height);
                UpdatePlayers();
            }
        }

        public float Height
        {
            get { return _height; }
            set
            {
                _height = value;
                if (TextDrawId < 0) return;
                Native.TextDrawTextSize(TextDrawId, _width, value);
                UpdatePlayers();
            }
        }

        public bool UseBox
        {
            get { return _useBox; }
            set
            {
                _useBox = value;
                if (TextDrawId < 0) return;
                Native.TextDrawUseBox(TextDrawId, value);
                UpdatePlayers();
            }
        }

        public bool Selectable
        {
            get { return _selectable; }
            set
            {
                _selectable = value;
                if (TextDrawId < 0) return;
                Native.TextDrawSetSelectable(TextDrawId, value);
                UpdatePlayers();
            }
        }

        public int PreviewModel
        {
            get { return _previewModel; }
            set
            {
                _previewModel = value;
                if (TextDrawId < 0) return;
                Native.TextDrawSetPreviewModel(TextDrawId, value);
                UpdatePlayers();
            }
        }

        public float PreviewRotationX
        {
            get { return _previewRotationX; }
            set
            {
                _previewRotationX = value;
                if (TextDrawId < 0) return;
                Native.TextDrawSetPreviewRot(TextDrawId, _previewRotationX, _previewRotationY, _previewRotationZ,
                    _previewZoom);
                UpdatePlayers();
            }
        }

        public float PreviewRotationY
        {
            get { return _previewRotationY; }
            set
            {
                _previewRotationY = value;
                if (TextDrawId < 0) return;
                Native.TextDrawSetPreviewRot(TextDrawId, _previewRotationX, _previewRotationY, _previewRotationZ,
                    _previewZoom);
                UpdatePlayers();
            }
        }

        public float PreviewRotationZ
        {
            get { return _previewRotationZ; }
            set
            {
                _previewRotationZ = value;
                if (TextDrawId < 0) return;
                Native.TextDrawSetPreviewRot(TextDrawId, _previewRotationX, _previewRotationY, _previewRotationZ,
                    _previewZoom);
                UpdatePlayers();
            }
        }

        public float PreviewZoom
        {
            get { return _previewZoom; }
            set
            {
                _previewZoom = value;
                if (TextDrawId < 0) return;
                Native.TextDrawSetPreviewRot(TextDrawId, _previewRotationX, _previewRotationY, _previewRotationZ,
                    _previewZoom);
                UpdatePlayers();
            }
        }

        public int PreviewPrimaryColor
        {
            get { return _previewPrimaryColor; }
            set
            {
                _previewPrimaryColor = value;
                if (TextDrawId < 0) return;
                Native.TextDrawSetPreviewVehCol(TextDrawId, _previewPrimaryColor, _previewSecondaryColor);
                UpdatePlayers();
            }
        }

        public int PreviewSecondaryColor
        {
            get { return _previewSecondaryColor; }
            set
            {
                _previewSecondaryColor = value;
                if (TextDrawId < 0) return;
                Native.TextDrawSetPreviewVehCol(TextDrawId, _previewPrimaryColor, _previewSecondaryColor);
                UpdatePlayers();
            }
        }

        public int TextDrawId
        {
            get; protected set;
        }

        #endregion

        #region Constructors

        public TextDraw()
        {
            TextDrawId = -1;
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
            : this(x, y, text, font, foreColor, letterWidth, letterHeight, width, height, alignment, shadow, outline)
        {
            Proportional = proportional;
        }

        #endregion

        #region Methods

        public virtual void Show()
        {
            if (TextDrawId == -1)
                Prepare();

            Native.TextDrawShowForAll(TextDrawId);
        }

        public virtual void Show(Player player)
        {
            if (TextDrawId == -1)
                Prepare();
            if (player != null)
                Native.TextDrawShowForPlayer(player.PlayerId, TextDrawId);
        }

        public virtual void Hide()
        {
            if (TextDrawId == -1) return;
            Native.TextDrawHideForAll(TextDrawId);
        }

        public virtual void Hide(Player player)
        {
            if (TextDrawId == -1 || player == null) return;
            Native.TextDrawHideForPlayer(player.PlayerId, TextDrawId);
        }

        public void Dispose()
        {
            Native.TextDrawDestroy(TextDrawId);
        }

        protected virtual void Prepare()
        {
            TextDrawId = Native.TextDrawCreate(X, Y, Text);

            if (_alignment != default(TextDrawAlignment))
                Native.TextDrawAlignment(TextDrawId, (int) _alignment);
            if (_backColor != 0)
                Native.TextDrawBackgroundColor(TextDrawId, _backColor);
            if (_foreColor != 0)
                Native.TextDrawColor(TextDrawId, _foreColor);
            if (_boxColor != 0)
                Native.TextDrawBoxColor(TextDrawId, _boxColor);
            if (_font != default(TextDrawFont))
                Native.TextDrawFont(TextDrawId, (int) _font);
            if (_letterWidth != 0 || _letterHeight != 0)
                Native.TextDrawLetterSize(TextDrawId, _letterWidth, _letterHeight);
            if (_outline > 0)
                Native.TextDrawSetOutline(TextDrawId, _outline);
            if (_proportional)
                Native.TextDrawSetProportional(TextDrawId, _proportional);
            if (_shadow > 0)
                Native.TextDrawSetShadow(TextDrawId, _shadow);
            if (_width != 0 || _height != 0)
                Native.TextDrawTextSize(TextDrawId, _width, _height);
            if (_useBox)
                Native.TextDrawUseBox(TextDrawId, _useBox);
            if (_selectable)
                Native.TextDrawSetSelectable(TextDrawId, _selectable);
            if (_previewModel != 0)
                Native.TextDrawSetPreviewModel(TextDrawId, _previewModel);
            if (_previewRotationX != 0 || _previewRotationY != 0 || _previewRotationZ != 0 || _previewZoom != 1)
                Native.TextDrawSetPreviewRot(TextDrawId, _previewRotationX, _previewRotationY, _previewRotationZ,
                    _previewZoom);
            if (_previewPrimaryColor == -1 && _previewSecondaryColor == -1)
                Native.TextDrawSetPreviewVehCol(TextDrawId, _previewPrimaryColor, _previewSecondaryColor);
        }

        public virtual void UpdatePlayers()
        {
            //TODO: Check what happens after Show() or Show() Hide(Player), does it still show for newcomers?
            //Then update the TD for these players here.
        }
        #endregion

        
    }
}
