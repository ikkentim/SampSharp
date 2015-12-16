using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP.Commands
{
    public class RemoteAdminPermissionChecker : IPermissionChecker
    {
        #region Implementation of IPermissionChecker

        public string Message => "You need to be logged in as RCON admin to use this command.";

        public bool Check(BasePlayer player)
        {
            return player.IsAdmin;
        }

        #endregion
    }
}