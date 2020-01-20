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
using System.Globalization;
using System.Linq;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP.Commands.ParameterTypes
{
    /// <summary>
    ///     Represents a vehicle command parameter.
    /// </summary>
    public class VehicleType : ICommandParameterType
    {
        #region Implementation of ICommandParameterType

        /// <summary>
        ///     Gets the value for the occurance of this parameter type at the start of the commandText. The processed text will be
        ///     removed from the commandText.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="output">The output.</param>
        /// <returns>
        ///     true if parsed successfully; false otherwise.
        /// </returns>
        public bool Parse(ref string commandText, out object output)
        {
            var text = commandText.TrimStart();
            output = null;

            if (string.IsNullOrEmpty(text))
                return false;

            var word = text.Split(' ').First();

            // find a vehicle with a matching id.
            if (!int.TryParse(word, NumberStyles.Integer, CultureInfo.InvariantCulture, out var id)) 
                return false;

            var vehicle = BaseVehicle.Find(id);
            if (vehicle == null)
                return false;

            output = vehicle;
            commandText = commandText.Substring(word.Length).TrimStart(' ');
            return true;
        }

        #endregion
    }
}
