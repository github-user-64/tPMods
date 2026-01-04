using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace BedWars.Edit
{
    public partial class EditData
    {
        /// <summary>
        /// 遍历在地图里的方块, 参数是在地图中的位置
        /// </summary>
        protected void ForMapTile(int startMapX, int startMapY, int width, int height, Action<TileData, Tile, int, int> action)
        {
            if (width < 1) return;
            if (height < 1) return;

            Rectangle mapSize = new Rectangle(0, 0, DataInfo.size.X, DataInfo.size.Y);
            Rectangle forSize = new Rectangle(startMapX, startMapY, width, height);
            Rectangle rect = Rectangle.Intersect(mapSize, forSize);
            if (rect.IsEmpty) return;

            int xlen = rect.X + rect.Width;
            int ylen = rect.Y + rect.Height;

            for (int y = rect.Y; y < ylen; ++y)
            {
                for (int x = rect.X; x < xlen; ++x)
                {
                    int tilex = DataInfo.pos.X + x;
                    int tiley = DataInfo.pos.Y + y;

                    Tile tile = Main.tile[tilex, tiley];

                    TileData data = DataTile[y][x];

                    action(data, tile, tilex, tiley);
                }
            }
        }

        public string TileCopy()
        {
            if (Data == null) return "地图数据为null";

            ForMapTile(0, 0, DataInfo.size.X, DataInfo.size.Y, (data, tile, x, y) =>
            {
                if (tile == null) return;

                TileData.Copy(data, tile);
            });

            return null;
        }

        public string TilePlace()
        {
            if (Data == null) return "地图数据为null";

            ForMapTile(0, 0, DataInfo.size.X, DataInfo.size.Y, (data, tile, x, y) =>
            {
                if (tile == null) return;

                TileData.Place(data, tile);

                if (data.wall > WallID.None) WorldGen.SquareWallFrame(x, y);//没这个墙壁会乱糟糟的
            });

            Common.Utils.ClearInRangeChest(DataInfo.pos, DataInfo.size);//清除箱子
            Common.Utils.ClearInRangeSign(DataInfo.pos, DataInfo.size);//清除告示牌

            return null;
        }

        /// <summary>
        /// 超出地图部分会跳过, 参数是世界位置
        /// </summary>
        public string TileCanActionSet(Rectangle rect, bool canAction)
        {
            if (Data == null) return "地图数据为null";

            rect.X -= DataInfo.pos.X;
            rect.Y -= DataInfo.pos.Y;

            ForMapTile(rect.X, rect.Y, rect.Width, rect.Height, (data, tile, x, y) =>
            {
                data.canAction = canAction;
            });

            return null;
        }
    }
}
