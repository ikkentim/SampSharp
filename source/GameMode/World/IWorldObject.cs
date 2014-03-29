namespace GameMode.World
{
    /// <summary>
    /// Defines an object that is placed in the world.
    /// </summary>
    public interface IWorldObject
    {
        /// <summary>
        /// Gets or sets the position of this IWorldObject.
        /// </summary>
        Vector Position { get; set; }

        /// <summary>
        /// Gets or sets the rotation of this IWorldObject.
        /// </summary>
        Vector Rotation { get; set; }
    }
}
