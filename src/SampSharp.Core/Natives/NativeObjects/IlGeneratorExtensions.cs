using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using SampSharp.Core.Communication;

namespace SampSharp.Core.Natives.NativeObjects
{
    internal static class IlGeneratorExtensions
    {
        public static void EmitCall(this ILGenerator ilGenerator, OpCode opCode, MethodInfo methodInfo)
        {
            ilGenerator.EmitCall(opCode, methodInfo, null);
        }

        public static void EmitCall(this ILGenerator ilGenerator, MethodInfo methodInfo)
        {
            ilGenerator.EmitCall(methodInfo.IsFinal || !methodInfo.IsVirtual ? OpCodes.Call : OpCodes.Callvirt,
                methodInfo);
        }

        public static void EmitCall(this ILGenerator ilGenerator, OpCode opCode, Type type, string methodName)
        {
            ilGenerator.EmitCall(opCode, type.GetMethod(methodName));
        }

        public static void EmitCall(this ILGenerator ilGenerator, OpCode opCode, Type type, string methodName,
            params Type[] types)
        {
            ilGenerator.EmitCall(opCode, type.GetMethod(methodName, types));
        }

        public static void EmitCall(this ILGenerator ilGenerator, Type type, string methodName, params Type[] types)
        {
            ilGenerator.EmitCall(type.GetMethod(methodName, types));
        }

        public static void EmitCall(this ILGenerator ilGenerator, Type type, string methodName)
        {
            ilGenerator.EmitCall(type.GetMethod(methodName));
        }

        public static void EmitPropertyGetterCall<T>(this ILGenerator ilGenerator, OpCode opCode,
            Expression<Func<T, object>> property)
        {
            ilGenerator.EmitCall(opCode,
                ((PropertyInfo) ((MemberExpression) ((UnaryExpression) property.Body).Operand).Member).GetMethod);
        }

        public static void EmitConvert<TFrom, TTo>(this ILGenerator ilGenerator)
        {
            string methodName = null;
            if (typeof(TTo) == typeof(int))
                methodName = nameof(ValueConverter.ToInt32);
            else if (typeof(TFrom) == typeof(int))
            {
                if (typeof(TTo) == typeof(float))
                    methodName = nameof(ValueConverter.ToSingle);
                else if (typeof(TTo) == typeof(bool))
                    methodName = nameof(ValueConverter.ToBoolean);
            }

            if (methodName == null)
                throw new Exception("Unsupported types");

            ilGenerator.EmitCall(typeof(ValueConverter), methodName, typeof(TFrom));
        }

        public static void Emit(this ILGenerator ilGenerator, OpCode opCode, ParameterInfo paramInfo)
        {
            ilGenerator.Emit(opCode, (IsStatic(paramInfo.Member) ? 0 : 1) + paramInfo.Position);
        }

        private static bool IsStatic(MemberInfo member)
        {
            return member switch
            {
                MethodBase m when m.IsStatic => true,
                MethodBase _ => false,
                PropertyInfo p => IsStatic(p.GetAccessors(true)[0]),
                _ => throw new Exception("Unknown member type")
            };
        }
    }
}