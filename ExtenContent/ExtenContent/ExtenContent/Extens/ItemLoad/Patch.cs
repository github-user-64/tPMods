using Microsoft.Xna.Framework;
using Terraria;
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

        internal static void ItemCheck_ShootPostfix(Player player, Item sItem, int weaponDamage, bool withAudioVisualFeedback)
        {
            ExtenManag.GetExtenItem(sItem.type)?.Shoot(player, sItem, weaponDamage, withAudioVisualFeedback);
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
    }
}
