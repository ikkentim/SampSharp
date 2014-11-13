#include <mono/jit/jit.h>

#pragma once

char *monostring_to_string(MonoString *string_obj);
MonoString *string_to_monostring(char *str, int len);