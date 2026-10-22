using ExtenContent.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 扩展物品
    /// </summary>
    public abstract class ExtenItem : ExtenType, LanguageUtils.ILanguageUtils, ClassPathUtils.IClassPathUtils
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
        public virtual string Texture => this.GetPath();
        /// <summary>
        /// 物品名
        /// </summary>
        public virtual LocalizedText DisplayName => this.GetLocalization($"{nameof(DisplayName)}", Name);
        /// <summary>
        /// 物品工具提示
        /// </summary>
        public virtual LocalizedText Tooltip => this.GetLocalization($"{nameof(Tooltip)}", string.Empty);

        /// <summary/>
        public virtual void SetDefault(Item item) { }
        /// <summary/>
        public virtual bool Shoot(Player player, Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) => true;
        /// <summary>
        /// 物品动画开始时
        /// </summary>
        public virtual void ApplyItemAnimationPostfix(Player player, Item item) { }
        /// <summary>
        /// 只对饰品有效
        /// </summary>
        public virtual void ApplyEquipFunctionalPostfix(Player player, int itemSlot, Item currentItem) { }
        /// <summary>
        /// 只对饰品有效
        /// </summary>
        public virtual void ApplyEquipVanityPostfix(Player player, int itemSlot, Item currentItem) { }
        /// <summary>
        /// 应用盔甲和饰品的效果
        /// </summary>
        public virtual void UpdateEquip(Player player, Item armorPiece) { }
        /// <summary/>
        public virtual void ModifyTooltips(Item item, ref int yoyoLogo, ref float oldKB, ref int numLines, ref string[] toolTipLine, ref Color[] lineColors) { }
        /// <summary>
        /// 允许通过右键单击使此项目可用,默认情况下返回false当通过右键单击使用此item时player.altFunctionUse将设置为2
        /// </summary>
        public virtual bool AltFunctionUse(Player player, Item item) => false;
        /// <summary/>
        public virtual bool CanUseItem(Player player, Item item) => true;
        /// <summary/>
        public virtual void OnHitNPC(Player player, Item item, Rectangle itemRectangle, int originalDamage, float knockBack, NPC npc) { }
        /// <summary/>
        public virtual void AddRecipes() { }
        /// <summary>
        /// <see cref="SetDefault(Item)"/>前调用, 默认实现用于设置装备slot
        /// </summary>
        public virtual void AutoDefaults(Item item) => EquipLoader.SetSlot(item);

        /// <summary>
        /// 获取一个配方对象
        /// </summary>
        public Recipe CreateRecipe(int stack = 1) => PatchRecipeLoader.Create(Type, stack);
    }
}
