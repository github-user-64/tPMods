using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModTool.Utils;
using System;
using tContentPatch.Content.UI;

namespace ModTool.Common.UI
{
    /// <summary>
    /// 项, 开关
    /// </summary>
    public class UIItemSwitch : UIItemMouseText
    {
        private readonly UISwitch ui_s = null;
        private readonly GetSetReset<bool> gsr = null;

        /// <exception cref="ArgumentNullException"></exception>
        public UIItemSwitch(GetSetReset<bool> gsr, Texture2D ico = null, string text = null) : base(ico, text)
        {
            this.gsr = gsr ?? throw new ArgumentNullException(nameof(gsr));

            ui_s = new UISwitch();
            ui_s.Height.Precent = 1f;
            ui_s.HAlign = 1f;
            ui_s.VAlign = 0.5f;
            ui_s.OnValUpdate = v => gsr.val = v;

            Append(ui_s);
        }

        /// <inheritdoc/>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            ui_s.SetVal(gsr.val);
        }
    }
}
