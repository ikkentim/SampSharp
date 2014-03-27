// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

namespace GameMode.Definitions
{
    public enum CameraMode
    {
        Invalid = -1,
        BehindCar = 3,
        FollowPed = 4,
        SniperAiming = 7,
        RocketLauncherAiming = 8,
        Fixed = 15,
        FirstPerson = 16,
        NormalCar = 18,
        BehindBoat = 22,
        CameraWeaponAiming = 46,
        HeatseekingRocketLauncher = 51,
        AimingWeapon = 53,
        VehicleDriveBy = 55,
        HelicopterChaseCam = 56
    }
}