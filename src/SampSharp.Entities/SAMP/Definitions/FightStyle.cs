﻿// SampSharp
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

/// <summary>Contains all fighting styles.</summary>
/// <remarks>See <see href="https://www.open.mp/docs/scripting/resources/fightingstyles">https://www.open.mp/docs/scripting/resources/fightingstyles</see>.</remarks>
[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "CommentTypo")]
public enum FightStyle
{
    /// <summary>Normal fighting style.</summary>
    Normal = 4,

    /// <summary>Boxing fighting style.</summary>
    Boxing = 5,

    /// <summary>Kung fu fighting style.</summary>
    Kungfu = 6,

    /// <summary>Kneehead fighting style.</summary>
    Kneehead = 7,

    /// <summary>Grabkick fighting style.</summary>
    Grabkick = 15,

    /// <summary>Elbow fighting style.</summary>
    Elbow = 16
}