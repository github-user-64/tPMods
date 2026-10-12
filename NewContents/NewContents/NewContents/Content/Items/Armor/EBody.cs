using ExtenContent.Extens;
using Terraria;

namespace NewContents.Content.Items.Armor
{
    internal class EBody : ExtenItem
    {
        public override string Texture => "Content/Armor/Item1_1";

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
            public override string Texture => "Content/Armor/Item1_Body";
        }
    }
}
