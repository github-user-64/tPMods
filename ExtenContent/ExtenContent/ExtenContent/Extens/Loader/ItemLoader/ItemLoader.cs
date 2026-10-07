using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;

namespace ExtenContent.Extens
{
    /// <summary/>
    public static partial class ItemLoader
    {
        private static readonly FieldInfo __itemNameCache = typeof(Lang).GetField("_itemNameCache", BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly FieldInfo __itemTooltipCache = typeof(Lang).GetField("_itemTooltipCache", BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly PropertyInfo _itemVariant = typeof(Item).GetProperty("Variant", BindingFlags.Public | BindingFlags.Instance);

        /// <summary>
        /// 物品数量, <see cref="ItemID.Count"/>的数量加上<see cref="ExtenItem"/>的数量
        /// </summary>
        public static int ItemCount { get; private set; } = ItemID.Count;
        private static readonly List<ExtenItem> items = new List<ExtenItem>();
        private static readonly Dictionary<string, ExtenItem> itemsKey = new Dictionary<string, ExtenItem>();
        private static readonly Dictionary<string, object> itemsUnload = new Dictionary<string, object>();

        internal static void Load()
        {
            ResizeArrays();
            FinishSetup();
            BuildLookup();

            items.ForEach(i => i.Load());
        }

        internal static void Unload()
        {
            items.ForEach(i => i.Unload());

            ItemCount = ItemID.Count;
            items.Clear();
            itemsKey.Clear();
            itemsUnload.Clear();
        }

        internal static void Register(ExtenItem item)
        {
            string key = item.FullName;
            if (key == null) throw new Exception($"{nameof(ItemLoader)}:物品的{nameof(ExtenItem.FullName)}为null");
            if (GetItem(key) != null) throw new Exception($"{nameof(ItemLoader)}:物品[{key}]已注册");

            items.Add(item);
            itemsKey[key] = item;
            ++ItemCount;
        }

        /// <summary>
        /// 注册一个卸载物品
        /// </summary>
        public static void RegisterUnload(string key)
        {
            if (key == null) return;
            if (itemsUnload.ContainsKey(key)) return;

            itemsUnload.Add(key, null);
        }

        /// <summary>
        /// 获取卸载物品的<see cref="ExtenType.FullName"/>, 不存在返回<see langword="null"/><br/>
        /// 返回的key肯定和传入的参数一样, 该方法是用于判断是否有注册这个卸载物品
        /// </summary>
        public static string GetUnloadItemKey(string key)
        {
            if (key == null) return null;
            if (itemsUnload.ContainsKey(key)) return key;

            return null;
        }

        /// <summary>
        /// 将<paramref name="item"/>设为卸载物品并返回<see langword="true"/>, 不存在则不处理并返回<see langword="false"/>
        /// </summary>
        public static bool SetUnloadItem(Item item, string key)
        {
            string uk = GetUnloadItemKey(key);
            if (uk == null) return false;

            item.SetDefaults(ExtenManag.ItemType<UnloadItem>());
            item.SetNameOverride(uk);

            return true;
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
        /// 获取<see cref="ExtenType.FullName"/>对应的<see cref="ExtenItem"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static ExtenItem GetItem(string key)
        {
            if (key == null) return null;
            if (itemsKey.ContainsKey(key) != true) return null;

            return itemsKey[key];
        }

        /// <summary>
        /// 获取<see cref="ExtenType.FullName"/>对应的<see cref="Item.type"/>, 不存在返回<see cref="ItemID.None"/>
        /// </summary>
        public static int GetItemType(string key)
        {
            return GetItem(key)?.Type ?? ItemID.None;
        }

        /// <summary>
        /// 获取<see cref="Item.type"/>对应的<see cref="ExtenType.FullName"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static string GetItemKey(int type)
        {
            return GetItem(type)?.FullName;
        }

        /// <summary>
        /// <see cref="Item.type"/>是否是<see cref="ExtenItem"/>
        /// </summary>
        public static bool TypeInRange(int type)
        {
            return ItemID.Count <= type && type < ItemCount;
        }
    }
}
