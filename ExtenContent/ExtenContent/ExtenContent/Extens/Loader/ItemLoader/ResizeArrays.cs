using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Prefixes;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace ExtenContent.Extens
{
    public static partial class ItemLoader
    {
        private static void ResizeArrays()
        {
            LocalizedText[] _itemNameCache = (LocalizedText[])__itemNameCache.GetValue(null);
            ItemTooltip[] _itemTooltipCache = (ItemTooltip[])__itemTooltipCache.GetValue(null);

            Array.Resize(ref TextureAssets.Item, ItemCount);
            Array.Resize(ref TextureAssets.ItemFlame, ItemCount);

            LoaderUtils.ResetStaticMembers(typeof(ItemID.Sets));
            LoaderUtils.ResetStaticMembers(typeof(AmmoID.Sets));
            LoaderUtils.ResetStaticMembers(typeof(PrefixLegacy.ItemSets));

            Array.Resize(ref Item.cachedItemSpawnsByType, ItemCount);
            Array.Resize(ref Item.staff, ItemCount);
            Array.Resize(ref Item.claw, ItemCount);

            Array.Resize(ref _itemNameCache, ItemCount);
            Array.Resize(ref _itemTooltipCache, ItemCount);

            for (int i = ItemID.Count; i < ItemCount; i++)
            {
                _itemNameCache[i] = LocalizedText.Empty;
                _itemTooltipCache[i] = ItemTooltip.None;
                Item.cachedItemSpawnsByType[i] = -1;
            }

            List<int> itemAnimationsRegistered = Main.itemAnimationsRegistered;
            lock (itemAnimationsRegistered)
            {
                Array.Resize(ref Main.itemAnimations, ItemCount);
                Main.InitializeItemAnimations();
            }

            __itemNameCache.SetValue(null, _itemNameCache);
            __itemTooltipCache.SetValue(null, _itemTooltipCache);
        }

        private static void FinishSetup()
        {
            LocalizedText[] _itemNameCache = (LocalizedText[])__itemNameCache.GetValue(null);
            ItemTooltip[] _itemTooltipCache = (ItemTooltip[])__itemTooltipCache.GetValue(null);

            for (int i = 0; i < items.Count; ++i)
            {
                ExtenItem item = items[i];
                item.Item.SetDefaults(ItemID.Count + i);

                if (Main.dedServ != true)
                {
                    TextureAssets.Item[item.Type] = item.Asset.Request<Texture2D>(item.Texture);
                }

                _itemNameCache[item.Type] = item.DisplayName;
                _itemTooltipCache[item.Type] = ItemTooltip.FromLanguageKey(item.Tooltip.Key);

                ContentSamples.ItemsByType[item.Type] = item.Item;
                ContentSamples.ItemsByType[item.Type].RebuildTooltip();
            }

            __itemNameCache.SetValue(null, _itemNameCache);
            __itemTooltipCache.SetValue(null, _itemTooltipCache);
        }
    }
}
