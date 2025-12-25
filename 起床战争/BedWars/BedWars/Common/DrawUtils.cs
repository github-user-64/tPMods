using Microsoft.Xna.Framework;
using Terraria;

namespace BedWars.Common
{
    public class DrawUtils
    {
        public static void Draw_rectangle(Point startTile, Point endTile, Color borderColor, Color backgroundColor)
        {
            //Vector2 _v = startWord;
            //startWord = new Vector2(Math.Min(startWord.X, endWord.X), Math.Min(startWord.Y, endWord.Y));
            //endWord = new Vector2(Math.Max(_v.X, endWord.X), Math.Max(_v.Y, endWord.Y));

            //Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, p1Word - Main.screenPosition, rect, backgroundColor);

            Terraria.Utils.DrawRectForTilesInWorld(Main.spriteBatch, startTile, endTile, borderColor);
        }
    }
}
