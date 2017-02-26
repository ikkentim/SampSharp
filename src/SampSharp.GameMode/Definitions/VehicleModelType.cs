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

namespace SampSharp.GameMode.Definitions
{
    /// <summary>
    ///     Contains all vehicle models.
    /// </summary>
    public enum VehicleModelType
    {
        // ReSharper disable InconsistentNaming
        /// <summary>
        ///     Model of a Landstalker.
        /// </summary>
        Landstalker = 400,

        /// <summary>
        ///     Model of a Bravura.
        /// </summary>
        Bravura = 401,

        /// <summary>
        ///     Model of a Buffalo.
        /// </summary>
        Buffalo = 402,

        /// <summary>
        ///     Model of a Linerunner.
        /// </summary>
        Linerunner,

        /// <summary>
        ///     Model of a Perenniel.
        /// </summary>
        Perenniel,

        /// <summary>
        ///     Model of a Sentinel.
        /// </summary>
        Sentinel,

        /// <summary>
        ///     Model of a Dumper.
        /// </summary>
        Dumper,

        /// <summary>
        ///     Model of a Firetruck.
        /// </summary>
        Firetruck,

        /// <summary>
        ///     Model of a Trashmaster.
        /// </summary>
        Trashmaster,

        /// <summary>
        ///     Model of a Stretch.
        /// </summary>
        Stretch,

        /// <summary>
        ///     Model of a Manana.
        /// </summary>
        Manana,

        /// <summary>
        ///     Model of a Infernus.
        /// </summary>
        Infernus,

        /// <summary>
        ///     Model of a Voodoo.
        /// </summary>
        Voodoo,

        /// <summary>
        ///     Model of a Pony.
        /// </summary>
        Pony,

        /// <summary>
        ///     Model of a Mule.
        /// </summary>
        Mule,

        /// <summary>
        ///     Model of a Cheetah.
        /// </summary>
        Cheetah,

        /// <summary>
        ///     Model of a Ambulance.
        /// </summary>
        Ambulance,

        /// <summary>
        ///     Model of a Leviathan.
        /// </summary>
        Leviathan,

        /// <summary>
        ///     Model of a Moonbeam.
        /// </summary>
        Moonbeam,

        /// <summary>
        ///     Model of a Esperanto.
        /// </summary>
        Esperanto,

        /// <summary>
        ///     Model of a Taxi.
        /// </summary>
        Taxi,

        /// <summary>
        ///     Model of a Washington.
        /// </summary>
        Washington,

        /// <summary>
        ///     Model of a Bobcat.
        /// </summary>
        Bobcat,

        /// <summary>
        ///     Model of a Mr Whoopee.
        /// </summary>
        MrWhoopee,

        /// <summary>
        ///     Model of a BF Injection.
        /// </summary>
        BFInjection,

        /// <summary>
        ///     Model of a Hunter.
        /// </summary>
        Hunter,

        /// <summary>
        ///     Model of a Premier.
        /// </summary>
        Premier,

        /// <summary>
        ///     Model of a Enforcer.
        /// </summary>
        Enforcer,

        /// <summary>
        ///     Model of a Securicar.
        /// </summary>
        Securicar,

        /// <summary>
        ///     Model of a Banshee.
        /// </summary>
        Banshee,

        /// <summary>
        ///     Model of a Predator.
        /// </summary>
        Predator,

        /// <summary>
        ///     Model of a Bus.
        /// </summary>
        Bus,

        /// <summary>
        ///     Model of a Rhino.
        /// </summary>
        Rhino,

        /// <summary>
        ///     Model of a Barracks.
        /// </summary>
        Barracks,

        /// <summary>
        ///     Model of a Hotknife.
        /// </summary>
        Hotknife,

        /// <summary>
        ///     Model of a Article Trailer.
        /// </summary>
        ArticleTrailer,

        /// <summary>
        ///     Model of a Previon.
        /// </summary>
        Previon,

        /// <summary>
        ///     Model of a Coach.
        /// </summary>
        Coach,

        /// <summary>
        ///     Model of a Cabbie.
        /// </summary>
        Cabbie,

        /// <summary>
        ///     Model of a Stallion.
        /// </summary>
        Stallion,

        /// <summary>
        ///     Model of a Rumpo.
        /// </summary>
        Rumpo,

        /// <summary>
        ///     Model of a RC Bandit.
        /// </summary>
        RCBandit,

        /// <summary>
        ///     Model of a Romero.
        /// </summary>
        Romero,

        /// <summary>
        ///     Model of a Packer.
        /// </summary>
        Packer,

        /// <summary>
        ///     Model of a Monster.
        /// </summary>
        Monster,

        /// <summary>
        ///     Model of a Admiral.
        /// </summary>
        Admiral,

        /// <summary>
        ///     Model of a Squallo.
        /// </summary>
        Squallo,

        /// <summary>
        ///     Model of a Seasparrow.
        /// </summary>
        Seasparrow,

        /// <summary>
        ///     Model of a Pizzaboy.
        /// </summary>
        Pizzaboy,

        /// <summary>
        ///     Model of a Tram.
        /// </summary>
        Tram,

        /// <summary>
        ///     Model of a Article Trailer 2.
        /// </summary>
        ArticleTrailer2,

        /// <summary>
        ///     Model of a Turismo.
        /// </summary>
        Turismo,

        /// <summary>
        ///     Model of a Speeder.
        /// </summary>
        Speeder,

        /// <summary>
        ///     Model of a Reefer.
        /// </summary>
        Reefer,

        /// <summary>
        ///     Model of a Tropic.
        /// </summary>
        Tropic,

        /// <summary>
        ///     Model of a Flatbed.
        /// </summary>
        Flatbed,

        /// <summary>
        ///     Model of a Yankee.
        /// </summary>
        Yankee,

        /// <summary>
        ///     Model of a Caddy.
        /// </summary>
        Caddy,

        /// <summary>
        ///     Model of a Solair.
        /// </summary>
        Solair,

        /// <summary>
        ///     Model of a Topfun Van Berkleys RC.
        /// </summary>
        TopfunVanBerkleysRC,

        /// <summary>
        ///     Model of a Skimmer.
        /// </summary>
        Skimmer,

        /// <summary>
        ///     Model of a PCJ-600.
        /// </summary>
        PCJ600,

        /// <summary>
        ///     Model of a Faggio.
        /// </summary>
        Faggio,

        /// <summary>
        ///     Model of a Freeway.
        /// </summary>
        Freeway,

        /// <summary>
        ///     Model of a RC Baron.
        /// </summary>
        RCBaron,

        /// <summary>
        ///     Model of a RC Raider.
        /// </summary>
        RCRaider,

        /// <summary>
        ///     Model of a Glendale.
        /// </summary>
        Glendale,

        /// <summary>
        ///     Model of a Oceanic.
        /// </summary>
        Oceanic,

        /// <summary>
        ///     Model of a Sanchez.
        /// </summary>
        Sanchez,

        /// <summary>
        ///     Model of a Sparrow.
        /// </summary>
        Sparrow,

        /// <summary>
        ///     Model of a Patriot.
        /// </summary>
        Patriot,

        /// <summary>
        ///     Model of a Quad.
        /// </summary>
        Quad,

        /// <summary>
        ///     Model of a Coastguard.
        /// </summary>
        Coastguard,

        /// <summary>
        ///     Model of a Dinghy.
        /// </summary>
        Dinghy,

        /// <summary>
        ///     Model of a Hermes.
        /// </summary>
        Hermes,

        /// <summary>
        ///     Model of a Sabre.
        /// </summary>
        Sabre,

        /// <summary>
        ///     Model of a Rustler.
        /// </summary>
        Rustler,

        /// <summary>
        ///     Model of a ZR350.
        /// </summary>
        ZR350,

        /// <summary>
        ///     Model of a Walton.
        /// </summary>
        Walton,

        /// <summary>
        ///     Model of a Regina.
        /// </summary>
        Regina,

        /// <summary>
        ///     Model of a Comet.
        /// </summary>
        Comet,

        /// <summary>
        ///     Model of a BMX.
        /// </summary>
        BMX,

        /// <summary>
        ///     Model of a Burrito.
        /// </summary>
        Burrito,

        /// <summary>
        ///     Model of a Camper.
        /// </summary>
        Camper,

        /// <summary>
        ///     Model of a Marquis.
        /// </summary>
        Marquis,

        /// <summary>
        ///     Model of a Baggage.
        /// </summary>
        Baggage,

        /// <summary>
        ///     Model of a Dozer.
        /// </summary>
        Dozer,

        /// <summary>
        ///     Model of a Maverick.
        /// </summary>
        Maverick,

        /// <summary>
        ///     Model of a SAN News Maverick.
        /// </summary>
        SANNewsMaverick,

        /// <summary>
        ///     Model of a Rancher.
        /// </summary>
        Rancher,

        /// <summary>
        ///     Model of a FBI Rancher.
        /// </summary>
        FBIRancher,

        /// <summary>
        ///     Model of a Virgo.
        /// </summary>
        Virgo,

        /// <summary>
        ///     Model of a Greenwood.
        /// </summary>
        Greenwood,

        /// <summary>
        ///     Model of a Jetmax.
        /// </summary>
        Jetmax,

        /// <summary>
        ///     Model of a Hotring Racer.
        /// </summary>
        HotringRacer,

        /// <summary>
        ///     Model of a Sandking.
        /// </summary>
        Sandking,

        /// <summary>
        ///     Model of a Blista Compact.
        /// </summary>
        BlistaCompact,

        /// <summary>
        ///     Model of a Police Maverick.
        /// </summary>
        PoliceMaverick,

        /// <summary>
        ///     Model of a Boxville.
        /// </summary>
        Boxville,

        /// <summary>
        ///     Model of a Benson.
        /// </summary>
        Benson,

        /// <summary>
        ///     Model of a Mesa.
        /// </summary>
        Mesa,

        /// <summary>
        ///     Model of a RC Goblin.
        /// </summary>
        RCGoblin,

        /// <summary>
        ///     Model of a Hotring Racer 2.
        /// </summary>
        HotringRacer2,

        /// <summary>
        ///     Model of a Hotring Racer 3.
        /// </summary>
        HotringRacer3,

        /// <summary>
        ///     Model of a Bloodring Banger.
        /// </summary>
        BloodringBanger,

        /// <summary>
        ///     Model of a Rancher 2.
        /// </summary>
        Rancher2,

        /// <summary>
        ///     Model of a Super GT.
        /// </summary>
        SuperGT,

        /// <summary>
        ///     Model of a Elegant.
        /// </summary>
        Elegant,

        /// <summary>
        ///     Model of a Journey.
        /// </summary>
        Journey,

        /// <summary>
        ///     Model of a Bike.
        /// </summary>
        Bike,

        /// <summary>
        ///     Model of a Mountain Bike.
        /// </summary>
        MountainBike,

        /// <summary>
        ///     Model of a Beagle.
        /// </summary>
        Beagle,

        /// <summary>
        ///     Model of a Cropduster.
        /// </summary>
        Cropduster,

        /// <summary>
        ///     Model of a Stuntplane.
        /// </summary>
        Stuntplane,

        /// <summary>
        ///     Model of a Tanker.
        /// </summary>
        Tanker,

        /// <summary>
        ///     Model of a Roadtrain.
        /// </summary>
        Roadtrain,

        /// <summary>
        ///     Model of a Nebula.
        /// </summary>
        Nebula,

        /// <summary>
        ///     Model of a Majestic.
        /// </summary>
        Majestic,

        /// <summary>
        ///     Model of a Buccaneer.
        /// </summary>
        Buccaneer,

        /// <summary>
        ///     Model of a Shamal.
        /// </summary>
        Shamal,

        /// <summary>
        ///     Model of a Hydra.
        /// </summary>
        Hydra,

        /// <summary>
        ///     Model of a FCR-900.
        /// </summary>
        FCR900,

        /// <summary>
        ///     Model of a NRG-500.
        /// </summary>
        NRG500,

        /// <summary>
        ///     Model of a HPV1000.
        /// </summary>
        HPV1000,

        /// <summary>
        ///     Model of a Cement Truck.
        /// </summary>
        CementTruck,

        /// <summary>
        ///     Model of a Towtruck.
        /// </summary>
        Towtruck,

        /// <summary>
        ///     Model of a Fortune.
        /// </summary>
        Fortune,

        /// <summary>
        ///     Model of a Cadrona.
        /// </summary>
        Cadrona,

        /// <summary>
        ///     Model of a FBITruck.
        /// </summary>
        FBITruck,

        /// <summary>
        ///     Model of a Willard.
        /// </summary>
        Willard,

        /// <summary>
        ///     Model of a Forklift.
        /// </summary>
        Forklift,

        /// <summary>
        ///     Model of a Tractor.
        /// </summary>
        Tractor,

        /// <summary>
        ///     Model of a Combine Harvester.
        /// </summary>
        CombineHarvester,

        /// <summary>
        ///     Model of a Feltzer.
        /// </summary>
        Feltzer,

        /// <summary>
        ///     Model of a Remington.
        /// </summary>
        Remington,

        /// <summary>
        ///     Model of a Slamvan.
        /// </summary>
        Slamvan,

        /// <summary>
        ///     Model of a Blade.
        /// </summary>
        Blade,

        /// <summary>
        ///     Model of a Freight Train.
        /// </summary>
        FreightTrain,

        /// <summary>
        ///     Model of a Brownstreak Train.
        /// </summary>
        BrownstreakTrain,

        /// <summary>
        ///     Model of a Vortex.
        /// </summary>
        Vortex,

        /// <summary>
        ///     Model of a Vincent.
        /// </summary>
        Vincent,

        /// <summary>
        ///     Model of a Bullet.
        /// </summary>
        Bullet,

        /// <summary>
        ///     Model of a Clover.
        /// </summary>
        Clover,

        /// <summary>
        ///     Model of a Sadler.
        /// </summary>
        Sadler,

        /// <summary>
        ///     Model of a Firetruck LA.
        /// </summary>
        FiretruckLA,

        /// <summary>
        ///     Model of a Hustler.
        /// </summary>
        Hustler,

        /// <summary>
        ///     Model of a Intruder.
        /// </summary>
        Intruder,

        /// <summary>
        ///     Model of a Primo.
        /// </summary>
        Primo,

        /// <summary>
        ///     Model of a Cargobob.
        /// </summary>
        Cargobob,

        /// <summary>
        ///     Model of a Tampa.
        /// </summary>
        Tampa,

        /// <summary>
        ///     Model of a Sunrise.
        /// </summary>
        Sunrise,

        /// <summary>
        ///     Model of a Merit.
        /// </summary>
        Merit,

        /// <summary>
        ///     Model of a Utility Van.
        /// </summary>
        UtilityVan,

        /// <summary>
        ///     Model of a Nevada.
        /// </summary>
        Nevada,

        /// <summary>
        ///     Model of a Yosemite.
        /// </summary>
        Yosemite,

        /// <summary>
        ///     Model of a Windsor.
        /// </summary>
        Windsor,

        /// <summary>
        ///     Model of a Monster A.
        /// </summary>
        MonsterA,

        /// <summary>
        ///     Model of a Monster B.
        /// </summary>
        MonsterB,

        /// <summary>
        ///     Model of a Uranus.
        /// </summary>
        Uranus,

        /// <summary>
        ///     Model of a Jester.
        /// </summary>
        Jester,

        /// <summary>
        ///     Model of a Sultan.
        /// </summary>
        Sultan,

        /// <summary>
        ///     Model of a Stratum.
        /// </summary>
        Stratum,

        /// <summary>
        ///     Model of a Elegy.
        /// </summary>
        Elegy,

        /// <summary>
        ///     Model of a Raindance.
        /// </summary>
        Raindance,

        /// <summary>
        ///     Model of a RC Tiger.
        /// </summary>
        RCTiger,

        /// <summary>
        ///     Model of a Flash.
        /// </summary>
        Flash,

        /// <summary>
        ///     Model of a Tahoma.
        /// </summary>
        Tahoma,

        /// <summary>
        ///     Model of a Savanna.
        /// </summary>
        Savanna,

        /// <summary>
        ///     Model of a Bandito.
        /// </summary>
        Bandito,

        /// <summary>
        ///     Model of a Freight Flat Trailer Train.
        /// </summary>
        FreightFlatTrailerTrain,

        /// <summary>
        ///     Model of a Streak Trailer Train.
        /// </summary>
        StreakTrailerTrain,

        /// <summary>
        ///     Model of a Kart.
        /// </summary>
        Kart,

        /// <summary>
        ///     Model of a Mower.
        /// </summary>
        Mower,

        /// <summary>
        ///     Model of a Dune.
        /// </summary>
        Dune,

        /// <summary>
        ///     Model of a Sweeper.
        /// </summary>
        Sweeper,

        /// <summary>
        ///     Model of a Broadway.
        /// </summary>
        Broadway,

        /// <summary>
        ///     Model of a Tornado.
        /// </summary>
        Tornado,

        /// <summary>
        ///     Model of a AT400.
        /// </summary>
        AT400,

        /// <summary>
        ///     Model of a DFT-30.
        /// </summary>
        DFT30,

        /// <summary>
        ///     Model of a Huntley.
        /// </summary>
        Huntley,

        /// <summary>
        ///     Model of a Stafford.
        /// </summary>
        Stafford,

        /// <summary>
        ///     Model of a BF400.
        /// </summary>
        BF400,

        /// <summary>
        ///     Model of a Newsvan.
        /// </summary>
        Newsvan,

        /// <summary>
        ///     Model of a Tug.
        /// </summary>
        Tug,

        /// <summary>
        ///     Model of a Petrol Trailer.
        /// </summary>
        PetrolTrailer,

        /// <summary>
        ///     Model of a Emperor.
        /// </summary>
        Emperor,

        /// <summary>
        ///     Model of a Wayfarer.
        /// </summary>
        Wayfarer,

        /// <summary>
        ///     Model of a Euros.
        /// </summary>
        Euros,

        /// <summary>
        ///     Model of a Hotdog.
        /// </summary>
        Hotdog,

        /// <summary>
        ///     Model of a Club.
        /// </summary>
        Club,

        /// <summary>
        ///     Model of a Freight Box Trailer Train.
        /// </summary>
        FreightBoxTrailerTrain,

        /// <summary>
        ///     Model of a Article Trailer 3.
        /// </summary>
        ArticleTrailer3,

        /// <summary>
        ///     Model of a Andromada.
        /// </summary>
        Andromada,

        /// <summary>
        ///     Model of a Dodo.
        /// </summary>
        Dodo,

        /// <summary>
        ///     Model of a RC Cam.
        /// </summary>
        RCCam,

        /// <summary>
        ///     Model of a Launch.
        /// </summary>
        Launch,

        /// <summary>
        ///     Model of a Police Car LSPD.
        /// </summary>
        PoliceCarLSPD,

        /// <summary>
        ///     Model of a Police Car SFPD.
        /// </summary>
        PoliceCarSFPD,

        /// <summary>
        ///     Model of a Police Car LVPD.
        /// </summary>
        PoliceCarLVPD,

        /// <summary>
        ///     Model of a Police Ranger.
        /// </summary>
        PoliceRanger,

        /// <summary>
        ///     Model of a Picador.
        /// </summary>
        Picador,

        /// <summary>
        ///     Model of a SWAT Truck.
        /// </summary>
        SWAT,

        /// <summary>
        ///     Model of a Alpha.
        /// </summary>
        Alpha,

        /// <summary>
        ///     Model of a Phoenix.
        /// </summary>
        Phoenix,

        /// <summary>
        ///     Model of a Damaged Glendale.
        /// </summary>
        GlendaleShit,

        /// <summary>
        ///     Model of a Damaged Sadler.
        /// </summary>
        SadlerShit,

        /// <summary>
        ///     Model of a Baggage Trailer A.
        /// </summary>
        BaggageTrailerA,

        /// <summary>
        ///     Model of a Baggage Trailer B.
        /// </summary>
        BaggageTrailerB,

        /// <summary>
        ///     Model of a Tug Stairs Trailer.
        /// </summary>
        TugStairsTrailer,

        /// <summary>
        ///     Model of a Boxville 2.
        /// </summary>
        Boxville2,

        /// <summary>
        ///     Model of a Farm Trailer.
        /// </summary>
        FarmTrailer,

        /// <summary>
        ///     Model of a Utility Trailer.
        /// </summary>
        UtilityTrailer
        // ReSharper restore InconsistentNaming
    }
}