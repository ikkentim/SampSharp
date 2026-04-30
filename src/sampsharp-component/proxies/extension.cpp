
#include <sdk.hpp>

#include "../proxy-api.hpp"

typedef void(API_CALLTYPE * free_fn)();

class ManagedExtensionImpl final : IExtension
{
private:
    UID extensionID_;
    free_fn free_fptr_;
    void * handle_;

public: 
    ManagedExtensionImpl(UID extensionID, free_fn free_fptr, void * handle)  : 
        extensionID_(extensionID),
        free_fptr_(free_fptr),
        handle_(handle) { }

    UID getExtensionID() override { return extensionID_; }

    void freeExtension() override
    {
        free_fptr_();
        delete this;
     }

    void * getHandle() {return  handle_; }

    void reset() override { }
};

extern "C" SDK_EXPORT ManagedExtensionImpl * __CDECL ManagedExtensionImpl_create(UID a, free_fn b, void * c)
{
    return new ManagedExtensionImpl(a, b, c);
}

extern "C" SDK_EXPORT void __CDECL ManagedExtensionImpl_delete(ManagedExtensionImpl * handler)
{
    delete handler;
}

extern "C" SDK_EXPORT void * __CDECL ManagedExtensionImpl_getHandle(ManagedExtensionImpl * handler)
{
    return handler->getHandle();
}

struct workaround {
    void * vptr;
    FlatHashMap<UID, Pair<IExtension*, bool>> miscExtensions;
};

extern "C" SDK_EXPORT IExtension * __CDECL IExtensible_getExtension_workaround(IExtensible * subject, UID extensionID)
{
    // workaround for the fact that the SDK doesn't expose the miscExtensions field
    // ref: https://github.com/openmultiplayer/open.mp-sdk/issues/44
    workaround * w = (workaround *) subject;

    auto it = w->miscExtensions.find(extensionID);
    if (it != w->miscExtensions.end())
    {
        return it->second.first;
    }

    return subject->getExtension(extensionID);
}

