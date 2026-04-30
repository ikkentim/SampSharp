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

/// <summary>Represents a collection of dialog rows of type <see cref="ListDialogRow" />.</summary>
public class ListDialogRowCollection : DialogRowCollection<ListDialogRow>
{
    /// <summary>Adds a row to the list with the specified <paramref name="text" />.</summary>
    /// <param name="text">The text of the row to add.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="text" /> is null.</exception>
    public void Add(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        Add(new ListDialogRow(text));
    }
}