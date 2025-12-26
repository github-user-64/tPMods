using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent;

namespace BedWars.Common
{
    public class DrawUtils
    {
        public static void LimitDrawWorld(ref Vector2 posWorld, int addSize)
        {
            Vector2 v = posWorld - Main.screenPosition;

            LimitDraw(ref v, addSize);

            posWorld = v + Main.screenPosition;
        }

        public static void LimitDraw(ref Vector2 pos, int addSize)
        {
            Vector2 v = pos;

            if (v.X < -addSize) v.X = -addSize;
            else if (v.X > Main.screenWidth + addSize) v.X = Main.screenWidth + addSize;

            if (v.Y < -addSize) v.Y = -addSize;
            else if (v.Y > Main.screenHeight + addSize) v.Y = Main.screenHeight + addSize;

            pos = v;
        }

        public static void Draw_rectangle(Point startTile, Point endTile, Color borderColor, Color backgroundColor, int width = 0)
        {
            Point p1 = new Point(Math.Min(startTile.X, endTile.X), Math.Min(startTile.Y, endTile.Y));
            Point p2 = new Point(Math.Max(startTile.X, endTile.X), Math.Max(startTile.Y, endTile.Y));
            startTile = p1;
            endTile = p2;

            endTile.X += 1;
            endTile.Y += 1;
            Vector2 start = Terraria.Utils.ToWorldCoordinates(startTile, 0, 0);
            Vector2 end = Terraria.Utils.ToWorldCoordinates(endTile, 0, 0);

            LimitDrawWorld(ref start, 16);
            LimitDrawWorld(ref end, 16);

            //
            Vector2 rectPos = start - Main.screenPosition;
            rectPos.X += width;
            rectPos.Y += width;

            Vector2 rectSize = end - start;
            rectSize.X -= width * 2;
            rectSize.Y -= width * 2;
            Rectangle rect = new Rectangle(0, 0, (int)rectSize.X, (int)rectSize.Y);
            Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, rectPos, rect, backgroundColor);
            //

            if (width < 1) return;

            //横线
            Terraria.Utils.DrawLine(Main.spriteBatch, start, new Vector2(end.X, start.Y), borderColor, borderColor, width);
            Terraria.Utils.DrawLine(Main.spriteBatch, end, new Vector2(start.X, end.Y), borderColor, borderColor, width);
            //竖线
            start.X += width;
            end.X -= width;
            Terraria.Utils.DrawLine(Main.spriteBatch, start, new Vector2(start.X, end.Y), borderColor, borderColor, width);
            Terraria.Utils.DrawLine(Main.spriteBatch, end, new Vector2(end.X, start.Y), borderColor, borderColor, width);
        }
    }
}
