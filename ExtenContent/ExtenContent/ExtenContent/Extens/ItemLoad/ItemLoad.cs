using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace ExtenContent.Extens
{
    /// <summary/>
    public static partial class ItemLoad
    {
        private static readonly FieldInfo __itemNameCache = typeof(Lang).GetField("_itemNameCache", BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly FieldInfo __itemTooltipCache = typeof(Lang).GetField("_itemTooltipCache", BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly PropertyInfo _itemVariant = typeof(Item).GetProperty("Variant", BindingFlags.Public | BindingFlags.Instance);

        /// <summary>
        /// 物品数量, <see cref="ItemID.Count"/>的数量加上<see cref="ExtenItem"/>的数量
        /// </summary>
        public static int ItemCount { get; private set; } = ItemID.Count;
        private static readonly List<ExtenItem> items = new List<ExtenItem>();

        internal static void Load()
        {
            ResizeArrays();
            FinishSetup();
            BuildLookup();
        }

        internal static void Unload()
        {
            ItemCount = ItemID.Count;
            items.Clear();
        }

        internal static void Register(ExtenItem item)
        {
            items.Add(item);
            ++ItemCount;
        }

        /// <summary>
        /// 获取<see cref="Item.type"/>对应的<see cref="ExtenItem"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static ExtenItem GetItem(int type)
        {
            if (TypeInRange(type) != true) return null;

            return items[type - ItemID.Count];
        }

        /// <summary>
        /// <see cref="Item.type"/>是否是<see cref="ExtenItem"/>
        /// </summary>
        public static bool TypeInRange(int type)
        {
            return ItemID.Count <= type && type < ItemCount;
        }

        private static void FinishSetup()
        {
            LocalizedText[] _itemNameCache = (LocalizedText[])__itemNameCache.GetValue(null);
            ItemTooltip[] _itemTooltipCache = (ItemTooltip[])__itemTooltipCache.GetValue(null);

            for (int i = 0; i < items.Count; ++i)
            {
                ExtenItem item = items[i];
                item.Item.SetDefaults(ItemID.Count + i);

                TextureAssets.Item[item.Type] = item.Asset.Request<Texture2D>(item.Texture);

                _itemNameCache[item.Type] = item.DisplayName;
                _itemTooltipCache[item.Type] = ItemTooltip.FromLanguageKey(item.Tooltip.Key);

                ContentSamples.ItemsByType[item.Type] = item.Item;
                ContentSamples.ItemsByType[item.Type].RebuildTooltip();
            }

            __itemNameCache.SetValue(null, _itemNameCache);
            __itemTooltipCache.SetValue(null, _itemTooltipCache);
        }

        private static void BuildLookup()
        {
            ArmorSetBonus[] array = new ArmorSetBonus[0];
            ArmorSetBonuses.SetsContaining = new ArmorSetBonus[ItemCount][];

            for (int i = 0; i < ArmorSetBonuses.SetsContaining.Length; i++)
            {
                ArmorSetBonuses.SetsContaining[i] = array;
            }

            foreach (IGrouping<int, ArmorSetBonus> item in Enumerable.GroupBy(ArmorSetBonuses.All, set => set.Head))
            {
                ArmorSetBonuses.SetsContaining[item.Key] = Enumerable.ToArray(item);
            }

            foreach (IGrouping<int, ArmorSetBonus> item2 in Enumerable.GroupBy(ArmorSetBonuses.All, set => set.Body))
            {
                ArmorSetBonuses.SetsContaining[item2.Key] = Enumerable.ToArray(item2);
            }

            foreach (IGrouping<int, ArmorSetBonus> item3 in Enumerable.GroupBy(ArmorSetBonuses.All, set => set.Legs))
            {
                ArmorSetBonuses.SetsContaining[item3.Key] = Enumerable.ToArray(item3);
            }

            ArmorSetBonuses.SetsContaining[0] = array;
        }
    }
}
