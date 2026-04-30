namespace SampSharp.Entities;

/// <summary>Contains methods which can be implemented by systems which handle server ticks.</summary>
/// <seealso cref="ISystem" />
public interface ITickingSystem : ISystem
{
    /// <summary>Occurs every server tick.</summary>
    void Tick();
}