using Microsoft.Xna.Framework.Graphics;
using ModTool.Common.UI;
using Terraria;

namespace BedWars.Edit.UI.EditItem_EditData
{
    internal class EditCopyTile : UIItemTextButton
    {
        public EditCopyTile(string btnText, Texture2D ico = null, string text = null) : base(btnText, ico, text)
        {
            MouseText = "地图的图格数据";

            OnClick += () =>
            {
                string ex = EditData.instance.TileCopy();

                Main.NewText(ex ?? "复制图格完成");
            };
        }
    }

    internal class EditPlaceTile : UIItemTextButton
    {
        public EditPlaceTile(string btnText, Texture2D ico = null, string text = null) : base(btnText, ico, text)
        {
            OnClick += () =>
            {
                string ex = EditData.instance.TilePlace();

                Main.NewText(ex ?? "放置图格完成");
            };
        }
    }
}
