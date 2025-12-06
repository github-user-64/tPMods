using Microsoft.Xna.Framework;
using ModTool.Utils;
using System;
using tContentPatch.Content.UI;

namespace ModTool.Common.UI
{
    /// <summary>
    /// 文本框
    /// </summary>
    /// <typeparam name="T">文本转化为该类型</typeparam>
    public class UITextBox<T> : UITextBox
    {
        private readonly GetSetReset<T> gsr = null;

        /// <exception cref="ArgumentNullException"/>
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

        /// <inheritdoc/>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Focus) return;

            Text = gsr.val?.ToString() ?? string.Empty;
        }
    }
}
