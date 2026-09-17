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

            return instance?.Type ?? ItemID.None;
        }

        /// <summary>
        /// 获取<see cref="ExtenEquip"/>对应的<see cref="ExtenEquip.Slot"/>, 不存在返回<see langword="-1"/>
        /// </summary>
        public static sbyte GetEquipSlot<T>() where T : ExtenEquip
        {
            T instance = ExtenInstance<T>.Instance;

            return (sbyte?)instance?.Slot ?? -1;
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

        /// <summary>
        /// 获取卸载物品的<see cref="ExtenType.FullName"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static string GetUnloadItemKey(string key) => ItemLoad.GetUnloadItemKey(key);
    }
}
