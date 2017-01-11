// SampSharp
// Copyright 2017 Tim Potze
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
namespace SampSharp.GameMode.Definitions
{
    /// <summary>
    ///     Contains all dialog styles.
    /// </summary>
    public enum DialogStyle
    {
        /// <summary>
        ///     A box with a caption, text and one or two buttons.
        /// </summary>
        MessageBox = 0,

        /// <summary>
        ///     A box with a caption, text, an inputbox and one or two buttons.
        /// </summary>
        Input = 1,

        /// <summary>
        ///     A box with a caption, a bunch of selectable items and one or two buttons.
        /// </summary>
        List = 2,

        /// <summary>
        ///     A box with a caption, text, an password-inputbox and one or two buttons.
        /// </summary>
        Password = 3,

        /// <summary>
        ///     A box with a caption, a bunch of selectable rows which contain a number of columns and one or two buttons.
        /// </summary>
        Tablist = 4,

        /// <summary>
        ///     A box with a caption, a bunch of selectable rows which contain a number of columns with a header and one or two
        ///     buttons.
        /// </summary>
        TablistHeaders = 5
    }
}