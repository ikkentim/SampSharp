using System.Numerics;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace TestMode.OpenMp.Entities;

public class RotationTestingSystem(IWorldService worldService, IEntityManager entityManager, IVehicleInfoService vehicleInfoService, ITimerService timerService) : ISystem
{
    [Event]
    public bool OnConsoleText(string command, string args, ConsoleCommandSender sender)
    {
        if (command == "test")
        {
            var eulerIn = new Vector3(10, 20, 30);
            var radIn = Vector3.DegreesToRadians(eulerIn);
            var quat = MathHelper.CreateQuaternionFromYawPitchRoll(radIn);
            var radOut = MathHelper.CreateYawPitchRollFromQuaternion(quat);
            var eulerOut = Vector3.RadiansToDegrees(radOut);
            Console.WriteLine($"{eulerIn} -> {eulerOut}");
            return true;
        }

        return false;
    }

    [Event]
    public bool OnPlayerCommandText(Player player, string cmdText)
    {
        if (cmdText.StartsWith("/spawn") && cmdText.Length > 7)
        {
            var mdl = int.Parse(cmdText[7..]);
            var vehicle = worldService.CreateVehicle((VehicleModelType)mdl, player.Position + GtaVector.Up * 5, 0, -1, -1);
            player.PutInVehicle(vehicle, 0);
            return true;
        }

        if (cmdText.StartsWith("/coords"))
        {
            var pos = player.Position;
            Console.WriteLine($"Position: {pos}");
            player.SendClientMessage($"Position: {pos}");

            Mark(pos, "*", Color.White);
            Mark(pos + Vector3.UnitX, "+X", Color.Red);
            Mark(pos + Vector3.UnitY, "+Y", Color.Green);
            Mark(pos + Vector3.UnitZ, "+Z", Color.Blue);
            return true;
        }

        if (cmdText == "/test")
        {
            var eulerIn = new Vector3(10, 20, 30);
            var radIn = Vector3.DegreesToRadians(eulerIn);
            var quat = MathHelper.CreateQuaternionFromYawPitchRoll(radIn);
            var radOut = MathHelper.CreateYawPitchRollFromQuaternion(quat);
            var eulerOut = Vector3.RadiansToDegrees(radOut);
            Console.WriteLine($"{eulerIn} -> {eulerOut}");
            player.SendClientMessage($"{eulerIn} -> {eulerOut}");
            return true;
        }

        if (cmdText.StartsWith("/arrow"))
        {
            var pts = cmdText.Length == 6
                ?
                [
                    100, 200, 300
                ]
                : cmdText[6..].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => float.TryParse(x, out var y) ? y : 0).ToArray();

            if (pts.Length < 3) { return false;}
            var vec = new Vector3(pts[0], pts[1], pts[2]);
            
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

            return true;

            void ArrowTest(string txt, Action<GlobalObject> mod)
            {
                var offset = index++ * 1.0f;

                var pos = player.Position + GtaVector.Up + GtaVector.Forward * offset;
                var obj = worldService.CreateObject(19132, pos, vec);

                mod(obj);

                timerService.Delay(_ => entityManager.Destroy(obj), TimeSpan.FromSeconds(60));

                Mark(pos + GtaVector.Up, txt, Color.White, 60);
            }
        }
        if (cmdText == "/circle")
        {
            var center = player.Position + GtaVector.Up;

            Mark(center, "[c]", Color.Red);
            for(var angle = 0; angle < 360; angle += 45)
            {
                var pos = center + Vector3.Transform(GtaVector.Up * 3, Quaternion.CreateFromAxisAngle(GtaVector.Up, float.DegreesToRadians(angle)));
            
                Mark(pos, $"[{angle}]", Color.Blue);
            }

            return true;
        }

        if (cmdText == "/angle")
        {
            var v= player.Vehicle;

            if (v == null)
            {
                return false;
            }

            var zAngle = v.Angle;

            var mat = Matrix4x4.CreateFromQuaternion(v.Rotation);
            var zAngle2 = float.RadiansToDegrees(MathHelper.GetZAngleFromRotationMatrix(mat));

            player.SendClientMessage($"Vehicle Z-angle(open.mp): {zAngle}, ZAngle through RotQuat(s#): {zAngle2}");
            
            return true;
        }
        return false;
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
        timerService.Delay(_ => entityManager.Destroy(label), TimeSpan.FromSeconds(sec));
    }
}