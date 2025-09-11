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
using SampSharp.GameMode.Definitions;

namespace SampSharp.GameMode.Display;

/// <summary>
///     Represents a message dialog.
/// </summary>
public class MessageDialog : Dialog
{
    /// <summary>
    ///     Initializes a new instance of the MessageDialog class.
    /// </summary>
    /// <param name="caption">
    ///     The title at the top of the dialog. The length of the caption can not exceed more than 64
    ///     characters before it starts to cut off.
    /// </param>
    /// <param name="message">The text to display in the main dialog. Use \n to start a new line and \t to tabulate.</param>
    /// <param name="button1">The text on the left button.</param>
    /// <param name="button2">The text on the right button. Leave it blank to hide it.</param>
    public MessageDialog(string caption, string message, string button1, string button2 = null) : base(
        DialogStyle.MessageBox, caption, button1, button2)
    {
        Message = message ?? throw new ArgumentNullException(nameof(message));
    }

    /// <summary>
    ///     Gets the Info displayed.
    /// </summary>
    protected override string Info => Message;

    /// <summary>
    ///     Gets or sets the message in the dialog.
    /// </summary>
    public string Message { get; set; }
}