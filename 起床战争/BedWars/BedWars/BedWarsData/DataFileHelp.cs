using System;
using System.IO;
using tContentPatch.Utils;

namespace BedWars.BedWarsData
{
    public static class DataFileHelp
    {
        public const string FileNameMapInfo = "地图信息.txt";
        public const string FileNameSpawItem = "生成物品.txt";
        public const string FileNameTeam = "队伍信息.txt";
        public const string FileNameTile = "图格.txt";

        /// <summary>
        /// 读取地图数据, 不会返回<see langword="null"/>但里面的东西会为<see langword="null"/>
        /// </summary>
        /// <exception cref="DirectoryNotFoundException"/>
        public static MapData ReadData(string dir)
        {
            if (Directory.Exists(dir) == false) throw new DirectoryNotFoundException();

            MapData mapData = new MapData();

            if (ReadFileTry(Path.Combine(dir, FileNameMapInfo), ref mapData.Info) == false)
            {
                throw new Exception("地图信息读取失败");
            }

            if (ReadFileTry(Path.Combine(dir, FileNameSpawItem), ref mapData.SpawItems) == false)
            {
                throw new Exception("生成物品读取失败");
            }

            if (ReadFileTry(Path.Combine(dir, FileNameTeam), ref mapData.Teams) == false)
            {
                throw new Exception("队伍信息读取失败");
            }

            if (ReadFileTry(Path.Combine(dir, FileNameTile), ref mapData.Tile) == false)
            {
                throw new Exception("图格读取失败");
            }

            return mapData;
        }

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public static void SaveData(string dir, MapData mapData)
        {
            if (mapData == null) throw new ArgumentNullException(nameof(mapData));
            if (Directory.Exists(dir) == false) throw new DirectoryNotFoundException();

            if (SaveFileTry(Path.Combine(dir, FileNameMapInfo), mapData.Info, true) == false)
            {
                throw new Exception("地图信息保存失败");
            }

            if (SaveFileTry(Path.Combine(dir, FileNameSpawItem), mapData.SpawItems, true) == false)
            {
                throw new Exception("生成物品保存失败");
            }

            if (SaveFileTry(Path.Combine(dir, FileNameTeam), mapData.Teams, true) == false)
            {
                throw new Exception("队伍信息保存失败");
            }

            if (SaveFileTry(Path.Combine(dir, FileNameTile), mapData.Teams, false) == false)
            {
                throw new Exception("图格保存失败");
            }
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
    }
}
