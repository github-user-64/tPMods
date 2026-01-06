using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using tContentPatch;
using Terraria;
using Terraria.ID;

namespace ModTool.Common
{
    /// <summary>
    /// 修改商店
    /// </summary>
    public static class ModifyShop
    {
        /// <summary/>
        public struct ItemData
        {
            /// <summary/>
            public int type;
            /// <summary/>
            public int stack;
            /// <summary/>
            public byte prefix;
            /// <summary/>
            public int value;
            /// <summary/>
            public byte buyOnce;

            /// <summary/>
            public void Copy(Item item = null)
            {
                if (item == null)
                {
                    type = 0;
                    stack = 0;
                    prefix = 0;
                    value = 0;
                    buyOnce = 0;
                    return;
                }

                type = item.type;
                stack = item.stack;
                prefix = item.prefix;
                value = item.value;
                buyOnce = Convert.ToByte(item.buyOnce);//一次性?
            }

            /// <summary/>
            public void Paste(ref Item item)
            {
                if (item == null) item = new Item();
                item.SetDefaults(type);
                item.stack = stack;
                item.prefix = prefix;
                item.value = value;
                item.buyOnce = Convert.ToBoolean(buyOnce);
            }
        }

        /// <summary>
        /// <paramref name="npc"/>是玩家正在对话的npc(会为<see langword="null"/>)
        /// </summary>
        /// <returns>如果有进行修改就返回<see langword="true"/></returns>
        public delegate bool Modify(ItemData[] items, NPC npc, Player player);
        private static readonly List<Modify> setupShop = new List<Modify>();
        /// <summary>
        /// npc是玩家正在对话的npc(会为<see langword="null"/>)
        /// </summary>
        /// <returns>如果有进行修改就返回<see langword="true"/></returns>
        public static event Modify SetupShop
        {
            add
            {
                if (value == null) return;
                setupShop.Add(value);
            }
            remove => setupShop.Remove(value);
        }

        private static ItemData[] Update(Player player, Chest shop = null)
        {
            NPC npc = null;
            if (Main.npc.IndexInRange(player.talkNPC)) npc = Main.npc[player.talkNPC];

            bool hasModify = false;
            ItemData[] data = ShopToItemDatas(shop);

            setupShop.ForEach(i =>
            {
                try
                {
                    hasModify |= i(data, npc, player);
                }
                catch { }
            });

            return hasModify ? data : null;
        }

        private class PatcSetupShop : PatchChest
        {
            public override void SetupShop(Chest This, int type)//单人或客户端每次打开商店都会调用
            {
                if (Main.netMode != 0 && Main.netMode != 1) return;//没必要加

                ItemData[] data = Update(Main.LocalPlayer, This);
                if (data == null) return;

                for (int i = 0; i < Chest.maxItems; ++i)
                {
                    data[i].Paste(ref This.item[i]);
                    //This.item[i].isAShopItem = true;
                }
            }
        }

        private class PatcServer : PatchMain
        {
            private class PatchMsg : PatchMessageBuffer
            {
                public override void GetDataPrefix(MessageBuffer This, int start, int length, int messageType)
                {
                    if (messageType != MessageID.SyncTalkNPC) return;
                    if (Main.netMode != 2) return;

                    if (updatas.Count > 60) return;

                    _ = This.reader.ReadByte();
                    int whoAmI = This.whoAmI;
                    int npcIndex = This.reader.ReadInt16();

                    if (Main.npc.IndexInRange(npcIndex) != true) return;
                    if (updatas.Contains(whoAmI)) return;
                    //玩家和npc对话时
                    updatas.Add(whoAmI);
                }
            }

            private class SyncData
            {
                public int whoAmI;
                public ItemData[] data;

                public SyncData(int whoAmI, ItemData[] data)
                {
                    this.whoAmI = whoAmI;
                    this.data = data;
                }
            }
            private static List<int> updatas = new List<int>();
            private static List<SyncData> datas = new List<SyncData>();
            private static int cd = 60;
            private static int index = -1;

            public override void UpdatePrefix(GameTime gameTime)
            {
                if (Main.netMode != 2) return;
                if (updatas.Count < 1)//没有需要同步的玩家
                {
                    datas.Clear();
                    return;
                }

                //直接获取商店数据, 需要同步就发送就行了
                //弄这么多只是为了防止有一堆玩家打开商店但只有1个玩家的商店需要同步, 导致同步的间隔变长
                //但基本用不上
                //我真的多此一举了吗?

                if (datas.Count < 1)//没有在发送的数据
                {
                    updatas.RemoveAll(i =>
                    {
                        Player player = Main.player[i];
                        if (player?.active != true) return true;//删除
                        if (Main.npc.IndexInRange(player.talkNPC) != true) return true;//删除没和npc对话玩家

                        ItemData[] data = Update(player, GetNPCChest(player.talkNPC));
                        if (data != null) datas.Add(new SyncData(i, data));//有数据需要同步

                        return false;
                    });

                    cd = datas.Count < 1 ? 30 : 30 / datas.Count;
                    if (cd < 1) cd = 1;
                    index = 0;
                }

                if (datas.Count < 1) return;//没有需要发送的数据
                if (Main.GameUpdateCount % cd != 0) return;

                SyncShopToPlay(datas[index].data, datas[index].whoAmI);
                if (++index >= datas.Count) datas.Clear();//发送到最后一个数据时清空
            }
        }

        /// <summary>
        /// 商店物品转数据列表, <paramref name="shop"/>为<see langword="null"/>返回空数组
        /// </summary>
        public static ItemData[] ShopToItemDatas(Chest shop = null)
        {
            ItemData[] items = new ItemData[Chest.maxItems];
            if (shop == null) return items;
            if (shop.item == null) return items;

            for (int i = 0; i < shop.item.Length; i++)
            {
                items[i].Copy(shop.item[i]);
            }

            return items;
        }

        /// <summary>
        /// 同步商店物品到玩家
        /// </summary>
        public static void SyncShopToPlay(ItemData[] shop, int whoAmI)
        {
            if (Main.player.IndexInRange(whoAmI) != true) return;
            if (Main.player[whoAmI]?.active != true) return;

            ContentPatch.PrintTry(Main.player[whoAmI].name);//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            for (int i = 0; i < shop.Length; i++)
            {
                ItemData item = shop[i];

                NetMessage.TrySendData(MessageID.ShopOverride, whoAmI, -1, null,
                    i,
                    item.type,
                    item.stack,
                    item.prefix,
                    item.value,
                    item.buyOnce);
            }
        }

        /// <summary>
        /// 获取索引处npc的商店, 不会判断npc是否活动, 失败返回<see langword="null"/>
        /// <para/>为什么会变成这样呢
        /// </summary>
        public static Chest GetNPCChest(int npcIndex)
        {
            if (Main.npc.IndexInRange(npcIndex) != true) return null;

            NPC npc = Main.npc[npcIndex];
            if (npc == null) return null;

            int index = -1;

            if (npc.type == 17)
            {
                index = 1;
            }
            else if (npc.type == 19)
            {
                index = 2;
            }
            else if (npc.type == 124)
            {
                index = 8;
            }
            else if (npc.type == 142)
            {
                index = 9;
            }
            else if (npc.type == 20)
            {
                index = 3;
            }
            else if (npc.type == 38)
            {
                index = 4;
            }
            else if (npc.type == 54)
            {
                index = 5;
            }
            else if (npc.type == 107)
            {
                index = 6;
            }
            else if (npc.type == 108)
            {
                index = 7;
            }
            else if (npc.type == 160)
            {
                index = 10;
            }
            else if (npc.type == 178)
            {
                index = 11;
            }
            else if (npc.type == 207)
            {
                index = 12;
            }
            else if (npc.type == 208)
            {
                index = 13;
            }
            else if (npc.type == 209)
            {
                index = 14;
            }
            else if (npc.type == 227)
            {
                index = 15;
            }
            else if (npc.type == 228)
            {
                index = 16;
            }
            else if (npc.type == 229)
            {
                index = 17;
            }
            else if (npc.type == 353)
            {
                index = 18;
            }
            else if (npc.type == 368)
            {
                index = 19;
            }
            else if (npc.type == 453)
            {
                index = 20;
            }
            else if (npc.type == 550)
            {
                index = 21;
            }
            else if (npc.type == 588)
            {
                index = 22;
            }
            else if (npc.type == 633)
            {
                index = 23;
            }
            else if (npc.type == 663)
            {
                index = 24;
            }
            else if (npc.type == 227)
            {
                index = 25;
            }

            if (index < 1) return null;
            if (index >= Main.instance.shop.Length) return null;

            return Main.instance.shop[index];
        }
    }
}
