using ExtenContent.Extens;
using ExtenContent.Utils;
using Terraria;
using Terraria.Localization;

namespace NewContents.Content.Items
{
    internal class EItem3 : ExtenItem
    {
        public override string Texture => "Terraria/Images/Item_701";
        public override LocalizedText DisplayName => LanguageUtils.GetOrRegister($"{FullName}.{nameof(DisplayName)}", "钨宽石");
        public override LocalizedText Tooltip => LanguageUtils.GetOrRegister($"{FullName}.{nameof(Tooltip)}",
            $"近战武器尺寸增加{EffectVal * 100}%");

        public const float EffectVal = 0.5f;

        public override void SetDefault(Item item)
        {
            item.width = 16;
            item.height = 16;
            item.accessory = true;
            item.value = Item.buyPrice(0, 40);
            item.rare = 3;
        }

        public override void ApplyEquipFunctionalPostfix(Player player, int itemSlot, Item currentItem)
        {
            Common.ModifyMeleeScale.Add(player, EffectVal);
        }
    }
}
