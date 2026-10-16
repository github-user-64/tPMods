using ExtenContent.Extens;
using Terraria;

namespace NewContents.Content.Items.Armor
{
    internal class Head1 : ExtenItem
    {
        public override void SetDefault(Item item)
        {
            item.width = 18;
            item.height = 18;
            item.defense = 1;
            item.headSlot = ExtenManag.EquipSlot<EE>();
        }

        internal class EE : ExtenEquip
        {
            public override EquipType EquipType => EquipType.Head;
            public override string Texture => "Content/Items/Armor/Item1_Head";
        }
    }
}
