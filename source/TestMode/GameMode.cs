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
using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;
using TestMode.Controllers;

namespace TestMode
{
    public class GameMode : BaseMode
    {
        public static TextDraw Test;

        protected override void LoadControllers(ControllerCollection controllers)
        {
            controllers.Remove<PlayerController>();
            controllers.Add(new MyPlayerContoller());
        }

        public override bool OnGameModeInit()
        {
            Console.WriteLine("OnGameModeInit");

            Color color1 = new Color(0xDD, 0xEE, 0xAA, 0xFF);
            int color1i = color1.GetColorValue(ColorFormat.RGBA);
            Console.WriteLine("(1): color: {0}, int: {1}", color1.ToString(ColorFormat.RGBA), color1i);
            var color2i = 0xFFEEDDCC;
            Color color2 = color2i;
            Console.Write("(2): uint: {0}, color: {1}", color2i, color2.ToString(ColorFormat.RGBA));

            AddPlayerClass(1, new Vector(0, 0, 5), 0);

            Test = new TextDraw(459.375000f, 78.166671f, "San Andreas", TextDrawFont.Diploma, Color.Red)
            {
                LetterWidth = 0.449999f,
                LetterHeight = 1.600000f,
                Width = 6.250000f,
                Height = 86.333374f,
                Shadow = 1,
                BackColor = Color.Black
            };

            return base.OnGameModeInit();
        }
    }
}