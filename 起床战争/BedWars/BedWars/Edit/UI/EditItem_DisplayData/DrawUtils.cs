using Microsoft.Xna.Framework;
using Terraria;

namespace BedWars.Edit.UI.EditItem_DisplayData
{
    internal static class DrawUtils
    {
        public static void Draw(Point pos, Point posAdd, Color color, string text = null, float textDis = 0)
        {
            pos.X += posAdd.X;
            pos.Y += posAdd.Y;

            Common.DrawUtils.Draw_rectangle(pos, pos, color * 0.9f, color * 0.2f, 1);

            if (text == null) return;
            if (text == string.Empty) return;

            Vector2 textP = pos.ToWorldCoordinates();
            Vector2 v = textP - Main.LocalPlayer.Center;
            if (v.Length() > textDis)
            {
                v = Vector2.Normalize(v) * textDis;
                textP = Main.LocalPlayer.Center + v;
            }

            Utils.DrawBorderString(Main.spriteBatch, text, textP - Main.screenPosition, color, anchorx: 0.5f, anchory: 0f);
        }
    }
}
