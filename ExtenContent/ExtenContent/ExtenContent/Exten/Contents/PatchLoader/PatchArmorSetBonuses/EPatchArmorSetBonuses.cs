using ExtenContent.Utils;
using Terraria.DataStructures;
using Terraria.Localization;

namespace ExtenContent.Exten
{
    /// <summary>
    /// 扩展套装效果修补
    /// </summary>
    public abstract class EPatchArmorSetBonus : ExtenType
    {
        /// <summary>
        /// 获取套装效果提示
        /// </summary>
        public LocalizedText GetTip(string key, string text)
        {
            return LanguageUtils.GetOrRegister($"{FullName}.{key}", text);
        }

        /// <summary>
        /// 添加套装效果
        /// </summary>
        public virtual void AddArmorSetBonus() { }

        /// <summary>
        /// 创建一个套装效果对象
        /// </summary>
        public ArmorSetBonus.Builder CreateArmorSetBonus(ArmorSetBonus.ArmorSetEffect effect, string textKey, string text, ArmorSetBonus.PartType primaryPart = ArmorSetBonus.PartType.None)
        {
            return ArmorSetBonus.Create(effect, GetTip(textKey, text).Key, primaryPart);
        }
    }
}
