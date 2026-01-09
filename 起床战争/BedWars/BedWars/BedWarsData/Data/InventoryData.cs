using System;
using System.Collections.Generic;
using Terraria;

namespace BedWars.BedWarsData
{
    public class InventoryData : ICheck
    {
        private static readonly Player _p = new Player();
        public static readonly int inventoryLen = _p.inventory.Length;
        public static readonly int armorLen = _p.armor.Length;
        public static readonly int dyeLen = _p.dye.Length;
        public static readonly int miscEquipLen = _p.miscEquips.Length;
        public static readonly int miscDyeLen = _p.miscDyes.Length;

        public List<ItemData> inventory = null;//物品栏
        public List<ItemData> armor = null;//装备
        public List<ItemData> dye = null;//染料

        public void Check(MapData mapData)
        {

        }

        private static void fori(int len, Action<int> action)
        {
            for (int i = 0; i < len; ++i) action(i);
        }

        private static (int len0, int len1, int len2) getLen(Player player)
        {
            (int len0, int len1, int len2) lens = (0, 0, 0);
            if (player == null) return lens;

            lens.len0 = Math.Min(inventoryLen, player.inventory?.Length ?? 0);
            lens.len1 = Math.Min(armorLen, player.armor?.Length ?? 0);
            lens.len2 = Math.Min(dyeLen, player.dye?.Length ?? 0);

            return lens;
        }

        public void Copy(Player player)
        {
            (int len0, int len1, int len2) = getLen(player);

            fori(len0, i => inventory[i].Copy(player.inventory[i]));
            fori(len1, i => armor[i].Copy(player.armor[i]));
            fori(len2, i => dye[i].Copy(player.dye[i]));
        }

        public void Paste(Player player)
        {
            (int len0, int len1, int len2) = getLen(player);

            fori(len0, i => inventory[i].Paste(player.inventory[i]));
            fori(len1, i => armor[i].Paste(player.armor[i]));
            fori(len2, i => dye[i].Paste(player.dye[i]));
        }
    }
}
