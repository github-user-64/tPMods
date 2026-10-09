using Terraria;
using Terraria.GameContent.Items;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ExtenContent.Extens
{
    public static partial class ItemLoader
    {
        /// <summary>
        /// <see cref="ThisMod.TerrariaVersionCheck"/>
        /// </summary>
        internal static void SetDefaults(Item item, int Type, ItemVariant variant = null)
        {
            if (TypeInRange(Type) != true) return;

            item.ResetStats(Type);

            if (variant == null)
            {
                variant = ItemVariants.SelectVariant(Type);
            }
            else if (!ItemVariants.HasVariant(Type, variant))
            {
                variant = null;
            }
            _itemVariant.SetValue(item, variant);

            item.material = ItemID.Sets.IsAMaterial[item.type];

            ExtenItem eitem = GetItem(item.type);
            eitem.SetDefault(item);

            item.dye = (byte)GameShaders.Armor.GetShaderIdFromItemId(item.type);
            if (item.hairDye != 0)
            {
                item.hairDye = GameShaders.Hair.GetShaderIdFromItemId(item.type);
            }

            if (item.bait > 0)
            {
                if (item.bait >= 50)
                {
                    item.rare = 3;
                }
                else if (item.bait >= 30)
                {
                    item.rare = 2;
                }
                else if (item.bait >= 15)
                {
                    item.rare = 1;
                }
            }

            if (Main.projHook[item.shoot])
            {
                item.useStyle = 0;
                item.useTime = 0;
                item.useAnimation = 0;
            }

            if (ItemID.Sets.IsDrill[item.type] || ItemID.Sets.IsChainsaw[item.type])
            {
                item.useTime = (int)(item.useTime * 0.6);
                if (item.useTime < 1)
                {
                    item.useTime = 1;
                }
                item.useAnimation = (int)(item.useAnimation * 0.6);
                if (item.useAnimation < 1)
                {
                    item.useAnimation = 1;
                }
                item.tileBoost--;
            }

            if (ItemID.Sets.IsFood[item.type])
            {
                item.holdStyle = 1;
            }

            item.RebuildTooltip();

            if (ItemID.Sets.Deprecated[item.type])
            {
                item.TurnToAir();
            }
        }
    }
}
