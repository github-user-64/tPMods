using ExtenContent.Utils;
using Terraria;
using Terraria.Localization;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 扩展buff
    /// </summary>
    public abstract class ExtenBuff : ExtenType, LanguageUtils.ILanguageUtils
    {
        /// <summary>
        /// 对应的buffType
        /// </summary>
        public int Type { get; internal set; }
        /// <summary>
        /// 图标位置
        /// </summary>
        public virtual string Texture { get; } = null;
        /// <summary/>
        public virtual LocalizedText DisplayName => this.GetLocalization($"{nameof(DisplayName)}", Name);
        /// <summary/>
        public virtual LocalizedText Description => this.GetLocalization($"{nameof(Description)}", null);

        /// <summary/>
        public virtual void UpdateBuffs(Player player) { }
        /// <summary/>
        public virtual void UpdateBuffs(NPC npc, bool lowerBuffTime) { }
    }
}
