using BedWars.BedWarsData;
using System;
using System.IO;

namespace BedWars.Edit
{
    public static class MapDataHelp
    {
        public const string FileNameMapInfo = "地图信息.txt";
        public const string FileNameSpawItem = "生成物品.txt";

        /// <summary>
        /// 读取地图数据, 不会返回<see langword="null"/>但里面的东西会为<see langword="null"/>
        /// </summary>
        /// <exception cref="DirectoryNotFoundException"/>
        public static MapData ReadData(string dir)
        {
            if (Directory.Exists(dir) == false) throw new DirectoryNotFoundException();

            MapData mapData = new MapData();

            if (Utils.Utils.ReadFileTry(Path.Combine(dir, FileNameMapInfo), ref mapData.Info) == false)
            {
                throw new Exception("地图信息读取失败");
            }

            if (Utils.Utils.ReadFileTry(Path.Combine(dir, FileNameSpawItem), ref mapData.SpawItems) == false)
            {
                throw new Exception("生成物品读取失败");
            }

            return mapData;
        }

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public static void SaveData(string dir, MapData mapData)
        {
            if (mapData == null) throw new ArgumentNullException(nameof(mapData));
            if (Directory.Exists(dir) == false) throw new DirectoryNotFoundException();

            if (Utils.Utils.SaveFileTry(Path.Combine(dir, FileNameMapInfo), mapData.Info, true) == false)
            {
                throw new Exception("地图信息保存失败");
            }

            if (Utils.Utils.SaveFileTry(Path.Combine(dir, FileNameSpawItem), mapData.SpawItems, true) == false)
            {
                throw new Exception("生成物品保存失败");
            }
        }
    }
}
