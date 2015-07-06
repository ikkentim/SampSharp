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

using System;
using System.Collections;
using System.Collections.Generic;
using SampSharp.GameMode.API;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP
{
    /// <summary>
    ///     Represents a collection of player variables.
    /// </summary>
    public class PVarCollection : IEnumerable<object>
    {
        private readonly GtaPlayer _player;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PVarCollection" /> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public PVarCollection(GtaPlayer player)
        {
            if (player == null)
                throw new ArgumentNullException("player");

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

                switch ((PlayerVarType) GetPVarType(_player.Id, varname))
                {
                    case PlayerVarType.Int:
                        return Get<int>(varname);
                    case PlayerVarType.Float:
                        return Get<float>(varname);
                    case PlayerVarType.String:
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
                    SetPVarInt(_player.Id, varname, (int) value);
                else if (value is float)
                    SetPVarFloat(_player.Id, varname, (float) value);
                else if (value is bool)
                    SetPVarInt(_player.Id, varname, (bool) value ? 1 : 0);
                else
                {
                    var s = value as string;
                    if (s != null)
                        SetPVarString(_player.Id, varname, s);
                }
            }
        }

        /// <summary>
        ///     Gets the upper index of the variables list.
        /// </summary>
        public int UpperIndex
        {
            get { return _player == null ? 0 : GetPVarsUpperIndex(_player.Id); }
        }

        #region Natives

        private delegate bool SetPVarIntImpl(int playerid, string varname, int value);

        private delegate bool SetPVarStringImpl(int playerid, string varname, string value);

        private delegate bool DeletePVarImpl(int playerid, string varname);

        private delegate float GetPVarFloatImpl(int playerid, string varname);

        private delegate int GetPVarIntImpl(int playerid, string varname);

        private delegate bool GetPVarNameAtIndexImpl(int playerid, int index, out string varname, int size);

        private delegate bool GetPVarStringImpl(int playerid, string varname, out string value, int size);

        private delegate int GetPVarsUpperIndexImpl(int playerid);

        private delegate int GetPVarTypeImpl(int playerid, string varname);

        private delegate bool SetPVarFloatImpl(int playerid, string varname, float value);

        [Native("SetPVarInt")]
        private static readonly SetPVarIntImpl SetPVarInt = null;
        [Native("GetPVarInt")]
        private static readonly GetPVarIntImpl GetPVarInt = null;
        [Native("SetPVarString")]
        private static readonly SetPVarStringImpl SetPVarString = null;
        [Native("GetPVarString")]
        private static readonly GetPVarStringImpl GetPVarString = null;
        [Native("SetPVarFloat")]
        private static readonly SetPVarFloatImpl SetPVarFloat = null;
        [Native("GetPVarFloat")]
        private static readonly GetPVarFloatImpl GetPVarFloat = null;
        [Native("DeletePVar")]
        private static readonly DeletePVarImpl DeletePVar = null;
        [Native("GetPVarsUpperIndex")]
        private static readonly GetPVarsUpperIndexImpl GetPVarsUpperIndex = null;
        [Native("GetPVarNameAtIndex")]
        private static readonly GetPVarNameAtIndexImpl GetPVarNameAtIndex = null;
        [Native("GetPVarType")]
        private static readonly GetPVarTypeImpl GetPVarType = null;

        #endregion

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<object> GetEnumerator()
        {
            var vars = new List<object>();
            for (int i = 0; i <= UpperIndex; i++)
            {
                object v = this[i];
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
                value = GetPVarInt(_player.Id, varname);
            else if (typeof (T) == typeof (float))
                value = GetPVarFloat(_player.Id, varname);
            else if (typeof (T) == typeof (string))
            {
                string output;
                GetPVarString(_player.Id, varname, out output, 64);
                value = output;
            }
            else if (typeof (T) == typeof (bool))
                value = GetPVarInt(_player.Id, varname) > 0;
            return (T) Convert.ChangeType(value, typeof (T));
        }

        /// <summary>
        ///     Checks whether a variable with the specified varname exists.
        /// </summary>
        /// <param name="varname">The varname.</param>
        /// <returns>True if the variable exists; False otherwise.</returns>
        public bool Exists(string varname)
        {
            return _player != null && GetPVarType(_player.Id, varname) != (int) PlayerVarType.None;
        }

        /// <summary>
        ///     Gets the type of the variable with the given <paramref name="varname" />.
        /// </summary>
        /// <param name="varname">The varname.</param>
        /// <returns>The type of the variable.</returns>
        public Type GetType(string varname)
        {
            if (_player == null) return null;

            switch ((PlayerVarType) GetPVarType(_player.Id, varname))
            {
                case PlayerVarType.Float:
                    return typeof (float);
                case PlayerVarType.Int:
                    return typeof (int);
                case PlayerVarType.String:
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
            GetPVarNameAtIndex(_player.Id, index, out name, 64);
            return name;
        }

        /// <summary>
        ///     Deletes the specified varname.
        /// </summary>
        /// <param name="varname">The varname.</param>
        /// <returns>True on success; False otherwise.</returns>
        public bool Delete(string varname)
        {
            return _player != null && DeletePVar(_player.Id, varname);
        }
    }
}