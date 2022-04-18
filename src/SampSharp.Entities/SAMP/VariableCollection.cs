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
using System.Linq;
using SampSharp.Core.Natives.NativeObjects;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a collection of SA:MP variables.
/// </summary>
public class VariableCollection : IDictionary<string, object>
{
    private const int MaxKeyLength = 40;

    private readonly IVariableCollectionNatives _native;

    internal VariableCollection(IVariableCollectionNatives native)
    {
        _native = native ?? throw new ArgumentNullException(nameof(native));
        Keys = new KeyCollection(this);
        Values = new ValueCollection(this);
    }

    /// <inheritdoc />
    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
        var upper = _native.GetVarsUpperIndex();

        for (var i = 0; i <= upper; i++)
        {
            _native.GetVarNameAtIndex(i, out var varName, MaxKeyLength + 1);

            if (varName == null)
                continue;

            var value = Get(varName);

            if (value == null)
                continue;

            yield return new KeyValuePair<string, object>(varName, value);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
    {
        Add(item.Key, item.Value);
    }

    /// <inheritdoc />
    public void Clear()
    {
        for (var i = _native.GetVarsUpperIndex(); i >= 0; i--)
        {
            _native.GetVarNameAtIndex(i, out var varName, MaxKeyLength + 1);

            if (!string.IsNullOrEmpty(varName))
                _native.DeleteVar(varName);
        }
    }

    bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
    {
        return TryGetValue(item.Key, out var value) && value == item.Value;
    }

    void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
    {
        if (array == null) throw new ArgumentNullException(nameof(array));
        if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));

        var upper = _native.GetVarsUpperIndex();

        for (var i = 0; i <= upper; i++)
        {
            _native.GetVarNameAtIndex(i, out var varName, MaxKeyLength + 1);

            if (string.IsNullOrEmpty(varName))
                continue;

            var value = Get(varName);

            if (value == null)
                continue;

            if (array.Length <= arrayIndex)
                throw new ArgumentException(
                    "The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.",
                    nameof(array));

            array[arrayIndex++] = new KeyValuePair<string, object>(varName, value);
        }
    }

    /// <inheritdoc />
    bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
    {
        var self = (ICollection<KeyValuePair<string, object>>) this;
        if (item.Key != null && item.Value != null && self.Contains(item))
            return _native.DeleteVar(item.Key);

        return false;
    }

    /// <inheritdoc />
    public int Count => CountVars();

    /// <inheritdoc />
    public bool IsReadOnly { get; } = false;

    /// <inheritdoc />
    public void Add(string key, object value)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));
        if (value == null) throw new ArgumentNullException(nameof(value));
        if (key.Length > MaxKeyLength)
            throw new ArgumentOutOfRangeException(nameof(key), key,
                $"The variable name is longer than the limit of {MaxKeyLength}.");

        if (_native.GetVarType(key) != (int)ServerVarType.None)
            throw new ArgumentException("An element with the same key already exists in this collection.",
                nameof(key));

        Set(key, value);
    }

    /// <inheritdoc />
    public bool ContainsKey(string key)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        return key is { Length: <= MaxKeyLength } && _native.GetVarType(key) != (int)ServerVarType.None;
    }

    /// <inheritdoc />
    public bool Remove(string key)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        if (key == null || key.Length > MaxKeyLength)
            return false;

        return _native.DeleteVar(key);
    }

    /// <inheritdoc />
    public bool TryGetValue(string key, out object value)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        if (key == null || key.Length > MaxKeyLength)
        {
            value = null;
            return false;
        }

        value = Get(key);
        return value != null;
    }

    /// <inheritdoc />
    public object this[string key]
    {
        get
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (key.Length > MaxKeyLength)
                throw new ArgumentOutOfRangeException(nameof(key), key,
                    $"The variable name is longer than the limit of {MaxKeyLength}.");

            return Get(key) ?? throw new KeyNotFoundException();
        }
        set
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (key.Length > 40)
                throw new ArgumentOutOfRangeException(nameof(key), key,
                    $"The variable name is longer than the limit of {MaxKeyLength}.");
            if (value == null) throw new ArgumentNullException(nameof(value));

            Set(key, value);
        }
    }

    /// <inheritdoc />
    public ICollection<string> Keys { get; }

    /// <inheritdoc />
    public ICollection<object> Values { get; }

    /// <summary>
    /// Gets an integer value of the variable with the specified <paramref name="varName" />.
    /// </summary>
    /// <param name="varName">The name of the variable to get.</param>
    /// <returns>The value of the variable or 0 if the variable does not exist.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="varName" /> is null.</exception>
    public int GetInt(string varName)
    {
        if (varName == null) throw new ArgumentNullException(nameof(varName));
        return _native.GetVarInt(varName);
    }

    /// <summary>
    /// Gets a string value of the variable with the specified <paramref name="varName" />.
    /// </summary>
    /// <param name="varName">The name of the variable to get.</param>
    /// <param name="length">The maximum length of the string value.</param>
    /// <returns>The value of the variable or 0 if the variable does not exist.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="varName" /> is null.</exception>
    public string GetString(string varName, int length = 1024)
    {
        if (varName == null) throw new ArgumentNullException(nameof(varName));
        _native.GetVarString(varName, out var value, length);
        return value;
    }

    /// <summary>
    /// Gets a floating-point value of the variable with the specified <paramref name="varName" />.
    /// </summary>
    /// <param name="varName">The name of the variable to get.</param>
    /// <returns>The value of the variable or 0 if the variable does not exist.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="varName" /> is null.</exception>
    public float GetFloat(string varName)
    {
        if (varName == null) throw new ArgumentNullException(nameof(varName));
        return _native.GetVarFloat(varName);
    }

    /// <summary>
    /// Sets and replaces the value of the variable with the specified <paramref name="varName" /> with the specified
    /// <paramref name="value" />.
    /// </summary>
    /// <param name="varName">The variable name.</param>
    /// <param name="value">The value to store in the variable.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="varName" /> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="varName" /> is longer than the limit of 40.</exception>
    public void Set(string varName, int value)
    {
        if (varName == null) throw new ArgumentNullException(nameof(varName));
        if (varName.Length > MaxKeyLength)
            throw new ArgumentOutOfRangeException(nameof(varName), varName,
                $"The variable name is longer than the limit of {MaxKeyLength}.");
        _native.SetVarInt(varName, value);
    }

    /// <summary>
    /// Sets and replaces the value of the variable with the specified <paramref name="varName" /> with the specified
    /// <paramref name="value" />.
    /// </summary>
    /// <param name="varName">The variable name.</param>
    /// <param name="value">The value to store in the variable.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="varName" /> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="varName" /> is longer than the limit of 40.</exception>
    public void Set(string varName, string value)
    {
        if (varName == null) throw new ArgumentNullException(nameof(varName));
        if (varName.Length > MaxKeyLength)
            throw new ArgumentOutOfRangeException(nameof(varName), varName,
                $"The variable name is longer than the limit of {MaxKeyLength}.");

        if (value == null)
            _native.DeleteVar(varName);
        else
            _native.SetVarString(varName, value);
    }

    /// <summary>
    /// Sets and replaces the value of the variable with the specified <paramref name="varName" /> with the specified
    /// <paramref name="value" />.
    /// </summary>
    /// <param name="varName">The variable name.</param>
    /// <param name="value">The value to store in the variable.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="varName" /> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="varName" /> is longer than the limit of 40.</exception>
    public void Set(string varName, float value)
    {
        if (varName == null) throw new ArgumentNullException(nameof(varName));
        if (varName.Length > MaxKeyLength)
            throw new ArgumentOutOfRangeException(nameof(varName), varName,
                $"The variable name is longer than the limit of {MaxKeyLength}.");
        _native.SetVarFloat(varName, value);
    }
        
    /// <summary>
    /// Sets and replaces the value of the variable with the specified <paramref name="varName" /> with the specified
    /// <paramref name="value" />.
    /// </summary>
    /// <param name="varName">The variable name.</param>
    /// <param name="value">The value to store in the variable.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="varName" /> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="varName" /> is longer than the limit of 40.</exception>
    public void Set(string varName, object value)
    {
        if (varName == null) throw new ArgumentNullException(nameof(varName));
        if (varName.Length > MaxKeyLength)
            throw new ArgumentOutOfRangeException(nameof(varName), varName,
                $"The variable name is longer than the limit of {MaxKeyLength}.");

        if (value == null)
        {
            _native.DeleteVar(varName);
            return;
        }

        switch (value)
        {
            case int v:
                Set(varName, v);
                return;
            case float v:
                Set(varName, v);
                return;
            case string v:
                Set(varName, v);
                return;
            default:
                throw new ArgumentOutOfRangeException(nameof(value), value,
                    "Value must be of type int, float or string.");
        }
    }

    private object Get(string varName)
    {
        if (varName == null) throw new ArgumentNullException(nameof(varName));
        if (varName.Length > MaxKeyLength)
            return null;
        switch ((ServerVarType) _native.GetVarType(varName))
        {
            case ServerVarType.Int:
                return GetInt(varName);
            case ServerVarType.String:
                return GetString(varName);
            case ServerVarType.Float:
                return GetFloat(varName);
            default:
                return null;
        }
    }

    private int CountVars()
    {
        var result = 0;
        for (var i = _native.GetVarsUpperIndex(); i >= 0; i--)
        {
            _native.GetVarNameAtIndex(i, out var varName, MaxKeyLength + 1);
            if (!string.IsNullOrEmpty(varName))
                result++;
        }

        return result;
    }

    private sealed class ValueCollection : ICollection<object>
    {
        private readonly VariableCollection _collection;

        public ValueCollection(VariableCollection collection)
        {
            _collection = collection;
        }


        public IEnumerator<object> GetEnumerator()
        {
            return _collection.Select(v => v.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(object item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(object item)
        {
            return item != null && (item is int || item is string || item is float) &&
                   _collection.Any(v => v.Value == item);
        }

        public void CopyTo(object[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            var upper = _collection._native.GetVarsUpperIndex();

            for (var i = 0; i <= upper; i++)
            {
                _collection._native.GetVarNameAtIndex(i, out var varName, MaxKeyLength + 1);

                if (string.IsNullOrEmpty(varName))
                    continue;

                var value = _collection.Get(varName);

                if (value == null)
                    continue;

                if (array.Length <= arrayIndex)
                    throw new ArgumentException(
                        "The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.",
                        nameof(array));

                array[arrayIndex++] = value;
            }
        }

        public bool Remove(object item)
        {
            throw new NotSupportedException();
        }

        public int Count => _collection.Count;
        public bool IsReadOnly { get; } = true;
    }

    private sealed class KeyCollection : ICollection<string>
    {
        private readonly VariableCollection _collection;

        public KeyCollection(VariableCollection collection)
        {
            _collection = collection;
        }

        public IEnumerator<string> GetEnumerator()
        {
            var upper = _collection._native.GetVarsUpperIndex();

            for (var i = 0; i <= upper; i++)
            {
                _collection._native.GetVarNameAtIndex(i, out var varName, MaxKeyLength + 1);

                if (string.IsNullOrEmpty(varName))
                    continue;

                yield return varName;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(string item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(string item)
        {
            return item is { Length: <= MaxKeyLength } && _collection._native.GetVarType(item) != (int)ServerVarType.None;
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            var upper = _collection._native.GetVarsUpperIndex();

            for (var i = 0; i <= upper; i++)
            {
                _collection._native.GetVarNameAtIndex(i, out var varName, MaxKeyLength + 1);

                if (string.IsNullOrEmpty(varName))
                    continue;

                if (array.Length <= arrayIndex)
                    throw new ArgumentException(
                        "The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.",
                        nameof(array));

                array[arrayIndex++] = varName;
            }
        }

        public bool Remove(string item)
        {
            throw new NotSupportedException();
        }

        public int Count => _collection.Count;
        public bool IsReadOnly { get; } = true;
    }
#pragma warning disable 1591

    public interface IVariableCollectionNatives
    {
        bool DeleteVar(string varName);
        float GetVarFloat(string varName);
        int GetVarInt(string varName);
        bool GetVarNameAtIndex(int index, out string varName, int size);
        bool GetVarString(string varName, out string value, int size);
        int GetVarsUpperIndex();
        int GetVarType(string varName);
        bool SetVarFloat(string varName, float value);
        bool SetVarInt(string varName, int value);
        bool SetVarString(string varName, string value);
    }

    [NativeObjectIdentifiers("PlayerId")]
    public class PlayerVariableCollectionNatives : IVariableCollectionNatives
    {
        public int PlayerId { get; set; }

        [NativeMethod(Function = "SetPVarInt")]
        public virtual bool SetVarInt(string varName, int value)

        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(Function = "GetPVarInt")]
        public virtual int GetVarInt(string varName)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(Function = "SetPVarString")]
        public virtual bool SetVarString(string varName, string value)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(Function = "GetPVarString")]
        public virtual bool GetVarString(string varName, out string value, int size)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(Function = "SetPVarFloat")]
        public virtual bool SetVarFloat(string varName, float value)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(Function = "GetPVarFloat")]
        public virtual float GetVarFloat(string varName)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(Function = "DeletePVar")]
        public virtual bool DeleteVar(string varName)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(Function = "GetPVarsUpperIndex")]
        public virtual int GetVarsUpperIndex()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(Function = "GetPVarNameAtIndex")]
        public virtual bool GetVarNameAtIndex(int index, out string varName, int size)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(Function = "GetPVarType")]
        public virtual int GetVarType(string varName)
        {
            throw new NativeNotImplementedException();
        }
    }

    public class ServerVariableCollectionNatives : IVariableCollectionNatives
    {
        [NativeMethod(Function = "SetSVarInt")]
        public virtual bool SetVarInt(string varName, int value)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(Function = "GetSVarInt")]
        public virtual int GetVarInt(string varName)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(Function = "SetSVarString")]
        public virtual bool SetVarString(string varName, string value)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(Function = "GetSVarString")]
        public virtual bool GetVarString(string varName, out string value, int size)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(Function = "SetSVarFloat")]
        public virtual bool SetVarFloat(string varName, float value)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(Function = "GetSVarFloat")]
        public virtual float GetVarFloat(string varName)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(Function = "DeleteSVar")]
        public virtual bool DeleteVar(string varName)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(Function = "GetSVarsUpperIndex")]
        public virtual int GetVarsUpperIndex()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(Function = "GetSVarNameAtIndex")]
        public virtual bool GetVarNameAtIndex(int index, out string varName, int size)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod(Function = "GetSVarType")]
        public virtual int GetVarType(string varName)
        {
            throw new NativeNotImplementedException();
        }
    }
}