using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace SampSharp.Core.Natives.NativeObjects.NativeHandles
{
    internal class NativeHandleBasedNativeObjectProxyFactory : NativeObjectProxyFactoryBase
    {
        private readonly INativeLoader _nativeLoader;

        public NativeHandleBasedNativeObjectProxyFactory(INativeLoader nativeLoader) : base("ProxyAssembly")
        {
            _nativeLoader = nativeLoader;
        }
        
        protected override MethodBuilder CreateMethodBuilder(TypeBuilder typeBuilder, NativeIlGenContext context)
        {
            if (context.HasVarArgs)
            {
                throw new NotSupportedException("VarArgs not supported in NativeHandleBasedNativeObjectProxyFactory.");
            }

            // Find the native.
            var sizes = context.Parameters
                .Select(p => p.LengthParam)
                .Where(p => p != null)
                .Select(p => (uint) p.Index)
                .ToArray();
            var types = context.Parameters
                .Select(p => p.InputType)
                .ToArray();
            var native = _nativeLoader.Load(context.NativeName, sizes, types);

            if (native == null)
                return null;
            
            // Generate the method body.
            var methodBuilder = typeBuilder.DefineMethod(context.BaseMethod.Name, context.MethodOverrideAttributes, context.BaseMethod.ReturnType, context.MethodParameterTypes);

            Generate(methodBuilder.GetILGenerator(), context, native);

            return methodBuilder;
        }

        private void Generate(ILGenerator ilGenerator, NativeIlGenContext context, INative native)
        {
            var argsLocal = GenerateArgsArray(ilGenerator, context);

            GenerateInvokeInputCode(ilGenerator, context, argsLocal);
            GenerateHandleInvokeCode(ilGenerator, native, context, argsLocal);
            GenerateInvokeOutputCode(ilGenerator, context, argsLocal);

            // return $0
            ilGenerator.Emit(OpCodes.Ret);
        }

        private static MethodInfo GetHandleInvokerMethod(NativeIlGenContext context)
        {
            // Pick the right invoke method based on the return type of the delegate.
            MethodInfo result;
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Static;
            if (context.BaseMethod.ReturnType == typeof(int))
                result = typeof(NativeHandleInvokers).GetMethod(nameof(NativeHandleInvokers.InvokeHandle), flags);
            else if (context.BaseMethod.ReturnType == typeof(bool))
                result = typeof(NativeHandleInvokers).GetMethod(nameof(NativeHandleInvokers.InvokeHandleAsBool), flags);
            else if (context.BaseMethod.ReturnType == typeof(float))
                result = typeof(NativeHandleInvokers).GetMethod(nameof(NativeHandleInvokers.InvokeHandleAsFloat), flags);
            else if (context.BaseMethod.ReturnType == typeof(void))
                result = typeof(NativeHandleInvokers).GetMethod(nameof(NativeHandleInvokers.InvokeHandleAsVoid), flags);
            else
                throw new Exception("Unsupported return type of method");

            if (result == null)
                throw new Exception("Native invoker is missing");

            return result;
        }

        private static LocalBuilder GenerateArgsArray(ILGenerator ilGenerator, NativeIlGenContext context)
        {
            // args = new object[params.Length]
            var result = ilGenerator.DeclareLocal(typeof(object[]));
            ilGenerator.Emit(OpCodes.Ldc_I4_S, context.Parameters.Length);
            ilGenerator.Emit(OpCodes.Newarr, typeof(object));
            ilGenerator.Emit(OpCodes.Stloc, result);

            return result;
        }

        private static void GenerateHandleInvokeCode(ILGenerator ilGenerator, INative native, NativeIlGenContext context, LocalBuilder argsLocal)
        {
            // NativeHandleInvokers.InvokeHandle(handle, args)
            ilGenerator.Emit(OpCodes.Ldc_I4, native.Handle);
            ilGenerator.Emit(OpCodes.Ldloc, argsLocal);
            ilGenerator.EmitCall(GetHandleInvokerMethod(context));
        }

        private static void GenerateInvokeOutputCode(ILGenerator ilGenerator, NativeIlGenContext context, LocalBuilder argsLocal)
        {
            // Generate a pass-back for every output parameter of the native.
            foreach (var parameter in context.Parameters)
            {
                if (!parameter.Type.HasFlag(NativeParameterType.Reference))
                    continue;

                // argI = args[i];
                ilGenerator.Emit(OpCodes.Ldarg, parameter.Parameter.Position + 1);
                ilGenerator.Emit(OpCodes.Ldloc, argsLocal);
                ilGenerator.Emit(OpCodes.Ldc_I4, parameter.Index);
                ilGenerator.Emit(OpCodes.Ldelem_Ref);

                var elType = parameter.InputType.GetElementType();
                if (elType == typeof(int))
                {
                    ilGenerator.Emit(OpCodes.Unbox_Any, typeof(int));
                    ilGenerator.Emit(OpCodes.Stind_I4);
                }
                else if (elType == typeof(bool))
                {
                    ilGenerator.Emit(OpCodes.Unbox_Any, typeof(bool));
                    ilGenerator.Emit(OpCodes.Stind_I4);
                }
                else if (elType == typeof(float))
                {
                    ilGenerator.Emit(OpCodes.Unbox_Any, typeof(float));
                    ilGenerator.Emit(OpCodes.Stind_R4);
                }
                else
                    ilGenerator.Emit(OpCodes.Stind_Ref);
            }

        }

        private static void GenerateInvokeInputCode(ILGenerator ilGenerator, NativeIlGenContext context, LocalBuilder argsLocal)
        {
            foreach (var parameter in context.Parameters)
            {
                if (parameter.Property != null)
                {
                    // args[i] = this.IdentifierI;
                    ilGenerator.Emit(OpCodes.Ldloc, argsLocal);
                    ilGenerator.Emit(OpCodes.Ldc_I4, parameter.Index);
                    ilGenerator.Emit(OpCodes.Ldarg_0);
                    ilGenerator.EmitCall(/*Callvirt*/ parameter.Property.GetGetMethod(true));
                    ilGenerator.Emit(OpCodes.Box, typeof(int));
                    ilGenerator.Emit(OpCodes.Stelem_Ref);
                }
                else
                {
                    // If this parameter is of an output type no pass-trough is required; skip it.
                    if (parameter.Parameter.IsOut)
                        continue;

                    // args[i] = argI;
                    ilGenerator.Emit(OpCodes.Ldloc, argsLocal);
                    ilGenerator.Emit(OpCodes.Ldc_I4, parameter.Index);
                    ilGenerator.Emit(OpCodes.Ldarg, parameter.Parameter.Position + 1);
                    if (parameter.Parameter.ParameterType.IsValueType)
                        ilGenerator.Emit(OpCodes.Box, parameter.Parameter.ParameterType);
                    ilGenerator.Emit(OpCodes.Stelem_Ref);
                }
            }
        }
    }
}