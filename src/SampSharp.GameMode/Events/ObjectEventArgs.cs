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

using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.ObjectMoved" /> event.
    /// </summary>
    public class ObjectEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the ObjectEventArgs class.
        /// </summary>
        /// <param name="objectid">Id of the object.</param>
        public ObjectEventArgs(int objectid)
        {
            ObjectId = objectid;
        }

        /// <summary>
        ///     Gets the id of the object.
        /// </summary>
        public int ObjectId { get; private set; }

        /// <summary>
        ///     Gets the object.
        /// </summary>
        public GlobalObject GlobalObject
        {
            get { return ObjectId == GlobalObject.InvalidId ? null : GlobalObject.FindOrCreate(ObjectId); }
        }
    }
}