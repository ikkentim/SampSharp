using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.Entities.Systems.IssueTests
{
    public class Issue333ReproIndexOutOfBoundsError : ISystem
    {
        [PlayerCommand]
        public void EntityManagerBugCommand(Player player, IDialogService dialogService)
        {
            // test for issue #333 
            var dialog = new MessageDialog("Welcome", "Welcome on my server!", "Continue");

            dialogService.Show(player.Entity, dialog,
                async r =>
                {
                    var dialog2 = new InputDialog
                    {
                        IsPassword = false,
                        Caption = "Mail",
                        Content = "Please enter your email :",
                        Button1 = "Valid",
                        Button2 = "Exit"
                    };

                    var result = await dialogService.Show(player.Entity, dialog2);

                    player.SendClientMessage($"You entered {result.InputText}");
                }
            );
        }
    }
}