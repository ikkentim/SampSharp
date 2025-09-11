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

using System;

namespace SampSharp.Entities.SAMP;

/// <summary>Represents a row in a <see cref="TablistDialog" /></summary>
public class TablistDialogRow : IDialogRow
{
    /// <summary>Initializes a new instance of the <see cref="ListDialogRow" /> class.</summary>
    /// <param name="columns">The columns of the row.</param>
    public TablistDialogRow(params string[] columns)
    {
        Columns = columns ?? throw new ArgumentNullException(nameof(columns));
    }

    /// <summary>Gets the columns of this tablist dialog row.</summary>
    public string[] Columns { get; }

    /// <summary>Gets the number of columns in this row.</summary>
    public int ColumnCount => Columns.Length;

    /// <summary>Gets or sets the tag. The tag can be used so associate data with this row which can be used retrieved when the user responds to the dialog.</summary>
    public object Tag { get; set; }

    string IDialogRow.RawText => string.Join("\t", Columns);
}