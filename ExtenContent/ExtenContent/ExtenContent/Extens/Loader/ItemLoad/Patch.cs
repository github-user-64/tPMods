using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Items;
using Terraria.ID;

namespace ExtenContent.Extens
{
    public static partial class ItemLoad
    {
        internal static void MouseText_DrawItemTooltip_GetLinesInfoPostfix(Item item, ref int yoyoLogo, ref float oldKB, ref int numLines, ref string[] toolTipLine, ref Color[] lineColors)
        {
            ExtenItem EItem = ExtenManag.GetExtenItem(item.type);
            if (EItem == null) return;

            if (EItem is UnloadItem)
            {
                toolTipLine[numLines] = $"卸载物品{ExtenManag.GetUnloadItemKey(item.Name)}";
                numLines++;

                return;
            }

            EItem.ModifyTooltips(item, ref yoyoLogo, ref oldKB, ref numLines, ref toolTipLine, ref lineColors);
        }

        internal static void SetDefaults(Item item, int Type, ItemVariant variant = null)
        {
            if (TypeInRange(Type) != true) return;

            item.ResetStats(Type);

            //if (variant == null)
            //{
            //    variant = ItemVariants.SelectVariant(Type);
            //}
            //else if (!ItemVariants.HasVariant(Type, variant))
            //{
            //    variant = null;
            //}

            _itemVariant.SetValue(item, variant);

            ExtenItem eitem = items[Type - ItemID.Count];
            eitem.SetDefault(item);

            if (Main.projHook[item.shoot])
            {
                item.useStyle = 0;
                item.useTime = 0;
                item.useAnimation = 0;
            }

            item.RebuildTooltip();
        }

        internal static bool ItemCheck_Shoot(Player player, ExtenItem ei, Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return ei.Shoot(player, item, source, position, velocity, type, damage, knockback);
        }

        internal static void ApplyItemAnimationPostfix(Player player, Item item)
        {
            ExtenManag.GetExtenItem(item.type)?.ApplyItemAnimationPostfix(player, item);
        }

        internal static void ApplyEquipFunctionalPostfix(Player player, int itemSlot, Item currentItem)
        {
            ExtenManag.GetExtenItem(currentItem.type)?.ApplyEquipFunctionalPostfix(player, itemSlot, currentItem);
        }

        internal static void ApplyEquipVanityPostfix(Player player, int itemSlot, Item currentItem)
        {
            ExtenManag.GetExtenItem(currentItem.type)?.ApplyEquipVanityPostfix(player, itemSlot, currentItem);
        }

        internal static bool AltFunctionUse(Player player, Item item)
        {
            return ExtenManag.GetExtenItem(item.type)?.AltFunctionUse(player, item) == true;
        }

        internal static void CanUseItem(ref bool result, Player player, Item item)
        {
            ExtenItem ei = ExtenManag.GetExtenItem(item.type);
            if (ei != null) result = ei.CanUseItem(player, item);
        }

        internal static void OnHitNPC(Player player, Item item, Rectangle itemRectangle, int originalDamage, float knockBack, NPC npc)
        {
            ExtenManag.GetExtenItem(item.type)?.OnHitNPC(player, item, itemRectangle, originalDamage, knockBack, npc);
        }
    }
}
