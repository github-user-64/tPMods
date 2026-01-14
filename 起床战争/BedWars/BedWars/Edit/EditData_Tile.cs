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
        /// 遍历在地图里的方块, 参数是在世界中的位置
        /// </summary>
        protected void ForMapTile(Rectangle rect, Action<TileData, Tile, int, int> action)
        {
            rect = Rectangle.Intersect(DataInfo.rect, rect);
            if (rect.IsEmpty) return;

            int xlen = rect.X + rect.Width;
            int ylen = rect.Y + rect.Height;

            for (int y = rect.Y; y < ylen; ++y)
            {
                for (int x = rect.X; x < xlen; ++x)
                {
                    Tile tile = Main.tile[x, y];

                    int datax = x - DataInfo.X;
                    int datay = y - DataInfo.Y;

                    TileData data = DataTile[datay][datax];

                    action(data, tile, x, y);
                }
            }
        }

        public string TileCopy()
        {
            if (Data == null) return "地图数据为null";

            ForMapTile(DataInfo.rect, (data, tile, x, y) =>
            {
                if (tile == null) return;

                TileData.Copy(data, tile);
            });

            return null;
        }

        public string TilePlace()
        {
            if (Data == null) return "地图数据为null";

            ForMapTile(DataInfo.rect, (data, tile, x, y) =>
            {
                if (tile == null) return;

                TileData.Place(data, tile);

                if (data.wall > WallID.None) WorldGen.SquareWallFrame(x, y);//没这个墙壁会乱糟糟的
            });

            Common.Utils.ClearInRangeChest(DataInfo.rect);//清除箱子
            Common.Utils.ClearInRangeSign(DataInfo.rect);//清除告示牌

            return null;
        }

        /// <summary>
        /// 超出地图部分会跳过, 参数是世界位置
        /// </summary>
        public string TileCanActionSet(Rectangle rect, bool canAction)
        {
            if (Data == null) return "地图数据为null";

            ForMapTile(rect, (data, tile, x, y) =>
            {
                data.canAction = canAction;
            });

            return null;
        }
    }
}
