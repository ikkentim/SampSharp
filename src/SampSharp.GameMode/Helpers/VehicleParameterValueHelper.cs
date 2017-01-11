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
using SampSharp.GameMode.Definitions;

namespace SampSharp.GameMode.Helpers
{
    /// <summary>
    ///     Contains helper methods for <see cref="VehicleParameterValue" /> values.
    /// </summary>
    internal static class VehicleParameterValueHelper
    {
        /// <summary>
        ///     Converts the specified <paramref name="value" /> to a boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default (unset) value.</param>
        /// <returns>The resulting boolean.</returns>
        public static bool ToBool(this VehicleParameterValue value, bool defaultValue = false)
        {
            return value == VehicleParameterValue.Unset ? defaultValue : (value == VehicleParameterValue.On);
        }

        /// <summary>
        ///     Converts the specified boolean <paramref name="value" /> to a <see cref="VehicleParameterValue" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The resulting <see cref="VehicleParameterValue" />.</returns>
        public static VehicleParameterValue FromBool(bool value)
        {
            return value ? VehicleParameterValue.On : VehicleParameterValue.Off;
        }
    }
}