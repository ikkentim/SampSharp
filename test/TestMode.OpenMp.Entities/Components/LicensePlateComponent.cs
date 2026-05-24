using SampSharp.Entities;

namespace TestMode.OpenMp.Entities.Components;

public class LicensePlateComponent(string licensePlate) : Component
{
    public string LicensePlate { get; set; } = licensePlate;

    public override string ToString()
    {
        return $"( LicensePlate = {LicensePlate})";
    }
}