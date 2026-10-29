using Terraria.ID;

namespace ExtenContent.Exten
{
    public static partial class ExtenManag
    {
        #region 装备
        /// <summary>
        /// 获取<see cref="ExtenEquip.Slot"/>, 不存在返回<see langword="-1"/>
        /// </summary>
        public static int EquipSlot<T>() where T : ExtenEquip
        {
            T instance = ExtenInstance<T>.Instance;

            return instance?.Slot ?? -1;
        }

        /// <summary>
        /// <see cref="ArmorIDs"/>是否是<see cref="ExtenEquip"/>
        /// </summary>
        public static bool IsExtenEquip(EquipType type, int slot) => EquipLoader.IsExten(type, slot);

        /// <summary>
        /// 获取<see cref="ExtenEquip"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static ExtenEquip GetExtenEquip(EquipType type, int slot) => EquipLoader.GetExtenEquip(type, slot);
        #endregion
    }
}
