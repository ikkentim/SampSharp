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

using SampSharp.GameMode.Definitions;

namespace SampSharp.GameMode.Events
{
    public class PlayerDamageEventArgs : PlayerEventArgs
    {
        public PlayerDamageEventArgs(int playerid, int otherplayerid, float amount, Weapon weapon, BodyPart bodypart)
            : base(playerid)
        {
            OtherPlayerId = otherplayerid;
            Amount = amount;
            Weapon = weapon;
            BodyPart = bodypart;
        }

        public int OtherPlayerId { get; private set; }
        public float Amount { get; private set; }
        public Weapon Weapon { get; private set; }
        public BodyPart BodyPart { get; private set; }
    }
}