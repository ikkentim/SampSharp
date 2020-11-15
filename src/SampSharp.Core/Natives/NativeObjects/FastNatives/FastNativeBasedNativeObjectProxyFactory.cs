using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using SampSharp.Core.Hosting;

namespace SampSharp.Core.Natives.NativeObjects.FastNatives
{
    /// <summary>
    /// Represents a factory for native proxies based on fast native interop.
    /// </summary>
    public class FastNativeBasedNativeObjectProxyFactory : NativeObjectProxyFactoryBase
    {
        private const int MaxStackAllocSize = 128;
        private readonly IGameModeClient _gameModeClient;
        
        /// <inheritdoc />
        public FastNativeBasedNativeObjectProxyFactory(IGameModeClient gameModeClient) : base(gameModeClient, "ProxyAssemblyFast")
        {
            _gameModeClient = gameModeClient;
        }
        
        /// <inheritdoc />
        protected override object[] GetProxyConstructorArgs()
        {
            return new object[] {_gameModeClient.SynchronizationProvider};
        }

        /// <inheritdoc />
        protected override FieldInfo[] GetProxyFields(TypeBuilder typeBuilder)
        {
            var syncField = typeBuilder.DefineField("_synchronizationProvider", typeof(ISynchronizationProvider), FieldAttributes.Private);
            return new[]
            {
                (FieldInfo) syncField
            };
        }

        /// <inheritdoc />
        protected override MethodBuilder CreateMethodBuilder(TypeBuilder typeBuilder, NativeIlGenContext context)
        {
            // Find the native.
            var native = Interop.FastNativeFind(context.NativeName);

            if (native == IntPtr.Zero)
                return null;

            var methodBuilder = typeBuilder.DefineMethod(context.BaseMethod.Name, context.MethodOverrideAttributes, context.BaseMethod.ReturnType, context.MethodParameterTypes);
            var ilGenerator = methodBuilder.GetILGenerator();
            
            Generate(ilGenerator, native, context);

            return methodBuilder;
        }

        private static void Generate(ILGenerator ilGenerator, IntPtr native, NativeIlGenContext context)
        {
            // Will be using direct reference to loc_0 for optimized calls to data buffer
            var data = ilGenerator.DeclareLocal(typeof(int*));
            if (data.LocalIndex != 0)
                throw new Exception("data should be local 0");

            // Data buffer will contain all param references + value type values
            var stackSize = context.Parameters.Length +
                            context.Parameters.Count(param => (param.Type & NativeParameterType.ValueTypeMask) != 0);

            // int* data = stackalloc int[n];
            ilGenerator.Emit(OpCodes.Ldc_I4, stackSize * 4); //4 bytes per cell
            ilGenerator.Emit(OpCodes.Conv_U);
            ilGenerator.Emit(OpCodes.Localloc);
            ilGenerator.Emit(OpCodes.Stloc_0);

            EmitInParamAssignment(ilGenerator, context, out var paramBuffers);
            EmitInvokeNative(ilGenerator, native, context);
            EmitOutParamAssignment(ilGenerator, context, paramBuffers);
            EmitReturnCast(ilGenerator, context);

            // return $0
            ilGenerator.Emit(OpCodes.Ret);
        }

        private static void EmitInvokeNative(ILGenerator ilGenerator, IntPtr native, NativeIlGenContext context)
        {
            var formatString = GenerateCallFormat(context);
            // if (_synchronizationProvider.InvokeRequired)
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldfld, context.ProxyGeneratedFields[0]);
            ilGenerator.EmitPropertyGetterCall<ISynchronizationProvider>(OpCodes.Callvirt, x => x.InvokeRequired);

            var elseLabel = ilGenerator.DefineLabel();
            var endifLabel = ilGenerator.DefineLabel();
            ilGenerator.Emit(OpCodes.Brfalse, elseLabel);

            // NativeUtils.SynchronizeInvoke(_synchronizationProvider, new IntPtr($0), format, ptr);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldfld, context.ProxyGeneratedFields[0]);
            ilGenerator.Emit(OpCodes.Ldc_I4, (int) native);
            ilGenerator.Emit(OpCodes.Newobj, typeof(IntPtr).GetConstructor(new[] {typeof(int)}));
            ilGenerator.Emit(OpCodes.Ldstr, formatString);
            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.EmitCall(typeof(NativeUtils), nameof(NativeUtils.SynchronizeInvoke));

            // else
            ilGenerator.Emit(OpCodes.Br, endifLabel);
            ilGenerator.MarkLabel(elseLabel);

            // SampSharp.Core.Hosting.Interop.FastNativeInvoke(new IntPtr($0), format, ptr);
            ilGenerator.Emit(OpCodes.Ldc_I4, (int) native);
            ilGenerator.Emit(OpCodes.Newobj, typeof(IntPtr).GetConstructor(new[] {typeof(int)}));
            ilGenerator.Emit(OpCodes.Ldstr, formatString);
            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.EmitCall(typeof(Interop), nameof(Interop.FastNativeInvoke));

            // endif
            ilGenerator.Emit(OpCodes.Br, endifLabel);
            ilGenerator.MarkLabel(endifLabel);
        }

        private static void EmitInParamAssignment(ILGenerator ilGenerator, NativeIlGenContext context, out LocalBuilder[] paramBuffers)
        {
            paramBuffers = new LocalBuilder[context.Parameters.Length];

            var dataIndex = context.Parameters.Length;
            for (var i = 0; i < context.Parameters.Length; i++)
            {
                void EmitBufferLocation()
                {
                    ilGenerator.Emit(OpCodes.Ldloc_0);
                    if (i > 0)
                    {
                        ilGenerator.Emit(OpCodes.Ldc_I4, i * 4);
                        ilGenerator.Emit(OpCodes.Add);
                    }
                }

                var param = context.Parameters[i];
                if ((param.Type & NativeParameterType.ValueTypeMask) != 0) // in/out int/float
                {
                    // data[i] = NativeUtils.IntPointerToInt(ptr + dataIndex);
                    EmitBufferLocation();

                    ilGenerator.Emit(OpCodes.Ldloc_0);
                    ilGenerator.Emit(OpCodes.Ldc_I4, dataIndex * 4); // + 1 (* 4 bytes)
                    ilGenerator.Emit(OpCodes.Add);
                    ilGenerator.EmitCall(OpCodes.Call, typeof(NativeUtils), nameof(NativeUtils.IntPointerToInt));
                    ilGenerator.Emit(OpCodes.Stind_I4);

                    if (param.Property != null)
                    {
                        // data[dataIndex] = Property;
                        ilGenerator.Emit(OpCodes.Ldloc_0);
                        ilGenerator.Emit(OpCodes.Ldc_I4, dataIndex * 4);
                        ilGenerator.Emit(OpCodes.Add);
                        ilGenerator.Emit(OpCodes.Ldarg_0);
                        ilGenerator.EmitCall(param.Property.GetMethod);
                        EmitConvertToInt(ilGenerator, param.Type);
                        ilGenerator.Emit(OpCodes.Stind_I4);
                    }

                    if (!param.Parameter.IsOut)
                    {
                        // data[dataIndex] = argI
                        ilGenerator.Emit(OpCodes.Ldloc_0);
                        ilGenerator.Emit(OpCodes.Ldc_I4, dataIndex * 4);
                        ilGenerator.Emit(OpCodes.Add);
                        ilGenerator.Emit(OpCodes.Ldarg, i + 1); // arg0=this, arg1=1st arg
                        EmitConvertToInt(ilGenerator, param.Type);
                        ilGenerator.Emit(OpCodes.Stind_I4);
                    }

                    dataIndex++;
                }
                else if (param.Type == NativeParameterType.String) // in string
                {
                    if (param.Property != null)
                    {
                        throw new Exception("Unsupported identifier property type");
                    }

                    var strLen = ilGenerator.DeclareLocal(typeof(int));

                    // int byteCount = NativeUtils.GetByteCount(textString);
                    ilGenerator.Emit(OpCodes.Ldarg, i + 1);
                    ilGenerator.EmitCall(typeof(NativeUtils), nameof(NativeUtils.GetByteCount));
                    ilGenerator.Emit(OpCodes.Stloc, strLen);

                    // Span<byte> strBuffer = stackalloc/new byte[...]
                    var strBufferSpan = EmitSpanAlloc(ilGenerator, strLen);

                    // NativeUtils.GetBytes(textString, strBuffer);
                    ilGenerator.Emit(OpCodes.Ldarg, i + 1);
                    ilGenerator.Emit(OpCodes.Ldloc, strBufferSpan);
                    ilGenerator.EmitCall(typeof(NativeUtils), nameof(NativeUtils.GetBytes2));

                    // data[i] = NativeUtils.BytePointerToInt(strBuffer);
                    EmitBufferLocation(); // data[i]
                    EmitSpanToPointer(ilGenerator, strBufferSpan);
                    ilGenerator.EmitCall(typeof(NativeUtils), nameof(NativeUtils.BytePointerToInt));
                    ilGenerator.Emit(OpCodes.Stind_I4);
                }
                else if (param.Type == NativeParameterType.StringReference)
                {
                    EmitThrowOnOutOfRangeLength(ilGenerator, param);

                    // var strBuf = stackalloc/new byte[...]
                    var strBuf = EmitSpanAlloc(ilGenerator, param.LengthParam.Index + 1);
                    paramBuffers[i] = strBuf;

                    // data[i] = NativeUtils.BytePointerToInt(strBufPtr);
                    EmitBufferLocation(); // data[i]
                    EmitSpanToPointer(ilGenerator, strBuf);
                    ilGenerator.EmitCall(typeof(NativeUtils), nameof(NativeUtils.BytePointerToInt));
                    ilGenerator.Emit(OpCodes.Stind_I4);
                }
                else if (param.Type.HasFlag(NativeParameterType.Array))
                {
                    if (param.Property != null)
                    {
                        throw new Exception("Unsupported identifier property type");
                    }

                    throw new NotImplementedException("Arrays not implemented");
                }
                else
                {
                    throw new Exception("Unknown native parameter type");
                }
            }
        }

        private static void EmitSpanToPointer(ILGenerator ilGenerator, LocalBuilder strBuf)
        {
            var local = EmitSpanToPointerLocal(ilGenerator, strBuf);
            ilGenerator.Emit(OpCodes.Ldloc, local);
        }

        private static LocalBuilder EmitSpanToPointerLocal(ILGenerator ilGenerator, LocalBuilder strBuf)
        {
            // fixed(byte* strBufPtr = strBuf)
            var strBufPinned = ilGenerator.DeclareLocal(typeof(byte*), true);
            var strBufPtr = ilGenerator.DeclareLocal(typeof(byte*));

            ilGenerator.Emit(OpCodes.Ldloca, strBuf);
            ilGenerator.EmitCall(typeof(Span<byte>), nameof(Span<byte>.GetPinnableReference));
            ilGenerator.Emit(OpCodes.Stloc, strBufPinned);
            ilGenerator.Emit(OpCodes.Ldloc, strBufPinned);
            ilGenerator.Emit(OpCodes.Conv_U);
            ilGenerator.Emit(OpCodes.Stloc, strBufPtr);
            return strBufPtr;
        }

        private static void EmitThrowOnOutOfRangeLength(ILGenerator ilGenerator, NativeIlGenParam param)
        {
            // if (strlen <= 0)
            ilGenerator.Emit(OpCodes.Ldarg, param.LengthParam.Index + 1);
            ilGenerator.Emit(OpCodes.Ldc_I4_0);
            ilGenerator.Emit(OpCodes.Cgt);
            ilGenerator.Emit(OpCodes.Ldc_I4_0);
            ilGenerator.Emit(OpCodes.Ceq);
            var falseLabel = ilGenerator.DefineLabel();
            ilGenerator.Emit(OpCodes.Brfalse, falseLabel);

            // throw new ArgumentOutOfRangeException("strlen");
            ilGenerator.Emit(OpCodes.Ldstr, param.LengthParam.Name);
            ilGenerator.Emit(OpCodes.Newobj,
                typeof(ArgumentOutOfRangeException).GetConstructor(new[] {typeof(string)}));
            ilGenerator.Emit(OpCodes.Throw);
            ilGenerator.MarkLabel(falseLabel);
        }

        private static void EmitOutParamAssignment(ILGenerator ilGenerator, NativeIlGenContext context, LocalBuilder[] paramBuffers)
        {
            var dataIndex = context.Parameters.Length;
            for (var i = 0; i < context.Parameters.Length; i++)
            {
                var param = context.Parameters[i];

                if ((param.Type & NativeParameterType.ValueTypeMask) != 0)
                {
                    if (param.Type.HasFlag(NativeParameterType.Reference))
                    {
                        ilGenerator.Emit(OpCodes.Ldarg, i + 1);
                        ilGenerator.Emit(OpCodes.Ldloc_0);
                        ilGenerator.Emit(OpCodes.Ldc_I4, dataIndex * 4);
                        ilGenerator.Emit(OpCodes.Add);
                        ilGenerator.Emit(OpCodes.Ldind_I4);
                        
                        switch (param.Type)
                        {
                            case NativeParameterType.SingleReference:
                                ilGenerator.EmitConvert<int,float>();
                                ilGenerator.Emit(OpCodes.Stind_R4);
                                break;
                            case NativeParameterType.Int32Reference:
                                ilGenerator.Emit(OpCodes.Stind_I4);
                                break;
                            case NativeParameterType.BoolReference:
                                ilGenerator.EmitConvert<int,bool>();
                                ilGenerator.Emit(OpCodes.Stind_I1);
                                break;
                            default:
                                throw new Exception("Unknown native parameter type");
                        }
                    }   
                    dataIndex++;
                }
                else if (param.Type == NativeParameterType.String)
                {
                    // uses own buffer
                }
                else if (param.Type == NativeParameterType.StringReference)
                {
                    // paramStr = NativeUtils.GetString(strBuf);
                    ilGenerator.Emit(OpCodes.Ldarg, i + 1);
                    ilGenerator.Emit(OpCodes.Ldloc, paramBuffers[i]);
                    ilGenerator.EmitCall(OpCodes.Call, typeof(NativeUtils).GetMethod(nameof(NativeUtils.GetString)), null);
                    ilGenerator.Emit(OpCodes.Stind_Ref);
                }
                else
                {
                    throw new Exception("Unknown native parameter type");
                }
            }
        }
        
        private static void EmitReturnCast(ILGenerator ilGenerator, NativeIlGenContext context)
        {
            if (context.BaseMethod.ReturnType == typeof(void))
                ilGenerator.Emit(OpCodes.Pop);
            else if (context.BaseMethod.ReturnType == typeof(bool))
                ilGenerator.EmitConvert<int,bool>();
            else if (context.BaseMethod.ReturnType == typeof(float))
                ilGenerator.EmitConvert<int,float>();
            else if (context.BaseMethod.ReturnType == typeof(int))
            {
                // nop
            }
            else
            {
                throw new Exception("Unsupported return type of native");
            }
        }

        private static void EmitConvertToInt(ILGenerator ilGenerator, NativeParameterType type)
        {
            if (type.HasFlag(NativeParameterType.Single))
                ilGenerator.EmitConvert<float,int>();
            if (type.HasFlag(NativeParameterType.Bool))
                ilGenerator.EmitConvert<bool,int>();
        }

        private static LocalBuilder EmitSpanAlloc(ILGenerator ilGenerator, int lengthArg)
        {
            return EmitSpanAlloc(ilGenerator, () => ilGenerator.Emit(OpCodes.Ldarg, lengthArg));
        }

        private static LocalBuilder EmitSpanAlloc(ILGenerator ilGenerator, LocalBuilder length)
        {
            return EmitSpanAlloc(ilGenerator, () => ilGenerator.Emit(OpCodes.Ldloc, length));
        }

        private static LocalBuilder EmitSpanAlloc(ILGenerator ilGenerator, Action loadLength)
        {
            var span = ilGenerator.DeclareLocal(typeof(Span<byte>));

            // Span<byte> span = ((strlen < MaxStackAllocSize) ? stackalloc byte[strlen] : new Span<byte>(new byte[strlen]));
            // if...
            loadLength();
            ilGenerator.Emit(OpCodes.Ldc_I4, MaxStackAllocSize);
            var labelArrayAlloc = ilGenerator.DefineLabel();
            ilGenerator.Emit(OpCodes.Bge, labelArrayAlloc);

            // then...
            loadLength();
            ilGenerator.Emit(OpCodes.Conv_U);
            ilGenerator.Emit(OpCodes.Localloc);
            loadLength();
            ilGenerator.Emit(OpCodes.Newobj, typeof(Span<byte>).GetConstructor(new[] {typeof(void*), typeof(int)}));
            ilGenerator.Emit(OpCodes.Stloc, span);
            var labelDone = ilGenerator.DefineLabel();
            ilGenerator.Emit(OpCodes.Br, labelDone);

            // else...
            ilGenerator.MarkLabel(labelArrayAlloc);
            ilGenerator.Emit(OpCodes.Ldloca, span);
            loadLength();
            ilGenerator.Emit(OpCodes.Newarr, typeof(byte));
            ilGenerator.Emit(OpCodes.Call, typeof(Span<byte>).GetConstructor(new[]{typeof(byte[])}));
            ilGenerator.Emit(OpCodes.Br, labelDone);

            ilGenerator.MarkLabel(labelDone);
            return span;
        }

        private static string GenerateCallFormat(NativeIlGenContext context)
        {
            var formatStringBuilder = new StringBuilder();
            foreach (var param in context.Parameters)
            {
                switch (param.Type)
                {
                    case NativeParameterType.Int32:
                    case NativeParameterType.Single:
                    case NativeParameterType.Bool:
                        formatStringBuilder.Append("d");
                        break;
                    case NativeParameterType.Int32Reference:
                    case NativeParameterType.SingleReference:
                    case NativeParameterType.BoolReference:
                        formatStringBuilder.Append("R");
                        break;
                    case NativeParameterType.String:
                        formatStringBuilder.Append("s");
                        break;
                    case NativeParameterType.StringReference:
                        formatStringBuilder.Append($"S[*{param.LengthParam.Index}]");
                        break;
                    case NativeParameterType.Int32Array:
                    case NativeParameterType.SingleArray:
                    case NativeParameterType.BoolArray:
                        formatStringBuilder.Append($"a[*{param.LengthParam.Index}]");
                        break;
                    case NativeParameterType.Int32ArrayReference:
                    case NativeParameterType.SingleArrayReference:
                    case NativeParameterType.BoolArrayReference:
                        formatStringBuilder.Append($"A[*{param.LengthParam.Index}]");
                        break;
                    default:
                        throw new Exception("Unknown parameter type");
                }
            }

            return formatStringBuilder.ToString();
        }
    }
}
