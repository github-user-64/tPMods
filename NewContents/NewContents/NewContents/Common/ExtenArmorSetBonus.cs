using ExtenContent.Exten;
using NewContents.Content.Items.Armor;
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
                    ExtenManag.ItemType<Equip1Head>(),
                    ExtenManag.ItemType<Equip2>(),
                },
                new int[] { ExtenManag.ItemType<Equip1Body>() },
                new int[] { ExtenManag.ItemType<Equip1Legs>() })
                .Add();
        }

        public static void GotBuff1(Player player)
        {
            player.AddBuff(ExtenManag.BuffType<Content.Buffs.Buff1>(), 60 * 10);
        }
    }
}
