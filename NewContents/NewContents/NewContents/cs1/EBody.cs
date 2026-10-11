using ExtenContent.Extens;
using Terraria;

namespace NewContents.cs1
{
    internal class EBody : ExtenItem
    {
        public override string Texture => "cs1/a2";

        public override void SetDefault(Item item)
        {
            item.width = 18;
            item.height = 18;
            item.defense = 1;
            item.bodySlot = ExtenManag.GetEquipSlot<EE>();
        }

        internal class EE : ExtenEquip
        {
            public override EquipType EquipType => EquipType.Body;
            public override string Texture => "cs1/a2_Body";
        }
    }
}
