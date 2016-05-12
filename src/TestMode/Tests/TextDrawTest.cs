// SampSharp
// Copyright 2016 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
            var d = new TextDraw(new Vector2(100, 100), "Diploma", TextDrawFont.Diploma);
            var p = new TextDraw(new Vector2(100, 140), "Pricedown", TextDrawFont.Pricedown);
            var n = new TextDraw(new Vector2(100, 180), "Normal", TextDrawFont.Normal);

            p.Show(player);
            d.Show(player);
            n.Show(player);
        }
    }
}