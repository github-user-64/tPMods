using ExtenContent.Exten;
using ExtenContent.Exten.Attributes;
using Terraria;

namespace NewContents.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class Equip1Head : EquipTemplate
    {
        public override void SetDefault(Item item)
        {
            item.defense = 1;
        }

        public override bool CanLoad() => true;
    }

    [AutoloadEquip(EquipType.Body)]
    internal class Equip1Body : EquipTemplate
    {
        public override void SetDefault(Item item)
        {
            item.defense = 2;
        }

        public override bool CanLoad() => true;
    }

    [AutoloadEquip(EquipType.Legs)]
    internal class Equip1Legs : EquipTemplate
    {
        public override void SetDefault(Item item)
        {
            item.defense = 3;
        }

        public override bool CanLoad() => true;
    }
}
