using System;
using GameMode.Definitions;

namespace GameMode.World
{
    public class TextDraw : IDisposable
    {
        public TextDrawAlignment Alignment { get; set; }
        public Color BackgroundColor { get; set; }
        public Color ForeColor { get; set; }
        public TextDrawFont Font { get; set; }
        public float LetterSizeWidth { get; set; }
        public float LetterSizeHeight { get; set; }
        public int Outline { get; set; }
        public bool Proportional { get; set; }
        public int Shadow { get; set; }
        public string Text { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public bool UseBox { get; set; }
        public bool Selectable { get; set; }
        public int PreviewModel { get; set; }
        public float PreviewRotationX { get; set; }
        public float PreviewRotationY { get; set; }
        public float PreviewRotationZ { get; set; }
        public float PreviewZoom { get; set; }
        public float PreviewPrimaryColor { get; set; }
        public float PreviewSecondaryColor { get; set; }

        public int TextDrawId { get; set; }

        public TextDraw()
        {
            
        }

        public virtual void Show()
        {
            Native.TextDrawShowForAll(TextDrawId);
        }

        public virtual void Show(Player player)
        {
            Native.TextDrawShowForPlayer(player.PlayerId, TextDrawId);
        }

        public virtual void Hide()
        {
            Native.TextDrawHideForAll(TextDrawId);
        }

        public virtual void Hide(Player player)
        {
            Native.TextDrawHideForPlayer(player.PlayerId, TextDrawId);
        }

        public void Dispose()
        {
            Native.TextDrawDestroy(TextDrawId);
        }
    }
}
