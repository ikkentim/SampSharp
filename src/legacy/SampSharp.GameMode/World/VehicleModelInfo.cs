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
using System.Collections.Generic;
using System.Linq;
using SampSharp.GameMode.Definitions;

// ReSharper disable StringLiteralTypo
namespace SampSharp.GameMode.World;

/// <summary>Contains vehicle category infos.</summary>
public readonly partial struct VehicleModelInfo
{
    private static readonly VehicleModelInfo[] _vehicleModelInfos =
    {
        // TODO: Use VehicleModelType values instead of numeric casted values
        new((VehicleModelType)400, "Landstalker", VehicleCategory.OffRoad, 4),
        new((VehicleModelType)401, "Bravura", VehicleCategory.Saloon, 2),
        new((VehicleModelType)402, "Buffalo", VehicleCategory.Sport, 2),
        new((VehicleModelType)403, "Linerunner", VehicleCategory.Industrial, 2),
        new((VehicleModelType)404, "Perenniel", VehicleCategory.Station, 4),
        new((VehicleModelType)405, "Sentinel", VehicleCategory.Saloon, 4),
        new((VehicleModelType)406, "Dumper", VehicleCategory.Unique, 1),
        new((VehicleModelType)407, "Firetruck", VehicleCategory.PublicService, 2),
        new((VehicleModelType)408, "Trashmaster", VehicleCategory.Industrial, 2),
        new((VehicleModelType)409, "Stretch", VehicleCategory.Unique, 4),
        new((VehicleModelType)410, "Manana", VehicleCategory.Saloon, 2),
        new((VehicleModelType)411, "Infernus", VehicleCategory.Sport, 2),
        new((VehicleModelType)412, "Voodoo", VehicleCategory.Lowrider, 2),
        new((VehicleModelType)413, "Pony", VehicleCategory.Industrial, 4),
        new((VehicleModelType)414, "Mule", VehicleCategory.Industrial, 2),
        new((VehicleModelType)415, "Cheetah", VehicleCategory.Sport, 2),
        new((VehicleModelType)416, "Ambulance", VehicleCategory.PublicService, 4),
        new((VehicleModelType)417, "Leviathan", VehicleCategory.Helicopter, 2),
        new((VehicleModelType)418, "Moonbeam", VehicleCategory.Station, 4),
        new((VehicleModelType)419, "Esperanto", VehicleCategory.Saloon, 2),
        new((VehicleModelType)420, "Taxi", VehicleCategory.PublicService, 4),
        new((VehicleModelType)421, "Washington", VehicleCategory.Saloon, 4),
        new((VehicleModelType)422, "Bobcat", VehicleCategory.Industrial, 2),
        new((VehicleModelType)423, "Mr Whoopee", VehicleCategory.Unique, 2),
        new((VehicleModelType)424, "BF Injection", VehicleCategory.OffRoad, 2),
        new((VehicleModelType)425, "Hunter", VehicleCategory.Helicopter, 1),
        new((VehicleModelType)426, "Premier", VehicleCategory.Saloon, 4),
        new((VehicleModelType)427, "Enforcer", VehicleCategory.PublicService, 4),
        new((VehicleModelType)428, "Securicar", VehicleCategory.Unique, 4),
        new((VehicleModelType)429, "Banshee", VehicleCategory.Sport, 2),
        new((VehicleModelType)430, "Predator", VehicleCategory.Boat, 1),
        new((VehicleModelType)431, "Bus", VehicleCategory.PublicService, 9),
        new((VehicleModelType)432, "Rhino", VehicleCategory.PublicService, 1),
        new((VehicleModelType)433, "Barracks", VehicleCategory.PublicService, 2),
        new((VehicleModelType)434, "Hotknife", VehicleCategory.Unique, 2),
        new((VehicleModelType)435, "Article Trailer", VehicleCategory.Trailer, 0),
        new((VehicleModelType)436, "Previon", VehicleCategory.Saloon, 2),
        new((VehicleModelType)437, "Coach", VehicleCategory.PublicService, 9),
        new((VehicleModelType)438, "Cabbie", VehicleCategory.PublicService, 4),
        new((VehicleModelType)439, "Stallion", VehicleCategory.Convertible, 2),
        new((VehicleModelType)440, "Rumpo", VehicleCategory.Industrial, 4),
        new((VehicleModelType)441, "RC Bandit", VehicleCategory.RemoteControl, 1),
        new((VehicleModelType)442, "Romero", VehicleCategory.Unique, 2),
        new((VehicleModelType)443, "Packer", VehicleCategory.Industrial, 2),
        new((VehicleModelType)444, "Monster", VehicleCategory.OffRoad, 2),
        new((VehicleModelType)445, "Admiral", VehicleCategory.Saloon, 4),
        new((VehicleModelType)446, "Squallo", VehicleCategory.Boat, 1),
        new((VehicleModelType)447, "Seasparrow", VehicleCategory.Helicopter, 2),
        new((VehicleModelType)448, "Pizzaboy", VehicleCategory.Bike, 1),
        new((VehicleModelType)449, "Tram", VehicleCategory.Unique, 6),
        new((VehicleModelType)450, "Article Trailer 2", VehicleCategory.Trailer, 0),
        new((VehicleModelType)451, "Turismo", VehicleCategory.Sport, 2),
        new((VehicleModelType)452, "Speeder", VehicleCategory.Boat, 1),
        new((VehicleModelType)453, "Reefer", VehicleCategory.Boat, 1),
        new((VehicleModelType)454, "Tropic", VehicleCategory.Boat, 1),
        new((VehicleModelType)455, "Flatbed", VehicleCategory.Industrial, 2),
        new((VehicleModelType)456, "Yankee", VehicleCategory.Industrial, 2),
        new((VehicleModelType)457, "Caddy", VehicleCategory.Unique, 2),
        new((VehicleModelType)458, "Solair", VehicleCategory.Station, 4),
        new((VehicleModelType)459, "Topfun Van (Berkley's RC)", VehicleCategory.Industrial, 4),
        new((VehicleModelType)460, "Skimmer", VehicleCategory.Airplane, 2),
        new((VehicleModelType)461, "PCJ-600", VehicleCategory.Bike, 2),
        new((VehicleModelType)462, "Faggio", VehicleCategory.Bike, 2),
        new((VehicleModelType)463, "Freeway", VehicleCategory.Bike, 2),
        new((VehicleModelType)464, "RC Baron", VehicleCategory.RemoteControl, 2),
        new((VehicleModelType)465, "RC Raider", VehicleCategory.RemoteControl, 2),
        new((VehicleModelType)466, "Glendale", VehicleCategory.Saloon, 4),
        new((VehicleModelType)467, "Oceanic", VehicleCategory.Saloon, 4),
        new((VehicleModelType)468, "Sanchez", VehicleCategory.Bike, 2),
        new((VehicleModelType)469, "Sparrow", VehicleCategory.Helicopter, 2),
        new((VehicleModelType)470, "Patriot", VehicleCategory.OffRoad, 4),
        new((VehicleModelType)471, "Quad", VehicleCategory.Bike, 2),
        new((VehicleModelType)472, "Coastguard", VehicleCategory.Boat, 1),
        new((VehicleModelType)473, "Dinghy", VehicleCategory.Boat, 1),
        new((VehicleModelType)474, "Hermes", VehicleCategory.Saloon, 2),
        new((VehicleModelType)475, "Sabre", VehicleCategory.Sport, 2),
        new((VehicleModelType)476, "Rustler", VehicleCategory.Airplane, 1),
        new((VehicleModelType)477, "ZR-350", VehicleCategory.Sport, 2),
        new((VehicleModelType)478, "Walton", VehicleCategory.Industrial, 2),
        new((VehicleModelType)479, "Regina", VehicleCategory.Station, 4),
        new((VehicleModelType)480, "Comet", VehicleCategory.Convertible, 2),
        new((VehicleModelType)481, "BMX", VehicleCategory.Bike, 1),
        new((VehicleModelType)482, "Burrito", VehicleCategory.Industrial, 4),
        new((VehicleModelType)483, "Camper", VehicleCategory.Unique, 3),
        new((VehicleModelType)484, "Marquis", VehicleCategory.Boat, 1),
        new((VehicleModelType)485, "Baggage", VehicleCategory.Unique, 1),
        new((VehicleModelType)486, "Dozer", VehicleCategory.Unique, 1),
        new((VehicleModelType)487, "Maverick", VehicleCategory.Helicopter, 4),
        new((VehicleModelType)488, "SAN News Maverick", VehicleCategory.Helicopter, 2),
        new((VehicleModelType)489, "Rancher", VehicleCategory.OffRoad, 2),
        new((VehicleModelType)490, "FBI Rancher", VehicleCategory.PublicService, 4),
        new((VehicleModelType)491, "Virgo", VehicleCategory.Saloon, 2),
        new((VehicleModelType)492, "Greenwood", VehicleCategory.Saloon, 4),
        new((VehicleModelType)493, "Jetmax", VehicleCategory.Boat, 1),
        new((VehicleModelType)494, "Hotring Racer", VehicleCategory.Sport, 2),
        new((VehicleModelType)495, "Sandking", VehicleCategory.OffRoad, 2),
        new((VehicleModelType)496, "Blista Compact", VehicleCategory.Sport, 2),
        new((VehicleModelType)497, "Police Maverick", VehicleCategory.Helicopter, 4),
        new((VehicleModelType)498, "Boxville", VehicleCategory.Industrial, 4),
        new((VehicleModelType)499, "Benson", VehicleCategory.Industrial, 2),
        new((VehicleModelType)500, "Mesa", VehicleCategory.OffRoad, 2),
        new((VehicleModelType)501, "RC Goblin", VehicleCategory.RemoteControl, 2),
        new((VehicleModelType)502, "Hotring Racer 2", VehicleCategory.Sport, 2),
        new((VehicleModelType)503, "Hotring Racer 3", VehicleCategory.Sport, 2),
        new((VehicleModelType)504, "Bloodring Banger", VehicleCategory.Saloon, 2),
        new((VehicleModelType)505, "Rancher", VehicleCategory.OffRoad, 2),
        new((VehicleModelType)506, "Super GT", VehicleCategory.Sport, 2),
        new((VehicleModelType)507, "Elegant", VehicleCategory.Saloon, 4),
        new((VehicleModelType)508, "Journey", VehicleCategory.Unique, 2),
        new((VehicleModelType)509, "Bike", VehicleCategory.Bike, 1),
        new((VehicleModelType)510, "Mountain Bike", VehicleCategory.Bike, 1),
        new((VehicleModelType)511, "Beagle", VehicleCategory.Airplane, 2),
        new((VehicleModelType)512, "Cropduster", VehicleCategory.Airplane, 1),
        new((VehicleModelType)513, "Stuntplane", VehicleCategory.Airplane, 1),
        new((VehicleModelType)514, "Tanker", VehicleCategory.Industrial, 2),
        new((VehicleModelType)515, "Roadtrain", VehicleCategory.Industrial, 2),
        new((VehicleModelType)516, "Nebula", VehicleCategory.Saloon, 4),
        new((VehicleModelType)517, "Majestic", VehicleCategory.Saloon, 2),
        new((VehicleModelType)518, "Buccaneer", VehicleCategory.Saloon, 2),
        new((VehicleModelType)519, "Shamal", VehicleCategory.Airplane, 1),
        new((VehicleModelType)520, "Hydra", VehicleCategory.Airplane, 1),
        new((VehicleModelType)521, "FCR-900", VehicleCategory.Bike, 2),
        new((VehicleModelType)522, "NRG-500", VehicleCategory.Bike, 2),
        new((VehicleModelType)523, "HPV1000", VehicleCategory.PublicService, 2),
        new((VehicleModelType)524, "Cement Truck", VehicleCategory.Industrial, 2),
        new((VehicleModelType)525, "Towtruck", VehicleCategory.Unique, 2),
        new((VehicleModelType)526, "Fortune", VehicleCategory.Saloon, 2),
        new((VehicleModelType)527, "Cadrona", VehicleCategory.Saloon, 2),
        new((VehicleModelType)528, "FBI Truck", VehicleCategory.PublicService, 2),
        new((VehicleModelType)529, "Willard", VehicleCategory.Saloon, 4),
        new((VehicleModelType)530, "Forklift", VehicleCategory.Unique, 1),
        new((VehicleModelType)531, "Tractor", VehicleCategory.Industrial, 1),
        new((VehicleModelType)532, "Combine Harvester", VehicleCategory.Unique, 1),
        new((VehicleModelType)533, "Feltzer", VehicleCategory.Convertible, 2),
        new((VehicleModelType)534, "Remington", VehicleCategory.Lowrider, 2),
        new((VehicleModelType)535, "Slamvan", VehicleCategory.Lowrider, 2),
        new((VehicleModelType)536, "Blade", VehicleCategory.Lowrider, 2),
        new((VehicleModelType)537, "Freight (Train)", VehicleCategory.Unique, 6),
        new((VehicleModelType)538, "Brownstreak (Train)", VehicleCategory.Unique, 6),
        new((VehicleModelType)539, "Vortex", VehicleCategory.Unique, 1),
        new((VehicleModelType)540, "Vincent", VehicleCategory.Saloon, 4),
        new((VehicleModelType)541, "Bullet", VehicleCategory.Sport, 2),
        new((VehicleModelType)542, "Clover", VehicleCategory.Saloon, 2),
        new((VehicleModelType)543, "Sadler", VehicleCategory.Industrial, 2),
        new((VehicleModelType)544, "Firetruck LA", VehicleCategory.PublicService, 2),
        new((VehicleModelType)545, "Hustler", VehicleCategory.Unique, 2),
        new((VehicleModelType)546, "Intruder", VehicleCategory.Saloon, 4),
        new((VehicleModelType)547, "Primo", VehicleCategory.Saloon, 4),
        new((VehicleModelType)548, "Cargobob", VehicleCategory.Helicopter, 2),
        new((VehicleModelType)549, "Tampa", VehicleCategory.Saloon, 2),
        new((VehicleModelType)550, "Sunrise", VehicleCategory.Saloon, 4),
        new((VehicleModelType)551, "Merit", VehicleCategory.Saloon, 4),
        new((VehicleModelType)552, "Utility Van", VehicleCategory.Industrial, 2),
        new((VehicleModelType)553, "Nevada", VehicleCategory.Airplane, 1),
        new((VehicleModelType)554, "Yosemite", VehicleCategory.Industrial, 2),
        new((VehicleModelType)555, "Windsor", VehicleCategory.Convertible, 2),
        new((VehicleModelType)556, "Monster \"A\"", VehicleCategory.OffRoad, 2),
        new((VehicleModelType)557, "Monster \"B\"", VehicleCategory.OffRoad, 2),
        new((VehicleModelType)558, "Uranus", VehicleCategory.Sport, 2),
        new((VehicleModelType)559, "Jester", VehicleCategory.Sport, 2),
        new((VehicleModelType)560, "Sultan", VehicleCategory.Saloon, 4),
        new((VehicleModelType)561, "Stratum", VehicleCategory.Station, 4),
        new((VehicleModelType)562, "Elegy", VehicleCategory.Saloon, 2),
        new((VehicleModelType)563, "Raindance", VehicleCategory.Helicopter, 2),
        new((VehicleModelType)564, "RC Tiger", VehicleCategory.RemoteControl, 1),
        new((VehicleModelType)565, "Flash", VehicleCategory.Sport, 2),
        new((VehicleModelType)566, "Tahoma", VehicleCategory.Lowrider, 4),
        new((VehicleModelType)567, "Savanna", VehicleCategory.Lowrider, 4),
        new((VehicleModelType)568, "Bandito", VehicleCategory.OffRoad, 1),
        new((VehicleModelType)569, "Freight Flat Trailer (Train)", VehicleCategory.TrainTrailer, 0),
        new((VehicleModelType)570, "Streak Trailer (Train)", VehicleCategory.TrainTrailer, 5),
        new((VehicleModelType)571, "Kart", VehicleCategory.Unique, 1),
        new((VehicleModelType)572, "Mower", VehicleCategory.Unique, 1),
        new((VehicleModelType)573, "Dune", VehicleCategory.OffRoad, 2),
        new((VehicleModelType)574, "Sweeper", VehicleCategory.Unique, 1),
        new((VehicleModelType)575, "Broadway", VehicleCategory.Lowrider, 2),
        new((VehicleModelType)576, "Tornado", VehicleCategory.Lowrider, 2),
        new((VehicleModelType)577, "AT400", VehicleCategory.Airplane, 2),
        new((VehicleModelType)578, "DFT-30", VehicleCategory.Industrial, 2),
        new((VehicleModelType)579, "Huntley", VehicleCategory.OffRoad, 4),
        new((VehicleModelType)580, "Stafford", VehicleCategory.Saloon, 4),
        new((VehicleModelType)581, "BF-400", VehicleCategory.Bike, 2),
        new((VehicleModelType)582, "Newsvan", VehicleCategory.Industrial, 4),
        new((VehicleModelType)583, "Tug", VehicleCategory.Unique, 1),
        new((VehicleModelType)584, "Petrol Trailer", VehicleCategory.Trailer, 0),
        new((VehicleModelType)585, "Emperor", VehicleCategory.Saloon, 4),
        new((VehicleModelType)586, "Wayfarer", VehicleCategory.Bike, 2),
        new((VehicleModelType)587, "Euros", VehicleCategory.Sport, 2),
        new((VehicleModelType)588, "Hotdog", VehicleCategory.Unique, 2),
        new((VehicleModelType)589, "Club", VehicleCategory.Sport, 2),
        new((VehicleModelType)590, "Freight Box Trailer (Train)", VehicleCategory.TrainTrailer, 0),
        new((VehicleModelType)591, "Article Trailer 3", VehicleCategory.Trailer, 0),
        new((VehicleModelType)592, "Andromada", VehicleCategory.Airplane, 2),
        new((VehicleModelType)593, "Dodo", VehicleCategory.Airplane, 2),
        new((VehicleModelType)594, "RC Cam", VehicleCategory.RemoteControl, 2),
        new((VehicleModelType)595, "Launch", VehicleCategory.Boat, 1),
        new((VehicleModelType)596, "Police Car (LSPD)", VehicleCategory.PublicService, 4),
        new((VehicleModelType)597, "Police Car (SFPD)", VehicleCategory.PublicService, 4),
        new((VehicleModelType)598, "Police Car (LVPD)", VehicleCategory.PublicService, 4),
        new((VehicleModelType)599, "Police Ranger", VehicleCategory.PublicService, 2),
        new((VehicleModelType)600, "Picador", VehicleCategory.Industrial, 2),
        new((VehicleModelType)601, "S.W.A.T.", VehicleCategory.PublicService, 2),
        new((VehicleModelType)602, "Alpha", VehicleCategory.Sport, 2),
        new((VehicleModelType)603, "Phoenix", VehicleCategory.Sport, 2),
        new((VehicleModelType)604, "Glendale Shit", VehicleCategory.Saloon, 4),
        new((VehicleModelType)605, "Sadler Shit", VehicleCategory.Industrial, 2),
        new((VehicleModelType)606, "Baggage Trailer \"A\"", VehicleCategory.Trailer, 0),
        new((VehicleModelType)607, "Baggage Trailer \"B\"", VehicleCategory.Trailer, 0),
        new((VehicleModelType)608, "Tug Stairs Trailer", VehicleCategory.Trailer, 0),
        new((VehicleModelType)609, "Boxville 2", VehicleCategory.Industrial, 4),
        new((VehicleModelType)610, "Farm Trailer", VehicleCategory.Trailer, 0),
        new((VehicleModelType)611, "Utility Trailer", VehicleCategory.Trailer, 0)
    };

    /// <summary>Initializes a new instance of the <see cref="VehicleModelInfo" /> class.</summary>
    private VehicleModelInfo(VehicleModelType type, string name, VehicleCategory category, int seatCount) : this()
    {
        Type = type;
        Name = name;
        Category = category;
        SeatCount = seatCount;
    }

    /// <summary>Gets the type of this <see cref="VehicleModelInfo" />.</summary>
    public VehicleModelType Type { get; }

    /// <summary>Gets the name of this <see cref="VehicleModelInfo" />.</summary>
    public string Name { get; }

    /// <summary>Gets the category of this <see cref="VehicleModelInfo" />.</summary>
    public VehicleCategory Category { get; }

    /// <summary>Gets the seats number of this <see cref="VehicleModelInfo" />.</summary>
    public int SeatCount { get; }

    /// <summary>Gets model information of the given type.</summary>
    /// <param name="infoType">The type of information to retrieve.</param>
    public Vector3 this[VehicleModelInfoType infoType]
    {
        get
        {
            VehicleModelInfoInternal.Instance.GetVehicleModelInfo((int)Type, (int)infoType, out var x, out var y, out var z);
            return new Vector3(x, y, z);
        }
    }

    /// <summary>Returns an instance of <see cref="VehicleModelInfo" /> containing information about the specified vehicle.</summary>
    /// <param name="vehicle">The vehicle to find information about.</param>
    /// <returns>An instance of <see cref="VehicleModelInfo" /> containing information about the specified vehicle.</returns>
    public static VehicleModelInfo ForVehicle(BaseVehicle vehicle)
    {
        if (vehicle == null)
        {
            throw new ArgumentNullException(nameof(vehicle));
        }

        return ForVehicle(vehicle.Model);
    }

    /// <summary>Returns an instance of <see cref="VehicleModelInfo" /> containing information about the given <see cref="VehicleModelType" />.</summary>
    /// <param name="model">The <see cref="VehicleModelType" /> to find information about.</param>
    /// <returns>An instance of <see cref="VehicleModelInfo" /> containing information about the given VehicleModelType.</returns>
    public static VehicleModelInfo ForVehicle(VehicleModelType model)
    {
        if (!Enum.IsDefined(model))
        {
            throw new ArgumentOutOfRangeException(nameof(model), "model is non-existant");
        }

        return _vehicleModelInfos[(int)model - 400];
    }
    
    /// <summary>Returns an <see cref="IEnumerable{VehicleModleInfo}"/> of vehicles in the specified <paramref name="category"/>.</summary>
    /// <param name="category">The category to get the vehicles.</param>
    /// <returns>An <see cref="IEnumerable{VehicleModleInfo}"/> of vehicles in the specified <paramref name="category"/>.</returns>
    public static IEnumerable<VehicleModelInfo> GetVehiclesInCategory(VehicleCategory category)
    {
        if (!Enum.IsDefined(category))
        {
            throw new ArgumentOutOfRangeException(nameof(category), "The specified category does not exist.");
        }

        return _vehicleModelInfos.Where(modelInfo => modelInfo.Category == category);
    }
}
