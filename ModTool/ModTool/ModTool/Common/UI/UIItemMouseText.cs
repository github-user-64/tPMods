using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tContentPatch.Content.UI.ModSet;
using Terraria;

namespace ModTool.Common.UI
{
    /// <summary>
    /// 项, 鼠标文本
    /// </summary>
    public class UIItemMouseText : UIItem
    {
        /// <summary>
        /// 鼠标文本
        /// </summary>
        public string MouseText = null;

        /// <summary/>
        public UIItemMouseText(Texture2D ico = null, string text = null) : base(ico, text) { }

        /// <inheritdoc/>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsMouseHovering == false) return;
            if (MouseText == null) return;

            Main.instance.MouseText(MouseText);
        }
    }
}
