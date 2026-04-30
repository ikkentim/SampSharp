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

namespace SampSharp.Entities.SAMP;

/// <summary>Contains all types of explosions with description</summary>
public enum ExplosionType
{
    /// <summary>Size large. Visible. Damage.</summary>
    LargeVisibleDamage,


    /// <summary>Size normal. Visible. Creates a fire.</summary>
    NormalVisibleFire,


    /// <summary>Size large. Visible. Damage. Creates a fire.</summary>
    LargeVisibleDamageFire,


    /// <summary>Size large. Visible. Damage. Sometimes it does not create a fire.</summary>
    LargeVisibleDamageFire2,


    /// <summary>Size normal. Visible. Damage. It represents a vanishing flash. No sound.</summary>
    NormalVisibleDamageFlash,


    /// <summary>Size normal. Visible. Damage. It represents a vanishing flash. No sound.</summary>
    NormalVisibleDamageFlash2,


    /// <summary>Size very large. Visible. Damage. Additional reddish explosion after-glow.</summary>
    VeryLargeVisibleDamage,


    /// <summary>Size huge. Visible. Damage. Additional reddish explosion after-glow.</summary>
    HugeVisibleDamage,


    /// <summary>Size normal. Invisible. Damage.</summary>
    NormalInvisibleDamage,


    /// <summary>Size normal. Damage. Creates a fire at ground level, otherwise explosion is heard but invisible.</summary>
    NormalInvisibleDamageFire,


    /// <summary>Size large. Visible. Damage. Compared to the LargeVisibleDamage, the explosion seems great.</summary>
    LargeVisibleDamage2,


    /// <summary>Size small. Visible. Damage.</summary>
    SmallVisibleDamage,


    /// <summary>Size very small. Visible. Damage.</summary>
    VerySmallVisibleDamage,


    /// <summary>Size large. Invisible. Produces no special effects other than black burn effects on the ground.</summary>
    LargeInvisible
}