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

namespace SampSharp.Entities.SAMP;

/// <summary>Represents a component which provides the data and functionality of a gang zone.</summary>
public class GangZone : Component
{
    /// <summary>Constructs an instance of GangZone, should be used internally.</summary>
    protected GangZone(float minX, float minY, float maxX, float maxY)
    {
        MinX = minX;
        MaxX = maxX;
        MinY = minY;
        MaxY = maxY;
    }

    /// <summary>Gets the minimum x value for this <see cref="GangZone" />.</summary>
    public virtual float MinX { get; }

    /// <summary>Gets the minimum y value for this <see cref="GangZone" />.</summary>
    public virtual float MinY { get; }

    /// <summary>Gets the maximum x value for this <see cref="GangZone" />.</summary>
    public virtual float MaxX { get; }

    /// <summary>Gets the maximum y value for this <see cref="GangZone" />.</summary>
    public virtual float MaxY { get; }

    /// <summary>Gets or sets the color of this <see cref="GangZone" />.</summary>
    public virtual Color Color { get; set; }

    /// <summary>Shows this <see cref="GangZone" />.</summary>
    public virtual void Show()
    {
        GetComponent<NativeGangZone>()
            .GangZoneShowForAll(Color);
    }

    /// <summary>Shows this <see cref="GangZone" /> to the specified <paramref name="player" />.</summary>
    /// <param name="player">The player.</param>
    public virtual void Show(EntityId player)
    {
        if (!player.IsOfType(SampEntities.PlayerType))
            throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

        GetComponent<NativeGangZone>()
            .GangZoneShowForPlayer(player, Color);
    }

    /// <summary>Hides this <see cref="GangZone" />.</summary>
    public virtual void Hide()
    {
        GetComponent<NativeGangZone>()
            .GangZoneHideForAll();
    }

    /// <summary>Hides this <see cref="GangZone" /> for the specified <paramref name="player" />.</summary>
    /// <param name="player">The player.</param>
    public virtual void Hide(EntityId player)
    {
        if (!player.IsOfType(SampEntities.PlayerType))
            throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

        GetComponent<NativeGangZone>()
            .GangZoneHideForPlayer(player);
    }

    /// <summary>Flashes this <see cref="GangZone" />.</summary>
    /// <param name="color">The color.</param>
    public virtual void Flash(Color color)
    {
        GetComponent<NativeGangZone>()
            .GangZoneFlashForAll(color);
    }

    /// <summary>Flashes this <see cref="GangZone" /> for the specified <paramref name="player" />.</summary>
    /// <param name="player">The player.</param>
    public virtual void Flash(EntityId player)
    {
        if (!player.IsOfType(SampEntities.PlayerType))
            throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

        Flash(player, new Color());
    }

    /// <summary>Flashes this <see cref="GangZone" /> for the specified <paramref name="player" />.</summary>
    /// <param name="player">The player.</param>
    /// <param name="color">The color.</param>
    public virtual void Flash(EntityId player, Color color)
    {
        if (!player.IsOfType(SampEntities.PlayerType))
            throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

        GetComponent<NativeGangZone>()
            .GangZoneFlashForPlayer(player, color);
    }

    /// <summary>Stops this <see cref="GangZone" /> from flash.</summary>
    public virtual void StopFlash()
    {
        GetComponent<NativeGangZone>()
            .GangZoneStopFlashForAll();
    }

    /// <summary>Stops this <see cref="GangZone" /> from flash for the specified player.</summary>
    /// <param name="player">The player.</param>
    public virtual void StopFlash(EntityId player)
    {
        if (!player.IsOfType(SampEntities.PlayerType))
            throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

        GetComponent<NativeGangZone>()
            .GangZoneStopFlashForPlayer(player);
    }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        GetComponent<NativeGangZone>()
            .GangZoneDestroy();
    }
}
