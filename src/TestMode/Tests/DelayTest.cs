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
using System.Threading;
using SampSharp.GameMode.SAMP;

namespace TestMode.Tests
{
    public class DelayTest : ITest
    {
        public void Start(GameMode gameMode)
        {
            Thread main = Thread.CurrentThread;

            Console.WriteLine("Starting delay on main thread");

            Delay.Run(1500,
                () => Console.WriteLine("Delay passed. Now on main thread? {0}", main == Thread.CurrentThread));
        }
    }
}