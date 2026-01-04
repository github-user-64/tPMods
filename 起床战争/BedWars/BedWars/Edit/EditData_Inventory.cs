using BedWars.BedWarsData;
using System;
using Terraria;

namespace BedWars.Edit
{
    public partial class EditData
    {
        public string InventoryCheck(Player player)
        {
            if (player == null) return "玩家为null";
            if (player.inventory?.Length == InventoryData.inventoryLen != true) return "物品栏数据异常";
            if (player.armor?.Length == InventoryData.armorLen != true) return "装备栏数据异常";
            if (player.dye?.Length == InventoryData.dyeLen != true) return "染料栏数据异常";
            return null;
        }

        public string InventoryCopy(Player player)
        {
            if (Data == null) return "地图数据为null";
            if (InventoryCheck(player) is string ex) return ex;

            DataInventory.Copy(player);

            return null;
        }

        private void fori(int len, Action<int> action)
        {
            for (int i = 0; i < len; ++i) action(i);
        }

        private bool oldItemsInit = false;
        private ItemData[] oldInventory = new ItemData[InventoryData.inventoryLen];
        private ItemData[] oldArmor = new ItemData[InventoryData.armorLen];
        private ItemData[] oldDye = new ItemData[InventoryData.dyeLen];
        public string InventoryPaste(Player player)
        {
            if (Data == null) return "地图数据为null";
            if (InventoryCheck(player) is string ex) return ex;

            if (oldItemsInit == false)
            {
                fori(oldInventory.Length, i => oldInventory[i] = new ItemData());
                fori(oldArmor.Length, i => oldArmor[i] = new ItemData());
                fori(oldDye.Length, i => oldDye[i] = new ItemData());
                oldItemsInit = true;
            }

            fori(oldInventory.Length, i => oldInventory[i].Copy(player.inventory[i]));
            fori(oldArmor.Length, i => oldArmor[i].Copy(player.armor[i]));
            fori(oldDye.Length, i => oldDye[i].Copy(player.dye[i]));

            DataInventory.Paste(player);

            return null;
        }

        public string InventoryReset(Player player)
        {
            if (Data == null) return "地图数据为null";
            if (InventoryCheck(player) is string ex) return ex;
            if (oldItemsInit == false) return "没有上次物品数据";

            fori(oldInventory.Length, i => oldInventory[i].Paste(player.inventory[i]));
            fori(oldArmor.Length, i => oldArmor[i].Paste(player.armor[i]));
            fori(oldDye.Length, i => oldDye[i].Paste(player.dye[i]));

            return null;
        }
    }
}
