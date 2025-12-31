using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace BedWars.Edit
{
    public partial class EditData
    {
        /// <summary>
        /// 遍历在地图里的方块, 参数都是在地图中的位置
        /// </summary>
        protected void ForMapTile(int startx, int starty, int endx, int endy, Action<TileData, Tile> action)
        {
            if (startx < endx) return;
            if (starty < endy) return;

            int sizex = endx - startx + 1;
            int sizey = endy - starty + 1;

            if (sizex < 1) return;
            if (sizey < 1) return;

            for (int y = starty; y < sizey; ++y)
            {
                for (int x = startx; x < sizex; ++x)
                {
                    int tilex = DataInfo.pos.X + x;
                    int tiley = DataInfo.pos.Y + y;

                    if (DataCheck.InWorld(tilex, tiley) == false) continue;

                    Tile tile = Main.tile[tilex, tiley];

                    TileData data = DataTile[y][x];

                    action(data, tile);
                }
            }
        }

        public string TileCopy()
        {
            if (Data == null) return "地图数据为null";

            ForMapTile(0, 0, DataInfo.size.X - 1, DataInfo.size.Y - 1, (data, tile) =>
            {
                if (tile == null) return;

                TileData.Copy(data, tile);
            });

            return null;
        }

        public string TilePlace()
        {
            if (Data == null) return "地图数据为null";

            ForMapTile(0, 0, DataInfo.size.X - 1, DataInfo.size.Y - 1, (data, tile) =>
            {
                if (tile == null) return;

                TileData.Place(data, tile);
            });

            return null;
        }

        /// <summary>
        /// 超出地图部分会跳过, 两个参数都是世界位置
        /// </summary>
        public string TileCanActionSet(Point pos, Point sizePos, bool canAction)
        {
            if (Data == null) return "地图数据为null";

            if (pos.X < sizePos.X) return null;
            if (pos.Y < sizePos.Y) return null;

            bool inmapPos = Data.InMap(pos);
            bool inmapSize = Data.InMap(sizePos);

            if (inmapPos == false && inmapSize == false) return null;

            if (inmapPos == false)
            {

            }
            else if (inmapSize == false)
            {
                
            }

            return null;
        }
    }
}
