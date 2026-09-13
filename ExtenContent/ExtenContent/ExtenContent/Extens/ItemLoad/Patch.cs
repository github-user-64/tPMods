using Terraria;
using Terraria.GameContent.Items;
using Terraria.ID;

namespace ExtenContent.Extens
{
    public static partial class ItemLoad
    {
        public static void SetDefaults(Item item, int Type, ItemVariant variant = null)
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

        public static void ItemCheck_ShootPostfix(Player __instance, Item sItem, int weaponDamage, bool withAudioVisualFeedback)
        {
            int Type = sItem.type;

            if (TypeInRange(Type) != true) return;

            ExtenItem eitem = items[Type - ItemID.Count];
            eitem.Shoot(__instance, sItem, weaponDamage, withAudioVisualFeedback);
        }
    }
}
