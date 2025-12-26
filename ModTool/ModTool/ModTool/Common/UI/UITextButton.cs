using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;

namespace ModTool.Common.UI
{
    /// <summary>
    /// 文本按钮
    /// </summary>
    public class UITextButton : UIText
    {
        /// <summary/>
        public UITextButton(string text, float textScale = 1, bool large = false) : base(text, textScale, large)
        {
            OnMouseOver += (e, s) =>
            {
                SoundEngine.PlaySound(SoundID.MenuTick);
                TextColor = Colors.FancyUIFatButtonMouseOver;
            };
            OnMouseOut += (e, s) => TextColor = Color.White;
            OnLeftClick += (e, s) => SoundEngine.PlaySound(SoundID.MenuTick);
        }
    }
}
