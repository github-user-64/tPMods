namespace ExtenContent.Extens
{
    /// <summary>
    /// 扩展装备
    /// </summary>
    public abstract class ExtenEquip : ExtenType
    {
        /// <summary>
        /// 纹理位置
        /// </summary>
        public virtual string Texture { get; } = null;
        /// <summary>
        /// 装备类型
        /// </summary>
        public abstract EquipType EquipType { get; }
        /// <summary>
        /// 装备插槽
        /// </summary>
        public int Slot { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual void UpdateEquips() { }
    }
}
