using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;

namespace BedWars.BedWarsData
{
    public class SpawItemData
    {
        [Newtonsoft.Json.JsonIgnore]
        public MapData mapData = null;
        public string name = null;
        /// <summary>
        /// 在世界里的位置
        /// </summary>
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
                type = Utils.Utils.GetRandItemID(types);
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

            Item.NewItem(null, pos.X, pos.Y, 0, 0, type, stack);
        }
    }
}
