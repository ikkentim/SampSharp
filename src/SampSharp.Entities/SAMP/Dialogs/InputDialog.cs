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

namespace SampSharp.Entities.SAMP;

/// <summary>Represents a dialog with an input field.</summary>
public class InputDialog : IDialog<InputDialogResponse>
{
    /// <summary>Gets or sets a value indicating whether the input is a password.</summary>
    public bool IsPassword { get; set; }

    /// <inheritdoc />
    public InputDialogResponse Translate(DialogResult dialogResult)
    {
        return new InputDialogResponse(dialogResult.Response, dialogResult.InputText);
    }

    DialogStyle IDialog.Style => IsPassword
        ? DialogStyle.Password
        : DialogStyle.Input;

    /// <summary>Gets or sets the text above the input field in this dialog.</summary>
    public string Content { get; set; }

    /// <summary>Gets or sets the caption of this input dialog.</summary>
    public string Caption { get; set; }

    /// <summary>Gets or sets the text on the left button of this input dialog.</summary>
    public string Button1 { get; set; }

    /// <summary>Gets or sets the text on the right button of this input dialog. If the value is <c>null</c>, the right button is hidden.</summary>
    public string Button2 { get; set; }
}