using ExtenContent.Exten;
using Terraria;

namespace ExtenContent.PrivateApi
{
    /// <summary/>
    public class ApiEquipLoader
    {
        internal ApiEquipLoader() { }

        /// <summary/>
        public void ApplyEquipFunctionalPostfix(Player player, int itemSlot, Item currentItem)
        {
            EquipLoader.ApplyEquipFunctionalPostfix(player, itemSlot, currentItem);
        }

        /// <summary/>
        public void ApplyEquipVanityPostfix(Player player, int itemSlot, Item currentItem)
        {
            EquipLoader.ApplyEquipVanityPostfix(player, itemSlot, currentItem);
        }

        /// <summary/>
        public void GrantArmorBenefitsPostfix(Player player, Item armorPiece)
        {
            EquipLoader.GrantArmorBenefitsPostfix(player, armorPiece);
        }
    }
}
