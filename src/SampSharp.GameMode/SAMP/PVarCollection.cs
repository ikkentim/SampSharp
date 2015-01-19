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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP
{
    public class PVarCollection : IEnumerable<object>
    {
        private readonly GtaPlayer _player;

        public PVarCollection(GtaPlayer player)
        {
            if (player == null)
                throw new ArgumentNullException("player");

            _player = player;
        }

        public object this[int index]
        {
            get { return this[NameAtIndex(index)]; }
            set { this[NameAtIndex(index)] = value; }
        }

        public object this[string varname]
        {
            get
            {
                if (varname == null || _player == null) return null;

                switch ((PlayerVarType) Native.GetPVarType(_player.Id, varname))
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
                    Native.SetPVarInt(_player.Id, varname, (int) value);
                else if (value is float)
                    Native.SetPVarFloat(_player.Id, varname, (float) value);
                else if (value is bool)
                    Native.SetPVarInt(_player.Id, varname, (bool) value ? 1 : 0);
                else
                {
                    var s = value as string;
                    if (s != null)
                        Native.SetPVarString(_player.Id, varname, s);
                }
            }
        }

        public int UpperIndex
        {
            get { return _player == null ? 0 : Native.GetPVarsUpperIndex(_player.Id); }
        }

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

        public T Get<T>(string varname)
        {
            if (_player == null) return default(T);

            object value = default(T);
            if (typeof (T) == typeof (int))
                value = Native.GetPVarInt(_player.Id, varname);
            else if (typeof (T) == typeof (float))
                value = Native.GetPVarFloat(_player.Id, varname);
            else if (typeof (T) == typeof (string))
                value = Native.GetPVarString(_player.Id, varname);
            else if (typeof (T) == typeof (bool))
                value = Native.GetPVarInt(_player.Id, varname) > 0;
            return (T) Convert.ChangeType(value, typeof (T));
        }

        public bool Exists(string varname)
        {
            return _player != null && Native.GetPVarType(_player.Id, varname) != (int) PlayerVarType.None;
        }

        public Type GetType(string varname)
        {
            if (_player == null) return null;

            switch ((PlayerVarType) Native.GetPVarType(_player.Id, varname))
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

        public string NameAtIndex(int index)
        {
            return _player == null ? null : Native.GetPVarNameAtIndex(_player.Id, index);
        }

        public bool Delete(string varname)
        {
            return _player != null && Native.DeletePVar(_player.Id, varname);
        }
    }
}