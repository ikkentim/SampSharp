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
using System.Threading.Tasks;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.Entities.Systems.Tests;

public class SystemDialogsTest : ISystem
{
    [PlayerCommand]
    public void TablistCommand(Player player, IDialogService dialogService)
    {
        dialogService.Show(player.Entity,
            new TablistDialog("Hello", "Left", "right", "Column1", "Column2", "Column3")
            {
                {"r1c1", "r1c2", "r1c3"},
                {new[] {"r2c1", "r2c2", "r2c3"}, @"Tag!!!"},
                {"r3c1", "r3c2", $"{Color.Red}r3c3"}
            },
            r =>
            {
                player.SendClientMessage(
                    $"Resp: {r.Response} {r.ItemIndex}: ({string.Join(" ", r.Item?.Columns!)},{r.Item?.Tag})");
            });
        player.PlaySound(1083);
    }

    [PlayerCommand]
    public void ListCommand(Player player, IDialogService dialogService)
    {
        dialogService.Show(player.Entity, new ListDialog("Hello", "Left", "right")
        {
            "item1",
            "item2",
            $"{Color.Red}item3"
        }, r => { player.SendClientMessage($"Resp: {r.Response} {r.ItemIndex}: ({r.Item?.Text},{r.Item?.Tag})"); });
        player.PlaySound(1083);
    }

    [PlayerCommand]
    public void DialogCommand(Player player, IDialogService dialogService)
    {
        dialogService.Show(player.Entity,
            new MessageDialog("Hello", "Hello, world! " + DateTime.Now, "Left", "right"),
            r => { player.SendClientMessage($"Resp: {r.Response}"); });
        player.PlaySound(1083);
    }

    [PlayerCommand("2dialogs")]
    public async void TwoDialogsCommand(Player player, IDialogService dialogService)
    {
        dialogService.Show(player.Entity,
            new MessageDialog("Hello", "Hello, world! " + DateTime.Now, "Left", "right"),
            r => { player.SendClientMessage($"Resp 1: {r.Response}"); });
        player.PlaySound(1083);

        await Task.Delay(2000);

        var task = dialogService.Show(player.Entity,
            new MessageDialog("Hello", "Hello, world! x2 " + DateTime.Now, "Left", "right"));
        player.PlaySound(1083);

        var r2 = await task;
        player.SendClientMessage($"Resp 2: {r2.Response}");
    }
}