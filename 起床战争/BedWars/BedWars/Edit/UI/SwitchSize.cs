using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace BedWars.Edit.UI
{
    internal class SwitchSize
    {
        public bool CanStartSwitch = false;
        public Action<Rectangle> OnSetSize = null;
        public Color BorderColor = Color.LawnGreen * 0.9f;
        public Color BackColor = Color.LawnGreen * 0.2f;
        public bool Switching { get; protected set; } = false;
        private Rectangle size = Rectangle.Empty;

        public void End()
        {
            Switching = false;
            CanStartSwitch = false;
        }

        public void Update()
        {
            if (Switching == false)
            {
                if (CanStartSwitch == false) return;

                Point pos = ModTool.Utils.Utils.MouseWorld.ToTileCoordinates();
                size.X = pos.X;
                size.Y = pos.Y;

                if (Main.LocalPlayer.mouseInterface) return;
                if ((Main.mouseLeft && Main.mouseLeftRelease) == false) return;

                Switching = true;
            }

            if (Switching == false) return;

            UpdateSize();

            if (Main.mouseRight && Main.mouseRightRelease)
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
            Point startTileP = new Point(Math.Min(size.X, size.Width), Math.Min(size.Y, size.Height));
            Point endTileP = new Point(Math.Max(size.X, size.Width), Math.Max(size.Y, size.Height));

            size.X = startTileP.X;
            size.Y = startTileP.Y;

            size.Width = endTileP.X - size.X + 1;
            size.Height = endTileP.Y - size.Y + 1;

            OnSetSize?.Invoke(size);
        }

        private void UpdateSize()
        {
            Point sizePos = ModTool.Utils.Utils.MouseWorld.ToTileCoordinates();
            size.Width = sizePos.X;
            size.Height = sizePos.Y;
        }

        public void DrawSwitch()
        {
            if (Switching == false)
            {
                if (CanStartSwitch == false) return;

                Common.DrawUtils.Draw_rectangle(size.X, size.Y, 1, 1, BorderColor, BackColor, 2);
                return;
            }

            Common.DrawUtils.Draw_rectangle(size.X, size.Y, size.Width, size.Height, BorderColor, BackColor, 2);
        }
    }
}
