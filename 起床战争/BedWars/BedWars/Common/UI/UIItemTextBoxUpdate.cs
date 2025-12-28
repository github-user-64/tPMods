using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModTool.Common.UI;
using System;
using tContentPatch.Content.UI;

namespace BedWars.Common.UI
{
    internal class UIItemTextBoxUpdate<T> : UIItemMouseText
    {
        public UITextBox tb { get; protected set; } = null;
        private GetSetString<T> gss = null;

        public UIItemTextBoxUpdate(GetSetString<T> gss,
            string text_default = "", int Text_MaxLength = -1, Texture2D ico = null, string text = null) :
            base(ico, text)
        {
            this.gss = gss;

            tb = new UITextBox(text_default);
            tb.Width.Precent = 0.5f;
            tb.Height.Set(-6, 1);
            tb.HAlign = 1;
            tb.VAlign = 0.5f;
            tb.Text_MaxLength = Text_MaxLength;
            tb.OnLostFocus += () =>
            {
                if (gss.GetS() == tb.Text) return;
                gss.SetS(tb.Text);
            };
            Append(tb);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (tb.Focus) return;

            tb.SetText(gss.GetS());
        }
    }
}
