using ExtenContent.Extens;
using Terraria;

namespace NewContents.Common
{
    internal class ExtenArmorSetBonus : EPatchArmorSetBonus
    {
        public override void AddArmorSetBonus()
        {
            CreateArmorSetBonus(MeleeDamageUp, nameof(MeleeDamageUp), $"超级力量!!!\n近战伤害提升{MeleeDamageUp_val * 100}%")
                .Set(ExtenManag.ItemType<cs1.EHead>(), ExtenManag.ItemType<cs1.EBody>(), ExtenManag.ItemType<cs1.ELegs>())
                .Set(ExtenManag.ItemType<cs1.EHead2>(), ExtenManag.ItemType<cs1.EBody>(), ExtenManag.ItemType<cs1.ELegs>())
                .Add();
        }

        protected readonly static float MeleeDamageUp_val = 1f;
        protected void MeleeDamageUp(Player player)
        {
            player.meleeDamage += MeleeDamageUp_val;
        }
    }
}
