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
using System.Reflection;

namespace SampSharp.Core.Natives;

/// <summary>
/// Provides information about a native parameter which can be consumed by a proxy factory IL generator.
/// </summary>
internal class NativeIlGenParam
{
    private ParameterInfo _parameter;
    private PropertyInfo _property;

    /// <summary>
    /// Gets or sets the index of this native parameter.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Gets or sets the method parameter of this native parameter.
    /// </summary>
    public ParameterInfo Parameter
    {
        get => _parameter;
        set
        {
            if (value != null)
            {
                _property = null;

                if (value.ParameterType.IsArray && value.GetCustomAttribute<ParamArrayAttribute>() != null)
                {
                    Type = NativeParameterType.VarArgs;
                }
                else
                {
                    Type = GetParameterType(value.ParameterType);
                }
            }
            _parameter = value;
        }
    }

    /// <summary>
    /// Gets or sets the index property of this native parameter.
    /// </summary>
    public PropertyInfo Property
    {
        get => _property;
        set
        {
            if (value != null)
            {
                _parameter = null;
                Type = GetParameterType(value.PropertyType);
            }
            _property = value;
        }
    }

    /// <summary>
    /// Gets or sets the length parameter of this native parameter.
    /// </summary>
    public NativeIlGenParam LengthParam { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this native parameter is a length parameter.
    /// </summary>
    public bool IsLengthParam { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this native parameter is an input value by reference.
    /// </summary>
    public bool IsReferenceInput { get; set; }

    /// <summary>
    /// Gets the name of this native parameter.
    /// </summary>
    public string Name => Parameter?.Name ?? Property?.Name;
    /// <summary>
    /// Gets the type of the method parameter or index property of this native parameter.
    /// </summary>
    public Type InputType => Parameter?.ParameterType ?? Property.PropertyType;

    /// <summary>
    /// Gets the type of this native parameter.
    /// </summary>
    public NativeParameterType Type { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this native parameter requires a length.
    /// </summary>
    public bool RequiresLength => Type.HasFlag(NativeParameterType.Array) || Type == NativeParameterType.StringReference;

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Name}[{Index}:{Type}{(LengthParam == null ? string.Empty : $", len={LengthParam.Name}")}]";
    }
        
    private static NativeParameterType GetParameterType(Type type)
    {
        var isByRef = type.IsByRef;
        var elementType = isByRef ? type.GetElementType()! : type;
        var isArray = elementType.IsArray;
        elementType = isArray ? elementType.GetElementType() : elementType;

        NativeParameterType parameterType;
        if (elementType == typeof(int)) parameterType = NativeParameterType.Int32;
        else if (elementType == typeof(float)) parameterType = NativeParameterType.Single;
        else if (elementType == typeof(bool)) parameterType = NativeParameterType.Bool;
        else if (elementType == typeof(string)) parameterType = NativeParameterType.String;
        else throw new ArgumentOutOfRangeException(nameof(type));

        if (isArray) parameterType |= NativeParameterType.Array;
        if (isByRef) parameterType |= NativeParameterType.Reference;

        return parameterType;
    }
}