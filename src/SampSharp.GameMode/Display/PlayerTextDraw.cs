// SampSharp
// Copyright 2017 Tim Potze
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
using System;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Helpers;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Display
{
    /// <summary>
    ///     Represents a player-textdraw.
    /// </summary>
    public partial class PlayerTextDraw : IdentifiedOwnedPool<PlayerTextDraw, BasePlayer>
    {
        /// <summary>
        ///     Identifier indicating the handle is invalid.
        /// </summary>
        public const int InvalidId = 0xFFFF;

        /// <summary>
        ///     Maximum number of player text draws which can exist.
        /// </summary>
        public const int Max = 256;

        #region Events

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerClickPlayerTextDraw(BasePlayer,ClickPlayerTextDrawEventArgs)" /> is
        ///     being called.
        ///     This callback is called when a player clicks on a player-textdraw.
        /// </summary>
        public event EventHandler<ClickPlayerTextDrawEventArgs> Click;

        #endregion

        #region Event raisers

        /// <summary>
        ///     Raises the <see cref="Click" /> event.
        /// </summary>
        /// <param name="e">An <see cref="ClickPlayerTextDrawEventArgs" /> that contains the event data. </param>
        public virtual void OnClick(ClickPlayerTextDrawEventArgs e)
        {
            Click?.Invoke(this, e);
        }

        #endregion

        #region Fields

        private TextDrawAlignment? _alignment;
        private Color? _backColor;
        private Color? _boxColor;
        private TextDrawFont? _font;
        private Color? _foreColor;
        private float? _height;
        private Vector2? _letterSize;
        private int? _outline;
        private Vector2 _position;
        private int? _previewModel;
        private int? _previewPrimaryColor;
        private Vector3? _previewRotation;
        private int? _previewSecondaryColor;
        private float? _previewZoom;
        private bool? _proportional;
        private bool? _selectable;
        private int? _shadow;
        private string _text;
        private bool? _useBox;
        private bool _visible;
        private float? _width;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerTextDraw" /> class.
        /// </summary>
        /// <param name="owner">The owner of the player-textdraw.</param>
        public PlayerTextDraw(BasePlayer owner)
        {
            if (owner == null)
                throw new ArgumentNullException(nameof(owner));

            IsApplyFixes = true;
            AutoDestroy = true;

            Owner = owner;
            _text = "_";
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerTextDraw" /> class.
        /// </summary>
        /// <param name="owner">The owner of the player-textdraw.</param>
        /// <param name="position">The position of the player-textdraw on the screen.</param>
        /// <param name="text">The text of the player-textdraw.</param>
        public PlayerTextDraw(BasePlayer owner, Vector2 position, string text) : this(owner)
        {
            _position = position;
            _text = text;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerTextDraw" /> class.
        /// </summary>
        /// <param name="owner">The owner of the player-textdraw.</param>
        /// <param name="position">The position of the player-textdraw on the screen.</param>
        /// <param name="text">The text of the player-textdraw.</param>
        /// <param name="font">The <see cref="TextDrawFont" /> of this textdraw.</param>
        public PlayerTextDraw(BasePlayer owner, Vector2 position, string text, TextDrawFont font)
            : this(owner, position, text)
        {
            _font = font;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerTextDraw" /> class.
        /// </summary>
        /// <param name="owner">The owner of the player-textdraw.</param>
        /// <param name="position">The position of the player-textdraw on the screen.</param>
        /// <param name="text">The text of the player-textdraw.</param>
        /// <param name="font">The <see cref="TextDrawFont" /> of the player-textdraw.</param>
        /// <param name="foreColor">The foreground <see cref="Color" /> of the player-textdraw.</param>
        public PlayerTextDraw(BasePlayer owner, Vector2 position, string text, TextDrawFont font, Color foreColor)
            : this(owner, position, text, font)
        {
            _foreColor = foreColor;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets whether SA-MP fixes should be applied.
        /// </summary>
        public bool IsApplyFixes { get; set; }

        /// <summary>
        ///     Gets or sets whether the textdraw should automatically be destroyed when hidden.
        /// </summary>
        /// <remarks>The textdraw will automatically be recreated once .Show is called.</remarks>
        public bool AutoDestroy { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="TextDrawAlignment" /> of this player-textdraw.
        /// </summary>
        public virtual TextDrawAlignment Alignment
        {
            get { return _alignment ?? TextDrawAlignment.Left; }
            set
            {
                _alignment = value;
                if (Id == -1) return;
                PlayerTextDrawInternal.Instance.PlayerTextDrawAlignment(Owner.Id, Id, (int) value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the background <see cref="Color" /> of this player-textdraw.
        /// </summary>
        public virtual Color BackColor
        {
            get { return _backColor ?? Color.Black; }
            set
            {
                _backColor = value;
                if (Id == -1) return;
                PlayerTextDrawInternal.Instance.PlayerTextDrawBackgroundColor(Owner.Id, Id, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the foreground <see cref="Color" /> of this player-textdraw.
        /// </summary>
        public virtual Color ForeColor
        {
            get { return _foreColor ?? Color.White; }
            set
            {
                _foreColor = value;
                if (Id == -1) return;
                PlayerTextDrawInternal.Instance.PlayerTextDrawColor(Owner.Id, Id, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the box <see cref="Color" /> of this player-textdraw.
        /// </summary>
        public virtual Color BoxColor
        {
            get { return _boxColor ?? Color.Transparent; }
            set
            {
                _boxColor = value;
                if (Id == -1) return;
                PlayerTextDrawInternal.Instance.PlayerTextDrawBoxColor(Owner.Id, Id, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the <see cref="TextDrawFont" /> to use in this player-textdraw.
        /// </summary>
        public virtual TextDrawFont Font
        {
            get { return _font ?? TextDrawFont.Normal; }
            set
            {
                _font = value;
                if (Id == -1) return;
                PlayerTextDrawInternal.Instance.PlayerTextDrawFont(Owner.Id, Id, (int) value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the size of the letters of this player-textdraw.
        /// </summary>
        public virtual Vector2 LetterSize
        {
            get { return _letterSize ?? Vector2.Zero; }
            set
            {
                _letterSize = value;
                if (Id == -1) return;
                PlayerTextDrawInternal.Instance.PlayerTextDrawLetterSize(Owner.Id, Id, value.X, value.Y);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the outline size of this player-textdraw.
        /// </summary>
        public virtual int Outline
        {
            get { return _outline ?? 0; }
            set
            {
                _outline = value;
                if (Id == -1) return;
                PlayerTextDrawInternal.Instance.PlayerTextDrawSetOutline(Owner.Id, Id, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets whether to proporionally space the characters of this player-textdraw.
        /// </summary>
        public virtual bool Proportional
        {
            get { return _proportional ?? false; }
            set
            {
                _proportional = value;
                if (Id == -1) return;
                PlayerTextDrawInternal.Instance.PlayerTextDrawSetProportional(Owner.Id, Id, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the shadow-size of this player-textdraw.
        /// </summary>
        public virtual int Shadow
        {
            get { return _shadow ?? 0; }
            set
            {
                _shadow = value;
                if (Id == -1) return;
                PlayerTextDrawInternal.Instance.PlayerTextDrawSetShadow(Owner.Id, Id, value);
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
                PlayerTextDrawInternal.Instance.PlayerTextDrawSetString(Owner.Id, Id, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the position of this player-textdraw on the screen.
        /// </summary>
        public virtual Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if (Id == -1) return;
                Refresh();
            }
        }

        /// <summary>
        ///     Gets or sets the width of this player-textdraw's box.
        /// </summary>
        public virtual float Width
        {
            get { return _width ?? 0; }
            set
            {
                _width = value;
                if (Id == -1) return;
                PlayerTextDrawInternal.Instance.PlayerTextDrawTextSize(Owner.Id, Id, value, Height);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the height of this player-textdraw's box.
        /// </summary>
        public virtual float Height
        {
            get { return _height ?? 0; }
            set
            {
                _height = value;
                if (Id == -1) return;
                PlayerTextDrawInternal.Instance.PlayerTextDrawTextSize(Owner.Id, Id, Width, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets whether to draw a box behind the player-textdraw.
        /// </summary>
        public virtual bool UseBox
        {
            get { return _useBox ?? false; }
            set
            {
                _useBox = value;
                if (Id == -1) return;
                PlayerTextDrawInternal.Instance.PlayerTextDrawUseBox(Owner.Id, Id, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets whether this player-textdraw is selectable.
        /// </summary>
        public virtual bool Selectable
        {
            get { return _selectable ?? false; }
            set
            {
                _selectable = value;
                if (Id == -1) return;
                PlayerTextDrawInternal.Instance.PlayerTextDrawSetSelectable(Owner.Id, Id, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the preview model to draw on this player-textdraw.
        /// </summary>
        public virtual int PreviewModel
        {
            get { return _previewModel ?? 0; }
            set
            {
                _previewModel = value;
                if (Id == -1) return;
                PlayerTextDrawInternal.Instance.PlayerTextDrawSetPreviewModel(Owner.Id, Id, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the rotation of this player-textdraw's preview model.
        /// </summary>
        public virtual Vector3 PreviewRotation
        {
            get { return _previewRotation ?? Vector3.Zero; }
            set
            {
                _previewRotation = value;
                if (Id == -1) return;
                PlayerTextDrawInternal.Instance.PlayerTextDrawSetPreviewRot(Owner.Id, Id, value.X, value.Y, value.Z, PreviewZoom);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the zoom level of this player-textdraw's preview model.
        /// </summary>
        public virtual float PreviewZoom
        {
            get { return _previewZoom ?? 0; }
            set
            {
                _previewZoom = value;
                if (Id == -1) return;
                PlayerTextDrawInternal.Instance.PlayerTextDrawSetPreviewRot(Owner.Id, Id, PreviewRotation.X, PreviewRotation.Y,
                    PreviewRotation.Z, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the primary vehicle color of this player-textdraw's preview model.
        /// </summary>
        public virtual int PreviewPrimaryColor
        {
            get { return _previewPrimaryColor ?? -1; }
            set
            {
                _previewPrimaryColor = value;
                if (Id == -1) return;
                PlayerTextDrawInternal.Instance.PlayerTextDrawSetPreviewVehCol(Owner.Id, Id, value, PreviewSecondaryColor);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets the secondary vehicle color of this player-textdraw's preview model.
        /// </summary>
        public virtual int PreviewSecondaryColor
        {
            get { return _previewSecondaryColor ?? -1; }
            set
            {
                _previewSecondaryColor = value;
                if (Id == -1) return;
                PlayerTextDrawInternal.Instance.PlayerTextDrawSetPreviewVehCol(Owner.Id, Id, PreviewPrimaryColor, value);
                Update();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (Id == -1) return;

            PlayerTextDrawInternal.Instance.PlayerTextDrawDestroy(Owner.Id, Id);
        }

        /// <summary>
        ///     Displays this player-textdraw to the <see cref="IdentifiedOwnedPool{TInstance,TOwner}.Owner" /> of this textdraw.
        /// </summary>
        public virtual void Show()
        {
            AssertNotDisposed();

            if (Id == -1) Refresh();
            _visible = true;

            PlayerTextDrawInternal.Instance.PlayerTextDrawShow(Owner.Id, Id);
        }

        /// <summary>
        ///     Hides this player-textdraw.
        /// </summary>
        public virtual void Hide()
        {
            AssertNotDisposed();

            if (Id == -1 || !_visible) return;
            _visible = false;

            PlayerTextDrawInternal.Instance.PlayerTextDrawHide(Owner.Id, Id);

            if (AutoDestroy)
            {
                PlayerTextDrawInternal.Instance.PlayerTextDrawDestroy(Owner.Id, Id);
                Id = -1;
            }
        }
        
        /// <summary>
        ///     Recreates this player-textdraw with all set properties. Called when changing the location on the screen.
        /// </summary>
        protected virtual void Refresh()
        {
            if (Id != -1) PlayerTextDrawInternal.Instance.PlayerTextDrawDestroy(Owner.Id, Id);
            Id = PlayerTextDrawInternal.Instance.CreatePlayerTextDraw(Owner.Id, Position.X, Position.Y, FixString(Text));

            //Reset properties
            _font.Do(x => Font = x);
            _alignment.Do(x => Alignment = x);
            _backColor.Do(x => BackColor = x);
            _foreColor.Do(x => ForeColor = x);
            _boxColor.Do(x => BoxColor = x);
            _letterSize.Do(x => LetterSize = x);
            _outline.Do(x => Outline = x);
            _proportional.Do(x => Proportional = x);
            _shadow.Do(x => Shadow = x);
            _width.Do(x => Width = x);
            _height.Do(x => Height = x);
            _useBox.Do(x => UseBox = x);
            _selectable.Do(x => Selectable = x);
            _previewModel.Do(x => PreviewModel = x);
            _previewRotation.Do(x => PreviewRotation = x);
            _previewZoom.Do(x => PreviewZoom = x);
            _previewPrimaryColor.Do(x => PreviewPrimaryColor = x);
            _previewSecondaryColor.Do(x => PreviewSecondaryColor = x);

            Update();
        }

        /// <summary>
        ///     Fixes a string so no SA-MP bugs will occur during application.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The fixed string</returns>
        protected virtual string FixString(string input)
        {
            if (!IsApplyFixes) return input;

            //TextDraw string may at max. be 1024 char long
            if (input.Length > 1024)
                input = input.Substring(0, 1024);

            //Empty strings can crash the server
            if (string.IsNullOrEmpty(input))
                input = "_";

            return input.Replace("\n", "~n~");
        }

        /// <summary>
        ///     Updates this textdraw on the client's screen.
        /// </summary>
        protected virtual void Update()
        {
            if (_visible) Show();
        }

        #endregion
    }
}