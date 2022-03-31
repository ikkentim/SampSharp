// SampSharp
// Copyright 2018 Tim Potze
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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using SampSharp.Core.Callbacks;
using SampSharp.Core.Logging;
using SampSharp.Core.Natives.NativeObjects.FastNatives;

namespace SampSharp.Core.Hosting
{
    /// <summary>
    /// Contains interop functions for the SampSharp plugin.
    /// </summary>
    public static unsafe class Interop
    {
        /// <summary>
        /// Registers a callback in the SampSharp plugin.
        /// </summary>
        /// <param name="data">The callback data.</param>
        [DllImport("SampSharp", EntryPoint = "sampsharp_register_callback", CallingConvention = CallingConvention.StdCall)]
        public static extern void RegisterCallback(IntPtr data);
       
        /// <summary>
        /// Gets a pointer to a native.
        /// </summary>
        /// <param name="name">The name of the native.</param>
        /// <returns>A pointer to a native.</returns>
        [DllImport("SampSharp", EntryPoint = "sampsharp_fast_native_find", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FastNativeFind(string name);
        
        /// <summary>
        /// Invokes a native by a pointer.
        /// </summary>
        /// <param name="native">The pointer to the native.</param>
        /// <param name="format">The format of the arguments.</param>
        /// <param name="args">A pointer to the arguments array.</param>
        /// <returns>The return value of the native.</returns>
        [DllImport("SampSharp", EntryPoint = "sampsharp_fast_native_invoke", CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe int FastNativeInvoke(IntPtr native, string format, int* args);

        public static int FastNativeInvokeViaApi(IntPtr native, string format, int* args)
        {
            var bytes = Encoding.ASCII.GetByteCount(format);
            var formatSt = stackalloc byte[bytes];
            Encoding.ASCII.GetBytes(format, new Span<byte>(formatSt, bytes));

            return api->InvokeNative(native.ToPointer(), (char *)formatSt, args);
        }

        public static IntPtr FastNativeFindViaApi(string name)
        {
            var bytes = Encoding.ASCII.GetByteCount(name);
            var nameStack = stackalloc byte[bytes];
            Encoding.ASCII.GetBytes(name, new Span<byte>(nameStack, bytes));

            return (IntPtr)api->FindNative((char *)nameStack);
        }

        public static void Print(string txt)
        {
            var format = stackalloc byte[3];
            format[0] = 0x25; // "%s\0"
            format[1] = 0x73;
            format[2] = 0x00;
            
            var bytes = Encoding.ASCII.GetByteCount(txt);

            if (bytes < 200)
            {
                var txtStack = stackalloc byte[bytes];
                Encoding.ASCII.GetBytes(txt, new Span<byte>(txtStack, bytes));
                api->PluginData->Logprintf((char *)format, (char *)txtStack);
            }
            else
            {
                var ptr = Marshal.StringToHGlobalAnsi(txt);

                try
                {
                    api->PluginData->Logprintf((char *)format, (char *)ptr.ToPointer());
                }
                finally
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }
        }

        private static SampSharpApi* api;

        [DllImport("SampSharp", EntryPoint = "sampsharp_get_api", CallingConvention = CallingConvention.StdCall)]
        private static extern SampSharpApi* GetApi(void *publicCall, void *tick);

        public static SampSharpApi* Api => api;
        
        public static void Testing()
        {
            Console.WriteLine("testing things");
            
            Print("hello worlddd");
        }

        internal static void Init()
        {
            var ptr = (delegate* unmanaged[Stdcall] <IntPtr, char*, IntPtr, IntPtr, void>)&PublicCallUnm;
            var ptrTick = (delegate* unmanaged[Stdcall]<void>)&TickUnm;

            api = GetApi(ptr, ptrTick);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static void PublicCallUnm(IntPtr amx, char *name, IntPtr parameters, IntPtr retval)
        {
            var client = InternalStorage.RunningClient as HostedGameModeClient;

            var nameStr = Marshal.PtrToStringAnsi((IntPtr)name);
            client?.PublicCall(amx, nameStr, parameters, retval);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static void TickUnm()
        {
            var client = InternalStorage.RunningClient as HostedGameModeClient;

            client?.Tick();
        }

        internal static int PublicCall(string name, IntPtr argumentsPtr, int length)
        {
            var client = InternalStorage.RunningClient as HostedGameModeClient;

            return client?.PublicCall(name, argumentsPtr, length) ?? 1;
        }

        internal static void Tick()
        {
            var client = InternalStorage.RunningClient as HostedGameModeClient;

            client?.Tick();
        }
    }

    internal interface INewCallbackParameter
    {
        object GetValue(IntPtr amx, IntPtr parameter);
    }

    internal class NewCallback
    {
        private readonly INewCallbackParameter[] _parameters;
        private readonly object _target;
        private readonly MethodInfo _method;
        private readonly bool _wrapped;
        private readonly object[] _parametersBuffer;
        private readonly FastMethodInfo _fastMethod;
        private readonly object[] _wrapBuffer;
        public NewCallback(INewCallbackParameter[] parameters, object target, MethodInfo method, bool wrapped)
        {
            _parameters = parameters;
            _parametersBuffer = new object[parameters.Length];
            _target = target;
            _method = method;
            _wrapped = wrapped;
            _fastMethod = new FastMethodInfo(method);

            if (wrapped) _wrapBuffer = new object[] { _parametersBuffer };
        }

        private static INewCallbackParameter ParameterForType1(Type type)
        {
            if (type == typeof(int))
            {
                return NewCallbackParameterInt.Instance;
            }

            if (type == typeof(bool))
            {
                return NewCallbackParameterBoolean.Instance;
            }

            if (type == typeof(float))
            {
                return NewCallbackParameterSingle.Instance;
            }

            if (type == typeof(string))
            {
                return NewCallbackParameterString.Instance;
            }

            return null;
        }

        private static INewCallbackParameter ParameterForType2(Type type, int offset)
        {
            if (type == typeof(int[]))
            {
                return new NewCallbackParameterIntArray(offset);
            }

            if (type == typeof(bool[]))
            {
                return new NewCallbackParameterBooleanArray(offset);
            }

            if (type == typeof(float[]))
            {
                return new NewCallbackParameterSingleArray(offset);
            }

            return null;
        }

        public static NewCallback For(object target, MethodInfo method, Type[] parameterTypes,
            uint?[] lengthIndices = null)
        {
            var parameters = new INewCallbackParameter[parameterTypes.Length];

            if (lengthIndices != null && lengthIndices.Length != parameterTypes.Length)
            {
                throw new ArgumentException("lengthIndices length must be same as parameterTypes length",
                    nameof(lengthIndices));
            }

            for (var i = 0; i < parameterTypes.Length; i++)
            {
                parameters[i] = ParameterForType1(parameterTypes[i]);

                if (parameters[i] != null)
                {
                    continue;
                }

                var index = (int?)lengthIndices?[i] ?? (i + 1);
                var offset = index - i;

                if (index >= parameterTypes.Length)
                {
                    throw new InvalidOperationException("Callback parameter length index out of bounds.");
                }

                // todo: check type of length? also in other method

                parameters[i] = ParameterForType2(parameterTypes[i], offset);

                if (parameters[i] != null)
                {
                    continue;
                }

                throw new CallbackRegistrationException("Unknown callback parameter type.");
            }

            return new NewCallback(parameters, target, method, true);
        }

        public static NewCallback For(object target, MethodInfo method)
        {
            var methodParameters = method.GetParameters();
            var parameters = methodParameters.Select(parameter =>
                {
                    var par = ParameterForType1(parameter.ParameterType);

                    if (par != null)
                    {
                        return par;
                    }

                    var attribute =
                        CustomAttributeExtensions
                            .GetCustomAttribute<ParameterLengthAttribute>((ParameterInfo)parameter);
                    var index = (int?)attribute?.Index ?? parameter.Position + 1;
                    var offset = index - parameter.Position;


                    if (index >= methodParameters.Length)
                    {
                        throw new InvalidOperationException("Callback parameter length index out of bounds.");
                    }

                    par = ParameterForType2(parameter.ParameterType, offset);

                    if (par != null)
                    {
                        return par;
                    }

                    throw new CallbackRegistrationException("Unknown callback parameter type.");
                })
                .ToArray();

            return new NewCallback(parameters, target, method, false);
        }

        public class FastMethodInfo
        {
            private delegate object ReturnValueDelegate(object instance, object[] arguments);
            private delegate void VoidDelegate(object instance, object[] arguments);

            public FastMethodInfo(MethodInfo methodInfo)
            {
                var instanceExpression = Expression.Parameter(typeof(object), "instance");
                var argumentsExpression = Expression.Parameter(typeof(object[]), "arguments");
                var argumentExpressions = new List<Expression>();
                var parameterInfos = methodInfo.GetParameters();
                for (var i = 0; i < parameterInfos.Length; ++i)
                {
                    var parameterInfo = parameterInfos[i];
                    argumentExpressions.Add(Expression.Convert(Expression.ArrayIndex(argumentsExpression, Expression.Constant(i)), parameterInfo.ParameterType));
                }
                var callExpression = Expression.Call(!methodInfo.IsStatic ? Expression.Convert(instanceExpression, methodInfo.ReflectedType) : null, methodInfo, argumentExpressions);
                if (callExpression.Type == typeof(void))
                {
                    var voidDelegate = Expression.Lambda<VoidDelegate>(callExpression, instanceExpression, argumentsExpression).Compile();
                    Delegate = (instance, arguments) => { voidDelegate(instance, arguments); return null; };
                }
                else
                    Delegate = Expression.Lambda<ReturnValueDelegate>(Expression.Convert(callExpression, typeof(object)), instanceExpression, argumentsExpression).Compile();
            }

            private ReturnValueDelegate Delegate { get; }

            public object Invoke(object instance, object[] arguments)
            {
                return Delegate(instance, arguments);
            }
        }

        public unsafe void Invoke(IntPtr amx, IntPtr parameters, IntPtr retval)
        {
            var paramCount = *(int*)parameters.ToPointer() / 4; // cell size

            if (paramCount != _parameters.Length)
            {
                CoreLog.Log(CoreLogLevel.Error,
                    $"Callback parameter mismatch. Expected {_parameters.Length} but received {paramCount} parameters.");
                return;
            }

            var args = _parametersBuffer;// new object[paramCount];

            for (var i = 0; i < paramCount; i++)
            {
                var param = _parameters[i];
                args[i] = param.GetValue(amx, IntPtr.Add(parameters, 4 + (4 * i)));
            }

            if (_wrapBuffer != null)
            {
                args = _wrapBuffer;
            }

            
            // var result = _method.Invoke(_target, args);
            var result = _fastMethod.Invoke(_target, args);

            if (retval != IntPtr.Zero)
            {
                *(int*)retval = ObjectToInt(result);
            }

            // to avoid holding on to handles, clear buffer
            for (var i = 0; i < paramCount; i++)
            {
                _parametersBuffer[i] = null;
            }
        }

        private static int ObjectToInt(object obj)
        {
            return obj switch
            {
                bool value => value ? 1 : 0,
                int value => value,
                float value => ValueConverter.ToInt32(value),
                _ => 1
            };
        }
    }

    internal class NewCallbackParameterBoolean : INewCallbackParameter
    {
        public static readonly NewCallbackParameterBoolean Instance = new();
        public unsafe object GetValue(IntPtr amx, IntPtr parameter)
        {
            return *(int*)parameter.ToPointer() != 0;
        }
    }

    internal class NewCallbackParameterBooleanArray : INewCallbackParameter
    {
        private readonly int _lengthOffset;

        public NewCallbackParameterBooleanArray(int lengthOffset)
        {
            _lengthOffset = lengthOffset;
        }

        public unsafe object GetValue(IntPtr amx, IntPtr parameter)
        {
            var addr = *(int *)parameter;
            var len = *(int*)IntPtr.Add(parameter, _lengthOffset * 4).ToPointer(); // assuming length is next parameter

            int* phys;
            Interop.Api->PluginData->AmxExports->GetAddr(amx.ToPointer(), addr, (void**)&phys);
            
            if ((IntPtr)phys == IntPtr.Zero)
            {
                return null;
            }

            var result = new bool[len];

            for (var i = 0; i < len; i++)
            {
                result[i] = phys[i] != 0;
            }

            return result;
        }
    }

    internal class NewCallbackParameterInt : INewCallbackParameter
    {
        public static readonly NewCallbackParameterInt Instance = new();
        public unsafe object GetValue(IntPtr amx, IntPtr parameter)
        {
            return *(int*)parameter.ToPointer();
        }
    }

    internal class NewCallbackParameterIntArray : INewCallbackParameter
    {
        private readonly int _lengthOffset;

        public NewCallbackParameterIntArray(int lengthOffset)
        {
            _lengthOffset = lengthOffset;
        }

        public unsafe object GetValue(IntPtr amx, IntPtr parameter)
        {
            var addr = *(int *)parameter;

            var len = *(int*)IntPtr.Add(parameter, _lengthOffset * 4).ToPointer(); // assuming length is next parameter
            
            int* phys;
            Interop.Api->PluginData->AmxExports->GetAddr(amx.ToPointer(), addr, (void**)&phys);
            
            if ((IntPtr)phys == IntPtr.Zero)
            {
                return null;
            }

            var result = new int[len];
            new Span<int>(phys, len).CopyTo(new Span<int>(result));

            return result;
        }
    }

    internal class NewCallbackParameterSingle : INewCallbackParameter
    {
        public static readonly NewCallbackParameterSingle Instance = new();
        public unsafe object GetValue(IntPtr amx, IntPtr parameter)
        {
            return *(float*)parameter.ToPointer();
        }
    }

    internal class NewCallbackParameterSingleArray : INewCallbackParameter
    {
        private readonly int _lengthOffset;

        public NewCallbackParameterSingleArray(int lengthOffset)
        {
            _lengthOffset = lengthOffset;
        }

        public unsafe object GetValue(IntPtr amx, IntPtr parameter)
        {
            var addr = *(int *)parameter;
            var len = *(int*)IntPtr.Add(parameter, _lengthOffset * 4).ToPointer();
            
            float* phys;
            Interop.Api->PluginData->AmxExports->GetAddr(amx.ToPointer(), addr, (void**)&phys);
            
            if ((IntPtr)phys == IntPtr.Zero)
            {
                return null;
            }

            var result = new float[len];
            new Span<float>(phys, len).CopyTo(new Span<float>(result));

            return result;
        }
    }

    internal class NewCallbackParameterString : INewCallbackParameter
    {
        public static readonly NewCallbackParameterString Instance = new();

        public unsafe object GetValue(IntPtr amx, IntPtr parameter)
        {
            var addr = *(int *)parameter;

            void* phys;
            Interop.Api->PluginData->AmxExports->GetAddr(amx.ToPointer(), addr, &phys);

            if ((IntPtr)phys == IntPtr.Zero)
            {
                return null;
            }

            var len = 0;
            Interop.Api->PluginData->AmxExports->StrLen(phys, &len);
            len++;// 1 space for terminator

            var buf = len < 100 ? stackalloc byte[len] : new byte[len];

            fixed (byte* p = &buf.GetPinnableReference())
            {
                Interop.Api->PluginData->AmxExports->GetString(p, phys, 0, len);
            }

            return NativeUtils.GetString(buf);
        }
    }
}