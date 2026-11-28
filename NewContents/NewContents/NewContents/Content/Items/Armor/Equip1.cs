using ExtenContent.Exten;
using ExtenContent.Exten.Attributes;
using Terraria;

namespace NewContents.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class Equip1Head : ExtenItem
    {
        public override void SetDefault(Item item) => item.defense = 1;
    }

    [AutoloadEquip(EquipType.Body)]
    internal class Equip1Body : ExtenItem
    {
        public override void SetDefault(Item item) => item.defense = 2;
    }

    [AutoloadEquip(EquipType.Legs)]
    internal class Equip1Legs : ExtenItem
    {
        public override void SetDefault(Item item) => item.defense = 3;
    }
}
