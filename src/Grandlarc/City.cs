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

namespace Grandlarc
{
    public enum City
    {
        LosSantos,
        SanFierro,
        LasVenturas
    }

    public static class CityHelpers
    {
        public static City Next(this City current)
        {
            switch (current)
            {
                case City.LosSantos:
                    return City.SanFierro;
                case City.SanFierro:
                    return City.LasVenturas;
                case City.LasVenturas:
                    return City.LosSantos;
                default: // none case
                    return City.LosSantos;
            }
        }

        public static City Prev(this City current)
        {
            switch (current)
            {
                case City.LosSantos:
                    return City.LasVenturas;
                case City.SanFierro:
                    return City.LosSantos;
                case City.LasVenturas:
                    return City.SanFierro;
                default: // none case
                    return City.LasVenturas;
            }
        }
    }
}