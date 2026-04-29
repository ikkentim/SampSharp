// SampSharp
// Copyright 2022 Tim Potze
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
using SampSharp.Entities;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.Entities.Systems.Tests;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S3400:Methods should not return constants", Justification = "Testing purposes")]
public class SystemRconCommandTest : ISystem
{
    [RconCommand]
    public bool RetCommand()
    {
        return true;
    }

    [RconCommand]
    public bool RetFalseCommand()
    {
        return false;
    }

    [RconCommand]
    public bool ErrCommand()
    {
        throw new InvalidOperationException("RCON threw an error");
    }

    [RconCommand]
    public void ArgsCommand(int a, int b, int c)
    {
        Console.WriteLine($"{a} {b} {c}");
    }

    [Event]
    public bool OnRconCommand(string cmd)
    {
        Console.WriteLine("RCON");

        return false;
    }
}