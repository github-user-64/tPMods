using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace BedWars.Edit
{
    public partial class EditData
    {
        /// <summary>
        /// 遍历在地图里的方块, 参数是在地图中的位置
        /// </summary>
        protected void ForMapTile(int startx, int starty, int width, int height, Action<TileData, Tile> action)
        {
            if (width < 1) return;
            if (height < 1) return;

            Rectangle mapSize = new Rectangle(0, 0, DataInfo.size.X, DataInfo.size.Y);
            Rectangle forSize = new Rectangle(startx, starty, width, height);
            Rectangle rect = Rectangle.Intersect(mapSize, forSize);
            if (rect.IsEmpty) return;

            for (int y = rect.Y; y < rect.Height; ++y)
            {
                for (int x = rect.X; x < rect.Width; ++x)
                {
                    int tilex = DataInfo.pos.X + x;
                    int tiley = DataInfo.pos.Y + y;

                    Tile tile = Main.tile[tilex, tiley];

                    TileData data = DataTile[y][x];

                    action(data, tile);
                }
            }
        }

        public string TileCopy()
        {
            if (Data == null) return "地图数据为null";

            ForMapTile(0, 0, DataInfo.size.X, DataInfo.size.Y, (data, tile) =>
            {
                if (tile == null) return;

                TileData.Copy(data, tile);
            });

            return null;
        }

        public string TilePlace()
        {
            if (Data == null) return "地图数据为null";

            ForMapTile(0, 0, DataInfo.size.X, DataInfo.size.Y, (data, tile) =>
            {
                if (tile == null) return;

                TileData.Place(data, tile);
            });

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

            ForMapTile(rect.X, rect.Y, rect.Width, rect.Height, (data, tile) =>
            {
                data.canAction = canAction;
            });

            return null;
        }
    }
}
