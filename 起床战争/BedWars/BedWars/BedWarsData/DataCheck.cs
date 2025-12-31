using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;

namespace BedWars.BedWarsData
{
    public static class DataCheck
    {
        /// <summary>
        /// 修复地图数据, 清理<see langword="null"/>, 不包括数据合理性, 检查数据合理性调用<see cref="CheckMapData(MapData)"/>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public static void Repair(MapData mapData)
        {
            if (mapData == null) throw new ArgumentNullException(nameof(mapData));

            if (mapData.Info == null) mapData.Info = new MapInfoData();

            CheckList(ref mapData.SpawItems);
            mapData.SpawItems.ForEach(i => i.mapData = mapData);
            
            CheckList(ref mapData.Teams);
            mapData.Teams.ForEach(i => i.mapData = mapData);

            RepairTile(mapData);
        }

        /// <summary>
        /// 将列表中的<see langword="null"/>用新建的对象填满
        /// </summary>
        private static void FillList<T>(List<T> list) where T : new()
        {
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i] == null) list[i] = new T();
            }
        }

        /// <summary>
        /// 删除<see langword="null"/>
        /// </summary>
        private static void CheckList<T>(ref List<T> list)
        {
            if (list == null) list = new List<T>();
            else list.RemoveAll(i => i == null);
        }

        /// <summary>
        /// 将列表用对象填满
        /// </summary>
        private static void CheckList<T>(ref List<T> list, int xcount) where T : new()
        {
            if (list == null) list = new List<T>();

            int add = xcount - list.Count;

            if (add == 0)//刚好
            {
                FillList(list);

                return;
            }

            if (add < 0)//超出
            {
                list.RemoveRange(xcount, -add);
                FillList(list);

                return;
            }

            FillList(list);

            for (int i = 0; i < add; ++i)
            {
                list.Add(new T());
            }
        }

        /// <summary>
        /// 将列表用对象填满
        /// </summary>
        private static void CheckList<T>(ref List<List<T>> list, int xcount, int ycount) where T : new()
        {
            CheckList(ref list, ycount);

            list.ForEach(i => CheckList(ref i, xcount));
        }

        /// <summary>
        /// <see cref="MapData.Tile"/>超出大小的就删除, 小于就添加, 填满<see langword="null"/>
        /// </summary>
        public static void RepairTile(MapData mapData)
        {
            CheckList(ref mapData.Tile, mapData.Info.size.X, mapData.Info.size.Y);
        }

        public static bool InWorld(Point pos)
        {
            return InWorld(pos.X, pos.Y);
        }

        public static bool InWorld(int x, int y)
        {
            return WorldGen.InWorld(x, y, 2);
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
            return mapData.InMap(pos.X, pos.Y);
        }

        public static bool InMap(this MapData mapData, int x, int y)
        {
            if (mapData?.Info == null) return false;
            if (x < mapData.Info.pos.X) return false;
            if (y < mapData.Info.pos.Y) return false;
            if (x > mapData.Info.pos.X + mapData.Info.size.X - 1) return false;
            if (y > mapData.Info.pos.Y + mapData.Info.size.Y - 1) return false;

            return true;
        }

        /// <summary>
        /// 相对位置
        /// </summary>
        public static bool InMapRelative(this MapData mapData, Point pos)
        {
            return mapData.InMapRelative(pos.X, pos.Y);
        }

        /// <summary>
        /// 相对位置
        /// </summary>
        public static bool InMapRelative(this MapData mapData, int x, int y)
        {
            if (mapData?.Info == null) return false;
            if (x < 0) return false;
            if (y < 0) return false;
            if (x > mapData.Info.size.X - 1) return false;
            if (y > mapData.Info.size.Y - 1) return false;

            return true;
        }
    }
}
