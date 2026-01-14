using Microsoft.Xna.Framework;
using Terraria;

namespace BedWars.Edit.UI.EditItem_DisplayData
{
    internal static class DrawUtils
    {
        public static void Draw(Rectangle rect, Color color, string text = null, float textDis = 0, float anchory = 0.5f)
        {
            Common.DrawUtils.Draw_rectangle(rect, color * 0.9f, color * 0.2f, 1);

            if (text == null) return;
            if (text == string.Empty) return;

            Vector2 textP = GetPos(rect, textDis);

            Utils.DrawBorderString(Main.spriteBatch, text, textP - Main.screenPosition, color, anchorx: 0.5f, anchory: anchory);
        }

        public static void Draw(Point pos, Point posAdd, Color color, string text = null, float textDis = 0)
        {
            Draw(new Rectangle(pos.X + posAdd.X, pos.Y + posAdd.Y, 1, 1), color, text, textDis, 0);
        }

        private static Vector2 GetPos(Rectangle rect, float textDis = 0)
        {
            Point pos = new Point(rect.X, rect.Y);

            Vector2 textP = pos.ToWorldCoordinates(0, 0);
            textP += new Point(rect.Width, rect.Height).ToWorldCoordinates() / 2;

            Vector2 v = textP - Main.LocalPlayer.Center;
            if (v.Length() > textDis)
            {
                v = Vector2.Normalize(v) * textDis;
                textP = Main.LocalPlayer.Center + v;
            }

            return textP;
        }
    }
}
