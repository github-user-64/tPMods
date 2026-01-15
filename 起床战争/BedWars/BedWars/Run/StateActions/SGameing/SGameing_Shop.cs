using BedWars.BedWarsData;
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
            public readonly ShopData shop = null;

            public ShopNPC(MapInfoData data, ShopData shop)
            {
                this.shop = shop;

                pos = data.Pos.ToWorldCoordinates(0, 0) + shop.pos.ToWorldCoordinates(0, 0);
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
                NetMessage.TrySendData(MessageID.SyncNPC, number: npc.whoAmI);
                npc = null;
            }

            public void SpawNPC()
            {
                DelNPC();

                int index = NPC.NewNPC(null, (int)pos.X, (int)pos.Y, NPCID.Clothier);

                npc = Main.npc[index];
                npc.position = pos;
                npc.townNpcVariationIndex = 1;//设为1就是微光变体了, 鬼知道什么原理, 看不懂
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

                shopNPC.SpawNPC();
            }
        }

        public override bool ModifyShop(ModifyShop.ItemData[] items, NPC npc, Player player)
        {
            ShopData shop = shopNPC.FirstOrDefault(i => i.npc == npc).shop;
            if (shop == null) return false;

            int len = Math.Min(items.Length, shop.item.Count);//没必要, 长度正常都一样

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
