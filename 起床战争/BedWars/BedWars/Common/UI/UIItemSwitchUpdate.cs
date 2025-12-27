using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using tContentPatch.Content.UI.ModSet;
using Terraria;

namespace BedWars.Common.UI
{
    internal class UIItemSwitchUpdate : UIItemSwitch
    {
        public Func<bool> GetV = null;
        public Action<bool> SetV = null;
        public string MouseText = null;

        public UIItemSwitchUpdate(Func<bool> GetV, Action<bool> SetV,
            Texture2D ico = null, string text = null) :
            base(ico, text)
        {
            this.GetV = GetV;
            this.SetV = SetV;

            OnValUpdate += v =>
            {
                if (GetV != null && GetV() == v) return;
                SetV?.Invoke(v);
            };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsMouseHovering && MouseText != null) Main.instance.MouseText(MouseText);

            if (GetV != null) SetVal(GetV());
        }
    }
}
