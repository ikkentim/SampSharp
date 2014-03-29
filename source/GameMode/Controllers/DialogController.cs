using GameMode.World;

namespace GameMode.Controllers
{
    /// <summary>
    /// A controller processing all dialog actions.
    /// </summary>
    public class DialogController : IController
    {
        /// <summary>
        /// Registers the events this DialogController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public void RegisterEvents(BaseMode gameMode)
        {
            gameMode.DialogResponse += (sender, args) =>
            {
                var dialog = Dialog.GetOpenDialog(args.Player);

                if (dialog != null) 
                    dialog.OnResponse(args);
            };

            gameMode.PlayerDisconnected += (sender, args) => Dialog.Hide(args.Player);
        }
    }
}
