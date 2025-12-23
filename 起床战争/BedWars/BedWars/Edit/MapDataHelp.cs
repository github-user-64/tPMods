using BedWars.Common;
using System;
using System.Collections.Generic;
using System.IO;
using tContentPatch.Utils;

namespace BedWars.Edit
{
    public static class MapDataHelp
    {
        public const string FileNameSpawItem = "生成物品.txt";

        /// <summary>
        /// 读取地图数据
        /// </summary>
        /// <exception cref="DirectoryNotFoundException"/>
        public static MapData ReadData(string dir)
        {
            if (Directory.Exists(dir) == false) throw new DirectoryNotFoundException();

            MapData mapData = new MapData();

            if (ReadFileTry(Path.Combine(dir, FileNameSpawItem), ref mapData.SpawItems) == false)
            {
                throw new Exception("生成物品数据读取失败");
            }

            return mapData;
        }

        public static bool ReadFileTry<T>(string file, ref T data)
        {
            try
            {
                data = MyJson1.Get2<T>(file);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SaveFileTry<T>(string file, T data, bool indented = false)
        {
            try
            {
                MyJson1.Save(data, file, indented);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public static void SaveData(string dir, MapData mapData)
        {
            if (mapData == null) throw new ArgumentNullException(nameof(mapData));
            if (Directory.Exists(dir) == false) throw new DirectoryNotFoundException();

            if (SaveFileTry(Path.Combine(dir, FileNameSpawItem), mapData.SpawItems, true) == false)
            {
                throw new Exception("生成物品数据保存失败");
            }
        }

        /// <summary>
        /// 检查修复地图数据
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public static void CheckData(MapData mapData)
        {
            if (mapData == null) throw new ArgumentNullException(nameof(mapData));

            if (mapData.SpawItems == null)
                mapData.SpawItems = new List<SpawItem.SpawData>();
            else
                mapData.SpawItems.RemoveAll(i => i == null);
        }
    }
}
