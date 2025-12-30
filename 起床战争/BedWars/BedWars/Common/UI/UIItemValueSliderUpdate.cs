using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tContentPatch.Content.UI.ModSet;
using Terraria;

namespace BedWars.Common.UI
{
    internal class UIItemValueSliderUpdate : UIItemValueSlider
    {
        public string MouseText = null;
        private GetSetStringInt gss = null;

        public UIItemValueSliderUpdate(GetSetStringInt gss, int min, int max, Texture2D ico = null, string text = null) : base(min, max, ico, text)
        {
            build(gss);
        }

        private void build(GetSetStringInt gss)
        {
            this.gss = gss;

            OnValUpdate += v =>
            {
                if (gss.Get() == v) return;
                gss.Set((int)v);
            };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsMouseHovering && MouseText != null) Main.instance.MouseText(MouseText);

            SetVal(gss.Get());
        }
    }
}
