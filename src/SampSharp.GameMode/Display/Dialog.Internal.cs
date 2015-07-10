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

using SampSharp.GameMode.Natives;

namespace SampSharp.GameMode.Display
{
    public partial class Dialog
    {
        private static class Internal
        {
            public delegate bool ShowPlayerDialogImpl(
                int playerid, int dialogid, int style, string caption, string info, string button1, string button2);

            [Native("ShowPlayerDialog")] public static readonly ShowPlayerDialogImpl ShowPlayerDialog = null;
        }
    }
}