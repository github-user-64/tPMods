using Microsoft.Xna.Framework;
using Terraria;

namespace BedWars.Edit.UI.EditItem
{
    internal class EditMapPos : UISwitchPos
    {
        public EditMapPos(string text) : base(text) { }

        public override void SetPos(Point pos)
        {
            string ex = EditData.SetMapPos(pos);
            if (ex != null)
            {
                Main.NewText(ex);
                return;
            }

            CombatText.NewText(new Rectangle(pos.X, pos.Y, 0, 0), Color.LawnGreen, $"设置在{pos.X},{pos.Y}", true, false);
        }
    }
}
