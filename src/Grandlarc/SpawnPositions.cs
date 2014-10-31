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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using SampSharp.GameMode.World;

namespace Grandlarc
{
    public class SpawnPositions
    {
        public static ReadOnlyDictionary<City, ReadOnlyCollection<WorldPosition>> Positions;

        static SpawnPositions()
        {
            var positions = new Dictionary<City, ReadOnlyCollection<WorldPosition>>();

            var lastVenturas = new List<WorldPosition>
            {
                new WorldPosition(new Vector(1435.8024, 2662.3647, 11.3926), 1.1650),
                new WorldPosition(new Vector(1457.4762, 2773.4868, 10.8203), 272.2754),
                new WorldPosition(new Vector(1739.6390, 2803.0569, 14.2735), 285.3929),
                new WorldPosition(new Vector(1870.3096, 2785.2471, 14.2734), 42.3102),
                new WorldPosition(new Vector(1959.7142, 2754.6863, 10.8203), 181.4731),
                new WorldPosition(new Vector(2314.2556, 2759.4504, 10.8203), 93.2711),
                new WorldPosition(new Vector(2216.5674, 2715.0334, 10.8130), 267.6540),
                new WorldPosition(new Vector(2101.4192, 2678.7874, 10.8130), 92.0607),
                new WorldPosition(new Vector(1951.1090, 2660.3877, 10.8203), 180.8461),
                new WorldPosition(new Vector(1666.6949, 2604.9861, 10.8203), 179.8495),
                new WorldPosition(new Vector(2808.3367, 2421.5107, 11.0625), 136.2060),
                new WorldPosition(new Vector(2633.3203, 2349.7061, 10.6719), 178.7175),
                new WorldPosition(new Vector(2606.6348, 2161.7490, 10.8203), 88.7508),
                new WorldPosition(new Vector(2616.5286, 2100.6226, 10.8158), 177.7834),
                new WorldPosition(new Vector(2491.8816, 2397.9370, 10.8203), 266.6003),
                new WorldPosition(new Vector(2531.7891, 2530.3223, 21.8750), 91.6686),
                new WorldPosition(new Vector(2340.6677, 2530.4324, 10.8203), 177.8630),
                new WorldPosition(new Vector(2097.6855, 2491.3313, 14.8390), 181.8117),
                new WorldPosition(new Vector(1893.1000, 2423.2412, 11.1782), 269.4385),
                new WorldPosition(new Vector(1698.9330, 2241.8320, 10.8203), 357.8584),
                new WorldPosition(new Vector(1479.4559, 2249.0769, 11.0234), 306.3790),
                new WorldPosition(new Vector(1298.1548, 2083.4016, 10.8127), 256.7034),
                new WorldPosition(new Vector(1117.8785, 2304.1514, 10.8203), 81.5490),
                new WorldPosition(new Vector(1108.9878, 1705.8639, 10.8203), 0.6785),
                new WorldPosition(new Vector(1423.9780, 1034.4188, 10.8203), 90.9590),
                new WorldPosition(new Vector(1537.4377, 752.0641, 11.0234), 271.6893),
                new WorldPosition(new Vector(1917.9590, 702.6984, 11.1328), 359.2682),
                new WorldPosition(new Vector(2089.4785, 658.0414, 11.2707), 357.3572),
                new WorldPosition(new Vector(2489.8286, 928.3251, 10.8280), 67.2245),
                new WorldPosition(new Vector(2697.4717, 856.4916, 9.8360), 267.0983),
                new WorldPosition(new Vector(2845.6104, 1288.1444, 11.3906), 3.6506),
                new WorldPosition(new Vector(2437.9370, 1293.1442, 10.8203), 86.3830),
                new WorldPosition(new Vector(2299.5430, 1451.4177, 10.8203), 269.1287),
                new WorldPosition(new Vector(2214.3008, 2041.9165, 10.8203), 268.7626),
                new WorldPosition(new Vector(2005.9174, 2152.0835, 10.8203), 270.1372),
                new WorldPosition(new Vector(2222.1042, 1837.4220, 10.8203), 88.6461),
                new WorldPosition(new Vector(2025.6753, 1916.4363, 12.3382), 272.5852),
                new WorldPosition(new Vector(2087.9902, 1516.5336, 10.8203), 48.9300),
                new WorldPosition(new Vector(2172.1624, 1398.7496, 11.0625), 91.3783),
                new WorldPosition(new Vector(2139.1841, 987.7975, 10.8203), 0.2315),
                new WorldPosition(new Vector(1860.9672, 1030.2910, 10.8203), 271.6988),
                new WorldPosition(new Vector(1673.2345, 1316.1067, 10.8203), 177.7294),
                new WorldPosition(new Vector(1412.6187, 2000.0596, 14.7396), 271.3568)
            };

            positions.Add(City.LasVenturas, lastVenturas.AsReadOnly());

            var losSantos = new List<WorldPosition>
            {
                new WorldPosition(new Vector(1751.1097, -2106.4529, 13.5469), 183.1979),
                new WorldPosition(new Vector(2652.6418, -1989.9175, 13.9988), 182.7107),
                new WorldPosition(new Vector(2489.5225, -1957.9258, 13.5881), 2.3440),
                new WorldPosition(new Vector(2689.5203, -1695.9354, 10.0517), 39.5312),
                new WorldPosition(new Vector(2770.5393, -1628.3069, 12.1775), 4.9637),
                new WorldPosition(new Vector(2807.9282, -1176.8883, 25.3805), 173.6018),
                new WorldPosition(new Vector(2552.5417, -958.0850, 82.6345), 280.2542),
                new WorldPosition(new Vector(2232.1309, -1159.5679, 25.8906), 103.2939),
                new WorldPosition(new Vector(2388.1003, -1279.8933, 25.1291), 94.3321),
                new WorldPosition(new Vector(2481.1885, -1536.7186, 24.1467), 273.4944),
                new WorldPosition(new Vector(2495.0720, -1687.5278, 13.5150), 359.6696),
                new WorldPosition(new Vector(2306.8252, -1675.4340, 13.9221), 2.6271),
                new WorldPosition(new Vector(2191.8403, -1455.8251, 25.5391), 267.9925),
                new WorldPosition(new Vector(1830.1359, -1092.1849, 23.8656), 94.0113),
                new WorldPosition(new Vector(2015.3630, -1717.2535, 13.5547), 93.3655),
                new WorldPosition(new Vector(1654.7091, -1656.8516, 22.5156), 177.9729),
                new WorldPosition(new Vector(1219.0851, -1812.8058, 16.5938), 190.0045),
                new WorldPosition(new Vector(1508.6849, -1059.0846, 25.0625), 1.8058),
                new WorldPosition(new Vector(1421.0819, -885.3383, 50.6531), 3.6516),
                new WorldPosition(new Vector(1133.8237, -1272.1558, 13.5469), 192.4113),
                new WorldPosition(new Vector(1235.2196, -1608.6111, 13.5469), 181.2655),
                new WorldPosition(new Vector(590.4648, -1252.2269, 18.2116), 25.0473),
                new WorldPosition(new Vector(842.5260, -1007.7679, 28.4185), 213.9953),
                new WorldPosition(new Vector(911.9332, -1232.6490, 16.9766), 5.2999),
                new WorldPosition(new Vector(477.6021, -1496.6207, 20.4345), 266.9252),
                new WorldPosition(new Vector(255.4621, -1366.3256, 53.1094), 312.0852),
                new WorldPosition(new Vector(281.5446, -1261.4562, 73.9319), 305.0017),
                new WorldPosition(new Vector(790.1918, -839.8533, 60.6328), 191.9514),
                new WorldPosition(new Vector(1299.1859, -801.4249, 84.1406), 269.5274),
                new WorldPosition(new Vector(1240.3170, -2036.6886, 59.9575), 276.4659),
                new WorldPosition(new Vector(2215.5181, -2627.8174, 13.5469), 273.7786),
                new WorldPosition(new Vector(2509.4346, -2637.6543, 13.6453), 358.3565),
            };

            positions.Add(City.LosSantos, losSantos.AsReadOnly());

            var sanFierro = new List<WorldPosition>
            {
                new WorldPosition(new Vector(-2723.4639, -314.8138, 7.1839), 43.5562),
                new WorldPosition(new Vector(-2694.5344, 64.5550, 4.3359), 95.0190),
                new WorldPosition(new Vector(-2458.2000, 134.5419, 35.1719), 303.9446),
                new WorldPosition(new Vector(-2796.6589, 219.5733, 7.1875), 88.8288),
                new WorldPosition(new Vector(-2706.5261, 397.7129, 4.3672), 179.8611),
                new WorldPosition(new Vector(-2866.7683, 691.9363, 23.4989), 286.3060),
                new WorldPosition(new Vector(-2764.9543, 785.6434, 52.7813), 357.6817),
                new WorldPosition(new Vector(-2660.9402, 883.2115, 79.7738), 357.4440),
                new WorldPosition(new Vector(-2861.0796, 1047.7109, 33.6068), 188.2750),
                new WorldPosition(new Vector(-2629.2009, 1383.1367, 7.1833), 179.7006),
                new WorldPosition(new Vector(-2079.6802, 1430.0189, 7.1016), 177.6486),
                new WorldPosition(new Vector(-1660.2294, 1382.6698, 9.8047), 136.2952),
                new WorldPosition(new Vector(-1674.1964, 430.3246, 7.1797), 226.1357),
                new WorldPosition(new Vector(-1954.9982, 141.8080, 27.1747), 277.7342),
                new WorldPosition(new Vector(-1956.1447, 287.1091, 35.4688), 90.4465),
                new WorldPosition(new Vector(-1888.1117, 615.7245, 35.1719), 128.4498),
                new WorldPosition(new Vector(-1922.5566, 886.8939, 35.3359), 272.1293),
                new WorldPosition(new Vector(-1983.3458, 1117.0645, 53.1243), 271.2390),
                new WorldPosition(new Vector(-2417.6458, 970.1491, 45.2969), 269.3676),
                new WorldPosition(new Vector(-2108.0171, 902.8030, 76.5792), 5.7139),
                new WorldPosition(new Vector(-2097.5664, 658.0771, 52.3672), 270.4487),
                new WorldPosition(new Vector(-2263.6650, 393.7423, 34.7708), 136.4152),
                new WorldPosition(new Vector(-2287.5027, 149.1875, 35.3125), 266.3989),
                new WorldPosition(new Vector(-2039.3571, -97.7205, 35.1641), 7.4744),
                new WorldPosition(new Vector(-1867.5022, -141.9203, 11.8984), 22.4499),
                new WorldPosition(new Vector(-1537.8992, 116.0441, 17.3226), 120.8537),
                new WorldPosition(new Vector(-1708.4763, 7.0187, 3.5489), 319.3260),
                new WorldPosition(new Vector(-1427.0858, -288.9430, 14.1484), 137.0812),
                new WorldPosition(new Vector(-2173.0654, -392.7444, 35.3359), 237.0159),
                new WorldPosition(new Vector(-2320.5286, -180.3870, 35.3135), 179.6980),
                new WorldPosition(new Vector(-2930.0049, 487.2518, 4.9141), 3.8258),
            };

            positions.Add(City.SanFierro, sanFierro.AsReadOnly());

            Positions = new ReadOnlyDictionary<City, ReadOnlyCollection<WorldPosition>>(positions);
        }

        public struct WorldPosition
        {
            public Vector Position;
            public float Rotation;

            public WorldPosition(Vector position, float rotation)
            {
                Position = position;
                Rotation = rotation;
            }

            public WorldPosition(Vector position, double rotation) : this(position, (float) rotation)
            {
            }
        }
    }
}