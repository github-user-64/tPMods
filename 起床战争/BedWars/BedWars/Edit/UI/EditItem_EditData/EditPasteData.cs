using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModTool.Common.UI;
using Terraria;

namespace BedWars.Edit.UI.EditItem_EditData
{
    /// <summary>
    /// 粘贴数据
    /// </summary>
    internal class EditPasteData : UIItemTextButton
    {
        public EditPasteData(string btnText, Texture2D ico = null, string text = null) : base(btnText, ico, text)
        {
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

                Main.NewText("粘贴完成");
            };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsMouseHovering) Main.instance.MouseText("箱子,告示牌");
        }
    }
}
