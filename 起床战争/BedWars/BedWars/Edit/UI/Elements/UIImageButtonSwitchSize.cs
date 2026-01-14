using BedWars.Common.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BedWars.Edit.UI.Elements
{
    internal class UIImageButtonSwitchSize : UIImageButton
    {
        public Action<Rectangle> OnSetSize = null;
        public Color BorderColor { get => ss.BorderColor; set => ss.BorderColor = value; }
        public Color BackColor { get => ss.BackColor; set => ss.BackColor = value; }
        protected SwitchSize ss = new SwitchSize();

        public UIImageButtonSwitchSize(int size, string mouseText, string image) : base(size, mouseText, image)
        {
            ss.OnSetSize += v =>
            {
                ss.CanStartSwitch = false;

                OnSetSize?.Invoke(v);
            };

            OnClick += () => ss.CanStartSwitch = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            ss.Update();
        }

        private void DrawSwitch(SpriteBatch spriteBatch)
        {
            ss.DrawSwitch();
        }

        public override void OnDeactivate()
        {
            ss.End();
            Common.GameInterface.OnDraw.Remove(DrawSwitch);

            base.OnDeactivate();
        }

        public override void OnActivate()
        {
            base.OnActivate();

            Common.GameInterface.OnDraw.Add(DrawSwitch);
        }
    }
}
