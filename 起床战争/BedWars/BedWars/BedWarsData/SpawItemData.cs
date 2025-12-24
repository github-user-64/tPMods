using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;

namespace BedWars.BedWarsData
{
    public class SpawItemData
    {
        public string name = null;
        /// <summary>
        /// 生成位置, 相对于<see cref="off"/>
        /// </summary>
        public Vector2 pos = Vector2.Zero;
        /// <summary>
        /// 加载时由<see cref="Edit.MapInfo.pos"/>赋值
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public Vector2 off = Vector2.Zero;
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
            Vector2 pos = this.pos + off;
            if (Utils.Utils.InWorld(pos, 16) == false) return;

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

            Item.NewItem(null, pos, Vector2.Zero, type, stack);
        }
    }
}
