using Terraria;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 扩展玩家修补
    /// </summary>
    public abstract class EPatchPlayer : ExtenType
    {
        /// <summary>
        /// 获取物品挥舞尺寸
        /// </summary>
        public virtual void GetAdjustedItemScalePostfix(ref float result, Player player, Item item) { }
    }
}
