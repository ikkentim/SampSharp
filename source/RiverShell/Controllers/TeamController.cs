using RiverShell.World;
using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;

namespace RiverShell.Controllers
{
    public class TeamController : IController
    {
        public void RegisterEvents(BaseMode gameMode)
        {
            
        }

        public void RegisterTypes()
        {
            Team.Register<Team>();
        }
    }
}
