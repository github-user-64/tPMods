using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;

namespace BedWars.Common.UI
{
    internal class UIFoldPanel : UIPanel
    {
        private UIFold f = null;

        public UIFoldPanel(UIFold f)
        {
            this.f = f;

            Width.Precent = 1;
            BackgroundColor = new Color(63, 82, 151) * 0.7f;
            BorderColor = new Color(43, 60, 120);
            SetPadding(6);

            Append(this.f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Height.Pixels = f.Height.Pixels + PaddingTop + PaddingBottom;
            Width.Pixels = f.Width.Pixels + PaddingLeft + PaddingRight;

            if (IsMouseHovering && f.IsOpen == false) BorderColor = new Color(123, 123, 200);
            else BorderColor = new Color(43, 60, 120);
        }
    }
}
