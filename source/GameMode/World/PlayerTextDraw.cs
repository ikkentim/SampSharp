using GameMode.Definitions;

namespace GameMode.World
{
    public class PlayerTextDraw : TextDraw
    {
        public PlayerTextDraw(Player player)
        {
            Player = player;
            TextDrawId = -1;
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

        public PlayerTextDraw(Player player, float x, float y, string text, TextDrawFont font, Color foreColor, float letterWidth,
            float letterHeight)
            : this(player, x, y, text, font, foreColor)
        {
            LetterWidth = letterWidth;
            LetterHeight = letterHeight;
        }

        public PlayerTextDraw(Player player, float x, float y, string text, TextDrawFont font, Color foreColor, float letterWidth,
            float letterHeight, float width, float height)
            : this(player, x, y, text, font, foreColor, letterWidth, letterHeight)
        {
            Width = width;
            Height = height;
        }

        public PlayerTextDraw(Player player, float x, float y, string text, TextDrawFont font, Color foreColor, float letterWidth,
            float letterHeight, float width, float height, TextDrawAlignment alignment)
            : this(player, x, y, text, font, foreColor, letterWidth, letterHeight, width, height)
        {
            Alignment = alignment;
        }

        public PlayerTextDraw(Player player, float x, float y, string text, TextDrawFont font, Color foreColor, float letterWidth,
            float letterHeight, float width, float height, TextDrawAlignment alignment, int shadow)
            : this(player, x, y, text, font, foreColor, letterWidth, letterHeight, width, height, alignment)
        {
            Shadow = shadow;
        }

        public PlayerTextDraw(Player player, float x, float y, string text, TextDrawFont font, Color foreColor, float letterWidth,
            float letterHeight, float width, float height, TextDrawAlignment alignment, int shadow, int outline)
            : this(player, x, y, text, font, foreColor, letterWidth, letterHeight, width, height, alignment, shadow)
        {
            Outline = outline;
        }

        public PlayerTextDraw(Player player, float x, float y, string text, TextDrawFont font, Color foreColor, float letterWidth,
            float letterHeight, float width, float height, TextDrawAlignment alignment, int shadow, int outline,
            Color backColor)
            : this(player, x, y, text, font, foreColor, letterWidth, letterHeight, width, height, alignment, shadow, outline)
        {
            BackColor = backColor;
        }

        public PlayerTextDraw(Player player, float x, float y, string text, TextDrawFont font, Color foreColor, float letterWidth,
            float letterHeight, float width, float height, TextDrawAlignment alignment, int shadow, int outline,
            Color backColor, bool proportional)
            : this(player, x, y, text, font, foreColor, letterWidth, letterHeight, width, height, alignment, shadow, outline, backColor)
        {
            Proportional = proportional;
        }


        public override void Show()
        {
            if (TextDrawId == -1)
                Prepare();
            if (Player != null)
                Native.PlayerTextDrawShow(Player.PlayerId, TextDrawId);
        }

        public override void Show(Player player)
        {
            if (player == Player)
                Show();
        }

        public override void Hide()
        {
            if (TextDrawId == -1) return;
            Native.TextDrawHideForAll(TextDrawId);
        }

        public override void Hide(Player player)
        {
            if(player == Player)
                Hide();
        }


        public override void Dispose()
        {
            if (TextDrawId < 0 || Player == null) return;
            Native.PlayerTextDrawDestroy(Player.PlayerId, TextDrawId);
        }

        protected override void Create()
        {
            TextDrawId = Player == null ? -1 : Native.CreatePlayerTextDraw(Player.PlayerId, X, Y, Text);
        }

        protected override void UpdatePlayers()
        {
            //TODO: Check what happens after Show() or Show() Hide(Player), does it still show for newcomers?
            //Then update the TD for these players here.
        }

        protected override void SetAlignment(TextDrawAlignment alignment)
        {
            if (TextDrawId < 0 || Player == null) return;
            Native.PlayerTextDrawAlignment(Player.PlayerId, TextDrawId, (int)alignment);
            UpdatePlayers();
        }

        protected override void SetText(string text)
        {
            if (TextDrawId < 0 || Player == null) return;
            Native.PlayerTextDrawSetString(Player.PlayerId, TextDrawId, text);
            UpdatePlayers();
        }

        protected override void SetBackColor(Color color)
        {
            if (TextDrawId < 0 || Player == null) return;
            Native.PlayerTextDrawBackgroundColor(Player.PlayerId, TextDrawId, color);
            UpdatePlayers();
        }

        protected override void SetForeColor(Color color)
        {
            if (TextDrawId < 0 || Player == null) return;
            Native.PlayerTextDrawColor(Player.PlayerId, TextDrawId, color);
            UpdatePlayers();
        }

        protected override void SetBoxColor(Color color)
        {
            if (TextDrawId < 0 || Player == null) return;
            Native.PlayerTextDrawBoxColor(Player.PlayerId, TextDrawId, color);
            UpdatePlayers();
        }

        protected override void SetFont(TextDrawFont font)
        {
            if (TextDrawId < 0 || Player == null) return;
            Native.PlayerTextDrawFont(Player.PlayerId, TextDrawId, (int)font);
            UpdatePlayers();
        }

        protected override void SetLetterSize(float width, float height)
        {
            if (TextDrawId < 0 || Player == null) return;
            Native.PlayerTextDrawLetterSize(Player.PlayerId, TextDrawId, width, height);
            UpdatePlayers();
        }

        protected override void SetOutline(int size)
        {
            if (TextDrawId < 0 || Player == null) return;
            Native.PlayerTextDrawSetOutline(Player.PlayerId, TextDrawId, size);
            UpdatePlayers();
        }

        protected override void SetProportional(bool proportional)
        {
            if (TextDrawId < 0 || Player == null) return;
            Native.PlayerTextDrawSetProportional(Player.PlayerId, TextDrawId, proportional);
            UpdatePlayers();
        }

        protected override void SetShadow(int shadow)
        {
            if (TextDrawId < 0 || Player == null) return;
            Native.PlayerTextDrawSetShadow(Player.PlayerId, TextDrawId, shadow);
            UpdatePlayers();
        }

        protected override void SetSize(float width, float height)
        {
            if (TextDrawId < 0 || Player == null) return;
            Native.PlayerTextDrawTextSize(Player.PlayerId, TextDrawId, width, height);
            UpdatePlayers();
        }

        protected override void SetUseBox(bool useBox)
        {
            if (TextDrawId < 0 || Player == null) return;
            Native.PlayerTextDrawUseBox(Player.PlayerId, TextDrawId, useBox);
            UpdatePlayers();
        }

        protected override void SetSelectable(bool selectable)
        {
            if (TextDrawId < 0 || Player == null) return;
            Native.PlayerTextDrawSetSelectable(Player.PlayerId, TextDrawId, selectable);
            UpdatePlayers();
        }

        protected override void SetPreviewModel(int model)
        {
            if (TextDrawId < 0 || Player == null) return;
            Native.PlayerTextDrawSetPreviewModel(Player.PlayerId, TextDrawId, model);
            UpdatePlayers();
        }

        protected override void SetPreviewRotation(Rotation rotation, float zoom)
        {
            if (TextDrawId < 0 || Player == null) return;
            Native.PlayerTextDrawSetPreviewRot(Player.PlayerId, TextDrawId, rotation.X, rotation.Y, rotation.Z, zoom);
            UpdatePlayers();
        }

        protected override void SetPreviewVehicleColors(int primaryColor, int secondaryColor)
        {
            if (TextDrawId < 0 || Player == null) return;
            Native.PlayerTextDrawSetPreviewVehCol(Player.PlayerId, TextDrawId, primaryColor, secondaryColor);
            UpdatePlayers();
        }

        public virtual Player Player { get; private set; }
    }
}
