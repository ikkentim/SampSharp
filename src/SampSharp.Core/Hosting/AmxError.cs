// SampSharp
// Copyright 2022 Tim Potze
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

namespace SampSharp.Core.Hosting;

public enum AmxError
{
    AMX_ERR_NONE,
    /* reserve the first 15 error codes for exit codes of the abstract machine */
    AMX_ERR_EXIT,         /* forced exit */
    AMX_ERR_ASSERT,       /* assertion failed */
    AMX_ERR_STACKERR,     /* stack/heap collision */
    AMX_ERR_BOUNDS,       /* index out of bounds */
    AMX_ERR_MEMACCESS,    /* invalid memory access */
    AMX_ERR_INVINSTR,     /* invalid instruction */
    AMX_ERR_STACKLOW,     /* stack underflow */
    AMX_ERR_HEAPLOW,      /* heap underflow */
    AMX_ERR_CALLBACK,     /* no callback, or invalid callback */
    AMX_ERR_NATIVE,       /* native function failed */
    AMX_ERR_DIVIDE,       /* divide by zero */
    AMX_ERR_SLEEP,        /* go into sleepmode - code can be restarted */
    AMX_ERR_INVSTATE,     /* invalid state for this access */

    AMX_ERR_MEMORY = 16,  /* out of memory */
    AMX_ERR_FORMAT,       /* invalid file format */
    AMX_ERR_VERSION,      /* file is for a newer version of the AMX */
    AMX_ERR_NOTFOUND,     /* function not found */
    AMX_ERR_INDEX,        /* invalid index parameter (bad entry point) */
    AMX_ERR_DEBUG,        /* debugger cannot run */
    AMX_ERR_INIT,         /* AMX not initialized (or doubly initialized) */
    AMX_ERR_USERDATA,     /* unable to set user data field (table full) */
    AMX_ERR_INIT_JIT,     /* cannot initialize the JIT */
    AMX_ERR_PARAMS,       /* parameter error */
    AMX_ERR_DOMAIN,       /* domain error, expression result does not fit in range */
    AMX_ERR_GENERAL,      /* general error (unknown or unspecific error) */
}