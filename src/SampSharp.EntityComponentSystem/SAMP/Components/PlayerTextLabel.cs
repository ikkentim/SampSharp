using System;
using System.Collections.Generic;
using System.Text;
using SampSharp.EntityComponentSystem.Components;
using SampSharp.EntityComponentSystem.Entities;
using SampSharp.EntityComponentSystem.SAMP.NativeComponents;

namespace SampSharp.EntityComponentSystem.SAMP.Components
{
    /// <summary>
    /// Represents a component which provides the data and functionality of a player 3D text label.
    /// </summary>
    public class PlayerTextLabel : Component
    {
        #region Properties

        /// <summary>
        ///     Gets the color of this player text label.
        /// </summary>
        public virtual Color Color { get; }

        /// <summary>
        ///     Gets the text of this player text label.
        /// </summary>
        public virtual string Text { get; }

        /// <summary>
        ///     Gets the position of this player text label.
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

        public PlayerTextLabel(Entity attachedEntity, string text, Color color, Vector3 position, float drawDistance,
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
            GetComponent<NativePlayerTextLabel>().DeletePlayer3DTextLabel();
        }
    }
}
