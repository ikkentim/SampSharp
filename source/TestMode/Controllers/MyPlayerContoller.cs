using GameMode.Controllers;
using GameMode.World;
using TestMode.World;

namespace TestMode.Controllers
{
    public class MyPlayerContoller : PlayerController
    {
        public override void RegisterTypes()
        {
            MyPlayer.Register<MyPlayer>();
        }
    }
}
