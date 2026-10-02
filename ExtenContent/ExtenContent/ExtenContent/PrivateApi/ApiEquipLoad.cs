using ExtenContent.Extens;
using Terraria;

namespace ExtenContent.PrivateApi
{
    /// <summary/>
    public class ApiEquipLoad
    {
        internal ApiEquipLoad() { }

        /// <summary/>
        public void ApplyEquipFunctionalPostfix(Player player, int itemSlot, Item currentItem)
        {
            EquipLoad.ApplyEquipFunctionalPostfix(player, itemSlot, currentItem);
        }

        /// <summary/>
        public void ApplyEquipVanityPostfix(Player player, int itemSlot, Item currentItem)
        {
            EquipLoad.ApplyEquipVanityPostfix(player, itemSlot, currentItem);
        }
    }
}
