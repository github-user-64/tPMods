using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using tContentPatch;
using Terraria;

namespace BedWars.Edit
{
    public partial class EditData
    {
        private class asd : PatchMain
        {
            public override void OnEnterWorldPrefix()
            {
                instance.Data = null;
            }
        }

        public string LoadData(MapData temp, Action<string> print = null)
        {
            try
            {
                if (temp.Info == null) print?.Invoke("地图信息为null");
                if (temp.SpawItems == null) print?.Invoke("生成物品为null");
                if (temp.Teams == null) print?.Invoke("队伍信息为null");

                print?.Invoke("检查数据");
                DataCheck.Repair(temp);
                DataCheck.CheckMapData(temp);

                Data = temp;
                print?.Invoke("加载完成");

                Common.GameAction.mapData = Data;

                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string LoadData(Action<string> print = null)
        {
            try
            {
                print?.Invoke("加载数据");

                MapData temp = DataFileHelp.ReadData(DirMapData);
                
                return LoadData(temp, print);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string SaveData()
        {
            try
            {
                Directory.CreateDirectory(DirMapData);
                DataFileHelp.SaveData(DirMapData, Data);

                return null;
            }
            catch (Exception ex)
            {
                return $"保存失败:{ex.Message}";
            }
        }

        public string ResetData()
        {
            MapData temp = new MapData();
            temp.Info = new MapInfoData();
            temp.Info.rect = new Rectangle(Main.spawnTileX, Main.spawnTileY, 2, 2);

            return LoadData(temp, null);
        }
    }
}
