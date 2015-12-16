using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP.Commands
{
    public interface IPermissionChecker
    {
        string Message { get; }
        bool Check(BasePlayer player);
    }
}