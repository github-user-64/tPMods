using Microsoft.Xna.Framework;
using ModTool.Common;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;

namespace BedWars.Run.StateActions
{
    internal partial class SGameing
    {
        private class ShopNPC
        {
            public NPC npc = null;
            public Vector2 pos;
            public readonly BedWarsData.ShopData shop = null;

            public ShopNPC(NPC npc, BedWarsData.ShopData shop)
            {
                this.npc = npc;
                this.shop = shop;

                pos = shop.pos.ToWorldCoordinates(0, 0);
            }

            public bool NPCActive()
            {
                if (npc == null) return false;
                if (npc.active == false) return false;
                if (Main.npc.IndexInRange(npc.whoAmI) != true) return false;
                if (Main.npc[npc.whoAmI] != npc) return false;
                return true;
            }

            public void DelNPC()
            {
                if (npc == null) return;

                npc.active = false;
                npc = null;
            }
        }

        private void UpdateShop()
        {
            for (int i = 0; i < shopNPC.Count; ++i)
            {
                ShopNPC shopNPC = this.shopNPC[i];

                if (shopNPC.NPCActive() == true)//如果npc还在
                {
                    float d = shopNPC.npc.position.Distance(shopNPC.pos);
                    if (d > 16 * 10) shopNPC.npc.position = shopNPC.pos;

                    continue;
                }

                shopNPC.DelNPC();//删除npc

                //生成npc
                int index = NPC.NewNPC(null, (int)shopNPC.pos.X, (int)shopNPC.pos.Y, NPCID.Clothier);

                shopNPC.npc = Main.npc[index];
                shopNPC.npc.position = shopNPC.pos;
            }
        }

        public override bool ModifyShop(ModifyShop.ItemData[] items, NPC npc, Player player)
        {
            BedWarsData.ShopData shop = shopNPC.FirstOrDefault(i => i.npc == npc).shop;
            if (shop == null) return false;

            int len = Math.Min(items.Length, shop.item.Count);

            for (int i = 0; i < len; ++i)
            {
                ModifyShop.ItemData data = shop.item[i];

                items[i].type = data.type;
                items[i].prefix = data.prefix;
                items[i].value = data.value;
                items[i].stack = data.stack;
                items[i].buyOnce = data.buyOnce;
            }

            return true;
        }
    }
}
