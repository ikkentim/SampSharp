using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SampSharp.GameMode.Helpers
{
    public static class LanguageExtention
    {
        private static readonly Dictionary<string, string> _cyrillic = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> _czech = new Dictionary<string, string>();

        static LanguageExtention()
        {
            var cyrilicFrom = "А Б В Г Д Е Ё Ж З И Й К Л М Н О П Р С Т У Ф Х Ц Ч Ш Щ Ь Ы Ъ Э Ю Я".Split(' ');
            var cyrillicTo =  "À Á Â Ã Ä Å ¨ Æ Ç È É Ê Ë Ì Í Î Ï Ð Ñ Ò Ó Ô Õ Ö × Ø Ù Ü Û Ú Ý Þ ß".Split(' ');

            for (int i = 0; i < cyrilicFrom.Length; i++) _cyrillic.Add(cyrilicFrom[i], cyrillicTo[i]);

            var czechFrom = "ú ů ý ž ’ „ “ ” – — á č ď é ě í ň ó ř š ť Ú Ů Ý Ž Á Č Ď É Ě Í Ň Ó Ř Š Ť".Split(' ');
            var czechToChars = new byte[]
            {
                250, 249, 0253, 0158, 0146, 0132, 0147, 0148, 0150, 0151, 0225, 0232, 0239, 0233, 0236, 0237, 0242, 0243,
                0248, 0154, 0157, 0218, 0217, 0221, 0142, 0193, 0200, 0207, 0201, 0204, 0205, 0210, 0211, 0216, 0138,
                0141
            }.Select(b => (char) b).ToArray();

            for (int i = 0; i < czechFrom.Length; i++)
            {
                _czech.Add(czechFrom[i], czechToChars[i].ToString());
            }


        }

        public static string Czech(this string input)
        {
            foreach (var p in _czech)
            {
                if (p.Key == null)
                    Console.WriteLine("keywups!");
                if (p.Value == null)
                    Console.WriteLine("valwups!");
                if (input == null)
                    Console.WriteLine("inpwups!");
                input = input.Replace(p.Key, p.Value);
                
            }
            return input;
            return _czech.Aggregate(input, (current, p) =>
            {
                if (p.Key == null)
                    Console.WriteLine("keywups!");
                if (p.Value == null)
                    Console.WriteLine("valwups!");
                if (current == null)
                    Console.WriteLine("curwups!");
                return current.Replace(p.Key, p.Value);
            });
        }
        public static string Cyrillic(this string input)
        {
            return _cyrillic.Aggregate(input, (current, p) =>
            {
                if (p.Key == null)
                    Console.WriteLine("keywups!");
                if (p.Value == null)
                    Console.WriteLine("valwups!");
                if (current == null)
                    Console.WriteLine("curwups!");
                return current.Replace(p.Key, p.Value);
            });
        }
    }
}
