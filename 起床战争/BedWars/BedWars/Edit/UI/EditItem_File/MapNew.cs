using Terraria;

namespace BedWars.Edit.UI.EditItem_File
{
    internal class MapNew : ModTool.Common.UI.UIItemTextButton
    {
        public MapNew(string btnText, string text) : base(btnText, null, text)
        {
            OnClick += () =>
            {
                string ex = EditData.instance.ResetData() ?? "新建成功";

                Main.NewText(ex);
            };
        }
    }
}
