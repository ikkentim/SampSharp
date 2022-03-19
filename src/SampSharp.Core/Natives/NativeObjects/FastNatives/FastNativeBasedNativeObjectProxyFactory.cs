using System;
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
    /// 
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "documentation for code generator")]
    internal class FastNativeBasedNativeObjectProxyFactory : NativeObjectProxyFactoryBase
    {
        private const int MaxStackAllocSize = 256;
        private readonly IGameModeClient _gameModeClient;
        
        /// <inheritdoc />
        public FastNativeBasedNativeObjectProxyFactory(IGameModeClient gameModeClient) : base("ProxyAssemblyFast")
        {
            _gameModeClient = gameModeClient;
        }
        
        /// <inheritdoc />
        protected override object[] GetProxyConstructorArgs()
        {
            return new object[] {_gameModeClient.SynchronizationProvider};
        }

        /// <inheritdoc />
        protected override FieldInfo[] DefineProxyFields(TypeBuilder typeBuilder)
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
            var args = ilGenerator.DeclareLocal(typeof(int*));
            if (args.LocalIndex != 0)
                throw new InvalidOperationException("args should be local 0");

            var values = ilGenerator.DeclareLocal(typeof(int*));
            if (values.LocalIndex != 1)
                throw new InvalidOperationException("values should be local 1");

            // Args buffer will contain all param references
            // Values buffer will contain all value type values.
            var argsCount = context.Parameters.Length;
            var valueCount = context.Parameters.Count(param => (param.Type & NativeParameterType.ValueTypeMask) != 0);

            // Must provide an arguments pointer to invoke_native, even if no arguments are required.
            if (argsCount == 0)
                argsCount = 1;

            NativeIlGenParam varArgsParam = null;

            if (context.HasVarArgs)
            {
                varArgsParam = context.Parameters.Last(x => x.Type == NativeParameterType.VarArgs);
                if (varArgsParam.Parameter.ParameterType != typeof(object[]))
                {
                    throw new InvalidOperationException("Variable arguments array in native method must be of type object[]");
                }

                if (varArgsParam != context.Parameters.Last())
                {
                    throw new InvalidOperationException("Variable arguments must be last parameter of method.");
                }
            }

            // int* args = stackalloc int[n];
            if (varArgsParam != null)
            {
                // (varArgs * 4) + ((argsCount * 4))
                ilGenerator.Emit(OpCodes.Ldarg, varArgsParam.Parameter);
                ilGenerator.Emit(OpCodes.Ldlen);
                ilGenerator.Emit(OpCodes.Conv_I4);
                ilGenerator.Emit(OpCodes.Ldc_I4_4); // 4 bytes per cell
                ilGenerator.Emit(OpCodes.Mul);

                ilGenerator.Emit(OpCodes.Ldc_I4, argsCount * 4); //4 bytes per cell
                ilGenerator.Emit(OpCodes.Add);
            }
            else
            {
                ilGenerator.Emit(OpCodes.Ldc_I4, argsCount * 4); //4 bytes per cell
            }

            ilGenerator.Emit(OpCodes.Conv_U);
            ilGenerator.Emit(OpCodes.Localloc);
            ilGenerator.Emit(OpCodes.Stloc_0);
            
            // int* values = stackalloc int[n];
            if (valueCount > 0 || varArgsParam != null)
            {
                if (varArgsParam != null)
                {
                    if (valueCount == 0)
                    {
                        // Need to allocate at least something if the varargs is empty.
                        valueCount++;
                    }
                    // (varArgs * 4) + ((valueCount * 4))
                    // For every vararg value, reserve 2 cells (the argument pointer and the value itself)
                    ilGenerator.Emit(OpCodes.Ldarg, varArgsParam.Parameter);
                    ilGenerator.Emit(OpCodes.Ldlen);
                    ilGenerator.Emit(OpCodes.Conv_I4);
                    ilGenerator.Emit(OpCodes.Ldc_I4_4); // 4 bytes per cell
                    ilGenerator.Emit(OpCodes.Mul);

                    ilGenerator.Emit(OpCodes.Ldc_I4, valueCount * 4); //4 bytes per cell
                    ilGenerator.Emit(OpCodes.Add);
                }
                else
                {
                    ilGenerator.Emit(OpCodes.Ldc_I4, valueCount * 4); //4 bytes per cell
                }

                ilGenerator.Emit(OpCodes.Conv_U);
                ilGenerator.Emit(OpCodes.Localloc);
                ilGenerator.Emit(OpCodes.Stloc_1);
            }

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

            void EmitFormatVarArgs()
            {
                if (context.HasVarArgs)
                {
                    // NativeUtils.AppendVarArgsFormat(format, varArgs);
                    var param = context.Parameters.Last(x => x.Type == NativeParameterType.VarArgs);
                    ilGenerator.Emit(OpCodes.Ldarg, param.Parameter);
                    ilGenerator.EmitCall(OpCodes.Call, typeof(NativeUtils), nameof(NativeUtils.AppendVarArgsFormat));
                }
            }

            // NativeUtils.SynchronizeInvoke(_synchronizationProvider, new IntPtr($0), format, ptr);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldfld, context.ProxyGeneratedFields[0]);
            ilGenerator.Emit(OpCodes.Ldc_I4, (int) native);
            ilGenerator.Emit(OpCodes.Newobj, typeof(IntPtr).GetConstructor(new[] {typeof(int)}));
            ilGenerator.Emit(OpCodes.Ldstr, formatString);
            EmitFormatVarArgs();
            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.EmitCall(typeof(NativeUtils), nameof(NativeUtils.SynchronizeInvoke));

            // else
            ilGenerator.Emit(OpCodes.Br, endifLabel);
            ilGenerator.MarkLabel(elseLabel);

            // SampSharp.Core.Hosting.Interop.FastNativeInvoke(new IntPtr($0), format, ptr);
            ilGenerator.Emit(OpCodes.Ldc_I4, (int) native);
            ilGenerator.Emit(OpCodes.Newobj, typeof(IntPtr).GetConstructor(new[] {typeof(int)}));
            ilGenerator.Emit(OpCodes.Ldstr, formatString);
            EmitFormatVarArgs();
            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.EmitCall(typeof(Interop), nameof(Interop.FastNativeInvoke));

            // endif
            ilGenerator.Emit(OpCodes.Br, endifLabel);
            ilGenerator.MarkLabel(endifLabel);
        }

        private static void EmitInParamAssignment(ILGenerator ilGenerator, NativeIlGenContext context, out LocalBuilder[] paramBuffers)
        {
            paramBuffers = new LocalBuilder[context.Parameters.Length];

            var valueIndex = 0;
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
                if ((param.Type & NativeParameterType.ValueTypeMask) != 0 && !param.Type.HasFlag(NativeParameterType.Array)) // in/out int/float
                {
                    // args[i] = NativeUtils.IntPointerToInt(values + valueIndex);
                    EmitBufferLocation();

                    ilGenerator.Emit(OpCodes.Ldloc_1);
                    ilGenerator.Emit(OpCodes.Ldc_I4, valueIndex * 4); // + 1 (* 4 bytes)
                    ilGenerator.Emit(OpCodes.Add);
                    ilGenerator.EmitCall(OpCodes.Call, typeof(NativeUtils), nameof(NativeUtils.IntPointerToInt));
                    ilGenerator.Emit(OpCodes.Stind_I4);

                    if (param.Property != null)
                    {
                        // values[valueIndex] = Property;
                        ilGenerator.Emit(OpCodes.Ldloc_1);
                        ilGenerator.Emit(OpCodes.Ldc_I4, valueIndex * 4);
                        ilGenerator.Emit(OpCodes.Add);
                        ilGenerator.Emit(OpCodes.Ldarg_0);
                        ilGenerator.EmitCall(param.Property.GetMethod);
                        EmitConvertToInt(ilGenerator, param.Type);
                        ilGenerator.Emit(OpCodes.Stind_I4);
                    }
                    else if (!param.Parameter.IsOut)
                    {
                        // values[valueIndex] = argI
                        ilGenerator.Emit(OpCodes.Ldloc_1);
                        ilGenerator.Emit(OpCodes.Ldc_I4, valueIndex * 4);
                        ilGenerator.Emit(OpCodes.Add);
                        ilGenerator.Emit(OpCodes.Ldarg, param.Parameter); // arg0=this, arg1=1st arg
                        EmitConvertToInt(ilGenerator, param.Type);
                        ilGenerator.Emit(OpCodes.Stind_I4);
                    }

                    valueIndex++;
                }
                else if (param.Type == NativeParameterType.String) // in string
                {
                    if (param.Property != null)
                    {
                        throw new InvalidOperationException("Unsupported identifier property type");
                    }

                    var strLen = ilGenerator.DeclareLocal(typeof(int));

                    // int byteCount = NativeUtils.GetByteCount(textString);
                    ilGenerator.Emit(OpCodes.Ldarg, param.Parameter);
                    ilGenerator.EmitCall(typeof(NativeUtils), nameof(NativeUtils.GetByteCount));
                    ilGenerator.Emit(OpCodes.Stloc, strLen);

                    // Span<byte> strBuffer = stackalloc/new byte[...]
                    var strBufferSpan = EmitSpanAlloc(ilGenerator, strLen);

                    // NativeUtils.GetBytes(textString, strBuffer);
                    ilGenerator.Emit(OpCodes.Ldarg, param.Parameter);
                    ilGenerator.Emit(OpCodes.Ldloc, strBufferSpan);
                    ilGenerator.EmitCall(typeof(NativeUtils), nameof(NativeUtils.GetBytes));

                    // args[i] = NativeUtils.BytePointerToInt(strBuffer);
                    EmitBufferLocation(); // args[i]
                    EmitByteSpanToPointer(ilGenerator, strBufferSpan);
                    ilGenerator.EmitCall(typeof(NativeUtils), nameof(NativeUtils.BytePointerToInt));
                    ilGenerator.Emit(OpCodes.Stind_I4);
                }
                else if (param.Type == NativeParameterType.StringReference)
                {
                    EmitThrowOnOutOfRangeLength(ilGenerator, param.LengthParam);

                    // var strBuf = stackalloc/new byte[...]
                    var strBuf = EmitSpanAlloc(ilGenerator, param.LengthParam.Parameter);
                    paramBuffers[i] = strBuf;

                    // args[i] = NativeUtils.BytePointerToInt(strBufPtr);
                    EmitBufferLocation(); // args[i]
                    EmitByteSpanToPointer(ilGenerator, strBuf);
                    ilGenerator.EmitCall(typeof(NativeUtils), nameof(NativeUtils.BytePointerToInt));
                    ilGenerator.Emit(OpCodes.Stind_I4);
                }
                else if (param.Type.HasFlag(NativeParameterType.Array))
                {
                    if (param.Property != null)
                    {
                        throw new InvalidOperationException("Unsupported identifier property type");
                    }

                    EmitThrowOnOutOfRangeLength(ilGenerator, param.LengthParam);

                    // var arraySpan = NativeUtils.ArrayToIntSpan(array/null,len)
                    var arraySpan = ilGenerator.DeclareLocal(typeof(Span<int>));
                    if (param.Parameter.IsOut)
                        ilGenerator.Emit(OpCodes.Ldnull);
                    else
                        ilGenerator.Emit(OpCodes.Ldarg, param.Parameter);
                    ilGenerator.Emit(OpCodes.Ldarg, param.LengthParam.Parameter);
                    ilGenerator.EmitCall(typeof(NativeUtils), nameof(NativeUtils.ArrayToIntSpan));
                    ilGenerator.Emit(OpCodes.Stloc, arraySpan);
                    
                    // args[i] = NativeUtils.BytePointerToInt(strBufPtr);
                    EmitBufferLocation(); // args[i]
                    EmitIntSpanToPointer(ilGenerator, arraySpan);
                    ilGenerator.EmitCall(typeof(NativeUtils), nameof(NativeUtils.IntPointerToInt));
                    ilGenerator.Emit(OpCodes.Stind_I4);
                    
                    if (param.Type.HasFlag(NativeParameterType.Reference))
                    {
                        paramBuffers[i] = arraySpan;
                    }
                }
                else if (param.Type == NativeParameterType.VarArgs)
                {
                    // var vContext = new VarArgsContext();
                    var vContext = ilGenerator.DeclareLocal(typeof(VarArgsState));
                    paramBuffers[i] = vContext;
                    ilGenerator.Emit(OpCodes.Newobj, typeof(VarArgsState).GetConstructor(Array.Empty<Type>()));
                    ilGenerator.Emit(OpCodes.Stloc, vContext);
                    
                    // NativeUtils.SetVarArgsValues(args, values, varArgs, i, valueIndex, vContext);
                    ilGenerator.Emit(OpCodes.Ldloc_0);
                    ilGenerator.Emit(OpCodes.Ldloc_1);
                    ilGenerator.Emit(OpCodes.Ldarg, param.Parameter);
                    ilGenerator.Emit(OpCodes.Ldc_I4, i);
                    ilGenerator.Emit(OpCodes.Ldc_I4, valueIndex);
                    ilGenerator.Emit(OpCodes.Ldloc, vContext);
                    ilGenerator.EmitCall(OpCodes.Call, typeof(NativeUtils), nameof(NativeUtils.SetVarArgsValues));
                }
                else
                {
                    throw new InvalidOperationException("Unknown native parameter type");
                }
            }
        }

        private static void EmitOutParamAssignment(ILGenerator ilGenerator, NativeIlGenContext context, LocalBuilder[] paramBuffers)
        {
            var valueIndex = 0;
            for (var i = 0; i < context.Parameters.Length; i++)
            {
                var param = context.Parameters[i];

                if ((param.Type & NativeParameterType.ValueTypeMask) != 0 && !param.Type.HasFlag(NativeParameterType.Array))
                {
                    if (param.Type.HasFlag(NativeParameterType.Reference))
                    {
                        ilGenerator.Emit(OpCodes.Ldarg, param.Parameter);
                        ilGenerator.Emit(OpCodes.Ldloc_1);
                        ilGenerator.Emit(OpCodes.Ldc_I4, valueIndex * 4);
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
                                throw new InvalidOperationException("Unknown native parameter type");
                        }
                    }   
                    valueIndex++;
                }
                else if (param.Type.HasFlag(NativeParameterType.Array | NativeParameterType.Reference))
                {
                    // argI = NativeUtils.IntSpanToArray<int>((Array)null, span);
                    ilGenerator.Emit(OpCodes.Ldarg, param.Parameter);
                    if (param.Parameter.IsOut)
                        ilGenerator.Emit(OpCodes.Ldnull);
                    else
                        ilGenerator.Emit(OpCodes.Ldarg, param.Parameter);
                    ilGenerator.Emit(OpCodes.Ldloc, paramBuffers[i]);
                    var method = typeof(NativeUtils).GetMethod(nameof(NativeUtils.IntSpanToArray))
                        .MakeGenericMethod(param.InputType.GetElementType().GetElementType());
                    ilGenerator.EmitCall(method);
                    ilGenerator.Emit(OpCodes.Stind_Ref);
                }
                else if (param.Type == NativeParameterType.String || param.Type.HasFlag(NativeParameterType.Array))
                {
                    // uses own buffer
                }
                else if (param.Type == NativeParameterType.StringReference)
                {
                    // paramStr = NativeUtils.GetString(strBuf);
                    ilGenerator.Emit(OpCodes.Ldarg, param.Parameter);
                    ilGenerator.Emit(OpCodes.Ldloc, paramBuffers[i]);
                    ilGenerator.EmitCall(OpCodes.Call, typeof(NativeUtils).GetMethod(nameof(NativeUtils.GetString)), null);
                    ilGenerator.Emit(OpCodes.Stind_Ref);
                }
                else if (param.Type == NativeParameterType.VarArgs)
                {
                    // vContext.Dispose();
                    ilGenerator.Emit(OpCodes.Ldloc, paramBuffers[i]);
                    ilGenerator.EmitCall(OpCodes.Callvirt, typeof(VarArgsState), nameof(VarArgsState.Dispose));
                }
                else
                {
                    throw new InvalidOperationException("Unknown native parameter type");
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
                throw new InvalidOperationException("Unsupported return type of native");
            }
        }

        private static void EmitByteSpanToPointer(ILGenerator ilGenerator, LocalBuilder span)
        {
            // fixed(byte* strBufPtr = span)
            var bufferPinned = ilGenerator.DeclareLocal(typeof(byte*), true);
            var bufferPointer = ilGenerator.DeclareLocal(typeof(byte*));

            ilGenerator.Emit(OpCodes.Ldloca, span);
            ilGenerator.EmitCall(typeof(Span<byte>), nameof(Span<byte>.GetPinnableReference));
            ilGenerator.Emit(OpCodes.Stloc, bufferPinned);
            ilGenerator.Emit(OpCodes.Ldloc, bufferPinned);
            ilGenerator.Emit(OpCodes.Conv_U);
            ilGenerator.Emit(OpCodes.Stloc, bufferPointer);
            ilGenerator.Emit(OpCodes.Ldloc, bufferPointer);
        }

        private static void EmitIntSpanToPointer(ILGenerator ilGenerator, LocalBuilder span)
        {
            // fixed(byte* strBufPtr = span)
            var strBufPinned = ilGenerator.DeclareLocal(typeof(int*), true);
            var strBufPtr = ilGenerator.DeclareLocal(typeof(int*));

            ilGenerator.Emit(OpCodes.Ldloca, span);
            ilGenerator.EmitCall(typeof(Span<int>), nameof(Span<int>.GetPinnableReference));
            ilGenerator.Emit(OpCodes.Stloc, strBufPinned);
            ilGenerator.Emit(OpCodes.Ldloc, strBufPinned);
            ilGenerator.Emit(OpCodes.Conv_U);
            ilGenerator.Emit(OpCodes.Stloc, strBufPtr);
            ilGenerator.Emit(OpCodes.Ldloc, strBufPtr);
        }

        private static void EmitThrowOnOutOfRangeLength(ILGenerator ilGenerator, NativeIlGenParam param)
        {
            // if (strlen <= 0)
            ilGenerator.Emit(OpCodes.Ldarg, param.Parameter);
            ilGenerator.Emit(OpCodes.Ldc_I4_0);
            ilGenerator.Emit(OpCodes.Cgt);
            ilGenerator.Emit(OpCodes.Ldc_I4_0);
            ilGenerator.Emit(OpCodes.Ceq);
            var falseLabel = ilGenerator.DefineLabel();
            ilGenerator.Emit(OpCodes.Brfalse, falseLabel);

            // throw new ArgumentOutOfRangeException("strlen");
            ilGenerator.Emit(OpCodes.Ldstr, param.Name);
            ilGenerator.Emit(OpCodes.Newobj,
                typeof(ArgumentOutOfRangeException).GetConstructor(new[] {typeof(string)}));
            ilGenerator.Emit(OpCodes.Throw);
            ilGenerator.MarkLabel(falseLabel);
        }

        private static void EmitConvertToInt(ILGenerator ilGenerator, NativeParameterType type)
        {
            if (type.HasFlag(NativeParameterType.Single))
                ilGenerator.EmitConvert<float,int>();
            if (type.HasFlag(NativeParameterType.Bool))
                ilGenerator.EmitConvert<bool,int>();
        }

        private static LocalBuilder EmitSpanAlloc(ILGenerator ilGenerator, ParameterInfo lengthArg)
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
            // context. ReferenceIndices
            var formatStringBuilder = new StringBuilder();
            foreach (var param in context.Parameters)
            {
                switch (param.Type)
                {
                    case NativeParameterType.Int32:
                    case NativeParameterType.Single:
                    case NativeParameterType.Bool:
                        formatStringBuilder.Append(param.IsReferenceInput ? "r" : "d");
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
                    case NativeParameterType.VarArgs:
                        // format appened at runtime
                        break;
                    default:
                        throw new InvalidOperationException("Unknown parameter type");
                }
            }

            return formatStringBuilder.ToString();
        }
    }
}
