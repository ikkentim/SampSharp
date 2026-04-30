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

/// <summary>Provides the functionality for showing dialogs to players.</summary>
public interface IDialogService
{
    /// <summary>Shows the specified <paramref name="dialog" /> to the <paramref name="player" />.</summary>
    /// <typeparam name="TResponse">The type of the response returned by the dialog.</typeparam>
    /// <param name="player">The player to show the dialog to.</param>
    /// <param name="dialog">The dialog to show to the player.</param>
    /// <param name="responseHandler">A handler for the dialog response.</param>
    void Show<TResponse>(Player player, IDialog<TResponse> dialog, Action<TResponse> responseHandler) where TResponse : struct;

    /// <summary>Shows the specified <paramref name="dialog" /> to the <paramref name="player" />.</summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="player">The player to show the dialog to.</param>
    /// <param name="dialog">The dialog to show to the player.</param>
    /// <returns>The dialog response.</returns>
    Task<TResponse> ShowAsync<TResponse>(Player player, IDialog<TResponse> dialog) where TResponse : struct;
}