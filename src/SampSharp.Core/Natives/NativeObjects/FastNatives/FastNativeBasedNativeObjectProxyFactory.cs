using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using SampSharp.Core.Communication;
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
        protected override MethodBuilder CreateMethodBuilder(string nativeName, Type proxyType, uint[] nativeArgumentLengths,
            TypeBuilder typeBuilder, MethodInfo method, string[] identifierPropertyNames, int idIndex, FieldInfo[] proxyFields)
        {
            // Define the method.
            var idCount = identifierPropertyNames.Length;

            var attributes = GetMethodOverrideAttributes(method);
            var parameterTypes = GetMethodParameters(method);
            
            // Find the native.
            var native = Interop.FastNativeFind(nativeName);

            if (native == IntPtr.Zero)
                return null;

            var parameters = NativeParameterInfo.ForTypes(method.GetParameters(), nativeArgumentLengths);
            
            var methodBuilder = typeBuilder.DefineMethod(method.Name, attributes, method.ReturnType, parameterTypes);
            var ilGenerator = methodBuilder.GetILGenerator();
            
            // Will be using direct reference to loc_0 for optimized calls to data buffer
            var data = ilGenerator.DeclareLocal(typeof(int*));
            if(data.LocalIndex != 0)
                throw new Exception("data should be local 0");
            
            // Data buffer will contain all param references + value type values
            var stackSize = parameters.Length +
                            parameters.Count(param => (param.Type & NativeParameterType.ValueTypeMask) != 0);

            // int* data = stackalloc int[n];
            ilGenerator.Emit(OpCodes.Ldc_I4, stackSize * 4);//4 bytes per cell
            ilGenerator.Emit(OpCodes.Conv_U);
            ilGenerator.Emit(OpCodes.Localloc);
            ilGenerator.Emit(OpCodes.Stloc_0);

            // Build string format
            var formatString = GenerateCallFormat(parameters);

            EmitInParamAssignment(parameters, ilGenerator, method, out var paramBuffers);
            EmitInvokeNative(proxyFields, ilGenerator, native, formatString);
            EmitOutParamAssignment(parameters, ilGenerator, paramBuffers);
            EmitCast(ilGenerator, method.ReturnType);

            // return $0
            ilGenerator.Emit(OpCodes.Ret);
            
            return methodBuilder;
        }

        private static void EmitInvokeNative(FieldInfo[] proxyFields, ILGenerator ilGenerator, IntPtr native,
            string formatString)
        {
            // if (_synchronizationProvider.InvokeRequired)
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldfld, proxyFields[0]);
            ilGenerator.EmitCall(OpCodes.Callvirt, PropertyGetter<ISynchronizationProvider>(x => x.InvokeRequired), null);

            var elseLabel = ilGenerator.DefineLabel();
            var endifLabel = ilGenerator.DefineLabel();
            ilGenerator.Emit(OpCodes.Brfalse, elseLabel);

            // NativeUtils.SynchronizeInvoke(_synchronizationProvider, new IntPtr($0), format, ptr);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldfld, proxyFields[0]);
            ilGenerator.Emit(OpCodes.Ldc_I4, (int) native);
            ilGenerator.Emit(OpCodes.Newobj, typeof(IntPtr).GetConstructor(new[] {typeof(int)}));
            ilGenerator.Emit(OpCodes.Ldstr, formatString);
            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.EmitCall(OpCodes.Call, Util(nameof(NativeUtils.SynchronizeInvoke)), null);

            // else
            ilGenerator.Emit(OpCodes.Br_S, endifLabel);
            ilGenerator.MarkLabel(elseLabel);

            // SampSharp.Core.Hosting.Interop.FastNativeInvoke(new IntPtr($0), format, ptr);
            ilGenerator.Emit(OpCodes.Ldc_I4, (int) native);
            ilGenerator.Emit(OpCodes.Newobj, typeof(IntPtr).GetConstructor(new[] {typeof(int)}));
            ilGenerator.Emit(OpCodes.Ldstr, formatString);
            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.EmitCall(OpCodes.Call, typeof(Interop).GetMethod(nameof(Interop.FastNativeInvoke)), null);

            // endif
            ilGenerator.Emit(OpCodes.Br_S, endifLabel);
            ilGenerator.MarkLabel(endifLabel);
        }

        private void EmitInParamAssignment(NativeParameterInfo[] parameters, ILGenerator ilGenerator, MethodInfo methodInfo, out LocalBuilder[] paramBuffers)
        {
            // TODO: Do not allocate big strings/arrays on stack

            paramBuffers = new LocalBuilder[parameters.Length];

            var dataIndex = parameters.Length;
            for (var i = 0; i < parameters.Length; i++)
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

                var param = parameters[i];
                if ((param.Type & NativeParameterType.ValueTypeMask) != 0) // in/out int/float
                {
                    // data[i] = NativeUtils.IntPointerToInt(ptr + dataIndex);
                    EmitBufferLocation();

                    ilGenerator.Emit(OpCodes.Ldloc_0);
                    ilGenerator.Emit(OpCodes.Ldc_I4, dataIndex * 4); // + 1 (* 4 bytes)
                    ilGenerator.Emit(OpCodes.Add);
                    ilGenerator.EmitCall(OpCodes.Call, Util(nameof(NativeUtils.IntPointerToInt)), null);
                    ilGenerator.Emit(OpCodes.Stind_I4);

                    if (!param.IsOutput)
                    {
                        // data[dataIndex] = argI/index
                        // TODO: object index getter

                        ilGenerator.Emit(OpCodes.Ldloc_0);
                        ilGenerator.Emit(OpCodes.Ldc_I4, dataIndex * 4);
                        ilGenerator.Emit(OpCodes.Add);
                        ilGenerator.Emit(OpCodes.Ldarg, i + 1); // arg0=this, arg1=1st arg

                        var converter = ConverterToInt32(param.Type);
                        if (converter != null)
                            ilGenerator.EmitCall(OpCodes.Call, converter, null);

                        ilGenerator.Emit(OpCodes.Stind_I4);
                    }

                    dataIndex++;
                }
                else if (param.Type == NativeParameterType.String) // in string
                {
                    var strBuffer = ilGenerator.DeclareLocal(typeof(byte*));
                    var strLen = ilGenerator.DeclareLocal(typeof(int));

                    // int byteCount = NativeUtils.GetByteCount(textString);
                    ilGenerator.Emit(OpCodes.Ldarg, i + 1);
                    ilGenerator.EmitCall(OpCodes.Call, Util(nameof(NativeUtils.GetByteCount)), null);
                    ilGenerator.Emit(OpCodes.Stloc, strLen);

                    // TODO: use heap if big
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
                    EmitBufferLocation();

                    ilGenerator.Emit(OpCodes.Ldloc, strBuffer);
                    ilGenerator.EmitCall(OpCodes.Call, Util(nameof(NativeUtils.BytePointerToInt)), null);
                    ilGenerator.Emit(OpCodes.Stind_I4);
                }
                else if (param.Type == NativeParameterType.StringReference)
                {
                    // if (strlen <= 0)
                    ilGenerator.Emit(OpCodes.Ldarg, param.LengthIndex + 1);
                    ilGenerator.Emit(OpCodes.Ldc_I4_0);
                    ilGenerator.Emit(OpCodes.Cgt);
                    ilGenerator.Emit(OpCodes.Ldc_I4_0);
                    ilGenerator.Emit(OpCodes.Ceq);
                    var falseLabel = ilGenerator.DefineLabel();
                    ilGenerator.Emit(OpCodes.Brfalse_S, falseLabel);

                    // throw new ArgumentOutOfRangeException("strlen");
                    ilGenerator.Emit(OpCodes.Ldstr, methodInfo.GetParameters()[param.LengthIndex].Name);// TODO optimize GetParameters calls
                    ilGenerator.Emit(OpCodes.Newobj,
                        typeof(ArgumentOutOfRangeException).GetConstructor(new[] {typeof(string)}));
                    ilGenerator.Emit(OpCodes.Throw);
                    ilGenerator.MarkLabel(falseLabel);
                    
                    // var strBuf = stackalloc/new byte[...]
                    var strBuf = SpanAlloc(ilGenerator, (int) param.LengthIndex + 1);
                    paramBuffers[i] = strBuf;

                    // fixed(byte* strBufPtr = strBuf)
                    var strBufPinned = ilGenerator.DeclareLocal(typeof(byte*), true);
                    var strBufPtr = ilGenerator.DeclareLocal(typeof(byte*));

                    ilGenerator.Emit(OpCodes.Ldloca, strBuf);
                    ilGenerator.EmitCall(OpCodes.Call, typeof(Span<byte>)
                        .GetMethod(nameof(Span<byte>.GetPinnableReference)), null);
                    ilGenerator.Emit(OpCodes.Stloc, strBufPinned);
                    ilGenerator.Emit(OpCodes.Ldloc, strBufPinned);
                    ilGenerator.Emit(OpCodes.Conv_U);
                    ilGenerator.Emit(OpCodes.Stloc, strBufPtr);

                    // data[i] = NativeUtils.BytePointerToInt(strBufPtr);
                    EmitBufferLocation();
                    ilGenerator.Emit(OpCodes.Ldloc, strBufPtr);
                    ilGenerator.EmitCall(OpCodes.Call, Util(nameof(NativeUtils.BytePointerToInt)), null);
                    ilGenerator.Emit(OpCodes.Stind_I4);
                }
                else if (param.Type.HasFlag(NativeParameterType.Array))
                {
                    throw new NotImplementedException("Arrays not implemented");
                }
                else
                {
                    throw new Exception("Unknown native parameter type");
                }
            }
        }

        private static LocalBuilder SpanAlloc(ILGenerator ilGenerator, int lengthArg)
        {
            // TODO: See where short forms _S are used and check if that's right.

            // var nameBuf = strlen < 128 ? stackalloc byte[strlen] : new Span<byte>(new byte[strlen]);
            // translates to...

            var span = ilGenerator.DeclareLocal(typeof(Span<byte>));

            // Span<byte> span = ((strlen < 128) ? stackalloc byte[strlen] : new Span<byte>(new byte[strlen]));
            // if...
            ilGenerator.Emit(OpCodes.Ldarg, lengthArg);
            ilGenerator.Emit(OpCodes.Ldc_I4, MaxStackAllocSize);
            var labelArrayAlloc = ilGenerator.DefineLabel();
            ilGenerator.Emit(OpCodes.Bge_S, labelArrayAlloc);

            // then...
            ilGenerator.Emit(OpCodes.Ldarg, lengthArg);
            ilGenerator.Emit(OpCodes.Conv_U);
            ilGenerator.Emit(OpCodes.Localloc);
            ilGenerator.Emit(OpCodes.Ldarg, lengthArg);
            ilGenerator.Emit(OpCodes.Newobj, typeof(Span<byte>).GetConstructor(new[] {typeof(void*), typeof(int)}));
            ilGenerator.Emit(OpCodes.Stloc, span);
            var labelDone = ilGenerator.DefineLabel();
            ilGenerator.Emit(OpCodes.Br_S, labelDone);

            // else...
            ilGenerator.MarkLabel(labelArrayAlloc);
            ilGenerator.Emit(OpCodes.Ldloca, span);
            ilGenerator.Emit(OpCodes.Ldarg, lengthArg);
            ilGenerator.Emit(OpCodes.Newarr, typeof(byte));
            ilGenerator.Emit(OpCodes.Call, typeof(Span<byte>).GetConstructor(new[]{typeof(byte[])}));
            ilGenerator.Emit(OpCodes.Br_S, labelDone);

            // IL_001d: ldarg.3
            // IL_001e: ldc.i4 128
            // IL_0023: bge.s IL_003b
            //
            // IL_0025: ldarg.3
            // IL_0026: stloc.s 4

            // IL_0028: ldloc.s 4
            // IL_002a: conv.u
            // IL_002b: localloc
            // IL_002d: ldloc.s 4
            // IL_002f: newobj instance void valuetype [System.Runtime]System.Span`1<uint8>::.ctor(void*, int32)
            // IL_0034: stloc.s 5
            // IL_0036: ldloc.s 5
            // IL_0038: stloc.3
            // IL_0039: br.s IL_0048
            //
            // IL_003b: ldloca.s 3
            // IL_003d: ldarg.3
            // IL_003e: newarr [System.Runtime]System.Byte
            // Span<byte> bytes = span;
            // IL_0043: call instance void valuetype [System.Runtime]System.Span`1<uint8>::.ctor(!0[])
            //
            // IL_0048: ldloc.3
            // IL_0049: stloc.1

            ilGenerator.MarkLabel(labelDone);
            return span;
        }

        private static void EmitOutParamAssignment(IReadOnlyList<NativeParameterInfo> parameters, ILGenerator ilGenerator, LocalBuilder[] paramBuffers)
        {
            var dataIndex = parameters.Count;
            for (var i = 0; i < parameters.Count; i++)
            {
                var param = parameters[i];

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
                                ilGenerator.EmitCall(OpCodes.Call, ConverterToSingle(), null);
                                ilGenerator.Emit(OpCodes.Stind_R4);
                                break;
                            case NativeParameterType.Int32Reference:
                                ilGenerator.Emit(OpCodes.Stind_I4);
                                break;
                            case NativeParameterType.BoolReference:
                                ilGenerator.EmitCall(OpCodes.Call, ConverterToBool(), null);
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
        
        private static MethodInfo Util(string utilName)
        {
            return typeof(NativeUtils).GetMethod(utilName);
        }

        private static MethodInfo ConverterToInt32(NativeParameterType type)
        {
            if (type.HasFlag(NativeParameterType.Single))
                return ConverterToInt32<float>();
            if (type.HasFlag(NativeParameterType.Bool))
                return ConverterToInt32<bool>();
            return null;
        }

        private static MethodInfo ConverterToInt32<T>()
        {
            return typeof(ValueConverter).GetMethod(nameof(ValueConverter.ToInt32), new []{typeof(T)});
        }

        private static MethodInfo ConverterToSingle()
        {
            return typeof(ValueConverter).GetMethod(nameof(ValueConverter.ToSingle), new []{typeof(int)});
        }

        private static MethodInfo ConverterToBool()
        {
            return typeof(ValueConverter).GetMethod(nameof(ValueConverter.ToBoolean), new []{typeof(int)});
        }

        private static MethodInfo PropertyGetter<T>(Expression<Func<T, object>> expr)
        {
            return ((PropertyInfo) ((MemberExpression)((UnaryExpression) expr.Body).Operand).Member).GetMethod;
        }

        private static void EmitCast(ILGenerator ilGenerator, Type targetType)
        {
            if (targetType == typeof(void))
                ilGenerator.Emit(OpCodes.Pop);
            else if (targetType == typeof(bool))
                ilGenerator.EmitCall(OpCodes.Call, ConverterToBool(), null);
            else if (targetType == typeof(float))
                ilGenerator.EmitCall(OpCodes.Call, ConverterToSingle(), null);
            else if (targetType == typeof(int))
            {
                // nop
            }
            else
            {
                throw new Exception("Unsupported return type of native");
            }
        }

        private static string GenerateCallFormat(IEnumerable<NativeParameterInfo> parameters)
        {
            var formatStringBuilder = new StringBuilder();
            foreach (var param in parameters)
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
                        formatStringBuilder.Append($"S[*{param.LengthIndex}]");
                        break;
                    case NativeParameterType.Int32Array:
                    case NativeParameterType.SingleArray:
                    case NativeParameterType.BoolArray:
                        formatStringBuilder.Append($"a[*{param.LengthIndex}]");
                        break;
                    case NativeParameterType.Int32ArrayReference:
                    case NativeParameterType.SingleArrayReference:
                    case NativeParameterType.BoolArrayReference:
                        formatStringBuilder.Append($"A[*{param.LengthIndex}]");
                        break;
                    default:
                        throw new Exception("Unknown parameter type");
                }
            }

            return formatStringBuilder.ToString();
        }
    }
}
