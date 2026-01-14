using BedWars.BedWarsData;
using BedWars.Edit.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria;

namespace BedWars.Edit.UI.EditItem_EditData
{
    internal class EditMapPos : UIItemSwitchPos
    {
        public EditMapPos(string text) : base(text) { }

        public override void SetPos(Point pos)
        {
            string ex = EditData.instance.SetMapPos(pos);
            if (ex != null)
            {
                Main.NewText(ex);
                return;
            }

            pos = EditData.instance.DataInfo?.Pos ?? new Point(-1, -1);

            string text = $"位置在{pos.X},{pos.Y}";
            Main.NewText(text);
            CombatText.NewText(new Rectangle(pos.X * 16, pos.Y * 16, 0, 0), Color.LawnGreen, text, false, false);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsMouseHovering == false) return;
            if (EditData.instance.DataInfo is MapInfoData info == false) return;

            Main.instance.MouseText($"右键传送");

            if ((Main.mouseRight && Main.mouseRightRelease) == false) return;

            EditData.instance.Tp(info.rect);
        }
    }
}
