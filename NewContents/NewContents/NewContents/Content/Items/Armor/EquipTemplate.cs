using ExtenContent.Extens;
using Terraria;

namespace NewContents.Content.Items.Armor
{
    internal class EquipTemplate : ExtenItem
    {
        public override void SetDefault(Item item)
        {
            item.width = 18;
            item.height = 18;
        }

        public override bool CanLoad() => false;
    }
}
