using BedWars.Common;
using tContentPatch;

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

                editWindow = new EditWindow.EditWindow(GameInterface.UI, "地图编辑", 330, 400);
                editWindow.Left.Pixels = 0;
            };
        }

        private static void SetEnable(bool enable)
        {
            if (enable == Init.enable) return;
            Init.enable = enable;

            if (enable)
            {
                editWindow.OnEditEnable();
                return;
            }

            editWindow.OnEditEnableNo();
        }
    }
}
