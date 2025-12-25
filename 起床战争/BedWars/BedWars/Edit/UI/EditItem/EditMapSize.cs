using Microsoft.Xna.Framework;
using tContentPatch.Content.UI.ModSet;

namespace BedWars.Edit.UI.EditItem
{
    internal class EditMapSize : UIItemSwitch
    {
        public EditMapSize(string text) : base(null, text)
        {
            SetVal(false);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


        }
    }
}
