// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using System;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    public class CharsetTest : ITest
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
        public static bool CharsetCommand(GtaPlayer player)
        {
            GtaPlayer.SendClientMessageToAll(Color.Teal, "this is a test: \u00D6");
            GtaPlayer.SendClientMessageToAll(Color.Teal, "this is a test: Ä ä Ö ö Ü ü ß ...");

            GtaPlayer.SendClientMessageToAll("Cyrillic characters:");
            GtaPlayer.SendClientMessageToAll("А Б В Г Д Е Ё Ж З И Й К Л М Н О П Р С Т У Ф Х Ц Ч Ш Щ Ь Ы Ъ Э Ю Я");
            GtaPlayer.SendClientMessageToAll("Czech characters:");
            GtaPlayer.SendClientMessageToAll("ú ů ý ž ’ „ “ ” – — á č ď é ě í ň ó ř š ť Ú Ů Ý Ž Á Č Ď É Ě Í Ň Ó Ř Š Ť");

            return true;
        }
    }
}