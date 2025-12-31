using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;

namespace BedWars.BedWarsData
{
    public static class DataCheck
    {
        /// <summary>
        /// 修复地图数据, 清理<see langword="null"/>, 不包括数据合理性
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public static void Repair(MapData mapData)
        {
            if (mapData == null) throw new ArgumentNullException(nameof(mapData));

            if (mapData.Info == null) mapData.Info = new MapInfoData();

            CheckList(ref mapData.SpawItems);
            CheckList(ref mapData.Teams);
            CheckList(ref mapData.CanTileDatas);

            mapData.SpawItems.ForEach(i => i.mapData = mapData);
            mapData.Teams.ForEach(i => i.mapData = mapData);
        }

        private static void CheckList<T>(ref List<T> list)
        {
            if (list == null) list = new List<T>();
            else list.RemoveAll(i => i == null);
        }

        public static bool InWorld(Point pos)
        {
            return WorldGen.InWorld(pos.X, pos.Y, 2);
        }

        public static bool InWorldSize(Point pos, Point size)
        {
            return WorldGen.InWorld(pos.X + size.X, pos.Y + size.Y, 2);
        }

        /// <exception cref="Exception"/>
        public static void CheckMapData(MapData mapData)
        {
            mapData.Check();
        }

        public static bool InMap(this MapData mapData, Point pos)
        {
            if (mapData?.Info == null) return false;
            if (pos.X < mapData.Info.pos.X) return false;
            if (pos.Y < mapData.Info.pos.Y) return false;
            if (pos.X > mapData.Info.pos.X + mapData.Info.size.X - 1) return false;
            if (pos.Y > mapData.Info.pos.Y + mapData.Info.size.Y - 1) return false;

            return true;
        }

        /// <summary>
        /// 相对位置
        /// </summary>
        public static bool InMapRelative(this MapData mapData, Point pos)
        {
            if (mapData?.Info == null) return false;
            if (pos.X < 0) return false;
            if (pos.Y < 0) return false;
            if (pos.X > mapData.Info.size.X - 1) return false;
            if (pos.Y > mapData.Info.size.Y - 1) return false;

            return true;
        }
    }
}
