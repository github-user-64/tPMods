using ChatBarCMD.Utils;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ChatBarCMD.Common.UI
{
    internal class UIItemTextBox<T> : UIItemMouseText
    {
        public UIItemTextBox(GetSetReset<T> gsr, Func<string, T> parseTry,
            Texture2D ico = null, string text = null) : base(ico, text)
        {
            UITextBox<T> ui_t = new UITextBox<T>(gsr, parseTry);
            ui_t.Width.Set(0, 0.5f);
            ui_t.Height.Set(-6, 1);
            ui_t.HAlign = 1;
            ui_t.VAlign = 0.5f;

            Append(ui_t);
        }
    }
}
