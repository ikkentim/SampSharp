#include "crash-handler.hpp"

#ifdef _WIN32

#include <windows.h>
#include <dbghelp.h>
#include <psapi.h>
#include <cstdarg>
#include <cstdio>
#include <atomic>
#include <mutex>

#pragma comment(lib, "dbghelp.lib")
#pragma comment(lib, "psapi.lib")

namespace sampsharp::crash
{
    namespace
    {
        ICore* g_core = nullptr;
        std::atomic<bool> g_handling{false};
        std::atomic<bool> g_sym_initialized{false};

        const char* codeName(const DWORD code)
        {
            switch (code)
            {
            case EXCEPTION_ACCESS_VIOLATION:      return "ACCESS_VIOLATION";
            case EXCEPTION_STACK_OVERFLOW:        return "STACK_OVERFLOW";
            case EXCEPTION_ILLEGAL_INSTRUCTION:   return "ILLEGAL_INSTRUCTION";
            case EXCEPTION_PRIV_INSTRUCTION:      return "PRIV_INSTRUCTION";
            case EXCEPTION_INT_DIVIDE_BY_ZERO:    return "INT_DIVIDE_BY_ZERO";
            case EXCEPTION_INT_OVERFLOW:          return "INT_OVERFLOW";
            case EXCEPTION_FLT_DIVIDE_BY_ZERO:    return "FLT_DIVIDE_BY_ZERO";
            case EXCEPTION_ARRAY_BOUNDS_EXCEEDED: return "ARRAY_BOUNDS_EXCEEDED";
            case EXCEPTION_DATATYPE_MISALIGNMENT: return "DATATYPE_MISALIGNMENT";
            case EXCEPTION_IN_PAGE_ERROR:         return "IN_PAGE_ERROR";
            case 0xC0000374:                      return "HEAP_CORRUPTION";
            case 0xC0000409:                      return "FAST_FAIL/STACK_BUFFER_OVERRUN";
            case 0xE06D7363:                      return "C++ EXCEPTION";
            case 0xE0434352:                      return "CLR EXCEPTION";
            default:                              return "UNKNOWN";
            }
        }

        void vlog(const char* fmt, va_list ap)
        {
            char buf[4096];
            vsnprintf(buf, sizeof(buf), fmt, ap);
            buf[sizeof(buf) - 1] = 0;
            if (g_core)
                g_core->logLn(LogLevel::Error, "%s", buf);
            else
            {
                fputs(buf, stderr);
                fputc('\n', stderr);
            }
        }

        void log(const char* fmt, ...)
        {
            va_list ap;
            va_start(ap, fmt);
            vlog(fmt, ap);
            va_end(ap);
        }

        void logModuleFor(void* addr)
        {
            HMODULE mod = nullptr;
            if (!GetModuleHandleExA(
                GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS | GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
                static_cast<LPCSTR>(addr), &mod) || !mod)
            {
                log("  %p  <unknown module>", addr);
                return;
            }
            char modPath[MAX_PATH] = {};
            GetModuleFileNameA(mod, modPath, MAX_PATH);
            const char* modName = strrchr(modPath, '\\');
            modName = modName ? modName + 1 : modPath;
            const uintptr_t offset = reinterpret_cast<uintptr_t>(addr) - reinterpret_cast<uintptr_t>(mod);

            char symStorage[sizeof(SYMBOL_INFO) + MAX_SYM_NAME] = {};
            auto* sym = reinterpret_cast<SYMBOL_INFO*>(symStorage);
            sym->SizeOfStruct = sizeof(SYMBOL_INFO);
            sym->MaxNameLen = MAX_SYM_NAME;
            DWORD64 disp = 0;
            IMAGEHLP_LINE64 line{};
            line.SizeOfStruct = sizeof(IMAGEHLP_LINE64);
            DWORD lineDisp = 0;

            const DWORD64 pc = reinterpret_cast<DWORD64>(addr);
            const BOOL haveSym = SymFromAddr(GetCurrentProcess(), pc, &disp, sym);
            const BOOL haveLine = SymGetLineFromAddr64(GetCurrentProcess(), pc, &lineDisp, &line);

            if (haveSym && haveLine)
                log("  %p  %s!%s+0x%llx  (%s:%lu)", addr, modName, sym->Name,
                    static_cast<unsigned long long>(disp), line.FileName, line.LineNumber);
            else if (haveSym)
                log("  %p  %s!%s+0x%llx", addr, modName, sym->Name,
                    static_cast<unsigned long long>(disp));
            else
                log("  %p  %s+0x%llx", addr, modName,
                    static_cast<unsigned long long>(offset));
        }

        // Safely read 8 bytes from a possibly-bad address. Returns false on fault.
        // Extracted into its own function because MSVC forbids __try in functions
        // with C++ objects that have destructors (e.g. std::lock_guard below).
        bool safeRead64(const DWORD64 addr, DWORD64* out)
        {
            __try
            {
                *out = *reinterpret_cast<DWORD64*>(addr);
                return true;
            }
            __except (EXCEPTION_EXECUTE_HANDLER)
            {
                return false;
            }
        }

        void logStackTrace(CONTEXT* ctx)
        {
            static std::mutex walkMutex;
            std::lock_guard<std::mutex> lock(walkMutex);

            const HANDLE process = GetCurrentProcess();
            if (!g_sym_initialized.exchange(true))
            {
                SymSetOptions(SYMOPT_DEFERRED_LOADS | SYMOPT_LOAD_LINES | SYMOPT_UNDNAME | SYMOPT_FAIL_CRITICAL_ERRORS);
                SymInitialize(process, nullptr, TRUE);
            }
            else
            {
                SymRefreshModuleList(process);
            }

            CONTEXT walkCtx = *ctx;
            DWORD machine;
            STACKFRAME64 frame{};
#ifdef _M_X64
            machine = IMAGE_FILE_MACHINE_AMD64;

            // If PC is bogus (null-call via bad function pointer or vtable),
            // StackWalk64 can't find unwind info and bails on the first frame.
            // Recover by lifting the return address off the top of the stack
            // so we at least see the caller. [Rsp] holds the return address
            // pushed by the call that jumped to the invalid target.
            bool recovered_pc = false;
            if (walkCtx.Rip == 0 || walkCtx.Rip < 0x10000)
            {
                log("[crash] PC is invalid (0x%llx) - recovering caller from [Rsp]",
                    static_cast<unsigned long long>(walkCtx.Rip));
                DWORD64 retAddr = 0;
                if (safeRead64(walkCtx.Rsp, &retAddr))
                {
                    walkCtx.Rip = retAddr;
                    walkCtx.Rsp += sizeof(DWORD64);
                    recovered_pc = true;
                    log("[crash] recovered caller PC: 0x%llx",
                        static_cast<unsigned long long>(retAddr));
                }
                else
                {
                    log("[crash] could not read return address from stack");
                }
            }

            frame.AddrPC.Offset = walkCtx.Rip;
            frame.AddrFrame.Offset = walkCtx.Rbp;
            frame.AddrStack.Offset = walkCtx.Rsp;
#else
            machine = IMAGE_FILE_MACHINE_I386;
            bool recovered_pc = false;
            frame.AddrPC.Offset = walkCtx.Eip;
            frame.AddrFrame.Offset = walkCtx.Ebp;
            frame.AddrStack.Offset = walkCtx.Esp;
#endif
            frame.AddrPC.Mode = AddrModeFlat;
            frame.AddrFrame.Mode = AddrModeFlat;
            frame.AddrStack.Mode = AddrModeFlat;

            log("[crash] stack trace%s:", recovered_pc ? " (starting from recovered caller)" : "");
            int walked = 0;
            for (int i = 0; i < 64; ++i)
            {
                if (!StackWalk64(machine, process, GetCurrentThread(), &frame, &walkCtx,
                                 nullptr, SymFunctionTableAccess64, SymGetModuleBase64, nullptr))
                    break;
                if (frame.AddrPC.Offset == 0)
                    break;
                logModuleFor(reinterpret_cast<void*>(frame.AddrPC.Offset));
                ++walked;
            }

            // If StackWalk64 gave us nothing (bad PC, no unwind info for the
            // top frame), fall back to scanning the stack for values that look
            // like return addresses into known modules. Noisy but often the
            // only signal we get for null-call / corrupted-vtable crashes.
            if (walked == 0)
            {
                log("[crash] stack walk yielded 0 frames - scanning raw stack for return addresses:");
                const DWORD64 rsp = ctx->Rsp;
                int found = 0;
                for (int i = 0; i < 512 && found < 32; ++i)
                {
                    DWORD64 val = 0;
                    if (!safeRead64(rsp + i * sizeof(DWORD64), &val))
                        break;
                    if (val < 0x10000) continue;
                    HMODULE mod = nullptr;
                    if (GetModuleHandleExA(
                            GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS | GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
                            reinterpret_cast<LPCSTR>(val), &mod) && mod)
                    {
                        logModuleFor(reinterpret_cast<void*>(val));
                        ++found;
                    }
                }
                if (found == 0)
                    log("[crash] no return addresses found on stack");
            }
        }

        void writeMinidump(EXCEPTION_POINTERS* ep)
        {
            char path[MAX_PATH];
            SYSTEMTIME st;
            GetLocalTime(&st);
            snprintf(path, sizeof(path),
                     "sampsharp_crash_%04u%02u%02u_%02u%02u%02u_%lu.dmp",
                     st.wYear, st.wMonth, st.wDay, st.wHour, st.wMinute, st.wSecond,
                     GetCurrentProcessId());

            const HANDLE file = CreateFileA(path, GENERIC_WRITE, 0, nullptr,
                                            CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, nullptr);
            if (file == INVALID_HANDLE_VALUE)
            {
                log("[crash] failed to create minidump file '%s' (err=%lu)", path, GetLastError());
                return;
            }

            MINIDUMP_EXCEPTION_INFORMATION mei{};
            mei.ThreadId = GetCurrentThreadId();
            mei.ExceptionPointers = ep;
            mei.ClientPointers = FALSE;

            const auto type = static_cast<MINIDUMP_TYPE>(
                MiniDumpWithFullMemory |
                MiniDumpWithHandleData |
                MiniDumpWithThreadInfo |
                MiniDumpWithUnloadedModules |
                MiniDumpWithFullMemoryInfo);

            const BOOL ok = MiniDumpWriteDump(GetCurrentProcess(), GetCurrentProcessId(),
                                              file, type, ep ? &mei : nullptr, nullptr, nullptr);
            CloseHandle(file);

            if (ok)
                log("[crash] minidump written: %s", path);
            else
                log("[crash] MiniDumpWriteDump failed (err=%lu)", GetLastError());
        }

        LONG WINAPI unhandledFilter(EXCEPTION_POINTERS* ep)
        {
            bool expected = false;
            if (!g_handling.compare_exchange_strong(expected, true))
                return EXCEPTION_CONTINUE_SEARCH;

            const DWORD code = ep->ExceptionRecord->ExceptionCode;
            void* addr = ep->ExceptionRecord->ExceptionAddress;

            log("================ SampSharp CRASH ================");
            log("[crash] code=0x%08lX (%s)  addr=%p  thread=%lu  pid=%lu",
                code, codeName(code), addr,
                GetCurrentThreadId(), GetCurrentProcessId());

            if (code == EXCEPTION_ACCESS_VIOLATION && ep->ExceptionRecord->NumberParameters >= 2)
            {
                const ULONG_PTR kind = ep->ExceptionRecord->ExceptionInformation[0];
                const auto av_addr = reinterpret_cast<void*>(ep->ExceptionRecord->ExceptionInformation[1]);
                const char* verb = kind == 0 ? "read from" : kind == 1 ? "write to" : "execute (DEP) at";
                log("[crash] access violation: %s %p", verb, av_addr);
            }

            log("[crash] faulting instruction:");
            logModuleFor(addr);

            logStackTrace(ep->ContextRecord);
            writeMinidump(ep);

            if (code == 0xC0000374)
            {
                log("[crash] HEAP_CORRUPTION is a delayed symptom - the real bug wrote");
                log("[crash] out-of-bounds (or freed twice) earlier on another code path.");
                log("[crash] To pinpoint it, enable PageHeap once, reproduce, then disable:");
                log("[crash]   gflags /p /enable omp-server.exe /full");
                log("[crash]   gflags /p /disable omp-server.exe");
                log("[crash] With PageHeap the crash happens exactly at the bad write.");
            }
            log("=================================================");

            // Let the OS take it from here (WER / default termination).
            return EXCEPTION_CONTINUE_SEARCH;
        }

        LPTOP_LEVEL_EXCEPTION_FILTER g_previous_filter = nullptr;
    }

    void install(ICore* core)
    {
        g_core = core;

        // Some CRT/host code calls SetUnhandledExceptionFilter(nullptr) to
        // disable user filters. Re-arm ours and keep the previous one for
        // chaining in case another component installs its own later.
        g_previous_filter = SetUnhandledExceptionFilter(unhandledFilter);

        // Don't let WER silently swallow fatal errors without giving us a chance.
        const UINT old = SetErrorMode(0);
        SetErrorMode(old | SEM_NOGPFAULTERRORBOX | SEM_FAILCRITICALERRORS);

        if (core)
            core->logLn(LogLevel::Message, "[SampSharp] crash handler installed");
    }
}

#else // !_WIN32

namespace sampsharp::crash
{
    void install(ICore*) {}
}

#endif
