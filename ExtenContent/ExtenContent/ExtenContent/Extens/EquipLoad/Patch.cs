using Terraria;

namespace ExtenContent.Extens
{
    public static partial class EquipLoad
    {
        internal static void ApplyEquipFunctionalPostfix(Player player, int itemSlot, Item currentItem)
        {
            GetEquipWing(currentItem.wingSlot)?.ApplyEquipFunctionalPostfix(player, itemSlot, currentItem);
        }

        internal static void ApplyEquipVanityPostfix(Player player, int itemSlot, Item currentItem)
        {
            GetEquipWing(currentItem.wingSlot)?.ApplyEquipVanityPostfix(player, itemSlot, currentItem);
        }
    }
}
