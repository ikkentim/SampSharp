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
    /// <summary>
    /// Contains all special actions.
    /// </summary>
    public enum SpecialAction
    {
        /// <summary>
        /// Nothing.
        /// </summary>
        None = 0,
        /// <summary>
        /// Player is ducking.
        /// </summary>
        Duck = 1,
        /// <summary>
        /// Player is using a jetpack.
        /// </summary>
        Usejetpack = 2,
        /// <summary>
        /// Player is entering a vehicle.
        /// </summary>
        EnterVehicle = 3,
        /// <summary>
        /// Player is leaving a vehicle.
        /// </summary>
        ExitVehicle = 4,
        /// <summary>
        /// Player is dancing. (Style 1)
        /// </summary>
        Dance1 = 5,
        /// <summary>
        /// Player is dancing. (Style 2)
        /// </summary>
        Dance2 = 6,
        /// <summary>
        /// Player is dancing. (Style 3)
        /// </summary>
        Dance3 = 7,
        /// <summary>
        /// Player is dancing. (Style 4)
        /// </summary>
        Dance4 = 8,
        /// <summary>
        /// Player is holding his hands up.
        /// </summary>
        HandsUp = 10,
        /// <summary>
        /// Player is using a cellphone.
        /// </summary>
        UseCellphone = 11,
        /// <summary>
        /// Player is sitting.
        /// </summary>
        Sitting = 12,
        /// <summary>
        /// Player stops using a cellphone.
        /// </summary>
        StopUseCellphone = 13,
        /// <summary>
        /// Player is drinking a beer.
        /// </summary>
        DrinkBeer = 20,
        /// <summary>
        /// Player is smokking a cigarette.
        /// </summary>
        SmokeCiggy = 21,
        /// <summary>
        /// Player is drinking whine.
        /// </summary>
        DrinkWine = 22,
        /// <summary>
        /// Player is drinking sprunk.
        /// </summary>
        DrinkSprunk = 23,
        /// <summary>
        /// Player is cuffed.
        /// </summary>
        Cuffed = 24,
        /// <summary>
        /// PLayer is carrying.
        /// </summary>
        Carry = 25
    }
}