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
using SampSharp.GameMode.Definitions;

namespace SampSharp.GameMode.World
{
    /// <summary>
    ///     Contains vehicle category infos.
    /// </summary>
    public partial struct VehicleModelInfo
    {
        #region Fields

        private static readonly VehicleModelInfo[] VehicleModelInfos =
        {
            new VehicleModelInfo(400, "Landstalker", VehicleCategory.OffRoad, 4),
            new VehicleModelInfo(401, "Bravura", VehicleCategory.Saloon, 2),
            new VehicleModelInfo(402, "Buffalo", VehicleCategory.Sport, 2),
            new VehicleModelInfo(403, "Linerunner", VehicleCategory.Industrial, 2),
            new VehicleModelInfo(404, "Perenniel", VehicleCategory.Station, 4),
            new VehicleModelInfo(405, "Sentinel", VehicleCategory.Saloon, 4),
            new VehicleModelInfo(406, "Dumper", VehicleCategory.Unique, 1),
            new VehicleModelInfo(407, "Firetruck", VehicleCategory.PublicService, 2),
            new VehicleModelInfo(408, "Trashmaster", VehicleCategory.Industrial, 2),
            new VehicleModelInfo(409, "Stretch", VehicleCategory.Unique, 4),
            new VehicleModelInfo(410, "Manana", VehicleCategory.Saloon, 2),
            new VehicleModelInfo(411, "Infernus", VehicleCategory.Sport, 2),
            new VehicleModelInfo(412, "Voodoo", VehicleCategory.Lowrider, 2),
            new VehicleModelInfo(413, "Pony", VehicleCategory.Industrial, 4),
            new VehicleModelInfo(414, "Mule", VehicleCategory.Industrial, 2),
            new VehicleModelInfo(415, "Cheetah", VehicleCategory.Sport, 2),
            new VehicleModelInfo(416, "Ambulance", VehicleCategory.PublicService, 4),
            new VehicleModelInfo(417, "Leviathan", VehicleCategory.Helicopter, 2),
            new VehicleModelInfo(418, "Moonbeam", VehicleCategory.Station, 4),
            new VehicleModelInfo(419, "Esperanto", VehicleCategory.Saloon, 2),
            new VehicleModelInfo(420, "Taxi", VehicleCategory.PublicService, 4),
            new VehicleModelInfo(421, "Washington", VehicleCategory.Saloon, 4),
            new VehicleModelInfo(422, "Bobcat", VehicleCategory.Industrial, 2),
            new VehicleModelInfo(423, "Mr Whoopee", VehicleCategory.Unique, 2),
            new VehicleModelInfo(424, "BF Injection", VehicleCategory.OffRoad, 2),
            new VehicleModelInfo(425, "Hunter", VehicleCategory.Helicopter, 1),
            new VehicleModelInfo(426, "Premier", VehicleCategory.Saloon, 4),
            new VehicleModelInfo(427, "Enforcer", VehicleCategory.PublicService, 4),
            new VehicleModelInfo(428, "Securicar", VehicleCategory.Unique, 4),
            new VehicleModelInfo(429, "Banshee", VehicleCategory.Sport, 2),
            new VehicleModelInfo(430, "Predator", VehicleCategory.Boat, 1),
            new VehicleModelInfo(431, "Bus", VehicleCategory.PublicService, 9),
            new VehicleModelInfo(432, "Rhino", VehicleCategory.PublicService, 1),
            new VehicleModelInfo(433, "Barracks", VehicleCategory.PublicService, 2),
            new VehicleModelInfo(434, "Hotknife", VehicleCategory.Unique, 2),
            new VehicleModelInfo(435, "Article Trailer", VehicleCategory.Trailer, 0),
            new VehicleModelInfo(436, "Previon", VehicleCategory.Saloon, 2),
            new VehicleModelInfo(437, "Coach", VehicleCategory.PublicService, 9),
            new VehicleModelInfo(438, "Cabbie", VehicleCategory.PublicService, 4),
            new VehicleModelInfo(439, "Stallion", VehicleCategory.Convertible, 2),
            new VehicleModelInfo(440, "Rumpo", VehicleCategory.Industrial, 4),
            new VehicleModelInfo(441, "RC Bandit", VehicleCategory.RemoteControl, 1),
            new VehicleModelInfo(442, "Romero", VehicleCategory.Unique, 2),
            new VehicleModelInfo(443, "Packer", VehicleCategory.Industrial, 2),
            new VehicleModelInfo(444, "Monster", VehicleCategory.OffRoad, 2),
            new VehicleModelInfo(445, "Admiral", VehicleCategory.Saloon, 4),
            new VehicleModelInfo(446, "Squallo", VehicleCategory.Boat, 1),
            new VehicleModelInfo(447, "Seasparrow", VehicleCategory.Helicopter, 2),
            new VehicleModelInfo(448, "Pizzaboy", VehicleCategory.Bike, 1),
            new VehicleModelInfo(449, "Tram", VehicleCategory.Unique, 6),
            new VehicleModelInfo(450, "Article Trailer 2", VehicleCategory.Trailer, 0),
            new VehicleModelInfo(451, "Turismo", VehicleCategory.Sport, 2),
            new VehicleModelInfo(452, "Speeder", VehicleCategory.Boat, 1),
            new VehicleModelInfo(453, "Reefer", VehicleCategory.Boat, 1),
            new VehicleModelInfo(454, "Tropic", VehicleCategory.Boat, 1),
            new VehicleModelInfo(456, "Flatbed", VehicleCategory.Industrial, 2),
            new VehicleModelInfo(456, "Yankee", VehicleCategory.Industrial, 2),
            new VehicleModelInfo(457, "Caddy", VehicleCategory.Unique, 2),
            new VehicleModelInfo(458, "Solair", VehicleCategory.Station, 4),
            new VehicleModelInfo(459, "Topfun Van (Berkley's RC)", VehicleCategory.Industrial, 4),
            new VehicleModelInfo(460, "Skimmer", VehicleCategory.Airplane, 2),
            new VehicleModelInfo(461, "PCJ-600", VehicleCategory.Bike, 2),
            new VehicleModelInfo(462, "Faggio", VehicleCategory.Bike, 2),
            new VehicleModelInfo(463, "Freeway", VehicleCategory.Bike, 2),
            new VehicleModelInfo(464, "RC Baron", VehicleCategory.RemoteControl, 2),
            new VehicleModelInfo(465, "RC Raider", VehicleCategory.RemoteControl, 2),
            new VehicleModelInfo(466, "Glendale", VehicleCategory.Saloon, 4),
            new VehicleModelInfo(467, "Oceanic", VehicleCategory.Saloon, 4),
            new VehicleModelInfo(468, "Sanchez", VehicleCategory.Bike, 2),
            new VehicleModelInfo(469, "Sparrow", VehicleCategory.Helicopter, 2),
            new VehicleModelInfo(470, "Patriot", VehicleCategory.OffRoad, 4),
            new VehicleModelInfo(471, "Quad", VehicleCategory.Bike, 2),
            new VehicleModelInfo(472, "Coastguard", VehicleCategory.Boat, 1),
            new VehicleModelInfo(473, "Dinghy", VehicleCategory.Boat, 1),
            new VehicleModelInfo(474, "Hermes", VehicleCategory.Saloon, 2),
            new VehicleModelInfo(475, "Sabre", VehicleCategory.Sport, 2),
            new VehicleModelInfo(476, "Rustler", VehicleCategory.Airplane, 1),
            new VehicleModelInfo(477, "ZR-350", VehicleCategory.Sport, 2),
            new VehicleModelInfo(478, "Walton", VehicleCategory.Industrial, 2),
            new VehicleModelInfo(479, "Regina", VehicleCategory.Station, 4),
            new VehicleModelInfo(480, "Comet", VehicleCategory.Convertible, 2),
            new VehicleModelInfo(481, "BMX", VehicleCategory.Bike, 1),
            new VehicleModelInfo(482, "Burrito", VehicleCategory.Industrial, 4),
            new VehicleModelInfo(483, "Camper", VehicleCategory.Unique, 3),
            new VehicleModelInfo(484, "Marquis", VehicleCategory.Boat, 1),
            new VehicleModelInfo(485, "Baggage", VehicleCategory.Unique, 1),
            new VehicleModelInfo(486, "Dozer", VehicleCategory.Unique, 1),
            new VehicleModelInfo(487, "Maverick", VehicleCategory.Helicopter, 4),
            new VehicleModelInfo(488, "SAN News Maverick", VehicleCategory.Helicopter, 2),
            new VehicleModelInfo(489, "Rancher", VehicleCategory.OffRoad, 2),
            new VehicleModelInfo(490, "FBI Rancher", VehicleCategory.PublicService, 4),
            new VehicleModelInfo(491, "Virgo", VehicleCategory.Saloon, 2),
            new VehicleModelInfo(492, "Greenwood", VehicleCategory.Saloon, 4),
            new VehicleModelInfo(493, "Jetmax", VehicleCategory.Boat, 1),
            new VehicleModelInfo(494, "Hotring Racer", VehicleCategory.Sport, 2),
            new VehicleModelInfo(495, "Sandking", VehicleCategory.OffRoad, 2),
            new VehicleModelInfo(496, "Blista Compact", VehicleCategory.Sport, 2),
            new VehicleModelInfo(497, "Police Maverick", VehicleCategory.Helicopter, 4),
            new VehicleModelInfo(498, "Boxville", VehicleCategory.Industrial, 4),
            new VehicleModelInfo(499, "Benson", VehicleCategory.Industrial, 2),
            new VehicleModelInfo(500, "Mesa", VehicleCategory.OffRoad, 2),
            new VehicleModelInfo(501, "RC Goblin", VehicleCategory.RemoteControl, 2),
            new VehicleModelInfo(502, "Hotring Racer 2", VehicleCategory.Sport, 2),
            new VehicleModelInfo(503, "Hotring Racer 3", VehicleCategory.Sport, 2),
            new VehicleModelInfo(504, "Bloodring Banger", VehicleCategory.Saloon, 2),
            new VehicleModelInfo(505, "Rancher", VehicleCategory.OffRoad, 2),
            new VehicleModelInfo(506, "Super GT", VehicleCategory.Sport, 2),
            new VehicleModelInfo(507, "Elegant", VehicleCategory.Saloon, 4),
            new VehicleModelInfo(508, "Journey", VehicleCategory.Unique, 2),
            new VehicleModelInfo(509, "Bike", VehicleCategory.Bike, 1),
            new VehicleModelInfo(510, "Mountain Bike", VehicleCategory.Bike, 1),
            new VehicleModelInfo(511, "Beagle", VehicleCategory.Airplane, 2),
            new VehicleModelInfo(512, "Cropduster", VehicleCategory.Airplane, 1),
            new VehicleModelInfo(513, "Stuntplane", VehicleCategory.Airplane, 1),
            new VehicleModelInfo(514, "Tanker", VehicleCategory.Industrial, 2),
            new VehicleModelInfo(515, "Roadtrain", VehicleCategory.Industrial, 2),
            new VehicleModelInfo(516, "Nebula", VehicleCategory.Saloon, 4),
            new VehicleModelInfo(517, "Majestic", VehicleCategory.Saloon, 2),
            new VehicleModelInfo(518, "Buccaneer", VehicleCategory.Saloon, 2),
            new VehicleModelInfo(519, "Shamal", VehicleCategory.Airplane, 1),
            new VehicleModelInfo(520, "Hydra", VehicleCategory.Airplane, 1),
            new VehicleModelInfo(521, "FCR-900", VehicleCategory.Bike, 2),
            new VehicleModelInfo(522, "NRG-500", VehicleCategory.Bike, 2),
            new VehicleModelInfo(523, "HPV1000", VehicleCategory.PublicService, 2),
            new VehicleModelInfo(524, "Cement Truck", VehicleCategory.Industrial, 2),
            new VehicleModelInfo(525, "Towtruck", VehicleCategory.Unique, 2),
            new VehicleModelInfo(526, "Fortune", VehicleCategory.Saloon, 2),
            new VehicleModelInfo(527, "Cadrona", VehicleCategory.Saloon, 2),
            new VehicleModelInfo(528, "FBI Truck", VehicleCategory.PublicService, 2),
            new VehicleModelInfo(529, "Willard", VehicleCategory.Saloon, 4),
            new VehicleModelInfo(530, "Forklift", VehicleCategory.Unique, 1),
            new VehicleModelInfo(531, "Tractor", VehicleCategory.Industrial, 1),
            new VehicleModelInfo(532, "Combine Harvester", VehicleCategory.Unique, 1),
            new VehicleModelInfo(533, "Feltzer", VehicleCategory.Convertible, 2),
            new VehicleModelInfo(534, "Remington", VehicleCategory.Lowrider, 2),
            new VehicleModelInfo(535, "Slamvan", VehicleCategory.Lowrider, 2),
            new VehicleModelInfo(536, "Blade", VehicleCategory.Lowrider, 2),
            new VehicleModelInfo(537, "Freight (Train)", VehicleCategory.Unique, 6),
            new VehicleModelInfo(538, "Brownstreak (Train)", VehicleCategory.Unique, 6),
            new VehicleModelInfo(539, "Vortex", VehicleCategory.Unique, 1),
            new VehicleModelInfo(540, "Vincent", VehicleCategory.Saloon, 4),
            new VehicleModelInfo(541, "Bullet", VehicleCategory.Sport, 2),
            new VehicleModelInfo(542, "Clover", VehicleCategory.Saloon, 2),
            new VehicleModelInfo(543, "Sadler", VehicleCategory.Industrial, 2),
            new VehicleModelInfo(544, "Firetruck LA", VehicleCategory.PublicService, 2),
            new VehicleModelInfo(545, "Hustler", VehicleCategory.Unique, 2),
            new VehicleModelInfo(546, "Intruder", VehicleCategory.Saloon, 4),
            new VehicleModelInfo(547, "Primo", VehicleCategory.Saloon, 4),
            new VehicleModelInfo(548, "Cargobob", VehicleCategory.Helicopter, 2),
            new VehicleModelInfo(549, "Tampa", VehicleCategory.Saloon, 2),
            new VehicleModelInfo(550, "Sunrise", VehicleCategory.Saloon, 4),
            new VehicleModelInfo(551, "Merit", VehicleCategory.Saloon, 4),
            new VehicleModelInfo(552, "Utility Van", VehicleCategory.Industrial, 2),
            new VehicleModelInfo(553, "Nevada", VehicleCategory.Airplane, 1),
            new VehicleModelInfo(554, "Yosemite", VehicleCategory.Industrial, 2),
            new VehicleModelInfo(555, "Windsor", VehicleCategory.Convertible, 2),
            new VehicleModelInfo(556, "Monster \"A\"", VehicleCategory.OffRoad, 2),
            new VehicleModelInfo(557, "Monster \"B\"", VehicleCategory.OffRoad, 2),
            new VehicleModelInfo(558, "Uranus", VehicleCategory.Sport, 2),
            new VehicleModelInfo(559, "Jester", VehicleCategory.Sport, 2),
            new VehicleModelInfo(560, "Sultan", VehicleCategory.Saloon, 4),
            new VehicleModelInfo(561, "Stratum", VehicleCategory.Station, 4),
            new VehicleModelInfo(562, "Elegy", VehicleCategory.Saloon, 2),
            new VehicleModelInfo(563, "Raindance", VehicleCategory.Helicopter, 2),
            new VehicleModelInfo(564, "RC Tiger", VehicleCategory.RemoteControl, 1),
            new VehicleModelInfo(565, "Flash", VehicleCategory.Sport, 2),
            new VehicleModelInfo(566, "Tahoma", VehicleCategory.Lowrider, 4),
            new VehicleModelInfo(567, "Savanna", VehicleCategory.Lowrider, 4),
            new VehicleModelInfo(568, "Bandito", VehicleCategory.OffRoad, 1),
            new VehicleModelInfo(569, "Freight Flat Trailer (Train)", VehicleCategory.TrainTrailer, 0),
            new VehicleModelInfo(570, "Streak Trailer (Train)", VehicleCategory.TrainTrailer, 5),
            new VehicleModelInfo(571, "Kart", VehicleCategory.Unique, 1),
            new VehicleModelInfo(572, "Mower", VehicleCategory.Unique, 1),
            new VehicleModelInfo(573, "Dune", VehicleCategory.OffRoad, 2),
            new VehicleModelInfo(574, "Sweeper", VehicleCategory.Unique, 1),
            new VehicleModelInfo(575, "Broadway", VehicleCategory.Lowrider, 2),
            new VehicleModelInfo(576, "Tornado", VehicleCategory.Lowrider, 2),
            new VehicleModelInfo(577, "AT400", VehicleCategory.Airplane, 2),
            new VehicleModelInfo(578, "DFT-30", VehicleCategory.Industrial, 2),
            new VehicleModelInfo(579, "Huntley", VehicleCategory.OffRoad, 4),
            new VehicleModelInfo(580, "Stafford", VehicleCategory.Saloon, 4),
            new VehicleModelInfo(581, "BF-400", VehicleCategory.Bike, 2),
            new VehicleModelInfo(582, "Newsvan", VehicleCategory.Industrial, 4),
            new VehicleModelInfo(583, "Tug", VehicleCategory.Unique, 1),
            new VehicleModelInfo(584, "Petrol Trailer", VehicleCategory.Trailer, 0),
            new VehicleModelInfo(585, "Emperor", VehicleCategory.Saloon, 4),
            new VehicleModelInfo(586, "Wayfarer", VehicleCategory.Bike, 2),
            new VehicleModelInfo(587, "Euros", VehicleCategory.Sport, 2),
            new VehicleModelInfo(588, "Hotdog", VehicleCategory.Unique, 2),
            new VehicleModelInfo(589, "Club", VehicleCategory.Sport, 2),
            new VehicleModelInfo(590, "Freight Box Trailer (Train)", VehicleCategory.TrainTrailer, 0),
            new VehicleModelInfo(591, "Article Trailer 3", VehicleCategory.Trailer, 0),
            new VehicleModelInfo(592, "Andromada", VehicleCategory.Airplane, 2),
            new VehicleModelInfo(593, "Dodo", VehicleCategory.Airplane, 2),
            new VehicleModelInfo(594, "RC Cam", VehicleCategory.RemoteControl, 2),
            new VehicleModelInfo(595, "Launch", VehicleCategory.Boat, 1),
            new VehicleModelInfo(596, "Police Car (LSPD)", VehicleCategory.PublicService, 4),
            new VehicleModelInfo(597, "Police Car (SFPD)", VehicleCategory.PublicService, 4),
            new VehicleModelInfo(598, "Police Car (LVPD)", VehicleCategory.PublicService, 4),
            new VehicleModelInfo(599, "Police Ranger", VehicleCategory.PublicService, 2),
            new VehicleModelInfo(600, "Picador", VehicleCategory.Industrial, 2),
            new VehicleModelInfo(601, "S.W.A.T.", VehicleCategory.PublicService, 2),
            new VehicleModelInfo(602, "Alpha", VehicleCategory.Sport, 2),
            new VehicleModelInfo(603, "Phoenix", VehicleCategory.Sport, 2),
            new VehicleModelInfo(604, "Glendale Shit", VehicleCategory.Saloon, 4),
            new VehicleModelInfo(605, "Sadler Shit", VehicleCategory.Industrial, 2),
            new VehicleModelInfo(606, "Baggage Trailer \"A\"", VehicleCategory.Trailer, 0),
            new VehicleModelInfo(607, "Baggage Trailer \"B\"", VehicleCategory.Trailer, 0),
            new VehicleModelInfo(608, "Tug Stairs Trailer", VehicleCategory.Trailer, 0),
            new VehicleModelInfo(609, "Boxville 2", VehicleCategory.Industrial, 4),
            new VehicleModelInfo(610, "Farm Trailer", VehicleCategory.Trailer, 0),
            new VehicleModelInfo(611, "Utility Trailer", VehicleCategory.Trailer, 0)
        };

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="VehicleModelInfo" /> class.
        /// </summary>
        private VehicleModelInfo(int type, string name, VehicleCategory category, int seatsNumber)
            : this()
        {
            Type = (VehicleModelType)type;
            Name = name;
            Category = category;
            SeatsNumber = seatsNumber;
        }

        /// <summary>
        ///     Gets the type of this <see cref="VehicleModelInfo" />.
        /// </summary>
        public VehicleModelType Type { get; }

        /// <summary>
        ///     Gets the name of this <see cref="VehicleModelInfo" />.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Gets the category of this <see cref="VehicleModelInfo" />.
        /// </summary>
        public VehicleCategory Category { get; private set; }

        /// <summary>
        ///     Gets the seats number of this <see cref="VehicleModelInfo" />.
        /// </summary>
        public int SeatsNumber { get; private set; }

        /// <summary>
        ///     Gets model information of the given type.
        /// </summary>
        /// <param name="infotype">The type of information to retrieve.</param>
        public Vector3 this[VehicleModelInfoType infotype]
        {
            get
            {
                VehicleModelInfoInternal.Instance.GetVehicleModelInfo((int)Type, (int)infotype, out var x, out var y, out var z);
                return new Vector3(x, y, z);
            }
        }

        /// <summary>
        ///     Returns an instance of <see cref="VehicleModelInfo" /> containing information about the specified vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle to find information about.</param>
        /// <returns>An instance of <see cref="VehicleModelInfo" /> containing information about the specified vehicle.</returns>
        public static VehicleModelInfo ForVehicle(SampSharp.GameMode.World.BaseVehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            var model = (int)vehicle.Model;

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
            if ((int)model < 400 || (int)model > 611)
            {
                throw new ArgumentOutOfRangeException(nameof(model), "model is non-existant");
            }

            return VehicleModelInfos[(int)model - 400];
        }
    }
}
