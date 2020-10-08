using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using SampSharp.Core.Hosting;

namespace SampSharp.Core.Natives.NativeObjects.FastNatives
{
    public class FastNativeBasedNativeObjectProxyFactory : NativeObjectProxyFactoryBase
    {
        public FastNativeBasedNativeObjectProxyFactory() : base("ProxyAssemblyFast")
        {
        }

        protected override MethodBuilder CreateMethodBuilder(string nativeName, Type proxyType, uint[] nativeArgumentLengths,
            TypeBuilder typeBuilder, MethodInfo method, string[] identifierPropertyNames, int idIndex)
        {
            // Define the method.
            var idCount = identifierPropertyNames.Length;

            var attributes = GetMethodOverrideAttributes(method);
            var parameterTypes = GetMethodParameters(method);
            var nativeParameterTypes = GetNativeParameters(parameterTypes, idIndex, idCount);

            // Find the native.
            var native = Interop.FastNativeFind(nativeName);

            if (native == IntPtr.Zero)
                return null;
            
            var methodBuilder = typeBuilder.DefineMethod(method.Name, attributes, method.ReturnType, parameterTypes);

            var ilGenerator = methodBuilder.GetILGenerator();
            
            var data = ilGenerator.DeclareLocal(typeof(int*));
            if(data.LocalIndex != 0)
                throw new Exception("data should be local 0");
            
            var stackSize = nativeParameterTypes.Count(type => type.IsByRef
                ? type.GetElementType().IsValueType
                : type.IsValueType) + nativeParameterTypes.Length;

            // int* data = stackalloc int[n];
            ilGenerator.Emit(OpCodes.Ldc_I4, stackSize * 4);//4 bytes per cell
            ilGenerator.Emit(OpCodes.Conv_U);
            ilGenerator.Emit(OpCodes.Localloc);
            ilGenerator.Emit(OpCodes.Stloc_0);


            // Build string format
            var (formatString, requiresFormatting) = GenerateCallFormat(nativeParameterTypes);

            // Emit data assignment
            var dataIndex = nativeParameterTypes.Length;
            for (var i = 0; i < nativeParameterTypes.Length; i++)
            {
                var type = nativeParameterTypes[i];
                if (type.IsValueType || (type.IsByRef && type.GetElementType().IsValueType))// in/out int/float
                {
                    // data[i] = NativeUtils.IntPointerToInt(ptr + dataIndex);
                    ilGenerator.Emit(OpCodes.Ldloc_0);
                    if (i > 0)
                    {
                        ilGenerator.Emit(OpCodes.Ldc_I4, i * 4);
                        ilGenerator.Emit(OpCodes.Add);
                    }

                    ilGenerator.Emit(OpCodes.Ldloc_0);
                    ilGenerator.Emit(OpCodes.Ldc_I4, dataIndex * 4); // + 1 (* 4 bytes)
                    ilGenerator.Emit(OpCodes.Add);
                    ilGenerator.EmitCall(OpCodes.Call, Util(nameof(NativeUtils.IntPointerToInt)), null);
                    ilGenerator.Emit(OpCodes.Stind_I4);

                    // data[dataIndex] = argI/index
                    // TODO: object index getter
                    ilGenerator.Emit(OpCodes.Ldloc_0);
                    ilGenerator.Emit(OpCodes.Ldc_I4, dataIndex * 4);
                    ilGenerator.Emit(OpCodes.Add);
                    ilGenerator.Emit(OpCodes.Ldarg, i + 1);// arg0=this, arg1=1st arg
                    ilGenerator.Emit(OpCodes.Stind_I4);// TODO: R4? for float?

                    dataIndex++;
                }
                else if (type == typeof(string))// in string
                {
                    var strBuffer = ilGenerator.DeclareLocal(typeof(byte*));
                    var strLen = ilGenerator.DeclareLocal(typeof(int));

                    // int byteCount = NativeUtils.GetByteCount(textString);
                    ilGenerator.Emit(OpCodes.Ldarg, i + 1);
                    ilGenerator.EmitCall(OpCodes.Call, Util(nameof(NativeUtils.GetByteCount)), null);
                    ilGenerator.Emit(OpCodes.Stloc, strLen);

                    // byte* strBuffer = stackalloc byte[(int)(uint)strLen];
                    ilGenerator.Emit(OpCodes.Ldloc, strLen);
                    ilGenerator.Emit(OpCodes.Conv_U);
                    ilGenerator.Emit(OpCodes.Localloc);
                    ilGenerator.Emit(OpCodes.Stloc, strBuffer);

                    // NativeUtils.GetBytes(textString, strBuffer, strLen);
                    ilGenerator.Emit(OpCodes.Ldarg, i + 1);
                    ilGenerator.Emit(OpCodes.Ldloc, strBuffer);
                    ilGenerator.Emit(OpCodes.Ldloc, strLen);
                    ilGenerator.EmitCall(OpCodes.Call, Util(nameof(NativeUtils.GetBytes)), null);

                    // data[i] = NativeUtils.BytePointerToInt(strBuffer);
                    ilGenerator.Emit(OpCodes.Ldloc_0);
                    if (i > 0)
                    {
                        ilGenerator.Emit(OpCodes.Ldc_I4, i * 4);
                        ilGenerator.Emit(OpCodes.Add);
                    }
                    ilGenerator.Emit(OpCodes.Ldloc, strBuffer);
                    ilGenerator.EmitCall(OpCodes.Call, Util(nameof(NativeUtils.BytePointerToInt)), null);
                    ilGenerator.Emit(OpCodes.Stind_I4);
                }
                else
                {
                    throw new NotImplementedException("Native parameter type not implemented");
                }
            }
            
            // SampSharp.Core.Hosting.Interop.FastNativeInvoke(new IntPtr($0), format, ptr);
            ilGenerator.Emit(OpCodes.Ldc_I4, (int)native);
            ilGenerator.Emit(OpCodes.Newobj, typeof(IntPtr).GetConstructor(new []{typeof(int)}));
            ilGenerator.Emit(OpCodes.Ldstr, formatString);
            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.EmitCall(OpCodes.Call, typeof(Interop).GetMethod(nameof(Interop.FastNativeInvoke)), null);

            // Emit out param assignment
            // TODO: Do not allocate big strings/arrays on stack
            dataIndex = nativeParameterTypes.Length;
            for (var i = 0; i < nativeParameterTypes.Length; i++)
            {
                var type = nativeParameterTypes[i];
                if (type.IsValueType)
                {
                    dataIndex++;
                }
                else if (type.IsByRef && type.GetElementType().IsValueType)// out int/float
                {
                    ilGenerator.Emit(OpCodes.Ldarg, i + 1);
                    ilGenerator.Emit(OpCodes.Ldloc_0);
                    ilGenerator.Emit(OpCodes.Ldc_I4, dataIndex * 4);
                    ilGenerator.Emit(OpCodes.Add);
                    ilGenerator.Emit(OpCodes.Ldind_I4); // TODO: R4? for float?
                    ilGenerator.Emit(OpCodes.Stind_I4);

                    dataIndex++;
                }
                else if (type == typeof(string))
                {
                    // uses own buffer
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            EmitCast(ilGenerator, method.ReturnType);

            // return $0
            ilGenerator.Emit(OpCodes.Ret);

            return methodBuilder;
        }

        private static MethodInfo Util(string utilName)
        {
            return typeof(NativeUtils).GetMethod(utilName);
        }

        private static void EmitCast(ILGenerator ilGenerator, Type targetType)
        {
            if (targetType == typeof(void))
                ilGenerator.Emit(OpCodes.Pop);
            else if (targetType == typeof(bool))
            {
                throw new NotImplementedException();
            }
            else if (targetType == typeof(int) || targetType == typeof(float))
            {
                // no conversion
            }
            else
            {
                throw new NotImplementedException("unsupported return type of native");
            }
        }

        private (string format, bool requiresFormatting) GenerateCallFormat(Type[] nativeParameterTypes)
        {
            var formatStringBuilder = new StringBuilder();
            for (var i = 0; i < nativeParameterTypes.Length; i++)
            {
                var type = nativeParameterTypes[i];

                if (type.IsByRef)
                {
                    var elementType = type.GetElementType();

                    if (elementType == typeof(int) || elementType == typeof(float))
                    {
                        formatStringBuilder.Append("R");
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                else if (type == typeof(int) || type == typeof(float))
                {
                    formatStringBuilder.Append("d");
                }
                else if (type == typeof(string))
                {
                    formatStringBuilder.Append("s");
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            return (formatStringBuilder.ToString(), false);
        }
    }
}
