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
using SampSharp.Core.Logging;
using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers;

/// <summary>A controller processing all NPC actions.</summary>
[Controller]
public class NpcController : Disposable, IEventListener, ITypeProvider
{
    /// <summary>Registers the events this NpcController wants to listen to.</summary>
    /// <param name="gameMode">The running GameMode.</param>
    public virtual void RegisterEvents(BaseMode gameMode)
    {
        ArgumentNullException.ThrowIfNull(gameMode);

        //Register all npc events
        //Same order as omp_npcs.inc
        gameMode.NPCFinishedMove += (sender, args) => (sender as Npc)?.OnFinishMove(args);
        gameMode.NPCCreated += (sender, args) => (sender as Npc)?.OnCreate(args);
        gameMode.NPCDestroyed += (sender, args) => (sender as Npc)?.OnDestroy(args);
        gameMode.NPCSpawned += (sender, args) => (sender as Npc)?.OnSpawn(args);
        gameMode.NPCRespawned += (sender, args) => (sender as Npc)?.OnRespawn(args);
        gameMode.NPCWeaponStateChanged += (sender, args) => (sender as Npc)?.OnWeaponStateChange(args);
        gameMode.NPCTakeDamage += (sender, args) => (sender as Npc)?.OnTakeDamage(args);
        gameMode.NPCGiveDamage += (sender, args) => (sender as Npc)?.OnGiveDamage(args);
        gameMode.NPCDied += (sender, args) => (sender as Npc)?.OnDeath(args);
        gameMode.NPCPlaybackStarted += (sender, args) => (sender as Npc)?.OnPlaybackStart(args);
        gameMode.NPCPlaybackEnded += (sender, args) => (sender as Npc)?.OnPlaybackEnd(args);
        gameMode.NPCWeaponShot += (sender, args) => (sender as Npc)?.OnWeaponShot(args);
        gameMode.NPCFinishNodePoint += (sender, args) => (sender as Npc)?.OnFinishNodePoint(args);
        gameMode.NPCFinishNode += (sender, args) => (sender as Npc)?.OnFinishNode(args);
        gameMode.NPCChangeNode += (sender, args) => (sender as Npc)?.OnChangeNode(args);
        gameMode.NPCFinishMovePath += (sender, args) => (sender as Npc)?.OnFinishMovePath(args);
        gameMode.NPCFinishMovePathPoint += (sender, args) => (sender as Npc)?.OnFinishMovePathPoint(args);
    }

    /// <summary>Registers types this NpcController requires the system to use.</summary>
    public virtual void RegisterTypes()
    {
        Npc.Register<Npc>();
    }

    /// <summary>Performs tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
    /// <param name="disposing">Whether managed resources should be disposed.</param>
    protected override void Dispose(bool disposing)
    {
        if (!disposing) return;

        foreach (var npc in Npc.All)
        {
            npc.Dispose();
        }
    }
}