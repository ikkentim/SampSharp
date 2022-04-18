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
using System.IO;
using System.Linq;

namespace SampSharp.GameMode.SAMP;

/// <summary>
///     Represents the server configuration file
/// </summary>
public class ServerConfig : IEnumerable<KeyValuePair<string, string>>
{
    private readonly Dictionary<string, string> _values = new();

    /// <summary>
    ///     Initializes a new instance of the <see cref="ServerConfig" /> class.
    /// </summary>
    /// <param name="configPath">The path to the configuration file.</param>
    public ServerConfig(string configPath)
    {
        if (!File.Exists(configPath))
            return;

        foreach (
            var parts in
            File.ReadAllLines(configPath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Split(new[] {' '}, 2)))
        {
            switch (parts.Length)
            {
                case 1:
                    _values[parts[0].Trim()] = string.Empty;
                    break;
                case 2:
                    _values[parts[0].Trim()] = parts[1];
                    break;
            }
        }
    }

    /// <summary>
    ///     Gets the <see cref="System.String" /> with the specified key.
    /// </summary>
    public string this[string key] => Get(key);

    /// <summary>
    ///     Gets the configuration value with the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="trimSpaces">If set to <c>true</c> trim white-space characters.</param>
    /// <returns>
    ///     The value.
    /// </returns>
    public string Get(string key, bool trimSpaces = true)
    {
        if (!_values.ContainsKey(key))
        {
            return null;
        }

        return trimSpaces ? _values[key].Trim() : _values[key];
    }

    /// <summary>
    ///     Gets the configuration value with the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="trimSpaces">If set to <c>true</c> trim white-space characters.</param>
    /// <returns>
    ///     The value.
    /// </returns>
    public string Get(string key, string defaultValue, bool trimSpaces = true)
    {
        return Get(key, trimSpaces) ?? (trimSpaces ? defaultValue.Trim() : defaultValue);
    }

    /// <summary>
    ///     Sets the configuration value with the specified key to the specified value for the current session.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="ArgumentNullException">key</exception>
    public void Set(string key, string value)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));
        _values[key] = value;
    }

    /// <summary>Returns an enumerator that iterates through the collection.</summary>
    /// <returns>An enumerator that can be used to iterate through the collection.</returns>
    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        return _values.GetEnumerator();
    }

    /// <summary>Returns an enumerator that iterates through a collection.</summary>
    /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}