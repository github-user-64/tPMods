using System;
using System.Collections.Generic;
using Terraria;

namespace BedWars.BedWarsData
{
    public class ChestData : ICheck
    {
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

        public void Copy(MapData mapData, Chest chest)
        {
            name = chest.name;
            x = chest.x - mapData.Info.X;
            y = chest.y - mapData.Info.Y;

            for (int i = 0; i < Chest.maxItems; ++i)
            {
                item[i].Copy(chest.item[i]);
            }
        }

        /// <summary>
        /// 创建箱子
        /// </summary>
        public void Paste(MapData mapData)
        {
            if (mapData.InMapRelative(x, y) == false) return;

            int tilex = mapData.Info.X + x;
            int tiley = mapData.Info.Y + y;

            int index = Chest.CreateChest(tilex, tiley);
            if (index < 0) return;

            Chest chest = Main.chest[index];

            if (chest == null)//如果是在单人就不用考虑这个
            {
                chest = new Chest();
                Main.chest[index] = chest;
                chest.x = tilex;
                chest.y = tiley;
                for (int i = 0; i < chest.item.Length; ++i)
                {
                    chest.item[i] = new Item();
                }
            }

            chest.name = name;

            for (int i = 0; i < Chest.maxItems; ++i)
            {
                item[i].Paste(chest.item[i]);
            }
        }
    }
}
