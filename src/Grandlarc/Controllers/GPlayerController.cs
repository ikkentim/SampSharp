using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode.Controllers;

namespace Grandlarc.Controllers
{
    public class GPlayerController : PlayerController
    {
        public override void RegisterTypes()
        {
            GPlayer.Register<GPlayer>();
        }
    }
}
