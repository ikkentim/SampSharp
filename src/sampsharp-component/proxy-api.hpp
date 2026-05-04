#pragma once

#include <type_traits>
#include <exception>
#include <cstdio>

#if defined(_WIN32)
    #define API_CALLTYPE __stdcall
#else
    #define API_CALLTYPE
#endif

//
// Null-subject guard. Used by PROXY() macros to avoid dereferencing a null
// pointer when gamemode code calls a method on a destroyed/disconnected
// open.mp entity (IPlayer, IVehicle, etc.). Every proxy export goes through
// this helper when `subject == nullptr`.
//
// * void return -> return;
// * reference return -> terminate (cannot manufacture a reference).
// * anything default-constructible -> return T{} (e.g. 0, nullptr, {}).
//
// NOTE: This catches only literal null handles. Stale/freed pointers that
// the managed side cached before a disconnect are still UB; tracking live
// handles in a registry is a separate follow-up.
//
namespace sampsharp
{
    template <typename T>
    [[noreturn]] inline T proxy_default_unsupported(const char* name)
    {
        std::fprintf(stderr,
            "sampsharp: PROXY call with null subject returning an unsupported type for '%s'. Aborting.\n",
            name);
        std::terminate();
    }

    template <typename T>
    inline T proxy_default(const char* name)
    {
        if constexpr (std::is_void_v<T>)
        {
            (void)name;
            return;
        }
        else if constexpr (std::is_reference_v<T>)
        {
            proxy_default_unsupported<T>(name);
        }
        else if constexpr (std::is_default_constructible_v<T>)
        {
            (void)name;
            return T{};
        }
        else
        {
            proxy_default_unsupported<T>(name);
        }
    }
} // namespace sampsharp

//
// internal macros
//

// expand variadic args as a numbered parameter list. e.g. _EXPAND_PARAM(a, b, X, Y) -> aXb _2, aYb _1
#define _EXPAND_PARAM(prefix,postfix,...) _EXPAND_PARAM_N(__VA_ARGS__,25,24,23,22,21,20,19,18,17,16,15,14,13,12,11,10,9,8,7,6,5,4,3,2,1,0)(prefix,postfix,##__VA_ARGS__)
#define _EXPAND_PARAM_N(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18, _19, _20, _21, _22, _23, _24, _25, n, ...) _EXPAND_PARAM ## n
#define _EXPAND_PARAM1(prefix, postfix, type, ...) prefix##type##postfix _1
#define _EXPAND_PARAM2(prefix, postfix, type, ...) prefix##type##postfix _2, _EXPAND_PARAM1(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM3(prefix, postfix, type, ...) prefix##type##postfix _3, _EXPAND_PARAM2(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM4(prefix, postfix, type, ...) prefix##type##postfix _4, _EXPAND_PARAM3(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM5(prefix, postfix, type, ...) prefix##type##postfix _5, _EXPAND_PARAM4(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM6(prefix, postfix, type, ...) prefix##type##postfix _6, _EXPAND_PARAM5(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM7(prefix, postfix, type, ...) prefix##type##postfix _7, _EXPAND_PARAM6(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM8(prefix, postfix, type, ...) prefix##type##postfix _8, _EXPAND_PARAM7(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM9(prefix, postfix, type, ...) prefix##type##postfix _9, _EXPAND_PARAM8(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM10(prefix, postfix, type, ...) prefix##type##postfix _10, _EXPAND_PARAM9(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM11(prefix, postfix, type, ...) prefix##type##postfix _11, _EXPAND_PARAM10(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM12(prefix, postfix, type, ...) prefix##type##postfix _12, _EXPAND_PARAM11(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM13(prefix, postfix, type, ...) prefix##type##postfix _13, _EXPAND_PARAM12(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM14(prefix, postfix, type, ...) prefix##type##postfix _14, _EXPAND_PARAM13(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM15(prefix, postfix, type, ...) prefix##type##postfix _15, _EXPAND_PARAM14(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM16(prefix, postfix, type, ...) prefix##type##postfix _16, _EXPAND_PARAM15(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM17(prefix, postfix, type, ...) prefix##type##postfix _17, _EXPAND_PARAM16(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM18(prefix, postfix, type, ...) prefix##type##postfix _18, _EXPAND_PARAM17(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM19(prefix, postfix, type, ...) prefix##type##postfix _19, _EXPAND_PARAM18(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM20(prefix, postfix, type, ...) prefix##type##postfix _20, _EXPAND_PARAM19(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM21(prefix, postfix, type, ...) prefix##type##postfix _21, _EXPAND_PARAM20(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM22(prefix, postfix, type, ...) prefix##type##postfix _22, _EXPAND_PARAM21(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM23(prefix, postfix, type, ...) prefix##type##postfix _23, _EXPAND_PARAM22(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM24(prefix, postfix, type, ...) prefix##type##postfix _24, _EXPAND_PARAM23(prefix, postfix, ##__VA_ARGS__)
#define _EXPAND_PARAM25(prefix, postfix, type, ...) prefix##type##postfix _25, _EXPAND_PARAM24(prefix, postfix, ##__VA_ARGS__)

// expand variadic args as a numbered argument list. e.g. _EXPAND_ARG(,X, Y) -> _2, _1
#define _EXPAND_ARG(prefix, ...) _EXPAND_ARG_N(__VA_ARGS__,25,24,23,22,21,20,19,18,17,16,15,14,13,12,11,10,9,8,7,6,5,4,3,2,1,0)(prefix,##__VA_ARGS__)
#define _EXPAND_ARG_N(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18, _19, _20, _21, _22, _23, _24, _25, n, ...) _EXPAND_ARG ## n
#define _EXPAND_ARG1(prefix,type, ...) prefix _1
#define _EXPAND_ARG2(prefix,type, ...) prefix _2, _EXPAND_ARG1(prefix,##__VA_ARGS__)
#define _EXPAND_ARG3(prefix,type, ...) prefix _3, _EXPAND_ARG2(prefix,##__VA_ARGS__)
#define _EXPAND_ARG4(prefix,type, ...) prefix _4, _EXPAND_ARG3(prefix,##__VA_ARGS__)
#define _EXPAND_ARG5(prefix,type, ...) prefix _5, _EXPAND_ARG4(prefix,##__VA_ARGS__)
#define _EXPAND_ARG6(prefix,type, ...) prefix _6, _EXPAND_ARG5(prefix,##__VA_ARGS__)
#define _EXPAND_ARG7(prefix,type, ...) prefix _7, _EXPAND_ARG6(prefix,##__VA_ARGS__)
#define _EXPAND_ARG8(prefix,type, ...) prefix _8, _EXPAND_ARG7(prefix,##__VA_ARGS__)
#define _EXPAND_ARG9(prefix,type, ...) prefix _9, _EXPAND_ARG8(prefix,##__VA_ARGS__)
#define _EXPAND_ARG10(prefix,type, ...) prefix _10, _EXPAND_ARG9(prefix,##__VA_ARGS__)
#define _EXPAND_ARG11(prefix,type, ...) prefix _11, _EXPAND_ARG10(prefix,##__VA_ARGS__)
#define _EXPAND_ARG12(prefix,type, ...) prefix _12, _EXPAND_ARG11(prefix,##__VA_ARGS__)
#define _EXPAND_ARG13(prefix,type, ...) prefix _13, _EXPAND_ARG12(prefix,##__VA_ARGS__)
#define _EXPAND_ARG14(prefix,type, ...) prefix _14, _EXPAND_ARG13(prefix,##__VA_ARGS__)
#define _EXPAND_ARG15(prefix,type, ...) prefix _15, _EXPAND_ARG14(prefix,##__VA_ARGS__)
#define _EXPAND_ARG16(prefix,type, ...) prefix _16, _EXPAND_ARG15(prefix,##__VA_ARGS__)
#define _EXPAND_ARG17(prefix,type, ...) prefix _17, _EXPAND_ARG16(prefix,##__VA_ARGS__)
#define _EXPAND_ARG18(prefix,type, ...) prefix _18, _EXPAND_ARG17(prefix,##__VA_ARGS__)
#define _EXPAND_ARG19(prefix,type, ...) prefix _19, _EXPAND_ARG18(prefix,##__VA_ARGS__)
#define _EXPAND_ARG20(prefix,type, ...) prefix _20, _EXPAND_ARG19(prefix,##__VA_ARGS__)
#define _EXPAND_ARG21(prefix,type, ...) prefix _21, _EXPAND_ARG20(prefix,##__VA_ARGS__)
#define _EXPAND_ARG22(prefix,type, ...) prefix _22, _EXPAND_ARG21(prefix,##__VA_ARGS__)
#define _EXPAND_ARG23(prefix,type, ...) prefix _23, _EXPAND_ARG22(prefix,##__VA_ARGS__)
#define _EXPAND_ARG24(prefix,type, ...) prefix _24, _EXPAND_ARG23(prefix,##__VA_ARGS__)
#define _EXPAND_ARG25(prefix,type, ...) prefix _25, _EXPAND_ARG24(prefix,##__VA_ARGS__)

/// expand variadic args as a numbered initializer list. e.g. _EXPAND_INIT(X, Y) -> X_(_2), Y_(_1)
#define _EXPAND_INIT(...) _EXPAND_INIT_N(__VA_ARGS__,25,24,23,22,21,20,19,18,17,16,15,14,13,12,11,10,9,8,7,6,5,4,3,2,1,0)(__VA_ARGS__)
#define _EXPAND_INIT_N(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18, _19, _20, _21, _22, _23, _24, _25, n, ...) _EXPAND_INIT ## n
#define _EXPAND_INIT1(type, ...) type##_(_1)
#define _EXPAND_INIT2(type, ...) type##_(_2), _EXPAND_INIT1(__VA_ARGS__)
#define _EXPAND_INIT3(type, ...) type##_(_3), _EXPAND_INIT2(__VA_ARGS__)
#define _EXPAND_INIT4(type, ...) type##_(_4), _EXPAND_INIT3(__VA_ARGS__)
#define _EXPAND_INIT5(type, ...) type##_(_5), _EXPAND_INIT4(__VA_ARGS__)
#define _EXPAND_INIT6(type, ...) type##_(_6), _EXPAND_INIT5(__VA_ARGS__)
#define _EXPAND_INIT7(type, ...) type##_(_7), _EXPAND_INIT6(__VA_ARGS__)
#define _EXPAND_INIT8(type, ...) type##_(_8), _EXPAND_INIT7(__VA_ARGS__)
#define _EXPAND_INIT9(type, ...) type##_(_9), _EXPAND_INIT8(__VA_ARGS__)
#define _EXPAND_INIT10(type, ...) type##_(_10), _EXPAND_INIT9(__VA_ARGS__)
#define _EXPAND_INIT11(type, ...) type##_(_11), _EXPAND_INIT10(__VA_ARGS__)
#define _EXPAND_INIT12(type, ...) type##_(_12), _EXPAND_INIT11(__VA_ARGS__)
#define _EXPAND_INIT13(type, ...) type##_(_13), _EXPAND_INIT12(__VA_ARGS__)
#define _EXPAND_INIT14(type, ...) type##_(_14), _EXPAND_INIT13(__VA_ARGS__)
#define _EXPAND_INIT15(type, ...) type##_(_15), _EXPAND_INIT14(__VA_ARGS__)
#define _EXPAND_INIT16(type, ...) type##_(_16), _EXPAND_INIT15(__VA_ARGS__)
#define _EXPAND_INIT17(type, ...) type##_(_17), _EXPAND_INIT16(__VA_ARGS__)
#define _EXPAND_INIT18(type, ...) type##_(_18), _EXPAND_INIT17(__VA_ARGS__)
#define _EXPAND_INIT19(type, ...) type##_(_19), _EXPAND_INIT18(__VA_ARGS__)
#define _EXPAND_INIT20(type, ...) type##_(_20), _EXPAND_INIT19(__VA_ARGS__)
#define _EXPAND_INIT21(type, ...) type##_(_21), _EXPAND_INIT20(__VA_ARGS__)
#define _EXPAND_INIT22(type, ...) type##_(_22), _EXPAND_INIT21(__VA_ARGS__)
#define _EXPAND_INIT23(type, ...) type##_(_23), _EXPAND_INIT22(__VA_ARGS__)
#define _EXPAND_INIT24(type, ...) type##_(_24), _EXPAND_INIT23(__VA_ARGS__)
#define _EXPAND_INIT25(type, ...) type##_(_25), _EXPAND_INIT24(__VA_ARGS__)

#define __PROXY_IMPL(type_subject, type_return, method, proxy_name, ...) \
    extern "C" SDK_EXPORT type_return __CDECL \
    proxy_name(type_subject * subject __VA_OPT__(, _EXPAND_PARAM(,,##__VA_ARGS__))) \
    { \
        if (!subject) { return ::sampsharp::proxy_default<type_return>(#proxy_name); } \
        return subject -> method ( \
            __VA_OPT__(_EXPAND_ARG(,##__VA_ARGS__)) \
        ); \
    }

#define __PROXY_IMPL_RESULT_PTR(type_subject, type_return, method, proxy_name, ...) \
    extern "C" SDK_EXPORT void __CDECL \
    proxy_name(type_subject * subject __VA_OPT__(, _EXPAND_PARAM(,,__VA_ARGS__)), type_return * result) \
    { \
        if (!subject) { if (result) { *result = type_return{}; } return; } \
        *result = subject -> method ( \
            __VA_OPT__(_EXPAND_ARG(,##__VA_ARGS__)) \
        ); \
    }

//
// macros for definition of exported proxy functions
//


#define __PROXY_CAST_NAMED(type_from, type_from_name, type_to, type_to_name) \
    extern "C" SDK_EXPORT type_to * __CDECL \
    cast_##type_from_name##_to_##type_to_name(type_from * from) \
    { \
        return static_cast<type_to *>(from); \
    }

/// proxy function for casting from one type to another and vice versa e.g. PROXY_CAST_NAMED(IFoo, foo, IBar, bar) -> IBar * cast_foo_to_bar(IFoo * from) { return static_cast<IBar *>(from); }
#define PROXY_CAST_NAMED(type_from, type_from_name, type_to, type_to_name) \
    __PROXY_CAST_NAMED(type_from, type_from_name, type_to, type_to_name); \
    __PROXY_CAST_NAMED(type_to, type_to_name, type_from, type_from_name)

/// proxy function for casting from one type to another e.g. PROXY_CAST(IFoo, IBar) -> IBar * cast_IFoo_to_IBar(IFoo * from) { return static_cast<IBar *>(from); }
#define PROXY_CAST(type_from, type_to) PROXY_CAST_NAMED(type_from, type_from, type_to, type_to)
 
/// proxy function macro. e.g. PROXY(subj, int, foo, bool) -> int subj_foo(subj * x, bool _1) { return x->foo(_1); }
#define PROXY(type_subject, type_return, method, ...) __PROXY_IMPL(type_subject, type_return, method, type_subject##_##method, __VA_ARGS__)

/// proxy function macro. e.g. PROXY_RESULT_PTR(subj, int, foo, bool) -> void subj_foo(subj * x, bool _1, int * result) { *result = x->foo(_1); }
#define PROXY_PTR(type_subject, type_return, method, ...) __PROXY_IMPL_RESULT_PTR(type_subject, type_return, method, type_subject##_##method, __VA_ARGS__)

/// proxy function macro for an overload. output is similar to PROXY macro, except the function name is post-fixed by overload argument
#define PROXY_OVERLOAD(type_subject, type_return, method, overload, ...) __PROXY_IMPL(type_subject, type_return, method, type_subject##_##method##overload, __VA_ARGS__)

/// proxy function macro for an overload. output is similar to PROXY_PTR macro, except the function name is post-fixed by overload argument
#define PROXY_OVERLOAD_PTR(type_subject, type_return, method, overload, ...) __PROXY_IMPL_RESULT_PTR(type_subject, type_return, method, type_subject##_##method##overload, __VA_ARGS__)

/// proxy function macro. e.g. PROXY(subj, int, foo, bool) -> int subj_foo(subj * x, bool _1) { return x->foo(_1); }
#define PROXY_NAMED(type_subject, name_subject, type_return, method, ...) __PROXY_IMPL(type_subject, type_return, method, name_subject##_##method, __VA_ARGS__)

/// proxy function macro. e.g. PROXY_RESULT_PTR(subj, int, foo, bool) -> void subj_foo(subj * x, bool _1, int * result) { *result = x->foo(_1); }
#define PROXY_NAMED_PTR(type_subject, name_subject, type_return, method, ...) __PROXY_IMPL_RESULT_PTR(type_subject, type_return, method, name_subject##_##method, __VA_ARGS__)

/// proxy function macro for an overload. output is similar to PROXY macro, except the function name is post-fixed by overload argument
#define PROXY_NAMED_OVERLOAD(type_subject, name_subject, type_return, method, overload, ...) __PROXY_IMPL(type_subject, type_return, method, name_subject##_##method##overload, __VA_ARGS__)

/// proxy function macro for an overload. output is similar to PROXY_PTR macro, except the function name is post-fixed by overload argument
#define PROXY_NAMED_OVERLOAD_PTR(type_subject, name_subject, type_return, method, overload, ...) __PROXY_IMPL_RESULT_PTR(type_subject, type_return, method, name_subject##_##method##overload, __VA_ARGS__)

/// proxy for event dispatcher functions and function to get the event dispatcher
#define PROXY_EVENT_DISPATCHER(type_subject, type_handler, method) \
    PROXY(type_subject, IEventDispatcher<type_handler>&, method);

/// proxy for event dispatcher functions and function to get the event dispatcher wit specific handler name
#define PROXY_EVENT_DISPATCHER_NAMED(type_subject, handler_type, handler_name, method) \
    PROXY(type_subject, IEventDispatcher<handler_type>&, method);

/// proxy for event dispatcher functions and function to get the event dispatcher
#define PROXY_INDEXED_EVENT_DISPATCHER(type_subject, type_handler, method) \
    PROXY(type_subject, IIndexedEventDispatcher<type_handler>&, method);

/// start of event handler proxy class 
#define PROXY_EVENT_HANDLER_BEGIN(handler_type) \
    class handler_type##Impl final : handler_type {

/// end of event handler proxy class + functions for creating/destroying proxy
#define PROXY_EVENT_HANDLER_END(handler_type, ...) \
    public: \
        handler_type##Impl(_EXPAND_ARG(void**, __VA_ARGS__)) : \
            _EXPAND_INIT(__VA_ARGS__) { } \
    }; \
    extern "C" SDK_EXPORT handler_type##Impl* __CDECL handler_type##Impl_create(_EXPAND_ARG(void**, __VA_ARGS__)) \
    { \
        return new handler_type##Impl(_EXPAND_ARG(,##__VA_ARGS__)); \
    } \
    extern "C" SDK_EXPORT void __CDECL handler_type##Impl_delete(handler_type##Impl* handler) \
    { \
        delete handler; \
    }

/// event handler function in event handler proxy class
#define PROXY_EVENT_HANDLER_EVENT(type_return, name, ...) \
    private: \
    typedef type_return(API_CALLTYPE * name##_fn)(_EXPAND_PARAM(, , __VA_ARGS__)); \
    void** name##_ = nullptr; \
    public: \
    type_return name(_EXPAND_PARAM(, , __VA_ARGS__)) override \
    { \
        return ((name##_fn)name##_)(_EXPAND_ARG(,##__VA_ARGS__)); \
    }
