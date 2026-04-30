using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.OpenMp.Core;

/// <summary>
/// Represents a base class for managed extensions which can be attached to extensible open.mp entities. The implementation must have an <c>[Extension(...)]</c> attribute.
/// </summary>
public abstract partial class Extension : IDisposable
{
    // Must keep a reference to this delegate to prevent it from being garbage collected
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly Action _free;

    private nint? _unmanagedCounterpart;
    private GCHandle? _gcHandle;
    private IExtensible? _appliedTo;

    /// <summary>
    /// Initializes a new instance of the <see cref="Extension" /> class.
    /// </summary>
    protected Extension()
    {
        _free = FreeExtension;
        var id = ExtensionIdProvider.GetId(GetType());
        var free = Marshal.GetFunctionPointerForDelegate(_free);
        var handle = GCHandle.Alloc(this, GCHandleType.Normal);

        _gcHandle = handle;
        _unmanagedCounterpart = CreateUnmanaged(id, free, GCHandle.ToIntPtr(handle));
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="Extension" /> class.
    /// </summary>
    ~Extension()
    {
        // Theoretically, this should never be called, as one of the unmanaged resources is a GC handle pointing to this
        // object. But just to be sure, we'll free the resources here as well.
        FreeUnmanagedResources();
    }

    /// <summary>
    /// Gets a value indicating whether this extension has been disposed.
    /// </summary>
    [MemberNotNullWhen(false, nameof(_gcHandle), nameof(_unmanagedCounterpart))]
    protected bool IsDisposed => !_unmanagedCounterpart.HasValue;

    /// <summary>
    /// Detaches this extension from the extensible it is currently applied to and destroys all resources held by this extension.
    /// </summary>
    public void Dispose()
    {
        Detach();

        FreeUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// This method is called when the extension is being cleaned up. The extension may be cleaned up because it is
    /// being removed from an extensible or because the extensible is being disposed.
    /// </summary>
    protected virtual void Cleanup()
    {
    }

    /// <summary>
    /// Gets the managed extension counterpart of the specified unmanaged extension.
    /// </summary>
    /// <param name="ext">The unmanaged extension.</param>
    /// <returns>The managed extension or <see langword="null" /> if the unmanaged extension is in an invalid state.</returns>
    internal static Extension? Get(IExtension ext)
    {
        var handle = GetHandle(ext.Handle);
        var gcHandle = GCHandle.FromIntPtr(handle);

        if (!gcHandle.IsAllocated)
        {
            return null;
        }

        return gcHandle.Target as Extension;
    }

    /// <summary>
    /// Gets a pointer to the unmanaged counterpart of this extension.
    /// </summary>
    /// <returns>A pointer.</returns>
    internal IExtension GetUnmanaged()
    {
        ObjectDisposedException.ThrowIf(IsDisposed, GetType());
        
        return new IExtension(_unmanagedCounterpart.Value);
    }

    /// <summary>
    /// Frees all unmanaged resources held by this extension.
    /// </summary>
    private void FreeUnmanagedResources()
    {
        FreeUnmanagedCounterpart();
        FreeHandle();
    }

    /// <summary>
    /// Free the unmanaged counterpart of this extension.
    /// </summary>
    private void FreeUnmanagedCounterpart()
    {
        if (_unmanagedCounterpart.HasValue)
        {
            Delete(_unmanagedCounterpart.Value);
            _unmanagedCounterpart = null;
            _appliedTo = null;
        }
    }

    /// <summary>
    /// Frees the GC handle held by this extension.
    /// </summary>
    private void FreeHandle()
    {
        if (_gcHandle.HasValue)
        {
            _gcHandle.Value.Free();
            _gcHandle = null;
        }
    }

    /// <summary>
    /// Removes the extension from the extensible it is currently applied to.
    /// </summary>
    private void Detach()
    {
        if (_appliedTo.HasValue && _unmanagedCounterpart.HasValue)
        {
            _appliedTo.Value.RemoveExtension(new IExtension(_unmanagedCounterpart.Value));
            _appliedTo = null;
        }
    }

    /// <summary>
    /// Attaches the extension to the specified extensible.
    /// </summary>
    /// <param name="extensible">The extensible entity to attach this extension to.</param>
    /// <exception cref="InvalidOperationException">Throw if this extension was already attached to another
    /// entity.</exception>
    internal void Attach(IExtensible extensible)
    {
        if (_appliedTo.HasValue)
        {
            throw new InvalidOperationException("Can only apply to one extensible");
        }

        _appliedTo = extensible;
    }

    /// <summary>
    /// Entry-point from the unmanaged side to free the extension.
    /// </summary>
    private void FreeExtension()
    {
        try
        {
            Cleanup();
            _appliedTo = null;
        }
        catch(Exception e)
        {
            SampSharpExceptionHandler.HandleException($"{GetType().FullName}.FreeExtension",e);
        }
        finally
        {
            FreeHandle();

            // no need to clean up the unmanaged counterpart - it's already been cleaned up by the caller
            _unmanagedCounterpart = null;
        }
    }

    [LibraryImport("SampSharp", EntryPoint = "ManagedExtensionImpl_create")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial nint CreateUnmanaged(UID id, nint freePointer, nint handle);

    [LibraryImport("SampSharp", EntryPoint = "ManagedExtensionImpl_delete")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial void Delete(nint handle);
    
    [LibraryImport("SampSharp", EntryPoint = "ManagedExtensionImpl_getHandle")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial nint GetHandle(nint handle);
}