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