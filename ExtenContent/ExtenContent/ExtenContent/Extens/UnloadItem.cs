using Terraria;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 卸载物品
    /// </summary>
    internal class UnloadItem : ExtenItem
    {
        public override string Texture => "ExtenItemUnload";

        public override void SetDefault(Item This)
        {
            This.width = 20;
            This.height = 20;
        }
    }
}
