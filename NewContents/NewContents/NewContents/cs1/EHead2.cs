using ExtenContent.Extens;
using Terraria;

namespace NewContents.cs1
{
    internal class EHead2 : ExtenItem
    {
        public override string Texture => "cs1/b1";

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
            public override string Texture => "cs1/b1_Head";
        }
    }
}
