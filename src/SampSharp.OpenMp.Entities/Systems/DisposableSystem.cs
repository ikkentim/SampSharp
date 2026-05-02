namespace SampSharp.Entities;

/// <summary>
/// Represents a base type for systems that contain disposable objects.
/// </summary>
public abstract class DisposableSystem : ISystem, IDisposable
{
    private readonly List<IDisposable> _disposables = [];
    private bool _disposed;

    /// <summary>
    /// Adds a disposable object to the list of objects to dispose when this system is disposed.
    /// </summary>
    /// <param name="disposable">The disposable object to add.</param>
    protected void AddDisposable(IDisposable? disposable)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        if (disposable is not null)
        {
            _disposables.Add(disposable);
        }
    }

    /// <summary>
    /// A method that is called when this system is disposed.
    /// </summary>
    protected virtual void OnDispose()
    {
        List<Exception>? errors = null;
        foreach (var disposable in _disposables)
        {
            try
            {
                disposable.Dispose();
            }
            catch (Exception ex)
            {
                errors ??= [];
                errors.Add(ex);
            }
        }

        _disposables.Clear();
        
        if (errors?.Count > 0)
        {
            throw new AggregateException(errors);
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        try
        {
            OnDispose();
        }
        finally
        {
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}