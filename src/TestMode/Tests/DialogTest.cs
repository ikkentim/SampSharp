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
using System.Linq;
using SampSharp.GameMode;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    public class DialogTest : ITest
    {
        public void Start(GameMode gameMode)
        {
        }

        [Command("dialog")]
        public static void DialogCommand(GtaPlayer player)
        {
            var dialog = new MessageDialog("Captions", "abc", "OK!", "Cancel");

            dialog.Show(player);

            dialog.Response += (sender, args) => player.SendClientMessage("Response: " + args.DialogButton);
        }

        [Command("dialoglist")]
        [Text("items")]
        public static void DialogListCommand(GtaPlayer player, string items)
        {
            var dialog = new ListDialog("Captions", "OK!");

            foreach (var i in items.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)))
                dialog.Items.Add(i);

            dialog.Show(player);

            dialog.Response += (sender, args) => player.SendClientMessage("Response: " + args.ListItem);
        }

        [Command("dialogasync")]
        public static async void DialogASyncCommand(GtaPlayer player)
        {
            var dialog = new InputDialog("Hello", "Insert something", false, "Confirm", "NO");
            var response = await dialog.ShowAsync(player);

            Console.WriteLine(Sync.IsRequired);
            Sync.Run(() => player.SendClientMessage("Response: " + response.InputText));
        }

        [Command("dialogasynclogintest")]
        public static async void DialogASyncCommandLogintest(GtaPlayer player)
        {
            var dialog = new InputDialog("Hello", "Login please!", true, "Login", "Cancel");

            var timerToShowThingsWork = new Timer(1000, true);
            timerToShowThingsWork.Tick +=
                (sender, args) => player.SendClientMessage("Things still work in the background!");

            var response = await dialog.ShowAsync(player);

            player.SendClientMessage(
                "Your input was {1}... Do we require a sync? {0}. So thats awesome! Lets move you up a lill. ",
                Sync.IsRequired, response.InputText);
            player.Position += new Vector3(0, 1, 0);

            timerToShowThingsWork.Dispose();
        }
    }
}