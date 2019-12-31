using SampSharp.Entities;
using SampSharp.Entities.SAMP.Components;

namespace TestMode.Entities.Services
{
    public class FunnyService : IFunnyService
    {
        public string MakePlayerNameFunny(Entity player)
        {
            var name = player.GetComponent<Player>().Name;

            var result = string.Empty;

            for (var i = name.Length - 1; i >= 0; i--)
            {
                result += name[i];
            }

            return result;
        }
    }
}
