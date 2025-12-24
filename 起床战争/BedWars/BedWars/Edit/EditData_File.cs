using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using System;
using System.IO;

namespace BedWars.Edit
{
    public static partial class EditData
    {
        public static string LoadData(MapData temp, Action<string> print = null)
        {
            try
            {
                if (temp.Info == null) print?.Invoke("地图信息为null");
                if (temp.SpawItems == null) print?.Invoke("生成物品为null");

                print?.Invoke("检查并修复数据");
                DataCheck.Repair(Data);
                DataCheck.CheckMapData(temp, true);

                Data = temp;
                print?.Invoke("加载完成");

                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string LoadData(Action<string> print = null)
        {
            try
            {
                print?.Invoke("加载数据");

                MapData temp = DataFileHelp.ReadData(ThisMod.DirMapData);
                
                return LoadData(temp, print);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string SaveData()
        {
            try
            {
                Directory.CreateDirectory(ThisMod.DirMapData);
                DataFileHelp.SaveData(ThisMod.DirMapData, Data);

                return null;
            }
            catch (Exception ex)
            {
                return $"保存失败:{ex.Message}";
            }
        }

        public static string ResetData()
        {
            MapData temp = new MapData();
            temp.Info = new MapInfoData();
            temp.Info.size = new Point(2, 2);

            return LoadData(temp);
        }
    }
}
