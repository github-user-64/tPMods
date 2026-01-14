using BedWars.Edit.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria;

namespace BedWars.Edit.UI.EditItem_EditData
{
    internal class EditMapVoidHeight : UIItemSwitchPos
    {
        public EditMapVoidHeight(string text) : base(text) { }

        public override void SetPos(Point pos)
        {
            int height = PosToHeight(pos);

            string ex = EditData.instance.SetVoidHeight(height);//如果为地图数据为null那么设置也会失败, 不用担心吧
            if (ex != null)
            {
                Main.NewText(ex);
                return;
            }

            height = EditData.instance.DataInfo?.voidHeight ?? -1;

            string text = $"高度为{height}";
            Main.NewText(text);
            CombatText.NewText(Main.LocalPlayer.getRect(), Color.LawnGreen, text, false, false);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsMouseHovering) Main.instance.MouseText("低于虚空高度的玩家会掉血, 高度小于1不生效");
        }

        public override void OnDrawSwitchPos(Point pos)
        {
            int height = PosToHeight(pos);

            Point pos2 = pos;

            if (EditData.instance.DataInfo != null)
            {
                pos = EditData.instance.DataInfo.Pos;
                pos.Y += EditData.instance.DataInfo.Height - 1;

                pos2 = pos;
                pos2.X += EditData.instance.DataInfo.Width - 1;
                pos2.Y -= height - 1;
            }

            Color rectC = Color.Pink;
            Color textC = Color.Pink;
            if (height < 1)
            {
                rectC = Color.Red;
                textC = Color.Red;
            }

            Utils.DrawBorderString(Main.spriteBatch, $"高度:{height}", Main.MouseScreen + new Vector2(0, 22), textC);

            if (height < 1) return;
            Common.DrawUtils.Draw_rectangle(pos, pos2, rectC * 0.9f, rectC * 0.2f, 2);
        }

        private int PosToHeight(Point pos)
        {
            if (EditData.instance.DataInfo == null) return 0;

            pos.Y -= EditData.instance.DataInfo.Y;

            int height = EditData.instance.DataInfo.Height - pos.Y;

            if (height < 0) height = 0;
            else if (height > EditData.instance.DataInfo.Height) height = EditData.instance.DataInfo.Height;

            return height;
        }
    }
}
