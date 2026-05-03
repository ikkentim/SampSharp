#include <sdk.hpp>
#include "../proxy-api.hpp"

typedef bool(API_CALLTYPE *OptionEnumeratorCallbackFn)(StringView name, ConfigOptionType type);

class OptionEnumeratorCallbackImpl final : public OptionEnumeratorCallback
{
    OptionEnumeratorCallbackFn _fn;

public:
    OptionEnumeratorCallbackImpl(OptionEnumeratorCallbackFn fn) : _fn(fn) {}
    bool proc(StringView name, ConfigOptionType type) override
    {
        return _fn(name, type);
    }
};

extern "C" SDK_EXPORT void __CDECL IConfig_enumOptions(IConfig *subject, OptionEnumeratorCallbackFn callback)
{
    if (!subject)
    {
        return;
    }
    OptionEnumeratorCallbackImpl impl(callback);
    subject->enumOptions(impl);
}