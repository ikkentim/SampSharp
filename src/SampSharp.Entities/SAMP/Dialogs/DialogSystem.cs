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

/// <summary>
/// Represents a system for handling dialog functionality
/// </summary>
public class DialogSystem : ISystem
{
    [Event]
    // ReSharper disable once UnusedMember.Local
    private void OnPlayerDisconnect(VisibleDialog player, DisconnectReason _)
    {
        player.Handler(new DialogResult(DialogResponse.Disconnected, 0, null));
    }

    [Event]
    // ReSharper disable once UnusedMember.Local
    private void OnDialogResponse(VisibleDialog player, int dialogId, int response, int listItem, string inputText)
    {
        if (dialogId != DialogService.DialogId)
            return; // Prevent dialog hacks

        player.ResponseReceived = true;
        player.Handler(new DialogResult(
            response == 1 ? DialogResponse.LeftButton : DialogResponse.RightButtonOrCancel, listItem, inputText));
    }
}