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
using SampSharp.GameMode.World;
using System;
using System.Collections.Generic;

namespace SampSharp.GameMode.Definitions
{
    /// <summary>
    ///     Contains vehicle category infos.
    /// </summary>
    public class VehicleInfo : IdentifiedPool<VehicleInfo>, IIdentifyable
    {
        #region VehicleInfos

        public static readonly VehicleInfo Landstalker = new VehicleInfo { Id = 400, Name = "Landstalker", Category = VehicleCategory.OffRoad };
        public static readonly VehicleInfo Bravura = new VehicleInfo { Id = 401, Name = "Bravura", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Buffalo = new VehicleInfo { Id = 402, Name = "Buffalo", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo Linerunner = new VehicleInfo { Id = 403, Name = "Linerunner", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo Perenniel = new VehicleInfo { Id = 404, Name = "Perenniel", Category = VehicleCategory.Station };
        public static readonly VehicleInfo Sentinel = new VehicleInfo { Id = 405, Name = "Sentinel", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Dumper = new VehicleInfo { Id = 406, Name = "Dumper", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo Firetruck = new VehicleInfo { Id = 407, Name = "Firetruck", Category = VehicleCategory.PublicService };
        public static readonly VehicleInfo Trashmaster = new VehicleInfo { Id = 408, Name = "Trashmaster", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo Stretch = new VehicleInfo { Id = 409, Name = "Stretch", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo Manana = new VehicleInfo { Id = 410, Name = "Manana", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Infernus = new VehicleInfo { Id = 411, Name = "Infernus", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo Voodoo = new VehicleInfo { Id = 412, Name = "Voodoo", Category = VehicleCategory.Lowrider };
        public static readonly VehicleInfo Pony = new VehicleInfo { Id = 413, Name = "Pony", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo Mule = new VehicleInfo { Id = 414, Name = "Mule", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo Cheetah = new VehicleInfo { Id = 415, Name = "Cheetah", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo Ambulance = new VehicleInfo { Id = 416, Name = "Ambulance", Category = VehicleCategory.PublicService };
        public static readonly VehicleInfo Leviathan = new VehicleInfo { Id = 417, Name = "Leviathan", Category = VehicleCategory.Helicopter };
        public static readonly VehicleInfo Moonbeam = new VehicleInfo { Id = 418, Name = "Moonbeam", Category = VehicleCategory.Station };
        public static readonly VehicleInfo Esperanto = new VehicleInfo { Id = 419, Name = "Esperanto", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Taxi = new VehicleInfo { Id = 420, Name = "Taxi", Category = VehicleCategory.PublicService };
        public static readonly VehicleInfo Washington = new VehicleInfo { Id = 421, Name = "Washington", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Bobcat = new VehicleInfo { Id = 422, Name = "Bobcat", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo MrWhoopee = new VehicleInfo { Id = 423, Name = "Mr Whoopee", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo BFInjection = new VehicleInfo { Id = 424, Name = "BF Injection", Category = VehicleCategory.OffRoad };
        public static readonly VehicleInfo Hunter = new VehicleInfo { Id = 425, Name = "Hunter", Category = VehicleCategory.Helicopter };
        public static readonly VehicleInfo Premier = new VehicleInfo { Id = 426, Name = "Premier", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Enforcer = new VehicleInfo { Id = 427, Name = "Enforcer", Category = VehicleCategory.PublicService };
        public static readonly VehicleInfo Securicar = new VehicleInfo { Id = 428, Name = "Securicar", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo Banshee = new VehicleInfo { Id = 429, Name = "Banshee", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo Predator = new VehicleInfo { Id = 430, Name = "Predator", Category = VehicleCategory.Boat };
        public static readonly VehicleInfo Bus = new VehicleInfo { Id = 431, Name = "Bus", Category = VehicleCategory.PublicService };
        public static readonly VehicleInfo Rhino = new VehicleInfo { Id = 432, Name = "Rhino", Category = VehicleCategory.PublicService };
        public static readonly VehicleInfo Barracks = new VehicleInfo { Id = 433, Name = "Barracks", Category = VehicleCategory.PublicService };
        public static readonly VehicleInfo Hotknife = new VehicleInfo { Id = 434, Name = "Hotknife", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo ArticleTrailer = new VehicleInfo { Id = 435, Name = "Article Trailer", Category = VehicleCategory.Trailer };
        public static readonly VehicleInfo Previon = new VehicleInfo { Id = 436, Name = "Previon", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Coach = new VehicleInfo { Id = 437, Name = "Coach", Category = VehicleCategory.PublicService };
        public static readonly VehicleInfo Cabbie = new VehicleInfo { Id = 438, Name = "Cabbie", Category = VehicleCategory.PublicService };
        public static readonly VehicleInfo Stallion = new VehicleInfo { Id = 439, Name = "Stallion", Category = VehicleCategory.Convertible };
        public static readonly VehicleInfo Rumpo = new VehicleInfo { Id = 440, Name = "Rumpo", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo RCBandit = new VehicleInfo { Id = 441, Name = "RC Bandit", Category = VehicleCategory.RemoteControl };
        public static readonly VehicleInfo Romero = new VehicleInfo { Id = 442, Name = "Romero", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo Packer = new VehicleInfo { Id = 443, Name = "Packer", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo Monster = new VehicleInfo { Id = 444, Name = "Monster", Category = VehicleCategory.OffRoad };
        public static readonly VehicleInfo Admiral = new VehicleInfo { Id = 445, Name = "Admiral", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Squallo = new VehicleInfo { Id = 446, Name = "Squallo", Category = VehicleCategory.Boat };
        public static readonly VehicleInfo Seasparrow = new VehicleInfo { Id = 447, Name = "Seasparrow", Category = VehicleCategory.Helicopter };
        public static readonly VehicleInfo Pizzaboy = new VehicleInfo { Id = 448, Name = "Pizzaboy", Category = VehicleCategory.Bike };
        public static readonly VehicleInfo Tram = new VehicleInfo { Id = 449, Name = "Tram", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo ArticleTrailer2 = new VehicleInfo { Id = 450, Name = "Article Trailer 2", Category = VehicleCategory.Trailer };
        public static readonly VehicleInfo Turismo = new VehicleInfo { Id = 451, Name = "Turismo", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo Speeder = new VehicleInfo { Id = 452, Name = "Speeder", Category = VehicleCategory.Boat };
        public static readonly VehicleInfo Reefer = new VehicleInfo { Id = 453, Name = "Reefer", Category = VehicleCategory.Boat };
        public static readonly VehicleInfo Tropic = new VehicleInfo { Id = 454, Name = "Tropic", Category = VehicleCategory.Boat };
        public static readonly VehicleInfo Flatbed = new VehicleInfo { Id = 455, Name = "Flatbed", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo Yankee = new VehicleInfo { Id = 456, Name = "Yankee", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo Caddy = new VehicleInfo { Id = 457, Name = "Caddy", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo Solair = new VehicleInfo { Id = 458, Name = "Solair", Category = VehicleCategory.Station };
        public static readonly VehicleInfo TopfunVanBerkleysRC = new VehicleInfo { Id = 459, Name = "Topfun Van (Berkley's RC)", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo Skimmer = new VehicleInfo { Id = 460, Name = "Skimmer", Category = VehicleCategory.Airplane };
        public static readonly VehicleInfo PCJ600 = new VehicleInfo { Id = 461, Name = "PCJ-600", Category = VehicleCategory.Bike };
        public static readonly VehicleInfo Faggio = new VehicleInfo { Id = 462, Name = "Faggio", Category = VehicleCategory.Bike };
        public static readonly VehicleInfo Freeway = new VehicleInfo { Id = 463, Name = "Freeway", Category = VehicleCategory.Bike };
        public static readonly VehicleInfo RCBaron = new VehicleInfo { Id = 464, Name = "RC Baron", Category = VehicleCategory.RemoteControl };
        public static readonly VehicleInfo RCRaider = new VehicleInfo { Id = 465, Name = "RC Raider", Category = VehicleCategory.RemoteControl };
        public static readonly VehicleInfo Glendale = new VehicleInfo { Id = 466, Name = "Glendale", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Oceanic = new VehicleInfo { Id = 467, Name = "Oceanic", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Sanchez = new VehicleInfo { Id = 468, Name = "Sanchez", Category = VehicleCategory.Bike };
        public static readonly VehicleInfo Sparrow = new VehicleInfo { Id = 469, Name = "Sparrow", Category = VehicleCategory.Helicopter };
        public static readonly VehicleInfo Patriot = new VehicleInfo { Id = 470, Name = "Patriot", Category = VehicleCategory.OffRoad };
        public static readonly VehicleInfo Quad = new VehicleInfo { Id = 471, Name = "Quad", Category = VehicleCategory.Bike };
        public static readonly VehicleInfo Coastguard = new VehicleInfo { Id = 472, Name = "Coastguard", Category = VehicleCategory.Boat };
        public static readonly VehicleInfo Dinghy = new VehicleInfo { Id = 473, Name = "Dinghy", Category = VehicleCategory.Boat };
        public static readonly VehicleInfo Hermes = new VehicleInfo { Id = 474, Name = "Hermes", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Sabre = new VehicleInfo { Id = 475, Name = "Sabre", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo Rustler = new VehicleInfo { Id = 476, Name = "Rustler", Category = VehicleCategory.Airplane };
        public static readonly VehicleInfo ZR350 = new VehicleInfo { Id = 477, Name = "ZR-350", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo Walton = new VehicleInfo { Id = 478, Name = "Walton", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo Regina = new VehicleInfo { Id = 479, Name = "Regina", Category = VehicleCategory.Station };
        public static readonly VehicleInfo Comet = new VehicleInfo { Id = 480, Name = "Comet", Category = VehicleCategory.Convertible };
        public static readonly VehicleInfo BMX = new VehicleInfo { Id = 481, Name = "BMX", Category = VehicleCategory.Bike };
        public static readonly VehicleInfo Burrito = new VehicleInfo { Id = 482, Name = "Burrito", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo Camper = new VehicleInfo { Id = 483, Name = "Camper", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo Marquis = new VehicleInfo { Id = 484, Name = "Marquis", Category = VehicleCategory.Boat };
        public static readonly VehicleInfo Baggage = new VehicleInfo { Id = 485, Name = "Baggage", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo Dozer = new VehicleInfo { Id = 486, Name = "Dozer", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo Maverick = new VehicleInfo { Id = 487, Name = "Maverick", Category = VehicleCategory.Helicopter };
        public static readonly VehicleInfo SANNewsMaverick = new VehicleInfo { Id = 488, Name = "SAN News Maverick", Category = VehicleCategory.Helicopter };
        public static readonly VehicleInfo Rancher = new VehicleInfo { Id = 489, Name = "Rancher", Category = VehicleCategory.OffRoad };
        public static readonly VehicleInfo FBIRancher = new VehicleInfo { Id = 490, Name = "FBI Rancher", Category = VehicleCategory.PublicService };
        public static readonly VehicleInfo Virgo = new VehicleInfo { Id = 491, Name = "Virgo", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Greenwood = new VehicleInfo { Id = 492, Name = "Greenwood", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Jetmax = new VehicleInfo { Id = 493, Name = "Jetmax", Category = VehicleCategory.Boat };
        public static readonly VehicleInfo HotringRacer = new VehicleInfo { Id = 494, Name = "Hotring Racer", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo Sandking = new VehicleInfo { Id = 495, Name = "Sandking", Category = VehicleCategory.OffRoad };
        public static readonly VehicleInfo BlistaCompact = new VehicleInfo { Id = 496, Name = "Blista Compact", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo PoliceMaverick = new VehicleInfo { Id = 497, Name = "Police Maverick", Category = VehicleCategory.Helicopter };
        public static readonly VehicleInfo Boxville = new VehicleInfo { Id = 498, Name = "Boxville", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo Benson = new VehicleInfo { Id = 499, Name = "Benson", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo Mesa = new VehicleInfo { Id = 500, Name = "Mesa", Category = VehicleCategory.OffRoad };
        public static readonly VehicleInfo RCGoblin = new VehicleInfo { Id = 501, Name = "RC Goblin", Category = VehicleCategory.RemoteControl };
        public static readonly VehicleInfo HotringRacer2 = new VehicleInfo { Id = 502, Name = "Hotring Racer 2", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo HotringRacer3 = new VehicleInfo { Id = 503, Name = "Hotring Racer 3", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo BloodringBanger = new VehicleInfo { Id = 504, Name = "Bloodring Banger", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Rancher2 = new VehicleInfo { Id = 505, Name = "Rancher", Category = VehicleCategory.OffRoad };
        public static readonly VehicleInfo SuperGT = new VehicleInfo { Id = 506, Name = "Super GT", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo Elegant = new VehicleInfo { Id = 507, Name = "Elegant", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Journey = new VehicleInfo { Id = 508, Name = "Journey", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo Bike = new VehicleInfo { Id = 509, Name = "Bike", Category = VehicleCategory.Bike };
        public static readonly VehicleInfo MountainBike = new VehicleInfo { Id = 510, Name = "Mountain Bike", Category = VehicleCategory.Bike };
        public static readonly VehicleInfo Beagle = new VehicleInfo { Id = 511, Name = "Beagle", Category = VehicleCategory.Airplane };
        public static readonly VehicleInfo Cropduster = new VehicleInfo { Id = 512, Name = "Cropduster", Category = VehicleCategory.Airplane };
        public static readonly VehicleInfo Stuntplane = new VehicleInfo { Id = 513, Name = "Stuntplane", Category = VehicleCategory.Airplane };
        public static readonly VehicleInfo Tanker = new VehicleInfo { Id = 514, Name = "Tanker", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo Roadtrain = new VehicleInfo { Id = 515, Name = "Roadtrain", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo Nebula = new VehicleInfo { Id = 516, Name = "Nebula", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Majestic = new VehicleInfo { Id = 517, Name = "Majestic", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Buccaneer = new VehicleInfo { Id = 518, Name = "Buccaneer", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Shamal = new VehicleInfo { Id = 519, Name = "Shamal", Category = VehicleCategory.Airplane };
        public static readonly VehicleInfo Hydra = new VehicleInfo { Id = 520, Name = "Hydra", Category = VehicleCategory.Airplane };
        public static readonly VehicleInfo FCR900 = new VehicleInfo { Id = 521, Name = "FCR-900", Category = VehicleCategory.Bike };
        public static readonly VehicleInfo NRG500 = new VehicleInfo { Id = 522, Name = "NRG-500", Category = VehicleCategory.Bike };
        public static readonly VehicleInfo HPV1000 = new VehicleInfo { Id = 523, Name = "HPV1000", Category = VehicleCategory.PublicService };
        public static readonly VehicleInfo CementTruck = new VehicleInfo { Id = 524, Name = "Cement Truck", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo Towtruck = new VehicleInfo { Id = 525, Name = "Towtruck", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo Fortune = new VehicleInfo { Id = 526, Name = "Fortune", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Cadrona = new VehicleInfo { Id = 527, Name = "Cadrona", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo FBITruck = new VehicleInfo { Id = 528, Name = "FBI Truck", Category = VehicleCategory.PublicService };
        public static readonly VehicleInfo Willard = new VehicleInfo { Id = 529, Name = "Willard", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Forklift = new VehicleInfo { Id = 530, Name = "Forklift", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo Tractor = new VehicleInfo { Id = 531, Name = "Tractor", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo CombineHarvester = new VehicleInfo { Id = 532, Name = "Combine Harvester", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo Feltzer = new VehicleInfo { Id = 533, Name = "Feltzer", Category = VehicleCategory.Convertible };
        public static readonly VehicleInfo Remington = new VehicleInfo { Id = 534, Name = "Remington", Category = VehicleCategory.Lowrider };
        public static readonly VehicleInfo Slamvan = new VehicleInfo { Id = 535, Name = "Slamvan", Category = VehicleCategory.Lowrider };
        public static readonly VehicleInfo Blade = new VehicleInfo { Id = 536, Name = "Blade", Category = VehicleCategory.Lowrider };
        public static readonly VehicleInfo FreightTrain = new VehicleInfo { Id = 537, Name = "Freight (Train)", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo BrownstreakTrain = new VehicleInfo { Id = 538, Name = "Brownstreak (Train)", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo Vortex = new VehicleInfo { Id = 539, Name = "Vortex", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo Vincent = new VehicleInfo { Id = 540, Name = "Vincent", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Bullet = new VehicleInfo { Id = 541, Name = "Bullet", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo Clover = new VehicleInfo { Id = 542, Name = "Clover", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Sadler = new VehicleInfo { Id = 543, Name = "Sadler", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo FiretruckLA = new VehicleInfo { Id = 544, Name = "Firetruck LA", Category = VehicleCategory.PublicService };
        public static readonly VehicleInfo Hustler = new VehicleInfo { Id = 545, Name = "Hustler", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo Intruder = new VehicleInfo { Id = 546, Name = "Intruder", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Primo = new VehicleInfo { Id = 547, Name = "Primo", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Cargobob = new VehicleInfo { Id = 548, Name = "Cargobob", Category = VehicleCategory.Helicopter };
        public static readonly VehicleInfo Tampa = new VehicleInfo { Id = 549, Name = "Tampa", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Sunrise = new VehicleInfo { Id = 550, Name = "Sunrise", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Merit = new VehicleInfo { Id = 551, Name = "Merit", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo UtilityVan = new VehicleInfo { Id = 552, Name = "Utility Van", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo Nevada = new VehicleInfo { Id = 553, Name = "Nevada", Category = VehicleCategory.Airplane };
        public static readonly VehicleInfo Yosemite = new VehicleInfo { Id = 554, Name = "Yosemite", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo Windsor = new VehicleInfo { Id = 555, Name = "Windsor", Category = VehicleCategory.Convertible };
        public static readonly VehicleInfo MonsterA = new VehicleInfo { Id = 556, Name = "Monster\"A\"", Category = VehicleCategory.OffRoad };
        public static readonly VehicleInfo MonsterB = new VehicleInfo { Id = 557, Name = "Monster \"B\"", Category = VehicleCategory.OffRoad };
        public static readonly VehicleInfo Uranus = new VehicleInfo { Id = 558, Name = "Uranus", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo Jester = new VehicleInfo { Id = 559, Name = "Jester", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo Sultan = new VehicleInfo { Id = 560, Name = "Sultan", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Stratum = new VehicleInfo { Id = 561, Name = "Stratum", Category = VehicleCategory.Station };
        public static readonly VehicleInfo Elegy = new VehicleInfo { Id = 562, Name = "Elegy", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Raindance = new VehicleInfo { Id = 563, Name = "Raindance", Category = VehicleCategory.Helicopter };
        public static readonly VehicleInfo RCTiger = new VehicleInfo { Id = 564, Name = "RC Tiger", Category = VehicleCategory.RemoteControl };
        public static readonly VehicleInfo Flash = new VehicleInfo { Id = 565, Name = "Flash", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo Tahoma = new VehicleInfo { Id = 566, Name = "Tahoma", Category = VehicleCategory.Lowrider };
        public static readonly VehicleInfo Savanna = new VehicleInfo { Id = 567, Name = "Savanna", Category = VehicleCategory.Lowrider };
        public static readonly VehicleInfo Bandito = new VehicleInfo { Id = 568, Name = "Bandito", Category = VehicleCategory.OffRoad };
        public static readonly VehicleInfo FreightFlatTrailerTrain = new VehicleInfo { Id = 569, Name = "Freight Flat Trailer (Train)", Category = VehicleCategory.TrainTrailer };
        public static readonly VehicleInfo StreakTrailerTrain = new VehicleInfo { Id = 570, Name = "Streak Trailer (Train)", Category = VehicleCategory.TrainTrailer };
        public static readonly VehicleInfo Kart = new VehicleInfo { Id = 571, Name = "Kart", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo Mower = new VehicleInfo { Id = 572, Name = "Mower", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo Dune = new VehicleInfo { Id = 573, Name = "Dune", Category = VehicleCategory.OffRoad };
        public static readonly VehicleInfo Sweeper = new VehicleInfo { Id = 574, Name = "Sweeper", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo Broadway = new VehicleInfo { Id = 575, Name = "Broadway", Category = VehicleCategory.Lowrider };
        public static readonly VehicleInfo Tornado = new VehicleInfo { Id = 576, Name = "Tornado", Category = VehicleCategory.Lowrider };
        public static readonly VehicleInfo AT400 = new VehicleInfo { Id = 577, Name = "AT400", Category = VehicleCategory.Airplane };
        public static readonly VehicleInfo DFT30 = new VehicleInfo { Id = 578, Name = "DFT-30", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo Huntley = new VehicleInfo { Id = 579, Name = "Huntley", Category = VehicleCategory.OffRoad };
        public static readonly VehicleInfo Stafford = new VehicleInfo { Id = 580, Name = "Stafford", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo BF400 = new VehicleInfo { Id = 581, Name = "BF-400", Category = VehicleCategory.Bike };
        public static readonly VehicleInfo Newsvan = new VehicleInfo { Id = 582, Name = "Newsvan", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo Tug = new VehicleInfo { Id = 583, Name = "Tug", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo PetrolTrailer = new VehicleInfo { Id = 584, Name = "Petrol Trailer", Category = VehicleCategory.Trailer };
        public static readonly VehicleInfo Emperor = new VehicleInfo { Id = 585, Name = "Emperor", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo Wayfarer = new VehicleInfo { Id = 586, Name = "Wayfarer", Category = VehicleCategory.Bike };
        public static readonly VehicleInfo Euros = new VehicleInfo { Id = 587, Name = "Euros", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo Hotdog = new VehicleInfo { Id = 588, Name = "Hotdog", Category = VehicleCategory.Unique };
        public static readonly VehicleInfo Club = new VehicleInfo { Id = 589, Name = "Club", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo FreightBoxTrailerTrain = new VehicleInfo { Id = 590, Name = "Freight Box Trailer (Train)", Category = VehicleCategory.TrainTrailer };
        public static readonly VehicleInfo ArticleTrailer3 = new VehicleInfo { Id = 591, Name = "Article Trailer 3", Category = VehicleCategory.Trailer };
        public static readonly VehicleInfo Andromada = new VehicleInfo { Id = 592, Name = "Andromada", Category = VehicleCategory.Airplane };
        public static readonly VehicleInfo Dodo = new VehicleInfo { Id = 593, Name = "Dodo", Category = VehicleCategory.Airplane };
        public static readonly VehicleInfo RCCam = new VehicleInfo { Id = 594, Name = "RC Cam", Category = VehicleCategory.RemoteControl };
        public static readonly VehicleInfo Launch = new VehicleInfo { Id = 595, Name = "Launch", Category = VehicleCategory.Boat };
        public static readonly VehicleInfo PoliceCarLSPD = new VehicleInfo { Id = 596, Name = "Police Car (LSPD)", Category = VehicleCategory.PublicService };
        public static readonly VehicleInfo PoliceCarSFPD = new VehicleInfo { Id = 597, Name = "Police Car (SFPD)", Category = VehicleCategory.PublicService };
        public static readonly VehicleInfo PoliceCarLVPD = new VehicleInfo { Id = 598, Name = "Police Car (LVPD)", Category = VehicleCategory.PublicService };
        public static readonly VehicleInfo PoliceRanger = new VehicleInfo { Id = 599, Name = "Police Ranger", Category = VehicleCategory.PublicService };
        public static readonly VehicleInfo Picador = new VehicleInfo { Id = 600, Name = "Picador", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo SWAT = new VehicleInfo { Id = 601, Name = "S.W.A.T.", Category = VehicleCategory.PublicService };
        public static readonly VehicleInfo Alpha = new VehicleInfo { Id = 602, Name = "Alpha", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo Phoenix = new VehicleInfo { Id = 603, Name = "Phoenix", Category = VehicleCategory.Sport };
        public static readonly VehicleInfo GlendaleShit = new VehicleInfo { Id = 604, Name = "Glendale Shit", Category = VehicleCategory.Saloon };
        public static readonly VehicleInfo SadlerShit = new VehicleInfo { Id = 605, Name = "Sadler Shit", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo BaggageTrailerA = new VehicleInfo { Id = 606, Name = "Baggage Trailer \"A\"", Category = VehicleCategory.Trailer };
        public static readonly VehicleInfo BaggageTrailerB = new VehicleInfo { Id = 607, Name = "Baggage Trailer \"B\"", Category = VehicleCategory.Trailer };
        public static readonly VehicleInfo TugStairsTrailer = new VehicleInfo { Id = 608, Name = "Tug Stairs Trailer", Category = VehicleCategory.Trailer };
        public static readonly VehicleInfo Boxville2 = new VehicleInfo { Id = 609, Name = "Boxville 2", Category = VehicleCategory.Industrial };
        public static readonly VehicleInfo FarmTrailer = new VehicleInfo { Id = 610, Name = "Farm Trailer", Category = VehicleCategory.Trailer };

        #endregion

        public int Id { get; private set; }
        public string Name { get; private set; }
        public VehicleCategory Category { get; private set; }
    }
}
