
#include <sdk.hpp>

#include "../proxy-api.hpp"

// PoolEventHandler<void *>
class PoolEventHandlerImpl final : PoolEventHandler<void *>
{
    typedef void(API_CALLTYPE * handle_fn)(void *&);

    handle_fn onPoolEntryCreated_;
    handle_fn onPoolEntryDestroyed_;
public: 
    PoolEventHandlerImpl(void ** onPoolEntryCreated, void ** onPoolEntryDestroyed) :
        onPoolEntryCreated_(reinterpret_cast<handle_fn>(onPoolEntryCreated)),
        onPoolEntryDestroyed_(reinterpret_cast<handle_fn>(onPoolEntryDestroyed)) { }

    void onPoolEntryCreated(void *& entry) override
    {
        onPoolEntryCreated_(entry);
    }
    void onPoolEntryDestroyed(void *& entry) override
    {
        onPoolEntryDestroyed_(entry);
    }
};

extern "C" SDK_EXPORT PoolEventHandlerImpl * __CDECL PoolEventHandlerImpl_create(void ** a, void ** b)
{
    return new PoolEventHandlerImpl(a, b);
}

extern "C" SDK_EXPORT void __CDECL PoolEventHandlerImpl_delete(const PoolEventHandlerImpl * handler)
{
    delete handler;
}
