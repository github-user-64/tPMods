using System;
using System.Collections.Generic;
using Terraria;

namespace BedWars.BedWarsData
{
    public class ChestData : ICheck
    {
        [Newtonsoft.Json.JsonIgnore]
        public MapData mapData = null;
        /// <summary>
        /// <see cref="Chest.MaxNameLength"/>
        /// </summary>
        public string name = null;
        /// <summary>
        /// 箱子左上角x, 相对位置
        /// </summary>
        public int x;
        /// <summary>
        /// 箱子左上角y, 相对位置
        /// </summary>
        public int y;
        /// <summary>
        /// <see cref="Chest.maxItems"/>
        /// </summary>
        public List<ItemData> item = null;

        public void Check(MapData mapData)
        {
            if (mapData.InMapRelative(x, y) == false) throw new Exception("箱子超出地图");

            if (name?.Length > Chest.MaxNameLength)
            {
                name.Substring(0, Chest.MaxNameLength);
            }
        }

        public static void Copy(ChestData data, Chest chest)
        {
            data.name = chest.name;
            data.x = chest.x - data.mapData.Info.pos.X;
            data.y = chest.y - data.mapData.Info.pos.Y;

            if (data.item == null) return;
            for (int i = 0; i < Chest.maxItems; ++i)
            {
                ItemData.Copy(data.item[i], chest.item[i]);
            }
        }

        public static void Paste(ChestData data, Chest chest)
        {
            chest.name = data.name;
            chest.x = data.x + data.mapData.Info.pos.X;
            chest.y = data.y + data.mapData.Info.pos.Y;

            for (int i = 0; i < Chest.maxItems; ++i)
            {
                ItemData.Paste(data.item[i], chest.item[i]);
            }
        }
    }
}
