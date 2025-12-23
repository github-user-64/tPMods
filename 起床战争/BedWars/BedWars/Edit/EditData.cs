using BedWars.Common;
using System;
using System.Collections.Generic;

namespace BedWars.Edit
{
    public static class EditData
    {
        public static MapData Data { get; private set; } = null;
        public static List<SpawItem.SpawData> DataSpawItems => Data?.SpawItems;

        public static string LoadData()
        {
            try
            {
                MapData temp = MapDataHelp.ReadData(ThisMod.Dir);
                MapDataHelp.CheckData(temp);
                Data = temp;

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
                MapDataHelp.SaveData(ThisMod.Dir, Data);

                return null;
            }
            catch (Exception ex)
            {
                return $"保存失败:{ex.Message}";
            }
        }

        public static void NewData()
        {
            Data = new MapData();
            MapDataHelp.CheckData(Data);
        }
    }
}
