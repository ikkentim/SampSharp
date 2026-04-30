#include <sdk.hpp>

struct FlatHashSetPtr { void* a; void* b; };

extern "C" SDK_EXPORT FlatHashSetPtr __CDECL FlatPtrHashSet_begin(FlatPtrHashSet<void *>& set)
{
    auto it = set.begin();
    return *(FlatHashSetPtr*)&it;
}

extern "C" SDK_EXPORT FlatHashSetPtr __CDECL FlatPtrHashSet_end(FlatPtrHashSet<void *>& set)
{
    auto it = set.end();
    return *(FlatHashSetPtr*)&it;
}

extern "C" SDK_EXPORT FlatHashSetPtr __CDECL FlatPtrHashSet_inc(FlatPtrHashSet<void *>::iterator value)
{
    value++;
    return *(FlatHashSetPtr*)&value;
}

extern "C" SDK_EXPORT size_t  __CDECL FlatPtrHashSet_size(FlatPtrHashSet<void *>& set)
{
    return set.size();
}

extern "C" SDK_EXPORT FlatHashSetPtr __CDECL FlatHashSetStringView_begin(FlatHashSet<StringView>& set)
{
    auto it = set.begin();
    return *(FlatHashSetPtr*)&it;
}

extern "C" SDK_EXPORT struct FlatHashSetPtr __CDECL FlatHashSetStringView_end(FlatHashSet<StringView>& set)
{
    auto it = set.end();
    return *(FlatHashSetPtr*)&it;
}

extern "C" SDK_EXPORT FlatHashSetPtr __CDECL FlatHashSetStringView_inc(FlatHashSet<StringView>::iterator value)
{
    value++;
    return *(FlatHashSetPtr*)&value;
}

extern "C" SDK_EXPORT size_t  __CDECL FlatHashSetStringView_size(FlatHashSet<StringView>& set)
{
    return set.size();
}

extern "C" SDK_EXPORT void __CDECL FlatHashSetStringView_emplace(FlatHashSet<StringView>& set, StringView value)
{
    set.emplace(value);
}