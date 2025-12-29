using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;

namespace BedWars.BedWarsData
{
    public class SpawItemData : ICheck
    {
        [Newtonsoft.Json.JsonIgnore]
        public MapData mapData = null;
        public string name = null;
        public Point pos = Point.Zero;
        public List<int> types = new List<int>();
        /// <summary>
        /// 如果为<see langword="true"/>则从除了<see cref="types"/>的所有中选择
        /// </summary>
        public bool exclude = false;
        public int maxStack = 1;
        public int cd = 30;

        public void Spaw()
        {
            if (maxStack < 1) return;
            if (mapData.InMap(pos) == false) return;

            int type = 0;

            if (exclude)
            {
                type = Common.Utils.GetRandItemID(types);
            }
            else if (types?.Count > 0)
            {
                type = types[ModTool.Utils.Utils.GetRand(0, types.Count)];
            }

            if (type == 0) return;

            Item item = new Item();
            item.SetDefaults(type);
            if (item.maxStack < 1) return;

            int stack = ModTool.Utils.Utils.GetRand(1, maxStack + 1);
            if (stack > item.maxStack) stack = item.maxStack;
            else if (stack < 1) stack = 1;

            Item.NewItem(null, pos.ToWorldCoordinates(), Vector2.Zero, type, stack);
        }

        public void Check(MapData mapData)
        {
            if (mapData.InMap(pos) == false) throw new Exception("生成物品超出地图");
        }

        public static void SetMapData(MapData mapData)
        {
            mapData.SpawItems.ForEach(i => i.mapData = mapData);
        }
    }
}
