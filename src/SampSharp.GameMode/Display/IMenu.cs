// SampSharp
// Copyright 2015 Tim Potze
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
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Display
{
    /// <summary>
    ///     Provides the functionality of a SA:MP menu.
    /// </summary>
    public interface IMenu : IIdentifiable, IDisposable
    {
        /// <summary>
        ///     Gets the title.
        /// </summary>
        string Title { get; }

        /// <summary>
        ///     Gets the position.
        /// </summary>
        Vector2 Position { get; }

        /// <summary>
        ///     Gets an <see cref="IReadOnlyCollection{T}" /> of <see cref="GtaPlayer" /> instances which are viewing this
        ///     instance.
        /// </summary>
        IReadOnlyCollection<GtaPlayer> Viewers { get; }

        /// <summary>
        ///     Gets a collection of columns.
        /// </summary>
        IList<MenuColumn> Columns { get; }

        /// <summary>
        ///     Gets a collection of rows.
        /// </summary>
        IList<MenuRow> Rows { get; }

        /// <summary>
        ///     Occurs when this <see cref="Menu" /> was exited.
        /// </summary>
        event EventHandler<EventArgs> Exit;

        /// <summary>
        ///     Occurs when there has been responded to this <see cref="Menu" />.
        /// </summary>
        event EventHandler<MenuRowEventArgs> Response;

        /// <summary>
        ///     Show this <see cref="IMenu" /> to the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player to show this menu to.</param>
        /// <returns>True when successful; False otherwise.</returns>
        bool Show(GtaPlayer player);

        /// <summary>
        ///     Hides this <see cref="IMenu" /> for the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player to hide this menu for.</param>
        void Hide(GtaPlayer player);

        /// <summary>
        ///     Hides this <see cref="IMenu" /> for all viewers.
        /// </summary>
        void HideForAll();

        /// <summary>
        ///     Raises the <see cref="Exit" /> event.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        void OnExit(GtaPlayer player, EventArgs e);

        /// <summary>
        ///     Raises the <see cref="Response" /> event.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="e">The <see cref="MenuRowEventArgs" /> instance containing the event data.</param>
        void OnResponse(GtaPlayer player, MenuRowEventArgs e);
    }
}