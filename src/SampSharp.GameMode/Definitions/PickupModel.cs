
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
    
    /**
     * List from: https://dev.prineside.com/en/gtasa_samp_model_id/category/pickups-and-icons/?fbclid=IwAR3jNuRFqhxWKCOMhfEMmaIXPMCly0pomv0ggK4DaksGJjyENGKYdA6AIBI&page=1
     * Javascript:
     *
    copy((function() {

	var data = (function(){
        var data = document.querySelectorAll("a");
        var result = [];
        for(let i = 0; i < data.length; i++) {
            var el = data[i];
            if(el.hasAttribute("data-title") && el.hasAttribute("data-model")) result.push({id: el.getAttribute("data-model"), name: el.getAttribute("data-name")});
        }
        return result;
    })();
	var result = "";
	data.forEach(d => {
		result += "/// <summary>\n/// " + d.name + "\n/// </summary>\n" + d.name + " = " + d.id + ",\n\n"
	});
	return result;
})());
     */
    
    /// <summary>
    ///     Contains all pickup models
    /// </summary>
    public enum PickupModel
    {
        /// <summary>
        /// Gasgrenade
        /// </summary>
        Gasgrenade = 1672,
        
        /// <summary>
        /// Money
        /// </summary>
        Money = 1212,
        
        /// <summary>
        /// CJ_PISTOL_AMMO
        /// </summary>
        CjPistolAmmo = 2037,
        
        /// <summary>
        /// lotion
        /// </summary>
        Lotion = 1644,
        
        /// <summary>
        /// health
        /// </summary>
        Health = 1240,
        
        /// <summary>
        /// Pain_Killer
        /// </summary>
        PainKiller = 2709,
        
        /// <summary>
        /// twoplayer
        /// </summary>
        Twoplayer = 1314,
        
        /// <summary>
        /// property_fsale
        /// </summary>
        PropertyFsale = 1273,
        
        /// <summary>
        /// info
        /// </summary>
        Info = 1239,
        
        /// <summary>
        /// property_locked
        /// </summary>
        PropertyLocked = 1272,
        
        /// <summary>
        /// bigdollar
        /// </summary>
        Bigdollar = 1274,
        
        /// <summary>
        /// WATCH_PICKUP
        /// </summary>
        WatchPickup = 2710,
        
        /// <summary>
        /// mine
        /// </summary>
        Mine = 1213,
        
        /// <summary>
        /// clothesp
        /// </summary>
        Clothesp = 1275,
        
        /// <summary>
        /// killfrenzy
        /// </summary>
        Killfrenzy = 1254,
        
        /// <summary>
        /// CJ_sawnoff
        /// </summary>
        CjSawnoff = 2034,
        
        /// <summary>
        /// killfrenzy2plyr
        /// </summary>
        Killfrenzy2Plyr = 1313,
        
        /// <summary>
        /// Landmine1
        /// </summary>
        Landmine1 = 19602,
        
        /// <summary>
        /// CJ_MP5K
        /// </summary>
        CjMp5K = 2044,
        
        /// <summary>
        /// adrenaline
        /// </summary>
        Adrenaline = 1241,
        
        /// <summary>
        /// CJ_sawnoff2
        /// </summary>
        CjSawnoff2 = 2033,
        
        /// <summary>
        /// NoModelFile
        /// </summary>
        NoModelFile = 18631,
        
        /// <summary>
        /// MedicCase1
        /// </summary>
        MedicCase1 = 11738,
        
        /// <summary>
        /// bribe
        /// </summary>
        Bribe = 1247,
        
        /// <summary>
        /// bodyarmour
        /// </summary>
        Bodyarmour = 1242,
        
        /// <summary>
        /// dynamite
        /// </summary>
        Dynamite = 1654,
        
        /// <summary>
        /// MedicalSatchel1
        /// </summary>
        MedicalSatchel1 = 11736,
        
        /// <summary>
        /// AmmoBox1
        /// </summary>
        AmmoBox1 = 19832,
        
        /// <summary>
        /// petrolcanm
        /// </summary>
        Petrolcanm = 1650,
        
        /// <summary>
        /// camerapickup
        /// </summary>
        Camerapickup = 1253,
        
        /// <summary>
        /// barrelexpos
        /// </summary>
        Barrelexpos = 1252,
        
        /// <summary>
        /// drug_white
        /// </summary>
        DrugWhite = 1575,
        
        /// <summary>
        /// drug_orange
        /// </summary>
        DrugOrange = 1576,
        
        /// <summary>
        /// drug_yellow
        /// </summary>
        DrugYellow = 1577,
        
        /// <summary>
        /// drug_green
        /// </summary>
        DrugGreen = 1578,
        
        /// <summary>
        /// drug_blue
        /// </summary>
        DrugBlue = 1579,
        
        /// <summary>
        /// drug_red
        /// </summary>
        DrugRed = 1580,
        
        /// <summary>
        /// bonus
        /// </summary>
        Bonus = 1248,
        
        /// <summary>
        /// pickupsave
        /// </summary>
        Pickupsave = 1277,
        
        /// <summary>
        /// briefcase
        /// </summary>
        Briefcase = 1210,
        
        /// <summary>
        /// CJ_SHELLS1
        /// </summary>
        CjShells1 = 2061,
        
        /// <summary>
        /// property_red
        /// </summary>
        PropertyRed = 19522,
        
        /// <summary>
        /// property_orange
        /// </summary>
        PropertyOrange = 19523,
        
        /// <summary>
        /// property_yellow
        /// </summary>
        PropertyYellow = 19524,
        
        /// <summary>
        /// ArrowType2
        /// </summary>
        ArrowType2 = 19131,
        
        /// <summary>
        /// ArrowType5
        /// </summary>
        ArrowType5 = 19134,
        
        /// <summary>
        /// ArrowType3
        /// </summary>
        ArrowType3 = 19132,
        
        /// <summary>
        /// rcbomb
        /// </summary>
        Rcbomb = 1636,
        
        /// <summary>
        /// EnExMarker1
        /// </summary>
        EnExMarker1 = 19135,
        
        /// <summary>
        /// CJ_BBAT_NAILS
        /// </summary>
        CjBbatNails = 2045,
        
        /// <summary>
        /// keycard
        /// </summary>
        Keycard = 1581,
        
        /// <summary>
        /// package1
        /// </summary>
        Package1 = 1276,
        
        /// <summary>
        /// cj_horse_Shoe
        /// </summary>
        CjHorseShoe = 954,
        
        /// <summary>
        /// CJ_FIRE_EXT
        /// </summary>
        CjFireExt = 2690,
        
        /// <summary>
        /// Flame_tins
        /// </summary>
        FlameTins = 2057,
        
        /// <summary>
        /// pizzabox
        /// </summary>
        Pizzabox = 1582,
        
        /// <summary>
        /// ArrowType4
        /// </summary>
        ArrowType4 = 19133,
        
        /// <summary>
        /// CJ_M16
        /// </summary>
        CjM16 = 2035,
        
        /// <summary>
        /// CJ_GUNSTUFF1
        /// </summary>
        CjGunstuff1 = 2059,
        
        /// <summary>
        /// pikupparachute
        /// </summary>
        Pikupparachute = 1310,
        
        /// <summary>
        /// CJ_MONEY_BAG
        /// </summary>
        CjMoneyBag = 1550,
        
        /// <summary>
        /// craigpackage
        /// </summary>
        Craigpackage = 1279,
        
        /// <summary>
        /// arrow
        /// </summary>
        Arrow = 1318,
        
        /// <summary>
        /// ArrowType1
        /// </summary>
        ArrowType1 = 19130,
        
        /// <summary>
        /// CJ_SHOVEL
        /// </summary>
        CjShovel = 2228,
        
        /// <summary>
        /// CJ_Gun_docs
        /// </summary>
        CjGunDocs = 2058,
        
        /// <summary>
        /// CJ_psg1
        /// </summary>
        CjPsg1 = 2036,
        
        /// <summary>
        /// CJ_SHOVEL2
        /// </summary>
        CjShovel2 = 2237,
        
        /// <summary>
        /// CJ_SANDBAG
        /// </summary>
        CjSandbag = 2060,
        
        /// <summary>
        /// EnExMarker2
        /// </summary>
        EnExMarker2 = 19197,
        
        /// <summary>
        /// EnExMarker3
        /// </summary>
        EnExMarker3 = 19198,
        
        /// <summary>
        /// XmasBox1
        /// </summary>
        XmasBox1 = 19054,
        
        /// <summary>
        /// XmasBox2
        /// </summary>
        XmasBox2 = 19055,
        
        /// <summary>
        /// XmasBox3
        /// </summary>
        XmasBox3 = 19056,
        
        /// <summary>
        /// XmasBox4
        /// </summary>
        XmasBox4 = 19057,
        
        /// <summary>
        /// XmasBox5
        /// </summary>
        XmasBox5 = 19058,
        
        /// <summary>
        /// EnExMarker4-2
        /// </summary>
        EnExMarker42 = 19605,
        
        /// <summary>
        /// EnExMarker4-3
        /// </summary>
        EnExMarker43 = 19606,
        
        /// <summary>
        /// EnExMarker4-4
        /// </summary>
        EnExMarker44 = 19607,
        
        /// <summary>
        /// CJ_FEILDGUN
        /// </summary>
        CjFeildgun = 2064,
        
        /// <summary>
        /// pumpkin01
        /// </summary>
        Pumpkin01 = 19320,
        
        /// <summary>
        /// WHeartBed1
        /// </summary>
        WHeartBed1 = 11731,
        
        /// <summary>
        /// CJ_cammo_NET
        /// </summary>
        CjCammoNet = 2068,
        
        /// <summary>
        /// chnsaw1
        /// </summary>
        Chnsaw1 = 14673,


        
    }
}