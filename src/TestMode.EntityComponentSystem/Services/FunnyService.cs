using System;
using System.Collections.Generic;
using System.Text;
using SampSharp.EntityComponentSystem.Entities;
using SampSharp.EntityComponentSystem.SAMP.Components;

namespace TestMode.EntityComponentSystem.Services
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
