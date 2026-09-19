using ExtenContent.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 扩展物品
    /// </summary>
    public abstract class ExtenItem : ExtenType
    {
        /// <summary/>
        public Item Item { get; } = new Item();
        /// <summary>
        /// 对应的<see cref="Item.type"/>
        /// </summary>
        public int Type => Item.type;
        /// <summary>
        /// 物品图标位置
        /// </summary>
        public virtual string Texture { get; } = null;
        /// <summary>
        /// 物品名
        /// </summary>
        public virtual LocalizedText DisplayName => LanguageUtils.GetOrRegister($"{FullName}.{nameof(DisplayName)}", Name);
        /// <summary>
        /// 物品工具提示
        /// </summary>
        public virtual LocalizedText Tooltip => LanguageUtils.GetOrRegister($"{FullName}.{nameof(Tooltip)}", string.Empty);

        /// <summary/>
        public virtual void SetDefault(Item item) { }
        /// <summary/>
        public virtual void Shoot(Player player, Item item, int weaponDamage, bool withAudioVisualFeedback) { }
        /// <summary>
        /// 物品动画开始时
        /// </summary>
        public virtual void ApplyItemAnimationPostfix(Player player, Item item) { }
        /// <summary>
        /// 
        /// </summary>
        public virtual void ApplyEquipFunctionalPostfix(Player player, int itemSlot, Item currentItem) { }
        /// <summary>
        /// 
        /// </summary>
        public virtual void ApplyEquipVanityPostfix(Player player, int itemSlot, Item currentItem) { }
        /// <summary>
        /// 
        /// </summary>
        public virtual void ModifyTooltips(Item item, ref int yoyoLogo, ref float oldKB, ref int numLines, ref string[] toolTipLine, ref Color[] lineColors) { }
        /// <summary>
        /// 允许通过右键单击使此项目可用,默认情况下返回false当通过右键单击使用此item时player.altFunctionUse将设置为2
        /// </summary>
        public virtual bool AltFunctionUse(Player player, Item item) => false;
        /// <summary>
        /// 
        /// </summary>
        public virtual bool CanUseItem(Player player, Item item) => true;
    }
}
