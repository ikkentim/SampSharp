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
using SampSharp.GameMode.SAMP;

namespace SampSharp.GameMode.World
{
    public interface IGameObject : IIdentifyable, IWorldObject
    {
        bool IsMoving { get; }
        bool IsValid { get; }
        int ModelId { get; }
        float DrawDistance { get; }


        void AttachTo(Player player, Vector offset, Vector rotation);
        void AttachTo(Vehicle vehicle, Vector offset, Vector rotation);
        int Move(Vector position, float speed, Vector rotation);
        int Move(Vector position, float speed);
        void Stop();
        void SetMaterial(int materialindex, int modelid, string txdname, string texturename, Color materialcolor);

        void SetMaterialText(string text, int materialindex, ObjectMaterialSize materialsize, string fontface,
            int fontsize, bool bold, Color foreColor, Color backColor, ObjectMaterialTextAlign textalignment);
    }
}