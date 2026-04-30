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

/// <summary>Represents a response to a <see cref="ListDialog" />.</summary>
public struct ListDialogResponse
{
    /// <summary>Initializes a new instance of the <see cref="MessageDialogResponse" /> struct.</summary>
    /// <param name="response">The way in which the player has responded to the dialog.</param>
    /// <param name="itemIndex">The index of the item the player selected in the dialog.</param>
    /// <param name="item">The item the player selected in the dialog.</param>
    public ListDialogResponse(DialogResponse response, int itemIndex, ListDialogRow? item)
    {
        Response = response;
        ItemIndex = itemIndex;
        Item = item;
    }

    /// <summary>Gets the way in which the player has responded to the dialog.</summary>
    public DialogResponse Response { get; }

    /// <summary>Gets the index of the item the player selected in the dialog.</summary>
    public int ItemIndex { get; }

    /// <summary>Gets the item the player selected in the dialog.</summary>
    public ListDialogRow? Item { get; }
}