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

using System.Collections.Generic;
using System.Threading.Tasks;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.Entities.Systems.Tests;

public class SystemVehicleRotationQuaternionErrorTest : ISystem
{
    [PlayerCommand]
    public async void RearCommand(Player player, IEntityManager entityManager, IWorldService worldService, IVehicleInfoService vehicleInfoService)
    {
        var labels = new List<EntityId>();

        foreach (var vehicle in entityManager.GetComponents<Vehicle>())
        {
            var model = vehicle.Model;

            var size = vehicleInfoService.GetModelInfo(model, VehicleModelInfoType.Size);
            var bumper = vehicleInfoService.GetModelInfo(model, VehicleModelInfoType.RearBumperZ);
            var offset = new Vector3(0, -size.Y / 2, bumper.Z);

            var rotation = vehicle.RotationQuaternion;

            var mRotation = rotation.LengthSquared > 10000 // Unoccupied vehicle updates corrupt the internal vehicle world matrix
                ? Matrix.CreateRotationZ(MathHelper.ToRadians(vehicle.Angle))
                : Matrix.CreateFromQuaternion(rotation);

            var matrix = Matrix.CreateTranslation(offset) * mRotation * Matrix.CreateTranslation(vehicle.Position);

            var point = matrix.Translation;

            var label = worldService.CreateTextLabel("[x]", Color.Blue, point, 100, 0, false);
            labels.Add(label.Entity);
        }

        player.SendClientMessage("Points added");

        await Task.Delay(10000);

        foreach (var l in labels)
            entityManager.Destroy(l);

        player.SendClientMessage("Points removed");
    }
}