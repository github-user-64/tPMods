using ExtenContent.Extens;
using Terraria;

namespace NewContents.Content.Items.Armor
{
    internal class Body1 : ExtenItem
    {
        public override void SetDefault(Item item)
        {
            item.width = 18;
            item.height = 18;
            item.defense = 1;
            item.bodySlot = ExtenManag.EquipSlot<EE>();
        }

        internal class EE : ExtenEquip
        {
            public override EquipType EquipType => EquipType.Body;
            public override string Texture => "Content/Items/Armor/Item1_Body";
        }
    }
}
