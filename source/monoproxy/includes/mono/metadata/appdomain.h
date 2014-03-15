/*
 * appdomain.h: AppDomain functions
 *
 * Author:
 *	Dietmar Maurer (dietmar@ximian.com)
 *
 * (C) 2001 Ximian, Inc.
 */

#ifndef _MONO_METADATA_APPDOMAIN_H_
#define _MONO_METADATA_APPDOMAIN_H_

#include <mono/utils/mono-publib.h>

#include <mono/metadata/object.h>
#include <mono/metadata/reflection.h>

MONO_BEGIN_DECLS

typedef void (*MonoThreadStartCB) (intptr_t tid, void* stack_start,
				   void* func);
typedef void (*MonoThreadAttachCB) (intptr_t tid, void* stack_start);

typedef struct _MonoAppDomain MonoAppDomain;
typedef struct _MonoJitInfo MonoJitInfo;

typedef void (*MonoDomainFunc) (MonoDomain *domain, void* user_data);

MonoDomain*
mono_init                  (const char *filename);

MonoDomain *
mono_init_from_assembly    (const char *domain_name, const char *filename);

MonoDomain *
mono_init_version          (const char *domain_name, const char *version);

MonoDomain*
mono_get_root_domain       (void);

void
mono_runtime_init          (MonoDomain *domain, MonoThreadStartCB start_cb,
			    MonoThreadAttachCB attach_cb);

void
mono_runtime_cleanup       (MonoDomain *domain);

void
mono_install_runtime_cleanup (MonoDomainFunc func);

void
mono_runtime_quit (void);

void
mono_runtime_set_shutting_down (void);

mono_bool
mono_runtime_is_shutting_down (void);

const char*
mono_check_corlib_version (void);

MonoDomain *
mono_domain_create         (void);

MonoDomain *
mono_domain_create_appdomain (char *friendly_name, char *configuration_file);

MonoDomain *
mono_domain_get            (void);

MonoDomain *
mono_domain_get_by_id      (int32_t domainid);

int32_t
mono_domain_get_id         (MonoDomain *domain);

mono_bool
mono_domain_set            (MonoDomain *domain, mono_bool force);

void
mono_domain_set_internal   (MonoDomain *domain);

void
mono_domain_unload (MonoDomain *domain);

void
mono_domain_try_unload (MonoDomain *domain, MonoObject **exc);

mono_bool
mono_domain_is_unloading   (MonoDomain *domain);

MonoDomain *
mono_domain_from_appdomain (MonoAppDomain *appdomain);

void
mono_domain_foreach        (MonoDomainFunc func, void* user_data);

MonoAssembly *
mono_domain_assembly_open  (MonoDomain *domain, const char *name);

mono_bool
mono_domain_finalize       (MonoDomain *domain, uint32_t timeout);

void
mono_domain_free           (MonoDomain *domain, mono_bool force);

mono_bool
mono_domain_has_type_resolve (MonoDomain *domain);

MonoReflectionAssembly *
mono_domain_try_type_resolve (MonoDomain *domain, char *name, MonoObject *tb);

mono_bool
mono_domain_owns_vtable_slot (MonoDomain *domain, void* vtable_slot);

void
mono_context_init 				   (MonoDomain *domain);

void 
mono_context_set				   (MonoAppContext *new_context);

MonoAppContext * 
mono_context_get				   (void);

MonoJitInfo *
mono_jit_info_table_find   (MonoDomain *domain, char *addr);

/* MonoJitInfo accessors */

void*
mono_jit_info_get_code_start (MonoJitInfo* ji);

int
mono_jit_info_get_code_size (MonoJitInfo* ji);

MonoMethod*
mono_jit_info_get_method (MonoJitInfo* ji);


MonoImage*
mono_get_corlib            (void);

MonoClass*
mono_get_object_class      (void);

MonoClass*
mono_get_byte_class        (void);

MonoClass*
mono_get_void_class        (void);

MonoClass*
mono_get_boolean_class     (void);

MonoClass*
mono_get_sbyte_class       (void);

MonoClass*
mono_get_int16_class       (void);

MonoClass*
mono_get_uint16_class      (void);

MonoClass*
mono_get_int32_class       (void);

MonoClass*
mono_get_uint32_class      (void);

MonoClass*
mono_get_intptr_class         (void);

MonoClass*
mono_get_uintptr_class        (void);

MonoClass*
mono_get_int64_class       (void);

MonoClass*
mono_get_uint64_class      (void);

MonoClass*
mono_get_single_class      (void);

MonoClass*
mono_get_double_class      (void);

MonoClass*
mono_get_char_class        (void);

MonoClass*
mono_get_string_class      (void);

MonoClass*
mono_get_enum_class        (void);

MonoClass*
mono_get_array_class       (void);

MonoClass*
mono_get_thread_class       (void);

MonoClass*
mono_get_exception_class    (void);

void
mono_security_enable_core_clr (void);

typedef mono_bool (*MonoCoreClrPlatformCB) (const char *image_name);

void
mono_security_set_core_clr_platform_callback (MonoCoreClrPlatformCB callback);

MONO_END_DECLS

#endif /* _MONO_METADATA_APPDOMAIN_H_ */

