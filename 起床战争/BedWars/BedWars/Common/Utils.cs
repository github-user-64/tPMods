using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace BedWars.Common
{
    public static class Utils
    {
        /// <summary>
        /// 获取随机物品id, 排除<paramref name="exclude"/>, 如果不存在返回0
        /// </summary>
        public static int GetRandItemID(List<int> exclude = null)
        {
            int index = ModTool.Utils.Utils.GetRand(1, ItemID.Count);

            for (int i = index; ;)
            {
                if (
                    ItemID.Sets.Deprecated[i] ||//已弃用
                    exclude?.Contains(i) == true//排除
                    )
                {
                    ++i;
                    if (i < ItemID.Count == false) i = 1;//到结尾就从头开始
                    if (i == index) return 0;//如果绕一圈回来了
                    continue;
                }

                return i;
            }
        }

        public static bool InWorld(Vector2 pos, float fluff = 0)
        {
            if (pos.X < fluff || pos.X >= Main.maxTilesX * 16 - fluff ||
                pos.Y < fluff || pos.Y >= Main.maxTilesY * 16 - fluff)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取范围内的箱子, 位置是世界位置
        /// </summary>
        public static List<(Chest, int)> GetChests(Point pos, Point size)
        {
            List<(Chest, int)> chests = new List<(Chest, int)>();

            if (size.X < 1) return chests;
            if (size.Y < 1) return chests;
            int endx = pos.X + size.X - 1;
            int endy = pos.Y + size.Y - 1;

            for (int i = 0; i < Main.chest.Length; ++i)
            {
                Chest c = Main.chest[i];

                if (c == null) continue;
                if (c.bankChest) continue;//是类似猪猪存钱罐的东西

                if (c.x < pos.X) continue;
                if (c.y < pos.Y) continue;
                if (c.x > endx) continue;
                if (c.y > endy) continue;

                chests.Add((c, i));
            }

            return chests;
        }

        ///// <summary>
        ///// Returns the coordinates of the top left tile of the multitile at the location provided. Returns <see cref="Point16.NegativeOne"/> if no tile exists at the coordinates. If the tile does not have a TileObjectData, such as if it were a normal terrain tile, the provided coordinates will be returned.
        ///// </summary>
        //public static Point16 TopLeft(int i, int j)
        //{
        //    Tile tile = Main.tile[i, j];

        //    if (!tile.active())
        //    {
        //        return Point16.NegativeOne;
        //    }
        //    var tileData = TileObjectData.GetTileData(tile);
        //    if (tileData == null)
        //    {
        //        return new Point16(i, j);
        //    }
        //    int partFrameX = tile.frameX % tileData.CoordinateFullWidth;
        //    int partFrameY = tile.frameY % tileData.CoordinateFullHeight;
        //    int partX = partFrameX / (tileData.CoordinateWidth + tileData.CoordinatePadding);
        //    int partY = 0;
        //    for (int remainingFrameY = partFrameY; partY + 1 < tileData.Height && remainingFrameY - tileData.CoordinateHeights[partY] - tileData.CoordinatePadding >= 0; partY++)
        //    {
        //        remainingFrameY -= tileData.CoordinateHeights[partY] + tileData.CoordinatePadding;
        //    }
        //    i -= partX;
        //    j -= partY;
        //    return new Point16(i, j);
        //}
    }
}
