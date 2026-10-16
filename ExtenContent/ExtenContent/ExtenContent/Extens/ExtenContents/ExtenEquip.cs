using ExtenContent.Utils;
using Terraria;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 扩展装备
    /// </summary>
    public abstract class ExtenEquip : ExtenType, TextureUtils.ITextureUtils
    {
        /// <summary>
        /// 纹理位置
        /// </summary>
        public virtual string Texture => this.GetTexture();
        /// <summary>
        /// 装备类型
        /// </summary>
        public abstract EquipType EquipType { get; }
        /// <summary>
        /// 装备插槽
        /// </summary>
        public int Slot { get; internal set; }

        /// <summary/>
        public virtual void ApplyEquipFunctionalPostfix(Player player, int itemSlot, Item currentItem) { }
        /// <summary/>
        public virtual void ApplyEquipVanityPostfix(Player player, int itemSlot, Item currentItem) { }
    }
}
