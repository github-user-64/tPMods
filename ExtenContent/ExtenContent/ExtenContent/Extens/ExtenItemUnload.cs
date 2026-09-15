using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 卸载物品
    /// </summary>
    internal class ExtenItemUnload : ExtenItem
    {
        private static readonly Dictionary<string, int> items = new Dictionary<string, int>();

        public override string Texture => "ExtenItemUnload";
        public string key = null;
        public Item item = null;

        public override void SetDefault(Item This)
        {
            This.width = 20;
            This.height = 20;
        }

        internal static void Load()
        {

        }

        internal static void Unload()
        {
            items.Clear();
        }

        /// <summary>
        /// 尝试注册卸载物品
        /// </summary>
        public static int TryRegist(string key)
        {
            if (key == null) return -1;

            int v = items.Count;

            items[key] = v;

            return v;
        }

        /// <summary>
        /// 获取卸载物品的<see cref="ExtenType.FullName"/>, 没有返回<see langword="null"/>
        /// </summary>
        public static string GetUnloadItemFullName(Item item)
        {
            if (item.type != ExtenManag.ItemType<ExtenItemUnload>()) return null;
            int v = item.buffType;

            if (v >= items.Count) return null;

            return items.ToArray()[v].Key;
        }
    }
}
