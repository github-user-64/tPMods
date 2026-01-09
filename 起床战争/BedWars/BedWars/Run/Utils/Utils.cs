using BedWars.BedWarsData;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace BedWars.Run
{
    public static class Utils
    {
        public static void SetItemsSync(Player player, InventoryData data = null)
        {
            SetItemsSync(player, player.inventory, PlayerItemSlotID.Inventory0, data?.inventory);//物品栏
            //PlayerItemSlotID.InventoryMouseItem. 不需要, 包含在物品栏里了
            SetItemsSync(player, player.trashItem, PlayerItemSlotID.TrashItem, null);//垃圾桶
            SetItemsSync(player, player.armor, PlayerItemSlotID.Armor0, data?.armor);//当前装备
            SetItemsSync(player, player.dye, PlayerItemSlotID.Dye0, data?.dye);//当前染料
            SetItemsSync(player, player.miscEquips, PlayerItemSlotID.Misc0, null);//杂项装备
            SetItemsSync(player, player.miscDyes, PlayerItemSlotID.MiscDye0, null);//杂项染料
            SetItemsSync(player, player.Loadouts[0].Armor, PlayerItemSlotID.Loadout1_Armor_0, null);
            SetItemsSync(player, player.Loadouts[1].Armor, PlayerItemSlotID.Loadout2_Armor_0, null);
            SetItemsSync(player, player.Loadouts[2].Armor, PlayerItemSlotID.Loadout3_Armor_0, null);
            SetItemsSync(player, player.Loadouts[0].Dye, PlayerItemSlotID.Loadout1_Dye_0, null);
            SetItemsSync(player, player.Loadouts[1].Dye, PlayerItemSlotID.Loadout2_Dye_0, null);
            SetItemsSync(player, player.Loadouts[2].Dye, PlayerItemSlotID.Loadout3_Dye_0, null);
        }

        /// <summary>
        /// <paramref name="data"/>为空代表空物品
        /// </summary>
        public static void SetItemsSync(Player player, Item item, int slot, ItemData data = null)
        {
            //不许要同步物品
            if (NeedSyncItem(item, data) == false) return;

            if (data == null) item.SetDefaults(ItemID.None);
            else data.Paste(item);

            SyncItem(player, item, slot);
        }

        public static void SetItemsSync(Player player, Item[] items, int slot, List<ItemData> datas = null)
        {
            if (datas == null)
            {
                for (int i = 0; i < items.Length; ++i)
                {
                    SetItemsSync(player, items[i], slot + i, null);
                }
                return;
            }

            int len = Math.Min(items.Length, datas.Count);

            for (int i = 0; i < len; ++i)
            {
                SetItemsSync(player, items[i], slot + i, datas[i]);
            }
        }

        public static bool NeedSyncItem(Item item = null, ItemData data = null)//需要同步物品
        {
            bool isnull1 = IsEmptyItem(item);
            bool isnull2 = IsEmptyItem(data);

            if (isnull1 && isnull2) return false;//两个都是空物品

            if (isnull1 || isnull2) return true;//其中一个是空物品
            if (item.type != data.type) return true;
            if (item.stack != data.stack) return true;
            if (item.prefix != data.prefix) return true;

            return false;
        }

        public static bool IsEmptyItem(Item item = null)
        {
            if (item == null) return true;
            if (item.type == ItemID.None) return true;
            return item.stack < 1;
        }

        public static bool IsEmptyItem(ItemData item = null)
        {
            if (item == null) return true;
            if (item.type == ItemID.None) return true;
            return item.stack < 1;
        }

        public static void SyncItem(Player player, Item item, int slot)
        {
            NetMessage.TrySendData(MessageID.SyncEquipment, -1, -1, null,
                player.whoAmI, slot, item.prefix);
        }
    }
}
