using BedWars.Common;
using tContentPatch;
using tContentPatch.Content.UI;

namespace BedWars.Edit.UI
{
    internal class Init : Mod
    {
        public class asd : PatchMain
        {
            public override void OnEnterWorldPrefix()
            {
                Enable = false;
            }
        }

        public static EditWindow.EditWindow editWindow { get; private set; } = null;
        private static bool enable = false;
        public static bool Enable
        {
            get => enable;
            set => SetEnable(value);
        }

        public override void Load()
        {
            GameInterface.OnInitUI += () =>
            {
                GameInterface.UI.Append(EnableEditSwitch.Build());

                editWindow = new EditWindow.EditWindow("地图编辑", 300, 400);
                editWindow.Left.Pixels = 0;
                editWindow.OnOpen += () => SetEnable(true);
                editWindow.OnClose += () => SetEnable(false);
            };
        }

        private static void SetEnable(bool enable)
        {
            if (enable == Init.enable) return;
            Init.enable = enable;

            if (enable)
            {
                editWindow.OnEditEnable();

                SwitchEditWindow(true);
            }
            else
            {
                editWindow.OnEditNoEnable();
            }
        }

        private static void SwitchWindow(UIWindow ui, bool open)
        {
            if (ui == null) return;

            if (open)
            {
                if (ui.IsOpen == false) ui.Open(GameInterface.UI);
                return;
            }

            if (ui.IsOpen == true) ui.Close();
        }

        public static void SwitchEditWindow(bool open)
        {
            SwitchWindow(editWindow, open);
        }
    }
}
