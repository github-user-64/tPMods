using Terraria;

namespace ExtenContent.Extens
{
    public static partial class EquipLoader
    {
        internal static void ApplyEquipFunctionalPostfix(Player player, int itemSlot, Item currentItem)
        {
            foreach (EquipType i in EquipTypes)
            {
                GetExtenEquip(i, GetItemSlot(i, currentItem))?.ApplyEquipFunctionalPostfix(player, itemSlot, currentItem);
            }
        }

        internal static void ApplyEquipVanityPostfix(Player player, int itemSlot, Item currentItem)
        {
            foreach (EquipType i in EquipTypes)
            {
                GetExtenEquip(i, GetItemSlot(i, currentItem))?.ApplyEquipVanityPostfix(player, itemSlot, currentItem);
            }
        }

        internal static void GrantArmorBenefitsPostfix(Player player, Item armorPiece)
        {
            foreach (EquipType i in EquipTypes)
            {
                GetExtenEquip(i, GetItemSlot(i, armorPiece))?.UpdateEquip(player, armorPiece);
            }
        }
    }
}
