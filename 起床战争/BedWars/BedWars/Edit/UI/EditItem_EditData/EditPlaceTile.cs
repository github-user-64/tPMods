using Microsoft.Xna.Framework.Graphics;
using ModTool.Common.UI;
using Terraria;

namespace BedWars.Edit.UI.EditItem_EditData
{
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
