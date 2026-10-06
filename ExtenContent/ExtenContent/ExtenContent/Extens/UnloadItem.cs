using Terraria;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 卸载物品
    /// </summary>
    public class UnloadItem : ExtenItem
    {
        /// <inheritdoc/>
        public override string Texture => "ExtenItemUnload";

        /// <inheritdoc/>
        public override void SetDefault(Item item)
        {
            item.width = 20;
            item.height = 20;
        }
    }
}
