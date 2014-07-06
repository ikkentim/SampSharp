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

using SampSharp.GameMode.Pools;
using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SampSharp.GameMode.Definitions
{
    /// <summary>
    ///     Contains vehicle category infos.
    /// </summary>
    public class VehicleInfo : IdentifiedPool<VehicleInfo>, IIdentifyable
    {        
        #region VehicleInfos
        
        public static readonly VehicleInfo Landstalker = new VehicleInfo(400, "Landstalker", VehicleCategory.OffRoad);
        public static readonly VehicleInfo Bravura = new VehicleInfo(401, "Bravura", VehicleCategory.Saloon);
        public static readonly VehicleInfo Buffalo = new VehicleInfo(402, "Buffalo", VehicleCategory.Sport);
        public static readonly VehicleInfo Linerunner = new VehicleInfo(403, "Linerunner", VehicleCategory.Industrial);
        public static readonly VehicleInfo Perenniel = new VehicleInfo(404, "Perenniel", VehicleCategory.Station);
        public static readonly VehicleInfo Sentinel = new VehicleInfo(405, "Sentinel", VehicleCategory.Saloon);
        public static readonly VehicleInfo Dumper = new VehicleInfo(406, "Dumper", VehicleCategory.Unique);
        public static readonly VehicleInfo Firetruck = new VehicleInfo(407, "Firetruck", VehicleCategory.PublicService);
        public static readonly VehicleInfo Trashmaster = new VehicleInfo(408, "Trashmaster", VehicleCategory.Industrial);
        public static readonly VehicleInfo Stretch = new VehicleInfo(409, "Stretch", VehicleCategory.Unique);
        public static readonly VehicleInfo Manana = new VehicleInfo(410, "Manana", VehicleCategory.Saloon);
        public static readonly VehicleInfo Infernus = new VehicleInfo(411, "Infernus", VehicleCategory.Sport);
        public static readonly VehicleInfo Voodoo = new VehicleInfo(412, "Voodoo", VehicleCategory.Lowrider);
        public static readonly VehicleInfo Pony = new VehicleInfo(413, "Pony", VehicleCategory.Industrial);
        public static readonly VehicleInfo Mule = new VehicleInfo(414, "Mule", VehicleCategory.Industrial);
        public static readonly VehicleInfo Cheetah = new VehicleInfo(415, "Cheetah", VehicleCategory.Sport);
        public static readonly VehicleInfo Ambulance = new VehicleInfo(416, "Ambulance", VehicleCategory.PublicService);
        public static readonly VehicleInfo Leviathan = new VehicleInfo(417, "Leviathan", VehicleCategory.Helicopter);
        public static readonly VehicleInfo Moonbeam = new VehicleInfo(418, "Moonbeam", VehicleCategory.Station);
        public static readonly VehicleInfo Esperanto = new VehicleInfo(419, "Esperanto", VehicleCategory.Saloon);
        public static readonly VehicleInfo Taxi = new VehicleInfo(420, "Taxi", VehicleCategory.PublicService);
        public static readonly VehicleInfo Washington = new VehicleInfo(421, "Washington", VehicleCategory.Saloon);
        public static readonly VehicleInfo Bobcat = new VehicleInfo(422, "Bobcat", VehicleCategory.Industrial);
        public static readonly VehicleInfo MrWhoopee = new VehicleInfo(423, "Mr Whoopee", VehicleCategory.Unique);
        public static readonly VehicleInfo BFInjection = new VehicleInfo(424, "BF Injection", VehicleCategory.OffRoad);
        public static readonly VehicleInfo Hunter = new VehicleInfo(425, "Hunter", VehicleCategory.Helicopter);
        public static readonly VehicleInfo Premier = new VehicleInfo(426, "Premier", VehicleCategory.Saloon);
        public static readonly VehicleInfo Enforcer = new VehicleInfo(427, "Enforcer", VehicleCategory.PublicService);
        public static readonly VehicleInfo Securicar = new VehicleInfo(428, "Securicar", VehicleCategory.Unique);
        public static readonly VehicleInfo Banshee = new VehicleInfo(429, "Banshee", VehicleCategory.Sport);
        public static readonly VehicleInfo Predator = new VehicleInfo(430, "Predator", VehicleCategory.Boat);
        public static readonly VehicleInfo Bus = new VehicleInfo(431, "Bus", VehicleCategory.PublicService);
        public static readonly VehicleInfo Rhino = new VehicleInfo(432, "Rhino", VehicleCategory.PublicService);
        public static readonly VehicleInfo Barracks = new VehicleInfo(433, "Barracks", VehicleCategory.PublicService);
        public static readonly VehicleInfo Hotknife = new VehicleInfo(434, "Hotknife", VehicleCategory.Unique);
        public static readonly VehicleInfo ArticleTrailer = new VehicleInfo(435, "Article Trailer", VehicleCategory.Trailer);
        public static readonly VehicleInfo Previon = new VehicleInfo(436, "Previon", VehicleCategory.Saloon);
        public static readonly VehicleInfo Coach = new VehicleInfo(437, "Coach", VehicleCategory.PublicService);
        public static readonly VehicleInfo Cabbie = new VehicleInfo(438, "Cabbie", VehicleCategory.PublicService);
        public static readonly VehicleInfo Stallion = new VehicleInfo(439, "Stallion", VehicleCategory.Convertible);
        public static readonly VehicleInfo Rumpo = new VehicleInfo(440, "Rumpo", VehicleCategory.Industrial);
        public static readonly VehicleInfo RCBandit = new VehicleInfo(441, "RC Bandit", VehicleCategory.RemoteControl);
        public static readonly VehicleInfo Romero = new VehicleInfo(442, "Romero", VehicleCategory.Unique);
        public static readonly VehicleInfo Packer = new VehicleInfo(443, "Packer", VehicleCategory.Industrial);
        public static readonly VehicleInfo Monster = new VehicleInfo(444, "Monster", VehicleCategory.OffRoad);
        public static readonly VehicleInfo Admiral = new VehicleInfo(445, "Admiral", VehicleCategory.Saloon);
        public static readonly VehicleInfo Squallo = new VehicleInfo(446, "Squallo", VehicleCategory.Boat);
        public static readonly VehicleInfo Seasparrow = new VehicleInfo(447, "Seasparrow", VehicleCategory.Helicopter);
        public static readonly VehicleInfo Pizzaboy = new VehicleInfo(448, "Pizzaboy", VehicleCategory.Bike);
        public static readonly VehicleInfo Tram = new VehicleInfo(449, "Tram", VehicleCategory.Unique);
        public static readonly VehicleInfo ArticleTrailer2 = new VehicleInfo(450, "Article Trailer 2", VehicleCategory.Trailer);
        public static readonly VehicleInfo Turismo = new VehicleInfo(451, "Turismo", VehicleCategory.Sport);
        public static readonly VehicleInfo Speeder = new VehicleInfo(452, "Speeder", VehicleCategory.Boat);
        public static readonly VehicleInfo Reefer = new VehicleInfo(453, "Reefer", VehicleCategory.Boat);
        public static readonly VehicleInfo Tropic = new VehicleInfo(454, "Tropic", VehicleCategory.Boat);
        public static readonly VehicleInfo Flatbed = new VehicleInfo(456, "Flatbed", VehicleCategory.Industrial);
        public static readonly VehicleInfo Yankee = new VehicleInfo(456, "Yankee", VehicleCategory.Industrial);
        public static readonly VehicleInfo Caddy = new VehicleInfo(457, "Caddy", VehicleCategory.Unique);
        public static readonly VehicleInfo Solair = new VehicleInfo(458, "Solair", VehicleCategory.Station);
        public static readonly VehicleInfo TopfunVanBerkleysRC = new VehicleInfo(459, "Topfun Van (Berkley's RC)", VehicleCategory.Industrial);
        public static readonly VehicleInfo Skimmer = new VehicleInfo(460, "Skimmer", VehicleCategory.Airplane);
        public static readonly VehicleInfo PCJ600 = new VehicleInfo(461, "PCJ-600", VehicleCategory.Bike);
        public static readonly VehicleInfo Faggio = new VehicleInfo(462, "Faggio", VehicleCategory.Bike);
        public static readonly VehicleInfo Freeway = new VehicleInfo(463, "Freeway", VehicleCategory.Bike);
        public static readonly VehicleInfo RCBaron = new VehicleInfo(464, "RC Baron", VehicleCategory.RemoteControl);
        public static readonly VehicleInfo RCRaider = new VehicleInfo(465, "RC Raider", VehicleCategory.RemoteControl);
        public static readonly VehicleInfo Glendale = new VehicleInfo(466, "Glendale", VehicleCategory.Saloon);
        public static readonly VehicleInfo Oceanic = new VehicleInfo(467, "Oceanic", VehicleCategory.Saloon);
        public static readonly VehicleInfo Sanchez = new VehicleInfo(468, "Sanchez", VehicleCategory.Bike);
        public static readonly VehicleInfo Sparrow = new VehicleInfo(469, "Sparrow", VehicleCategory.Helicopter);
        public static readonly VehicleInfo Patriot = new VehicleInfo(470, "Patriot", VehicleCategory.OffRoad);
        public static readonly VehicleInfo Quad = new VehicleInfo(471, "Quad", VehicleCategory.Bike);
        public static readonly VehicleInfo Coastguard = new VehicleInfo(472, "Coastguard", VehicleCategory.Boat);
        public static readonly VehicleInfo Dinghy = new VehicleInfo(473, "Dinghy", VehicleCategory.Boat);
        public static readonly VehicleInfo Hermes = new VehicleInfo(474, "Hermes", VehicleCategory.Saloon);
        public static readonly VehicleInfo Sabre = new VehicleInfo(475, "Sabre", VehicleCategory.Sport);
        public static readonly VehicleInfo Rustler = new VehicleInfo(476, "Rustler", VehicleCategory.Airplane);
        public static readonly VehicleInfo ZR350 = new VehicleInfo(477, "ZR-350", VehicleCategory.Sport);
        public static readonly VehicleInfo Walton = new VehicleInfo(478, "Walton", VehicleCategory.Industrial);
        public static readonly VehicleInfo Regina = new VehicleInfo(479, "Regina", VehicleCategory.Station);
        public static readonly VehicleInfo Comet = new VehicleInfo(480, "Comet", VehicleCategory.Convertible);
        public static readonly VehicleInfo BMX = new VehicleInfo(481, "BMX", VehicleCategory.Bike);
        public static readonly VehicleInfo Burrito = new VehicleInfo(482, "Burrito", VehicleCategory.Industrial);
        public static readonly VehicleInfo Camper = new VehicleInfo(483, "Camper", VehicleCategory.Unique);
        public static readonly VehicleInfo Marquis = new VehicleInfo(484, "Marquis", VehicleCategory.Boat);
        public static readonly VehicleInfo Baggage = new VehicleInfo(485, "Baggage", VehicleCategory.Unique);
        public static readonly VehicleInfo Dozer = new VehicleInfo(486, "Dozer", VehicleCategory.Unique);
        public static readonly VehicleInfo Maverick = new VehicleInfo(487, "Maverick", VehicleCategory.Helicopter);
        public static readonly VehicleInfo SANNewsMaverick = new VehicleInfo(488, "SAN News Maverick", VehicleCategory.Helicopter);
        public static readonly VehicleInfo Rancher = new VehicleInfo(489, "Rancher", VehicleCategory.OffRoad);
        public static readonly VehicleInfo FBIRancher = new VehicleInfo(490, "FBI Rancher", VehicleCategory.PublicService);
        public static readonly VehicleInfo Virgo = new VehicleInfo(491, "Virgo", VehicleCategory.Saloon);
        public static readonly VehicleInfo Greenwood = new VehicleInfo(492, "Greenwood", VehicleCategory.Saloon);
        public static readonly VehicleInfo Jetmax = new VehicleInfo(493, "Jetmax", VehicleCategory.Boat);
        public static readonly VehicleInfo HotringRacer = new VehicleInfo(494, "Hotring Racer", VehicleCategory.Sport);
        public static readonly VehicleInfo Sandking = new VehicleInfo(495, "Sandking", VehicleCategory.OffRoad);
        public static readonly VehicleInfo BlistaCompact = new VehicleInfo(496, "Blista Compact", VehicleCategory.Sport);
        public static readonly VehicleInfo PoliceMaverick = new VehicleInfo(497, "Police Maverick", VehicleCategory.Helicopter);
        public static readonly VehicleInfo Boxville = new VehicleInfo(498, "Boxville", VehicleCategory.Industrial);
        public static readonly VehicleInfo Benson = new VehicleInfo(499, "Benson", VehicleCategory.Industrial);
        public static readonly VehicleInfo Mesa = new VehicleInfo(500, "Mesa", VehicleCategory.OffRoad);
        public static readonly VehicleInfo RCGoblin = new VehicleInfo(501, "RC Goblin", VehicleCategory.RemoteControl);
        public static readonly VehicleInfo HotringRacer2 = new VehicleInfo(502, "Hotring Racer 2", VehicleCategory.Sport);
        public static readonly VehicleInfo HotringRacer3 = new VehicleInfo(503, "Hotring Racer 3", VehicleCategory.Sport);
        public static readonly VehicleInfo BloodringBanger = new VehicleInfo(504, "Bloodring Banger", VehicleCategory.Saloon);
        public static readonly VehicleInfo Rancher2 = new VehicleInfo(505, "Rancher", VehicleCategory.OffRoad);
        public static readonly VehicleInfo SuperGT = new VehicleInfo(506, "Super GT", VehicleCategory.Sport);
        public static readonly VehicleInfo Elegant = new VehicleInfo(507, "Elegant", VehicleCategory.Saloon);
        public static readonly VehicleInfo Journey = new VehicleInfo(508, "Journey", VehicleCategory.Unique);
        public static readonly VehicleInfo Bike = new VehicleInfo(509, "Bike", VehicleCategory.Bike);
        public static readonly VehicleInfo MountainBike = new VehicleInfo(510, "Mountain Bike", VehicleCategory.Bike);
        public static readonly VehicleInfo Beagle = new VehicleInfo(511, "Beagle", VehicleCategory.Airplane);
        public static readonly VehicleInfo Cropduster = new VehicleInfo(512, "Cropduster", VehicleCategory.Airplane);
        public static readonly VehicleInfo Stuntplane = new VehicleInfo(513, "Stuntplane", VehicleCategory.Airplane);
        public static readonly VehicleInfo Tanker = new VehicleInfo(514, "Tanker", VehicleCategory.Industrial);
        public static readonly VehicleInfo Roadtrain = new VehicleInfo(515, "Roadtrain", VehicleCategory.Industrial);
        public static readonly VehicleInfo Nebula = new VehicleInfo(516, "Nebula", VehicleCategory.Saloon);
        public static readonly VehicleInfo Majestic = new VehicleInfo(517, "Majestic", VehicleCategory.Saloon);
        public static readonly VehicleInfo Buccaneer = new VehicleInfo(518, "Buccaneer", VehicleCategory.Saloon);
        public static readonly VehicleInfo Shamal = new VehicleInfo(519, "Shamal", VehicleCategory.Airplane);
        public static readonly VehicleInfo Hydra = new VehicleInfo(520, "Hydra", VehicleCategory.Airplane);
        public static readonly VehicleInfo FCR900 = new VehicleInfo(521, "FCR-900", VehicleCategory.Bike);
        public static readonly VehicleInfo NRG500 = new VehicleInfo(522, "NRG-500", VehicleCategory.Bike);
        public static readonly VehicleInfo HPV1000 = new VehicleInfo(523, "HPV1000", VehicleCategory.PublicService);
        public static readonly VehicleInfo CementTruck = new VehicleInfo(524, "Cement Truck", VehicleCategory.Industrial);
        public static readonly VehicleInfo Towtruck = new VehicleInfo(525, "Towtruck", VehicleCategory.Unique);
        public static readonly VehicleInfo Fortune = new VehicleInfo(526, "Fortune", VehicleCategory.Saloon);
        public static readonly VehicleInfo Cadrona = new VehicleInfo(527, "Cadrona", VehicleCategory.Saloon);
        public static readonly VehicleInfo FBITruck = new VehicleInfo(528, "FBI Truck", VehicleCategory.PublicService);
        public static readonly VehicleInfo Willard = new VehicleInfo(529, "Willard", VehicleCategory.Saloon);
        public static readonly VehicleInfo Forklift = new VehicleInfo(530, "Forklift", VehicleCategory.Unique);
        public static readonly VehicleInfo Tractor = new VehicleInfo(531, "Tractor", VehicleCategory.Industrial);
        public static readonly VehicleInfo CombineHarvester = new VehicleInfo(532, "Combine Harvester", VehicleCategory.Unique);
        public static readonly VehicleInfo Feltzer = new VehicleInfo(533, "Feltzer", VehicleCategory.Convertible);
        public static readonly VehicleInfo Remington = new VehicleInfo(534, "Remington", VehicleCategory.Lowrider);
        public static readonly VehicleInfo Slamvan = new VehicleInfo(535, "Slamvan", VehicleCategory.Lowrider);
        public static readonly VehicleInfo Blade = new VehicleInfo(536, "Blade", VehicleCategory.Lowrider);
        public static readonly VehicleInfo FreightTrain = new VehicleInfo(537, "Freight (Train)", VehicleCategory.Unique);
        public static readonly VehicleInfo BrownstreakTrain = new VehicleInfo(538, "Brownstreak (Train)", VehicleCategory.Unique);
        public static readonly VehicleInfo Vortex = new VehicleInfo(539, "Vortex", VehicleCategory.Unique);
        public static readonly VehicleInfo Vincent = new VehicleInfo(540, "Vincent", VehicleCategory.Saloon);
        public static readonly VehicleInfo Bullet = new VehicleInfo(541, "Bullet", VehicleCategory.Sport);
        public static readonly VehicleInfo Clover = new VehicleInfo(542, "Clover", VehicleCategory.Saloon);
        public static readonly VehicleInfo Sadler = new VehicleInfo(543, "Sadler", VehicleCategory.Industrial);
        public static readonly VehicleInfo FiretruckLA = new VehicleInfo(544, "Firetruck LA", VehicleCategory.PublicService);
        public static readonly VehicleInfo Hustler = new VehicleInfo(545, "Hustler", VehicleCategory.Unique);
        public static readonly VehicleInfo Intruder = new VehicleInfo(546, "Intruder", VehicleCategory.Saloon);
        public static readonly VehicleInfo Primo = new VehicleInfo(547, "Primo", VehicleCategory.Saloon);
        public static readonly VehicleInfo Cargobob = new VehicleInfo(548, "Cargobob", VehicleCategory.Helicopter);
        public static readonly VehicleInfo Tampa = new VehicleInfo(549, "Tampa", VehicleCategory.Saloon);
        public static readonly VehicleInfo Sunrise = new VehicleInfo(550, "Sunrise", VehicleCategory.Saloon);
        public static readonly VehicleInfo Merit = new VehicleInfo(551, "Merit", VehicleCategory.Saloon);
        public static readonly VehicleInfo UtilityVan = new VehicleInfo(552, "Utility Van", VehicleCategory.Industrial);
        public static readonly VehicleInfo Nevada = new VehicleInfo(553, "Nevada", VehicleCategory.Airplane);
        public static readonly VehicleInfo Yosemite = new VehicleInfo(554, "Yosemite", VehicleCategory.Industrial);
        public static readonly VehicleInfo Windsor = new VehicleInfo(555, "Windsor", VehicleCategory.Convertible);
        public static readonly VehicleInfo MonsterA = new VehicleInfo(556, "Monster\"A\"", VehicleCategory.OffRoad);
        public static readonly VehicleInfo MonsterB = new VehicleInfo(557, "Monster \"B\"", VehicleCategory.OffRoad);
        public static readonly VehicleInfo Uranus = new VehicleInfo(558, "Uranus", VehicleCategory.Sport);
        public static readonly VehicleInfo Jester = new VehicleInfo(559, "Jester", VehicleCategory.Sport);
        public static readonly VehicleInfo Sultan = new VehicleInfo(560, "Sultan", VehicleCategory.Saloon);
        public static readonly VehicleInfo Stratum = new VehicleInfo(561, "Stratum", VehicleCategory.Station);
        public static readonly VehicleInfo Elegy = new VehicleInfo(562, "Elegy", VehicleCategory.Saloon);
        public static readonly VehicleInfo Raindance = new VehicleInfo(563, "Raindance", VehicleCategory.Helicopter);
        public static readonly VehicleInfo RCTiger = new VehicleInfo(564, "RC Tiger", VehicleCategory.RemoteControl);
        public static readonly VehicleInfo Flash = new VehicleInfo(565, "Flash", VehicleCategory.Sport);
        public static readonly VehicleInfo Tahoma = new VehicleInfo(566, "Tahoma", VehicleCategory.Lowrider);
        public static readonly VehicleInfo Savanna = new VehicleInfo(567, "Savanna", VehicleCategory.Lowrider);
        public static readonly VehicleInfo Bandito = new VehicleInfo(568, "Bandito", VehicleCategory.OffRoad);
        public static readonly VehicleInfo FreightFlatTrailerTrain = new VehicleInfo(569, "Freight Flat Trailer (Train)", VehicleCategory.TrainTrailer);
        public static readonly VehicleInfo StreakTrailerTrain = new VehicleInfo(570, "Streak Trailer (Train)", VehicleCategory.TrainTrailer);
        public static readonly VehicleInfo Kart = new VehicleInfo(571, "Kart", VehicleCategory.Unique);
        public static readonly VehicleInfo Mower = new VehicleInfo(572, "Mower", VehicleCategory.Unique);
        public static readonly VehicleInfo Dune = new VehicleInfo(573, "Dune", VehicleCategory.OffRoad);
        public static readonly VehicleInfo Sweeper = new VehicleInfo(574, "Sweeper", VehicleCategory.Unique);
        public static readonly VehicleInfo Broadway = new VehicleInfo(575, "Broadway", VehicleCategory.Lowrider);
        public static readonly VehicleInfo Tornado = new VehicleInfo(576, "Tornado", VehicleCategory.Lowrider);
        public static readonly VehicleInfo AT400 = new VehicleInfo(577, "AT400", VehicleCategory.Airplane);
        public static readonly VehicleInfo DFT30 = new VehicleInfo(578, "DFT-30", VehicleCategory.Industrial);
        public static readonly VehicleInfo Huntley = new VehicleInfo(579, "Huntley", VehicleCategory.OffRoad);
        public static readonly VehicleInfo Stafford = new VehicleInfo(580, "Stafford", VehicleCategory.Saloon);
        public static readonly VehicleInfo BF400 = new VehicleInfo(581, "BF-400", VehicleCategory.Bike);
        public static readonly VehicleInfo Newsvan = new VehicleInfo(582, "Newsvan", VehicleCategory.Industrial);
        public static readonly VehicleInfo Tug = new VehicleInfo(583, "Tug", VehicleCategory.Unique);
        public static readonly VehicleInfo PetrolTrailer = new VehicleInfo(584, "Petrol Trailer", VehicleCategory.Trailer);
        public static readonly VehicleInfo Emperor = new VehicleInfo(585, "Emperor", VehicleCategory.Saloon);
        public static readonly VehicleInfo Wayfarer = new VehicleInfo(586, "Wayfarer", VehicleCategory.Bike);
        public static readonly VehicleInfo Euros = new VehicleInfo(587, "Euros", VehicleCategory.Sport);
        public static readonly VehicleInfo Hotdog = new VehicleInfo(588, "Hotdog", VehicleCategory.Unique);
        public static readonly VehicleInfo Club = new VehicleInfo(589, "Club", VehicleCategory.Sport);
        public static readonly VehicleInfo FreightBoxTrailerTrain = new VehicleInfo(590, "Freight Box Trailer (Train)", VehicleCategory.TrainTrailer);
        public static readonly VehicleInfo ArticleTrailer3 = new VehicleInfo(591, "Article Trailer 3", VehicleCategory.Trailer);
        public static readonly VehicleInfo Andromada = new VehicleInfo(592, "Andromada", VehicleCategory.Airplane);
        public static readonly VehicleInfo Dodo = new VehicleInfo(593, "Dodo", VehicleCategory.Airplane);
        public static readonly VehicleInfo RCCam = new VehicleInfo(594, "RC Cam", VehicleCategory.RemoteControl);
        public static readonly VehicleInfo Launch = new VehicleInfo(595, "Launch", VehicleCategory.Boat);
        public static readonly VehicleInfo PoliceCarLSPD = new VehicleInfo(596, "Police Car (LSPD)", VehicleCategory.PublicService);
        public static readonly VehicleInfo PoliceCarSFPD = new VehicleInfo(597, "Police Car (SFPD)", VehicleCategory.PublicService);
        public static readonly VehicleInfo PoliceCarLVPD = new VehicleInfo(598, "Police Car (LVPD)", VehicleCategory.PublicService);
        public static readonly VehicleInfo PoliceRanger = new VehicleInfo(599, "Police Ranger", VehicleCategory.PublicService);
        public static readonly VehicleInfo Picador = new VehicleInfo(600, "Picador", VehicleCategory.Industrial);
        public static readonly VehicleInfo SWAT = new VehicleInfo(601, "S.W.A.T.", VehicleCategory.PublicService);
        public static readonly VehicleInfo Alpha = new VehicleInfo(602, "Alpha", VehicleCategory.Sport);
        public static readonly VehicleInfo Phoenix = new VehicleInfo(603, "Phoenix", VehicleCategory.Sport);
        public static readonly VehicleInfo GlendaleShit = new VehicleInfo(604, "Glendale Shit", VehicleCategory.Saloon);
        public static readonly VehicleInfo SadlerShit = new VehicleInfo(605, "Sadler Shit", VehicleCategory.Industrial);
        public static readonly VehicleInfo BaggageTrailerA = new VehicleInfo(606, "Baggage Trailer \"A\"", VehicleCategory.Trailer);
        public static readonly VehicleInfo BaggageTrailerB = new VehicleInfo(607, "Baggage Trailer \"B\"", VehicleCategory.Trailer);
        public static readonly VehicleInfo TugStairsTrailer = new VehicleInfo(608, "Tug Stairs Trailer", VehicleCategory.Trailer);
        public static readonly VehicleInfo Boxville2 = new VehicleInfo(609, "Boxville 2", VehicleCategory.Industrial);
        public static readonly VehicleInfo FarmTrailer = new VehicleInfo(610, "Farm Trailer", VehicleCategory.Trailer);

        #endregion

        public int Id { get; private set; }
        public string Name { get; private set; }
        public VehicleCategory Category { get; private set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VehicleInfo" /> class.
        /// </summary>
        private VehicleInfo(int id, string name, VehicleCategory category)
            : base()
        {
            this.Id = id;
            this.Name = name;
            this.Category = category;
        }

        /// <summary>
        ///     Registers the type to use when initializing new instances.
        ///     Ensures initialization of all vehicle infos.
        /// </summary>
        /// <typeparam name="T2">The Type to use when initializing new instances.</typeparam>
        public new static void Register<T2>()
        {
            Type = typeof(T2);
        }

        /// <summary>
        ///     Finds an instance with the given <paramref name="id" />".
        ///     Ensures initialization of all vehicle infos.
        /// </summary>
        /// <param name="id">The identity of the instance to find.</param>
        /// <returns>The found instance.</returns>
        public new static VehicleInfo Find(int id)
        {
            return All.FirstOrDefault(i => i.Id == id);
        }

        /// <summary>
        ///     Initializes a new instance with the given id.
        ///     Ensures initialization of all vehicle infos.
        /// </summary>
        /// <param name="id">The identity of the instance to create.</param>
        /// <returns>The initialized instance.</returns>
        public new static VehicleInfo Create(int id)
        {
            return (VehicleInfo)Activator.CreateInstance(Type, id);
        }

        /// <summary>
        ///     Finds an instance with the given <paramref name="id" /> or initializes a new one.
        ///     Ensures initialization of all vehicle infos.
        /// </summary>
        /// <param name="id">The identity of the instance to find or create.</param>
        /// <returns>The found instance.</returns>
        public new static VehicleInfo FindOrCreate(int id)
        {
            return Find(id) ?? Create(id);
        }
    }
}
