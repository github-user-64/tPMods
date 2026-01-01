using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent;

namespace BedWars.Common
{
    public class DrawUtils
    {
        public static void AddSize(ref Rectangle rect, int size)
        {
            rect.X -= size;
            rect.Y -= size;
            rect.Width += size * 2;
            rect.Height += size * 2;
        }

        public static void Draw_rectangle(Point startTile, Point endTile, Color borderColor, Color backgroundColor, int width = 0)
        {
            Draw_rectangle(startTile.X, startTile.Y, endTile.X, endTile.Y, borderColor, backgroundColor, width);
        }

        public static void Draw_rectangle(Rectangle sizeTile, Color borderColor, Color backgroundColor, int width = 0)
        {
            Draw_rectangle(sizeTile.X, sizeTile.Y,
                sizeTile.X + sizeTile.Width,
                sizeTile.Y + sizeTile.Height,
                borderColor, backgroundColor, width);
        }

        public static void Draw_rectangle(int startTileX, int startTileY, int endTileX, int endTileY,
            Color borderColor, Color backgroundColor, int width = 0)
        {
            Point startTileP = new Point(Math.Min(startTileX, endTileX), Math.Min(startTileY, endTileY));
            Point endTileP = new Point(Math.Max(startTileX, endTileX), Math.Max(startTileY, endTileY));

            Vector2 start = Terraria.Utils.ToWorldCoordinates(startTileP, 0, 0);
            Vector2 end = Terraria.Utils.ToWorldCoordinates(endTileP, 16, 16);
            start -= Main.screenPosition;
            end -= Main.screenPosition;

            Rectangle drawSize = new Rectangle((int)start.X, (int)start.Y, (int)(end.X - start.X), (int)(end.Y - start.Y));

            Rectangle screenSize = new Rectangle(0, 0, Main.screenWidth, Main.screenHeight);
            AddSize(ref screenSize, width);

            Rectangle drawRect = Rectangle.Intersect(drawSize, screenSize);
            if (drawRect.IsEmpty) return;

            //
            Rectangle rect = drawRect;
            AddSize(ref rect, -width);
            Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, rect, backgroundColor);
            //

            if (width < 1) return;

            start = Main.screenPosition + new Vector2(drawRect.X, drawRect.Y);
            end = start + new Vector2(drawRect.Width, drawRect.Height);

            //横线
            Terraria.Utils.DrawLine(Main.spriteBatch, start, new Vector2(end.X, start.Y), borderColor, borderColor, width);
            Terraria.Utils.DrawLine(Main.spriteBatch, end, new Vector2(start.X, end.Y), borderColor, borderColor, width);
            //竖线
            start.X += width;
            end.X -= width;
            //start.Y += width;
            //end.Y -= width;
            Terraria.Utils.DrawLine(Main.spriteBatch, start, new Vector2(start.X, end.Y), borderColor, borderColor, width);
            Terraria.Utils.DrawLine(Main.spriteBatch, end, new Vector2(end.X, start.Y), borderColor, borderColor, width);
        }
    }
}
