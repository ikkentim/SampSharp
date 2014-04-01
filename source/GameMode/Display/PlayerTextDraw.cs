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

using GameMode.Definitions;
using GameMode.World;

namespace GameMode.Display
{
    public class PlayerTextDraw : TextDraw
    {
        public PlayerTextDraw(Player player)
        {
            Player = player;
            Id = -1;
        }

        public PlayerTextDraw(Player player, float x, float y, string text) : this(player)
        {
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

        public PlayerTextDraw(Player player, float x, float y, string text, TextDrawFont font, Color foreColor,
            float letterWidth,
            float letterHeight)
            : this(player, x, y, text, font, foreColor)
        {
            LetterWidth = letterWidth;
            LetterHeight = letterHeight;
        }

        public PlayerTextDraw(Player player, float x, float y, string text, TextDrawFont font, Color foreColor,
            float letterWidth,
            float letterHeight, float width, float height)
            : this(player, x, y, text, font, foreColor, letterWidth, letterHeight)
        {
            Width = width;
            Height = height;
        }

        public PlayerTextDraw(Player player, float x, float y, string text, TextDrawFont font, Color foreColor,
            float letterWidth,
            float letterHeight, float width, float height, TextDrawAlignment alignment)
            : this(player, x, y, text, font, foreColor, letterWidth, letterHeight, width, height)
        {
            Alignment = alignment;
        }

        public PlayerTextDraw(Player player, float x, float y, string text, TextDrawFont font, Color foreColor,
            float letterWidth,
            float letterHeight, float width, float height, TextDrawAlignment alignment, int shadow)
            : this(player, x, y, text, font, foreColor, letterWidth, letterHeight, width, height, alignment)
        {
            Shadow = shadow;
        }

        public PlayerTextDraw(Player player, float x, float y, string text, TextDrawFont font, Color foreColor,
            float letterWidth,
            float letterHeight, float width, float height, TextDrawAlignment alignment, int shadow, int outline)
            : this(player, x, y, text, font, foreColor, letterWidth, letterHeight, width, height, alignment, shadow)
        {
            Outline = outline;
        }

        public PlayerTextDraw(Player player, float x, float y, string text, TextDrawFont font, Color foreColor,
            float letterWidth,
            float letterHeight, float width, float height, TextDrawAlignment alignment, int shadow, int outline,
            Color backColor)
            : this(
                player, x, y, text, font, foreColor, letterWidth, letterHeight, width, height, alignment, shadow,
                outline)
        {
            BackColor = backColor;
        }

        public PlayerTextDraw(Player player, float x, float y, string text, TextDrawFont font, Color foreColor,
            float letterWidth,
            float letterHeight, float width, float height, TextDrawAlignment alignment, int shadow, int outline,
            Color backColor, bool proportional)
            : this(
                player, x, y, text, font, foreColor, letterWidth, letterHeight, width, height, alignment, shadow,
                outline, backColor)
        {
            Proportional = proportional;
        }

        public virtual Player Player { get; private set; }


        public override void Show()
        {
            if (Id == -1)
                Prepare();
            if (Player != null)
                Native.PlayerTextDrawShow(Player.Id, Id);
        }

        public override void Show(Player player)
        {
            if (player == Player)
                Show();
        }

        public override void Hide()
        {
            if (Id == -1) return;
            Native.TextDrawHideForAll(Id);
        }

        public override void Hide(Player player)
        {
            if (player == Player)
                Hide();
        }


        public override void Dispose()
        {
            if (Id < 0 || Player == null) return;
            Native.PlayerTextDrawDestroy(Player.Id, Id);
        }

        protected override void Create()
        {
            Id = Player == null ? -1 : Native.CreatePlayerTextDraw(Player.Id, X, Y, Text);
        }

        protected override void UpdatePlayers()
        {
            //TODO: Check what happens after Show() or Show() Hide(Player), does it still show for newcomers?
            //Then update the TD for these players here.
        }

        protected override void SetAlignment(TextDrawAlignment alignment)
        {
            if (Id < 0 || Player == null) return;
            Native.PlayerTextDrawAlignment(Player.Id, Id, (int) alignment);
            UpdatePlayers();
        }

        protected override void SetText(string text)
        {
            if (Id < 0 || Player == null) return;
            Native.PlayerTextDrawSetString(Player.Id, Id, text);
            UpdatePlayers();
        }

        protected override void SetBackColor(Color color)
        {
            if (Id < 0 || Player == null) return;
            Native.PlayerTextDrawBackgroundColor(Player.Id, Id, color);
            UpdatePlayers();
        }

        protected override void SetForeColor(Color color)
        {
            if (Id < 0 || Player == null) return;
            Native.PlayerTextDrawColor(Player.Id, Id, color);
            UpdatePlayers();
        }

        protected override void SetBoxColor(Color color)
        {
            if (Id < 0 || Player == null) return;
            Native.PlayerTextDrawBoxColor(Player.Id, Id, color);
            UpdatePlayers();
        }

        protected override void SetFont(TextDrawFont font)
        {
            if (Id < 0 || Player == null) return;
            Native.PlayerTextDrawFont(Player.Id, Id, (int) font);
            UpdatePlayers();
        }

        protected override void SetLetterSize(float width, float height)
        {
            if (Id < 0 || Player == null) return;
            Native.PlayerTextDrawLetterSize(Player.Id, Id, width, height);
            UpdatePlayers();
        }

        protected override void SetOutline(int size)
        {
            if (Id < 0 || Player == null) return;
            Native.PlayerTextDrawSetOutline(Player.Id, Id, size);
            UpdatePlayers();
        }

        protected override void SetProportional(bool proportional)
        {
            if (Id < 0 || Player == null) return;
            Native.PlayerTextDrawSetProportional(Player.Id, Id, proportional);
            UpdatePlayers();
        }

        protected override void SetShadow(int shadow)
        {
            if (Id < 0 || Player == null) return;
            Native.PlayerTextDrawSetShadow(Player.Id, Id, shadow);
            UpdatePlayers();
        }

        protected override void SetSize(float width, float height)
        {
            if (Id < 0 || Player == null) return;
            Native.PlayerTextDrawTextSize(Player.Id, Id, width, height);
            UpdatePlayers();
        }

        protected override void SetUseBox(bool useBox)
        {
            if (Id < 0 || Player == null) return;
            Native.PlayerTextDrawUseBox(Player.Id, Id, useBox);
            UpdatePlayers();
        }

        protected override void SetSelectable(bool selectable)
        {
            if (Id < 0 || Player == null) return;
            Native.PlayerTextDrawSetSelectable(Player.Id, Id, selectable);
            UpdatePlayers();
        }

        protected override void SetPreviewModel(int model)
        {
            if (Id < 0 || Player == null) return;
            Native.PlayerTextDrawSetPreviewModel(Player.Id, Id, model);
            UpdatePlayers();
        }

        protected override void SetPreviewRotation(Vector rotation, float zoom)
        {
            if (Id < 0 || Player == null) return;
            Native.PlayerTextDrawSetPreviewRot(Player.Id, Id, rotation.X, rotation.Y, rotation.Z, zoom);
            UpdatePlayers();
        }

        protected override void SetPreviewVehicleColors(int primaryColor, int secondaryColor)
        {
            if (Id < 0 || Player == null) return;
            Native.PlayerTextDrawSetPreviewVehCol(Player.Id, Id, primaryColor, secondaryColor);
            UpdatePlayers();
        }
    }
}