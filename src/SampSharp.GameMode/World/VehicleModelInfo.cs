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
            new VehicleModelInfo(400, "Landstalker", VehicleCategory.OffRoad),
            new VehicleModelInfo(401, "Bravura", VehicleCategory.Saloon),
            new VehicleModelInfo(402, "Buffalo", VehicleCategory.Sport),
            new VehicleModelInfo(403, "Linerunner", VehicleCategory.Industrial),
            new VehicleModelInfo(404, "Perenniel", VehicleCategory.Station),
            new VehicleModelInfo(405, "Sentinel", VehicleCategory.Saloon),
            new VehicleModelInfo(406, "Dumper", VehicleCategory.Unique),
            new VehicleModelInfo(407, "Firetruck", VehicleCategory.PublicService),
            new VehicleModelInfo(408, "Trashmaster", VehicleCategory.Industrial),
            new VehicleModelInfo(409, "Stretch", VehicleCategory.Unique),
            new VehicleModelInfo(410, "Manana", VehicleCategory.Saloon),
            new VehicleModelInfo(411, "Infernus", VehicleCategory.Sport),
            new VehicleModelInfo(412, "Voodoo", VehicleCategory.Lowrider),
            new VehicleModelInfo(413, "Pony", VehicleCategory.Industrial),
            new VehicleModelInfo(414, "Mule", VehicleCategory.Industrial),
            new VehicleModelInfo(415, "Cheetah", VehicleCategory.Sport),
            new VehicleModelInfo(416, "Ambulance", VehicleCategory.PublicService),
            new VehicleModelInfo(417, "Leviathan", VehicleCategory.Helicopter),
            new VehicleModelInfo(418, "Moonbeam", VehicleCategory.Station),
            new VehicleModelInfo(419, "Esperanto", VehicleCategory.Saloon),
            new VehicleModelInfo(420, "Taxi", VehicleCategory.PublicService),
            new VehicleModelInfo(421, "Washington", VehicleCategory.Saloon),
            new VehicleModelInfo(422, "Bobcat", VehicleCategory.Industrial),
            new VehicleModelInfo(423, "Mr Whoopee", VehicleCategory.Unique),
            new VehicleModelInfo(424, "BF Injection", VehicleCategory.OffRoad),
            new VehicleModelInfo(425, "Hunter", VehicleCategory.Helicopter),
            new VehicleModelInfo(426, "Premier", VehicleCategory.Saloon),
            new VehicleModelInfo(427, "Enforcer", VehicleCategory.PublicService),
            new VehicleModelInfo(428, "Securicar", VehicleCategory.Unique),
            new VehicleModelInfo(429, "Banshee", VehicleCategory.Sport),
            new VehicleModelInfo(430, "Predator", VehicleCategory.Boat),
            new VehicleModelInfo(431, "Bus", VehicleCategory.PublicService),
            new VehicleModelInfo(432, "Rhino", VehicleCategory.PublicService),
            new VehicleModelInfo(433, "Barracks", VehicleCategory.PublicService),
            new VehicleModelInfo(434, "Hotknife", VehicleCategory.Unique),
            new VehicleModelInfo(435, "Article Trailer", VehicleCategory.Trailer),
            new VehicleModelInfo(436, "Previon", VehicleCategory.Saloon),
            new VehicleModelInfo(437, "Coach", VehicleCategory.PublicService),
            new VehicleModelInfo(438, "Cabbie", VehicleCategory.PublicService),
            new VehicleModelInfo(439, "Stallion", VehicleCategory.Convertible),
            new VehicleModelInfo(440, "Rumpo", VehicleCategory.Industrial),
            new VehicleModelInfo(441, "RC Bandit", VehicleCategory.RemoteControl),
            new VehicleModelInfo(442, "Romero", VehicleCategory.Unique),
            new VehicleModelInfo(443, "Packer", VehicleCategory.Industrial),
            new VehicleModelInfo(444, "Monster", VehicleCategory.OffRoad),
            new VehicleModelInfo(445, "Admiral", VehicleCategory.Saloon),
            new VehicleModelInfo(446, "Squallo", VehicleCategory.Boat),
            new VehicleModelInfo(447, "Seasparrow", VehicleCategory.Helicopter),
            new VehicleModelInfo(448, "Pizzaboy", VehicleCategory.Bike),
            new VehicleModelInfo(449, "Tram", VehicleCategory.Unique),
            new VehicleModelInfo(450, "Article Trailer 2", VehicleCategory.Trailer),
            new VehicleModelInfo(451, "Turismo", VehicleCategory.Sport),
            new VehicleModelInfo(452, "Speeder", VehicleCategory.Boat),
            new VehicleModelInfo(453, "Reefer", VehicleCategory.Boat),
            new VehicleModelInfo(454, "Tropic", VehicleCategory.Boat),
            new VehicleModelInfo(456, "Flatbed", VehicleCategory.Industrial),
            new VehicleModelInfo(456, "Yankee", VehicleCategory.Industrial),
            new VehicleModelInfo(457, "Caddy", VehicleCategory.Unique),
            new VehicleModelInfo(458, "Solair", VehicleCategory.Station),
            new VehicleModelInfo(459, "Topfun Van (Berkley's RC)", VehicleCategory.Industrial),
            new VehicleModelInfo(460, "Skimmer", VehicleCategory.Airplane),
            new VehicleModelInfo(461, "PCJ-600", VehicleCategory.Bike),
            new VehicleModelInfo(462, "Faggio", VehicleCategory.Bike),
            new VehicleModelInfo(463, "Freeway", VehicleCategory.Bike),
            new VehicleModelInfo(464, "RC Baron", VehicleCategory.RemoteControl),
            new VehicleModelInfo(465, "RC Raider", VehicleCategory.RemoteControl),
            new VehicleModelInfo(466, "Glendale", VehicleCategory.Saloon),
            new VehicleModelInfo(467, "Oceanic", VehicleCategory.Saloon),
            new VehicleModelInfo(468, "Sanchez", VehicleCategory.Bike),
            new VehicleModelInfo(469, "Sparrow", VehicleCategory.Helicopter),
            new VehicleModelInfo(470, "Patriot", VehicleCategory.OffRoad),
            new VehicleModelInfo(471, "Quad", VehicleCategory.Bike),
            new VehicleModelInfo(472, "Coastguard", VehicleCategory.Boat),
            new VehicleModelInfo(473, "Dinghy", VehicleCategory.Boat),
            new VehicleModelInfo(474, "Hermes", VehicleCategory.Saloon),
            new VehicleModelInfo(475, "Sabre", VehicleCategory.Sport),
            new VehicleModelInfo(476, "Rustler", VehicleCategory.Airplane),
            new VehicleModelInfo(477, "ZR-350", VehicleCategory.Sport),
            new VehicleModelInfo(478, "Walton", VehicleCategory.Industrial),
            new VehicleModelInfo(479, "Regina", VehicleCategory.Station),
            new VehicleModelInfo(480, "Comet", VehicleCategory.Convertible),
            new VehicleModelInfo(481, "BMX", VehicleCategory.Bike),
            new VehicleModelInfo(482, "Burrito", VehicleCategory.Industrial),
            new VehicleModelInfo(483, "Camper", VehicleCategory.Unique),
            new VehicleModelInfo(484, "Marquis", VehicleCategory.Boat),
            new VehicleModelInfo(485, "Baggage", VehicleCategory.Unique),
            new VehicleModelInfo(486, "Dozer", VehicleCategory.Unique),
            new VehicleModelInfo(487, "Maverick", VehicleCategory.Helicopter),
            new VehicleModelInfo(488, "SAN News Maverick", VehicleCategory.Helicopter),
            new VehicleModelInfo(489, "Rancher", VehicleCategory.OffRoad),
            new VehicleModelInfo(490, "FBI Rancher", VehicleCategory.PublicService),
            new VehicleModelInfo(491, "Virgo", VehicleCategory.Saloon),
            new VehicleModelInfo(492, "Greenwood", VehicleCategory.Saloon),
            new VehicleModelInfo(493, "Jetmax", VehicleCategory.Boat),
            new VehicleModelInfo(494, "Hotring Racer", VehicleCategory.Sport),
            new VehicleModelInfo(495, "Sandking", VehicleCategory.OffRoad),
            new VehicleModelInfo(496, "Blista Compact", VehicleCategory.Sport),
            new VehicleModelInfo(497, "Police Maverick", VehicleCategory.Helicopter),
            new VehicleModelInfo(498, "Boxville", VehicleCategory.Industrial),
            new VehicleModelInfo(499, "Benson", VehicleCategory.Industrial),
            new VehicleModelInfo(500, "Mesa", VehicleCategory.OffRoad),
            new VehicleModelInfo(501, "RC Goblin", VehicleCategory.RemoteControl),
            new VehicleModelInfo(502, "Hotring Racer 2", VehicleCategory.Sport),
            new VehicleModelInfo(503, "Hotring Racer 3", VehicleCategory.Sport),
            new VehicleModelInfo(504, "Bloodring Banger", VehicleCategory.Saloon),
            new VehicleModelInfo(505, "Rancher", VehicleCategory.OffRoad),
            new VehicleModelInfo(506, "Super GT", VehicleCategory.Sport),
            new VehicleModelInfo(507, "Elegant", VehicleCategory.Saloon),
            new VehicleModelInfo(508, "Journey", VehicleCategory.Unique),
            new VehicleModelInfo(509, "Bike", VehicleCategory.Bike),
            new VehicleModelInfo(510, "Mountain Bike", VehicleCategory.Bike),
            new VehicleModelInfo(511, "Beagle", VehicleCategory.Airplane),
            new VehicleModelInfo(512, "Cropduster", VehicleCategory.Airplane),
            new VehicleModelInfo(513, "Stuntplane", VehicleCategory.Airplane),
            new VehicleModelInfo(514, "Tanker", VehicleCategory.Industrial),
            new VehicleModelInfo(515, "Roadtrain", VehicleCategory.Industrial),
            new VehicleModelInfo(516, "Nebula", VehicleCategory.Saloon),
            new VehicleModelInfo(517, "Majestic", VehicleCategory.Saloon),
            new VehicleModelInfo(518, "Buccaneer", VehicleCategory.Saloon),
            new VehicleModelInfo(519, "Shamal", VehicleCategory.Airplane),
            new VehicleModelInfo(520, "Hydra", VehicleCategory.Airplane),
            new VehicleModelInfo(521, "FCR-900", VehicleCategory.Bike),
            new VehicleModelInfo(522, "NRG-500", VehicleCategory.Bike),
            new VehicleModelInfo(523, "HPV1000", VehicleCategory.PublicService),
            new VehicleModelInfo(524, "Cement Truck", VehicleCategory.Industrial),
            new VehicleModelInfo(525, "Towtruck", VehicleCategory.Unique),
            new VehicleModelInfo(526, "Fortune", VehicleCategory.Saloon),
            new VehicleModelInfo(527, "Cadrona", VehicleCategory.Saloon),
            new VehicleModelInfo(528, "FBI Truck", VehicleCategory.PublicService),
            new VehicleModelInfo(529, "Willard", VehicleCategory.Saloon),
            new VehicleModelInfo(530, "Forklift", VehicleCategory.Unique),
            new VehicleModelInfo(531, "Tractor", VehicleCategory.Industrial),
            new VehicleModelInfo(532, "Combine Harvester", VehicleCategory.Unique),
            new VehicleModelInfo(533, "Feltzer", VehicleCategory.Convertible),
            new VehicleModelInfo(534, "Remington", VehicleCategory.Lowrider),
            new VehicleModelInfo(535, "Slamvan", VehicleCategory.Lowrider),
            new VehicleModelInfo(536, "Blade", VehicleCategory.Lowrider),
            new VehicleModelInfo(537, "Freight (Train)", VehicleCategory.Unique),
            new VehicleModelInfo(538, "Brownstreak (Train)", VehicleCategory.Unique),
            new VehicleModelInfo(539, "Vortex", VehicleCategory.Unique),
            new VehicleModelInfo(540, "Vincent", VehicleCategory.Saloon),
            new VehicleModelInfo(541, "Bullet", VehicleCategory.Sport),
            new VehicleModelInfo(542, "Clover", VehicleCategory.Saloon),
            new VehicleModelInfo(543, "Sadler", VehicleCategory.Industrial),
            new VehicleModelInfo(544, "Firetruck LA", VehicleCategory.PublicService),
            new VehicleModelInfo(545, "Hustler", VehicleCategory.Unique),
            new VehicleModelInfo(546, "Intruder", VehicleCategory.Saloon),
            new VehicleModelInfo(547, "Primo", VehicleCategory.Saloon),
            new VehicleModelInfo(548, "Cargobob", VehicleCategory.Helicopter),
            new VehicleModelInfo(549, "Tampa", VehicleCategory.Saloon),
            new VehicleModelInfo(550, "Sunrise", VehicleCategory.Saloon),
            new VehicleModelInfo(551, "Merit", VehicleCategory.Saloon),
            new VehicleModelInfo(552, "Utility Van", VehicleCategory.Industrial),
            new VehicleModelInfo(553, "Nevada", VehicleCategory.Airplane),
            new VehicleModelInfo(554, "Yosemite", VehicleCategory.Industrial),
            new VehicleModelInfo(555, "Windsor", VehicleCategory.Convertible),
            new VehicleModelInfo(556, "Monster \"A\"", VehicleCategory.OffRoad),
            new VehicleModelInfo(557, "Monster \"B\"", VehicleCategory.OffRoad),
            new VehicleModelInfo(558, "Uranus", VehicleCategory.Sport),
            new VehicleModelInfo(559, "Jester", VehicleCategory.Sport),
            new VehicleModelInfo(560, "Sultan", VehicleCategory.Saloon),
            new VehicleModelInfo(561, "Stratum", VehicleCategory.Station),
            new VehicleModelInfo(562, "Elegy", VehicleCategory.Saloon),
            new VehicleModelInfo(563, "Raindance", VehicleCategory.Helicopter),
            new VehicleModelInfo(564, "RC Tiger", VehicleCategory.RemoteControl),
            new VehicleModelInfo(565, "Flash", VehicleCategory.Sport),
            new VehicleModelInfo(566, "Tahoma", VehicleCategory.Lowrider),
            new VehicleModelInfo(567, "Savanna", VehicleCategory.Lowrider),
            new VehicleModelInfo(568, "Bandito", VehicleCategory.OffRoad),
            new VehicleModelInfo(569, "Freight Flat Trailer (Train)", VehicleCategory.TrainTrailer),
            new VehicleModelInfo(570, "Streak Trailer (Train)", VehicleCategory.TrainTrailer),
            new VehicleModelInfo(571, "Kart", VehicleCategory.Unique),
            new VehicleModelInfo(572, "Mower", VehicleCategory.Unique),
            new VehicleModelInfo(573, "Dune", VehicleCategory.OffRoad),
            new VehicleModelInfo(574, "Sweeper", VehicleCategory.Unique),
            new VehicleModelInfo(575, "Broadway", VehicleCategory.Lowrider),
            new VehicleModelInfo(576, "Tornado", VehicleCategory.Lowrider),
            new VehicleModelInfo(577, "AT400", VehicleCategory.Airplane),
            new VehicleModelInfo(578, "DFT-30", VehicleCategory.Industrial),
            new VehicleModelInfo(579, "Huntley", VehicleCategory.OffRoad),
            new VehicleModelInfo(580, "Stafford", VehicleCategory.Saloon),
            new VehicleModelInfo(581, "BF-400", VehicleCategory.Bike),
            new VehicleModelInfo(582, "Newsvan", VehicleCategory.Industrial),
            new VehicleModelInfo(583, "Tug", VehicleCategory.Unique),
            new VehicleModelInfo(584, "Petrol Trailer", VehicleCategory.Trailer),
            new VehicleModelInfo(585, "Emperor", VehicleCategory.Saloon),
            new VehicleModelInfo(586, "Wayfarer", VehicleCategory.Bike),
            new VehicleModelInfo(587, "Euros", VehicleCategory.Sport),
            new VehicleModelInfo(588, "Hotdog", VehicleCategory.Unique),
            new VehicleModelInfo(589, "Club", VehicleCategory.Sport),
            new VehicleModelInfo(590, "Freight Box Trailer (Train)", VehicleCategory.TrainTrailer),
            new VehicleModelInfo(591, "Article Trailer 3", VehicleCategory.Trailer),
            new VehicleModelInfo(592, "Andromada", VehicleCategory.Airplane),
            new VehicleModelInfo(593, "Dodo", VehicleCategory.Airplane),
            new VehicleModelInfo(594, "RC Cam", VehicleCategory.RemoteControl),
            new VehicleModelInfo(595, "Launch", VehicleCategory.Boat),
            new VehicleModelInfo(596, "Police Car (LSPD)", VehicleCategory.PublicService),
            new VehicleModelInfo(597, "Police Car (SFPD)", VehicleCategory.PublicService),
            new VehicleModelInfo(598, "Police Car (LVPD)", VehicleCategory.PublicService),
            new VehicleModelInfo(599, "Police Ranger", VehicleCategory.PublicService),
            new VehicleModelInfo(600, "Picador", VehicleCategory.Industrial),
            new VehicleModelInfo(601, "S.W.A.T.", VehicleCategory.PublicService),
            new VehicleModelInfo(602, "Alpha", VehicleCategory.Sport),
            new VehicleModelInfo(603, "Phoenix", VehicleCategory.Sport),
            new VehicleModelInfo(604, "Glendale Shit", VehicleCategory.Saloon),
            new VehicleModelInfo(605, "Sadler Shit", VehicleCategory.Industrial),
            new VehicleModelInfo(606, "Baggage Trailer \"A\"", VehicleCategory.Trailer),
            new VehicleModelInfo(607, "Baggage Trailer \"B\"", VehicleCategory.Trailer),
            new VehicleModelInfo(608, "Tug Stairs Trailer", VehicleCategory.Trailer),
            new VehicleModelInfo(609, "Boxville 2", VehicleCategory.Industrial),
            new VehicleModelInfo(610, "Farm Trailer", VehicleCategory.Trailer)
        };

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="VehicleModelInfo" /> class.
        /// </summary>
        private VehicleModelInfo(int type, string name, VehicleCategory category)
            : this()
        {
            Type = (VehicleModelType) type;
            Name = name;
            Category = category;
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
        ///     Gets model information of the given type.
        /// </summary>
        /// <param name="infotype">The type of information to retrieve.</param>
        public Vector3 this[VehicleModelInfoType infotype]
        {
            get
            {
                float x, y, z;
                VehicleModelInfoInternal.Instance.GetVehicleModelInfo((int) Type, (int) infotype, out x, out y, out z);
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
}