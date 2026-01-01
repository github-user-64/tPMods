using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using tContentPatch.Content.UI.ModSet;
using Terraria;

namespace BedWars.Edit.UI.Elements
{
    internal class UIItemSwitchSize : UIItemSwitch
    {
        public Action<Rectangle> OnSetSize = null;
        public Color BorderColor = Color.LawnGreen * 0.9f;
        public Color BackColor = Color.LawnGreen * 0.2f;
        public bool Switching = false;
        private Rectangle size = Rectangle.Empty;

        public UIItemSwitchSize(string text) : base(null, text)
        {
            OnValUpdate += v =>
            {
                if (v == false) Switching = false;
            };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            UpdateSwitch();

            if (IsMouseHovering) Main.instance.MouseText("启用后按住并拖动");
        }

        private void UpdateSwitch()
        {
            if (Switching == false)
            {
                if (GetVal() == false) return;
                if (Main.LocalPlayer.mouseInterface) return;
                if (Main.mouseLeft == false) return;

                Point pos = ModTool.Utils.Utils.MouseWorld.ToTileCoordinates();
                size.X = pos.X;
                size.Y = pos.Y;
                Switching = true;
            }

            if (Switching == false) return;

            UpdateSize();

            if (Main.mouseRight)
            {
                Switching = false;
                CombatText.NewText(Main.LocalPlayer.getRect(), Color.Red, "取消选择", true, false);
            }
            else if (Main.mouseLeft == false)
            {
                Switching = false;
                SetSize();
            }
        }

        private void SetSize()
        {
            size.Width -= size.X + 1;
            size.Height -= size.Y + 1;

            OnSetSize?.Invoke(size);
        }

        private void UpdateSize()
        {
            Point sizePos = ModTool.Utils.Utils.MouseWorld.ToTileCoordinates();
            size.Width = sizePos.X;
            size.Height = sizePos.Y;
        }

        private void DrawSwitchSize(SpriteBatch spriteBatch)
        {
            if (Switching == false) return;

            Common.DrawUtils.Draw_rectangle(size.X, size.Y, size.Width, size.Height, BorderColor, BackColor, 2);
        }

        public override void OnDeactivate()
        {
            Switching = false;
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
