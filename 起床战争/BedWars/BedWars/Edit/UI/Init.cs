using BedWars.Common;
using tContentPatch;

namespace BedWars.Edit.UI
{
    internal class Init : Mod
    {
        public override void Load()
        {
            GameInterface.OnInitUI += () =>
            {
                GameInterface.UI.Append(EnableEditSwitch.Build());
            };
        }

        public static EditWindow editWindow { get; private set; } = null;
        public static void SwitchEditWindow(bool open)
        {
            if (editWindow == null)
            {
                editWindow = new EditWindow("地图编辑", 300, 400);
                editWindow.Left.Pixels = 0;
            }

            if (open) editWindow.Open(GameInterface.UI);
            else editWindow.Close();
        }
    }
}
