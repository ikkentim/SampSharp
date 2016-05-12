// SampSharp
// Copyright 2016 Tim Potze
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

using SampSharp.GameMode.API;

namespace SampSharp.GameMode.SAMP
{
    public partial class PVarCollection
    {
        private static class Internal
        {
            public delegate bool DeletePVarImpl(int playerid, string varname);

            public delegate float GetPVarFloatImpl(int playerid, string varname);

            public delegate int GetPVarIntImpl(int playerid, string varname);

            public delegate bool GetPVarNameAtIndexImpl(int playerid, int index, out string varname, int size);

            public delegate bool GetPVarStringImpl(int playerid, string varname, out string value, int size);

            public delegate int GetPVarsUpperIndexImpl(int playerid);

            public delegate int GetPVarTypeImpl(int playerid, string varname);

            public delegate bool SetPVarFloatImpl(int playerid, string varname, float value);

            public delegate bool SetPVarIntImpl(int playerid, string varname, int value);

            public delegate bool SetPVarStringImpl(int playerid, string varname, string value);

            [Native("SetPVarInt")] public static readonly SetPVarIntImpl SetPVarInt = null;
            [Native("GetPVarInt")] public static readonly GetPVarIntImpl GetPVarInt = null;
            [Native("SetPVarString")] public static readonly SetPVarStringImpl SetPVarString = null;
            [Native("GetPVarString")] public static readonly GetPVarStringImpl GetPVarString = null;
            [Native("SetPVarFloat")] public static readonly SetPVarFloatImpl SetPVarFloat = null;
            [Native("GetPVarFloat")] public static readonly GetPVarFloatImpl GetPVarFloat = null;
            [Native("DeletePVar")] public static readonly DeletePVarImpl DeletePVar = null;
            [Native("GetPVarsUpperIndex")] public static readonly GetPVarsUpperIndexImpl GetPVarsUpperIndex = null;
            [Native("GetPVarNameAtIndex")] public static readonly GetPVarNameAtIndexImpl GetPVarNameAtIndex = null;
            [Native("GetPVarType")] public static readonly GetPVarTypeImpl GetPVarType = null;
        }
    }
}