using ExtenContent.Utils;
using Terraria;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 扩展装备
    /// </summary>
    public abstract class ExtenEquip : ExtenType, ClassPathUtils.IClassPathUtils
    {
        /// <summary>
        /// 纹理位置
        /// </summary>
        public virtual string Texture => this.GetPath();
        /// <summary>
        /// 装备类型
        /// </summary>
        public abstract EquipType EquipType { get; }
        /// <summary>
        /// 装备插槽
        /// </summary>
        public int Slot { get; internal set; }

        /// <summary>
        /// 只对饰品有效
        /// </summary>
        public virtual void ApplyEquipFunctionalPostfix(Player player, int itemSlot, Item currentItem) { }
        /// <summary>
        /// 只对饰品有效
        /// </summary>
        public virtual void ApplyEquipVanityPostfix(Player player, int itemSlot, Item currentItem) { }
        /// <summary>
        /// 应用盔甲和饰品的效果
        /// </summary>
        public virtual void UpdateEquip(Player player, Item armorPiece) { }
    }
}
