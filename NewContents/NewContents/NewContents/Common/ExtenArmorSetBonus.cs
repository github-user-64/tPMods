using ExtenContent.Extens;
using Terraria;

namespace NewContents.Common
{
    internal class ExtenArmorSetBonus : EPatchArmorSetBonus
    {
        public override void AddArmorSetBonus()
        {
            CreateArmorSetBonus(GotBuff1, nameof(GotBuff1), $"获取来自异界的力量")
                .Set(new int[]
                {
                    ExtenManag.ItemType<cs1.EHead>(),
                    ExtenManag.ItemType<cs1.EHead2>(),
                },
                new int[] { ExtenManag.ItemType<cs1.EBody>() },
                new int[] { ExtenManag.ItemType<cs1.ELegs>() })
                .Add();
        }

        public static void GotBuff1(Player player)
        {
            player.AddBuff(ExtenManag.BuffType<Content.Buffs.EBuff1>(), 60 * 10);
        }
    }
}
