using System;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using SampSharp.GameMode.Tools;

namespace SampSharp.GameMode.API
{
    public class DefaultNative : INative
    {
        private readonly Type[] _parameterTypes;

        internal DefaultNative(string name, int handle, Type[] parameterTypes)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            
            Name = name;
            Handle = handle;
            _parameterTypes = parameterTypes;
        }

        #region Implementation of INative

        /// <summary>
        ///     Gets the handle of this native.
        /// </summary>
        public int Handle { get; }

        /// <summary>
        ///     Gets the name of the native function.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Gets the parameter types.
        /// </summary>
        public Type[] ParameterTypes => _parameterTypes;

        /// <summary>
        ///     Invokes the native with the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The return value of the native.</returns>
        public int Invoke(params object[] arguments)
        {
            if (Sync.IsRequired)
            {
                FrameworkLog.WriteLine(FrameworkMessageLevel.Debug,
                    $"Call to native handle {this} is being synchronized.");
                return Sync.RunSync(() => CastArgsAndInvoke(arguments));
            }

            return CastArgsAndInvoke(arguments);
        }

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a float.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The return value of the native as a float.</returns>
        public float InvokeFloat(params object[] arguments)
        {
            return ConvertIntToFloat(Invoke(arguments));
        }

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a bool.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The return value of the native as a bool.</returns>
        public bool InvokeBool(params object[] arguments)
        {
            return ConvertIntToBool(Invoke(arguments));
        }
        
        #endregion

        #region Overrides of Object

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return $"{Name}#0x{Handle.ToString("X")}";
        }

        #endregion

        private int CastArgsAndInvoke(object[] args)
        {
            var baseArgs = new object[args.Length];
            var parameterCount = (_parameterTypes?.Length ?? 0);

            for (var i = 0; i < parameterCount; i++)
            {
                var type = _parameterTypes[i].IsByRef ? _parameterTypes[i].GetElementType() : _parameterTypes[i];

                if (type == typeof(float))
                    baseArgs[i] = Cast<float, int>(ConvertFloatToInt, args[i]);
                else if (type == typeof(bool))
                    baseArgs[i] = Cast<bool, int>(ConvertBoolToInt, args[i]);
                else if (type == typeof(float[]))
                    baseArgs[i] = Cast<float[], int[]>(ConvertFloatArrayToIntArray, args[i]);
                else if (type == typeof(bool[]))
                    baseArgs[i] = Cast<bool[], int[]>(ConvertBoolArrayToIntArray, args[i]);
                else
                    baseArgs[i] = args[i];
            }
            
            var result = Interop.InvokeNative(Handle, baseArgs);

            for (var i = 0; i < parameterCount; i++)
            {
                if (!_parameterTypes[i].IsByRef)
                    continue;

                var type = _parameterTypes[i].GetElementType();

                if (type == typeof(float))
                    args[i] = Cast<int, float>(ConvertIntToFloat, baseArgs[i]);
                else if (type == typeof(bool))
                    args[i] = Cast<int, bool>(ConvertIntToBool, baseArgs[i]);
                else if (type == typeof(float[]))
                    args[i] = Cast<int[], float[]>(ConvertIntArrayToFloatArray, baseArgs[i]);
                else if (type == typeof(bool[]))
                    args[i] = Cast<int[], bool[]>(ConvertIntArrayToBoolArray, baseArgs[i]);
                else
                    args[i] = baseArgs[i];
            }

            return result;
        }

        #region Argument Casting Methods

        private static object Cast<T1, T2>(Func<T1, T2> func, object input)
        {
            return input is T1 ? (object)func((T1)input) : null;
        }

        private static float ConvertIntToFloat(int value)
        {
            return new ValueUnion { Integer = value }.Float;
        }

        private static int ConvertFloatToInt(float value)
        {
            return new ValueUnion { Float = value }.Integer;
        }

        private static float[] ConvertIntArrayToFloatArray(int[] value)
        {
            return value.Select(ConvertIntToFloat).ToArray();
        }

        private static int[] ConvertFloatArrayToIntArray(float[] value)
        {
            return value.Select(ConvertFloatToInt).ToArray();
        }

        private static int ConvertBoolToInt(bool value)
        {
            return value ? 1 : 0;
        }

        private static bool ConvertIntToBool(int value)
        {
            return value != 0;
        }

        private static bool[] ConvertIntArrayToBoolArray(int[] value)
        {
            return value.Select(ConvertIntToBool).ToArray();
        }

        private static int[] ConvertBoolArrayToIntArray(bool[] value)
        {
            return value.Select(ConvertBoolToInt).ToArray();
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct ValueUnion
        {
            [FieldOffset(0)]
            public int Integer;
            [FieldOffset(0)]
            public float Float;
        }

        #endregion
    }
}