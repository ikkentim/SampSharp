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

using System.Collections.Generic;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

// ReSharper disable StringLiteralTypo

namespace TestMode.GameMode.Commands;

internal static class DialogCommands
{
    [Command("msgdialog")]
    public static void MsgDialogTestCommand(BasePlayer player)
    {
        var msgDialog = new MessageDialog("caption", "message", "Ok", "Close");
        msgDialog.Response += (_, _) =>
        {
            player.SendClientMessage("Dialog closed");
        };
        msgDialog.Show(player);
    }

    [Command("inputdialog")]
    public static void InputDialogTestCommand(BasePlayer player)
    {
        var inputDialog = new InputDialog("caption", "message", false, "Ok", "Close");
        inputDialog.Response += (_, e) =>
        {
            player.SendClientMessage($"Dialog closed, input text is \"{e.InputText}\"");
        };
        inputDialog.Show(player);
    }

    [Command("listdialog")]
    public static void ListDialogHelpCommand(BasePlayer player)
    {
        player.SendClientMessage("Usage: /listdialog [old,str,veh,player,color,null] [item,range]");
    }

    [CommandGroup("listdialog")]
    internal static class GroupListDialog
    {
        [CommandGroup("old")]
        internal static class GroupListDialogOld
        {
            [Command("item")]
            public static void ListDialogItemTestCommand(BasePlayer player)
            {
                var listDialog = new ListDialog("String options", "Select", "Close");
                listDialog.AddItem("option 1");
                listDialog.AddItem("option 2");
                listDialog.AddItem("option 3");
                listDialog.Response += (_, e) =>
                {
                    player.SendClientMessage($"Dialog closed, selected value is \"{e.ListItem}\"");
                };
                listDialog.Show(player);
            }

            [Command("range")]
            public static void ListDialogRangeTestCommand(BasePlayer player)
            {
                var listDialog = new ListDialog("String options", "Select", "Close");
                var options = new List<string>
                {
                    "option 10",
                    "option 11",
                    "option 12"
                };
                listDialog.AddItems(options);
                listDialog.Response += (_, e) =>
                {
                    player.SendClientMessage($"Dialog closed, selected value is \"{e.ListItem}\"");
                };
                listDialog.Show(player);
            }
        }

        [CommandGroup("str")]
        internal static class GroupListDialogStr
        {
            [Command("item")]
            public static void ListDialogItemTestCommand(BasePlayer player)
            {
                var listDialog = new ListDialog<string>("String options", "Select", "Close");
                listDialog.AddItem("option 1");
                listDialog.AddItem("option 2");
                listDialog.AddItem("option 3");
                listDialog.Response += (_, e) =>
                {
                    player.SendClientMessage($"Dialog closed, selected value is \"{e.ItemValue}\"");
                };
                listDialog.Show(player);
            }

            [Command("range")]
            public static void ListDialogRangeTestCommand(BasePlayer player)
            {
                var listDialog = new ListDialog<string>("String options", "Select", "Close");
                var options = new List<string>
                {
                    "option 10",
                    "option 11",
                    "option 12"
                };
                listDialog.AddItems(options);
                listDialog.Response += (_, e) =>
                {
                    player.SendClientMessage($"Dialog closed, selected value is \"{e.ItemValue}\"");
                };
                listDialog.Show(player);
            }
        }

        [CommandGroup("veh")]
        internal static class GroupListDialogVeh
        {
            [Command("item")]
            public static void ListDialogItemTestCommand(BasePlayer player)
            {
                var listDialog = new ListDialog<VehicleModelType>("Vehicles", "Select", "Close");
                listDialog.AddItem(VehicleModelType.Admiral);
                listDialog.AddItem(VehicleModelType.Buccaneer);
                listDialog.AddItem(VehicleModelType.FreightFlatTrailerTrain);
                listDialog.AddItem(VehicleModelType.Ambulance);
                listDialog.AddItem(VehicleModelType.Dune);
                listDialog.Response += (_, e) =>
                {
                    player.SendClientMessage($"Dialog closed, selected model is \"{e.ItemValue}\"");
                };
                listDialog.Show(player);
            }

            [Command("range")]
            public static void ListDialogRangeTestCommand(BasePlayer player)
            {
                var listDialog = new ListDialog<BaseVehicle>("Vehicles", "Select", "Close");
                listDialog.AddItems(BaseVehicle.All);
                listDialog.Response += (_, e) =>
                {
                    var veh = e.ItemValue;
                    player.PutInVehicle(veh);
                    player.SendClientMessage($"Dialog closed, selected vehicle is \"{e.ItemValue.ModelInfo.Name}\"");
                };
                listDialog.Show(player);
            }
        }

        [CommandGroup("player")]
        internal static class GroupListDialogPlayer
        {
            [Command("item")]
            public static void ListDialogItemTestCommand(BasePlayer player)
            {
                var listDialog = new ListDialog<BasePlayer>("Players", "Select", "Close");
                listDialog.AddItem(BasePlayer.Find(0));
                listDialog.Response += (_, e) =>
                {
                    player.SendClientMessage($"Dialog closed, selected player is \"{e.ItemValue.Name}\"");
                };
                listDialog.Show(player);
            }

            [Command("range")]
            public static void ListDialogRangeTestCommand(BasePlayer player)
            {
                var listDialog = new ListDialog<BasePlayer>("Players", "Select", "Close");
                listDialog.AddItems(BasePlayer.All);
                listDialog.Response += (_, e) =>
                {
                    player.SendClientMessage($"Dialog closed, selected player is \"{e.ItemValue.Name}\"");
                };
                listDialog.Show(player);
            }
        }

        [CommandGroup("color")]
        internal static class GroupListDialogColor
        {
            [Command("item")]
            public static void ListDialogItemTestCommand(BasePlayer player)
            {
                var listDialog = new ListDialog<ColorValue>("Colors", "Select", "Close");
                listDialog.AddItem(new ColorValue(Color.Red));
                listDialog.AddItem(new ColorValue(Color.MediumBlue));
                listDialog.AddItem(new ColorValue(Color.DarkGray));
                listDialog.AddItem(new ColorValue(Color.Turquoise));
                listDialog.AddItem(new ColorValue(new Color(1, 125, 14)));
                listDialog.Response += (_, e) =>
                {
                    player.SendClientMessage($"Dialog closed, selected value is {e.ItemValue}: \"{e.ItemValue}{Color.White}\"");
                };
                listDialog.Show(player);
            }

            [Command("item-async")]
            public static async void ListDialogItemTestCommandAsync(BasePlayer player)
            {
                var listDialog = new ListDialog<ColorValue>("Colors", "Select", "Close");
                listDialog.AddItem(new ColorValue(Color.Red));
                listDialog.AddItem(new ColorValue(Color.MediumBlue));
                listDialog.AddItem(new ColorValue(Color.DarkGray));
                listDialog.AddItem(new ColorValue(Color.Turquoise));
                listDialog.AddItem(new ColorValue(new Color(1, 125, 14)));

                var result = await listDialog.ShowAsync(player);
                player.SendClientMessage($"Dialog closed, selected value is {result.ListItem}: \"{result.ItemValue}{Color.White}\"");
            }

            [Command("range")]
            public static void ListDialogRangeTestCommand(BasePlayer player)
            {
                var listDialog = new ListDialog<ColorValue>("Colors", "Select", "Close");
                var colors = new List<ColorValue>
                {
                    new(Color.Chocolate),
                    new(Color.LightGreen),
                    new(Color.AliceBlue),
                    new(new Color(54, 26, 178))
                };
                listDialog.AddItems(colors);
                listDialog.Response += (_, e) =>
                {
                    player.SendClientMessage($"Dialog closed, selected value is {e.ListItem}: \"{e.ItemValue}{Color.White}\"");
                };
                listDialog.Show(player);
            }
        }

        [CommandGroup("null")]
        internal static class GroupListDialogNull
        {
            [Command("item")]
            public static void ListDialogItemTestCommand(BasePlayer player)
            {
                var listDialog = new ListDialog<object>("Colors", "Select", "Close");
                listDialog.AddItem(new ColorValue(Color.Red));
                listDialog.AddItem(new ColorValue(Color.MediumBlue));
                listDialog.AddItem(null);
                listDialog.AddItem(new ColorValue(Color.Turquoise));
                listDialog.AddItem(new ColorValue(new Color(1, 125, 14)));
                listDialog.Response += (_, e) =>
                {
                    player.SendClientMessage($"Dialog closed, selected value is {e.ListItem}: \"{e.ItemValue}{Color.White}\"");
                };
                listDialog.Show(player);
            }
        }
    }
}