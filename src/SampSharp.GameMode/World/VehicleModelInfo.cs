// SampSharp
// Copyright 2022 Tim Potze
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
using SampSharp.GameMode.Definitions;

// ReSharper disable StringLiteralTypo
namespace SampSharp.GameMode.World;

/// <summary>
///     Contains vehicle category infos.
/// </summary>
public readonly partial struct VehicleModelInfo
{
    private static readonly VehicleModelInfo[] VehicleModelInfos =
    {
        new(400, "Landstalker", VehicleCategory.OffRoad, 4),
        new(401, "Bravura", VehicleCategory.Saloon, 2),
        new(402, "Buffalo", VehicleCategory.Sport, 2),
        new(403, "Linerunner", VehicleCategory.Industrial, 2),
        new(404, "Perenniel", VehicleCategory.Station, 4),
        new(405, "Sentinel", VehicleCategory.Saloon, 4),
        new(406, "Dumper", VehicleCategory.Unique, 1),
        new(407, "Firetruck", VehicleCategory.PublicService, 2),
        new(408, "Trashmaster", VehicleCategory.Industrial, 2),
        new(409, "Stretch", VehicleCategory.Unique, 4),
        new(410, "Manana", VehicleCategory.Saloon, 2),
        new(411, "Infernus", VehicleCategory.Sport, 2),
        new(412, "Voodoo", VehicleCategory.Lowrider, 2),
        new(413, "Pony", VehicleCategory.Industrial, 4),
        new(414, "Mule", VehicleCategory.Industrial, 2),
        new(415, "Cheetah", VehicleCategory.Sport, 2),
        new(416, "Ambulance", VehicleCategory.PublicService, 4),
        new(417, "Leviathan", VehicleCategory.Helicopter, 2),
        new(418, "Moonbeam", VehicleCategory.Station, 4),
        new(419, "Esperanto", VehicleCategory.Saloon, 2),
        new(420, "Taxi", VehicleCategory.PublicService, 4),
        new(421, "Washington", VehicleCategory.Saloon, 4),
        new(422, "Bobcat", VehicleCategory.Industrial, 2),
        new(423, "Mr Whoopee", VehicleCategory.Unique, 2),
        new(424, "BF Injection", VehicleCategory.OffRoad, 2),
        new(425, "Hunter", VehicleCategory.Helicopter, 1),
        new(426, "Premier", VehicleCategory.Saloon, 4),
        new(427, "Enforcer", VehicleCategory.PublicService, 4),
        new(428, "Securicar", VehicleCategory.Unique, 4),
        new(429, "Banshee", VehicleCategory.Sport, 2),
        new(430, "Predator", VehicleCategory.Boat, 1),
        new(431, "Bus", VehicleCategory.PublicService, 9),
        new(432, "Rhino", VehicleCategory.PublicService, 1),
        new(433, "Barracks", VehicleCategory.PublicService, 2),
        new(434, "Hotknife", VehicleCategory.Unique, 2),
        new(435, "Article Trailer", VehicleCategory.Trailer, 0),
        new(436, "Previon", VehicleCategory.Saloon, 2),
        new(437, "Coach", VehicleCategory.PublicService, 9),
        new(438, "Cabbie", VehicleCategory.PublicService, 4),
        new(439, "Stallion", VehicleCategory.Convertible, 2),
        new(440, "Rumpo", VehicleCategory.Industrial, 4),
        new(441, "RC Bandit", VehicleCategory.RemoteControl, 1),
        new(442, "Romero", VehicleCategory.Unique, 2),
        new(443, "Packer", VehicleCategory.Industrial, 2),
        new(444, "Monster", VehicleCategory.OffRoad, 2),
        new(445, "Admiral", VehicleCategory.Saloon, 4),
        new(446, "Squallo", VehicleCategory.Boat, 1),
        new(447, "Seasparrow", VehicleCategory.Helicopter, 2),
        new(448, "Pizzaboy", VehicleCategory.Bike, 1),
        new(449, "Tram", VehicleCategory.Unique, 6),
        new(450, "Article Trailer 2", VehicleCategory.Trailer, 0),
        new(451, "Turismo", VehicleCategory.Sport, 2),
        new(452, "Speeder", VehicleCategory.Boat, 1),
        new(453, "Reefer", VehicleCategory.Boat, 1),
        new(454, "Tropic", VehicleCategory.Boat, 1),
        new(456, "Flatbed", VehicleCategory.Industrial, 2),
        new(456, "Yankee", VehicleCategory.Industrial, 2),
        new(457, "Caddy", VehicleCategory.Unique, 2),
        new(458, "Solair", VehicleCategory.Station, 4),
        new(459, "Topfun Van (Berkley's RC)", VehicleCategory.Industrial, 4),
        new(460, "Skimmer", VehicleCategory.Airplane, 2),
        new(461, "PCJ-600", VehicleCategory.Bike, 2),
        new(462, "Faggio", VehicleCategory.Bike, 2),
        new(463, "Freeway", VehicleCategory.Bike, 2),
        new(464, "RC Baron", VehicleCategory.RemoteControl, 2),
        new(465, "RC Raider", VehicleCategory.RemoteControl, 2),
        new(466, "Glendale", VehicleCategory.Saloon, 4),
        new(467, "Oceanic", VehicleCategory.Saloon, 4),
        new(468, "Sanchez", VehicleCategory.Bike, 2),
        new(469, "Sparrow", VehicleCategory.Helicopter, 2),
        new(470, "Patriot", VehicleCategory.OffRoad, 4),
        new(471, "Quad", VehicleCategory.Bike, 2),
        new(472, "Coastguard", VehicleCategory.Boat, 1),
        new(473, "Dinghy", VehicleCategory.Boat, 1),
        new(474, "Hermes", VehicleCategory.Saloon, 2),
        new(475, "Sabre", VehicleCategory.Sport, 2),
        new(476, "Rustler", VehicleCategory.Airplane, 1),
        new(477, "ZR-350", VehicleCategory.Sport, 2),
        new(478, "Walton", VehicleCategory.Industrial, 2),
        new(479, "Regina", VehicleCategory.Station, 4),
        new(480, "Comet", VehicleCategory.Convertible, 2),
        new(481, "BMX", VehicleCategory.Bike, 1),
        new(482, "Burrito", VehicleCategory.Industrial, 4),
        new(483, "Camper", VehicleCategory.Unique, 3),
        new(484, "Marquis", VehicleCategory.Boat, 1),
        new(485, "Baggage", VehicleCategory.Unique, 1),
        new(486, "Dozer", VehicleCategory.Unique, 1),
        new(487, "Maverick", VehicleCategory.Helicopter, 4),
        new(488, "SAN News Maverick", VehicleCategory.Helicopter, 2),
        new(489, "Rancher", VehicleCategory.OffRoad, 2),
        new(490, "FBI Rancher", VehicleCategory.PublicService, 4),
        new(491, "Virgo", VehicleCategory.Saloon, 2),
        new(492, "Greenwood", VehicleCategory.Saloon, 4),
        new(493, "Jetmax", VehicleCategory.Boat, 1),
        new(494, "Hotring Racer", VehicleCategory.Sport, 2),
        new(495, "Sandking", VehicleCategory.OffRoad, 2),
        new(496, "Blista Compact", VehicleCategory.Sport, 2),
        new(497, "Police Maverick", VehicleCategory.Helicopter, 4),
        new(498, "Boxville", VehicleCategory.Industrial, 4),
        new(499, "Benson", VehicleCategory.Industrial, 2),
        new(500, "Mesa", VehicleCategory.OffRoad, 2),
        new(501, "RC Goblin", VehicleCategory.RemoteControl, 2),
        new(502, "Hotring Racer 2", VehicleCategory.Sport, 2),
        new(503, "Hotring Racer 3", VehicleCategory.Sport, 2),
        new(504, "Bloodring Banger", VehicleCategory.Saloon, 2),
        new(505, "Rancher", VehicleCategory.OffRoad, 2),
        new(506, "Super GT", VehicleCategory.Sport, 2),
        new(507, "Elegant", VehicleCategory.Saloon, 4),
        new(508, "Journey", VehicleCategory.Unique, 2),
        new(509, "Bike", VehicleCategory.Bike, 1),
        new(510, "Mountain Bike", VehicleCategory.Bike, 1),
        new(511, "Beagle", VehicleCategory.Airplane, 2),
        new(512, "Cropduster", VehicleCategory.Airplane, 1),
        new(513, "Stuntplane", VehicleCategory.Airplane, 1),
        new(514, "Tanker", VehicleCategory.Industrial, 2),
        new(515, "Roadtrain", VehicleCategory.Industrial, 2),
        new(516, "Nebula", VehicleCategory.Saloon, 4),
        new(517, "Majestic", VehicleCategory.Saloon, 2),
        new(518, "Buccaneer", VehicleCategory.Saloon, 2),
        new(519, "Shamal", VehicleCategory.Airplane, 1),
        new(520, "Hydra", VehicleCategory.Airplane, 1),
        new(521, "FCR-900", VehicleCategory.Bike, 2),
        new(522, "NRG-500", VehicleCategory.Bike, 2),
        new(523, "HPV1000", VehicleCategory.PublicService, 2),
        new(524, "Cement Truck", VehicleCategory.Industrial, 2),
        new(525, "Towtruck", VehicleCategory.Unique, 2),
        new(526, "Fortune", VehicleCategory.Saloon, 2),
        new(527, "Cadrona", VehicleCategory.Saloon, 2),
        new(528, "FBI Truck", VehicleCategory.PublicService, 2),
        new(529, "Willard", VehicleCategory.Saloon, 4),
        new(530, "Forklift", VehicleCategory.Unique, 1),
        new(531, "Tractor", VehicleCategory.Industrial, 1),
        new(532, "Combine Harvester", VehicleCategory.Unique, 1),
        new(533, "Feltzer", VehicleCategory.Convertible, 2),
        new(534, "Remington", VehicleCategory.Lowrider, 2),
        new(535, "Slamvan", VehicleCategory.Lowrider, 2),
        new(536, "Blade", VehicleCategory.Lowrider, 2),
        new(537, "Freight (Train)", VehicleCategory.Unique, 6),
        new(538, "Brownstreak (Train)", VehicleCategory.Unique, 6),
        new(539, "Vortex", VehicleCategory.Unique, 1),
        new(540, "Vincent", VehicleCategory.Saloon, 4),
        new(541, "Bullet", VehicleCategory.Sport, 2),
        new(542, "Clover", VehicleCategory.Saloon, 2),
        new(543, "Sadler", VehicleCategory.Industrial, 2),
        new(544, "Firetruck LA", VehicleCategory.PublicService, 2),
        new(545, "Hustler", VehicleCategory.Unique, 2),
        new(546, "Intruder", VehicleCategory.Saloon, 4),
        new(547, "Primo", VehicleCategory.Saloon, 4),
        new(548, "Cargobob", VehicleCategory.Helicopter, 2),
        new(549, "Tampa", VehicleCategory.Saloon, 2),
        new(550, "Sunrise", VehicleCategory.Saloon, 4),
        new(551, "Merit", VehicleCategory.Saloon, 4),
        new(552, "Utility Van", VehicleCategory.Industrial, 2),
        new(553, "Nevada", VehicleCategory.Airplane, 1),
        new(554, "Yosemite", VehicleCategory.Industrial, 2),
        new(555, "Windsor", VehicleCategory.Convertible, 2),
        new(556, "Monster \"A\"", VehicleCategory.OffRoad, 2),
        new(557, "Monster \"B\"", VehicleCategory.OffRoad, 2),
        new(558, "Uranus", VehicleCategory.Sport, 2),
        new(559, "Jester", VehicleCategory.Sport, 2),
        new(560, "Sultan", VehicleCategory.Saloon, 4),
        new(561, "Stratum", VehicleCategory.Station, 4),
        new(562, "Elegy", VehicleCategory.Saloon, 2),
        new(563, "Raindance", VehicleCategory.Helicopter, 2),
        new(564, "RC Tiger", VehicleCategory.RemoteControl, 1),
        new(565, "Flash", VehicleCategory.Sport, 2),
        new(566, "Tahoma", VehicleCategory.Lowrider, 4),
        new(567, "Savanna", VehicleCategory.Lowrider, 4),
        new(568, "Bandito", VehicleCategory.OffRoad, 1),
        new(569, "Freight Flat Trailer (Train)", VehicleCategory.TrainTrailer, 0),
        new(570, "Streak Trailer (Train)", VehicleCategory.TrainTrailer, 5),
        new(571, "Kart", VehicleCategory.Unique, 1),
        new(572, "Mower", VehicleCategory.Unique, 1),
        new(573, "Dune", VehicleCategory.OffRoad, 2),
        new(574, "Sweeper", VehicleCategory.Unique, 1),
        new(575, "Broadway", VehicleCategory.Lowrider, 2),
        new(576, "Tornado", VehicleCategory.Lowrider, 2),
        new(577, "AT400", VehicleCategory.Airplane, 2),
        new(578, "DFT-30", VehicleCategory.Industrial, 2),
        new(579, "Huntley", VehicleCategory.OffRoad, 4),
        new(580, "Stafford", VehicleCategory.Saloon, 4),
        new(581, "BF-400", VehicleCategory.Bike, 2),
        new(582, "Newsvan", VehicleCategory.Industrial, 4),
        new(583, "Tug", VehicleCategory.Unique, 1),
        new(584, "Petrol Trailer", VehicleCategory.Trailer, 0),
        new(585, "Emperor", VehicleCategory.Saloon, 4),
        new(586, "Wayfarer", VehicleCategory.Bike, 2),
        new(587, "Euros", VehicleCategory.Sport, 2),
        new(588, "Hotdog", VehicleCategory.Unique, 2),
        new(589, "Club", VehicleCategory.Sport, 2),
        new(590, "Freight Box Trailer (Train)", VehicleCategory.TrainTrailer, 0),
        new(591, "Article Trailer 3", VehicleCategory.Trailer, 0),
        new(592, "Andromada", VehicleCategory.Airplane, 2),
        new(593, "Dodo", VehicleCategory.Airplane, 2),
        new(594, "RC Cam", VehicleCategory.RemoteControl, 2),
        new(595, "Launch", VehicleCategory.Boat, 1),
        new(596, "Police Car (LSPD)", VehicleCategory.PublicService, 4),
        new(597, "Police Car (SFPD)", VehicleCategory.PublicService, 4),
        new(598, "Police Car (LVPD)", VehicleCategory.PublicService, 4),
        new(599, "Police Ranger", VehicleCategory.PublicService, 2),
        new(600, "Picador", VehicleCategory.Industrial, 2),
        new(601, "S.W.A.T.", VehicleCategory.PublicService, 2),
        new(602, "Alpha", VehicleCategory.Sport, 2),
        new(603, "Phoenix", VehicleCategory.Sport, 2),
        new(604, "Glendale Shit", VehicleCategory.Saloon, 4),
        new(605, "Sadler Shit", VehicleCategory.Industrial, 2),
        new(606, "Baggage Trailer \"A\"", VehicleCategory.Trailer, 0),
        new(607, "Baggage Trailer \"B\"", VehicleCategory.Trailer, 0),
        new(608, "Tug Stairs Trailer", VehicleCategory.Trailer, 0),
        new(609, "Boxville 2", VehicleCategory.Industrial, 4),
        new(610, "Farm Trailer", VehicleCategory.Trailer, 0),
        new(611, "Utility Trailer", VehicleCategory.Trailer, 0)
    };

    /// <summary>
    ///     Initializes a new instance of the <see cref="VehicleModelInfo" /> class.
    /// </summary>
    private VehicleModelInfo(int type, string name, VehicleCategory category, int seatCount)
        : this()
    {
        Type = (VehicleModelType) type;
        Name = name;
        Category = category;
        SeatCount = seatCount;
    }

    /// <summary>
    ///     Gets the type of this <see cref="VehicleModelInfo" />.
    /// </summary>
    public VehicleModelType Type { get; }

    /// <summary>
    ///     Gets the name of this <see cref="VehicleModelInfo" />.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Gets the category of this <see cref="VehicleModelInfo" />.
    /// </summary>
    public VehicleCategory Category { get; }

    /// <summary>
    ///     Gets the seats number of this <see cref="VehicleModelInfo" />.
    /// </summary>
    public int SeatCount { get; }

    /// <summary>
    ///     Gets model information of the given type.
    /// </summary>
    /// <param name="infotype">The type of information to retrieve.</param>
    public Vector3 this[VehicleModelInfoType infotype]
    {
        get
        {
            VehicleModelInfoInternal.Instance.GetVehicleModelInfo((int) Type, (int) infotype, out var x, out var y, out var z);
            return new Vector3(x, y, z);
        }
    }

    /// <summary>
    ///     Returns an instance of <see cref="VehicleModelInfo" /> containing information about the specified vehicle.
    /// </summary>
    /// <param name="vehicle">The vehicle to find information about.</param>
    /// <returns>An instance of <see cref="VehicleModelInfo" /> containing information about the specified vehicle.</returns>
    public static VehicleModelInfo ForVehicle(BaseVehicle vehicle)
    {
        if (vehicle == null)
        {
            throw new ArgumentNullException(nameof(vehicle));
        }

        var model = (int) vehicle.Model;

        if (model < 400 || model > 611)
        {
            throw new ArgumentOutOfRangeException(nameof(vehicle), "vehicle's model is non-existant");
        }

        return VehicleModelInfos[model - 400];
    }

    /// <summary>
    ///     Returns an instance of <see cref="VehicleModelInfo" /> containing information about the given
    ///     <see cref="VehicleModelType" />.
    /// </summary>
    /// <param name="model">The <see cref="VehicleModelType" /> to find information about.</param>
    /// <returns>An instance of <see cref="VehicleModelInfo" /> containing information about the given VehicleModelType.</returns>
    public static VehicleModelInfo ForVehicle(VehicleModelType model)
    {
        if ((int) model < 400 || (int) model > 611)
        {
            throw new ArgumentOutOfRangeException(nameof(model), "model is non-existant");
        }

        return VehicleModelInfos[(int) model - 400];
    }
}