using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tContentPatch.Content.UI.ModSet;
using Terraria;

namespace BedWars.Edit.UI.EditItem
{
    internal class EditMapPos : UIItemSwitch
    {
        public EditMapPos(string text) : base(null, text)
        {
            SetVal(false);
        }

        private Point pos = Point.Zero;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (GetVal() == false) return;

            pos = Utils.ToTileCoordinates(Main.MouseWorld);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (GetVal() == false) return;

            Common.DrawUtils.Draw_rectangle(pos, pos, Color.Gainsboro, Color.FloralWhite);
        }
    }
}
