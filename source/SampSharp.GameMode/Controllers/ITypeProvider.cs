namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     Provides the functionality for an <see cref="IController"/> to register types.
    /// </summary>
    public interface ITypeProvider : IController
    {
        /// <summary>
        ///     Registers types this <see cref="ITypeProvider"/> requires the system to use.
        /// </summary>
        void RegisterTypes();
    }
}
