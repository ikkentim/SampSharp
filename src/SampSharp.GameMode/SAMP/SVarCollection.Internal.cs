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
using SampSharp.Core.Natives.NativeObjects;

namespace SampSharp.GameMode.SAMP
{
    public partial class SVarCollection
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public class SVarCollectionInternal : NativeObjectSingleton<SVarCollectionInternal>
        {
            [NativeMethod]
            public virtual bool SetSVarInt(string varname, int value)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetSVarInt(string varname)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetSVarString(string varname, string value)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetSVarString(string varname, out string value, int size)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetSVarFloat(string varname, float value)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual float GetSVarFloat(string varname)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool DeleteSVar(string varname)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetSVarsUpperIndex()
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetSVarNameAtIndex(int index, out string varname, int size)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetSVarType(string varname)
            {
                throw new NativeNotImplementedException();
            }
        }
#pragma warning restore CS1591
    }
}