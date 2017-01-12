// SampSharp
// Copyright 2017 Tim Potze
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
using System;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    public class CharsetTest// : ITest
    {
        public void Start(GameMode gameMode)
        {
            Console.WriteLine(": Common German characters:");
            Console.WriteLine(": abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
            Console.WriteLine(": \u0410");
            Console.WriteLine(": Ä ä Ö ö Ü ü ß ...");
            Console.WriteLine();
            Console.WriteLine(": Czech characters:");
            Console.WriteLine(": abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
            Console.WriteLine(": ú ů ý ž ’ „ “ ” – — á č ď é ě í ň ó ř š ť Ú Ů Ý Ž Á Č Ď É Ě Í Ň Ó Ř Š Ť");
            Console.WriteLine(": ú ů ý ž ’ „ “ ” – — á č ď é ě í ň ó ř š ť Ú Ů Ý Ž Á Č Ď É Ě Í Ň Ó Ř Š Ť");
            Console.WriteLine();
            Console.WriteLine(": Cyrillic characters:");
            Console.WriteLine(": АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЬЫЪЭЮЯ");
            Console.WriteLine();
        }

        [Command("charset")]
        public static bool CharsetCommand(BasePlayer player)
        {
            BasePlayer.SendClientMessageToAll(Color.Teal, "this is a test: \u00D6");
            BasePlayer.SendClientMessageToAll(Color.Teal, "this is a test: Ä ä Ö ö Ü ü ß ...");

            BasePlayer.SendClientMessageToAll("Cyrillic characters:");
            BasePlayer.SendClientMessageToAll("А Б В Г Д Е Ё Ж З И Й К Л М Н О П Р С Т У Ф Х Ц Ч Ш Щ Ь Ы Ъ Э Ю Я");
            BasePlayer.SendClientMessageToAll("Czech characters:");
            BasePlayer.SendClientMessageToAll("ú ů ý ž ’ „ “ ” – — á č ď é ě í ň ó ř š ť Ú Ů Ý Ž Á Č Ď É Ě Í Ň Ó Ř Š Ť");

            return true;
        }
    }
}