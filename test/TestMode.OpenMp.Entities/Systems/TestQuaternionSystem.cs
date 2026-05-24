using System.Numerics;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.OpenMp.Entities.Systems;

[CommandGroup("quat")]
public class TestQuaternionSystem(IWorldService worldService, IEntityManager entityManager, IVehicleInfoService vehicleInfoService, ITimerService timerService) : ISystem
{
    [PlayerCommand]
    [Alias("coords")]
    public void CoordsCommand(Player player)
    {
        var pos = player.Position;
        Console.WriteLine($"Position: {pos}");
        player.SendClientMessage($"Position: {pos}");

        Mark(pos, "*", Color.White);
        Mark(pos + Vector3.UnitX, "+X", Color.Red);
        Mark(pos + Vector3.UnitY, "+Y", Color.Green);
        Mark(pos + Vector3.UnitZ, "+Z", Color.Blue);
    }

    [ConsoleCommand]
    public void TestCommand()
    {
        var eulerIn = new Vector3(10, 20, 30);
        var radIn = Vector3.DegreesToRadians(eulerIn);
        var quat = MathHelper.CreateQuaternionFromYawPitchRoll(radIn);
        var radOut = MathHelper.CreateYawPitchRollFromQuaternion(quat);
        var eulerOut = Vector3.RadiansToDegrees(radOut);
        Console.WriteLine($"{eulerIn} -> {eulerOut}");
    }

    [PlayerCommand]
    public void TestCommand(Player player)
    {
        var eulerIn = new Vector3(10, 20, 30);
        var radIn = Vector3.DegreesToRadians(eulerIn);
        var quat = MathHelper.CreateQuaternionFromYawPitchRoll(radIn);
        var radOut = MathHelper.CreateYawPitchRollFromQuaternion(quat);
        var eulerOut = Vector3.RadiansToDegrees(radOut);
        player.SendClientMessage($"{eulerIn} -> {eulerOut}");
    }

    [PlayerCommand]
    public void ArrowCommand(Player player, float x, float y, float z)
    {
        var vec = new Vector3(x, y, z);

        var index = 0;

        ArrowTest("create(vec)", _ => { });
        // ArrowTest("Rotation = MathHelper.CreateQuaternionFromYawPitchRoll(vec)", obj =>
        // {
        //     var rads = Vector3.DegreesToRadians(vec);
        //     var quat = MathHelper.CreateQuaternionFromYawPitchRoll(rads);
        //     obj.Rotation = quat;
        // }); 
        // ArrowTest("RotationEuler = vec", obj =>
        // {
        //     obj.RotationEuler = vec;
        // });
        ArrowTest("RotationEuler = RotationEuler", obj =>
        {
            player.SendClientMessage($"euser@create={vec}");
            player.SendClientMessage($"RotationEuler = {obj.RotationEuler}");

            obj.RotationEuler = obj.RotationEuler;
        });

        void ArrowTest(string txt, Action<GlobalObject> mod)
        {
            var offset = index++ * 1.0f;

            var pos = player.Position + GtaVector.Up + GtaVector.Forward * offset;
            var obj = worldService.CreateObject(19132, pos, vec);

            mod(obj);

            timerService.Delay(_ => obj.Destroy(), TimeSpan.FromSeconds(60));

            Mark(pos + GtaVector.Up, txt, Color.White, 60);
        }
    }

    [PlayerCommand]
    public void CircleCommand(Player player)
    {
        var center = player.Position + GtaVector.Up;

        Mark(center, "[c]", Color.Red);
        for (var angle = 0; angle < 360; angle += 45)
        {
            var pos = center + Vector3.Transform(GtaVector.Up * 3, Quaternion.CreateFromAxisAngle(GtaVector.Up, float.DegreesToRadians(angle)));

            Mark(pos, $"[{angle}]", Color.Blue);
        }
    }

    [PlayerCommand]
    public void AngleCommand(Player player)
    {
        var v = player.Vehicle;

        if (v is null)
        {
            player.SendClientMessage("Not in a vehicle.");
            return;
        }

        var zAngle = v.Angle;

        var mat = Matrix4x4.CreateFromQuaternion(v.Rotation);
        var zAngle2 = float.RadiansToDegrees(MathHelper.GetZAngleFromRotationMatrix(mat));

        player.SendClientMessage($"Vehicle Z-angle(open.mp): {zAngle}, ZAngle through RotQuat(s#): {zAngle2}");
    }


    [Timer(100)]
    public void UpdateMark()
    {
        foreach (var vehicle in entityManager.GetComponents<Vehicle>())
        {
            var label = vehicle.GetComponentInChildren<TextLabel>() 
                      ?? worldService.CreateTextLabel("[x]", Color.White, Vector3.Zero, 20, parent: vehicle);

            // calculate offset to the rear center bumper of the vehicle
            var model = vehicle.Model;
            var offset = vehicleInfoService.GetModelInfo(model, VehicleModelInfoType.PetrolCap);
 
            var rotMatrix = Matrix4x4.CreateFromQuaternion(vehicle.Rotation);
            var trMatrix = Matrix4x4.CreateTranslation(offset) * rotMatrix * Matrix4x4.CreateTranslation(vehicle.Position);

            var point = trMatrix.Translation;
     
            label.Position = point;
        }
    }

    private void Mark(Vector3 point, string txt, Color color, int sec = 10)
    {
        var label = worldService.CreateTextLabel(txt, color, point, 100, 0, false);
        timerService.Delay(_ => label.Destroy(), TimeSpan.FromSeconds(sec));
    }
}