using System.Collections.Generic;
using System.Threading.Tasks;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.Entities.Systems.Tests
{
    public class SystemVehicleRotationQuaternionErrorTest : ISystem
    {
        [PlayerCommand]
        public async void RearCommand(Player player, IEntityManager entityManager, IWorldService worldService,
            IVehicleInfoService vehicleInfoService)
        {
            var labels = new List<EntityId>();

            foreach (var vehicle in entityManager.GetComponents<Vehicle>())
            {
                var model = vehicle.Model;

                var size = vehicleInfoService.GetModelInfo(model, VehicleModelInfoType.Size);
                var bumper = vehicleInfoService.GetModelInfo(model, VehicleModelInfoType.RearBumperZ);
                var offset = new Vector3(0, -size.Y / 2, bumper.Z);

                var rotation = vehicle.RotationQuaternion;

                var mRotation =
                    rotation.LengthSquared >
                    10000 // Unoccupied vehicle updates corrupt the internal vehicle world matrix
                        ? Matrix.CreateRotationZ(MathHelper.ToRadians(vehicle.Angle))
                        : Matrix.CreateFromQuaternion(rotation);

                var matrix = Matrix.CreateTranslation(offset) *
                             mRotation *
                             Matrix.CreateTranslation(vehicle.Position);

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
}