using ExtenContent.Extens;
using ExtenContent.Utils;
using Terraria;
using Terraria.Localization;

namespace NewContents.Content.Items.Armor
{
    internal class Head2 : ExtenItem
    {
        public override string Texture => "Terraria/Images/Item_1742";
        public override LocalizedText Tooltip => this.GetLocalization($"{nameof(Tooltip)}", "[W]已损坏");

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
            public override string Texture => "Terraria/Images/Armor_Head_116";
        }
    }
}
