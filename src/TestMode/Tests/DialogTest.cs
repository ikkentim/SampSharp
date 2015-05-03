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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    public class DialogTest : ITest
    {
        public void Start(GameMode gameMode)
        {

        }

        [Command("dialogcols")]
        public static void DialogaCommand(GtaPlayer player)
        {
            var dialog = new Dialog("Captions", new[]
                {
                    "Col1", "Col2", "Col3", "Col4"
                }, new[,]
                {
                    {"11","12","13","14"},
                    {"21","22","23","24"},
                    {"31","32","33","34"},
                    {"41","42","43","44"},
                    {"51","52","53","54"},
                    {"61","62","63","64"},
                    {"71","72","73","74"}
                }, "OK!", "Cancel");

            dialog.Show(player);

            dialog.Response += (sender, args) =>
            {
                player.SendClientMessage("Response: " + args.DialogButton);
            };
        }
        [Command("dialogasync")]
        public static async void DialogaAyncCommand(GtaPlayer player)
        {
            var dialog = new Dialog(DialogStyle.Input, "Hello", "Insert something", "Confirm", "NO");
            var response = await dialog.ShowAsync(player);

            player.SendClientMessage("Response: " + response.InputText);
        }
    }
}