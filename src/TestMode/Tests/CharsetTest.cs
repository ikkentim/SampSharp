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
using System.Linq;
using System.Text;
using SampSharp.GameMode.Helpers;
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
            Console.WriteLine(": abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".Czech());
            Console.WriteLine(": ú ů ý ž ’ „ “ ” – — á č ď é ě í ň ó ř š ť Ú Ů Ý Ž Á Č Ď É Ě Í Ň Ó Ř Š Ť".Cyrillic().Czech());
            Console.WriteLine(": ú ů ý ž ’ „ “ ” – — á č ď é ě í ň ó ř š ť Ú Ů Ý Ž Á Č Ď É Ě Í Ň Ó Ř Š Ť".Czech());
            Console.WriteLine();
            Console.WriteLine(": Cyrillic characters:");
            Console.WriteLine(": abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".Cyrillic());
            Console.WriteLine(": АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЬЫЪЭЮЯ".Cyrillic());
            Console.WriteLine();

            Console.WriteLine("Czech conversions::");
            var czechFrom = "ú ů ý ž ’ „ “ ” – — á č ď é ě í ň ó ř š ť Ú Ů Ý Ž Á Č Ď É Ě Í Ň Ó Ř Š Ť".Split(' ');
            var czechToChars = new byte[]
            {
                250, 249, 0253, 0158, 0146, 0132, 0147, 0148, 0150, 0151, 0225, 0232, 0239, 0233, 0236, 0237, 0242, 0243,
                0248, 0154, 0157, 0218, 0217, 0221, 0142, 0193, 0200, 0207, 0201, 0204, 0205, 0210, 0211, 0216, 0138,
                0141
            }.Select(b => (char)b).ToArray();

            for (int i = 0; i < czechFrom.Length; i++)
            {
                Console.WriteLine("{0:X}({0}) => {1:X}({1}) ;; mod={2} ;; page={3}", (int)czechFrom[i][0], (int)czechToChars[i], ((int)czechFrom[i][0] % 256) - (int)czechToChars[i], (int)czechFrom[i][0] / 256);
            }

            Console.WriteLine();
            Console.WriteLine("Cyrillic conversions::");

            var cyrilicFrom = "А Б В Г Д Е Ё Ж З И Й К Л М Н О П Р С Т У Ф Х Ц Ч Ш Щ Ь Ы Ъ Э Ю Я".Split(' ');
            var cyrillicTo = "À Á Â Ã Ä Å ¨ Æ Ç È É Ê Ë Ì Í Î Ï Ð Ñ Ò Ó Ô Õ Ö × Ø Ù Ü Û Ú Ý Þ ß".Split(' ');

            for (int i = 0; i < cyrilicFrom.Length; i++)
            {
                Console.WriteLine("{0:X}({0}) => {1:X}({1}) ;; mod={2} ;; page={3}", (int)cyrilicFrom[i][0], (int)cyrillicTo[i][0], ((int)cyrilicFrom[i][0] % 256) - (int)cyrillicTo[i][0], (int)cyrilicFrom[i][0] / 256);   
            }

        }

        [Command("charset")]
        public static bool CharsetCommand(GtaPlayer player)
        {
            GtaPlayer.SendClientMessageToAll(Color.Teal, "this is a test: \u00D6");
            GtaPlayer.SendClientMessageToAll(Color.Teal, "this is a test: Ä ä Ö ö Ü ü ß ...");

            GtaPlayer.SendClientMessageToAll("Cyrillic characters:");
            GtaPlayer.SendClientMessageToAll("А Б В Г Д Е Ё Ж З И Й К Л М Н О П Р С Т У Ф Х Ц Ч Ш Щ Ь Ы Ъ Э Ю Я".Cyrillic());
            GtaPlayer.SendClientMessageToAll("ú ů ý ž ’ „ “ ” – — á č ď é ě í ň ó ř š ť Ú Ů Ý Ž Á Č Ď É Ě Í Ň Ó Ř Š Ť".Czech());

            return true;
        }
    }
}