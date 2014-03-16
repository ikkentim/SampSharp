using GameMode.Definitions;

namespace GameMode.Events
{
    public class DialogResponseEventArgs : PlayerEventArgs
    {
        public DialogResponseEventArgs(int playerid, int dialogid, int response, int listitem, string inputtext)
            : base(playerid)
        {
            DialogId = dialogid;
            DialogButton = (DialogButton) response;
            ListItem = listitem;
            InputText = inputtext;
        }

        public int DialogId { get; private set; }

        public DialogButton DialogButton { get; private set; }

        public int ListItem { get; private set; }

        public string InputText { get; private set; }
    }
}
