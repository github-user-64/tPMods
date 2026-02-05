using Terraria;

namespace BedWars.Edit.UI.EditItem_File
{
    internal class UpdateModConfig : ModTool.Common.UI.UIItemTextButton
    {
        public UpdateModConfig(string btnText, string text) : base(btnText, null, text)
        {
            OnClick += () =>
            {
                string v = ModConfig.Update() ? "成功" : "失败";

                Main.NewText($"更新{v}");
                Main.NewText($"地图目录:{ThisMod.DirMapData}");
            };
        }
    }
}
