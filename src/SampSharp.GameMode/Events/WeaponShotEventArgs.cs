// SampSharp
// Copyright (C) 2015 Tim Potze
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

using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerWeaponShot" /> event.
    /// </summary>
    public class WeaponShotEventArgs : PlayerClickMapEventArgs
    {
        public WeaponShotEventArgs(int playerid, Weapon weapon, BulletHitType hittype, int hitid, Vector position)
            : base(playerid, position)
        {
            Weapon = weapon;
            BulletHitType = hittype;
            HitId = hitid;
        }

        public Weapon Weapon { get; private set; }

        public BulletHitType BulletHitType { get; private set; }

        public int HitId { get; private set; }
    }
}