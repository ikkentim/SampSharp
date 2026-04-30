namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IExtensible" /> interface.
/// </summary>
[OpenMpApi]
public readonly partial struct IExtensible
{
    /// <summary>
    /// Gets the extension with the specified <paramref name="id" />.
    /// </summary>
    /// <param name="id">The identifier of the extension type.</param>
    /// <returns>The extension or <see langword="null" /> if the extension could not be found.</returns>
    // workaround for the fact that the SDK doesn't expose the miscExtensions field
    // ref: https://github.com/openmultiplayer/open.mp-sdk/issues/44
    [OpenMpApiOverload("_workaround")]
    public partial IExtension GetExtension(UID id);

    private partial bool AddExtension(IExtension extension, bool autoDeleteExt);

    [OpenMpApiOverload("_uid")]
    [OpenMpApiFunction("removeExtension")]
    private partial bool RemoveExtensionInternal(UID id);

    [OpenMpApiFunction("removeExtension")]
    private partial bool RemoveExtensionInternal(IExtension extension);

    /// <summary>
    /// Removes the extension with the specified <paramref name="id" /> from this extensible.
    /// </summary>
    /// <param name="id">The identifier of the extension.</param>
    /// <exception cref="ArgumentException">Thrown if the extension could not be found.</exception>
    public void RemoveExtension(UID id)
    {
        if (!RemoveExtensionInternal(id))
        {
            throw new ArgumentException($"Failed to remove extension with id '{id}'.", nameof(id));
        }
    }

    /// <summary>
    /// Removes the specified <paramref name="extension" /> from this extensible.
    /// </summary>
    /// <param name="extension">The extension to remove.</param>
    /// <exception cref="ArgumentException">Thrown if the extension could not be found.</exception>
    public void RemoveExtension(IExtension extension)
    {
        if (extension == null)
        {
            // Can't use ThrowIfNull - extension is not a reference type.
            throw new ArgumentNullException(nameof(extension));
        }

        if (!RemoveExtensionInternal(extension))
        {
            throw new ArgumentException("Failed to remove extension", nameof(extension));
        }
    }

    /// <summary>
    /// Removes the specified managed <paramref name="extension" /> from this extensible.
    /// </summary>
    /// <typeparam name="T">The type of the managed extension.</typeparam>
    /// <param name="extension">The managed extension to remove.</param>
    /// <exception cref="ArgumentException">Thrown if the extension could not be found.</exception>
    public void RemoveExtension<T>(T extension) where T : Extension
    {
        ArgumentNullException.ThrowIfNull(extension);

        RemoveExtension(extension.GetUnmanaged());
    }

    /// <summary>
    /// Adds the specified managed <paramref name="extension" /> to this extensible.
    /// </summary>
    /// <typeparam name="T">The type of the managed extension.</typeparam>
    /// <param name="extension">An instance of the extension to add. The extension will be disposed if the extension could not be added to this extensible.</param>
    /// <remarks>A managed extension can only be added to one extensible.</remarks>
    /// <exception cref="ArgumentException">Throw when an instance of the extension type was already added to this extensible.</exception>
    public void AddExtension<T>(T extension) where T : Extension
    {
        ArgumentNullException.ThrowIfNull(extension);

        var unmanaged = extension.GetUnmanaged();

        try
        {
            if (!AddExtension(unmanaged, true))
            {
                throw new ArgumentException("Failed to add extension", nameof(extension));
            }
        }
        catch
        {
            extension.Dispose();
            throw;
        }
    }

    /// <summary>
    /// Gets the specified managed extension from this extensible.
    /// </summary>
    /// <typeparam name="T">The type of the managed extension.</typeparam>
    /// <returns>An instance of the extension with type <typeparamref name="T" />.</returns>
    /// <exception cref="ArgumentException">Thrown if the extension could not be found.</exception>
    public T GetExtension<T>() where T : Extension
    {
        var result = TryGetExtension<T>();

        return result ?? throw new ArgumentException($"Extension of type '{typeof(T).Name}' not found.", nameof(T));
    }

    /// <summary>
    /// Tries to get the specified managed extension from this extensible.
    /// </summary>
    /// <typeparam name="T">The type of the managed extension.</typeparam>
    /// <returns>An instance of the extension with type <typeparamref name="T" /> or <see langword="null" /> if no extension with the specified type could be found.</returns>
    public T? TryGetExtension<T>() where T : Extension
    {
        if (!HasValue)
        {
            return null;
        }

        var ext = GetExtension(ExtensionIdProvider<T>.Id);

        if (ext == null)
        {
            return null;
        }


        return Extension.Get(ext) as T;
    }

    /// <summary>
    /// Gets the specified unmanaged extension from this extensible.
    /// </summary>
    /// <typeparam name="T">The type of the unmanaged extension.</typeparam>
    /// <returns>The unmanaged extension or <see langword="null" /> if the extension could not be found.</returns>
    public T QueryExtension<T>() where T : unmanaged, IExtension.IManagedInterface
    {
        if (!HasValue)
        {
            return default;
        }

        var extension = GetExtension(T.ExtensionId).Handle;

        return StructPointer.AsStruct<T>(extension);
    }

    /// <summary>
    /// Tries to get the specified unmanaged extension from this extensible.
    /// </summary>
    /// <typeparam name="T">The type of the unmanaged extension.</typeparam>
    /// <param name="extension">The extension if found, otherwise <see langword="null" />.</param>
    /// <returns><see langword="true" /> if the extension was found; <see langword="false" /> otherwise.</returns>
    public bool TryQueryExtension<T>(out T extension) where T : unmanaged, IExtension.IManagedInterface
    {
        extension = QueryExtension<T>();
        return extension.HasValue;
    }
}
