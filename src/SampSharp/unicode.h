#include <mono/jit/jit.h>

#pragma once

char get_char_from_unicode(mono_unichar2 ch);
mono_unichar2 get_unicode_from_char(char ch);
void set_codepage(int page);