using ExtenContent.Utils;
using Terraria;
using Terraria.Localization;

namespace ExtenContent.Extens
{
    public abstract class ExtenItem : ExtenType
    {
        public Item Item { get; } = new Item();
        public int Type => Item.type;
        public virtual string Texture { get; } = null;
        public virtual LocalizedText DisplayName => LanguageUtils.GetOrRegister($"{FullName}.{nameof(DisplayName)}", Name);
        public virtual LocalizedText Tooltip => LanguageUtils.GetOrRegister($"{FullName}.{nameof(Tooltip)}", string.Empty);

        public virtual void SetDefault(Item This) { }
        public virtual void Shoot(Player Player, Item This, int weaponDamage, bool withAudioVisualFeedback) { }
    }
}
