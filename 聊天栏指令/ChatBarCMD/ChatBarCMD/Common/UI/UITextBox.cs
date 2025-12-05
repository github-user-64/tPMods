using ChatBarCMD.Utils;
using Microsoft.Xna.Framework;
using System;
using tContentPatch.Content.UI;

namespace ChatBarCMD.Common.UI
{
    internal class UITextBox<T> : UITextBox
    {
        private readonly GetSetReset<T> gsr = null;

        public UITextBox(GetSetReset<T> gsr, Func<string, T> parseTry,
            string text_default = "") : base(text_default)
        {
            this.gsr = gsr ?? throw new ArgumentNullException(nameof(gsr));

            OnLostFocus += () =>
            {
                try
                {
                    gsr.val = parseTry(Text);
                }
                catch
                {
                    Text = gsr.val?.ToString() ?? string.Empty;
                }
            };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Focus) return;

            Text = gsr.val?.ToString() ?? string.Empty;
        }
    }
}
