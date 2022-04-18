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

using System;
using System.Runtime.InteropServices;

namespace SampSharp.Core.UnitTests.TestHelpers;

/// <summary>
/// Read-/writable structs compatible with SampSharp.Core.Hosting.
/// </summary>
public static class InteropStructs
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SampSharpApiRw
    {
        public uint Size;
        public PluginDataRw* PluginData;
        public delegate* unmanaged[Stdcall] <byte*, void*> FindNative;
        public delegate* unmanaged[Stdcall] <void*, byte*, int*, int> InvokeNative;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PluginDataRw
    {
        public delegate* unmanaged <byte*, byte*, void> Logprintf;

#pragma warning disable S1144 // Unused private types or members should be removed
        private readonly IntPtr _placeholder1;
        private readonly IntPtr _placeholder2;
        private readonly IntPtr _placeholder3;
        private readonly IntPtr _placeholder4;
        private readonly IntPtr _placeholder5;
        private readonly IntPtr _placeholder6;
        private readonly IntPtr _placeholder7;
        private readonly IntPtr _placeholder8;
        private readonly IntPtr _placeholder9;
        private readonly IntPtr _placeholderA;
        private readonly IntPtr _placeholderB;
        private readonly IntPtr _placeholderC;
        private readonly IntPtr _placeholderD;
        private readonly IntPtr _placeholderE;
        private readonly IntPtr _placeholderF;
#pragma warning restore S1144 // Unused private types or members should be removed

        public AmxExportRw* AmxExports;
        public delegate* unmanaged <byte*, int> CallPublicFs;
        public delegate* unmanaged <byte*, int> CallPublicGm;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AmxExportRw
    {
        public delegate* unmanaged <ushort*, ushort*> Align16;
        public delegate* unmanaged <uint*, uint*> Align32;
        public delegate* unmanaged <ulong*, ulong*> Align64;
        public delegate* unmanaged <void*, int, void*, void**, int> Allot;
        public delegate* unmanaged <void*, int, void*, void*, int> Callback;
        public delegate* unmanaged <void*, int> Cleanup;
        public delegate* unmanaged <void*, void*, void*, int> Clone;
        public delegate* unmanaged <void*, void*, int, int> Exec;
        public delegate* unmanaged <void*, void*, int*, int> FindNative;
        public delegate* unmanaged <void*, byte*, int*, int> FindPublic;
        public delegate* unmanaged <void*, byte*, void*, int> FindPubVar;
        public delegate* unmanaged <void*, int, byte*, int> FindTagId;
        public delegate* unmanaged <void*, ushort*, int> Flags;
        public delegate* unmanaged <void*, int, void**, int> GetAddr;
        public delegate* unmanaged <void*, int, byte*, int> GetNative;
        public delegate* unmanaged <void*, int, byte*, int> GetPublic;
        public delegate* unmanaged <void*, int, byte*, void*, int> GetPubVar;
        public delegate* unmanaged <void*, void*, int, int, int> GetString;
        public delegate* unmanaged <void*, int, byte*, void*, int> GetTag;
        public delegate* unmanaged <void*, long, void**, int> GetUserData;
        public delegate* unmanaged <void*, void*, int> Init;
        public delegate* unmanaged <void*, void*, void*, int> InitJIT;
        public delegate* unmanaged <void*, long*, long*, long*, int> MemInfo;
        public delegate* unmanaged <void*, int*, int> NameLength;
        public delegate* unmanaged <byte*, void*, int> NativeInfo;
        public delegate* unmanaged <void*, int*, int> NumNatives;
        public delegate* unmanaged <void*, int*, int> NumPublics;
        public delegate* unmanaged <void*, int*, int> NumPubVars;
        public delegate* unmanaged <void*, int*, int> NumTags;
        public delegate* unmanaged <void*, int, int> Push;
        public delegate* unmanaged <void*, void*, void**, void*, int, int> PushArray;
        public delegate* unmanaged <void*, void*, void**, byte*, int, int, int> PushString;
        public delegate* unmanaged <void*, int, int> RaiseError;
        public delegate* unmanaged <void*, void*, int, int> Register;
        public delegate* unmanaged <void*, int, int> Release;
        public delegate* unmanaged <void*, void*, int> SetCallback;
        public delegate* unmanaged <void*, void*, int> SetDebugHook;
        public delegate* unmanaged <void*, byte*, int, int, int, int> SetString;
        public delegate* unmanaged <void*, long, void*, int> SetUserData;
        public delegate* unmanaged <void*, int*, int> StrLen;
        public delegate* unmanaged <byte*, int*, int> UTF8Check;
        public delegate* unmanaged <byte*, byte**, void*, int> UTF8Get;
        public delegate* unmanaged <void*, int*, int> UTF8Len;
        public delegate* unmanaged <byte*, byte**, int, int, int> UTF8Put;
    }
}