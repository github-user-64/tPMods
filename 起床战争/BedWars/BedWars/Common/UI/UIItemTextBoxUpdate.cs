using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModTool.Common.UI;
using System;
using tContentPatch.Content.UI;

namespace BedWars.Common.UI
{
    internal class UIItemTextBoxUpdate : UIItemMouseText
    {
        public Func<string> GetV = null;
        public Action<string> SetV = null;
        public UITextBox tb { get; protected set; } = null;

        public UIItemTextBoxUpdate(Func<string> GetV, Action<string> SetV,
            string text_default = "", int Text_MaxLength = -1, Texture2D ico = null, string text = null) :
            base(ico, text)
        {
            this.GetV = GetV;
            this.SetV = SetV;

            tb = new UITextBox(text_default);
            tb.Width.Precent = 0.5f;
            tb.Height.Set(-6, 1);
            tb.HAlign = 1;
            tb.VAlign = 0.5f;
            tb.Text_MaxLength = Text_MaxLength;
            tb.OnLostFocus += () =>
            {
                if (GetV != null && GetV() == tb.Text) return;
                SetV?.Invoke(tb.Text);
            };
            Append(tb);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (tb.Focus) return;

            if (GetV != null) tb.SetText(GetV());
        }
    }
}
