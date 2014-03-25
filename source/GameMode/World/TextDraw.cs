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
        private Rotation _previewRotation;
        private float _previewZoom = 1;
        private int _previewPrimaryColor = -1;
        private int _previewSecondaryColor = -1;

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
                if (TextDrawId < 0) return;
                Prepare();
            }
        }

        public virtual float Y
        {
            get { return _y; }
            set
            {
                _y = value;
                if (TextDrawId < 0) return;
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

        public virtual Rotation PreviewRotation
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

        public virtual int TextDrawId
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
            : this(x, y, text, font, foreColor, letterWidth, letterHeight, width, height, alignment, shadow, outline, backColor)
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

        public virtual void Dispose()
        {
            Native.TextDrawDestroy(TextDrawId);
        }

        protected virtual void Create()
        {
            
            TextDrawId = Native.TextDrawCreate(X, Y, Text);
        }

        protected virtual void Prepare()
        {
            Create();

            if (_alignment != default(TextDrawAlignment))
                SetAlignment(_alignment);
            if (_backColor != 0)
                SetBackColor(_backColor);
            if (_foreColor != 0)
                SetForeColor(_foreColor);
            if (_boxColor != 0)
                SetBoxColor(_boxColor);
            if (_font != default(TextDrawFont))
                SetFont(_font);
            if (_letterWidth != 0 || _letterHeight != 0)
                SetLetterSize(_letterWidth, _letterHeight);
            if (_outline > 0)
                SetOutline(_outline);
            if (_proportional)
                SetProportional(_proportional);
            if (_shadow > 0)
                SetShadow(_shadow);
            if (_width != 0 || _height != 0)
                SetSize(_width, _height);
            if (_useBox)
                SetUseBox(_useBox);
            if (_selectable)
                SetSelectable(_selectable);
            if (_previewModel != 0)
                SetPreviewModel(_previewModel);
            if (_previewRotation.X != 0 || _previewRotation.Y != 0 || _previewRotation.Z != 0 || _previewZoom != 1)
                SetPreviewRotation(_previewRotation, _previewZoom);
            if (_previewPrimaryColor == -1 && _previewSecondaryColor == -1)
                SetPreviewVehicleColors(_previewPrimaryColor, _previewSecondaryColor);
        }

        protected virtual void UpdatePlayers()
        {
            //TODO: Check what happens after Show() or Show() Hide(Player), does it still show for newcomers?
            //Then update the TD for these players here.
        }

        protected virtual void SetAlignment(TextDrawAlignment alignment)
        {
            if (TextDrawId < 0) return;
            Native.TextDrawAlignment(TextDrawId, (int)alignment);
            UpdatePlayers();
        }

        protected virtual void SetText(string text)
        {
            if (TextDrawId < 0) return;
            Native.TextDrawSetString(TextDrawId, text);
            UpdatePlayers();
        }

        protected virtual void SetBackColor(Color color)
        {
            if (TextDrawId < 0) return;
            Native.TextDrawBackgroundColor(TextDrawId, color);
            UpdatePlayers();
        }

        protected virtual void SetForeColor(Color color)
        {
            if (TextDrawId < 0) return;
            Native.TextDrawColor(TextDrawId, color);
            UpdatePlayers();
        }

        protected virtual void SetBoxColor(Color color)
        {
            if (TextDrawId < 0) return;
            Native.TextDrawBoxColor(TextDrawId, color);
            UpdatePlayers();
        }

        protected virtual void SetFont(TextDrawFont font)
        {
            if (TextDrawId < 0) return;
            Native.TextDrawFont(TextDrawId, (int)font);
            UpdatePlayers();
        }

        protected virtual void SetLetterSize(float width, float height)
        {
            if (TextDrawId < 0) return;
            Native.TextDrawLetterSize(TextDrawId, width, height);
            UpdatePlayers();
        }

        protected virtual void SetOutline(int size)
        {
            if (TextDrawId < 0) return;
            Native.TextDrawSetOutline(TextDrawId, size);
            UpdatePlayers();
        }

        protected virtual void SetProportional(bool proportional)
        {
            if (TextDrawId < 0) return;
            Native.TextDrawSetProportional(TextDrawId, proportional);
            UpdatePlayers(); 
        }

        protected virtual void SetShadow(int shadow)
        {
            if (TextDrawId < 0) return;
            Native.TextDrawSetShadow(TextDrawId, shadow);
            UpdatePlayers();
        }

        protected virtual void SetSize(float width, float height)
        {
            if (TextDrawId < 0) return;
            Native.TextDrawTextSize(TextDrawId, width, height);
            UpdatePlayers();
        }

        public virtual void SetUseBox(bool useBox)
        {
            if (TextDrawId < 0) return;
            Native.TextDrawUseBox(TextDrawId, useBox);
            UpdatePlayers();
        }

        protected virtual void SetSelectable(bool selectable)
        {
            if (TextDrawId < 0) return;
            Native.TextDrawSetSelectable(TextDrawId, selectable);
            UpdatePlayers(); 
        }

        protected virtual void SetPreviewModel(int model)
        {
            if (TextDrawId < 0) return;
            Native.TextDrawSetPreviewModel(TextDrawId, model);
            UpdatePlayers(); 
        }

        protected virtual void SetPreviewRotation(Rotation rotation, float zoom)
        {
            if (TextDrawId < 0) return;
            Native.TextDrawSetPreviewRot(TextDrawId, rotation.X, rotation.Y, rotation.Z, zoom);
            UpdatePlayers();
        }

        protected virtual void SetPreviewVehicleColors(int primaryColor, int secondaryColor)
        {
            if (TextDrawId < 0) return;
            Native.TextDrawSetPreviewVehCol(TextDrawId, primaryColor, secondaryColor);
            UpdatePlayers(); 
        }
        #endregion

        
    }
}
