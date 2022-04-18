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

using System.Linq;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;
using TestMode.Entities.Components;

namespace TestMode.Entities.Systems.Tests;

public class SystemEntityComponentsTest : ISystem
{
    [PlayerCommand]
    public void AddComponentCommand(Player player)
    {
        if (player.GetComponent<TestComponent>() == null)
            player.AddComponent<TestComponent>();

        player.SendClientMessage("Added TestComponent!");
    }

    [PlayerCommand]
    public void RemoveComponentCommand(Player player)
    {
        player.DestroyComponents<TestComponent>();

        player.SendClientMessage("Remove TestComponent!");
    }

    [PlayerCommand]
    public void GetComponentsCommand(Player player)
    {
        foreach (var comp in player.GetComponents<Component>())
            player.SendClientMessage(comp.GetType()
                .FullName);
    }

    [PlayerCommand]
    public void EntitiesCommand(Player player, IEntityManager entityManager)
    {
        void Print(int indent, EntityId entity)
        {
            var ind = string.Concat(Enumerable.Repeat(' ', indent));

            player.SendClientMessage($"{ind}{entity}");
            foreach (var com in entityManager.GetComponents<Component>(entity))
                player.SendClientMessage($"{ind}::>{com.GetType().Name}");

            foreach (var child in entityManager.GetChildren(entity))
                Print(indent + 2, child);
        }

        foreach (var root in entityManager.GetRootEntities())
            Print(0, root);
    }
}