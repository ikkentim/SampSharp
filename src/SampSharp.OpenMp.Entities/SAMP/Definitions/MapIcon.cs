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

using System.Diagnostics.CodeAnalysis;

namespace SampSharp.Entities.SAMP;

/// <summary>Contains all map icons.</summary>
[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "CommentTypo")]
public enum MapIcon
{
    /// <summary>Can be used in any colour. Used for Single Player objectives.</summary>
    ColoredSquareTriangleDynamic = 0,

    /// <summary>2 times bigger than ID 0 and without the border.</summary>
    [Obsolete("This element will cause your game to crash if you have map legends enabled while viewing the map", false)]
    WhiteSquare = 1,

    /// <summary>Will be used on the minimap by default.</summary>
    [Obsolete("This element will cause your game to crash if you have map legends enabled while viewing the map", false)]
    PlayerPosition = 2,

    /// <summary>Your position when on the large map.</summary>
    PlayerMenuMap = 3,

    /// <summary>Always appears on the radar toward the north.</summary>
    [Obsolete("This element will cause your game to crash if you have map legends enabled while viewing the map", false)]
    North = 4,

    /// <summary>Air Yard</summary>
    AirYard = 5,

    /// <summary>Ammunation</summary>
    Ammunation = 6,

    /// <summary>Barber</summary>
    Barber = 7,

    /// <summary>Big Smoke</summary>
    BigSmoke = 8,

    /// <summary>Boat Yard</summary>
    BoatYard = 9,

    /// <summary>Burger Shot</summary>
    BurgerShot = 10,

    /// <summary>Quarry</summary>
    Quarry = 11,

    /// <summary>Catalina</summary>
    Catalina = 12,

    /// <summary>Cesar</summary>
    Cesar = 13,

    /// <summary>Cluckin' Bell</summary>
    CluckinBell = 14,

    /// <summary>Carl Johnson</summary>
    CarlJohnson = 15,

    /// <summary>C.R.A.S.H</summary>
    Crash = 16,

    /// <summary>Diner</summary>
    Diner = 17,

    /// <summary>Emmet</summary>
    Emmet = 18,

    /// <summary>Enemy Attack</summary>
    EnemyAttack = 19,

    /// <summary>Fire</summary>
    Fire = 20,

    /// <summary>Girlfriend</summary>
    Girlfriend = 21,

    /// <summary>Hospital</summary>
    Hospital = 22,

    /// <summary>Loco</summary>
    Loco = 23,

    /// <summary>Madd Dogg</summary>
    MaddDogg = 24,

    /// <summary>Caligulas</summary>
    Caligulas = 25,

    /// <summary>MC Loc</summary>
    McLoc = 26,

    /// <summary>Mod garage</summary>
    ModGarage = 27,

    /// <summary>OG Loc</summary>
    OgLoc = 28,

    /// <summary>Well Stacked Pizza Co</summary>
    WellStackedPizzaCo = 29,

    /// <summary>Police</summary>
    Police = 30,

    /// <summary>A property you're free to purchase.</summary>
    FreeProperty = 31,

    /// <summary>A property that isn't available for purchase.</summary>
    Property = 32,

    /// <summary>Race</summary>
    Race = 33,

    /// <summary>Ryder</summary>
    Ryder = 34,

    /// <summary>Used for safehouses where you save the game in singleplayer.</summary>
    SaveGame = 35,

    /// <summary>School</summary>
    School = 36,

    /// <summary>Unknown</summary>
    Unknown = 37,

    /// <summary>Sweet</summary>
    Sweet = 38,

    /// <summary>Tattoo</summary>
    Tattoo = 39,

    /// <summary>The Truth</summary>
    TheTruth = 40,

    /// <summary>Can be placed by players on the pause menu map by right-clicking</summary>
    Waypoint = 41,

    /// <summary>Toreno</summary>
    Toreno = 42,

    /// <summary>Triads</summary>
    Triads = 43,

    /// <summary>Triads Casino</summary>
    TriadsCasino = 44,

    /// <summary>Clothes</summary>
    Clothes = 45,

    /// <summary>Woozie</summary>
    Woozie = 46,

    /// <summary>Zero</summary>
    Zero = 47,

    /// <summary>Club</summary>
    Club = 48,

    /// <summary>Bar</summary>
    Bar = 49,

    /// <summary>Restaurant</summary>
    Restaurant = 50,

    /// <summary>Truck</summary>
    Truck = 51,

    /// <summary>Frequently used for banks.</summary>
    Robbery = 52,

    /// <summary>Race</summary>
    RaceFlag = 53,

    /// <summary>Gym</summary>
    Gym = 54,

    /// <summary>Car</summary>
    Car = 55,

    /// <summary>Light</summary>
    [Obsolete("This element will cause your game to crash if you have map legends enabled while viewing the map", false)]
    Light = 56,

    /// <summary>Closest airport</summary>
    ClosestAirport = 57,

    /// <summary>Varrios Los Aztecas</summary>
    VarriosLosAztecas = 58,

    /// <summary>Ballas</summary>
    Ballas = 59,

    /// <summary>Los Santos Vagos</summary>
    LosSantosVagos = 60,

    /// <summary>San Fierro Rifa</summary>
    SanFierroRifa = 61,

    /// <summary>Grove street</summary>
    GroveStreet = 62,

    /// <summary>Pay 'n' Spray</summary>
    PayNSpray = 63
}