using ExtenContent.Extens;
using ExtenContent.Utils;
using NewContents.Common;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;

namespace NewContents.Content.Buffs
{
    internal class EBuff1 : ExtenBuff
    {
        public override string Texture => "Content/Buff1";
        public override LocalizedText DisplayName => LanguageUtils.GetOrRegister($"{FullName}.{nameof(DisplayName)}", "力量");
        public override LocalizedText Description => LanguageUtils.GetOrRegister($"{FullName}.{nameof(Description)}",
            $"力量!!!\n近战伤害增加{val * 100}%");

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;//退出世界时消失
            Main.buffNoTimeDisplay[Type] = true;//不显示时间
        }

        public readonly static float val = 1f;
        public override void UpdateBuffs(Player player)
        {
            ArmorSetBonus.QueryContext context2 = new ArmorSetBonus.QueryContext(player);

            if (ArmorSetBonuses.GetCompleteSet(context2)?.Effect == ExtenArmorSetBonus.GotBuff1)
            {
                player.meleeDamage += val;
                return;
            }

            //没有套装效果时
            player.ClearBuff(Type);
        }
    }
}
