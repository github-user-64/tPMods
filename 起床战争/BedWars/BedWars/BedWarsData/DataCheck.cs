using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using static PlayerAccount.Common.FunctionCommand.accAction;

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
        }

        private static void CheckList<T>(ref List<T> list)
        {
            if (list == null) list = new List<T>();
            else list.RemoveAll(i => i == null);
        }

        /// <exception cref="Exception"/>
        public static void CheckMapData(this MapData mapData, bool repair)
        {
            mapData.CheckMapInfo(repair);
            mapData.CheckSpawItem(repair);
        }

        public static bool InWorld_MapInfoPos(Point pos)
        {
            return WorldGen.InWorld(pos.X, pos.Y, 2);
        }

        public static bool InWorld_MapInfoSize(Point pos, Point size)
        {
            return WorldGen.InWorld(pos.X + size.X, pos.Y + size.Y, 2);
        }

        /// <exception cref="Exception"/>
        public static void CheckMapInfo(this MapData mapData, bool repair)
        {
            if (InWorld_MapInfoPos(mapData.Info.pos) == false)
            {
                if (!repair) throw new Exception("地图位置超出世界");

                mapData.Info.pos = new Point(Main.spawnTileX, Main.spawnTileY);
            }

            if (mapData.Info.size.X < 2) throw new Exception("地图大小不能小于2");
            if (mapData.Info.size.Y < 2) throw new Exception("地图大小不能小于2");

            if (InWorld_MapInfoSize(mapData.Info.pos, mapData.Info.size) == false)
            {
                throw new Exception("地图大小超出世界");
            }
        }

        /// <exception cref="Exception"/>
        public static void CheckSpawItem(this MapData mapData, bool repair)
        {
            List<SpawItemData> del = new List<SpawItemData>();

            foreach (SpawItemData i in mapData.SpawItems)
            {
                if (mapData.InMap(i.pos)) continue;
                if (!repair) throw new Exception("生成物品超出地图");

                del.Add(i);
            }

            del.ForEach(i => mapData.SpawItems.Remove(i));
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

        public static void SetMapData_SpawItem(this MapData mapData)
        {
            mapData.SpawItems.ForEach(i => i.mapData = mapData);
        }
    }
}
