using Microsoft.Xna.Framework;
using Terraria;

namespace BedWars.Edit.UI.EditItem
{
    internal class EditMapSize : UISwitchPos
    {
        public EditMapSize(string text) : base(text) { }

        public override void SetPos(Point pos)
        {
            string ex = EditData.SetMapSize(pos);
            if (ex != null)
            {
                Main.NewText(ex);
                return;
            }

            Point size = EditData.DataInfo.size;

            CombatText.NewText(new Rectangle(pos.X, pos.Y, 0, 0), Color.LawnGreen, $"大小为{size.X},{size.Y}", true, false);
        }

        public override void DrawSwitchPos(Point pos)
        {
            Point p = EditData.DataInfo?.pos ?? pos;

            Common.DrawUtils.Draw_rectangle(p, pos, Color.LawnGreen * 0.9f, Color.LawnGreen * 0.2f, 2);

            Point size = new Point(pos.X - p.X, pos.Y - p.Y);

            Color textC = size.X < 2 || size.Y < 2 ? Color.Red : Color.LawnGreen;
            Utils.DrawBorderString(Main.spriteBatch, $"大小:{size.X},{size.Y}", Main.MouseScreen + new Vector2(0, 20), textC);
        }
    }
}
