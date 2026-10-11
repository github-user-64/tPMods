using ExtenContent.Extens;
using Terraria;
using Terraria.ID;

namespace NewContents.cs1
{
    internal class EHead : ExtenItem
    {
        public override string Texture => "cs1/a1";

        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.HidesHead[ExtenManag.GetEquipSlot<EE>()] = true;
        }

        public override void SetDefault(Item item)
        {
            item.width = 18;
            item.height = 18;
            item.defense = 1;
            item.headSlot = ExtenManag.GetEquipSlot<EE>();
        }

        internal class EE : ExtenEquip
        {
            public override EquipType EquipType => EquipType.Head;
            public override string Texture => "cs1/a1_Head";
        }
    }
}
