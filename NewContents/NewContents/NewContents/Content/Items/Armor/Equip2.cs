using ExtenContent.Extens;
using ExtenContent.Utils;
using Terraria;
using Terraria.Localization;

namespace NewContents.Content.Items.Armor
{
    internal class Equip2 : EquipTemplate
    {
        public override string Texture => "Terraria/Images/Item_1742";
        public override LocalizedText Tooltip => this.GetLocalization($"{nameof(Tooltip)}", "\"[W]已损坏，你无法思考\"");

        public override void SetDefault(Item item)
        {
            item.defense = 11;
            item.headSlot = ExtenManag.EquipSlot<EE>();
        }

        public override bool CanLoad() => true;

        internal class EE : ExtenEquip
        {
            public override EquipType EquipType => EquipType.Head;
            public override string Texture => "Terraria/Images/Armor_Head_116";
        }

        public override void UpdateEquip(Player player, Item armorPiece)
        {
            if (player != Main.LocalPlayer) return;
            if (Main.dedServ) return;

            Main.bgStyle = ExtenInstance<Back.Back1>.Instance.Type;
        }
    }
}
