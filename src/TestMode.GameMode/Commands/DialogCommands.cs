using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestMode.Commands
{
    class DialogCommands
    {
        [Command("msgdialog")]
        public static void MsgDialogTestCommand(BasePlayer player)
        {
            MessageDialog msgDialog = new MessageDialog("caption", "message", "Ok", "Close");
            msgDialog.Response += (object sender, SampSharp.GameMode.Events.DialogResponseEventArgs e) =>
            {
                player.SendClientMessage($"Dialog closed");
            };
            msgDialog.Show(player);
        }
        [Command("inputdialog")]
        public static void InputDialogTestCommand(BasePlayer player)
        {
            InputDialog inputDialog = new InputDialog("caption", "message", false, "Ok", "Close");
            inputDialog.Response += (object sender, SampSharp.GameMode.Events.DialogResponseEventArgs e) =>
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
        internal class GroupListDialog
        {
            [CommandGroup("old")]
            internal class GroupListDialogOld
            {
                [Command("item")]
                public static void ListDialogItemTestCommand(BasePlayer player)
                {
                    ListDialog listDialog = new ListDialog("String options", "Select", "Close");
                    listDialog.AddItem("option 1");
                    listDialog.AddItem("option 2");
                    listDialog.AddItem("option 3");
                    listDialog.Response += (object sender, SampSharp.GameMode.Events.DialogResponseEventArgs e) =>
                    {
                        player.SendClientMessage($"Dialog closed, selected value is \"{e.ListItem}\"");
                    };
                    listDialog.Show(player);
                }
                [Command("range")]
                public static void ListDialogRangeTestCommand(BasePlayer player)
                {
                    ListDialog listDialog = new ListDialog("String options", "Select", "Close");
                    List<string> options = new List<string>();
                    options.Add("option 10");
                    options.Add("option 11");
                    options.Add("option 12");
                    listDialog.AddItems(options);
                    listDialog.Response += (object sender, SampSharp.GameMode.Events.DialogResponseEventArgs e) =>
                    {
                        player.SendClientMessage($"Dialog closed, selected value is \"{e.ListItem}\"");
                    };
                    listDialog.Show(player);
                }
            }
            [CommandGroup("str")]
            internal class GroupListDialogStr
            {
                [Command("item")]
                public static void ListDialogItemTestCommand(BasePlayer player)
                {
                    ListDialog<string> listDialog = new ListDialog<string>("String options", "Select", "Close");
                    listDialog.AddItem("option 1");
                    listDialog.AddItem("option 2");
                    listDialog.AddItem("option 3");
                    listDialog.Response += (object sender, SampSharp.GameMode.Events.DialogResponseEventArgs<string> e) =>
                    {
                        player.SendClientMessage($"Dialog closed, selected value is \"{e.ListItem}\"");
                    };
                    listDialog.Show(player);
                }
                [Command("range")]
                public static void ListDialogRangeTestCommand(BasePlayer player)
                {
                    ListDialog<string> listDialog = new ListDialog<string>("String options", "Select", "Close");
                    List<string> options = new List<string>();
                    options.Add("option 10");
                    options.Add("option 11");
                    options.Add("option 12");
                    listDialog.AddItems(options);
                    listDialog.Response += (object sender, SampSharp.GameMode.Events.DialogResponseEventArgs<string> e) =>
                    {
                        player.SendClientMessage($"Dialog closed, selected value is \"{e.ListItem}\"");
                    };
                    listDialog.Show(player);
                }
            }
            [CommandGroup("veh")]
            internal class GroupListDialogVeh
            {
                [Command("item")]
                public static void ListDialogItemTestCommand(BasePlayer player)
                {
                    ListDialog<VehicleModelType> listDialog = new ListDialog<VehicleModelType>("Vehicles", "Select", "Close");
                    listDialog.AddItem(VehicleModelType.Admiral);
                    listDialog.AddItem(VehicleModelType.Buccaneer);
                    listDialog.AddItem(VehicleModelType.FreightFlatTrailerTrain);
                    listDialog.AddItem(VehicleModelType.Ambulance);
                    listDialog.AddItem(VehicleModelType.Dune);
                    listDialog.Response += (object sender, SampSharp.GameMode.Events.DialogResponseEventArgs<VehicleModelType> e) =>
                    {
                        player.SendClientMessage($"Dialog closed, selected model is \"{e.ListItem}\"");
                    };
                    listDialog.Show(player);
                }
                [Command("range")]
                public static void ListDialogRangeTestCommand(BasePlayer player)
                {
                    ListDialog<BaseVehicle> listDialog = new ListDialog<BaseVehicle>("Vehicles", "Select", "Close");
                    listDialog.AddItems(BaseVehicle.All);
                    listDialog.Response += (object sender, SampSharp.GameMode.Events.DialogResponseEventArgs<BaseVehicle> e) =>
                    {
                        BaseVehicle veh = e.ListItem;
                        player.PutInVehicle(veh);
                        player.SendClientMessage($"Dialog closed, selected vehicle is \"{e.ListItem.ModelInfo.Name}\"");
                    };
                    listDialog.Show(player);
                }
            }
            [CommandGroup("player")]
            internal class GroupListDialogPlayer
            {
                [Command("item")]
                public static void ListDialogItemTestCommand(BasePlayer player)
                {
                    ListDialog<BasePlayer> listDialog = new ListDialog<BasePlayer>("Players", "Select", "Close");
                    listDialog.AddItem(BasePlayer.Find(0));
                    listDialog.Response += (object sender, SampSharp.GameMode.Events.DialogResponseEventArgs<BasePlayer> e) =>
                    {
                        player.SendClientMessage($"Dialog closed, selected player is \"{e.ListItem.Name}\"");
                    };
                    listDialog.Show(player);
                }
                [Command("range")]
                public static void ListDialogRangeTestCommand(BasePlayer player)
                {
                    ListDialog<BasePlayer> listDialog = new ListDialog<BasePlayer>("Players", "Select", "Close");
                    listDialog.AddItems(BasePlayer.All);
                    listDialog.Response += (object sender, SampSharp.GameMode.Events.DialogResponseEventArgs<BasePlayer> e) =>
                    {
                        player.SendClientMessage($"Dialog closed, selected player is \"{e.ListItem.Name}\"");
                    };
                    listDialog.Show(player);
                }
            }
            [CommandGroup("color")]
            internal class GroupListDialogColor
            {
                [Command("item")]
                public static void ListDialogItemTestCommand(BasePlayer player)
                {
                    ListDialog<Color> listDialog = new ListDialog<Color>("Colors", "Select", "Close");
                    listDialog.AddItem(Color.Red);
                    listDialog.AddItem(Color.MediumBlue);
                    listDialog.AddItem(Color.DarkGray);
                    listDialog.AddItem(Color.Turquoise);
                    listDialog.AddItem(new Color(1, 125, 14));
                    listDialog.Response += (object sender, SampSharp.GameMode.Events.DialogResponseEventArgs<Color> e) =>
                    {
                        player.SendClientMessage($"Dialog closed, selected value is \"{e.ListItem + e.ListItem.ToLiteralString() + Color.White}\"");
                    };
                    listDialog.Show(player);
                }
                [Command("range")]
                public static void ListDialogRangeTestCommand(BasePlayer player)
                {
                    ListDialog<Color> listDialog = new ListDialog<Color>("Colors", "Select", "Close");
                    List<Color> colors = new List<Color>();
                    colors.Add(Color.Chocolate);
                    colors.Add(Color.LightGreen);
                    colors.Add(Color.AliceBlue);
                    colors.Add(new Color(54, 26, 178));
                    listDialog.AddItems(colors);
                    listDialog.Response += (object sender, SampSharp.GameMode.Events.DialogResponseEventArgs<Color> e) =>
                    {
                        player.SendClientMessage($"Dialog closed, selected value is \"{e.ListItem + e.ListItem.ToLiteralString() + Color.White}\"");
                    };
                    listDialog.Show(player);
                }
            }
            [CommandGroup("null")]
            internal class GroupListDialogNull
            {
                [Command("item")]
                public static void ListDialogItemTestCommand(BasePlayer player)
                {
                    ListDialog<Color> listDialog = new ListDialog<Color>("Colors", "Select", "Close");
                    listDialog.AddItem(Color.Red);
                    listDialog.AddItem(Color.MediumBlue);
                    listDialog.AddItem(null);
                    listDialog.AddItem(Color.Turquoise);
                    listDialog.AddItem(new Color(1, 125, 14));
                    listDialog.Response += (object sender, SampSharp.GameMode.Events.DialogResponseEventArgs<Color> e) =>
                    {
                        player.SendClientMessage($"Dialog closed, selected value is \"{e.ListItem + e.ListItem.ToLiteralString() + Color.White}\"");
                    };
                    listDialog.Show(player);
                }
            }
        }
    }
}
