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

    /// <summary>
    /// 复制图格数据
    /// </summary>
    internal class EditCopyTileData : UIItemTextButton
    {
        public EditCopyTileData(string btnText, Texture2D ico = null, string text = null) : base(btnText, ico, text)
        {
            MouseText = "地图的箱子,告示牌数据";

            OnClick += () =>
            {
                string ex = EditData.instance.ChestCopy();
                if (ex != null)
                {
                    Main.NewText(ex);
                    return;
                }

                ex = EditData.instance.SignCopy();
                if (ex != null)
                {
                    Main.NewText(ex);
                    return;
                }

                Main.NewText("复制数据完成");
            };
        }
    }

    /// <summary>
    /// 粘贴图格数据
    /// </summary>
    internal class EditPasteTileData : UIItemTextButton
    {
        public EditPasteTileData(string btnText, Texture2D ico = null, string text = null) : base(btnText, ico, text)
        {
            MouseText = "箱子,告示牌";

            OnClick += () =>
            {
                string ex = EditData.instance.ChestPaste();
                if (ex != null)
                {
                    Main.NewText(ex);
                    return;
                }

                ex = EditData.instance.SignPaste();
                if (ex != null)
                {
                    Main.NewText(ex);
                    return;
                }

                Main.NewText("粘贴数据完成");
            };
        }
    }

    internal class ClearGameTile : UIItemTextButton
    {
        public ClearGameTile(string btnText, Texture2D ico = null, string text = null) : base(btnText, ico, text)
        {
            MouseText = "清空地图范围内的:方块,数据";

            OnClick += () =>
            {
                string ex = EditData.instance.TileGameClear();
                if (ex != null) Main.NewText(ex);
            };
        }
    }
}
