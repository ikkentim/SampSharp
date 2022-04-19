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
using System.Collections;
using System.Collections.Generic;
using SampSharp.GameMode.Definitions;

namespace SampSharp.GameMode.SAMP;

/// <summary>Represents a collection of server variables.</summary>
public partial class SVarCollection : IEnumerable<object>
{
    /// <summary>Gets or sets the <see cref="System.Object" /> at the specified index.</summary>
    public object this[int index]
    {
        get => this[NameAtIndex(index)];
        set => this[NameAtIndex(index)] = value;
    }

    /// <summary>Gets or sets the <see cref="object" /> with the specified <paramref name="varname" />.</summary>
    public object this[string varname]
    {
        get
        {
            if (varname == null) return null;

            return (ServerVarType)SVarCollectionInternal.Instance.GetSVarType(varname) switch
            {
                ServerVarType.Int => Get<int>(varname),
                ServerVarType.Float => Get<float>(varname),
                ServerVarType.String => Get<string>(varname),
                _ => null
            };
        }
        set
        {
            if (varname == null) return;

            switch (value)
            {
                case null:
                    Delete(varname);
                    return;
                case int i:
                    SVarCollectionInternal.Instance.SetSVarInt(varname, i);
                    break;
                case float f:
                    SVarCollectionInternal.Instance.SetSVarFloat(varname, f);
                    break;
                case bool b:
                    SVarCollectionInternal.Instance.SetSVarInt(varname, b
                        ? 1
                        : 0);
                    break;
                case string s:
                    SVarCollectionInternal.Instance.SetSVarString(varname, s);
                    break;
            }
        }
    }

    /// <summary>Gets the upper index of the variables list.</summary>
    public int UpperIndex => SVarCollectionInternal.Instance.GetSVarsUpperIndex();

    /// <summary>Returns an enumerator that iterates through the collection.</summary>
    /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
    public IEnumerator<object> GetEnumerator()
    {
        var vars = new List<object>();
        for (var i = 0; i <= UpperIndex; i++)
        {
            var v = this[i];
            if (v != null) vars.Add(v);
        }

        return vars.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>Gets the variable with the specified <paramref name="varname" />.</summary>
    /// <typeparam name="T">The type of the variable.</typeparam>
    /// <param name="varname">The name of the variable.</param>
    /// <returns>The variable with the specified name.</returns>
    public T Get<T>(string varname)
    {
        object value = default(T);
        if (typeof(T) == typeof(int))
            value = SVarCollectionInternal.Instance.GetSVarInt(varname);
        else if (typeof(T) == typeof(float))
            value = SVarCollectionInternal.Instance.GetSVarFloat(varname);
        else if (typeof(T) == typeof(string))
        {
            SVarCollectionInternal.Instance.GetSVarString(varname, out var output, 64);
            value = output;
        }
        else if (typeof(T) == typeof(bool))
            value = SVarCollectionInternal.Instance.GetSVarInt(varname) > 0;

        return (T)Convert.ChangeType(value, typeof(T));
    }

    /// <summary>Checks whether a variable with the specified name exists.</summary>
    /// <param name="varname">The variable name.</param>
    /// <returns>True if the variable exists; False otherwise.</returns>
    public bool Exists(string varname)
    {
        return SVarCollectionInternal.Instance.GetSVarType(varname) != (int)ServerVarType.None;
    }

    /// <summary>Gets the type of the variable with the given <paramref name="varname" />.</summary>
    /// <param name="varname">The name of the variable.</param>
    /// <returns>The type of the variable.</returns>
    public Type GetType(string varname)
    {
        switch ((ServerVarType)SVarCollectionInternal.Instance.GetSVarType(varname))
        {
            case ServerVarType.Float:
                return typeof(float);
            case ServerVarType.Int:
                return typeof(int);
            case ServerVarType.String:
                return typeof(string);
            default:
                return null;
        }
    }

    /// <summary>Gets the name at the given <paramref name="index" />.</summary>
    /// <param name="index">The index.</param>
    /// <returns>The name at the given index.</returns>
    public string NameAtIndex(int index)
    {
        SVarCollectionInternal.Instance.GetSVarNameAtIndex(index, out var name, 64);
        return name;
    }

    /// <summary>Deletes the specified variable.</summary>
    /// <param name="varname">The name of the variable.</param>
    /// <returns>True on success; False otherwise.</returns>
    public bool Delete(string varname)
    {
        return SVarCollectionInternal.Instance.DeleteSVar(varname);
    }
}