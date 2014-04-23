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

namespace SampSharp.GameMode.Definitions
{
    /// <summary>
    ///     Contains all vehiclemodel info types.
    /// </summary>
    public enum VehicleModelInfo
    {
        /// <summary>
        ///     Vehicle size
        /// </summary>
        Size = 1,

        /// <summary>
        ///     Position of the front seat. (calculated from the center of the vehicle)
        /// </summary>
        FrontSeat = 2,

        /// <summary>
        ///     Position of the rear seat. (calculated from the center of the vehicle)
        /// </summary>
        RearSeat = 3,

        /// <summary>
        ///     Position of the fuel cap. (calculated from the center of the vehicle)
        /// </summary>
        PetrolCap = 4,

        /// <summary>
        ///     Position of the front wheels. (calculated from the center of the vehicle)
        /// </summary>
        WheelsFront = 5,

        /// <summary>
        ///     Position of the rear wheels. (calculated from the center of the vehicle)
        /// </summary>
        WheelsRear = 6,

        /// <summary>
        ///     Position of the middle wheels, applies to vehicles with 3 axes. (calculated from the center of the vehicle)
        /// </summary>
        WheelsMiddle = 7,

        /// <summary>
        ///     Height of the front bumper. (calculated from the center of the vehicle)
        /// </summary>
        FrontBumperZ = 8,

        /// <summary>
        ///     Height of the rear bumper. (calculated from the center of the vehicle)
        /// </summary>
        RearBumperZ = 9
    }
}