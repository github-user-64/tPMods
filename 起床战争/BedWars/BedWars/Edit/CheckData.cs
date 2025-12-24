using BedWars.BedWarsData;
using BedWars.Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;

namespace BedWars.Edit
{
    public static class CheckData
    {
        /// <summary>
        /// 检查修复地图数据, 清理<see langword="null"/>, 不包括数据合理性
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

        public static void CheckMapData(MapData mapData)
        {
            if (Utils.Utils.InWorld(mapData.Info.pos, 16 * 2) == false)
            {
                mapData.Info.pos = new Vector2(Main.spawnTileX, Main.spawnTileY) * 16;

                mapData.SpawItems.RemoveAll(i => Utils.Utils.InWorld(mapData.Info.pos + i.pos, 16) == false);
            }
        }

        public static string CheckMapPos(MapData mapData, bool repair)
        {
            if (Utils.Utils.InWorld(mapData.Info.pos, 16 * 2) == false)
            {
                if (!repair) return "地图不在世界范围内";
                mapData.Info.pos = new Vector2(Main.spawnTileX, Main.spawnTileY) * 16;
            }

            mapData.SpawItems.RemoveAll(i => Utils.Utils.InWorld(mapData.Info.pos + i.pos, 16) == false);

            return null;
        }

        public static string CheckSpawItem(MapData mapData, bool repair)
        {
            foreach (SpawItemData i in mapData.SpawItems)
            {
                if (Utils.Utils.InWorld(mapData.Info.pos + i.pos) == false)
                {

                }
            }

            return null;
        }
    }
}
