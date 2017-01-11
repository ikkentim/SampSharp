// SampSharp
// Copyright 2017 Tim Potze
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
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP
{
    /// <summary>
    ///     Represents a collection of player variables.
    /// </summary>
    public partial class PVarCollection : IEnumerable<object>
    {
        private readonly BasePlayer _player;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PVarCollection" /> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public PVarCollection(BasePlayer player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            _player = player;
        }

        /// <summary>
        ///     Gets or sets the <see cref="System.Object" /> at the specified index.
        /// </summary>
        public object this[int index]
        {
            get { return this[NameAtIndex(index)]; }
            set { this[NameAtIndex(index)] = value; }
        }

        /// <summary>
        ///     Gets or sets the <see cref="System.Object" /> with the specified varname.
        /// </summary>
        public object this[string varname]
        {
            get
            {
                if (varname == null || _player == null) return null;

                switch ((ServerVarType) PVarCollectionInternal.Instance.GetPVarType(_player.Id, varname))
                {
                    case ServerVarType.Int:
                        return Get<int>(varname);
                    case ServerVarType.Float:
                        return Get<float>(varname);
                    case ServerVarType.String:
                        return Get<string>(varname);
                    default:
                        return null;
                }
            }
            set
            {
                if (varname == null || _player == null) return;

                if (value == null)
                {
                    Delete(varname);
                    return;
                }

                if (value is int)
                    PVarCollectionInternal.Instance.SetPVarInt(_player.Id, varname, (int) value);
                else if (value is float)
                    PVarCollectionInternal.Instance.SetPVarFloat(_player.Id, varname, (float) value);
                else if (value is bool)
                    PVarCollectionInternal.Instance.SetPVarInt(_player.Id, varname, (bool) value ? 1 : 0);
                else
                {
                    var s = value as string;
                    if (s != null)
                        PVarCollectionInternal.Instance.SetPVarString(_player.Id, varname, s);
                }
            }
        }

        /// <summary>
        ///     Gets the upper index of the variables list.
        /// </summary>
        public int UpperIndex => _player == null ? 0 : PVarCollectionInternal.Instance.GetPVarsUpperIndex(_player.Id);

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
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

        /// <summary>
        ///     Gets the variable with the specified varname.
        /// </summary>
        /// <typeparam name="T">The type of the variable.</typeparam>
        /// <param name="varname">The varname.</param>
        /// <returns>The variable with the specified varname.</returns>
        public T Get<T>(string varname)
        {
            if (_player == null) return default(T);

            object value = default(T);
            if (typeof (T) == typeof (int))
                value = PVarCollectionInternal.Instance.GetPVarInt(_player.Id, varname);
            else if (typeof (T) == typeof (float))
                value = PVarCollectionInternal.Instance.GetPVarFloat(_player.Id, varname);
            else if (typeof (T) == typeof (string))
            {
                string output;
                PVarCollectionInternal.Instance.GetPVarString(_player.Id, varname, out output, 64);
                value = output;
            }
            else if (typeof (T) == typeof (bool))
                value = PVarCollectionInternal.Instance.GetPVarInt(_player.Id, varname) > 0;
            return (T) Convert.ChangeType(value, typeof (T));
        }

        /// <summary>
        ///     Checks whether a variable with the specified varname exists.
        /// </summary>
        /// <param name="varname">The varname.</param>
        /// <returns>True if the variable exists; False otherwise.</returns>
        public bool Exists(string varname)
        {
            return _player != null && PVarCollectionInternal.Instance.GetPVarType(_player.Id, varname) != (int) ServerVarType.None;
        }

        /// <summary>
        ///     Gets the type of the variable with the given <paramref name="varname" />.
        /// </summary>
        /// <param name="varname">The varname.</param>
        /// <returns>The type of the variable.</returns>
        public Type GetType(string varname)
        {
            if (_player == null) return null;

            switch ((ServerVarType) PVarCollectionInternal.Instance.GetPVarType(_player.Id, varname))
            {
                case ServerVarType.Float:
                    return typeof (float);
                case ServerVarType.Int:
                    return typeof (int);
                case ServerVarType.String:
                    return typeof (string);
                default:
                    return null;
            }
        }

        /// <summary>
        ///     Gets the name at the given <paramref name="index" />.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The name at the given index.</returns>
        public string NameAtIndex(int index)
        {
            if (_player == null) return null;

            string name;
            PVarCollectionInternal.Instance.GetPVarNameAtIndex(_player.Id, index, out name, 64);
            return name;
        }

        /// <summary>
        ///     Deletes the specified varname.
        /// </summary>
        /// <param name="varname">The varname.</param>
        /// <returns>True on success; False otherwise.</returns>
        public bool Delete(string varname)
        {
            return _player != null && PVarCollectionInternal.Instance.DeletePVar(_player.Id, varname);
        }
    }
}