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

/// <summary>Provides the functionality of a dialog definition with a specialized dialog response struct of type <typeparamref name="TResponse" />.</summary>
/// <typeparam name="TResponse">The type of the specialized response struct.</typeparam>
public interface IDialog<out TResponse> : IDialog where TResponse : struct
{
    /// <summary>Translates the specified <paramref name="dialogResult" /> to a specialized response of type <typeparamref name="TResponse" />.</summary>
    /// <param name="dialogResult">The dialog result to translate.</param>
    /// <returns>The translated dialog result.</returns>
    TResponse Translate(DialogResult dialogResult);
}

/// <summary>Provides the functionality of a dialog definition.</summary>
public interface IDialog
{
    /// <summary>Gets the dialog style.</summary>
    DialogStyle Style { get; }

    /// <summary>Gets the caption of  the dialog.</summary>
    string? Caption { get; }

    /// <summary>Gets the content of the dialog.</summary>
    string? Content { get; }

    /// <summary>Gets the text on the left button.</summary>
    string? Button1 { get; }

    /// <summary>Gets the text on the right button.</summary>
    string? Button2 { get; }
}