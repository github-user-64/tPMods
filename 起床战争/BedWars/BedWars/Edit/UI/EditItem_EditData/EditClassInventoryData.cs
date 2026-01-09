using Microsoft.Xna.Framework.Graphics;
using ModTool.Common.UI;
using Terraria;

namespace BedWars.Edit.UI.EditItem_EditData
{
    internal class EditCopyInventoryData : UIItemTextButton
    {
        public EditCopyInventoryData(string btnText, Texture2D ico = null, string text = null) : base(btnText, ico, text)
        {
            MouseText = "开始游戏时玩家的背包数据(物品栏,盔甲,饰品,时装,染料)";

            OnClick += () =>
            {
                string ex = EditData.instance.InventoryCopy(Main.LocalPlayer);

                if (ex != null) Main.NewText(ex);
            };
        }
    }

    internal class EditPasteInventoryData : UIItemTextButton
    {
        public EditPasteInventoryData(string btnText, Texture2D ico = null, string text = null) : base(btnText, ico, text)
        {
            OnClick += () =>
            {
                string ex = EditData.instance.InventoryPaste(Main.LocalPlayer);

                if (ex != null) Main.NewText(ex);
            };
        }
    }

    internal class EditResetInventoryData : UIItemTextButton
    {
        public EditResetInventoryData(string btnText, Texture2D ico = null, string text = null) : base(btnText, ico, text)
        {
            MouseText = "回到上次粘贴物品栏前";

            OnClick += () =>
            {
                string ex = EditData.instance.InventoryReset(Main.LocalPlayer);

                if (ex != null) Main.NewText(ex);
            };
        }
    }
}
