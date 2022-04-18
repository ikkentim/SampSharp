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

namespace TestMode.Entities.Systems.Tests;

public class SystemTextdrawTest : ISystem
{
    private TextDraw _welcome;

    [Event]
    public void OnGameModeInit(IWorldService worldService)
    {
        _welcome = worldService.CreateTextDraw(new Vector2(20, 40), "Hello, world");
        _welcome.Alignment = TextDrawAlignment.Left;
        _welcome.Font = TextDrawFont.Diploma;
        _welcome.Proportional = true;
        Console.WriteLine("TD pos: " + _welcome.Position);
        Console.WriteLine(_welcome.Entity.ToString());
    }


    [Event]
    public void OnPlayerConnect(Player player)
    {
        _welcome.Show(player.Entity);
    }

    [PlayerCommand]
    public void HelloPlayerCommand(Player player, IWorldService worldService)
    {
        var welcome = worldService.CreatePlayerTextDraw(player, new Vector2(100, 80), "Hello, Player");
        welcome.Alignment = TextDrawAlignment.Left;
        welcome.Font = TextDrawFont.Diploma;
        welcome.Proportional = true;
        welcome.LetterSize = new Vector2(1, 1.2f);
        welcome.Show();
        player.SendClientMessage("Show see msg now...");
    }
}