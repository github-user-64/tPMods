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
        private static void ContentInit()
        {
            LocalizedText[] _itemNameCache = (LocalizedText[])__itemNameCache.GetValue(null);
            ItemTooltip[] _itemTooltipCache = (ItemTooltip[])__itemTooltipCache.GetValue(null);

            LoaderUtils.ResetStaticMembers(typeof(ItemID));
            LoaderUtils.ResetStaticMembers(typeof(ItemID.Sets));
            LoaderUtils.ResetStaticMembers(typeof(AmmoID.Sets));
            LoaderUtils.ResetStaticMembers(typeof(PrefixLegacy.ItemSets));

            Array.Resize(ref TextureAssets.Item, ItemCount);
            Array.Resize(ref TextureAssets.ItemFlame, ItemCount);

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

        private static void ContentLoad()
        {
            LocalizedText[] _itemNameCache = (LocalizedText[])__itemNameCache.GetValue(null);
            ItemTooltip[] _itemTooltipCache = (ItemTooltip[])__itemTooltipCache.GetValue(null);

            foreach (ExtenItem i in items)
            {
                i.Item.SetDefaults(i.Type);

                _itemNameCache[i.Type] = i.DisplayName;
                _itemTooltipCache[i.Type] = ItemTooltip.FromLanguageKey(i.Tooltip.Key);

                ContentSamples.ItemsByType[i.Type] = i.Item;
                ContentSamples.ItemsByType[i.Type].RebuildTooltip();

                ItemID.Search.Add(i.FullName, i.Type);

                if (Main.dedServ) continue;

                TextureAssets.Item[i.Type] = i.Asset.Request<Texture2D>(i.Texture);
            }

            __itemNameCache.SetValue(null, _itemNameCache);
            __itemTooltipCache.SetValue(null, _itemTooltipCache);
        }
    }
}
