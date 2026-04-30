#include "compat.hpp"

#ifdef WINDOWS

#include <stdexcept>

std::wstring widen_impl(std::string const &in)
{
    std::wstring out{};

    if (!in.empty())
    {
        const int inSize = static_cast<int>(in.size());

        const int len = MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS,
                                      in.c_str(), inSize, nullptr, 0);
        if ( len == 0 )
        {
            throw std::runtime_error("Invalid character sequence.");
        }

        out.resize(len);
        MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS,
                            in.c_str(), inSize, out.data(), static_cast<int>(out.size()));
    }

    return out;
}

#endif
