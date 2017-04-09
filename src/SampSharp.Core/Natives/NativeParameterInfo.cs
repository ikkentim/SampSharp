using System;
using System.Collections.Generic;
using SampSharp.Core.Communication;

namespace SampSharp.Core.Natives
{
    public struct NativeParameterInfo
    {
        private const NativeParameterType ArgumentMask = NativeParameterType.Int32 |
                                                         NativeParameterType.Single |
                                                         NativeParameterType.Bool |
                                                         NativeParameterType.String;

        public NativeParameterInfo(NativeParameterType type, uint lengthIndex)
        {
            Type = type;
            LengthIndex = lengthIndex;
        }

        public NativeParameterType Type { get; }

        public ServerCommandArgument ArgumentType
        {
            get
            {
                var value = ServerCommandArgument.Terminator;

                switch (Type & ArgumentMask)
                {
                    case NativeParameterType.Int32:
                    case NativeParameterType.Single:
                    case NativeParameterType.Bool:
                        value = ServerCommandArgument.Value;
                        break;
                    case NativeParameterType.String:
                        value = ServerCommandArgument.String;
                        break;
                }

                if (Type.HasFlag(NativeParameterType.Array))
                {
                    value |= ServerCommandArgument.Array;
                }

                if (Type.HasFlag(NativeParameterType.Reference))
                {
                    value |= ServerCommandArgument.Reference;
                }

                return value;
            }
        }

        public bool RequiresLength => IsArray || IsReference;

        private bool IsArray => Type.HasFlag(NativeParameterType.Array);
        
        private bool IsReference => Type.HasFlag(NativeParameterType.Reference);

        public uint LengthIndex { get; }

        public object GetReferenceArgument(byte[] response, ref int index)
        {
            object result = null;
            switch (Type)
            {
                case NativeParameterType.Int32Reference:
                    result = ValueConverter.ToInt32(response, index);
                    index += 4;
                    break;
                case NativeParameterType.SingleReference:
                    result = ValueConverter.ToSingle(response, index);
                    index += 4;
                    break;
                case NativeParameterType.BoolReference:
                    result = ValueConverter.ToBoolean(response, index);
                    index += 4;
                    break;
                case NativeParameterType.StringReference:
                    var str = ValueConverter.ToString(response, index);
                    result = str;
                    index += str.Length + 1;
                    break;
                case NativeParameterType.Int32ArrayReference:
                {
                    var len = ValueConverter.ToInt32(response, index);
                    index += 4;
                    var arr = new int[len];
                    for (var i = 0; i < len; i++)
                    {
                        arr[i] = ValueConverter.ToInt32(response, index);
                        index += 4;
                    }
                    break;
                }
                case NativeParameterType.SingleArrayReference:
                {
                    var len = ValueConverter.ToInt32(response, index);
                    index += 4;
                    var arr = new float[len];
                    for (var i = 0; i < len; i++)
                    {
                        arr[i] = ValueConverter.ToSingle(response, index);
                        index += 4;
                    }
                    break;
                }
                case NativeParameterType.BoolArrayReference:
                {
                    var len = ValueConverter.ToInt32(response, index);
                    index += 4;
                    var arr = new bool[len];
                    for (var i = 0; i < len; i++)
                    {
                        arr[i] = ValueConverter.ToBoolean(response, index);
                        index += 4;
                    }
                    break;
                }
            }

            return result;
        }

        public IEnumerable<byte> GetBytes(object value, int length)
        {
            switch (Type)
            {
                case NativeParameterType.Int32:
                case NativeParameterType.Int32Reference:
                    if (value is int v)
                        return ValueConverter.GetBytes(v);
                    break;
                case NativeParameterType.Single:
                case NativeParameterType.SingleReference:
                    if (value is float f)
                        return ValueConverter.GetBytes(f);
                    break;
                case NativeParameterType.Bool:
                case NativeParameterType.BoolReference:
                    if (value is bool b)
                        return ValueConverter.GetBytes(b);
                    break;
                case NativeParameterType.String:
                    if (value is string s)
                        return ValueConverter.GetBytes(s);
                    else if (value == null)
                        return ValueConverter.GetBytes("");
                    break;
                case NativeParameterType.StringReference:
                case NativeParameterType.Int32ArrayReference:
                case NativeParameterType.SingleArrayReference:
                case NativeParameterType.BoolArrayReference:
                    if (length < 1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(length));
                    }
                    return ValueConverter.GetBytes(length);
                case NativeParameterType.Int32Array:
                    if (value is int[] ai)
                    {
                        if (length < 1)
                        {
                            throw new ArgumentOutOfRangeException(nameof(length));
                        }

                        var array = new byte[length * 4 + 4];
                        ValueConverter.GetBytes(length).CopyTo(array, 0);
                        for (var i = 0; i < length; i++)
                        {
                            ValueConverter.GetBytes(ai[i]).CopyTo(array, 4 + i * 4);
                        }

                        return array;
                    }
                    break;
                case NativeParameterType.SingleArray:
                    if (value is float[] af)
                    {
                        if (length < 1)
                        {
                            throw new ArgumentOutOfRangeException(nameof(length));
                        }

                        var array = new byte[length * 4 + 4];
                        ValueConverter.GetBytes(length).CopyTo(array, 0);
                        for (var i = 0; i < length; i++)
                        {
                            ValueConverter.GetBytes(af[i]).CopyTo(array, 4 + i * 4);
                        }

                        return array;
                    }
                    break;
                case NativeParameterType.BoolArray:
                    if (value is bool[] ab)
                    {
                        if (length < 1)
                        {
                            throw new ArgumentOutOfRangeException(nameof(length));
                        }

                        var array = new byte[length * 4 + 4];
                        ValueConverter.GetBytes(length).CopyTo(array, 0);
                        for (var i = 0; i < length; i++)
                        {
                            ValueConverter.GetBytes(ab[i]).CopyTo(array, 4 + i * 4);
                        }

                        return array;
                    }
                    break;
            }

            throw new ArgumentException("Value is of invalid type", nameof(value));
        }
    }
}