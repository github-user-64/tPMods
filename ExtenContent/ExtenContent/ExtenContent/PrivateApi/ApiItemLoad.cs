using ExtenContent.Extens;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Items;
using Terraria.IO;

namespace ExtenContent.PrivateApi
{
    /// <summary/>
    public partial class ApiItemLoad
    {
        internal ApiItemLoad() { }

        /// <summary/>
        public void LoadPlayerPostfix(PlayerFileData result, string playerPath, bool cloudSave)
        {
            ItemLoad.LoadPlayerPostfix(result, playerPath, cloudSave);
        }

        /// <summary/>
        public void SavePlayerPrefix(PlayerFileData playerFile, bool skipMapSave)
        {
            ItemLoad.SavePlayerPrefix(playerFile, skipMapSave);
        }

        /// <summary/>
        public void MouseText_DrawItemTooltip_GetLinesInfoPostfix(Item item, ref int yoyoLogo, ref float oldKB, ref int numLines, ref string[] toolTipLine, ref Color[] lineColors)
        {
            ItemLoad.MouseText_DrawItemTooltip_GetLinesInfoPostfix(item, ref yoyoLogo, ref oldKB, ref numLines, ref toolTipLine, ref lineColors);
        }

        /// <summary/>
        public void SetDefaults(Item item, int Type, ItemVariant variant = null)
        {
            ItemLoad.SetDefaults(item, Type, variant);
        }

        /// <summary/>
        public bool ItemCheck_Shoot(Player player, ExtenItem ei, Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return ItemLoad.ItemCheck_Shoot(player, ei, item, source, position, velocity, type, damage, knockback);
        }

        /// <summary/>
        public void ApplyItemAnimationPostfix(Player player, Item item)
        {
            ItemLoad.ApplyItemAnimationPostfix(player, item);
        }

        /// <summary/>
        public void ApplyEquipFunctionalPostfix(Player player, int itemSlot, Item currentItem)
        {
            ItemLoad.ApplyEquipFunctionalPostfix(player, itemSlot, currentItem);
        }

        /// <summary/>
        public void ApplyEquipVanityPostfix(Player player, int itemSlot, Item currentItem)
        {
            ItemLoad.ApplyEquipVanityPostfix(player, itemSlot, currentItem);
        }

        /// <summary/>
        public bool AltFunctionUse(Player player, Item item)
        {
            return ItemLoad.AltFunctionUse(player, item);
        }

        /// <summary/>
        public void CanUseItem(ref bool result, Player player, Item item)
        {
            ItemLoad.CanUseItem(ref result, player, item);
        }

        /// <summary/>
        public void OnHitNPC(Player player, Item item, Rectangle itemRectangle, int originalDamage, float knockBack, NPC npc)
        {
            ItemLoad.OnHitNPC(player, item, itemRectangle, originalDamage, knockBack, npc);
        }
    }
}
