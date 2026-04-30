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
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;
using TestMode.Entities.Components;

namespace TestMode.Entities.Systems.Tests;

public class SystemPlayerCommandTest : ISystem
{
    [PlayerCommand]
    public void UnusedCommand(UnusedComponent sender)
    {
        Console.WriteLine("How did he manage to call this?! " + sender);
    }

    [PlayerCommand]
    public void TestCommand(Player sender, int a, int b, int c)
    {
        sender.SendClientMessage($"Hello, world! {a} {b} {c}");
    }

    [PlayerCommand]
    public void Test2Command(Player sender, int a, int b, int c, string d)
    {
        sender.SendClientMessage($"Hello, world! {a} {b} {c} {d}");
    }

    [PlayerCommand]
    public void Test3Command(Player sender, int a, int b, int c, string d = "a sensible default")
    {
        sender.SendClientMessage($"Hello, world! {a} {b} {c} {d}");
    }
}