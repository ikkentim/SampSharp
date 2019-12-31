using SampSharp.Entities.SAMP.NativeComponents;

namespace SampSharp.Entities.SAMP.Components
{
    /// <summary>
    /// Represents a component which provides the data and functionality of a 3D text label.
    /// </summary>
    public class TextLabel : Component
    {
        #region Properties

        /// <summary>
        ///     Gets the color of this text label.
        /// </summary>
        public virtual Color Color { get; }

        /// <summary>
        ///     Gets the text of this text label.
        /// </summary>
        public virtual string Text { get; }

        /// <summary>
        ///     Gets the position of this text label.
        /// </summary>
        public virtual Vector3 Position { get; }

        /// <summary>
        ///     Gets the draw distance.
        /// </summary>
        public virtual float DrawDistance { get; }

        /// <summary>
        ///     Gets a value indicating whether to test the line of sight.
        /// </summary>
        public virtual bool TestLos { get; }

        /// <summary>
        ///     Gets the attached entity.
        /// </summary>
        public virtual Entity AttachedEntity { get; }

        #endregion

        #region Constructors

        public TextLabel(Entity attachedEntity, string text, Color color, Vector3 position, float drawDistance,
            bool testLos)
        {
            AttachedEntity = attachedEntity;
            Text = text;
            Color = color;
            Position = position;
            DrawDistance = drawDistance;
            TestLos = testLos;
        }

        #endregion

        /// <inheritdoc />
        protected override void OnDestroyComponent()
        {
            GetComponent<NativeTextLabel>().Delete3DTextLabel();
        }
    }
}
