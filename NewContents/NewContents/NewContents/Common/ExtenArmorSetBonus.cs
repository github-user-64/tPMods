using ExtenContent.Extens;
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
                    ExtenManag.ItemType<Head1>(),
                    ExtenManag.ItemType<Head2>(),
                },
                new int[] { ExtenManag.ItemType<Body1>() },
                new int[] { ExtenManag.ItemType<Legs1>() })
                .Add();
        }

        public static void GotBuff1(Player player)
        {
            player.AddBuff(ExtenManag.BuffType<Content.Buffs.Buff1>(), 60 * 10);
        }
    }
}
