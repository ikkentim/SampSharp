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
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;

namespace SampSharp.GameMode.World;

/// <summary>
///     Represents a player text label.
/// </summary>
public partial class PlayerTextLabel : IdentifiedOwnedPool<PlayerTextLabel, BasePlayer>
{
    /// <summary>
    ///     Identifier indicating the handle is invalid.
    /// </summary>
    public const int InvalidId = 0xFFFF;

    /// <summary>
    ///     Maximum number of per-player text labels which can exist.
    /// </summary>
    public const int Max = 1024;

    /// <summary>
    ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">Whether managed resources should be disposed.</param>
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        PlayerTextLabelInternal.Instance.DeletePlayer3DTextLabel(Owner.Id, Id);
    }

    private BasePlayer _attachedPlayer;
    private BaseVehicle _attachedVehicle;

    private Color _color;
    private float _drawDistance;
    private Vector3 _position;
    private bool _testLOS;
    private string _text;

    /// <summary>
    ///     Gets or sets the color of this <see cref="PlayerTextLabel" />.
    /// </summary>
    public virtual Color Color
    {
        get => _color;
        set
        {
            _color = value;
            PlayerTextLabelInternal.Instance.UpdatePlayer3DTextLabelText(Owner.Id, Id, Color, Text);
        }
    }

    /// <summary>
    ///     Gets or sets the text of this <see cref="PlayerTextLabel" />.
    /// </summary>
    public virtual string Text
    {
        get => _text;
        set
        {
            _text = value;
            PlayerTextLabelInternal.Instance.UpdatePlayer3DTextLabelText(Owner.Id, Id, Color, Text);
        }
    }

    /// <summary>
    ///     Gets or sets the position of this <see cref="PlayerTextLabel" />.
    /// </summary>
    public virtual Vector3 Position
    {
        get => _position;
        set
        {
            _position = value;
            PlayerTextLabelInternal.Instance.DeletePlayer3DTextLabel(Owner.Id, Id);
            Id = PlayerTextLabelInternal.Instance.CreatePlayer3DTextLabel(Owner.Id, Text, Color, Position.X, Position.Y, Position.Z,
                DrawDistance,
                AttachedPlayer?.Id ?? BasePlayer.InvalidId,
                AttachedVehicle?.Id ?? BaseVehicle.InvalidId, TestLOS);
        }
    }

    /// <summary>
    ///     Gets or sets the draw distance.
    /// </summary>
    public virtual float DrawDistance
    {
        get => _drawDistance;
        set
        {
            _drawDistance = value;
            PlayerTextLabelInternal.Instance.DeletePlayer3DTextLabel(Owner.Id, Id);
            Id = PlayerTextLabelInternal.Instance.CreatePlayer3DTextLabel(Owner.Id, Text, Color, Position.X, Position.Y, Position.Z,
                DrawDistance,
                AttachedPlayer?.Id ?? BasePlayer.InvalidId,
                AttachedVehicle?.Id ?? BaseVehicle.InvalidId, TestLOS);
        }
    }

    /// <summary>
    ///     Gets or sets a value indicating whether to test the line of sight.
    /// </summary>
    public virtual bool TestLOS
    {
        get => _testLOS;
        set
        {
            _testLOS = value;
            PlayerTextLabelInternal.Instance.DeletePlayer3DTextLabel(Owner.Id, Id);
            Id = PlayerTextLabelInternal.Instance.CreatePlayer3DTextLabel(Owner.Id, Text, Color, Position.X, Position.Y, Position.Z,
                DrawDistance,
                AttachedPlayer?.Id ?? BasePlayer.InvalidId,
                AttachedVehicle?.Id ?? BaseVehicle.InvalidId, TestLOS);
        }
    }

    /// <summary>
    ///     Gets or sets the attached player.
    /// </summary>
    public virtual BasePlayer AttachedPlayer
    {
        get => _attachedPlayer;
        set
        {
            _attachedPlayer = value;
            PlayerTextLabelInternal.Instance.DeletePlayer3DTextLabel(Owner.Id, Id);
            Id = PlayerTextLabelInternal.Instance.CreatePlayer3DTextLabel(Owner.Id, Text, Color, Position.X, Position.Y, Position.Z,
                DrawDistance,
                AttachedPlayer?.Id ?? BasePlayer.InvalidId,
                AttachedVehicle?.Id ?? BaseVehicle.InvalidId, TestLOS);
        }
    }

    /// <summary>
    ///     Gets or sets the attached vehicle.
    /// </summary>
    public virtual BaseVehicle AttachedVehicle
    {
        get => _attachedVehicle;
        set
        {
            _attachedVehicle = value;
            PlayerTextLabelInternal.Instance.DeletePlayer3DTextLabel(Owner.Id, Id);
            Id = PlayerTextLabelInternal.Instance.CreatePlayer3DTextLabel(Owner.Id, Text, Color, Position.X, Position.Y, Position.Z,
                DrawDistance,
                AttachedPlayer?.Id ?? BasePlayer.InvalidId,
                AttachedVehicle?.Id ?? BaseVehicle.InvalidId, TestLOS);
        }
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PlayerTextLabel" /> class.
    /// </summary>
    /// <param name="owner">The owner.</param>
    /// <param name="text">The text.</param>
    /// <param name="color">The color.</param>
    /// <param name="position">The position.</param>
    /// <param name="drawDistance">The draw distance.</param>
    /// <param name="testLOS">if set to <c>true</c> [test los].</param>
    /// <exception cref="System.ArgumentNullException">owner</exception>
    public PlayerTextLabel(BasePlayer owner, string text, Color color, Vector3 position, float drawDistance,
        bool testLOS)
    {
        Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        _color = color;
        _position = position;
        _drawDistance = drawDistance;
        _testLOS = testLOS;
        _text = text;

        Id = PlayerTextLabelInternal.Instance.CreatePlayer3DTextLabel(owner.Id, text, color, position.X, position.Y, position.Z,
            drawDistance,
            BasePlayer.InvalidId, BaseVehicle.InvalidId, testLOS);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PlayerTextLabel" /> class.
    /// </summary>
    /// <param name="owner">The owner.</param>
    /// <param name="text">The text.</param>
    /// <param name="color">The color.</param>
    /// <param name="position">The position.</param>
    /// <param name="drawDistance">The draw distance.</param>
    public PlayerTextLabel(BasePlayer owner, string text, Color color, Vector3 position, float drawDistance)
        : this(owner, text, color, position, drawDistance, true)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PlayerTextLabel" /> class.
    /// </summary>
    /// <param name="owner">The owner.</param>
    /// <param name="text">The text.</param>
    /// <param name="color">The color.</param>
    /// <param name="position">The position.</param>
    /// <param name="drawDistance">The draw distance.</param>
    /// <param name="testLOS">if set to <c>true</c> [test los].</param>
    /// <param name="attachedPlayer">The attached player.</param>
    /// <exception cref="System.ArgumentNullException">
    ///     owner
    ///     or
    ///     attachedPlayer
    /// </exception>
    public PlayerTextLabel(BasePlayer owner, string text, Color color, Vector3 position, float drawDistance,
        bool testLOS, BasePlayer attachedPlayer)
    {
        Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        _color = color;
        _position = position;
        _drawDistance = drawDistance;
        _testLOS = testLOS;
        _text = text;
        _attachedPlayer = attachedPlayer ?? throw new ArgumentNullException(nameof(attachedPlayer));

        Id = PlayerTextLabelInternal.Instance.CreatePlayer3DTextLabel(owner.Id, text, color, position.X, position.Y, position.Z,
            drawDistance,
            attachedPlayer.Id, BaseVehicle.InvalidId, testLOS);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PlayerTextLabel" /> class.
    /// </summary>
    /// <param name="owner">The owner.</param>
    /// <param name="text">The text.</param>
    /// <param name="color">The color.</param>
    /// <param name="position">The position.</param>
    /// <param name="drawDistance">The draw distance.</param>
    /// <param name="attachedPlayer">The attached player.</param>
    public PlayerTextLabel(BasePlayer owner, string text, Color color, Vector3 position, float drawDistance,
        BasePlayer attachedPlayer) : this(owner, text, color, position, drawDistance, true, attachedPlayer)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PlayerTextLabel" /> class.
    /// </summary>
    /// <param name="owner">The owner.</param>
    /// <param name="text">The text.</param>
    /// <param name="color">The color.</param>
    /// <param name="position">The position.</param>
    /// <param name="drawDistance">The draw distance.</param>
    /// <param name="testLOS">if set to <c>true</c> [test los].</param>
    /// <param name="attachedVehicle">The attached vehicle.</param>
    /// <exception cref="System.ArgumentNullException">
    ///     owner
    ///     or
    ///     attachedVehicle
    /// </exception>
    public PlayerTextLabel(BasePlayer owner, string text, Color color, Vector3 position, float drawDistance,
        bool testLOS, BaseVehicle attachedVehicle)
    {
        Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        _color = color;
        _position = position;
        _drawDistance = drawDistance;
        _testLOS = testLOS;
        _text = text;
        _attachedVehicle = attachedVehicle ?? throw new ArgumentNullException(nameof(attachedVehicle));

        Id = PlayerTextLabelInternal.Instance.CreatePlayer3DTextLabel(owner.Id, text, color, position.X, position.Y, position.Z,
            drawDistance,
            BasePlayer.InvalidId, attachedVehicle.Id, testLOS);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PlayerTextLabel" /> class.
    /// </summary>
    /// <param name="owner">The owner.</param>
    /// <param name="text">The text.</param>
    /// <param name="color">The color.</param>
    /// <param name="position">The position.</param>
    /// <param name="drawDistance">The draw distance.</param>
    /// <param name="attachedVehicle">The attached vehicle.</param>
    public PlayerTextLabel(BasePlayer owner, string text, Color color, Vector3 position, float drawDistance,
        BaseVehicle attachedVehicle)
        : this(owner, text, color, position, drawDistance, true, attachedVehicle)
    {
    }
}