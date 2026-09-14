using Terraria;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 卸载物品
    /// </summary>
    internal class ExtenItemUnload : ExtenItem
    {
        public override string Texture => "ExtenItemUnload";

        public override void SetDefault(Item This)
        {
            This.width = 2;
            This.height = 2;
        }
    }
}
