using Terraria;

namespace ExtenContent.Extens
{
    public static partial class EquipLoader
    {
        internal static void ApplyEquipFunctionalPostfix(Player player, int itemSlot, Item currentItem)
        {
            GetEquip(EquipType.Head, currentItem.headSlot)?.ApplyEquipFunctionalPostfix(player, itemSlot, currentItem);
            GetEquip(EquipType.Body, currentItem.bodySlot)?.ApplyEquipFunctionalPostfix(player, itemSlot, currentItem);
            GetEquip(EquipType.Legs, currentItem.legSlot)?.ApplyEquipFunctionalPostfix(player, itemSlot, currentItem);
            GetEquip(EquipType.Wings, currentItem.wingSlot)?.ApplyEquipFunctionalPostfix(player, itemSlot, currentItem);
        }

        internal static void ApplyEquipVanityPostfix(Player player, int itemSlot, Item currentItem)
        {
            GetEquip(EquipType.Head, currentItem.headSlot)?.ApplyEquipVanityPostfix(player, itemSlot, currentItem);
            GetEquip(EquipType.Body, currentItem.bodySlot)?.ApplyEquipVanityPostfix(player, itemSlot, currentItem);
            GetEquip(EquipType.Legs, currentItem.legSlot)?.ApplyEquipVanityPostfix(player, itemSlot, currentItem);
            GetEquip(EquipType.Wings, currentItem.wingSlot)?.ApplyEquipVanityPostfix(player, itemSlot, currentItem);
        }
    }
}
