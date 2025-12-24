using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BedWars.Edit
{
    public static class EditData
    {
        public static MapData Data { get; private set; } = null;
        public static MapInfoData Info => Data?.Info;
        public static List<SpawItemData> DataSpawItems => Data?.SpawItems;

        public static string LoadData(Action<string> print = null)
        {
            try
            {
                print?.Invoke("加载数据");

                MapData temp = MapDataHelp.ReadData(ThisMod.DirMapData);
                if (temp.Info == null) print?.Invoke("地图信息为null");
                if (temp.SpawItems == null) print?.Invoke("生成物品为null");

                print?.Invoke("检查并修复地图数据");
                CheckData.Repair(temp);

                Data = temp;
                print?.Invoke("加载完成");

                return null;
            }
            catch (Exception ex)
            {
                return $"加载失败:{ex.Message}";
            }
        }

        public static string SaveData()
        {
            try
            {
                MapDataHelp.SaveData(ThisMod.DirMapData, Data);

                return null;
            }
            catch (Exception ex)
            {
                return $"保存失败:{ex.Message}";
            }
        }

        public static void SetDefaultData()
        {
            Data = new MapData();
            CheckData.Repair(Data);
        }

        public static string SetMapPos(Vector2 pos)
        {
            if (Data == null) return "地图数据为null";

            return null;
        }
    }
}
