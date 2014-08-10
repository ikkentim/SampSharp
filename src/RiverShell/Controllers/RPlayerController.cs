using RiverShell.World;
using SampSharp.GameMode.Controllers;

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
