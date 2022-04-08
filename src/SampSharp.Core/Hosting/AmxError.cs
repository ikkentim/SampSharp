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

/// <summary>
/// Contains error codes used by Pawn AMX.
/// </summary>
public enum AmxError
{
    /// <summary>
    /// No error.
    /// </summary>
    None,

    /// <summary>
    /// Exit code: forced exit.
    /// </summary>
    Exit,

    /// <summary>
    /// Exit code: assertion failed.
    /// </summary>
    Assert,

    /// <summary>
    /// Exit code: stack/heap collision.
    /// </summary>
    Stackerr,

    /// <summary>
    /// Exit code: index out of bounds.
    /// </summary>
    Bounds,

    /// <summary>
    /// Exit code: invalid memory access.
    /// </summary>
    Memaccess,

    /// <summary>
    /// Exit code: invalid instruction.
    /// </summary>
    Invinstr,

    /// <summary>
    /// Exit code: stack underflow.
    /// </summary>
    Stacklow,

    /// <summary>
    /// Exit code: heap underflow.
    /// </summary>
    Heaplow,

    /// <summary>
    /// Exit code: no callback, or invalid callback.
    /// </summary>
    Callback,

    /// <summary>
    /// Exit code: native function failed
    /// </summary>
    Native,

    /// <summary>
    /// Exit code: divide by zero
    /// </summary>
    Divide,

    /// <summary>
    /// Exit code: go into sleepmode - code can be restarted
    /// </summary>
    Sleep,

    /// <summary>
    /// Exit code: invalid state for this access
    /// </summary>
    Invstate,

    /// <summary>
    /// Out of memory.
    /// </summary>
    Memory = 16,

    /// <summary>
    /// Invalid file format.
    /// </summary>
    Format,

    /// <summary>
    /// File is for a newer version of the AMX.
    /// </summary>
    Version,

    /// <summary>
    /// Function not found.
    /// </summary>
    Notfound,

    /// <summary>
    /// Invalid index parameter (bad entry point).
    /// </summary>
    Index,

    /// <summary>
    /// Debugger cannot run.
    /// </summary>
    Debug,

    /// <summary>
    /// AMX not initialized (or doubly initialized).
    /// </summary>
    Init,

    /// <summary>
    /// Unable to set user data field (table full)
    /// </summary>
    Userdata,

    /// <summary>
    /// Cannot initialize the JIT.
    /// </summary>
    InitJit,

    /// <summary>
    /// Parameter error.
    /// </summary>
    Params,

    /// <summary>
    /// Domain error, expression result does not fit in range.
    /// </summary>
    Domain,

    /// <summary>
    /// General error (unknown or unspecific error).
    /// </summary>
    General,
}