using Microsoft.Xna.Framework;
using tContentPatch.Content.UI.ModSet;

namespace BedWars.Edit.UI.EditItem_Other
{
    internal class OpenWindowEditSpawItem : UIItemSwitch
    {
        public OpenWindowEditSpawItem(string text) : base(null, text)
        {
            OnValUpdate += v =>
            {
                Init.SwitchEditWindow_SpawItem(v);
            };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            SetVal(Init.editWindow_SpawItem?.IsOpen == true);
        }
    }
}
