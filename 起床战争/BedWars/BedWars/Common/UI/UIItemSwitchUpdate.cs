using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tContentPatch.Content.UI.ModSet;
using Terraria;

namespace BedWars.Common.UI
{
    internal class UIItemSwitchUpdate : UIItemSwitch
    {
        public string MouseText = null;
        private GetSetStringBool gss = null;

        public UIItemSwitchUpdate(GetSetStringBool gss,
            Texture2D ico = null, string text = null) :
            base(ico, text)
        {
            this.gss = gss;

            OnValUpdate += v =>
            {
                if (gss.Get() == v) return;
                gss.Set(v);
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
