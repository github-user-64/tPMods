using ExtenContent.Exten;
using ExtenContent.Utils;
using NewContents.Common;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;

namespace NewContents.Content.Buffs
{
    internal class Buff1 : ExtenBuff
    {
        public override LocalizedText DisplayName => this.GetLocalization($"{nameof(DisplayName)}", "力量");
        public override LocalizedText Description => this.GetLocalization($"{nameof(Description)}",
            $"近战伤害增加{val * 100}%\n力量!!!");

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;//退出世界时消失
            Main.buffNoTimeDisplay[Type] = true;//不显示时间
        }

        public readonly static float val = 1f;
        public override void UpdateBuffs(Player player)
        {
            ArmorSetBonus.QueryContext context = new ArmorSetBonus.QueryContext(player);

            if (ArmorSetBonuses.GetCompleteSet(context)?.Effect == ExtenArmorSetBonus.GotBuff1)
            {
                player.meleeDamage += val;
                return;
            }

            //没有套装效果时
            player.ClearBuff(Type);
        }
    }
}
