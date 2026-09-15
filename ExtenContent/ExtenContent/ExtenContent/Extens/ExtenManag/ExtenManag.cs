using Terraria;
using Terraria.ID;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 扩展管理
    /// </summary>
    public static partial class ExtenManag
    {
        /// <summary>
        /// 获取<see cref="ExtenItem"/>对应的<see cref="Item.type"/>, 不存在返回<see cref="ItemID.None"/>
        /// </summary>
        public static int ItemType<T>() where T : ExtenItem
        {
            T instance = ExtenInstance<T>.Instance;
            if (instance == null) return ItemID.None;

            return instance.Type;
        }

        /// <summary>
        /// 获取<see cref="Item.type"/>对应的<see cref="ExtenItem"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static ExtenItem GetExtenItem(int type) => ItemLoad.GetItem(type);

        /// <summary>
        /// 获取<see cref="ExtenType.FullName"/>对应的<see cref="ExtenItem"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static ExtenItem GetItem(string key) => ItemLoad.GetItem(key);

        /// <summary>
        /// <see cref="Item.type"/>是否是<see cref="ExtenItem"/>
        /// </summary>
        public static bool IsExtenItem(int type) => ItemLoad.TypeInRange(type);
    }
}
