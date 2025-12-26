using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace BedWars.Edit.UI
{
    internal class SwitchPos
    {
        public Action<Point> OnSet = null;
        public Action OnNoSet = null;
        public Point pos = Point.Zero;
        public bool Enable = false;

        public void Update()
        {
            if (Enable == false) return;
            if (Main.instance.ShouldUpdateEntities() == false) Enable = false;

            pos = Utils.ToTileCoordinates(ModTool.Utils.Utils.MouseWorld);

            if (Main.LocalPlayer?.mouseInterface == true) return;
            if (Main.mouseLeft)
            {
                Enable = false;

                OnSet?.Invoke(pos);
            }
            else if (Main.mouseRight)
            {
                Enable = false;

                OnNoSet?.Invoke();
            }
        }
    }
}
