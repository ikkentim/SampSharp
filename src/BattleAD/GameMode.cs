using SampSharp.GameMode;

namespace BattleAD
{
    public class GameMode : BaseMode
    {
        public static string Version = "1.0.0";

        public override bool OnGameModeInit()
        {
            SetGameModeText("Battle A/D");
            UsePlayerPedAnims();


            return base.OnGameModeInit();
        }
    }
}
