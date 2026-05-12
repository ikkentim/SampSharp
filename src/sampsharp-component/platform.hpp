
// On non-Windows platforms, __CDECL (from the SDK's types.hpp) expands to
// __attribute__((__cdecl__)) which is only meaningful on 32-bit x86 and
// generates -Wattributes warnings on x86_64 Linux builds. Override to empty.
#if !defined(_WIN32)
#ifdef __CDECL
#undef __CDECL
#endif
#define __CDECL
#endif

#define API_CALLTYPE __CDECL