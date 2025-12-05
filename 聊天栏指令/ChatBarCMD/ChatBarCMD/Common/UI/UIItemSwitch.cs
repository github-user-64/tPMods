using ChatBarCMD.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using tContentPatch.Content.UI;

namespace ChatBarCMD.Common.UI
{
    internal class UIItemSwitch : UIItemMouseText
    {
        private readonly UISwitch ui_s = null;
        private readonly GetSetReset<bool> gsr = null;

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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            ui_s.SetVal(gsr.val);
        }
    }
}
