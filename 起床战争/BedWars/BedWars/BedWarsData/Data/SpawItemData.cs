using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace BedWars.BedWarsData
{
    public class SpawItemData : ICheck
    {
        public string name = null;
        /// <summary>
        /// 相对位置
        /// </summary>
        public Point pos = Point.Zero;
        public List<int> types = new List<int>();
        /// <summary>
        /// 如果为<see langword="true"/>则从除了<see cref="types"/>的所有中选择
        /// </summary>
        public bool exclude = false;
        public int maxStack = 1;
        public int cd = 30;

        public void Spaw(MapData mapData)
        {
            if (maxStack < 1) return;
            if (mapData.InMapRelative(pos) == false) return;

            int type = ItemID.None;

            if (exclude)
            {
                type = Common.Utils.GetRandItemID(types);
            }
            else if (types?.Count > ItemID.None)
            {
                type = types[ModTool.Utils.Utils.GetRand(0, types.Count)];
            }

            if (type == ItemID.None) return;

            Item item = new Item();
            item.SetDefaults(type);
            if (item.maxStack < 1) return;

            int stack = ModTool.Utils.Utils.GetRand(1, maxStack + 1);
            if (stack > item.maxStack) stack = item.maxStack;
            else if (stack < 1) stack = 1;

            Point p = new Point(mapData.Info.X + pos.X, mapData.Info.Y + pos.Y);

            NewItem(p.ToWorldCoordinates(), type, stack);
        }

        private void NewItem(Vector2 pos, int type, int stack)
        {
            foreach (Item i in Main.item)
            {
                if (i?.active != true) continue;
                if (i.type != type) continue;
                if (i.stack < 1) continue;

                float dis = i.Center.Distance(pos);
                if (dis > 16 * 8) continue;

                if (i.stack + stack > i.maxStack) continue;//如果不能堆叠

                i.stack += stack;
                i.Center = pos;
                i.velocity = Vector2.UnitY * -5;

                NetMessage.TrySendData(MessageID.SyncItem, number: i.whoAmI);
                return;
            }

            Item.NewItem(null, pos, Vector2.Zero, type, stack);
        }

        public void Check(MapData mapData)
        {
            if (mapData.InMapRelative(pos) == false) throw new Exception("生成物品超出地图");
        }
    }
}
