using Terraria;

namespace BedWars.Edit.UI.EditItem_File
{
    internal class OpenMapDirectory : ModTool.Common.UI.UIItemTextButton
    {
        public OpenMapDirectory(string btnText, string text) : base(btnText, null, text)
        {
            OnClick += () =>
            {
                string ex = EditData.instance.OpenDirectory();
                if (ex == null)
                {
                    Main.NewText("已打开", 0, 255, 0);
                    return;
                }

                Main.NewText("打开失败", 255, 0, 0);
                Main.NewText(ex, 255, 0, 0);
            };
        }
    }
}
