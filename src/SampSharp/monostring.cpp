#include "monostring.h"
#include "unicode.h"
#include <mono/jit/jit.h>
#include <string>

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
    return mono_string_from_utf16(buffer);
}