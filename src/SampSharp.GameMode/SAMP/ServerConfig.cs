// SampSharp
// Copyright 2015 Tim Potze
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

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SampSharp.GameMode.SAMP
{
    /// <summary>
    ///     Represents the server configuration file
    /// </summary>
    public class ServerConfig
    {
        private readonly Dictionary<string, string> _values = new Dictionary<string, string>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="ServerConfig" /> class.
        /// </summary>
        public ServerConfig()
        {
            foreach (
                var parts in
                    File.ReadAllLines("server.cfg")
                        .Where(line => !string.IsNullOrWhiteSpace(line))
                        .Select(line => line.Split(new[] {' '}, 2)))
            {
                switch (parts.Length)
                {
                    case 1:
                        _values[parts[0]] = string.Empty;
                        break;
                    case 2:
                        _values[parts[0]] = parts[1];
                        break;
                }
            }
        }

        /// <summary>
        ///     Gets the <see cref="System.String" /> with the specified key.
        /// </summary>
        public string this[string key]
        {
            get { return Read(key); }
        }

        /// <summary>
        ///     Reads the configuration value with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        public string Read(string key)
        {
            return _values.ContainsKey(key) ? _values[key] : null;
        }

        /// <summary>
        ///     Reads the configuration value with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        ///     The value.
        /// </returns>
        public string Read(string key, string defaultValue)
        {
            return Read(key) ?? defaultValue;
        }
    }
}