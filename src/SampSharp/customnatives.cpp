// SampSharp
// Copyright 2015 Tim Potze
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#include "customnatives.h"
#include <assert.h>
#include <sampgdk/sampgdk.h>
#include <mono/metadata/appdomain.h>
#include <mono/metadata/exception.h>
#include "monohelper.h"

#define MAX_NATIVE_ARGS                 (32)
#define MAX_NATIVE_ARG_FORMAT_LEN       (8)
#define ERR_EXCEPTION                   (-1)

//
// native functions
bool native_exists(MonoString *name_string) {
    char *name;

    if (!name_string) {
        mono_raise_exception(mono_get_exception_invalid_operation(
            "name cannot be null"));
        return ERR_EXCEPTION;
    }

    name = mono_string_to_utf8(name_string);

    AMX_NATIVE native = sampgdk::FindNative(name);

    return !!native;
}
int call_native_array(MonoString *name_string, MonoString *format_string,
    MonoArray *args_array, MonoArray *sizes_array) {
    char *name;
    char *format;
    char amx_format[MAX_NATIVE_ARGS * MAX_NATIVE_ARG_FORMAT_LEN] = "";
    void *params[MAX_NATIVE_ARGS];
    int param_size[MAX_NATIVE_ARGS];
    int args_count = 0;
    int size_idx = 0;

    if (!name_string) {
        mono_raise_exception(mono_get_exception_invalid_operation(
            "name cannot be null"));
        return ERR_EXCEPTION;
    }

    name = mono_string_to_utf8(name_string);
    format = !format_string ? "" : mono_string_to_utf8(format_string);

    args_count = !args_array ? 0 : mono_array_length(args_array);

    if (args_count > MAX_NATIVE_ARGS) {
        mono_raise_exception(mono_get_exception_invalid_operation(
            "too many arguments"));
        return ERR_EXCEPTION;
    }

    AMX_NATIVE native = sampgdk::FindNative(name);
    if (!native) {
        mono_raise_exception(mono_get_exception_invalid_operation(
            "native not found"));
        return ERR_EXCEPTION;
    }

    for (int i = 0; i < args_count; i++) {
        switch (format[i]) {
        case 'd': /* integer */
        case 'b': /* boolean */
            params[i] = mono_object_unbox(
                mono_array_get(args_array, MonoObject *, i));
            sprintf(amx_format, "%sd", amx_format);
            break;
        case 'f': /* floating-point */
            params[i] = &amx_ftoc(*(float *)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, i)));
            sprintf(amx_format, "%sf", amx_format);
            break;
        case 's': { /* const string */
            params[i] = monostring_to_string(
                mono_array_get(args_array, MonoString *, i));
            sprintf(amx_format, "%ss", amx_format);
            break;
        }
        case 'a': { /* array of integers */
            MonoArray *values_array =
                mono_array_get(args_array, MonoArray *, i);

            if (!sizes_array) {
                mono_raise_exception(mono_get_exception_invalid_operation(
                    "sizes cannot be null when an array or string "
                    "reference type is passed as parameter."));
                return ERR_EXCEPTION;
            }

            int size_info = mono_array_get(sizes_array, int, size_idx++);
            param_size[i] = size_info < 0 ? -size_info
                : *(int *)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, size_info));

            cell *value = new cell[param_size[i]];
            for (int j = 0; j < param_size[i]; j++) {
                value[j] = mono_array_get(values_array, int, j);
            }
            params[i] = value;

            sprintf(amx_format, "%sa[%d]", amx_format, param_size[i]);
            break;
        }
        case 'v': { /* array of floats */
            MonoArray *values_array =
                mono_array_get(args_array, MonoArray *, i);

            if (!sizes_array) {
                mono_raise_exception(mono_get_exception_invalid_operation(
                    "sizes cannot be null when an array or string "
                    "reference type is passed as parameter."));
                return ERR_EXCEPTION;
            }

            int size_info = mono_array_get(sizes_array, int, size_idx++);
            param_size[i] = size_info < 0 ? -size_info
                : *(int *)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, size_info));

            cell *value = new cell[param_size[i]];
            for (int j = 0; j<param_size[i]; j++) {
                value[j] = amx_ftoc(mono_array_get(values_array, float, j));
            }
            params[i] = value;

            sprintf(amx_format, "%sa[%d]", amx_format, param_size[i]);
            break;
        }
        case 'D': /* integer reference */
            params[i] = *(int **)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, i));
            sprintf(amx_format, "%sR", amx_format);
            break;
        case 'F': /* floating-point reference */
            params[i] = &amx_ftoc(**(float **)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, i)));
            sprintf(amx_format, "%sR", amx_format);
            break;
        case 'S': /* non-const string (writeable) */ {
            if (!sizes_array) {
                mono_raise_exception(mono_get_exception_invalid_operation(
                    "sizes cannot be null when an array or string "
                    "reference type is passed as parameter."));
                return ERR_EXCEPTION;
            }

            int size_info = mono_array_get(sizes_array, int, size_idx++);
            param_size[i] = size_info < 0 ? -size_info
                : *(int *)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, size_info));

            params[i] = new cell[param_size[i] + 1] {'\0'};

            assert(params[i]);

            sprintf(amx_format, "%sS[%d]", amx_format, param_size[i]);
            break;
        }
        case 'A': { /* array of integers reference */
            if (!sizes_array) {
                mono_raise_exception(mono_get_exception_invalid_operation(
                    "sizes cannot be null when an array or string "
                    "reference type is passed as parameter."));
                return ERR_EXCEPTION;
            }

            int size_info = mono_array_get(sizes_array, int, size_idx++);
            param_size[i] = size_info < 0 ? -size_info
                : *(int *)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, size_info));

            cell *value = new cell[param_size[i]];
            for (int j = 0; j < param_size[i]; j++) {
                /* Set default value to int.MinValue */
                value[j] = -2147483648;
            }
            params[i] = value;

            sprintf(amx_format, "%sA[%d]", amx_format, param_size[i]);
            break;
        }
        case 'V': { /* array of floating-points reference */
            if (!sizes_array) {
                mono_raise_exception(mono_get_exception_invalid_operation(
                    "sizes cannot be null when an array or string "
                    "reference type is passed as parameter."));
                return ERR_EXCEPTION;
            }

            int size_info = mono_array_get(sizes_array, int, size_idx++);
            param_size[i] = size_info < 0 ? -size_info
                : *(int *)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, size_info));

            params[i] = new cell[param_size[i]];

            sprintf(amx_format, "%sR[%d]", amx_format, param_size[i]);
            break;
        }
        default:
            mono_raise_exception(mono_get_exception_invalid_operation(
                "invalid format type"));
            return ERR_EXCEPTION;
            break;
        }

    }

    int return_value = sampgdk::InvokeNativeArray(native, amx_format, params);

    for (int i = 0; i < args_count; i++) {
        switch (format[i]) {
        case 's': /* const string */
        case 'a': /* array of integers */
        case 'v': /* array of floats */
            delete[] params[i];
            break;
        case 'D': /* integer reference */
            **(int **)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, i)) =
                *(int *)params[i];
            break;
        case 'F': /* floating-point reference */
            **(float **)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, i)) =
                amx_ctof(*(cell *)params[i]);
            break;
        case 'S': /* non-const string (writeable) */
            *mono_array_get(args_array, MonoString **, i) =
                string_to_monostring((char *)params[i], param_size[i]);

            delete[] params[i];
            break;
        case 'A': { /* array of integers reference */
            cell *param_array = (cell *)params[i];
            MonoArray *arr = mono_array_new(mono_domain_get(),
                mono_get_int32_class(), param_size[i]);
            for (int j = 0; j < param_size[i]; j++) {
                mono_array_set(arr, int, j, param_array[j]);
            }
            *mono_array_get(args_array, MonoArray **, i) = arr;

            delete[] params[i];
            break;
        }
        case 'V': { /* array of floating-points reference */
            cell *param_array = (cell *)params[i];

            MonoArray *arr = mono_array_new(mono_domain_get(),
                mono_get_single_class(), param_size[i]);
            for (int j = 0; j<param_size[i]; j++) {
                mono_array_set(arr, float, j, amx_ctof(param_array[j]));
            }
            *mono_array_get(args_array, MonoArray **, i) = arr;

            delete[] params[i];
            break;
        }
        }
    }

    return return_value;
}
