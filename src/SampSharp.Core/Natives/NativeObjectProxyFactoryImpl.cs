using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using SampSharp.Core.Hosting;
using SampSharp.Core.Natives.NativeObjects;

// ReSharper disable CommentTypo
namespace SampSharp.Core.Natives
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "Documentation for generated IL code.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = "IL code generation")]
    internal class NativeObjectProxyFactoryImpl : INativeObjectProxyFactory
    {
        private const string AssemblyName = "SampSharpProxyAssembly";
        
        private readonly IGameModeClient _gameModeClient;
        private int _typeNumber;
        private readonly Dictionary<Type, KnownType> _knownTypes = new();
        private readonly ModuleBuilder _moduleBuilder;
        private readonly object _lock = new();

        public NativeObjectProxyFactoryImpl(IGameModeClient gameModeClient)
        {
            _gameModeClient = gameModeClient;
            
            var asmName = new AssemblyName(AssemblyName);
            var asmBuilder = AssemblyBuilder.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Run);
            _moduleBuilder = asmBuilder.DefineDynamicModule(asmName.Name + ".dll");
        }
        
        public object CreateInstance(Type type, params object[] arguments)
        {
            if(type == null)
                throw new ArgumentNullException(nameof(type));
            
            // Check already known types.
            if (_knownTypes.TryGetValue(type, out var outType))
                return CreateProxyInstance(outType, arguments);

            lock (_lock)
            {
                // Double check known types because we're in a lock by now.
                if (!_knownTypes.TryGetValue(type, out outType))
                    _knownTypes[type] = outType = GenerateProxyType(type);

                return CreateProxyInstance(outType, arguments);
            }
        }

        private object CreateProxyInstance(KnownType proxyType, object[] arguments)
        {

            return proxyType.IsGenerated
                ? Activator.CreateInstance(proxyType.Type,
                    new object[] { _gameModeClient.SynchronizationProvider }.Concat(arguments).ToArray())
                : Activator.CreateInstance(proxyType.Type, arguments);
        }
        
        private KnownType GenerateProxyType(Type type)
        {
            // The generated assembly can only access public types or types which are visible to the generated assembly.
            if (!(type.IsNested ? type.IsNestedPublic : type.IsPublic) && type.Assembly
                    .GetCustomAttributes<InternalsVisibleToAttribute>().All(x => x.AssemblyName != AssemblyName))
            {
                throw new ArgumentException(
                    $"Type '{type}' is not public. Native proxies can only be created for public types and types in assemblies which expose their internals to the '{AssemblyName}' assembly using the InternalsVisibleToAttribute.",
                    nameof(type));
            }

            // Define a type for the native object. Number the types to avoid name collisions.
            var typeBuilder = _moduleBuilder.DefineType($"{type.Name}ProxyClass_{++_typeNumber}",
                TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Class, type);
            
            var syncField = typeBuilder.DefineField("_synchronizationProvider", typeof(ISynchronizationProvider),
                FieldAttributes.Private);
            
            var objectIdentifiers = type.GetCustomAttribute<NativeObjectIdentifiersAttribute>()?.Identifiers ??
                                    Array.Empty<string>();
            
            var didWrapAnything = WrapProperties(type, objectIdentifiers, typeBuilder, syncField) ||
                                  WrapMethods(type, objectIdentifiers, typeBuilder, syncField);

            // Only return the generated type if we've actually generated any members.
            if (didWrapAnything)
            {
                GenerateConstructors(type, typeBuilder, syncField);
                
                // Store the newly created type and return and instance of it.
                return new KnownType(typeBuilder.CreateTypeInfo()!.AsType(), true);
            }

            return new KnownType(type, false);
        }

        private static bool WrapMethods(Type type, string[] objectIdentifiers, TypeBuilder typeBuilder, FieldBuilder syncField)
        {
            var result = false;
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                // Make sure the method is virtual, public and not protected.
                if (!method.IsVirtual || (!method.IsPublic && !method.IsFamily))
                    continue;
                
                var attr = method.GetCustomAttribute<NativeMethodAttribute>();

                if (attr == null)
                    continue;

                if (WrapMethod(type, attr, method, objectIdentifiers, typeBuilder, syncField))
                    result = true;
            }

            return result;
        }

        private static bool WrapProperties(Type type, string[] objectIdentifiers, TypeBuilder typeBuilder, FieldBuilder syncField)
        {
            var result = false;

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic |
                                                        BindingFlags.Instance))
            {
                var get = property.GetMethod;
                var set = property.SetMethod;

                // Make sure the property is virtual and not an indexed property.
                if (!(get?.IsVirtual ?? set!.IsVirtual) || property.GetIndexParameters().Length != 0)
                    continue;
                
                var propertyAttribute = property.GetCustomAttribute<NativePropertyAttribute>();

                if (propertyAttribute == null)
                    continue;

                if (WrapProperty(type, propertyAttribute, objectIdentifiers, typeBuilder, property, get, set, syncField))
                    result = true;
            }

            return result;
        }

        private static void GenerateConstructors(Type type, TypeBuilder typeBuilder, FieldInfo syncField)
        {
            // Implement all constructs from base type. Prepend constructor parameters with synchronizationProvider.
            foreach (var baseConstructor in type.GetConstructors())
            {
                //.method public hidebysig specialname rtspecialname 
                // instance void 
                var constructorParams = baseConstructor.GetParameters();
                var paramTypes = new[] { typeof(ISynchronizationProvider) }
                    .Concat(constructorParams.Select(p => p.ParameterType))
                    .ToArray();
                var attributes = MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.SpecialName |
                                 MethodAttributes.RTSpecialName;
                var constructor = typeBuilder.DefineConstructor(attributes, CallingConventions.Standard, paramTypes);

                var ilGenerator = constructor.GetILGenerator();

                // base.ctor(arg2, arg3, ..., argN)
                ilGenerator.Emit(OpCodes.Ldarg_0);
                for (var i = 0; i < constructorParams.Length; i++)
                    ilGenerator.Emit(OpCodes.Ldarg, i + 2); // start at arg2 - arg0 is this, arg1 is synchronization provider
                ilGenerator.Emit(OpCodes.Call, baseConstructor);
                
                // _synchronizationProvider = synchronizationProvider;
                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.Emit(OpCodes.Ldarg_1);
                ilGenerator.Emit(OpCodes.Stfld, syncField);
                
                // }
                ilGenerator.Emit(OpCodes.Ret);

            }
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
                // (varArgs * cell.size) + ((argsCount * cell.size))
                ilGenerator.Emit(OpCodes.Ldarg, varArgsParam.Parameter);
                ilGenerator.Emit(OpCodes.Ldlen);
                ilGenerator.Emit(OpCodes.Conv_I4);
                ilGenerator.Emit(OpCodes.Ldc_I4, AmxCell.Size);
                ilGenerator.Emit(OpCodes.Mul);

                ilGenerator.Emit(OpCodes.Ldc_I4, argsCount * AmxCell.Size);
                ilGenerator.Emit(OpCodes.Add);
            }
            else
            {
                ilGenerator.Emit(OpCodes.Ldc_I4, argsCount * AmxCell.Size);
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
                    // (varArgs * cell.size) + ((valueCount * cell.size))
                    // For every vararg value, reserve 2 cells (the argument pointer and the value itself)
                    ilGenerator.Emit(OpCodes.Ldarg, varArgsParam.Parameter);
                    ilGenerator.Emit(OpCodes.Ldlen);
                    ilGenerator.Emit(OpCodes.Conv_I4);
                    ilGenerator.Emit(OpCodes.Ldc_I4, AmxCell.Size);
                    ilGenerator.Emit(OpCodes.Mul);

                    ilGenerator.Emit(OpCodes.Ldc_I4, valueCount * AmxCell.Size);
                    ilGenerator.Emit(OpCodes.Add);
                }
                else
                {
                    ilGenerator.Emit(OpCodes.Ldc_I4, valueCount * AmxCell.Size);
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
           
        private static unsafe void EmitInvokeNative(ILGenerator ilGenerator, IntPtr native, NativeIlGenContext context)
        {
            var formatString = GenerateCallFormat(context);
            // if (_synchronizationProvider.InvokeRequired)
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldfld, context.SynchronizationProviderField);
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
            ilGenerator.Emit(OpCodes.Ldfld, context.SynchronizationProviderField);
            ilGenerator.Emit(OpCodes.Ldc_I4, native.ToInt32());
            ilGenerator.Emit(OpCodes.Newobj, typeof(IntPtr).GetConstructor(new[] {typeof(int)})!);
            ilGenerator.Emit(OpCodes.Ldstr, formatString);
            EmitFormatVarArgs();
            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.EmitCall(typeof(NativeUtils), nameof(NativeUtils.SynchronizeInvoke));

            // else
            ilGenerator.Emit(OpCodes.Br, endifLabel);
            ilGenerator.MarkLabel(elseLabel);

            // SampSharp.Core.Hosting.Interop.FastNativeInvoke(new IntPtr($0), format, ptr);
            ilGenerator.Emit(OpCodes.Ldc_I4, native.ToInt32());
            ilGenerator.Emit(OpCodes.Newobj, typeof(IntPtr).GetConstructor(new[] {typeof(int)})!);
            ilGenerator.Emit(OpCodes.Ldstr, formatString);
            EmitFormatVarArgs();
            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.EmitCall(typeof(Interop), nameof(Interop.FastNativeInvoke));

            // endif
            ilGenerator.Emit(OpCodes.Br, endifLabel);
            ilGenerator.MarkLabel(endifLabel);
        }

        private static unsafe void EmitInParamAssignment(ILGenerator ilGenerator, NativeIlGenContext context, out LocalBuilder[] paramBuffers)
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
                        ilGenerator.Emit(OpCodes.Ldc_I4, i * AmxCell.Size);
                        ilGenerator.Emit(OpCodes.Add);
                    }
                }

                var param = context.Parameters[i];
                if ((param.Type & NativeParameterType.ValueTypeMask) != 0 && !param.Type.HasFlag(NativeParameterType.Array)) // in/out int/float
                {
                    // args[i] = NativeUtils.IntPointerToInt(values + valueIndex);
                    EmitBufferLocation();

                    ilGenerator.Emit(OpCodes.Ldloc_1);
                    ilGenerator.Emit(OpCodes.Ldc_I4, valueIndex * AmxCell.Size);
                    ilGenerator.Emit(OpCodes.Add);
                    ilGenerator.EmitCall(OpCodes.Call, typeof(NativeUtils), nameof(NativeUtils.IntPointerToInt));
                    ilGenerator.Emit(OpCodes.Stind_I4);

                    if (param.Property != null)
                    {
                        // values[valueIndex] = Property;
                        ilGenerator.Emit(OpCodes.Ldloc_1);
                        ilGenerator.Emit(OpCodes.Ldc_I4, valueIndex * AmxCell.Size);
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
                        ilGenerator.Emit(OpCodes.Ldc_I4, valueIndex * AmxCell.Size);
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
                    var strBufferSpan = ilGenerator.EmitSpanAlloc(strLen);

                    // NativeUtils.GetBytes(textString, strBuffer);
                    ilGenerator.Emit(OpCodes.Ldarg, param.Parameter);
                    ilGenerator.Emit(OpCodes.Ldloc, strBufferSpan);
                    ilGenerator.EmitCall(typeof(NativeUtils), nameof(NativeUtils.GetBytes));

                    // args[i] = NativeUtils.BytePointerToInt(strBuffer);
                    EmitBufferLocation(); // args[i]
                    ilGenerator.EmitByteSpanToPointer(strBufferSpan);
                    ilGenerator.EmitCall(typeof(NativeUtils), nameof(NativeUtils.BytePointerToInt));
                    ilGenerator.Emit(OpCodes.Stind_I4);
                }
                else if (param.Type == NativeParameterType.StringReference)
                {
                    ilGenerator.EmitThrowOnOutOfRangeLength(param.LengthParam);

                    // var strBuf = stackalloc/new byte[...]
                    var strBuf = ilGenerator.EmitSpanAlloc(param.LengthParam.Parameter, AmxCell.Size);
                    paramBuffers[i] = strBuf;

                    // args[i] = NativeUtils.BytePointerToInt(strBufPtr);
                    EmitBufferLocation(); // args[i]
                    ilGenerator.EmitByteSpanToPointer(strBuf);
                    ilGenerator.EmitCall(typeof(NativeUtils), nameof(NativeUtils.BytePointerToInt));
                    ilGenerator.Emit(OpCodes.Stind_I4);
                }
                else if (param.Type.HasFlag(NativeParameterType.Array))
                {
                    if (param.Property != null)
                    {

                        throw new InvalidOperationException("Unsupported identifier property type");
                    }

                    ilGenerator.EmitThrowOnOutOfRangeLength(param.LengthParam);

                    // var arraySpan = NativeUtils.ArrayToIntSpan(array/null,len)
                    var arraySpan = ilGenerator.DeclareLocal(typeof(Span<int>));
                    if (param.Parameter.IsOut)
                        ilGenerator.Emit(OpCodes.Ldnull);
                    else
                        ilGenerator.Emit(OpCodes.Ldarg, param.Parameter);
                    ilGenerator.Emit(OpCodes.Ldarg, param.LengthParam.Parameter);
                    ilGenerator.EmitCall(typeof(NativeUtils), nameof(NativeUtils.ArrayToIntSpan));
                    ilGenerator.Emit(OpCodes.Stloc, arraySpan);
                    
                    // args[i] = NativeUtils.IntPointerToInt(arraySpan);
                    EmitBufferLocation(); // args[i]
                    ilGenerator.EmitIntSpanToPointer(arraySpan);
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
                    ilGenerator.Emit(OpCodes.Newobj, typeof(VarArgsState).GetConstructor(Array.Empty<Type>())!);
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
                        ilGenerator.Emit(OpCodes.Ldc_I4, valueIndex * AmxCell.Size);
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
                    var method = typeof(NativeUtils).GetMethod(nameof(NativeUtils.IntSpanToArray))!
                        .MakeGenericMethod(param.InputType.GetElementType()!.GetElementType()!);
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
                    ilGenerator.EmitCall(OpCodes.Call, typeof(NativeUtils).GetMethod(nameof(NativeUtils.GetString))!, null);
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

        private static void EmitConvertToInt(ILGenerator ilGenerator, NativeParameterType type)
        {
            if (type.HasFlag(NativeParameterType.Single))
                ilGenerator.EmitConvert<float,int>();
            if (type.HasFlag(NativeParameterType.Bool))
                ilGenerator.EmitConvert<bool,int>();
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
                        formatStringBuilder.Append(param.IsReferenceInput ? 'r' : 'd');
                       break;
                    case NativeParameterType.Int32Reference:
                    case NativeParameterType.SingleReference:
                    case NativeParameterType.BoolReference:
                        formatStringBuilder.Append('R');
                        break;
                    case NativeParameterType.String:
                        formatStringBuilder.Append('s');
                        break;
                    case NativeParameterType.StringReference:
                        formatStringBuilder.Append(CultureInfo.InvariantCulture, $"S[*{param.LengthParam.Index}]");
                        break;
                    case NativeParameterType.Int32Array:
                    case NativeParameterType.SingleArray:
                    case NativeParameterType.BoolArray:
                        formatStringBuilder.Append(CultureInfo.InvariantCulture, $"a[*{param.LengthParam.Index}]");
                        break;
                    case NativeParameterType.Int32ArrayReference:
                    case NativeParameterType.SingleArrayReference:
                    case NativeParameterType.BoolArrayReference:
                        formatStringBuilder.Append(CultureInfo.InvariantCulture, $"A[*{param.LengthParam.Index}]");
                        break;
                    case NativeParameterType.VarArgs:
                        // format apended at runtime
                        break;
                    default:
                        throw new InvalidOperationException("Unknown parameter type");
                }
            }

            return formatStringBuilder.ToString();
        }
        
        private static bool WrapMethod(Type type, NativeMethodAttribute attr, MethodInfo method, string[] objectIdentifiers,
            TypeBuilder typeBuilder, FieldInfo syncField)
        {
            // Determine the details about the native.
            var name = attr.Function ?? method.Name;
            var idIndex = Math.Max(0, attr.IdentifiersIndex);

            var result = CreateMethodBuilder(name, type, attr.Lengths ?? Array.Empty<uint>(), typeBuilder, method,
                attr.IgnoreIdentifiers ? Array.Empty<string>() : objectIdentifiers, idIndex, syncField, attr.ReferenceIndices);
            return result != null;
        }

        private static bool WrapProperty(Type type, NativePropertyAttribute propertyAttribute, string[] objectIdentifiers,
            TypeBuilder typeBuilder, PropertyInfo property, MethodInfo get, MethodInfo set, FieldInfo syncField)
        {
            var identifiers = propertyAttribute.IgnoreIdentifiers ? Array.Empty<string>() : objectIdentifiers;

            MethodBuilder getMethodBuilder = null;
            MethodBuilder setMethodBuilder = null;

            if (get != null)
            {
                var nativeName = propertyAttribute.GetFunction ?? $"Get{property.Name}";
                var argLengths = propertyAttribute.GetLengths ?? Array.Empty<uint>();
                getMethodBuilder = CreateMethodBuilder(nativeName, type, argLengths, typeBuilder, get, identifiers, 0, syncField, null);
            }

            if (set != null)
            {
                var nativeName = propertyAttribute.SetFunction ?? $"Set{property.Name}";
                var argLengths = propertyAttribute.SetLengths ?? Array.Empty<uint>();
                setMethodBuilder = CreateMethodBuilder(nativeName, type, argLengths, typeBuilder, set, identifiers, 0, syncField, null);
            }

            if (getMethodBuilder == null && setMethodBuilder == null)
                return false;
            
            // Define the wrapping property.
            var wrapProperty = typeBuilder.DefineProperty(property.Name, PropertyAttributes.None, property.PropertyType,
                Type.EmptyTypes);

            if(getMethodBuilder != null)
                wrapProperty.SetGetMethod(getMethodBuilder);

            if(setMethodBuilder != null)
                wrapProperty.SetSetMethod(setMethodBuilder);
            
            return true;
        }
        
        private static MethodBuilder CreateMethodBuilder(string nativeName, Type proxyType,
            uint[] nativeArgumentLengths,
            TypeBuilder typeBuilder, MethodInfo method, string[] identifierPropertyNames, int idIndex,
            FieldInfo syncField, int[] referenceIndices)
        {
            return CreateMethodBuilder(typeBuilder,
                CreateContext(nativeName, proxyType, nativeArgumentLengths, method, identifierPropertyNames, idIndex,
                    syncField, referenceIndices));
        }

        private static MethodBuilder CreateMethodBuilder(TypeBuilder typeBuilder, NativeIlGenContext context)
        {
            var native = Interop.FastNativeFind(context.NativeName);

            if (native == IntPtr.Zero)
                return null;

            var methodBuilder = typeBuilder.DefineMethod(context.BaseMethod.Name, context.MethodOverrideAttributes, context.BaseMethod.ReturnType, context.MethodParameterTypes);
            var ilGenerator = methodBuilder.GetILGenerator();
            
            Generate(ilGenerator, native, context);

            return methodBuilder;
        }

        private static NativeIlGenContext CreateContext(string nativeName, Type proxyType,
            uint[] nativeArgumentLengths, MethodInfo method, string[] identifierPropertyNames, int idIndex,
            FieldInfo syncField, int[] referenceIndices)
        {
            var methodParameters = method.GetParameters();
            identifierPropertyNames ??= Array.Empty<string>();
            
            // Seed parameters array with indices
            var parameters = new NativeIlGenParam[methodParameters.Length + identifierPropertyNames.Length];
            for (var i = 0; i < parameters.Length; i++)
            {
                parameters[i] = new NativeIlGenParam
                {
                    Index = i
                };
            }

            // Populate identifier parameters
            for (var i = 0; i < identifierPropertyNames.Length; i++)
            {
                parameters[idIndex + i].Property = proxyType.GetProperty(identifierPropertyNames[i],
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            }

            // Populate method parameter based parameters
            var lengthIndex = 0;
            for (var i = 0; i < methodParameters.Length; i++)
            {
                var methodParameter = methodParameters[i];
                var paramIndex = i >= idIndex ? i + identifierPropertyNames.Length : i;
                var parameter = parameters[paramIndex];

                parameter.IsReferenceInput = referenceIndices != null && referenceIndices.Contains(i);
                parameter.Parameter = methodParameter;

                if (parameter.RequiresLength && nativeArgumentLengths.Length > 0)
                {
                    if(nativeArgumentLengths.Length == lengthIndex)
                        throw new InvalidOperationException("No length provided for native argument");

                    parameter.LengthParam = parameters[nativeArgumentLengths[lengthIndex++]];
                }
            }

            // Find length parameters for arrays and string refs
            if (nativeArgumentLengths.Length == 0)
            {
                for (var i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i].RequiresLength)
                    {
                        for (var j = i + 1; j < parameters.Length; j++)
                        {
                            if (parameters[j].Type == NativeParameterType.Int32 && !parameters[j].IsLengthParam)
                            {
                                parameters[j].IsLengthParam = true;
                                parameters[i].LengthParam = parameters[j];
                                break;
                            }
                        }

                        if (parameters[i].LengthParam == null)
                            throw new InvalidOperationException("No length provided for native argument");
                    }
                }
            }

            var methodParameterTypes = method.GetParameters()
                .Select(p => p.ParameterType)
                .ToArray();

            var methodOverrideAttributes =
                (method.IsPublic ? MethodAttributes.Public : MethodAttributes.Family) |
                MethodAttributes.ReuseSlot |
                MethodAttributes.Virtual |
                MethodAttributes.HideBySig;

            return new NativeIlGenContext
            {
                NativeName = nativeName,
                BaseMethod = method,
                SynchronizationProviderField = syncField,
                Parameters = parameters,
                MethodParameterTypes = methodParameterTypes,
                MethodOverrideAttributes = methodOverrideAttributes,
                HasVarArgs = parameters.Any(x=> x.Type == NativeParameterType.VarArgs)
            };
        }

        
        private sealed record KnownType(Type Type, bool IsGenerated);
    }
}
