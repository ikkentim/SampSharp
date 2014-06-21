using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode.Display;

namespace TestMode.Tests
{
    class DialogTest : ITest
    {
        public void Start(GameMode gameMode)
        {
            //Hide before any dialog is shown
            gameMode.PlayerConnected += (sender, args) => Dialog.Hide(args.Player);
        }
    }
}
