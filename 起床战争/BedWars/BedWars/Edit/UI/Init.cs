using BedWars.Common;
using BedWars.Edit.UI.EditWindow;
using tContentPatch;
using tContentPatch.Content.UI;

namespace BedWars.Edit.UI
{
    internal class Init : Mod
    {
        public static EditWindow.EditWindow editWindow { get; private set; } = null;
        public static EditWindowSpawItem.EditWindow editWindow_SpawItem { get; private set; } = null;
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

                editWindow_SpawItem = new EditWindowSpawItem.EditWindow("编辑生成物品", 400, 400);
            };
        }

        private static void SetEnable(bool enable)
        {
            if (enable == Init.enable) return;
            Init.enable = enable;

            if (enable)
            {
                editWindow.OnEditEnable();
                editWindow_SpawItem.OnEditEnable();

                SwitchEditWindow(true);
            }
            else
            {
                editWindow.OnEditNoEnable();
                editWindow_SpawItem.OnEditNoEnable();
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

        public static void SwitchEditWindow_SpawItem(bool open)
        {
            SwitchWindow(editWindow_SpawItem, open);
        }
    }
}
