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

using SampSharp.GameMode.Display;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events;

/// <summary>Provides data for the <see cref="BaseMode.DialogResponse" />, <see cref="BasePlayer.DialogResponse" /> or <see cref="Dialog.Response" /> event.</summary>
public class DialogResponseEventArgs<T> : DialogResponseEventArgs
{
    /// <summary>Initializes a new instance of the DialogResponseEventArgs class.</summary>
    /// <param name="player">The player.</param>
    /// <param name="dialogid">Id of the dialog.</param>
    /// <param name="response">Response of the dialog response.</param>
    /// <param name="listitem">List item of the dialog response.</param>
    /// <param name="itemValue">The value of the item of the dialog response.</param>
    /// <param name="inputtext">Input text of the dialog response.</param>
    public DialogResponseEventArgs(BasePlayer player, int dialogid, int response, int listitem, T itemValue, string inputtext) : base(player, dialogid,
        response, listitem, inputtext)
    {
        ItemValue = itemValue;
    }

    /// <summary>Get the value of the item clicked for this response.</summary>
    public T ItemValue { get; }
}