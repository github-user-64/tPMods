using Microsoft.Xna.Framework.Graphics;
using ModTool.Common.UI;

namespace BedWars.Common.UI
{
    internal class UIItemTextBoxUpdate<T> : UIItemMouseText
    {
        public UITextBoxUpdate<T> tb { get; protected set; } = null;
        public GetSetString<T> gss { get => tb.gss; set => tb.gss = value; }

        public UIItemTextBoxUpdate(GetSetString<T> gss = null,
            string text_default = "", int Text_MaxLength = -1, Texture2D ico = null, string text = null) :
            base(ico, text)
        {
            tb = new UITextBoxUpdate<T>(null, text_default);
            tb.Width.Precent = 0.5f;
            tb.Height.Set(-6, 1);
            tb.HAlign = 1;
            tb.VAlign = 0.5f;
            tb.Text_MaxLength = Text_MaxLength;
            Append(tb);

            this.gss = gss;
        }
    }
}
