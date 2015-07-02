using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode.Checkers
{
    class AdminChecker : IPermissionChecker
    {
        public string Message { get; private set; }

        public AdminChecker()
        {
            Message = "You must be admin to use this command!";
        }

        public bool Check(GtaPlayer player)
        {
            return player.IsAdmin;
        }
    }
}
