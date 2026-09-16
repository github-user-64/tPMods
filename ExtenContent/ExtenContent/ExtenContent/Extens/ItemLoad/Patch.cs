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
            if (EItem is UnloadItem != true) return;

            toolTipLine[numLines] = $"卸载物品{ExtenManag.GetUnloadItemKey(item.Name)}";
            numLines++;
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

        internal static void ItemCheck_ShootPostfix(Player __instance, Item sItem, int weaponDamage, bool withAudioVisualFeedback)
        {
            ExtenManag.GetExtenItem(sItem.type)?.Shoot(__instance, sItem, weaponDamage, withAudioVisualFeedback);
        }

        internal static void ApplyItemAnimationPostfix(Player This, Item item)
        {
            ExtenManag.GetExtenItem(item.type)?.ApplyItemAnimationPostfix(This, item);
        }
    }
}
