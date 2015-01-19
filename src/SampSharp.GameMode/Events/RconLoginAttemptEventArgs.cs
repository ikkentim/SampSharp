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

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.RconLoginAttempt" /> event.
    /// </summary>
    public class RconLoginAttemptEventArgs
    {
        public RconLoginAttemptEventArgs(string ip, string password, bool success)
        {
            IP = ip;
            Password = password;
            SuccessfulLogin = success;
        }

        public string IP { get; private set; }

        public string Password { get; private set; }

        public bool SuccessfulLogin { get; private set; }
    }
}