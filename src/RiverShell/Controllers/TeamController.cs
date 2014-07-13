using RiverShell.World;
using SampSharp.GameMode.Controllers;

namespace RiverShell.Controllers
{
    public class TeamController : ITypeProvider, IController
    {
        public void RegisterTypes()
        {
            Team.Register<Team>();
        }
    }
}
