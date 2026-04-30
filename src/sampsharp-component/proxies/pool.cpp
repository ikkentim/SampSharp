#include <sdk.hpp>

template <typename T>
struct IPoolHack : public IPool<T> {
    const FlatPtrHashSet<T>& exposeEntries() {
        return this->entries(); // Accessing protected method
    }
};

extern "C" SDK_EXPORT const void* __CDECL IPool_entries(IPool<IIDProvider>* pool)
{
    IPoolHack<IIDProvider>* hack = static_cast<IPoolHack<IIDProvider>*>(pool);
    const FlatPtrHashSet<IIDProvider>& entriesRef = hack->exposeEntries();

    return static_cast<const void*>(&entriesRef);
}