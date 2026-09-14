using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Prefixes;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace ExtenContent.Extens
{
    public static partial class ItemLoad
    {
        private static void ResizeArrays()
        {
            LocalizedText[] _itemNameCache = (LocalizedText[])__itemNameCache.GetValue(null);
            ItemTooltip[] _itemTooltipCache = (ItemTooltip[])__itemTooltipCache.GetValue(null);

            Array.Resize(ref TextureAssets.Item, ItemCount);
            Array.Resize(ref TextureAssets.ItemFlame, ItemCount);

            ResetStaticMembers(typeof(ItemID.Sets));
            ResetStaticMembers(typeof(AmmoID.Sets));
            ResetStaticMembers(typeof(PrefixLegacy.ItemSets));

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

        private static void ResetStaticMembers(Type type)
        {
            ConstructorInfo init = type.TypeInitializer;
            if (init == null) return;

            init.Invoke(null, null);
        }
    }
}
