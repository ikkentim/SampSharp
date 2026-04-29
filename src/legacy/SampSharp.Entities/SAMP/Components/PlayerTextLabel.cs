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

/// <summary>Represents a component which provides the data and functionality of a player 3D text label.</summary>
public class PlayerTextLabel : Component
{
    /// <summary>Constructs an instance of PlayerTextLabel, should be used internally.</summary>
    protected PlayerTextLabel(string text, Color color, Vector3 position, float drawDistance, int virtualWorld, bool testLos, EntityId attachedEntity)
    {
        Text = text;
        Color = color;
        Position = position;
        DrawDistance = drawDistance;
        VirtualWorld = virtualWorld;
        TestLos = testLos;
        AttachedEntity = attachedEntity;
    }

    /// <summary>Gets the color of this player text label.</summary>
    public virtual Color Color { get; }

    /// <summary>Gets the text of this player text label.</summary>
    public virtual string Text { get; }

    /// <summary>Gets the position of this player text label.</summary>
    public virtual Vector3 Position { get; }

    /// <summary>Gets the draw distance.</summary>
    public virtual float DrawDistance { get; }

    /// <summary>Gets the virtual world.</summary>
    public virtual int VirtualWorld { get; }

    /// <summary>Gets a value indicating whether to test the line of sight.</summary>
    public virtual bool TestLos { get; }

    /// <summary>Gets the attached entity.</summary>
    public virtual EntityId AttachedEntity { get; }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        GetComponent<NativePlayerTextLabel>()
            .DeletePlayer3DTextLabel();
    }
}
