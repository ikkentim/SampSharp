using System;
using System.Runtime.InteropServices;

namespace SampSharp.Core.Hosting;

[StructLayout(LayoutKind.Sequential)]
public readonly unsafe struct AmxExport
{
    /// <summary>
    /// typedef uint16_t * AMXAPI (*amx_Align16_t)(uint16_t *v);
    /// </summary>
    public readonly delegate* unmanaged <ushort*, ushort*> Align16;

    /// <summary>
    /// typedef uint32_t * AMXAPI (*amx_Align32_t)(uint32_t *v);
    /// </summary>
    public readonly delegate* unmanaged <uint*, uint*> Align32;

    /// <summary>
    /// typedef uint64_t * AMXAPI (*amx_Align64_t)(uint64_t *v);
    /// </summary>
    public readonly delegate* unmanaged <ulong*, ulong*> Align64;

    /// <summary>
    /// typedef int  AMXAPI (*amx_Allot_t)(AMX *amx, int cells, cell *amx_addr, cell **phys_addr);
    /// </summary>
    public readonly delegate* unmanaged <void*, int, void*, void**, int> Allot;

    /// <summary>
    /// typedef int  AMXAPI (*amx_Callback_t)(AMX *amx, cell index, cell *result, cell *params);
    /// </summary>
    public readonly delegate* unmanaged <void*, int, void*, void*, int> Callback;

    /// <summary>
    /// typedef int  AMXAPI (*amx_Cleanup_t)(AMX *amx);
    /// </summary>
    public readonly IntPtr Cleanup;

    /// <summary>
    /// typedef int  AMXAPI (*amx_Clone_t)(AMX *amxClone, AMX *amxSource, void *data);
    /// </summary>
    public readonly IntPtr Clone;

    /// <summary>
    /// typedef int  AMXAPI (*amx_Exec_t)(AMX *amx, cell *retval, int index);
    /// </summary>
    public readonly IntPtr Exec;

    /// <summary>
    /// typedef int  AMXAPI (*amx_FindNative_t)(AMX *amx, const char *name, int *index);
    /// </summary>
    public readonly IntPtr FindNative;

    /// <summary>
    /// typedef int  AMXAPI (*amx_FindPublic_t)(AMX *amx, const char *funcname, int *index);
    /// </summary>
    public readonly IntPtr FindPublic;

    /// <summary>
    /// typedef int  AMXAPI (*amx_FindPubVar_t)(AMX *amx, const char *varname, cell *amx_addr);
    /// </summary>
    public readonly IntPtr FindPubVar;

    /// <summary>
    /// typedef int  AMXAPI (*amx_FindTagId_t)(AMX *amx, cell tag_id, char *tagname);
    /// </summary>
    public readonly IntPtr FindTagId;

    /// <summary>
    /// typedef int  AMXAPI (*amx_Flags_t)(AMX *amx,uint16_t *flags);
    /// </summary>
    public readonly IntPtr Flags;

    /// <summary>
    /// typedef int  AMXAPI (*amx_GetAddr_t)(AMX *amx,cell amx_addr,cell **phys_addr);
    /// </summary>
    public readonly IntPtr GetAddr;

    /// <summary>
    /// typedef int  AMXAPI (*amx_GetNative_t)(AMX *amx, int index, char *funcname);
    /// </summary>
    public readonly IntPtr GetNative;

    /// <summary>
    /// typedef int  AMXAPI (*amx_GetPublic_t)(AMX *amx, int index, char *funcname);
    /// </summary>
    public readonly IntPtr GetPublic;

    /// <summary>
    /// typedef int  AMXAPI (*amx_GetPubVar_t)(AMX *amx, int index, char *varname, cell *amx_addr);
    /// </summary>
    public readonly IntPtr GetPubVar;

    /// <summary>
    /// typedef int  AMXAPI (*amx_GetString_t)(char *dest,const cell *source, int use_wchar, size_t size);
    /// </summary>
    public readonly IntPtr GetString;

    /// <summary>
    /// typedef int  AMXAPI (*amx_GetTag_t)(AMX *amx, int index, char *tagname, cell *tag_id);
    /// </summary>
    public readonly IntPtr GetTag;

    /// <summary>
    /// typedef int  AMXAPI (*amx_GetUserData_t)(AMX *amx, long tag, void **ptr);
    /// </summary>
    public readonly IntPtr GetUserData;

    /// <summary>
    /// typedef int  AMXAPI (*amx_Init_t)(AMX *amx, void *program);
    /// </summary>
    public readonly IntPtr Init;

    /// <summary>
    /// typedef int  AMXAPI (*amx_InitJIT_t)(AMX *amx, void *reloc_table, void *native_code);
    /// </summary>
    public readonly IntPtr InitJIT;

    /// <summary>
    /// typedef int  AMXAPI (*amx_MemInfo_t)(AMX *amx, long *codesize, long *datasize, long *stackheap);
    /// </summary>
    public readonly IntPtr MemInfo;

    /// <summary>
    /// typedef int  AMXAPI (*amx_NameLength_t)(AMX *amx, int *length);
    /// </summary>
    public readonly IntPtr NameLength;

    /// <summary>
    /// typedef AMX_NATIVE_INFO *  AMXAPI (*amx_NativeInfo_t)(const char *name, AMX_NATIVE func);
    /// </summary>
    public readonly IntPtr NativeInfo;

    /// <summary>
    /// typedef int  AMXAPI (*amx_NumNatives_t)(AMX *amx, int *number);
    /// </summary>
    public readonly IntPtr NumNatives;

    /// <summary>
    /// typedef int  AMXAPI (*amx_NumPublics_t)(AMX *amx, int *number);
    /// </summary>
    public readonly IntPtr NumPublics;

    /// <summary>
    /// typedef int  AMXAPI (*amx_NumPubVars_t)(AMX *amx, int *number);
    /// </summary>
    public readonly IntPtr NumPubVars;

    /// <summary>
    /// typedef int  AMXAPI (*amx_NumTags_t)(AMX *amx, int *number);
    /// </summary>
    public readonly IntPtr NumTags;

    /// <summary>
    /// typedef int  AMXAPI (*amx_Push_t)(AMX *amx, cell value);
    /// </summary>
    public readonly IntPtr Push;

    /// <summary>
    /// typedef int  AMXAPI (*amx_PushArray_t)(AMX *amx, cell *amx_addr, cell **phys_addr, const cell array[], int numcells);
    /// </summary>
    public readonly IntPtr PushArray;

    /// <summary>
    /// typedef int  AMXAPI (*amx_PushString_t)(AMX *amx, cell *amx_addr, cell **phys_addr, const char *string, int pack, int use_wchar);
    /// </summary>
    public readonly IntPtr PushString;

    /// <summary>
    /// typedef int  AMXAPI (*amx_RaiseError_t)(AMX *amx, int error);
    /// </summary>
    public readonly IntPtr RaiseError;

    /// <summary>
    /// typedef int  AMXAPI (*amx_Register_t)(AMX *amx, const AMX_NATIVE_INFO *nativelist, int number);
    /// </summary>
    public readonly IntPtr Register;

    /// <summary>
    /// typedef int  AMXAPI (*amx_Release_t)(AMX *amx, cell amx_addr);
    /// </summary>
    public readonly IntPtr Release;

    /// <summary>
    /// typedef int  AMXAPI (*amx_SetCallback_t)(AMX *amx, AMX_CALLBACK callback);
    /// </summary>
    public readonly IntPtr SetCallback;

    /// <summary>
    /// typedef int  AMXAPI (*amx_SetDebugHook_t)(AMX *amx, AMX_DEBUG debug);
    /// </summary>
    public readonly IntPtr SetDebugHook;

    /// <summary>
    /// typedef int  AMXAPI (*amx_SetString_t)(cell *dest, const char *source, int pack, int use_wchar, size_t size);
    /// </summary>
    public readonly IntPtr SetString;

    /// <summary>
    /// typedef int  AMXAPI (*amx_SetUserData_t)(AMX *amx, long tag, void *ptr);
    /// </summary>
    public readonly IntPtr SetUserData;

    /// <summary>
    /// typedef int  AMXAPI (*amx_StrLen_t)(const cell *cstring, int *length);
    /// </summary>
    public readonly IntPtr StrLen;

    /// <summary>
    /// typedef int  AMXAPI (*amx_UTF8Check_t)(const char *string, int *length);
    /// </summary>
    public readonly IntPtr UTF8Check;

    /// <summary>
    /// typedef int  AMXAPI (*amx_UTF8Get_t)(const char *string, const char **endptr, cell *value);
    /// </summary>
    public readonly IntPtr UTF8Get;

    /// <summary>
    /// typedef int  AMXAPI (*amx_UTF8Len_t)(const cell *cstr, int *length);
    /// </summary>
    public readonly IntPtr UTF8Len;

    /// <summary>
    /// typedef int  AMXAPI (*amx_UTF8Put_t)(char *string, char **endptr, int maxchars, cell value);
    /// </summary>
    public readonly IntPtr UTF8Put;
}