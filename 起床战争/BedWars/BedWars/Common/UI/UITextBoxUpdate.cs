using Microsoft.Xna.Framework;
using tContentPatch.Content.UI;
using Terraria;

namespace BedWars.Common.UI
{
    internal class UITextBoxUpdate<T> : UITextBox
    {
        public string MouseText = null;
        public GetSetString<T> gss = null;

        public UITextBoxUpdate(GetSetString<T> gss = null, string text_default = "") : base(text_default)
        {
            this.gss = gss;

            OnLostFocus += () =>
            {
                if (this.gss == null) return;
                if (this.gss.GetS() == Text) return;
                this.gss.SetS(Text);
            };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsMouseHovering && MouseText != null) Main.instance.MouseText(MouseText);

            if (Focus) return;

            SetText(gss?.GetS());
        }
    }
}
