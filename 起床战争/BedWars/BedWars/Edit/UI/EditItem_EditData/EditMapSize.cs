using BedWars.Edit.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria;

namespace BedWars.Edit.UI.EditItem_EditData
{
    internal class EditMapSize : UIItemSwitchPos
    {
        public EditMapSize(string text) : base(text) { }

        public override void SetPos(Point pos)
        {
            Point p = EditData.instance.DataInfo?.pos ?? pos;//如果为地图数据为null那么设置也会失败, 不用担心吧
            Point size = new Point(pos.X - p.X + 1, pos.Y - p.Y + 1);

            string ex = EditData.instance.SetMapSize(size);
            if (ex != null)
            {
                Main.NewText(ex);
                return;
            }

            size = EditData.instance.DataInfo.size;

            string text = $"大小为{size.X},{size.Y}";
            Main.NewText(text);
            CombatText.NewText(new Rectangle(pos.X * 16, pos.Y * 16, 0, 0), Color.LawnGreen, text, false, false);
        }

        public override void DrawSwitchPos(Point pos)
        {
            Point p = EditData.instance.DataInfo?.pos ?? pos;
            Point size = new Point(pos.X - p.X + 1, pos.Y - p.Y + 1);

            Color rectC = Color.LawnGreen;
            Color textC = Color.LawnGreen;
            if (size.X < 2 || size.Y < 2)
            {
                rectC = Color.Red;
                textC = Color.Red;
            }

            Common.DrawUtils.Draw_rectangle(p, pos, rectC * 0.9f, rectC * 0.2f, 2);

            Utils.DrawBorderString(Main.spriteBatch, $"大小:{size.X},{size.Y}", Main.MouseScreen + new Vector2(0, 22), textC);
        }
    }
}
