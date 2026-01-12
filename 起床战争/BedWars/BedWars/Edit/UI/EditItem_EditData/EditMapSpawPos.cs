using BedWars.BedWarsData;
using BedWars.Edit.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria;

namespace BedWars.Edit.UI.EditItem_EditData
{
    internal class EditMapSpawPos : UIItemSwitchPos
    {
        public EditMapSpawPos(string text) : base(text) { }

        public override void SetPos(Point pos)
        {
            string ex = EditData.instance.SetMapSpawPos(pos);
            if (ex != null)
            {
                Main.NewText(ex);
                return;
            }

            pos = EditData.instance.DataInfo?.spawPos ?? new Point(-1, -1);

            string text = $"位置在{pos.X},{pos.Y}";
            Main.NewText(text);
            CombatText.NewText(new Rectangle(pos.X * 16, pos.Y * 16, 0, 0), Color.LawnGreen, text, false, false);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsMouseHovering == false) return;
            if (EditData.instance.DataInfo is MapInfoData info == false) return;

            Point point = new Point(info.pos.X + info.spawPos.X, info.pos.Y + info.spawPos.Y);
            Vector2 pos = point.ToWorldCoordinates();

            Main.instance.MouseText($"右键传送{pos.X}, {pos.Y}");

            if ((Main.mouseRight && Main.mouseRightRelease) == false) return;

            if (WorldGen.InWorld(point.X, point.Y) == false)
            {
                Main.NewText($"超出世界:{pos.X},{pos.Y}");
                return;
            }

            Main.LocalPlayer.Center = pos;
        }
    }
}
