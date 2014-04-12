using GameMode.Controllers;
using RiverShell.World;

namespace RiverShell.Controllers
{
    public class RPlayerController : PlayerController
    {
        public override void RegisterTypes()
        {
            RPlayer.Register<RPlayer>();
        }
    }
}
