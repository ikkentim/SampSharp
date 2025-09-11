﻿// SampSharp
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

using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.Entities.Systems.Tests;

public class SystemCheckpointTest : ISystem
{
    [PlayerCommand]
    public void CheckpointCommand(Player sender)
    {
        sender.SetCheckpoint(sender.Position, 3);
    }
    
    [Event]
    public void OnPlayerEnterCheckpoint(Player sender)
    {
        sender.SendClientMessage("You've entered a checkpoint");
    }

    [Event]
    public void OnPlayerLeaveCheckpoint(Player sender)
    {
        sender.SendClientMessage("You've left a checkpoint");
    }
}