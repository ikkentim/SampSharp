using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace SampSharp.Core.Hosting;

[StructLayout(LayoutKind.Sequential)]
[SuppressMessage("ReSharper", "CommentTypo")]
[SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "ReSharper")]
public readonly unsafe struct AmxExport
{
    /// <summary>
    /// typedef uint16_t * (*amx_Align16_t)(uint16_t *v);
    /// </summary>
    public readonly delegate* unmanaged <ushort*, ushort*> Align16;

    /// <summary>
    /// typedef uint32_t * (*amx_Align32_t)(uint32_t *v);
    /// </summary>
    public readonly delegate* unmanaged <uint*, uint*> Align32;

    /// <summary>
    /// typedef uint64_t * (*amx_Align64_t)(uint64_t *v);
    /// </summary>
    public readonly delegate* unmanaged <ulong*, ulong*> Align64;

    /// <summary>
    /// typedef int (*amx_Allot_t)(AMX *amx, int cells, cell *amx_addr, cell **phys_addr);
    /// </summary>
    public readonly delegate* unmanaged <void*, int, void*, void**, int> Allot;

    /// <summary>
    /// typedef int (*amx_Callback_t)(AMX *amx, cell index, cell *result, cell *params);
    /// </summary>
    public readonly delegate* unmanaged <void*, int, void*, void*, int> Callback;

    /// <summary>
    /// typedef int (*amx_Cleanup_t)(AMX *amx);
    /// </summary>
    public readonly delegate* unmanaged <void*, int> Cleanup;

    /// <summary>
    /// typedef int (*amx_Clone_t)(AMX *amxClone, AMX *amxSource, void *data);
    /// </summary>
    public readonly delegate* unmanaged <void*, void*, void *, int> Clone;

    /// <summary>
    /// typedef int (*amx_Exec_t)(AMX *amx, cell *retval, int index);
    /// </summary>
    public readonly delegate* unmanaged <void*, void *, int, int> Exec;

    /// <summary>
    /// typedef int (*amx_FindNative_t)(AMX *amx, const char *name, int *index);
    /// </summary>
    public readonly delegate* unmanaged <void*, void*, int*, int> FindNative;

    /// <summary>
    /// typedef int (*amx_FindPublic_t)(AMX *amx, const char *funcname, int *index);
    /// </summary>
    public readonly delegate* unmanaged <void*, byte*, int*, int> FindPublic;

    /// <summary>
    /// typedef int (*amx_FindPubVar_t)(AMX *amx, const char *varname, cell *amx_addr);
    /// </summary>
    public readonly delegate* unmanaged <void*, byte*, void *, int> FindPubVar;

    /// <summary>
    /// typedef int (*amx_FindTagId_t)(AMX *amx, cell tag_id, char *tagname);
    /// </summary>
    public readonly delegate* unmanaged <void*, int, byte*, int> FindTagId;

    /// <summary>
    /// typedef int (*amx_Flags_t)(AMX *amx,uint16_t *flags);
    /// </summary>
    public readonly delegate* unmanaged <void*, ushort *, int> Flags;

    /// <summary>
    /// typedef int (*amx_GetAddr_t)(AMX *amx,cell amx_addr,cell **phys_addr);
    /// </summary>
    public readonly delegate* unmanaged <void*, int, void **, int> GetAddr;

    /// <summary>
    /// typedef int (*amx_GetNative_t)(AMX *amx, int index, char *funcname);
    /// </summary>
    public readonly delegate* unmanaged <void*, int, byte*, int> GetNative;

    /// <summary>
    /// typedef int (*amx_GetPublic_t)(AMX *amx, int index, char *funcname);
    /// </summary>
    public readonly delegate* unmanaged <void*, int, byte*, int> GetPublic;

    /// <summary>
    /// typedef int (*amx_GetPubVar_t)(AMX *amx, int index, char *varname, cell *amx_addr);
    /// </summary>
    public readonly delegate* unmanaged <void*, int, byte*, void*, int> GetPubVar;

    /// <summary>
    /// typedef int (*amx_GetString_t)(char *dest,const cell *source, int use_wchar, size_t size);
    /// </summary>
    public readonly delegate* unmanaged <void*, void*, int, int, int> GetString;

    /// <summary>
    /// typedef int (*amx_GetTag_t)(AMX *amx, int index, char *tagname, cell *tag_id);
    /// </summary>
    public readonly delegate* unmanaged <void*, int, byte*, void *, int> GetTag;

    /// <summary>
    /// typedef int (*amx_GetUserData_t)(AMX *amx, long tag, void **ptr);
    /// </summary>
    public readonly delegate* unmanaged <void*, long, void**, int> GetUserData;

    /// <summary>
    /// typedef int (*amx_Init_t)(AMX *amx, void *program);
    /// </summary>
    public readonly delegate* unmanaged <void*, void*, int> Init;

    /// <summary>
    /// typedef int (*amx_InitJIT_t)(AMX *amx, void *reloc_table, void *native_code);
    /// </summary>
    public readonly delegate* unmanaged <void*, void*, void*, int> InitJIT;

    /// <summary>
    /// typedef int (*amx_MemInfo_t)(AMX *amx, long *codesize, long *datasize, long *stackheap);
    /// </summary>
    public readonly delegate* unmanaged <void*, long*, long*, long*, int> MemInfo;

    /// <summary>
    /// typedef int (*amx_NameLength_t)(AMX *amx, int *length);
    /// </summary>
    public readonly delegate* unmanaged <void*, int*, int> NameLength;

    /// <summary>
    /// typedef AMX_NATIVE_INFO *  AMXAPI (*amx_NativeInfo_t)(const char *name, AMX_NATIVE func);
    /// </summary>
    public readonly delegate* unmanaged <byte*, void*, int> NativeInfo;

    /// <summary>
    /// typedef int (*amx_NumNatives_t)(AMX *amx, int *number);
    /// </summary>
    public readonly delegate* unmanaged <void*, int*, int> NumNatives;

    /// <summary>
    /// typedef int (*amx_NumPublics_t)(AMX *amx, int *number);
    /// </summary>
    public readonly delegate* unmanaged <void*, int*, int> NumPublics;

    /// <summary>
    /// typedef int (*amx_NumPubVars_t)(AMX *amx, int *number);
    /// </summary>
    public readonly delegate* unmanaged <void*, int*, int> NumPubVars;

    /// <summary>
    /// typedef int (*amx_NumTags_t)(AMX *amx, int *number);
    /// </summary>
    public readonly delegate* unmanaged <void*, int*, int> NumTags;

    /// <summary>
    /// typedef int (*amx_Push_t)(AMX *amx, cell value);
    /// </summary>
    public readonly delegate* unmanaged <void*, int, int> Push;

    /// <summary>
    /// typedef int (*amx_PushArray_t)(AMX *amx, cell *amx_addr, cell **phys_addr, const cell array[], int numcells);
    /// </summary>
    public readonly delegate* unmanaged <void*, void*, void**, void*, int, int> PushArray;

    /// <summary>
    /// typedef int (*amx_PushString_t)(AMX *amx, cell *amx_addr, cell **phys_addr, const char *string, int pack, int use_wchar);
    /// </summary>
    public readonly delegate* unmanaged <void*, void*, void**, byte*, int,int, int> PushString;

    /// <summary>
    /// typedef int (*amx_RaiseError_t)(AMX *amx, int error);
    /// </summary>
    public readonly delegate* unmanaged <void*, int, int> RaiseError;

    /// <summary>
    /// typedef int (*amx_Register_t)(AMX *amx, const AMX_NATIVE_INFO *nativelist, int number);
    /// </summary>
    public readonly delegate* unmanaged <void*, void*, int, int> Register;

    /// <summary>
    /// typedef int (*amx_Release_t)(AMX *amx, cell amx_addr);
    /// </summary>
    public readonly delegate* unmanaged <void*, int, int> Release;

    /// <summary>
    /// typedef int (*amx_SetCallback_t)(AMX *amx, AMX_CALLBACK callback);
    /// </summary>
    public readonly delegate* unmanaged <void*, void*, int> SetCallback;

    /// <summary>
    /// typedef int (*amx_SetDebugHook_t)(AMX *amx, AMX_DEBUG debug);
    /// </summary>
    public readonly delegate* unmanaged <void*, void*, int> SetDebugHook;

    /// <summary>
    /// typedef int (*amx_SetString_t)(cell *dest, const char *source, int pack, int use_wchar, size_t size);
    /// </summary>
    public readonly delegate* unmanaged <void*, byte*, int, int, int, int> SetString;

    /// <summary>
    /// typedef int (*amx_SetUserData_t)(AMX *amx, long tag, void *ptr);
    /// </summary>
    public readonly delegate* unmanaged <void*, long, void*, int> SetUserData;

    /// <summary>
    /// typedef int (*amx_StrLen_t)(const cell *cstring, int *length);
    /// </summary>
    public readonly delegate* unmanaged <void*, int*, int> StrLen;

    /// <summary>
    /// typedef int (*amx_UTF8Check_t)(const char *string, int *length);
    /// </summary>
    public readonly delegate* unmanaged <byte*, int*, int> UTF8Check;

    /// <summary>
    /// typedef int (*amx_UTF8Get_t)(const char *string, const char **endptr, cell *value);
    /// </summary>
    public readonly delegate* unmanaged <byte*, byte**, void*, int> UTF8Get;

    /// <summary>
    /// typedef int (*amx_UTF8Len_t)(const cell *cstr, int *length);
    /// </summary>
    public readonly delegate* unmanaged <void*, int*, int> UTF8Len;

    /// <summary>
    /// typedef int (*amx_UTF8Put_t)(char *string, char **endptr, int maxchars, cell value);
    /// </summary>
    public readonly delegate* unmanaged <byte*, byte**, int, int, int> UTF8Put;
}