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

using SampSharp.GameMode.Pools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP.Commands
{
    public abstract class Command : Pool<Command>
    {
        public abstract string Name { get; protected set; }

        public abstract bool RunCommand(Player player, string args);

        public virtual bool HasPlayerPermissionForCommand(Player player)
        {
            return true;
        }

        public virtual bool CommandTextMatchesCommand(ref string commandText)
        {
            if (commandText.StartsWith(Name))
            {
                commandText = Name == commandText ? string.Empty : commandText.Substring(Name.Length);
                return true;
            }
            return false;
        }
    }
}