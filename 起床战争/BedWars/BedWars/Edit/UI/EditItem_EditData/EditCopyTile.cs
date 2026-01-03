using Microsoft.Xna.Framework.Graphics;
using ModTool.Common.UI;
using Terraria;

namespace BedWars.Edit.UI.EditItem_EditData
{
    internal class EditCopyTile : UIItemTextButton
    {
        public EditCopyTile(string btnText, Texture2D ico = null, string text = null) : base(btnText, ico, text)
        {
            OnClick += () =>
            {
                string ex = EditData.instance.TileCopy();

                Main.NewText(ex ?? "复制图格完成");
            };
        }
    }
}
