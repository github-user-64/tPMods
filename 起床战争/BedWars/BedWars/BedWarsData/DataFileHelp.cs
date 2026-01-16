using System;
using System.Collections.Generic;
using System.IO;
using tContentPatch.Utils;

namespace BedWars.BedWarsData
{
    public static class DataFileHelp
    {
        public const string FileNameMapInfo = "地图信息.txt";
        public const string FileNameSpawItem = "生成物品.txt";
        public const string FileNameTeam = "队伍信息.txt";
        public const string FileNameTile = "图格.tile";
        public const string FileNameChest = "箱子.txt";
        public const string FileNameSign = "告示牌.txt";
        public const string FileNameInventory = "物品栏.txt";
        public const string FileNameShops = "商店.txt";

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

            if (ReadFileTry(Path.Combine(dir, FileNameChest), ref mapData.Chests) == false)
            {
                throw new Exception("箱子读取失败");
            }

            if (ReadFileTry(Path.Combine(dir, FileNameSign), ref mapData.Signs) == false)
            {
                throw new Exception("告示牌读取失败");
            }

            if (ReadFileTry(Path.Combine(dir, FileNameInventory), ref mapData.Inventory) == false)
            {
                throw new Exception("物品栏读取失败");
            }

            if (ReadFileTry(Path.Combine(dir, FileNameShops), ref mapData.Shops) == false)
            {
                throw new Exception("商店读取失败");
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

            if (SaveFileTry(Path.Combine(dir, FileNameTile), mapData.Tile, mapData.Info.Width) == false)
            {
                throw new Exception("图格保存失败");
            }

            if (SaveFileTry(Path.Combine(dir, FileNameChest), mapData.Chests, true) == false)
            {
                throw new Exception("箱子保存失败");
            }

            if (SaveFileTry(Path.Combine(dir, FileNameSign), mapData.Signs, true) == false)
            {
                throw new Exception("告示牌保存失败");
            }

            if (SaveFileTry(Path.Combine(dir, FileNameInventory), mapData.Inventory, true) == false)
            {
                throw new Exception("物品栏保存失败");
            }

            if (SaveFileTry(Path.Combine(dir, FileNameShops), mapData.Shops, true) == false)
            {
                throw new Exception("商店保存失败");
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

        /// <summary>
        /// 图格
        /// </summary>
        public static bool ReadFileTry(string file, ref List<List<TileData>> data)
        {
            FileStream fs = null;

            try
            {
                fs = new FileStream(file, FileMode.Open);
                BinaryReader sr = new BinaryReader(fs);

                int width = sr.ReadInt32();
                int height = sr.ReadInt32();

                List<List<TileData>> datas = new List<List<TileData>>();

                for (int y = 0; y < height; ++y)
                {
                    List<TileData> td = new List<TileData>();
                    datas.Add(td);

                    for (int x = 0; x < width; ++x)
                    {
                        td.Add(new TileData().Read(sr));
                    }
                }

                data = datas;

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                fs?.Close();
            }
        }

        /// <summary>
        /// 图格
        /// </summary>
        public static bool SaveFileTry(string file, List<List<TileData>> data, int width)
        {
            FileStream fs = null;

            try
            {
                fs = new FileStream(file, FileMode.Open);
                BinaryWriter sw = new BinaryWriter(fs);

                int height = data.Count;

                sw.Write(width);
                sw.Write(height);

                for (int y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        data[y][x].Writer(sw);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                fs?.Close();
            }
        }
    }
}
