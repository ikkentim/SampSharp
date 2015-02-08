#include "monohelper.h"

#include <string>
#include <assert.h>
#include "PathUtil.h"
#include "unicode.h"

void mono_convert_symbols(const char * path) {
    std::string converter_path_string = PathUtil::GetLibDirectory()
        .append("mono/4.5/pdb2mdb.exe");

    char *converter_path = new char[converter_path_string.size() + 1];

    converter_path[converter_path_string.size()] = 0;
    memcpy(converter_path, converter_path_string.c_str(),
        converter_path_string.size());

    MonoAssembly *converter_assembly = mono_domain_assembly_open(
        mono_domain_get(), converter_path);

    assert(converter_assembly);

    char * argv[2];
    argv[0] = (char *)converter_path;
    argv[1] = (char *)path;
    mono_jit_exec(mono_domain_get(), converter_assembly, 2, argv);
}

char *monostring_to_string(MonoString *string_obj)
{
    mono_unichar2 *uni_buffer = mono_string_chars(string_obj);
    int len = mono_string_length(string_obj);

    char *buffer = new char[len + 1];
    for (int i = 0; i < len; i++) {
        buffer[i] = get_char_from_unicode(uni_buffer[i]);
    }
    buffer[len] = '\0';

    return buffer;
}

MonoString *string_to_monostring(char *str, int len)
{
    mono_unichar2 *buffer = new mono_unichar2[len + 1];
    for (int i = 0; i < len; i++) {
        buffer[i] = get_unicode_from_char(str[i]);

        if (str[i] == '\0') {
            return mono_string_from_utf16(buffer);
        }
    }
    buffer[len] = '\0';

    MonoString *result = mono_string_from_utf16(buffer);
    delete[] buffer;
    return result;
}