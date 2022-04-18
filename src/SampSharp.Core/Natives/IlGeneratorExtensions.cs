// SampSharp
// Copyright 2022 Tim Potze
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
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace SampSharp.Core.Natives;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "Documentation for generated IL code.")]
internal static class IlGeneratorExtensions
{
    private const int MaxStackAllocSize = 256;

    /// <summary>
    /// Emits the call to the specified <paramref name="methodInfo"/>.
    /// </summary>
    /// <param name="ilGenerator">The il generator.</param>
    /// <param name="opCode">The op code to use for calling the method.</param>
    /// <param name="methodInfo">The method to call.</param>
    public static void EmitCall(this ILGenerator ilGenerator, OpCode opCode, MethodInfo methodInfo)
    {
        ilGenerator.EmitCall(opCode, methodInfo, null);
    }
        
    /// <summary>
    /// Emits the call to the specified <paramref name="methodInfo"/>.
    /// </summary>
    /// <param name="ilGenerator">The il generator.</param>
    /// <param name="methodInfo">The method to call.</param>
    public static void EmitCall(this ILGenerator ilGenerator, MethodInfo methodInfo)
    {
        ilGenerator.EmitCall(methodInfo.IsFinal || !methodInfo.IsVirtual ? OpCodes.Call : OpCodes.Callvirt,
            methodInfo);
    }

    /// <summary>
    /// Emits the call to the specified <paramref name="methodName"/> in the specified <paramref name="type"/>.
    /// </summary>
    /// <param name="ilGenerator">The il generator.</param>
    /// <param name="opCode">The op code to use for calling the method.</param>
    /// <param name="type">The type which contains the type.</param>
    /// <param name="methodName">The name of the method to call</param>
    public static void EmitCall(this ILGenerator ilGenerator, OpCode opCode, Type type, string methodName)
    {
        ilGenerator.EmitCall(opCode, type.GetMethod(methodName));
    }
        
    /// <summary>
    /// Emits the call to the specified <paramref name="methodName"/> in the specified <paramref name="type"/>.
    /// </summary>
    /// <param name="ilGenerator">The il generator.</param>
    /// <param name="type">The type which contains the type.</param>
    /// <param name="methodName">The name of the method to call</param>
    /// <param name="types">The parameter types of the method.</param>
    public static void EmitCall(this ILGenerator ilGenerator, Type type, string methodName, params Type[] types)
    {
        ilGenerator.EmitCall(type.GetMethod(methodName, types));
    }
        
    /// <summary>
    /// Emits the call to the specified <paramref name="methodName"/> in the specified <paramref name="type"/>.
    /// </summary>
    /// <param name="ilGenerator">The il generator.</param>
    /// <param name="type">The type which contains the type.</param>
    /// <param name="methodName">The name of the method to call</param>
    public static void EmitCall(this ILGenerator ilGenerator, Type type, string methodName)
    {
        ilGenerator.EmitCall(type.GetMethod(methodName));
    }

    /// <summary>
    /// Emits the call to the getter of the specified <paramref name="property"/>.
    /// </summary>
    /// <typeparam name="T">The type which contains the property.</typeparam>
    /// <param name="ilGenerator">The il generator.</param>
    /// <param name="opCode">The op code to use for calling the getter method.</param>
    /// <param name="property">An expression of property. e.g. <code>(string x) => x.Length</code>.</param>
    public static void EmitPropertyGetterCall<T>(this ILGenerator ilGenerator, OpCode opCode,
        Expression<Func<T, object>> property)
    {
        ilGenerator.EmitCall(opCode,
            ((PropertyInfo) ((MemberExpression) ((UnaryExpression) property.Body).Operand).Member).GetMethod);
    }

    /// <summary>
    /// Emits the a bitwise conversion from <typeparamref name="TFrom"/> to <typeparamref name="TTo"/>. Only int, float, bool are supported.
    /// </summary>
    /// <typeparam name="TFrom">The type to convert from.</typeparam>
    /// <typeparam name="TTo">The type to convert to.</typeparam>
    /// <param name="ilGenerator">The il generator.</param>
    /// <exception cref="InvalidOperationException">Thrown when unsupported type is supplied.</exception>
    public static void EmitConvert<TFrom, TTo>(this ILGenerator ilGenerator)
    {
        string methodName = null;
        if (typeof(TTo) == typeof(int))
        {
            methodName = nameof(ValueConverter.ToInt32);
        }
        else if (typeof(TFrom) == typeof(int))
        {
            if (typeof(TTo) == typeof(float))
                methodName = nameof(ValueConverter.ToSingle);
            else if (typeof(TTo) == typeof(bool))
                methodName = nameof(ValueConverter.ToBoolean);
        }

        if (methodName == null)
            throw new InvalidOperationException("Unsupported types");

        ilGenerator.EmitCall(typeof(ValueConverter), methodName, typeof(TFrom));
    }

    /// <summary>
    /// Emits the specified <paramref name="opCode"/> with the argument number of the specified <paramref name="paramInfo"/>.
    /// </summary>
    /// <param name="ilGenerator">The il generator.</param>
    /// <param name="opCode">The op code.</param>
    /// <param name="paramInfo">The parameter information.</param>
    public static void Emit(this ILGenerator ilGenerator, OpCode opCode, ParameterInfo paramInfo)
    {
        ilGenerator.Emit(opCode, (IsStatic(paramInfo.Member) ? 0 : 1) + paramInfo.Position);
    }
        
    /// <summary>
    /// Emits a conversion from the specified <paramref name="span"/> to a byte pointer. The byte pointer is on the stack after the emitted code. The contents of the span is stored in a pinned local.
    /// </summary>
    /// <param name="ilGenerator">The il generator.</param>
    /// <param name="span">The span.</param>
    public static void EmitByteSpanToPointer(this ILGenerator ilGenerator, LocalBuilder span)
    {
        // fixed(byte* ptr = span)
        // leave ptr on stack
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
        
    /// <summary>
    /// Emits a conversion from the specified <paramref name="span"/> to an int pointer. The int pointer is on the stack after the emitted code. The contents of the span is stored in a pinned local.
    /// </summary>
    /// <param name="ilGenerator">The il generator.</param>
    /// <param name="span">The span.</param>
    public static void EmitIntSpanToPointer(this ILGenerator ilGenerator, LocalBuilder span)
    {
        // fixed(byte* ptr = span)
        // leave ptr on stack
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

    /// <summary>
    /// Emits a <see cref="ArgumentOutOfRangeException"/> when the specified <paramref name="param"/> is 0 or less.
    /// </summary>
    /// <param name="ilGenerator">The il generator.</param>
    /// <param name="param">The parameter.</param>
    public static void EmitThrowOnOutOfRangeLength(this ILGenerator ilGenerator, NativeIlGenParam param)
    {
        // if ($param <= 0)
        ilGenerator.Emit(OpCodes.Ldarg, param.Parameter);
        ilGenerator.Emit(OpCodes.Ldc_I4_0);
        ilGenerator.Emit(OpCodes.Cgt);
        ilGenerator.Emit(OpCodes.Ldc_I4_0);
        ilGenerator.Emit(OpCodes.Ceq);
        var falseLabel = ilGenerator.DefineLabel();
        ilGenerator.Emit(OpCodes.Brfalse, falseLabel);

        // throw new ArgumentOutOfRangeException($paramName);
        ilGenerator.Emit(OpCodes.Ldstr, param.Name);
        ilGenerator.Emit(OpCodes.Newobj,
            typeof(ArgumentOutOfRangeException).GetConstructor(new[] {typeof(string)})!);
        ilGenerator.Emit(OpCodes.Throw);
        ilGenerator.MarkLabel(falseLabel);
    }

    /// <summary>
    /// Emits the allocation of a byte span with a size specified by the <paramref name="lengthArg"/> multiplied by the <paramref name="multiplier"/>.
    /// </summary>
    /// <param name="ilGenerator">The il generator.</param>
    /// <param name="lengthArg">The length argument.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns>The local which contains the allocated byte span.</returns>
    public static LocalBuilder EmitSpanAlloc(this ILGenerator ilGenerator, ParameterInfo lengthArg, int multiplier)
    {
        return EmitSpanAlloc(ilGenerator, () =>
        {
            ilGenerator.Emit(OpCodes.Ldarg, lengthArg);
            ilGenerator.Emit(OpCodes.Ldc_I4, multiplier);
            ilGenerator.Emit(OpCodes.Mul);
        });
    }

    /// <summary>
    /// Emits the allocation of a byte span with the specified <paramref name="length"/>.
    /// </summary>
    /// <param name="ilGenerator">The il generator.</param>
    /// <param name="length">The length.</param>
    /// <returns>The local which contains the allocated byte span.</returns>
    public static LocalBuilder EmitSpanAlloc(this ILGenerator ilGenerator, LocalBuilder length)
    {
        return EmitSpanAlloc(ilGenerator, () => ilGenerator.Emit(OpCodes.Ldloc, length));
    }
        
    /// <summary>
    /// Emits the allocation of a byte span with the length which is loaded onto the stack by the code emitted in the <paramref name="loadLength"/> action.
    /// </summary>
    /// <param name="ilGenerator">The il generator.</param>
    /// <param name="loadLength">Loads the length onto the stack.</param>
    /// <returns>The local which contains the allocated byte span.</returns>
    public static LocalBuilder EmitSpanAlloc(this ILGenerator ilGenerator, Action loadLength)
    {
        var span = ilGenerator.DeclareLocal(typeof(Span<byte>));

        // Span<byte> span = (($length < MaxStackAllocSize) ? stackalloc byte[$length] : new Span<byte>(new byte[$length]));
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
        ilGenerator.Emit(OpCodes.Newobj, typeof(Span<byte>).GetConstructor(new[] {typeof(void*), typeof(int)})!);
        ilGenerator.Emit(OpCodes.Stloc, span);
        var labelDone = ilGenerator.DefineLabel();
        ilGenerator.Emit(OpCodes.Br, labelDone);

        // else...
        ilGenerator.MarkLabel(labelArrayAlloc);
        ilGenerator.Emit(OpCodes.Ldloca, span);
        loadLength();
        ilGenerator.Emit(OpCodes.Newarr, typeof(byte));
        ilGenerator.Emit(OpCodes.Call, typeof(Span<byte>).GetConstructor(new[]{typeof(byte[])})!);
        ilGenerator.Emit(OpCodes.Br, labelDone);

        ilGenerator.MarkLabel(labelDone);
        return span;
    }
        
    private static bool IsStatic(MemberInfo member)
    {
        return member switch
        {
            MethodBase { IsStatic: true } => true,
            MethodBase => false,
            PropertyInfo p => IsStatic(p.GetAccessors(true)[0]),
            _ => throw new InvalidOperationException("Unknown member type")
        };
    }
}