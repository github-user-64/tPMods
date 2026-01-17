using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using tContentPatch.Content.UI.ModSet;
using Terraria;

namespace BedWars.Edit.UI.Elements
{
    internal class UIItemSwitchSize : UIItemSwitch
    {
        public string MouseText = "启用后按住并拖动";
        public Action<Rectangle> OnSetSize = null;
        public Color BorderColor { get => ss.BorderColor; set => ss.BorderColor = value; }
        public Color BackColor { get => ss.BackColor; set => ss.BackColor = value; }
        protected SwitchSize ss = new SwitchSize();

        public UIItemSwitchSize(string text) : base(null, text)
        {
            ss.OnSetSize += v =>
            {
                OnSetSize?.Invoke(v);
            };

            OnValUpdate += v =>
            {
                if (v == false) ss.End();
            };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            ss.CanStartSwitch = GetVal();
            ss.Update();

            if (IsMouseHovering && MouseText != null) Main.instance.MouseText(MouseText);
        }

        private void DrawSwitchSize(SpriteBatch spriteBatch)
        {
            ss.DrawSwitch();
        }

        public override void OnDeactivate()
        {
            ss.End();
            Common.GameInterface.OnDraw.Remove(DrawSwitchSize);

            base.OnDeactivate();
        }

        public override void OnActivate()
        {
            base.OnActivate();

            Common.GameInterface.OnDraw.Add(DrawSwitchSize);
        }
    }
}
