using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    public class TextDrawTest
    {
        [Command("tdtesta")]
        public static void Testa(BasePlayer player)
        {
            var d= new TextDraw(new Vector2(100, 100), "Diploma", TextDrawFont.Diploma);
            var p= new TextDraw(new Vector2(100, 140), "Pricedown", TextDrawFont.Pricedown);
            var n = new TextDraw(new Vector2(100, 180), "Normal", TextDrawFont.Normal);

            p.Show(player);
            d.Show(player);
            n.Show(player);
        }
    }
}
