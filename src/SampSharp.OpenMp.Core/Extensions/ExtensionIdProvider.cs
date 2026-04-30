using System.Collections.Concurrent;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.OpenMp.Core;

internal static class ExtensionIdProvider<T> where T : Extension
{
    public static UID Id { get; } = ExtensionIdProvider.GetId(typeof(T));
}

internal static class ExtensionIdProvider
{
    private static readonly ConcurrentDictionary<Type, UID> _idCache = new();

    public static UID GetId(Type type)
    {
        return _idCache.GetOrAdd(type, static t =>
        {
            if (Attribute.GetCustomAttribute(t, typeof(ExtensionAttribute), false) is not ExtensionAttribute attribute)
            {
                throw new ArgumentException("Type does not have an ExtensionAttribute.", nameof(type));
            }
            return attribute.Uid;
        });
    }
}
